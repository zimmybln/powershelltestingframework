using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellSampleComponents.Data
{
    public partial class Contact
    {
        internal string SearchValue { get; set; }
        
        public static implicit operator Contact(int id)
        {
            return new Contact() { Id = id };
        }

        public static implicit operator Contact(string searchValue)
        {
            return new Contact() {SearchValue = searchValue};
        }
    }
}
