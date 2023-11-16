using System;

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
        public void Transfer(Cont destinatie, float cantitate) {
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
        public Cont TransferMinFonduri(Cont destinatie, float cantitate) {
            if(Zero(cantitate))
                throw new ZeroException();
            else if (Negativ(cantitate))
                throw new NegativException();
            else if (Balanta - cantitate > MinBalanta)
            {
                destinatie.Deposit(cantitate);
                Retragere(cantitate);
            }
            else throw new NotEnoughFundsException();

            return destinatie;
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
    }
    public class  NotEnoughFundsException : ApplicationException
    {
        
    }
    public class NegativException : ApplicationException
    {

    }
    public class ZeroException : ApplicationException
    {

    }
}