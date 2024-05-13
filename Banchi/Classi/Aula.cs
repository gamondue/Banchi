namespace Banchi
{
    internal class Aula
    {
        public string NomeAula { get; set; }
        // dimensioni dell'aula. 
        public double BaseInMetri { get; set; }
        public double AltezzaInMetri { get; set; }
        // ci serve la lista dei banchi che stanno in questa aula
        public List<Banco> Banchi { get; set; }
        // ci serve la lista dei computer che stanno in questa aula
        public List<Computer> Computer { get; set; }

        // vogliamo disegnare le porte e le finestre dell'aula
        // !!!! qui metteremo le strutture dati che serviranno per disegnare le porte e le finestre !!!!

        // forse ci servirà la lista degli studenti che stanno in questa aula
        //public List<Studente> Studenti { get; set; } // eliminarla se poi non serve
        public Aula(string NomeAula, double AltezzaInMetri, double BaseInMetri)
        {
            // inizializzazione delle proprietà
            this.AltezzaInMetri = AltezzaInMetri;
            this.BaseInMetri = BaseInMetri;
            this.NomeAula = NomeAula;
            Banchi = new List<Banco>();
            Computer = new List<Computer>();
            //Studenti = new List<Studente>();
        }
        public override string ToString()
        {
            return NomeAula; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
