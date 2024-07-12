namespace Banchi
{
    public class Computer
    {
        public enum StatoComputer
        {
            NonDefinito,
            InUso,
            Funzionante,
            DaRiparare,
            DaRitirare,
        }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }
        public string IndirizzoIP { get; set; }
        public string Processore { get; set; }
        public string SistemaOperativo { get; set; }
        public StatoComputer Stato { get; set; }
        public string Note { get; set; }
        public Computer(string NomeDispositivo, string MarcaComputer = null,
            string Modello = null,
            string Processore = null, string TipoSistema = null, 
            string IndirizzoIPComputer = null, 
            StatoComputer Stato = StatoComputer.NonDefinito, 
            string NoteComputer = null
            )
        {
            // inizializzazione delle proprietà
            this.Nome = NomeDispositivo;
            this.Marca = MarcaComputer;
            this.Modello = Modello;
            this.IndirizzoIP = IndirizzoIPComputer;
            this.Note = NoteComputer;
            this.Processore = Processore;
            this.SistemaOperativo = TipoSistema;
            this.Stato = Stato;
        }
        public override string ToString()
        {
            return Nome; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
