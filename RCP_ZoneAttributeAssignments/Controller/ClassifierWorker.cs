using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM = Tekla.Structures.Model;

namespace RCP_ExternalProperty.Controller
{
    public class ClassifierWorker
    {
        public List<CodeWorker> CodeWorkers { get; set; }
        public ClassifierWorker() 
        {
            
        }
        public ClassifierWorker(bool metka)
        {
            if (metka)
            {
                Classifier classifier = new Classifier();
                string code = "Assembly Code";
                string description = "Assembly Description";

                CodeWorkers = new List<CodeWorker>()
                {
                    //TODO: вынести данные настройки в xml.
                    new CodeWorker(classifier.AssemblyCodes[1],code,description, new Model.CodeFilter(new List<string>(){"4","71" }) ),
                    new CodeWorker(classifier.AssemblyCodes[2],code,description, new Model.CodeFilter(new List<string>(){"6"}) ),
                    new CodeWorker(classifier.AssemblyCodes[5],code,description, new Model.CodeFilter(new List<string>(){"2106","2107"}) ),
                    new CodeWorker(classifier.AssemblyCodes[7],code,description, new Model.CodeFilter(new List<string>(){"2100"}) ),
                    new CodeWorker(classifier.AssemblyCodes[0],code,description, new Model.CodeFilter(new List<string>(){ }) )
                };
            }
        }

        public void CheckEnumrator(TSM.ModelObjectEnumerator modelObjectEnumerator) 
        {
            if (CodeWorkers != null)
            {
                if (CodeWorkers.Count > 0)
                {
                    while (modelObjectEnumerator.MoveNext())
                    {
                        if (modelObjectEnumerator.Current is TSM.Part part)
                        {
                            foreach (var codeWorker in CodeWorkers)
                            {
                                if (codeWorker.CheckClassPart(part))
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            
        }
    }
}
