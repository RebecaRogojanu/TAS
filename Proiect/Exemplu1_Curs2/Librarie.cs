using card;
using carte;
namespace librarie
{
    public class Biblioteca
    {
        //Cheia colectiei = Titlu+Autor
        public Dictionary<string, ICarte> carti;

        public Biblioteca()
        {
            carti = new Dictionary<string, ICarte>();
        }
        private bool Pozitiv(float valoare)
        {
            if (valoare > 0)
                return true;
            return false;

        }
        public bool AdaugaCarte(ICarte carte)
        {
            if (Pozitiv(carte.NumarExemplare))
            {
                string cheie = carte.Titlu + carte.Autor;
                if (carti.ContainsKey(cheie))
                {
                    return false;
                }
                else
                {
                    carti.Add(cheie, carte);
                    return true;
                }
            }
            else
            {
                return false;

            }
        }
        public void AdaugaCantitate(string cheie, int cantitate)
        {
            if (Pozitiv(cantitate))
            {
                if (!carti.ContainsKey(cheie))
                {
                    throw new ArgumentException("Cartea specificata nu exista in biblioteca");
                }
                else
                {
                    carti[cheie].NumarExemplare += cantitate;
                }
        }
            else throw new ArgumentException("Cantitatea trebuie să fie pozitiva");

        }
        public bool CarteaExista(string cheie)
        {
            return carti.ContainsKey(cheie);
        }

        public bool ImprumutaCarte(string cheie, ICardBiblioteca card)
        {
            if (carti.ContainsKey(cheie) && carti[cheie].NumarExemplare > 0)
            {
                carti[cheie].NumarExemplare--;
                card.CarteImprumutata(carti[cheie]);
                return true;
            }
            return false;
        }

        public bool ReturneazaCarte(string cheie, ICardBiblioteca card)
        {
            if (carti.ContainsKey(cheie))
            {
                carti[cheie].NumarExemplare++;
                card.ReturneazaCarte(carti[cheie]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CumparaCarte(string cheie, int numarExemplare, ICardBiblioteca card)
        {
            if (!carti.ContainsKey(cheie))
            {
                throw new ArgumentException("Cartea specificata nu exista în biblioteca");
            }
            else if (numarExemplare <= 0)
            {
                throw new ArgumentException("Numarul de exemplare specificat este invalid.");
            }
            else
            if (carti[cheie].NumarExemplare < numarExemplare)
            {
                throw new ArgumentException("Nu sunt destule produse in stoc");
            }

            carti[cheie].NumarExemplare -= numarExemplare;
            card.AdaugaPuncteBonus(5 * numarExemplare);
        }

        public void CumparaCarteCuPuncte(string cheie, int pret, ICardBiblioteca card)
        {
            if (!carti.ContainsKey(cheie))
            {
                throw new ArgumentException("Cartea specificata nu exista în biblioteca");
            }
            else if (carti[cheie].NumarExemplare <= 0)
            {
                throw new ArgumentException("Numarul de exemplare specificat este invalid");
            }

            int costInPuncte = pret * 10;
            if (card.PuncteBonus < costInPuncte)
            {
                throw new ArgumentException("Nu aveti suficiente puncte bonus pentru a cumpara aceasta carte");
            }

            carti[cheie].NumarExemplare--;                                       // Scădem numărul de exemplare ale cărții
            card.ScadePuncteBonus(costInPuncte);                                 // Scădem punctele necesare de pe card
        }
    }
}