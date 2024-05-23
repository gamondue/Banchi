namespace Banchi
{
    public class Studente
    {
        public int CodiceStudente { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CodiceClasse { get; set; }
        public Studente(string Nome, string Cognome, string CodiceClasse)
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
