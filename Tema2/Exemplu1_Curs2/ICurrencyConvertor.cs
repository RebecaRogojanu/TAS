using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_Curs3
{
    public interface ICurrencyConvertor
    {
        float EuroInRon(float sumaInEur);
        float RonInEuro(float sumaInRon);
    }
}
