using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema3
{
    public interface ICurrencyConvertor
    {
        float EuroInRon(float sumaInEur);
        float RonInEuro(float sumaInRon);
    }
}
