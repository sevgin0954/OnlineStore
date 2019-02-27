using AutoMapper;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Account.BindingModels;
using OnlineStore.Models.WebModels.Account.ViewModels;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Models.WebModels.DeliveryInfo.BindingModels;
using OnlineStore.Models.WebModels.DeliveryInfo.ViewModels;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Models.WebModels.Session;
using Quest = OnlineStore.Models.WebModels.Quest.ViewModels;
using Security = OnlineStore.Models.WebModels.Security.ViewModels;

namespace OnlineStore.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<DeliveryInfo, DeliveryInfoViewModel>()
                .ReverseMap();

            this.CreateMap<DeliveryInfoBindingModel, DeliveryInfo>()
                .ForMember("DistrictId", opt => opt.MapFrom(src => src.SelectedDistrictId))
                .ForMember("PopulatedPlaceId", opt => opt.MapFrom(src => src.SelectedPopulatedPlaceId))
                .ReverseMap();

            this.CreateMap<PersonInfoBindingModel, User>()
                .ReverseMap();

            this.CreateMap<User, Security.IndexViewModel>();

            this.CreateMap<Category, CategoryViewModel>();

            this.CreateMap<ProductBindingModel, Product>()
                .ForMember(m => m.Photos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(m => m.Photos, opt => opt.Ignore());

            this.CreateMap<Product, Quest.ProductConciseViewModel>();

            this.CreateMap<Product, ProductShoppingCartViewModel>()
                .ForMember(dest => dest.MainPhoto, opt => opt.Ignore());

            this.CreateMap<Product, ProductSessionModel>()
                .ForMember("ProductId", opt => opt.MapFrom(src => src.Id));
        }
    }
}
