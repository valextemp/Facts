using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastruture.Mappers.Base;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.AspNetCore.Controllers;
using Calabonga.PagedListCore;
using AutoMapper;

namespace Calabonga.Facts.Web.Infrastruture.Mappers
{
    public class TagMapperConfiguration : MapperConfigurationBase
    {
        public TagMapperConfiguration()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag,TagUpdateViewModel>();
            CreateMap<TagUpdateViewModel, Tag>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Facts, o => o.Ignore())
                ;

            //Для пейджинга фактов
            CreateMap<IPagedList<Tag>, IPagedList<TagViewModel>>().ConvertUsing<PagedListConverter<Tag, TagViewModel>>();
        }
    }


}
