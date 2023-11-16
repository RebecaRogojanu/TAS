using banca;
using NUnit.Framework;
using Tema2_Curs3;

namespace banca
{
public class Teste
{
        Cont sursa;
        Cont destinatie;
    [SetUp]
    public void Setup()
    {
            //arrange
            sursa = new Cont();
            sursa.Deposit(200.00F);
            destinatie = new Cont();
            destinatie.Deposit(150.00F);
    }

        //Injectare prin Parametru
        [Test]
        public void TransferFundsFromEurAccount()
        {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(100);
            Cont destinatie = new Cont();
            destinatie.Deposit(200);
            var curs = 5;

            //act
            sursa.TranferFonduriDinEuro_versiune1(destinatie, 10, curs);

            //assert
            Assert.AreEqual(250, destinatie.Balanta);
            Assert.AreEqual(50, sursa.Balanta);
        }
        //TestStub
        [Test]
        public void TransferFundsFromEurAccount2()
        {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(100);
            Cont destinatie = new Cont();
            destinatie.Deposit(200);
            var convertor = new CurrencyConvertorStub(5);         //

            //act
            sursa.TranferFonduriDinEuro_versiune2(destinatie, 10, convertor);

            //assert
            Assert.AreEqual(250, destinatie.Balanta);
            Assert.AreEqual(50, sursa.Balanta);
        }
        [Test]
        public void TransferFundsFromRonAccount()
        {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(100);                          
            Cont destinatie = new Cont();
            destinatie.Deposit(200);
            var convertor = new CurrencyConvertorStub(5);         

            //act
            sursa.TranferFunduriDinRonInEuro(destinatie, 10, convertor);                   

            //assert
            Assert.AreEqual(202, destinatie.Balanta);
            Assert.AreEqual(98, sursa.Balanta);
        }
        [Test]
        public void TranferCatreAltaBanca()
        {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(150);
            Cont destinatie = new Cont();
            destinatie.Deposit(100);
            var taxaTransfer = new TaxaTransferAltaBancaStub(5);

            //act
            sursa.TranferCatreAltaBanca(destinatie, 100, taxaTransfer);

            //assert
            Assert.AreEqual(200, destinatie.Balanta);
            Assert.AreEqual(45, sursa.Balanta);
        }
    }

    public class CurrencyConvertorStub : ICurrencyConvertor
    {
        private float curs;

        public CurrencyConvertorStub(float curs)
        {
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
    }

    public class TaxaTransferAltaBancaStub : ITaxaTranferAltaBanca
    {
        private float taxa;

        public TaxaTransferAltaBancaStub(float taxa)
        {
            this.taxa = taxa;
        }
        public float TaxaTransfer(float cantitate)
        {
            return (cantitate + taxa);
        }
    }
}