using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP_ExternalProperty.Model
{
    public class CodeFilter
    {
        public List<string> FirstClassName { get; set; }

        public CodeFilter(List<string> FirstClassName) 
        {
            this.FirstClassName = FirstClassName;
        }

        public bool Check(string value) 
        {
            if (FirstClassName.Count == 0)
            {
                return true;
            }

            foreach (var filter in FirstClassName)
            {
                var filterLight = filter.Length;

                if (value.Length > filterLight)
                {
                    var valueTesting = value.Substring(0, filterLight);

                    if (filter.CompareTo(valueTesting) == 0)
                    {
                        return true;
                    }
                }
                else if (value.Length == filterLight)
                {
                    if (filter.CompareTo(value) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
