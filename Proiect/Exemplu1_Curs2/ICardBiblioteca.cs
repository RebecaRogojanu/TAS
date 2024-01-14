using carte;

namespace card
{
    
    public interface ICardBiblioteca
    {
        int PuncteBonus { get; set; }
        string NumeDetinator { get; set; }
        List<ICarte> CartiImprumutate { get; set; }

        public bool ContineCarte(string titlu, string autor);
        void CarteImprumutata(ICarte carte);
        void ReturneazaCarte(ICarte carte);
        int Puncte();
        void AdaugaPuncteBonus(int puncte);
        void ScadePuncteBonus(int puncte);
    }
}
