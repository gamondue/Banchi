using System.Windows.Controls;
﻿using Banchi.Classi;
using System.IO;

namespace Banchi
{
    public static partial class DataLayer
    {
        static List<Aula> listaAule = new List<Aula>();
        static List<Classe> listaClassi = new List<Classe>();

        public static string PathDatiUtente;
        public static string PathDatiCondivisa;
        public static string PathDatiModelli;

        public static string FileAule;
        public static string FileClassi;
        public static string FileStudenti;

        public static void Inizializzazioni()
        {
            PathDatiUtente = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Banchi"); 
            // path da aggiungere in seguito
            PathDatiCondivisa = PathDatiUtente;
            // path da aggiungere in seguito
            //PathDatiModelli = PathDatiUtente;
            PathDatiModelli = "non esiste";
            CreaCartelleSeNonEsistono();
            FileAule = Path.Combine(PathDatiUtente, "Aule.tsv");
            FileClassi = Path.Combine(PathDatiUtente, "Classi.tsv");
            FileStudenti = Path.Combine(PathDatiUtente, "Studenti.tsv");
            CreaFileSeNonEsiste(FileAule);
            CreaFileSeNonEsiste(FileClassi);
            CreaFileSeNonEsiste(FileStudenti);
        }

        private static void CreaCartelleSeNonEsistono()
        {
            if (!Directory.Exists(PathDatiUtente))
            {
                Directory.CreateDirectory(PathDatiUtente);
            }
            if (!Directory.Exists(PathDatiCondivisa))
            {
                Directory.CreateDirectory(PathDatiCondivisa);
            }
            if (!Directory.Exists(PathDatiModelli) || 
                Utente.Accesso != Utente.RuoloUtente.ModificheAiModelli)
            {
                Directory.CreateDirectory(PathDatiModelli);
            }
        }
        private static void CreaFileSeNonEsiste(string nomeFile)
        {
            if (!File.Exists(nomeFile))
            {
                File.Create(nomeFile);
            }
        }
        public static List<Aula> LeggiTutteLeAule()
        {
            string[] stringheLette = File.ReadAllLines(FileAule);
            string[] split = new string[3];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Aula a = new Aula(split[0], Convert.ToDouble(split[2]), Convert.ToDouble(split[1]));
                listaAule.Add(a);
            }
            return listaAule;
        }
        public static void ScriviTutteLeAule(List<Aula> listaAule)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto= new string[listaAule.Count + 1];
            // salva prima riga di intestazione
            arraySupporto[0] = "NomeAula\tBase\tAltezza";
            // salva nelle righe successive le aule che sono nella lista
            // passata come parametro
            for (int i = 0; i< listaAule.Count; i++)
            {
                arraySupporto[i + 1] = listaAule[i].NomeAula.ToString() + "\t" + 
                    listaAule[i].BaseInCentimetri.ToString() + "\t" + listaAule[i].AltezzaInCentimetri.ToString();
            }
            File.WriteAllLines(FileAule, arraySupporto);  
        }
        public static List<Classe> LeggiTutteLeClassi()
        {
            string[] stringheLette = File.ReadAllLines(FileClassi);
            string[] split = new string[2];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Classe a = new Classe(split[0], split[1]);
                listaClassi.Add(a);
            }           
            return listaClassi;
        }
        public static void ScriviTutteLeClassi(List<Classe> listaClassi)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto = new string[listaClassi.Count + 1];
            for (int i = 0; i < listaClassi.Count; i++)
            {
                arraySupporto[i] = listaClassi[i].CodiceClasse.ToString() + "\t" + listaClassi[i].AnnoScolastico.ToString();
            }
            File.AppendAllLines(FileClassi, arraySupporto);
        }
        public static List<Studente> LeggiTuttiGliStudenti()
        {
            List<Studente> listaStudenti = new List<Studente>();

            string[] stringheLette = File.ReadAllLines(FileStudenti);
            string[] split = new string[3];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Studente s = new Studente(split[0], split[1], split[2]);
                listaStudenti.Add(s);
            }
            return listaStudenti;
        }
        public static void ScriviTuttiGliStudenti(List<Studente> listaStudenti)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto = new string[listaStudenti.Count + 1];
            for (int i = 0; i < listaStudenti.Count; i++)
            {
                arraySupporto[i] = listaStudenti[i].Nome.ToString() + "\t" + listaStudenti[i].Cognome.ToString() + "\t" + listaStudenti[i].CodiceClasse.ToString();
            }
            File.AppendAllLines(FileStudenti, arraySupporto);
        }
        public static List<Studente> LeggiStudentiClasse(Classe classe)
        {
            // legge dal file Studenti.tsv tutti gli studenti della classe passata come parametro
            // e li mette nella lista che è inclusa dentro il tipo Classe
            List<Studente> listaStudenti = new List<Studente>();

            string[] stringheLette = File.ReadAllLines(FileStudenti);
            for (int i = 1; i < stringheLette.Length; i++)
            {
                string[] split = stringheLette[i].Split("\t");
                if (split[3] == classe.CodiceClasse)
                {
                    Studente s = new Studente(split[2], split[1], split[3]);
                    listaStudenti.Add(s);
                }
            }
            classe.Studenti = listaStudenti; 
            return listaStudenti;
        }
        internal static List<Aula> LeggiTutteLeAuleUtente()
        {
            return null;
        }
        internal static List<Classe> LeggiTutteLeClassiUtente()
        {
            return null;
        }
        
        internal static List<Computer> SalvaComputer()
        {
            return null;
        }
    }
}
