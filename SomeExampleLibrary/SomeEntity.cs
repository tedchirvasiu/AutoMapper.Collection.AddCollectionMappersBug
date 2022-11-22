using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExampleLibrary {
    public class SomeEntity {

        public virtual ICollection<string> Elements { get; set; }

    }
}
