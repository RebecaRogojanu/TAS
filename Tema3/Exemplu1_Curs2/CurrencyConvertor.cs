using banca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema3
{
    public class CurrencyConvertor: ICurrencyConvertor
    {
        private float curs;

        public CurrencyConvertor(float curs)
        {
            if (Zero(curs))
                throw new ZeroException();
            this.curs = curs;
        }
        public float EuroInRon(float sumaInEur)
        {
            return sumaInEur * curs;
        }
        public float RonInEuro(float sumaInRon)
        {
            return sumaInRon / curs;
        }



        public bool Zero(float valoare)
        {
            if (valoare == 0)
                return true;
            return false;

        }
    }
}
