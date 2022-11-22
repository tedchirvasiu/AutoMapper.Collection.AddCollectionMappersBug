using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SomeExampleLibrary {
    public class SomeMappingProfile : Profile {

        public SomeMappingProfile() {

            CreateMap<SomeEntity, SomeDto>();
        }

    }
}
