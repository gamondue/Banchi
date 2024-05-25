using System.Windows.Controls;
using Banchi.Classi;
using System.IO;
using System.Windows;

namespace Banchi
{
    public static partial class DataLayer
    {
        //liste per le varie 
        static List<Aula> listaAule = new List<Aula>();
        static List<Classe> listaClassi = new List<Classe>();
        static List<Banco> listaBanchi = new List<Banco>();
        static List<Computer> listaComputer = new List<Computer>();

        // posizione dove si trova il file
        public static string PathDatiUtente;
        public static string PathDatiCondivisa;
        public static string PathDatiModelli;

        // creazione nome del file in cui salviamo i dati
        public static string FileAule;
        public static string FileClassi;
        public static string FileStudenti;
        public static string FileBanchi;
        public static string FileComputer;

      public static void Inizializzazioni()
        {
            // 
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
            FileBanchi = Path.Combine(PathDatiUtente, "Banchi.tsv");
            FileComputer = Path.Combine(PathDatiUtente, "Computer.tsv");

            CreaFileSeNonEsiste(FileAule);
            CreaFileSeNonEsiste(FileClassi);
            CreaFileSeNonEsiste(FileStudenti);
            CreaFileSeNonEsiste(FileBanchi);
            CreaFileSeNonEsiste(FileComputer);
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
            string[] arraySupporto = new string[listaAule.Count + 1];
            // salva prima riga di intestazione
            arraySupporto[0] = "NomeAula\tBase\tAltezza";
            // salva nelle righe successive le aule che sono nella lista
            // passata come parametro
            for (int i = 0; i < listaAule.Count; i++)
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

        public static void ScriviTuttiComputer(List<Computer> listaComputer)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto = new string[listaComputer.Count + 1];
            for (int i = 0; i < listaComputer.Count; i++)
            {
                arraySupporto[i] = listaComputer[i].NomeDispositivo.ToString() + "\t" + listaComputer[i].MarcaComputer.ToString() + "\t" + listaComputer[i].IndirizzoIPComputer.ToString() + "\t" + listaComputer[i].NoteComputer.ToString() + "\t" + listaComputer[i].Processore.ToString() + "\t" + listaComputer[i].TipoSistema.ToString();
            }
            File.AppendAllLines(FileClassi, arraySupporto);
        }

        //I file di Banchi e Computer li abbiamo inseriti nel bin così che poi lei potrà copiarli e metterli nei documenti

        public static List<Computer> LeggiTuttiComputer()
        {
            List<Computer> listaComputer = new List<Computer>();

            string[] stringheLette = File.ReadAllLines(FileComputer);
            string[] split = new string[6];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Computer c = new Computer(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5]);
                listaComputer.Add(c);
            }
            return listaComputer;
        }


        public static Computer LeggiComputerRichiesto(int NomeDispositivo)
        {
            Computer computerRichiesto = null;
            string[] stringheLette = File.ReadAllLines(FileComputer);
            for (int i = 1; i < stringheLette.Length; i++)
            {
                string[] split = stringheLette[i].Split("\t");
                if (split[0] == NomeDispositivo.ToString())
                {
                    computerRichiesto = new(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5]);
                    break;
                }
            }
            return computerRichiesto;
        }

        public static void ScriviTuttiBanchi(List<Banco> listaBanco)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto = new string[listaBanchi.Count + 1];
            for (int i = 0; i < listaBanchi.Count; i++)
            {
                arraySupporto[i] = listaBanchi[i].CodiceBanco.ToString() + "\t" + listaBanchi[i].NomeClasse.ToString() + "\t" + listaBanchi[i].Size.Width.ToString() + "\t" + listaBanchi[i].Size.Height.ToString() + "\t" + listaBanchi[i].IsCattedra.ToString() + "\t" + listaBanchi[i].CognomeNomeStudente.ToString() + "\t" + listaBanchi[i].Position.X.ToString() + "\t" + listaBanchi[i].Position.Y.ToString();
            }
            File.AppendAllLines(FileBanchi, arraySupporto);
        }
        public static List<Banco> LeggiTuttiBanchi()
        {

            string[] stringheLette = File.ReadAllLines(FileBanchi);
            string[] split = new string[8];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");

                Size size = new(Convert.ToDouble(split[2]), Convert.ToDouble(split[3]));
                Point posizione = new(Convert.ToDouble(split[7]), Convert.ToDouble(split[8]));
                bool cattedra = Convert.ToBoolean(split[5]);
                Label l = new();
                Banco b = new(l, cattedra, size, posizione);
                listaBanchi.Add(b);
            }
            return listaBanchi;
        }
        internal static List<Aula> LeggiTutteLeAuleUtente()
        {
            return null;
        }
        public static void ScriviTuttiBanchi(List<Banco> listaBanco)
        {
            // array di appoggio della dimesione giusta
            string[] arraySupporto = new string[listaBanchi.Count + 1];
            for (int i = 0; i < listaBanchi.Count; i++)
            {
                arraySupporto[i] = listaBanchi[i].CodiceBanco.ToString() + "\t" + listaBanchi[i].NomeClasse.ToString() + "\t" + listaBanchi[i].Size.Width.ToString() + "\t" + listaBanchi[i].Size.Height.ToString() + "\t" + listaBanchi[i].IsCattedra.ToString() + "\t" + listaBanchi[i].CognomeNomeStudente.ToString() + "\t" + listaBanchi[i].Position.X.ToString() + "\t" + listaBanchi[i].Position.Y.ToString(); ////TODO COMPUTER
            }
            File.AppendAllLines(FileBanchi, arraySupporto);
        }
        public static List<Banco> LeggiTuttiBanchi()
        {

            string[] stringheLette = File.ReadAllLines(FileBanchi);
            string[] split = new string[8];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");

                Size size = new(Convert.ToDouble(split[2]), Convert.ToDouble(split[3]));
                Point posizione = new(Convert.ToDouble(split[7]), Convert.ToDouble(split[8]));
                bool cattedra = Convert.ToBoolean(split[5]);
                Label l = new();
                Banco b = new(l, cattedra, size, posizione);
                listaBanchi.Add(b);
            }
            return listaBanchi;
        }
        internal static List<Computer> SalvaComputer()
        {
            return null;
        }
    }
}
