using AutoMapper;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Account.BindingModels;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using DeliveryInfoBinding = OnlineStore.Models.WebModels.DeliveryInfo.BindingModels;
using OnlineStore.Models.WebModels.DeliveryInfo.ViewModels;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Models.WebModels.Session;
using Quest = OnlineStore.Models.WebModels.Quest.ViewModels;
using OrderBindingModels = OnlineStore.Models.WebModels.OrderModels.BindingModels;
using Security = OnlineStore.Models.WebModels.Security.ViewModels;
using OnlineStore.Models.WebModels.OrderModels.BindingModels;
using System.Linq;
using OnlineStore.Models.WebModels.OrderModels.ViewModels;
using OnlineStore.Models.WebModels.ProductModels.ViewModels;

namespace OnlineStore.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<DeliveryInfo, DeliveryInfoViewModel>()
                .ReverseMap();

            this.CreateMap<DeliveryInfoBinding.DeliveryInfoBindingModel, DeliveryInfo>()
                .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.SelectedDistrictId))
                .ForMember(dest => dest.PopulatedPlaceId, opt => opt.MapFrom(src => src.SelectedPopulatedPlaceId))
                .ReverseMap();

            this.CreateMap<DeliveryInfo, OrderBindingModels.DeliveryInfoBindingModel>()
                .ForMember(dest => dest.SelectedDistrictName, opt => opt.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.SelectedPopulatedName, opt => opt.MapFrom(src => src.PopulatedPlace.Name));

            //--------------------------------------------------------------------------------------------------------

            this.CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<SubCategory, ProductBindingModel>()
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Photos, opt => opt.Ignore());

            //--------------------------------------------------------------------------------------------------------

            this.CreateMap<ProductBindingModel, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name));

            this.CreateMap<Product, Quest.ProductConciseViewModel>();

            this.CreateMap<Product, ProductShoppingCartViewModel>()
                .ForMember(dest => dest.MainPhoto, opt => opt.Ignore());

            this.CreateMap<Product, ProductSessionModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Product, ProductConciseBindingModel>()
                .ForMember(dest => dest.MainPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.Orders.Sum(o => o.Count)));

            this.CreateMap<Product, FavoriteProductViewModel>();

            //--------------------------------------------------------------------------------------------------------

            this.CreateMap<User, UsersViewModel>()
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.Orders.Count));

            this.CreateMap<PersonInfoBindingModel, User>()
                .ReverseMap();

            this.CreateMap<User, Security.IndexViewModel>();

            //---------------------------------------------------------------------------------------------------------

            this.CreateMap<Order, MyOrderConciseViewModel>()
                .ForMember(dest => dest.OrderStatusName, opt => opt.MapFrom(src => src.OrderStatus.Name));
        }
    }
}
