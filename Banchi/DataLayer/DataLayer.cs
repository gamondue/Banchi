using Banchi.Classi;
using System.IO;
using System.Windows.Controls;

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
            // lettura di tutte le righe del file 
            string[] righeLette = File.ReadAllLines(FileAule);
            string[] split;
            int nRiga = 2; // salto le prime due righe, che sono di descrizione 
            split = righeLette[nRiga].Split("\t");
            Aula a = null;
            while (nRiga < righeLette.Length - 1)
            {
                // per primi ci sono i dati dell'aula 
                a = new Aula(split[0], Convert.ToDouble(split[1]), Convert.ToDouble(split[2]),
                    null, Convert.ToInt32(split[3]));
                // alla riga successiva arrivano i banchi (se ci sono), che hanno un tab come primo campo
                nRiga++;
                split = righeLette[nRiga].Split("\t");
                // gira finché c'è un vuoto come primo campo
                while (split[0] == "" && nRiga < righeLette.Length)
                {
                    Banco b = new Banco(Convert.ToBoolean(split[2]), Convert.ToDouble(split[3]),
                        Convert.ToDouble(split[4]), Convert.ToDouble(split[5]), Convert.ToDouble(split[6]), null);
                    a.Banchi.Add(b);
                    nRiga++;
                    if (nRiga < righeLette.Length)
                        split = righeLette[nRiga].Split("\t");
                }
                // prossimo giro per cercare la prossima aula
                listaAule.Add(a);
            }
            return listaAule;
        }
        public static void ScriviTutteLeAule(List<Aula> listaAule)
        {
            // TODO
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
        public static List<Computer> LeggiTuttiComputer()
        {
            List<Computer> listaComputer = new List<Computer>();
            string[] stringheLette = File.ReadAllLines(FileComputer);
            string[] split = new string[6];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                Computer c = new Computer(split[0], split[1], split[2], split[3], split[4], split[5]);
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
                    computerRichiesto = new(split[0], split[1], split[2], split[3], split[4], split[5]);
                    break;
                }
            }
            return computerRichiesto;
        }
        public static void ScriviTuttiBanchi(List<Banco> listaBanco)
        {
            // array di appoggio della dimensione giusta
            string[] arraySupporto = new string[listaBanchi.Count + 1];
            for (int i = 0; i < listaBanchi.Count; i++)
            {
                arraySupporto[i] = listaBanchi[i].CodiceBanco.ToString()
                    + "\t" + listaBanchi[i].NomeClasse.ToString()
                    + "\t" + listaBanchi[i].BaseInCentimetri.ToString()
                    + "\t" + listaBanchi[i].AltezzaInCentimetri.ToString()
                    + "\t" + listaBanchi[i].IsCattedra.ToString()
                    + "\t" + listaBanchi[i].Studente.Cognome
                    + "\t" + listaBanchi[i].Studente.Nome
                    + "\t" + listaBanchi[i].PosizioneX.ToString()
                    + "\t" + listaBanchi[i].PosizioneY.ToString();
            }
            File.AppendAllLines(FileBanchi, arraySupporto);
        }
        public static List<Banco> LeggiTuttiBanchi()
        {
            string[] stringheLette = File.ReadAllLines(FileBanchi);
            for (int i = 1; i < stringheLette.Length; i++)
            {
                string[] split = stringheLette[i].Split("\t");

                double larghezza = Convert.ToDouble(split[2]);
                double altezza = Convert.ToDouble(split[3]);
                double posizioneX = Convert.ToDouble(split[7]);
                double posizioneY = Convert.ToDouble(split[8]);
                bool cattedra = Convert.ToBoolean(split[4]);
                Label l = new();
                Banco b = new(cattedra, larghezza, altezza, posizioneX, posizioneY, l);
                b.Studente.Cognome = split[5];
                b.Studente.Nome = split[6];
                listaBanchi.Add(b);
            }
            return listaBanchi;
        }
        internal static List<Aula> LeggiTutteLeAuleUtente()
        {
            return null;
        }
        internal static void SalvaComputer()
        {

        }
        internal static List<Classe> LeggiTutteLeClassiUtente()
        {
            return null;
        }
        internal static void ScriviBanchiDellAula(List<Banco> banchi, Aula aula)
        {

        }
        internal static List<Banco> LeggiBanchiDellAula(Aula aula)
        {
            return null;
        }
    }
}
