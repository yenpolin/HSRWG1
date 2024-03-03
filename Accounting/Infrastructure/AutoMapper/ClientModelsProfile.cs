using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.Models;
using AutoMapper;
using MoonCakeOrder.Application.ClientModels;

namespace MoonCakeOrder.Infrastructure.AutoMapper
{
    public class ClientModelsProfile: Profile
    {
        public ClientModelsProfile(){
            CreateMap<Value, ValueClientModel>();
        }
        
    }
}