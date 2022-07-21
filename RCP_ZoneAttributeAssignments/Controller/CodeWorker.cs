using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCP_ExternalProperty.Model;
using TSM = Tekla.Structures.Model;

namespace RCP_ExternalProperty.Controller
{
    public class CodeWorker
    {
        public AssemblyDescription InAssemblyDescription { get; set; }
        public string AssemblyCodeKey { get; set; }
        public string AssemblyDescriptionKey { get; set; }

        public CodeFilter  InCodeFilter { get; set; }



        public CodeWorker(AssemblyDescription InAssemblyDescription, string AssemblyCodeKey, string AssemblyDescriptionKey, CodeFilter InCodeFilter) 
        {
            this.InAssemblyDescription = InAssemblyDescription;
            this.AssemblyCodeKey = AssemblyCodeKey;
            this.AssemblyDescriptionKey = AssemblyDescriptionKey;
            this.InCodeFilter = InCodeFilter;
        }

        public bool CheckClassPart(TSM.Part part) 
        {
            string partClass = part.Class;

            if (InCodeFilter.Check(partClass))
            {
                part.SetUserProperty(AssemblyCodeKey, InAssemblyDescription.Code);
                part.SetUserProperty(AssemblyDescriptionKey, InAssemblyDescription.Discription);
                return true; 
            }

            return false;
        }
    }
}
