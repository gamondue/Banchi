﻿namespace Banchi
{
    public class Classe
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
        public override string ToString()
        {
            return CodiceClasse; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
