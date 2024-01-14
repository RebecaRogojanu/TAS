using System.Diagnostics.CodeAnalysis;

namespace carte
{
    public class Carte : ICarte
    {
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public int NumarExemplare { get; set; }
        public uint AnPublicare { get; set; }
        public string Categorie { get; set; }

        public Carte(string titlu, string autor, int numarExemplare, uint an, string categorie)
        {
            Titlu = titlu;
            Autor = autor;
            NumarExemplare = numarExemplare;
            AnPublicare = an;
            Categorie = categorie;
        }
    }
}
