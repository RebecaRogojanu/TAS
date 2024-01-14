using carte;

namespace card
{
    public class CardBiblioteca : ICardBiblioteca
    {
        public string NumeDetinator { get; set; }
        public int PuncteBonus { get; set; }
        public List<ICarte> CartiImprumutate { get; set; }

        public CardBiblioteca(string nume)
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
