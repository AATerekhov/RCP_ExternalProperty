using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCP_ExternalProperty.Model;

namespace RCP_ExternalProperty.Controller
{
    public class Classifier
    {
        public List<AssemblyDescription> AssemblyCodes { get; set; }
        public Classifier() 
        {
            AssemblyCodes = new List<AssemblyDescription>()
            {
                new AssemblyDescription("ОС.КЭ.4.1","Стальная балка"),//0
                new AssemblyDescription("ОС.КЭ.4.2","Стальная стойка"),//1
                new AssemblyDescription("ОС.КЭ.4.3","Ферма стальная"),//2
                new AssemblyDescription("ОС.КЭ.4.4","Настил металлический"),//3
                new AssemblyDescription("ОС.КЭ.4.5","Рамная конструкция"),//4
                new AssemblyDescription("ОС.КЭ.4.6","Фасонка металлическая"),//5
                new AssemblyDescription("ОС.КЭ.4.7","Жёсткая арматура"),//6
                new AssemblyDescription("ОС.КЭ.4.8","Закладная деталь"),//7
                new AssemblyDescription("ОС.КЭ.4.9","Арматурный каркас"),//8
                new AssemblyDescription("ОС.КЭ.4.10","Стержневая арматура"),//9
            };
        }
    }
}
