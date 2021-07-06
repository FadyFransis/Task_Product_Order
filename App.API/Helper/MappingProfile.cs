using AutoMapper;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Models;
using System;
using System.Linq;
using System.Reflection;
using App.API.DTOs;

namespace App.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            var asm = Assembly.Load("App.Core");
            var classes =
                asm.GetTypes().Where(p =>
                    p.Namespace != null && (p.Namespace.Equals("App.Core.Entities") || p.Namespace.Equals("App.Core.Entities.Lookups")) &&
                    p.IsClass
                    && (p.IsSubclassOf(typeof(BaseEntity)) ||
                        p.IsSubclassOf(typeof(BaseNameEntity)) ||
                        p.IsSubclassOf(typeof(CommonBaseNameBusinessEntity)))

                ).ToList();
            foreach (Type c in classes)
            {
                //CreateMap(c, c).ReverseMap();
                CreateMap(c, c)
                        .ForMember("CreationDate", act => act.Ignore())
                        .ForMember("LastUpdatedDate", act => act.Ignore());
                //.ForMember("RecordStatus", act => act.Ignore());
            }
            // map entity with model
            CreateMap<AppUser, AppUserModel>().ReverseMap();
          
           
            CreateMap<Product, ProductModel>().ReverseMap(); 
            CreateMap<Order, OrderModel>().ReverseMap().ForMember(x => x.User, act => act.Ignore());
            CreateMap<OrderItem, OrderItemModel>().ReverseMap().ForMember(x => x.Product, act => act.Ignore());
          
            
            // map model with dto
            CreateMap<AppUserDTO, AppUserModel>().ReverseMap();
          
          
            CreateMap<ProductBaseDTO, ProductModel>().ReverseMap();
            CreateMap<ProductDetailsDTO, ProductModel>().ReverseMap();
          
            CreateMap<OrderDTO, OrderModel>().ReverseMap();
            CreateMap<OrderLookupDTO, OrderModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemModel>().ReverseMap();
            CreateMap<OrderItemLookupDTO, OrderItemModel>().ReverseMap();
            CreateMap<ProductDetailsDTO, ProductModel>().ReverseMap();
           
            CreateMap<EditOrderDTO, OrderModel>().ReverseMap();
          
        }
    }
}
