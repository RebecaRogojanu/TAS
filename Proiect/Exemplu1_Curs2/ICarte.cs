namespace carte
{
    public interface ICarte
    {
        string Titlu { get; set; }
        string Autor { get; set; }
        int NumarExemplare { get; set; }
        uint AnPublicare { get; set; }
        string Categorie { get; set; }
    }
}
