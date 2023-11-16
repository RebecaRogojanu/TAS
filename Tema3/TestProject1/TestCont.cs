using banca;
using Moq;
using NUnit.Framework;
using Tema3;

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
        //TestStub
        [Test]
        [TestCase(200,150,10,5)]
        [TestCase(100, 53.8F, 12, 4.96F)]
        [TestCase(101, 0, 18, 5)]
        [TestCase(100, 150, 18, 5)]
        [TestCase(50, 50, 0, 4.95F)]
        [TestCase(101, 10, -10, 5)]
        public void TransferMoq(float sumaSursa, float sumaDestinatar, float sumaTrimisa, float curs)
        {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(sumaSursa);
            Cont destinatie = new Cont();
            destinatie.Deposit(sumaDestinatar);
            var convertorMock = new Mock<ICurrencyConvertor>();
            convertorMock.Setup(_ => _.EuroInRon(sumaTrimisa)).Returns(sumaTrimisa * curs);
            
            //act
            sursa.TranferDinEuroInLei(destinatie, sumaTrimisa, convertorMock.Object);

            //assert
            Assert.AreEqual(sumaDestinatar + curs * sumaTrimisa, destinatie.Balanta);
            Assert.AreEqual(sumaSursa - curs * sumaTrimisa, sursa.Balanta);
            convertorMock.Verify(_ => _.EuroInRon(sumaTrimisa), Times.Once());              //verify behavior
        }
    }

    public class CurrencyConvertorStub : ICurrencyConvertor
    {
        private float rata;

        public CurrencyConvertorStub(float rate)
        {
            this.rata = rate;
        }
        public float EuroInRon(float sumaInEur)
        {
            return sumaInEur * rata;
        }
        public float RonInEuro(float sumaInRon)
        {
            return sumaInRon / rata;
        }
    }
}