using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP_ExternalProperty.Model
{
    public class AssemblyDescription
    {
        public string Discription { get; set; }
        public string Code { get; set; }
        public AssemblyDescription() 
        {
            
        }
        public AssemblyDescription(string Code, string Discription)
        {
            this.Code = Code;
            this.Discription = Discription;
        }
    }
}
