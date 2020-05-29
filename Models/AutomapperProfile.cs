using AutoMapper;
using EHealthConsult.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ConsultantsVM, Consultant>()
                .ReverseMap();
        }
    }
}
