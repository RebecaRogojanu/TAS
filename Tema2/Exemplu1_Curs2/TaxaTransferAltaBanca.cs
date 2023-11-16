using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_Curs3
{
    public class TaxaTransferAltaBanca: ITaxaTranferAltaBanca
    {
        private float taxa;

        public TaxaTransferAltaBanca(float taxa)
        {
            this.taxa = taxa;
        }
        public float TaxaTransfer(float cantitate)
        {
            return (cantitate + taxa);
        }
    }
}
