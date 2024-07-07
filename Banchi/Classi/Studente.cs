namespace Banchi
{
    public class Studente
    {
        public int CodiceStudente { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        DateTime DataNascita { get; set; }
        public string CodiceClasse { get; set; }
        public double Media { get; internal set; }
        public List<double> Voti { get; set; } 
        public Studente(string Cognome, string Nome, string CodiceClasse)
        {
            // inizializzazione delle proprietà
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.CodiceClasse = CodiceClasse;
        }
        public override string ToString()
        {
            return Cognome + " " + Nome;
        }
    }
}
