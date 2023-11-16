using System;
using Tema3;
using Moq;

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
            if (Zero(cantitate))
                throw new ZeroException();
            else if (Negativ(cantitate))
                throw new NegativException();
            else
            {
                destinatie.Deposit(cantitate);
                Retragere(cantitate);
            }
        }
        public void TranferDinEuroInLei(Cont destinatie, float sumaInEur, ICurrencyConvertor convertor)
        {
            float sumaInRon = convertor.EuroInRon(sumaInEur);
            if (Zero(sumaInRon))
                throw new ZeroException();
            else if (Negativ(sumaInRon))
                throw new NegativException();
            else if (Balanta - sumaInRon > MinBalanta)
            {
                destinatie.Deposit(sumaInRon);
                Retragere(sumaInRon);
            }
            else throw new NotEnoughFundsException();
            
        }
        public bool Negativ(float valoare)
        {
            if (valoare < 0)
            {  //throw new ArgumentException("Cantitatea trebuie să fie pozitivă.");
                return true;
            }
            return false;
        }
        public bool Zero(float valoare)
        {
            if (valoare == 0)
                return true;
            return false;

        }
    }
    public class NotEnoughFundsException : ApplicationException
    {

    }
    public class NegativException : ApplicationException
    {

    }
    public class ZeroException : ApplicationException
    {

    }
}