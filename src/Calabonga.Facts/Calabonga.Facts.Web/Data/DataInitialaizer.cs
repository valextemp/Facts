using Calabonga.Facts.Web.Infrastruture;
using Calabonga.Microservices.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Calabonga.Facts.Web.Data
{
    public static class DataInitialaizer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExists = context!.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator && await databaseCreator.ExistsAsync();
            
            if (isExists) //Если БД существует, то ничем наполнять не нужно
            {
                return;
            }

            //Если не существует, то начинаем эту ветку
            await context!.Database.MigrateAsync();

            var roles=AppData.Roles.ToArray();
            //var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            foreach (var role in roles) 
            {
                //if (!context.Roles.Any(x=>x.Name==role))
                //{
                //    await roleStore.CreateAsync(new IdentityRole()
                //    {
                //        NormalizedName = role.ToUpper()
                //    });
                //}

                if (!context.Roles.Any(x => x.Name == role))
                {

                    await roleManager!.CreateAsync(new IdentityRole(role));

                }

            }

            const string username = "valex@mail.ru";
            if (context.Users.Any(x=> x.Email == username)) 
            {
                return; //если такой пользователь есть, то ничего не делаем
            }

            var user = new IdentityUser
            {
                Email = username,
                EmailConfirmed = true,
                NormalizedEmail = username.ToUpper(),
                PhoneNumber = "+79000000000",
                UserName = username,
                PhoneNumberConfirmed = true,
                NormalizedUserName = username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),

            };

            //Работа по добавлению Хеша от пароля
            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "!1qQ!1Qq");

            var userStore=new UserStore<IdentityUser>(context);
            var identityResult=await userStore.CreateAsync(user);
            if (!identityResult.Succeeded) 
            {
                var message = string.Join("' ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new MicroserviceDatabaseException();
            }

            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            //await userManager!.AddToRolesAsync(user, roles); //Не сработало, надо переписать по старому
            foreach (var role in roles)
            {
                var identityResultRole = await userManager!.AddToRoleAsync(user, role);
                if (!identityResultRole.Succeeded) 
                {
                    var message = string.Join("' ", identityResultRole.Errors.Select(x => $"{x.Code}: {x.Description}"));
                    throw new MicroserviceDatabaseException();
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
