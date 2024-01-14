using card;
using carte;
using Moq;

namespace librarie
{
    public class TesteMock
    {
        public Biblioteca biblioteca;
        public ICardBiblioteca cardBiblioteca;
        public ICarte carte, carte1, carte2, carte3, carte4;
        public string cheie, cheie1, cheie2, cheie3, cheie4;
        [SetUp]
        public void Setup()
        {
            //arrange
            biblioteca = new Biblioteca();
            cardBiblioteca = new CardBiblioteca("Flavia");
            carte1 = new Carte("Ion", "Liviu Rebreanu", 10, 1920, "Roman social");
            cheie1 = carte1.Titlu + carte1.Autor;
            carte2 = new Carte("De veghe in lanul de secara", "J.D.Salinger", 1, 1951, "Fictiune");
            cheie2 = carte2.Titlu + carte2.Autor;
            carte3 = new Carte("Casa Bantuita", "Shirley Jackson", 3, 1959, "Roman gotic");
            cheie3 = carte3.Titlu + carte3.Autor;
            carte4 = new Carte("Fluturi", "Irina Binder", 0, 2013, "Fictiune");
            cheie4 = carte4.Titlu + carte4.Autor;
            biblioteca.carti.Add(cheie1, carte1);
            biblioteca.carti.Add(cheie2, carte2);
            biblioteca.carti.Add(cheie3, carte3);
            biblioteca.carti.Add(cheie4, carte4);
        }

        [Test]
        [Category("Pass")]
        public void AdaugareCarte()
        {
            //arrange
            Mock<ICarte> carteMock = new Mock<ICarte>();
            carteMock.Setup(_ => _.Titlu).Returns("Napasta");
            carteMock.Setup(_ => _.Autor).Returns("I. L. Caragiale");
            carteMock.Setup(_ => _.NumarExemplare).Returns(10);
            carteMock.Setup(_ => _.AnPublicare).Returns(1890);
            carteMock.Setup(_ => _.Categorie).Returns("Dramaturgie");

            //act
            bool val = biblioteca.AdaugaCarte(carteMock.Object);

            //assert
            Assert.IsTrue(val);
            Assert.IsTrue(biblioteca.carti.ContainsKey("NapastaI. L. Caragiale"));
        }

        [Test]
        [Category("Pass")]
        public void AdaugareCarteExistenta()
        {
            Mock<ICarte> carteMock = new Mock<ICarte>();
            carteMock.Setup(_ => _.Titlu).Returns("Casa Bantuita");
            carteMock.Setup(_ => _.Autor).Returns("Shirley Jackson");
            carteMock.Setup(_ => _.NumarExemplare).Returns(9);
            carteMock.Setup(_ => _.AnPublicare).Returns(1959);
            carteMock.Setup(_ => _.Categorie).Returns("Roman gotic");

            //act
            bool val = biblioteca.AdaugaCarte(carteMock.Object);

            //assert
            Assert.IsFalse(val);
            Assert.AreEqual(3, biblioteca.carti["Casa BantuitaShirley Jackson"].NumarExemplare);                //Asteptare cantitate initiala
        }


        //Testare functie ImprumutaCarte()
        [Test]
        [Category("Pass")]
        [TestCase("IonLiviu Rebreanu")]
        //[TestCase("De veghe in lanul de secaraJ.D.Salinger")]                                                
        public void CarteImprumutata(string cheie)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            Mock<ICardBiblioteca> cardMock = new Mock<ICardBiblioteca>();

            //act
            bool rezultat = biblioteca.ImprumutaCarte(cheie, cardMock.Object);

            //assert
            Assert.IsTrue(rezultat);
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            cardMock.Verify(_ => _.CarteImprumutata(It.Is<ICarte>(c => c.Titlu == "Ion" && c.Autor == "Liviu Rebreanu")), Times.Once());

        }
        [Test]
        [Category("Pass")]
        [TestCase("IonLiviu Rebreanu")]                                                                         //Pass
        //[TestCase("MaraIoan Sadoveanu")]                                                                      //Fail - carte nu exista in biblioteca
        public void CarteReturnata(string cheie)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            var cardMock = new Mock<ICardBiblioteca>();
            
            //act
            bool rezultat = biblioteca.ReturneazaCarte(cheie, cardMock.Object);

            //assert
            Assert.AreEqual(cantitate_initiala + 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.IsTrue(rezultat);
            cardMock.Verify(_ => _.ReturneazaCarte(It.Is<ICarte>(c => c.Titlu == "Ion" && c.Autor == "Liviu Rebreanu")), Times.Once());
        }

        [Test]
        [TestCase("IonLiviu Rebreanu", 10)]                                                                      //Pass
        //[TestCase("IonLiviu Rebreanu", 1)]                                                                     //Pass
        //[TestCase("AnaEm Sava", 3)]                                                                            //Neinregistrata
        //[TestCase("FluturiIrina Binder", 0)]                                                                   //Cantitate zero
        //[TestCase("IonLiviu Rebreanu", 11)]                                                                    //Stoc insuficient

        public void CarteCumparata(string cheie, int cantitate)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            var cardMock = new Mock<ICardBiblioteca>();
            cardMock.Setup(_ => _.NumeDetinator).Returns("Ana");

            //act
            biblioteca.CumparaCarte(cheie, cantitate, cardMock.Object);

            //assert
            Assert.IsTrue(biblioteca.carti.ContainsKey(cheie));
            Assert.AreEqual(cantitate_initiala - cantitate, biblioteca.carti[cheie].NumarExemplare);
            cardMock.Verify(_ => _.AdaugaPuncteBonus(5*cantitate), Times.Once()); 
        }

        [Test]
        [TestCase("IonLiviu Rebreanu", 150, 11)]
        //[TestCase("IonLiviu Rebreanu", 151, 15)]
        //[TestCase("IonLiviu Rebreanu", 200, 15)]
        //[TestCase("IonLiviu Rebreanu", 149, 15)]
        //[TestCase("IonLiviu Rebreanu", 100, 15)]
        public void CarteCumparataCuPuncte(string cheie, int puncte, int pret)
        {
            var cardMock = new Mock<ICardBiblioteca>();
            cardMock.Setup(_ => _.NumeDetinator).Returns("Ana");
            cardMock.Setup(_ => _.PuncteBonus).Returns(puncte);
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;

            //act
            biblioteca.CumparaCarteCuPuncte(cheie, pret, cardMock.Object);

            //assert
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            cardMock.Verify(_ => _.ScadePuncteBonus(pret * 10), Times.Once());
        }
    }
}