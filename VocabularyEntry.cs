using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vokabeltrainer
{
    public class VocabularyEntry(int iD, string dE, string eN, string fR)
    {
        public int ID { get; private set; } = iD;
        public string DE { get; set; } = dE;
        public string EN { get; set; } = eN;
        public string FR { get; set; } = fR;
        public string GetAttr(string property_name)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property.Name == property_name)
                    return property.GetValue(this).ToString();
            }
            return "";
        }
    }
}
