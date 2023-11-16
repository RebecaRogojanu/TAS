using banca;
using NUnit.Framework;

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

    [Test]
    [Category("Pass")]
        public void Test1()
        {
            //act
            sursa.Transfer(destinatie, 90.00F);                           //TransferFunds e motoda pe care vreau eu sa o testez
                                                                          //Transferam 90 si verificam conturile

            //assert
            Assert.AreEqual(240.00F, destinatie.Balanta);                 //Verificam daca 150+90 este 240
            Assert.AreEqual(110.00F, sursa.Balanta);                      //Verificam daca 200-90 este 110
        }
    [Test]
    [Category("Fail")]

        public void TestNeg()
        {
            //act
            sursa.Transfer(destinatie, -20);
            //assert
            Assert.AreEqual(150.00F, destinatie.Balanta);                 //Verificam daca transferurile nu s-au realizat
            Assert.AreEqual(200.00F, sursa.Balanta);
    }

        [Test]
        [TestCase(200, 0, 50)]                                            //200 in primult cont, 0 in al doilea, tranferam 78
        [TestCase(200, 0, 189)]                                           //189 - valoare limita - DomainTesting
        [TestCase(200, 0, 1)]                                             //1 - valoare limita
        [TestCase(200, 0, 190)]                                           //190 - valoarea limita + 1
        [TestCase(200, 0, 0)]                                             //0 - valoarea limita -1
        //Nu ne lasa minBalance, decat sa transferam valori intre [1, 189]
    public void TransferMinFonduri(int a, int b, int c)
    {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(a);
            Cont destinatie = new Cont();
            destinatie.Deposit(b);

            //act
            sursa.TransferMinFonduri(destinatie, c);

            //assert
            Assert.AreEqual(c, destinatie.Balanta);
    }

        [Test]
        [TestCase(100, 0, -10)]
        [TestCase(100, 0, 20)]
    public void TransferValoareaNeg_MinFunc(int a, int b, int c)
    {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(a);
            Cont destinatie = new Cont();
            destinatie.Deposit(b);

            //act
            sursa.TransferMinFonduri(destinatie, c);

            //assert
            Assert.AreEqual(c, destinatie.Balanta);
    }
        [Test]
        [TestCase(100, 0, -10)]
        [TestCase(100, 0, 20)]
     public void TransferValoareaNeg(int a, int b, int c)
     {
            //arrange
            Cont sursa = new Cont();
            sursa.Deposit(a);
            Cont destinatie = new Cont();
            destinatie.Deposit(b);

            //act
            sursa.Transfer(destinatie, c);

            //assert
            Assert.AreEqual(c, destinatie.Balanta);
     }
    }
}