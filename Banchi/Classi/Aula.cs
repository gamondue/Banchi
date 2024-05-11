namespace Banchi
{
    internal class Aula
    {
        public double AltezzaInMetri { get; set; }
        public double LarghezzaInMetri { get; set; }
        public string NomeAula { get; set; }
        public List<Banco> Banchi { get; set; }
        public List<Computer> Computer { get; set; }
        public List<Studente> Studenti { get; set; }
        public Aula(string NomeAula, double AltezzaInMetri, double LarghezzaInMetri)
        {
            this.AltezzaInMetri = AltezzaInMetri;
            this.LarghezzaInMetri = LarghezzaInMetri;
            this.NomeAula = NomeAula;
            Banchi = new List<Banco>();
            Computer = new List<Computer>();
            Studenti = new List<Studente>();
        }
    }
}
