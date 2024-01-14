using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastruture.Mappers.Base;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.AspNetCore.Controllers;
using Calabonga.PagedListCore;
using AutoMapper;

namespace Calabonga.Facts.Web.Infrastruture.Mappers
{
    public class FactMapperConfiguration:MapperConfigurationBase
    {
        public FactMapperConfiguration()
        {
            CreateMap<Fact, FactViewModel>();
            CreateMap<FactCreateViewModel, Fact>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Tags, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedBy, o => o.Ignore())
                ;
            CreateMap<Fact,FactUpdateViewModel>();
            CreateMap<FactUpdateViewModel, Fact>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Tags, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedBy, o => o.Ignore())
                ;

            //Для пейджинга фактов
            CreateMap<IPagedList<Fact>, IPagedList<FactViewModel>>().ConvertUsing<PagedListConverter<Fact, FactViewModel>>();
        }
    }


}
