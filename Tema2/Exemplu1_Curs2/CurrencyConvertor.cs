using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema2_Curs3
{
    public class CurrencyConvertor: ICurrencyConvertor
    {
        private float curs;

        public CurrencyConvertor(float curs)
        {
            this.curs = curs;
        }
        public float EuroInRon(float sumaInEur)
        {
            return sumaInEur * curs ;
        }
        public float RonInEuro(float sumaInRon) { 
            return sumaInRon / curs;
        }
    }
}
