using System.IO;
using System.Windows.Controls;

namespace Banchi
{
    public static partial class DataLayer
    {
        // campi statici che contengono i dati di configurazione del programma
        // posizione dove si trova il file
        public static string PathDatiUtente;
        public static string PathDatiCondivisi;
        public static string PathDatiConfigurazione;

        // creazione nome del file in cui salviamo i dati
        public static string FileAuleCondiviso;
        public static string FileClassiCondiviso;
        public static string FileStudentiCondiviso;
        public static string FileBanchiCondiviso;
        public static string FileComputerCondiviso;

        public static void Inizializzazioni()
        {
            // path dove si trovano i dati dell'utente, deve essere in una cartella cui l'utente ha accesso
            // in scrittura, per essere sicuri la mettiamo dentro la cartella Documenti, in una cartella "Banchi"
            PathDatiUtente = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Banchi");
            // i dati di configurazione del programma saranno in una sottocartella "config"
            PathDatiConfigurazione = Path.Combine(PathDatiUtente, "Config");
            // a regime la cartella condivisa starà sul della scuola server, ma per le prove la mettiamo in locale
            PathDatiCondivisi = @"C:\Banchi\DatiCondivisi";

            CreaCartelleSeNonEsistono();
            FileAuleCondiviso = Path.Combine(PathDatiCondivisi, "Aule.tsv");
            FileClassiCondiviso = Path.Combine(PathDatiCondivisi, "Classi.tsv");
            FileStudentiCondiviso = Path.Combine(PathDatiCondivisi, "Studenti.tsv");
            FileBanchiCondiviso = Path.Combine(PathDatiCondivisi, "Banchi.tsv");
            FileComputerCondiviso = Path.Combine(PathDatiCondivisi, "Computer.tsv");

            CreaFileSeNonEsiste(FileAuleCondiviso);
            CreaFileSeNonEsiste(FileClassiCondiviso);
            CreaFileSeNonEsiste(FileStudentiCondiviso);
            CreaFileSeNonEsiste(FileBanchiCondiviso);
            CreaFileSeNonEsiste(FileComputerCondiviso);
        }
        private static void CreaCartelleSeNonEsistono()
        {
            if (!Directory.Exists(PathDatiUtente))
            {
                Directory.CreateDirectory(PathDatiUtente);
            }
            if (!Directory.Exists(PathDatiConfigurazione))
            {
                Directory.CreateDirectory(PathDatiConfigurazione);
            }
            if (!Directory.Exists(PathDatiCondivisi))
            {
                Directory.CreateDirectory(PathDatiCondivisi);
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
            List<Aula> listaAule = new List<Aula>();
            // lettura di tutte le righe del file 
            string[] righeLette = File.ReadAllLines(FileAuleCondiviso);
            if (righeLette.Length <= 1)
            {
                return listaAule;
            }
            string[] split;
            int nRiga = 2; // salto le prime due righe, che sono di descrizione 
            split = righeLette[nRiga].Split("\t");
            Aula a = null;
            while (nRiga < righeLette.Length - 1)
            {
                // per primi ci sono i dati dell'aula 
                a = new
                    (split[0], Convert.ToDouble(split[1]), Convert.ToDouble(split[2]),
                    Convert.ToInt32(split[3]), Convert.ToInt32(split[4]), Convert.ToInt32(split[5]));
                // alla riga successiva arrivano i banchi (se ci sono), che hanno il primo campo vuoto
                nRiga++;
                // poi seguono i dati dei banchi, uno per riga
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
            // array di appoggio della dimensione giusta
            string[] arraySupporto = new string[listaAule.Count + 2];
            // salva le due prime righe di intestazione
            arraySupporto[0] = "NomeAula\tBase\tAltezza\tDirezioneNord\tRosaDeiVentiX\tRosaDeiVentiY";
            arraySupporto[1] = "CodiceBanco\tIsCattedra\tBaseInCentimetri\tAltezzaInCentimetri\tPosizioneX\tPosizioneY";
            // salva nelle righe successive le aule che sono nella lista
            // passata come parametro
            for (int i = 0; i < listaAule.Count; i++)
            {
                arraySupporto[i + 2] = listaAule[i].NomeAula.ToString() + "\t" +
                    listaAule[i].BaseInCentimetri.ToString() + "\t" + listaAule[i].AltezzaInCentimetri.ToString();
            }
            File.WriteAllLines(FileAuleCondiviso, arraySupporto);
        }
        public static List<Classe> LeggiTutteLeClassi()
        {
            List<Classe> listaClassi = new List<Classe>();
            string[] stringheLette = File.ReadAllLines(FileClassiCondiviso);
            if (stringheLette.Length <= 1)
            {
                return listaClassi;
            }
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
            File.AppendAllLines(FileClassiCondiviso, arraySupporto);
        }
        public static List<Studente> LeggiTuttiGliStudenti()
        {
            List<Studente> listaStudenti = new List<Studente>();
            string[] stringheLette = File.ReadAllLines(FileStudentiCondiviso);
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
                arraySupporto[i] = listaStudenti[i].Nome.ToString() + "\t" + listaStudenti[i].Cognome.ToString() + "\t" +
                    listaStudenti[i].CodiceClasse.ToString() + listaStudenti[i].Media;
            }
            File.AppendAllLines(FileStudentiCondiviso, arraySupporto);
        }
        public static List<Studente> LeggiStudentiDiUnaClasse(Classe classe)
        {
            // legge dal file Studenti.tsv tutti gli studenti della classe passata come parametro
            // e li mette nella lista che è inclusa dentro il tipo Classe
            List<Studente> listaStudenti = new List<Studente>();
            string[] stringheLette = File.ReadAllLines(FileStudentiCondiviso);
            for (int i = 1; i < stringheLette.Length; i++)
            {
                string[] split = stringheLette[i].Split("\t");
                if (split[3] == classe.CodiceClasse)
                {
                    Studente s = new Studente(split[1], split[2], split[3]);
                    listaStudenti.Add(s);
                }
            }
            classe.Studenti = listaStudenti;
            return listaStudenti;
        }
        public static void ScriviTuttiComputer(List<Computer> listaComputer)
        {
            // array di appoggio della dimensione giusta
            string[] arraySupporto = new string[listaComputer.Count + 1];
            // ordina la lista per Nome
            listaComputer.Sort((x, y) => x.Nome.CompareTo(y.Nome));
            // prima riga di intestazione
            arraySupporto[0] = "NomeDispositivo\tMarca\tModello\tProcessore\tSistema\tIndirizzo IP\tStato\tNote\r\n";
            for (int i = 0; i < listaComputer.Count; i++)
            {
                arraySupporto[i + 1] = listaComputer[i].Nome.ToString()
                    + "\t" + listaComputer[i].Marca.ToString()
                    + "\t" + listaComputer[i].Modello.ToString()
                    + "\t" + listaComputer[i].Processore.ToString()
                    + "\t" + listaComputer[i].SistemaOperativo.ToString()
                    + "\t" + listaComputer[i].IndirizzoIP.ToString()
                    + "\t" + (int)listaComputer[i].Stato
                    + "\t" + listaComputer[i].Note.ToString();
            }
            File.WriteAllLines(FileComputerCondiviso, arraySupporto);
        }
        public static List<Computer> LeggiTuttiComputer()
        {
            List<Computer> listaComputer = new List<Computer>();
            string[] stringheLette = File.ReadAllLines(FileComputerCondiviso);
            if (stringheLette.Length <= 1)
            {
                return listaComputer;
            }
            string[] split = new string[6];
            for (int i = 1; i < stringheLette.Length; i++)
            {
                split = stringheLette[i].Split("\t");
                if (split.Length > 1)
                {
                    Computer c = new Computer(split[0], split[1], split[2], split[3], split[4],
                        split[5], (Computer.StatoComputer)Convert.ToInt32(split[6]),
                        split[7]);
                    listaComputer.Add(c);
                }
            }
            return listaComputer;
        }
        public static Computer LeggiComputerRichiesto(string NomeDispositivo)
        {
            List<Computer> tutti = LeggiTuttiComputer();
            foreach (Computer c in tutti)
            {
                if (c.Nome == NomeDispositivo)
                {
                    return c;
                }
            }
            return null;
        }
        public static void ScriviTuttiBanchi(List<Banco> listaBanco)
        {
            List<Banco> listaBanchi = new List<Banco>();
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
                    + "\t" + listaBanchi[i].PosizioneXInCentimetri.ToString()
                    + "\t" + listaBanchi[i].PosizioneYInCentimetri.ToString();
            }
            File.AppendAllLines(FileBanchiCondiviso, arraySupporto);
        }
        public static List<Banco> LeggiTuttiBanchi()
        {
            List<Banco> listaBanchi = new List<Banco>();
            string[] stringheLette = File.ReadAllLines(FileBanchiCondiviso);
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
        public static void ScriviAulaEClasse(Aula aula, Classe classe)
        {
            string FileSalvaAuleEClasse = Path.Combine(PathDatiUtente, "AC_" + aula.NomeAula + "_" + classe.CodiceClasse + ".tsv");
            string[] arraySupporto = new string[aula.Banchi.Count + 3];
            arraySupporto[0] = "NomeAula\tBase\tAltezza\tDirezioneNord\tClasse";
            arraySupporto[1] = "vuoto\tCodiceBanco\tIsCattedra\tBaseInCentimetri\tAltezzaInCentimetri" +
                "\tPosizioneX\tPosizioneY\tCognome Studente\tNome Studente\tComputer";
            arraySupporto[2] = aula.NomeAula + "\t" + aula.BaseInCentimetri + "\t" + aula.AltezzaInCentimetri
                + "\t" + aula.DirezioneNord + "\t" + classe.CodiceClasse;
            int i = 3;
            foreach (Banco b in aula.Banchi)
            {
                arraySupporto[i] = "\t" + b.CodiceBanco + "\t" + b.IsCattedra + "\t"
                + b.BaseInCentimetri + "\t" + b.AltezzaInCentimetri
                + "\t" + b.PosizioneXInCentimetri + "\t" + b.PosizioneYInCentimetri
                + "\t" + b.Studente.Cognome + "\t" + b.Studente.Nome
                + "\t" + b.Computer;
                i++;
            }
            File.WriteAllLines(FileSalvaAuleEClasse, arraySupporto);
        }
        internal static List<Aula> LeggiTutteLeAuleEClassiCondivise()
        {
            List<Aula> listaAule = new List<Aula>();
            // cerca tutti i file tsv che iniziano con AC_ e legge le aule e le classi
            // che ci sono dentro
            string[] filesAC = Directory.GetFiles(PathDatiCondivisi, "AC_*.tsv");
            foreach (string file in filesAC)
            {
                string[] righeLette = File.ReadAllLines(file);
                string[] split = righeLette[2].Split("\t");
                Aula a = new Aula(split[0], Convert.ToDouble(split[1]), Convert.ToDouble(split[2]),
                    Convert.ToInt32(split[3]));
                a.Classe = new Classe(split[4]);
                // alla riga successiva arrivano i banchi (se ci sono), che hanno un tab come primo campo
                int nRiga = 3;
                split = righeLette[nRiga].Split("\t");
                // gira finché c'è un vuoto come primo campo
                while (split[0] == "" && nRiga < righeLette.Length)
                {
                    Banco b = new Banco(Convert.ToBoolean(split[2]), Convert.ToDouble(split[3]),
                        Convert.ToDouble(split[4]),
                        Convert.ToDouble(split[5]), Convert.ToDouble(split[6]), null);
                    if (split.Length > 6 && split[7] != "")
                    {
                        b.Studente = new Studente(split[7], split[8], a.Classe.CodiceClasse);
                    }
                    if (split.Length > 8 && split[9] != "")
                    {
                        b.Computer = new Computer(split[9]);
                    }
                    a.Banchi.Add(b);
                    nRiga++;
                    if (nRiga < righeLette.Length)
                        split = righeLette[nRiga].Split("\t");
                }
                listaAule.Add(a);
            }
            return listaAule;
        }
        internal static List<Aula> LeggiTutteLeAuleUtente()
        {
            return null;
        }
        internal static List<Classe> LeggiTutteLeClassiUtente()
        {
            return null;
        }
        internal static void ScriviBanchiDellAula(List<Banco> banchi, Aula aula)
        {
            throw new NotImplementedException();
        }
        internal static List<Banco> LeggiBanchiDellAula(Aula aula)
        {
            return null;
        }
        internal static void SalvaComputer(Computer computerDaSalvare, string chiave)
        {
            List<Computer> listaTutti = LeggiTuttiComputer();
            Computer trovato = CercaComputer(chiave, listaTutti);
            if (trovato == null)
            {
                listaTutti.Add(computerDaSalvare);
            }
            else
            {
                listaTutti.Remove(trovato);
                listaTutti.Add(computerDaSalvare);
            }
            ScriviTuttiComputer(listaTutti);
        }
        internal static Computer CercaComputer(string nomeDispositivo, List<Computer> lista)
        {
            foreach (Computer c in lista)
            {
                if (c.Nome == nomeDispositivo)
                {
                    return c;
                }
            }
            return null;
        }
        internal static void AggiungiComputers(List<Computer> lista)
        {
            List<Computer> listaTutti = LeggiTuttiComputer();
            foreach (Computer c in lista)
            {
                Computer trovato = CercaComputer(c.Nome, listaTutti);
                if (trovato == null)
                {
                    // aggiungo il computer alla lista, perchè non c'era
                    listaTutti.Add(c);
                }
                else
                {
                    // siccome c'era già, lo sostituisco
                    listaTutti.Remove(trovato);
                    listaTutti.Add(c);
                }
            }
            ScriviTuttiComputer(listaTutti);
        }
        internal static void EliminaComputer(string codiceComputer)
        {
            List<Computer> listaTutti = LeggiTuttiComputer();
            for (int i = 0; i < listaTutti.Count; i++)
            {
                Computer trovato = CercaComputer(listaTutti[i].Nome, listaTutti);
                if (trovato != null && trovato.Nome == codiceComputer)
                {
                    // tolgo il computer alla lista
                    listaTutti.Remove(listaTutti[i]);
                }
            }
            ScriviTuttiComputer(listaTutti);
        }
        internal static void SalvaSegnalazione(string messaggioCompleto)
        {
            string pathEFile = Path.Combine(PathDatiUtente, "Segnalazioni.txt");
            File.AppendAllText(pathEFile, messaggioCompleto + "\n\n");
        }
    }
}
