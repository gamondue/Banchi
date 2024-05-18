namespace Banchi
{
    internal class Studente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceClasse { get; set; }
        public List<Studente> Studenti { get; set; }

        public Studente(string Nome, string Cognome, string CodiceClasse)
        {
            // inizializzazione delle proprietà
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.CodiceClasse = CodiceClasse;
        }
    }
}
