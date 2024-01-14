using card;
using carte;

namespace librarie
{
    public class Teste
    {
        public Biblioteca biblioteca;
        public CardBiblioteca cardBiblioteca;
        public Carte carte, carte1, carte2, carte3, carte4, carte5;
        public string cheie, cheie1, cheie2, cheie3, cheie4, cheie5;
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
            carte5 = new Carte("Mara", "Ioan Sadoveanu", 4, 1894, "Clasice");
            cheie5 = carte5.Titlu + carte5.Autor;
            biblioteca.carti.Add(cheie1, carte1);
            biblioteca.carti.Add(cheie2, carte2);
            biblioteca.carti.Add(cheie3, carte3);
            biblioteca.carti.Add(cheie4, carte4);
        }

        //Testare functie AdaugaCarte()
        [Test]
        [Category("Pass")]
        public void AdaugaCarteNoua()
        {
            carte = new Carte("Napasta", "I. L. Caragiale", 3, 1890, "Dramaturgie");
            cheie = "Napasta" + "I. L. Caragiale";

            //act
            biblioteca.AdaugaCarte(carte);

            //assert
            Assert.IsTrue(biblioteca.carti.ContainsKey(cheie));
            Assert.AreEqual(3, biblioteca.carti[cheie].NumarExemplare);
        }
        [Test]
        [Category("Pass")]
        public void AdaugareCarteExistenta()
        {
            carte = new Carte("Casa Bantuita", "Shirley Jackson", 9, 1959, "Roman gotic");
            cheie = "Casa Bantuita" + "Shirley Jackson";
            //act
            bool val = biblioteca.AdaugaCarte(carte);

            //assert
            Assert.IsFalse(val);
            Assert.AreEqual(3, biblioteca.carti[cheie].NumarExemplare);                                   //Asteptare cantitate initiala
        }
        [Test]
        [Category("Pass")]
        [TestCase(0)]
        [TestCase(-1)]
        public void AdaugareCarte_CantitateZeroNeg(int cant)
        {
            carte = new Carte("Napasta", "I. L. Caragiale", cant, 1890, "Dramaturgie");
            cheie = "Napasta" + "I. L. Caragiale";
            //act
            biblioteca.AdaugaCarte(carte);

            //assert
            Assert.IsFalse(biblioteca.carti.ContainsKey(cheie));
        }

        /*Testare functie AdaugaCantitate()*/
        [Test]
        [Category("Pass/Fail")]
        [TestCase("Ion", "Liviu Rebreanu", 5)]
        [TestCase("Casa Bantuita", "Shirley Jackson", -5)]
        [TestCase("Casa Bantuita", "Shirley Jackson", 0)]
        [TestCase("Casa Bantuita", "Shirley Jackson", 1)]
        [TestCase("Napasta", "I. L. Caragiale", 3)]
        public void AdaugCantitateCarte(string titlu, string autor, int cantitate)
        {
            cheie = titlu + autor;
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            //act
            biblioteca.AdaugaCantitate(cheie, cantitate);

            //assert
            Assert.AreEqual(cantitate_initiala + cantitate, biblioteca.carti[cheie].NumarExemplare);
        }

        /*Testarea functiei CarteaExista()*/
        [Test]
        [TestCase("Casa BantuitaShirley Jackson")]
        [TestCase("NapastaI. L. Caragiale")]
        public void VerificareCarteaExista(string cheie) {
            bool val = biblioteca.CarteaExista(cheie);
            Assert.IsTrue(val);
        }

        //Testare functie ImprumutaCarte()
        [Test]
        [TestCase("NapastaI. L. Caragiale")]
        [TestCase("IonLiviu Rebreanu")]
        [TestCase("De veghe in lanul de secaraJ.D.Salinger")]                                               //Pt a testa limita de 1 la cantitate
        public void CarteImprumutata(string cheie)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            CardBiblioteca card = new CardBiblioteca("Alin");
            //act
            bool val = biblioteca.ImprumutaCarte(cheie, card);
            //assert
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.IsTrue(val);
            Assert.IsTrue(card.ContineCarte(biblioteca.carti[cheie].Titlu, biblioteca.carti[cheie].Autor));   //Testare functie CarteImprumutata din clasa CardBiblioteca

        }
        [Test]
        [Category("Pass")]
        public void CarteImprumutata_StocZero()
        {
            CardBiblioteca card = new CardBiblioteca("Alex");
            //act
            bool val = biblioteca.ImprumutaCarte(cheie4, card);
            //assert
            Assert.IsFalse(val);
        }

        //Testare functie ReturneazaCarte()
        [Test]
        [Category("Pass")]
        public void CarteReturnata()
        {
            CardBiblioteca card = new CardBiblioteca("Alin");
            //act
            bool val = biblioteca.ReturneazaCarte(cheie1, card);

            //assert
            Assert.AreEqual(11, biblioteca.carti[cheie1].NumarExemplare);
            Assert.IsTrue(val);
            Assert.IsFalse(card.ContineCarte(biblioteca.carti[cheie1].Titlu, biblioteca.carti[cheie1].Autor));  //Testare functie ReturneazaCarte din clasa CardBiblioteca
        }
        [Test]
        [Category("Pass")]
        public void CarteReturnata_Neinregistrata()
        {
            CardBiblioteca card = new CardBiblioteca("Alex");
            //act
            bool val = biblioteca.ReturneazaCarte(cheie5, card);

            //assert
            Assert.IsFalse(biblioteca.carti.ContainsKey(cheie5));
            Assert.IsFalse(val);
        }

        //Testare functie CumparaCarte()
        [Test]
        [Category("Error Return")]
        [TestCase("AnaEm Sava", 3)]                                                                          //Neinregistrata
        [TestCase("IonLiviu Rebreanu", -1)]                                                                  //Cantitate negativa
        [TestCase("IonLiviu Rebreanu", 0)]                                                                   //Cantitate zero
        [TestCase("IonLiviu Rebreanu", 15)]                                                                  //Stoc insuficient
        [TestCase("IonLiviu Rebreanu", 11)]                                                                  //Stoc insuficient
        public void CarteCumparata_Error(string cheie, int cantitate)
        {
            //act
            biblioteca.CumparaCarte(cheie, cantitate, cardBiblioteca);

            //assert
            Assert.IsFalse(biblioteca.carti.ContainsKey(cheie));
        }
        [Test]
        [Category("Pass")]
        [TestCase("IonLiviu Rebreanu", 1)]
        [TestCase("IonLiviu Rebreanu", 9)]
        [TestCase("IonLiviu Rebreanu", 10)]
        public void CarteCumparata(string cheie, int cant)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            //act
            biblioteca.CumparaCarte(cheie, cant, cardBiblioteca);

            //assert
            Assert.IsTrue(biblioteca.carti.ContainsKey(cheie));
            Assert.AreEqual(cantitate_initiala - cant, biblioteca.carti[cheie].NumarExemplare);
            Assert.AreEqual(5 * cant, cardBiblioteca.Puncte());                                   //Testare functie AdaugaPuncteBonus()
        }

        //Testare functie CumparaCarteCuPuncte()
        [Test]
        [TestCase("AnaEm Sava", 350, 30)]
        [TestCase("IonLiviu Rebreanu", 150, 15)]
        [TestCase("IonLiviu Rebreanu", 200, 15)]
        [TestCase("IonLiviu Rebreanu", 149, 15)]
        [TestCase("FluturiIrina Binder", 355, 20)]
        [TestCase("De veghe in lanul de secaraJ.D.Salinger", 350, 15)]
        public void CarteCumparataCuPuncte(string cheie, int puncte, int pret)
        {
            CardBiblioteca card = new CardBiblioteca("Mara");
            card.PuncteBonus = puncte;
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            //act
            biblioteca.CumparaCarteCuPuncte(cheie, pret, card);

            //assert
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.AreEqual(puncte - pret * 10, card.PuncteBonus);

        }
    }
}