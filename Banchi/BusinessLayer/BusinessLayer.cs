using Banchi.Classi;
using System.IO;

namespace Banchi
{
    public static partial class BusinessLayer
    {
        public static void Inizializzazioni()
        {
            // inizializza le classi e le aule
            DataLayer.Inizializzazioni();
            // determina il tipo dell'utente in base al suo nome
            // ed alla possibilità di modificare i modelli
            Utente.Username = Environment.UserName;
            if (Utente.Username.Contains("stud"))
            {
                Utente.Accesso = Utente.RuoloUtente.SolaLettura;
            }
            else if (PuòModificareModelli())
            {
                Utente.Accesso = Utente.RuoloUtente.ModificheAiModelli;
            }
            else
                Utente.Accesso = Utente.RuoloUtente.ModifichePersonali;
        }
        private static bool PuòModificareModelli()
        {
            // restituisce true se l'utente può modificare i modelli
            string nomeFileProva = Path.Combine(DataLayer.PathDatiCondivisi, "TestFile.txt");
            string contenuto = new Random().Next(100000).ToString();
            try
            {
                File.WriteAllText(nomeFileProva, contenuto);
                string risultato = File.ReadAllText(nomeFileProva);
                if (contenuto == risultato)
                {
                    File.Delete(nomeFileProva);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static List<Classe> LeggiTutteLeClassi()
        {
            return DataLayer.LeggiTutteLeClassi();
        }
        public static List<Aula> LeggiTutteLeAule()
        {
            return DataLayer.LeggiTutteLeAule();
        }
        public static List<Studente> LeggiTuttiGliStudenti()
        {
            return DataLayer.LeggiTuttiGliStudenti();
        }
        public static List<Studente> LeggiStudentiDiUnaClasse(Classe classe)
        {
            return DataLayer.LeggiStudentiDiUnaClasse(classe);
        }
        public static void ScriviTutteLeAule(List<Aula> listaAule)
        {
            DataLayer.ScriviTutteLeAule(listaAule);
        }
        public static void ScriviTutteLeClassi(List<Classe> listaClassi)
        {
            DataLayer.ScriviTutteLeClassi(listaClassi);
        }
        public static void ScriviTuttiGliStudenti(List<Studente> listaStudenti)
        {
            DataLayer.ScriviTuttiGliStudenti(listaStudenti);
        }
        public static void ImportazioneDatiNuovoAnno()
        {
            // legge dal file di Spaggiari i dati di tutte le classi
            // e tutti gli studenti del nuovo anno scolastico,
            // creando i file di testo che verranno usati da questo programma 
            throw new NotImplementedException();
        }
        internal static List<Aula> LeggiTutteLeAuleUtente()
        {
            return DataLayer.LeggiTutteLeAuleUtente();
        }
        internal static List<Classe> LeggiTutteLeClassiUtente()
        {
            return DataLayer.LeggiTutteLeClassiUtente();
        }
        internal static void SalvaUnComputer(Computer computer, string chiave)
        {
            DataLayer.SalvaComputer(computer, chiave);
        }
        internal static List<Computer>? LeggiTuttiIComputer()
        {
            return DataLayer.LeggiTuttiComputer();
        }
        internal static void ScriviBanchiDellAula(List<Banco> banchi, Aula aula)
        {
            DataLayer.ScriviBanchiDellAula(banchi, aula);
        }
        internal static List<Banco> LeggiBanchiDellAula(Aula aula)
        {
            return DataLayer.LeggiBanchiDellAula(aula);
        }
        internal static List<Studente> OrdinamentoCasualeListaStudenti(List<Studente> listaStudenti)
        {
            List<Studente> listaStudentiRandom = new List<Studente>(0);
            if (listaStudenti != null)
            {
                bool[] antiRipetizioneRandom = new bool[listaStudenti.Count()];
                for (int i = listaStudenti.Count() - 1; i > 0; i--)
                {
                    Random studenteRandom = new Random();
                    int numero = studenteRandom.Next(i + 1);
                    int posizioneCicloRandom = 0;
                    for (int j = 0; j < listaStudenti.Count(); j++)
                    {
                        if (antiRipetizioneRandom[j] != true)
                        {
                            if (posizioneCicloRandom.ToString() == numero.ToString())
                            {
                                listaStudentiRandom.Add(listaStudenti[j]);
                                antiRipetizioneRandom[j] = true;
                                break;
                            }
                            else
                                posizioneCicloRandom++;
                        }
                    }
                }
                for (int i = 0; i < listaStudenti.Count(); i++)
                {
                    if (antiRipetizioneRandom[i] != true)
                    {
                        listaStudentiRandom.Add(listaStudenti[i]);
                        break;
                    }
                }

            }
            return listaStudentiRandom;
        }
        internal static List<Studente> OrdinamentoAlfabeticoListaStudenti(List<Studente> listaStudenti)
        {
            if (listaStudenti != null)
            {
                for (int i = 0; i < listaStudenti.Count() - 1; i++)
                {
                    string cognomeSort = listaStudenti[i].Cognome;
                    int indiceCognomeSort = i;

                    for (int j = i; j < listaStudenti.Count(); j++)
                    {
                        //se è 1 scambio e se è -1 no scambio
                        int valoreDiScambioDelSort = String.Compare(cognomeSort, listaStudenti[j].Cognome);
                        if (valoreDiScambioDelSort > 0)
                        {
                            cognomeSort = listaStudenti[j].Cognome;
                            indiceCognomeSort = j;
                        }
                    }

                    Studente supporto = listaStudenti[i];
                    listaStudenti[i] = listaStudenti[indiceCognomeSort];
                    listaStudenti[indiceCognomeSort] = supporto;
                }
            }
            return listaStudenti;
        }
        internal static List<Studente> OrdinamentoVotoListaStudenti(List<Studente> listaStudenti)
        {
            if (listaStudenti != null)
            {
                for (int i = 0; i < listaStudenti.Count() - 1; i++)
                {
                    double votoSort = listaStudenti[i].Media;
                    int indiceVotoSort = i;

                    for (int j = i; j < listaStudenti.Count(); j++)
                    {
                        if (listaStudenti[j].Media > votoSort)
                        {
                            votoSort = listaStudenti[j].Media;
                            indiceVotoSort = j;
                        }
                    }
                    Studente supporto = listaStudenti[i];
                    listaStudenti[i] = listaStudenti[indiceVotoSort];
                    listaStudenti[indiceVotoSort] = supporto;
                }
            }
            return listaStudenti;
        }
        internal static void ScriviAulaEClasse(Aula aula, Classe classe)
        {
            DataLayer.ScriviAulaEClasse(aula, classe);
        }
        internal static List<Aula> LeggiTutteLeAuleConClasse()
        {
            return DataLayer.LeggiTutteLeAuleEClassiCondivise();
        }
        internal static List<Computer> GeneraComputers(string SchemaPerGenerazioneNome,
            Computer DatiComputer, int NumeroIniziale, int NumeroFinale)
        {
            List<Computer> listaComputer = new List<Computer>();
            for (int i = NumeroIniziale; i <= NumeroFinale; i++)
            {
                Computer computer = new Computer(SchemaPerGenerazioneNome.Replace("*", i.ToString("00")));
                computer.MarcaComputer = DatiComputer.MarcaComputer;
                computer.Modello = DatiComputer.Modello;
                computer.Processore = DatiComputer.Processore;
                computer.TipoSistema = DatiComputer.TipoSistema;
                string[] indirizzoIP = DatiComputer.IndirizzoIPComputer.Split('.');
                computer.IndirizzoIPComputer = indirizzoIP[0] + "." + indirizzoIP[1] + "."
                    + indirizzoIP[2] + "." + i.ToString();
                computer.NoteComputer = DatiComputer.NoteComputer;
                computer.Stato = DatiComputer.Stato;
                listaComputer.Add(computer);
            }
            return listaComputer;
        }
        internal static Computer CercaComputer(string nomeDispositivo, List<Computer> lista)
        {
            return DataLayer.CercaComputer(nomeDispositivo, lista);
        }
        internal static void AggiungiComputers(List<Computer> lista)
        {
            DataLayer.AggiungiComputers(lista);
        }
        internal static List<Computer> ComputerFiltrati(string filtro, List<Computer> listaComputer)
        {
            // filtraggio dei computer 
            filtro = filtro.ToLower(); // Converti in minuscolo per confronto case-insensitive
            List<Computer> listaFiltrati = new();
            // metti nella lista solo gli elementi che corrispondono alla stringa di filtro 
            foreach (Computer item in listaComputer)
            {
                if (item.ToString().ToLower().Contains(filtro))
                {
                    // Aggiungi solo gli elementi che corrispondono al filtro
                    listaFiltrati.Add(item);
                }
            }
            return listaFiltrati;
        }
        internal static void EliminaComputer(string codiceComputer)
        {
            DataLayer.EliminaComputer(codiceComputer);
        }
    }
}