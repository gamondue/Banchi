namespace Banchi
{
    public class Computer
    {
        public string NomeDispositivo { get; set; }
        public string MarcaComputer { get; set; }
        public string IndirizzoIPComputer { get; set; }
        public string NoteComputer { get; set; }
        public string Processore { get; set; }
        public string TipoSistema { get; set; }
        public Computer(string NomeDispositivo, string MarcaComputer = null,
            string IndirizzoIPComputer = null, string NoteComputer = null,
            string Processore = null, string TipoSistema = null)
        {
            // inizializzazione delle proprietà
            this.NomeDispositivo = NomeDispositivo;
            this.MarcaComputer = MarcaComputer;
            this.IndirizzoIPComputer = IndirizzoIPComputer;
            this.NoteComputer = NoteComputer;
            this.Processore = Processore;
            this.TipoSistema = TipoSistema;
        }
        public override string ToString()
        {
            return NomeDispositivo; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
