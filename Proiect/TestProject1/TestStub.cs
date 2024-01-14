using card;
using carte;

namespace librarie
{
    public class TesteStub
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
            cardBiblioteca = new CardBibliotecaStub("Flavia");
            carte1 = new CarteStub("Ion", "Liviu Rebreanu", 10, 1920, "Roman social");
            cheie1 = carte1.Titlu + carte1.Autor;
            carte2 = new CarteStub("De veghe in lanul de secara", "J.D.Salinger", 1, 1951, "Fictiune");
            cheie2 = carte2.Titlu + carte2.Autor;
            carte3 = new CarteStub("Casa Bantuita", "Shirley Jackson", 3, 1959, "Roman gotic");
            cheie3 = carte3.Titlu + carte3.Autor;
            carte4 = new CarteStub("Fluturi", "Irina Binder", 0, 2013, "Fictiune");
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
            var carte = new CarteStub("Napasta", "I. L. Caragiale", 9, 1890,"Dramaturgie");
            cheie = "Napasta" + "I. L. Caragiale";
            //act
            bool val = biblioteca.AdaugaCarte(carte);

            //assert
            Assert.IsTrue(val);
            Assert.AreEqual(9, biblioteca.carti[cheie].NumarExemplare);                                    //Asteptare cantitate initiala
        }
        [Test]
        [Category("Pass")]
        public void AdaugareCarteExistenta()
        {
            carte = new CarteStub("Casa Bantuita", "Shirley Jackson", 9, 1959, "Roman gotic");
            cheie = "Casa Bantuita" + "Shirley Jackson";
            //act
            bool val = biblioteca.AdaugaCarte(carte);

            //assert
            Assert.IsFalse(val);
            Assert.AreEqual(3, biblioteca.carti[cheie].NumarExemplare);                                  //Asteptare cantitate initiala
        }

        [Test]
        [Category("Pass")]
        public void AdaugareCantitateCarteExistenta()
        {
            //act
            biblioteca.AdaugaCantitate(cheie3,5);

            //assert
            Assert.AreEqual(8, biblioteca.carti[cheie3].NumarExemplare);                                  //Asteptare cantitate initiala
        }
        [Test]
        [Category("Fail")]
        public void AdaugareCantitateCarteNeexistenta()
        {
            cheie = "CasaShirley Jackson";
            //act
            biblioteca.AdaugaCantitate(cheie, 5);

            //assert
            Assert.AreEqual(5, biblioteca.carti[cheie].NumarExemplare);                    
        }
        //Testare functie ImprumutaCarte()
        [Test]
        [Category("Pass")]
        [TestCase("IonLiviu Rebreanu")]
        //[TestCase("De veghe in lanul de secaraJ.D.Salinger")]                                             
        public void CarteImprumutata(string cheie)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            ICardBiblioteca card = new CardBibliotecaStub("Alin");
            //act
            bool val = biblioteca.ImprumutaCarte(cheie, card);

            //assert
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.IsTrue(val);
            Assert.IsTrue(card.ContineCarte(biblioteca.carti[cheie].Titlu, biblioteca.carti[cheie].Autor)); //Testare functie CarteImprumutata din clasa CardBiblioteca

        }
        [Test]
        [Category("Pass")]
        [TestCase("IonLiviu Rebreanu")]                                                                      //Pass
        //[TestCase("MaraIoan Sadoveanu")]                                                                   //Fail - carte nu exista in biblioteca
        public void CarteReturnata(string cheie)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            ICardBiblioteca card = new CardBibliotecaStub("Alin");
            //act
            bool val = biblioteca.ReturneazaCarte(cheie, card);

            //assert
            Assert.AreEqual(cantitate_initiala + 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.IsTrue(val);
            Assert.IsFalse(card.ContineCarte(biblioteca.carti[cheie].Titlu, biblioteca.carti[cheie].Autor));  //Testare functie ReturneazaCarte din clasa CardBiblioteca
        }

        [Test]
        [TestCase("IonLiviu Rebreanu", 10)]                                                                  //Pass
        //[TestCase("IonLiviu Rebreanu", 1)]                                                                 //Pass
        //[TestCase("AnaEm Sava", 3)]                                                                        //Neinregistrata
        //[TestCase("IonLiviu Rebreanu", 0)]                                                                 //Cantitate zero
        //[TestCase("IonLiviu Rebreanu", 11)]                                                                //Stoc insuficient

        public void CarteCumparata(string cheie, int cantitate)
        {
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            //act
            biblioteca.CumparaCarte(cheie, cantitate, cardBiblioteca);

            //assert
            Assert.IsTrue(biblioteca.carti.ContainsKey(cheie));
            Assert.AreEqual(cantitate_initiala - cantitate, biblioteca.carti[cheie].NumarExemplare);
            Assert.AreEqual(5 * cantitate, cardBiblioteca.Puncte());                                //Testare functie AdaugaPuncteBonus()
        }

        [Test]
        [TestCase("IonLiviu Rebreanu", 150, 15)]
        //[TestCase("IonLiviu Rebreanu", 151, 15)]
        //[TestCase("IonLiviu Rebreanu", 200, 15)]
        //[TestCase("IonLiviu Rebreanu", 149, 15)]
        //[TestCase("IonLiviu Rebreanu", 100, 15)]
        public void CarteCumparataCuPuncte(string cheie, int puncte, int pret)
        {
            ICardBiblioteca card = new CardBibliotecaStub("Mara");
            card.PuncteBonus = puncte;
            int cantitate_initiala = biblioteca.carti[cheie].NumarExemplare;
            //act
            biblioteca.CumparaCarteCuPuncte(cheie, pret, card);

            //assert
            Assert.AreEqual(cantitate_initiala - 1, biblioteca.carti[cheie].NumarExemplare);
            Assert.AreEqual(puncte - pret * 10, card.PuncteBonus);

        }

        /* Clasele STUB */
        public class CarteStub : ICarte
        {
            public string Titlu { get; set; }
            public string Autor { get; set; }
            public int NumarExemplare { get; set; }
            public uint AnPublicare { get; set; }
            public string Categorie { get; set; }

            public CarteStub(string titlu, string autor, int numarExemplare, uint an, string categorie)
            {
                Titlu = titlu;
                Autor = autor;
                NumarExemplare = numarExemplare;
                AnPublicare = an;
                Categorie = categorie;
            }
        }
        public class CardBibliotecaStub : ICardBiblioteca
        {
            public string NumeDetinator { get; set; }
            public int PuncteBonus { get; set; }
            public List<ICarte> CartiImprumutate { get; set; }

            public CardBibliotecaStub(string nume)
            {
                NumeDetinator = nume;
                CartiImprumutate = new List<ICarte>();
                PuncteBonus = 0;
            }
            public void CarteImprumutata(ICarte carte)
            {
                CartiImprumutate.Add(carte);
            }
            public void ReturneazaCarte(ICarte carte)
            {
                CartiImprumutate.Remove(carte);
            }
            public bool ContineCarte(string titlu, string autor)
            {
                return CartiImprumutate.Any(carte => carte.Titlu == titlu && carte.Autor == autor);
            }
            public int Puncte() { return PuncteBonus; }
            public void AdaugaPuncteBonus(int puncte)
            {
                PuncteBonus += puncte;
            }
            public void ScadePuncteBonus(int puncte)
            {
                PuncteBonus -= puncte;
            }

        }
    }
}