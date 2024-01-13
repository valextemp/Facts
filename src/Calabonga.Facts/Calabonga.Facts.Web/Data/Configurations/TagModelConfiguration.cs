using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Facts.Web.Data.Configurations
{
    public class TagModelConfiguration: IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(x => x.Id);
          
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.HasIndex(x => x.Name); //Пример создания индекса

            //builder.HasMany(x => x.Facts).WithMany(x => x.Tags); //Поскольку в Facts связь уже указана, то здесь прописывать связь не нужно
        }
    }
}
