using System;
using Tema2_Curs3;

namespace banca
{
    public class Cont
    {
        private float balanta;
        private float minBalanta = 10;

        public Cont() {
            balanta = 0;
        }
        public Cont(int valuare) {
            balanta = valuare;
        }
        public float Balanta
        {
            get { return balanta; }
        }
        public float MinBalanta
        {
            get { return minBalanta; }
        }
        public void Deposit(float cantitate) {
            if(!Negativ(cantitate))
                 balanta += cantitate;
        }
        public void Retragere(float cantitate)
        {
            if (!Negativ(cantitate))
                balanta -= cantitate;  
        }
        public void Transfer(Cont destinatie, float cantitate)
        {
                destinatie.Deposit(cantitate);              //Test failed amount+1
                Retragere(cantitate);
        }
        private bool Negativ(float valoare)
        {
            if (valoare < 0)
            {  //throw new ArgumentException("Cantitatea trebuie să fie pozitivă.");
                return true;
            }
            return false;
        }
        private bool Zero(float valoare)
        {
            if (valoare == 0)
                return true;
            return false;

        }
        public void TranferFonduriDinEuro_versiune1(Cont destinatie, float sumaInEuro, float curs) { 
            float sumaInRon = sumaInEuro * curs;
            destinatie.Deposit(sumaInRon);
            Retragere(sumaInRon);
        }
        public void TranferFonduriDinEuro_versiune2(Cont destinatie, float sumaInEuro, ICurrencyConvertor convertor)
        {
            float sumaInRon = convertor.EuroInRon(sumaInEuro);
            destinatie.Deposit(sumaInRon);
            Retragere(sumaInRon);
        }
        public void TranferFunduriDinRonInEuro(Cont destinatie, float sumaInRon, ICurrencyConvertor convertor)
        {
            float sumaInEuro = convertor.RonInEuro(sumaInRon);
            destinatie.Deposit(sumaInEuro);
            Retragere(sumaInEuro);
        }
        public void TranferCatreAltaBanca(Cont destinatie, float cantitate, ITaxaTranferAltaBanca taxaTransfer)
        {
            float sumaTransferata = taxaTransfer.TaxaTransfer(cantitate);
            destinatie.Deposit(cantitate);
            Retragere(sumaTransferata);
        }
    }
    public class NegativException : ApplicationException
    {

    }
    public class ZeroException : ApplicationException
    {

    }
}