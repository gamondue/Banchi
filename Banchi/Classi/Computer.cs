﻿namespace Banchi
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
        public string NomeDispositivo { get; set; }
        public string MarcaComputer { get; set; }
        public string Modello { get; set; }
        public string IndirizzoIPComputer { get; set; }
        public string NoteComputer { get; set; }
        public string Processore { get; set; }
        public string TipoSistema { get; set; }
        public StatoComputer Stato { get; set; }
        public Computer(string NomeDispositivo, string MarcaComputer = null,
            string Modello = null,
            string Processore = null, string TipoSistema = null, 
            string IndirizzoIPComputer = null, 
            StatoComputer Stato = StatoComputer.NonDefinito, 
            string NoteComputer = null
            )
        {
            // inizializzazione delle proprietà
            this.NomeDispositivo = NomeDispositivo;
            this.MarcaComputer = MarcaComputer;
            this.Modello = Modello;
            this.IndirizzoIPComputer = IndirizzoIPComputer;
            this.NoteComputer = NoteComputer;
            this.Processore = Processore;
            this.TipoSistema = TipoSistema;
            this.Stato = Stato;
        }
        public override string ToString()
        {
            return NomeDispositivo; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
