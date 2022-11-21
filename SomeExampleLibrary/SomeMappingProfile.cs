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

            CreateMap<SomeEntity, SomeDto>()
                //This line seems to cause trouble
                .ForMember(dto => dto.Ids, m => m.MapFrom(entity => entity.Elements.Select(e => e.Id)));

        }

    }
}
