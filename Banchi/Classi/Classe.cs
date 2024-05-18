namespace Banchi
{
    internal class Classe
    {
        public string CodiceClasse { get; set; }
        public string AnnoScolastico { get; set; }
        public List<Studente> Studenti { get; set; }
        public Classe(string CodiceClasse, string AnnoScolastico)
        {
            // inizializzazione delle proprietà
            this.CodiceClasse = CodiceClasse;
            this.AnnoScolastico = AnnoScolastico;
        }
    }
}
