using System.IO;

namespace Banchi
{
    internal static partial class DataLayer
    {
        static List<Aula> listaAule = new List<Aula>();
        static List<Classe> listaClassi = new List<Classe>();
        static List<Studente> listaStudenti = new List<Studente>();
        static string[] arraySupporto= new string[100];

        public static string PathDatiUtente;
        public static string PathDatiCondivisa;
        public static string PathDatiModelli;

        public static string FileAule;
        public static string FileClassi;
        public static string FileStudenti;

        internal static void Inizializzazioni()
        {
            PathDatiUtente = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Banchi"); 
            // path da aggiungere in seguito
            PathDatiCondivisa = PathDatiUtente;
            // path da aggiungere in seguito
            PathDatiModelli = PathDatiUtente;
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
            if (!Directory.Exists(PathDatiModelli))
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

        internal static List<Aula> LeggiTutteLeAule()
        {
            string[] stringheLette = File.ReadAllLines(FileAule);
            string[] split = new string[3];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Aula a = new Aula(split[0], Convert.ToDouble(split[1]), Convert.ToDouble(split[2]));
                listaAule.Add(a);
            }
            return listaAule;
        }

        internal static void ScriviTutteLeAule(List<Aula> listaAule)
        {
            // salva prima riga di intestazione
            arraySupporto[0] = "NomeAula\tBase\tAltezza";
            // salva nelle righe successive le aule che sono nella lista
            // passata come parametro
            for (int i = 0; i< listaAule.Count; i++)
            {
                arraySupporto[i + 1] = listaAule[i].NomeAula.ToString() + "\t" + 
                    listaAule[i].BaseInCentimetri.ToString() + "\t" + listaAule[i].AltezzaInCentimetri.ToString();
            }
            File.AppendAllLines(FileAule, arraySupporto);  
        }
          
        internal static List<Classe> LeggiTutteLeClassi()
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

        internal static void ScriviTutteLeClassi(List<Classe> listaClassi)
        {
            for (int i = 0; i < listaClassi.Count; i++)
            {
                arraySupporto[i] = listaClassi[i].CodiceClasse.ToString() + "\t" + listaClassi[i].AnnoScolastico.ToString();
            }
            File.AppendAllLines(FileClassi, arraySupporto);
        }

        internal static List<Studente> LeggiTuttiGliStudenti()
        {
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
        internal static void ScriviTuttiGliStudenti(List<Studente> listaStudenti)
        {
            for (int i = 0; i < listaStudenti.Count; i++)
            {
                arraySupporto[i] = listaStudenti[i].Nome.ToString() + "\t" + listaStudenti[i].Cognome.ToString() + "\t" + listaStudenti[i].CodiceClasse.ToString();
            }
            File.AppendAllLines(FileStudenti, arraySupporto);
        }

        internal static void LeggiStudentiClasse(Classe classe)
        {
            // legge dal file Studenti.tsv tutti gli studenti della classe passata come parametro
            // e li mette nella lista che è inclusa dentro il tipo Classe
            throw new NotImplementedException();
        }
    }
}
