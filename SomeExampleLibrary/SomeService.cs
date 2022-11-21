using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExampleLibrary {
    public class SomeService {

        IMapper Mapper { get; set; }

        /// <summary>
        /// Attempting to inject IMapper will cycle forever
        /// </summary>
        /// <param name="mapper"></param>
        public SomeService(IMapper mapper) {
            Mapper = mapper;
        }
    }
}
