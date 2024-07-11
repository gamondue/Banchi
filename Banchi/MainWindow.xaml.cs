using Banchi.Classi;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Label = System.Windows.Controls.Label;

namespace Banchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // il sorgente aggiornato del programma Banchi è su GitHub a: 
        // https://github.com/gamondue/Banchi
        // Banchi è Free Software con licenza GPL

        private Aula aulaCorrente; // aula selezionata nella ListBox o impostata manualmente
        private Banco bancoCorrente; // banco selezionato nella ListBoxo o impostato manualmente
        private Classe classeCorrente; // classe selezionata nella ListBox o impostata manualmente
        private Studente studenteCorrente; // studente selezionato nella ListBox o impostato manualmente
        private Computer computerCorrente; // computer selezionato nella ListBox o impostato manualmente

        bool isDragging = false;
        private System.Windows.Point startPosition;

        //private Studente studenteCorrente;

        internal Label labelSelezionata;

        List<Aula> listaAuleUtente;
        List<Classe> listaClassiUtente;
        List<Aula> listaAuleCondivise;
        List<Classe> listaClassiCondivise;
        List<Computer> listaComputer;
        List<Aula> listaAuleEClassi;
        List<Studente> listaDistribuzioneBanco;

        bool cartiglioIsCheckedMainWindow = false;
        private Cartiglio cartiglio;
        private Label graficaCartiglio;
        //private RosaDeiVenti rosaDeiVenti;
        private Image graficaRosaDeiVenti;

        public MainWindow()
        {
            InitializeComponent();
            BusinessLayer.Inizializzazioni();

            if (Utente.Accesso != Utente.RuoloUtente.ModificheAiModelli)
            {
                btn_SalvataggioAulaClasseCondivise.Visibility = Visibility.Hidden;
            }
            // legge tutte le aule dal "database" e le mette in una lista
            listaAuleUtente = BusinessLayer.LeggiTutteLeAuleUtente();
            // riempimento del ComboBox con le aule appena lette
            if (listaAuleUtente != null)
                foreach (Aula a in listaAuleUtente)
                {
                    // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                    // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                    cmbAuleUtente.Items.Add(a);
                }
            listaClassiUtente = BusinessLayer.LeggiTutteLeClassiUtente();
            if (listaAuleUtente != null)
                foreach (Classe a in listaClassiUtente)
                {
                    cmbClasseUtente.Items.Add(a);
                }
            // riempio i combobox dei modelli
            listaAuleCondivise = BusinessLayer.LeggiTutteLeAule();
            // riempimento del ComboBox con le aule appena lette
            foreach (Aula a in listaAuleCondivise)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                cmbModelliAule.Items.Add(a);
            }
            // esempi di inizializzazione di un ComboBox
            //cmbModelliAule.SelectedIndex = 1; // seleziona listaAuleUtente seconda aula
            //cmbModelliAule.SelectedItem = cmbAuleUtente.Items[1]; // seleziona listaAuleUtente seconda aula
            // recupera il nome dell'aula selezionata 
            //string aulaSelezionata = ((Aula)cmbAule.SelectedItem).NomeAula;
            // recupera l'altezza dell'aula selezionata
            //double altezzaAula = ((Aula)cmbAule.SelectedItem).AltezzaInCentimetri;
            listaClassiCondivise = BusinessLayer.LeggiTutteLeClassi();
            // riempimento del ComboBox con le aule appena lette
            foreach (Classe c in listaClassiCondivise)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                cmbModelliClasse.Items.Add(c);
            }
            //cmbModelliClasse.SelectedIndex = 1;
            //cmbModelliClasse.SelectedItem = cmbClasseUtente.Items[1];

            listaAuleEClassi = BusinessLayer.LeggiTutteLeAuleConClasse();
            // riempimento del ComboBox con le aule appena lette
            foreach (Aula a in listaAuleEClassi)
            {
                cmbAulaEClasse.Items.Add(a);
            }
            listaComputer = (List<Computer>)BusinessLayer.LeggiTuttiIComputer();
            // riempimento del listbox con i computer appena letti
            riempiListBoxComputer(listaComputer);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // evento che viene lanciato alla fine del caricamento della finestra 
            // e quindi quando tutti i controlli sono pronti

            // oscura i controlli che non si possono usare, in base ai diritti dell'utente
            if (Utente.Accesso != Utente.RuoloUtente.ModificheAiModelli)
            {
                // cancella i controlli che permettono di modificare i modelli
                // e che permettono di salvare i dati

                // queste cancellazioni corrispondono al diritto "ModifichePersonali"
                btn_Banchi.Visibility = Visibility.Hidden;
                btn_Aula.Visibility = Visibility.Hidden;
                btn_Computer.Visibility = Visibility.Hidden;
                btn_Classe.Visibility = Visibility.Hidden;
                btn_SalvataggioAulaClasseCondivise.Visibility = Visibility.Hidden;
                if (Utente.Accesso != Utente.RuoloUtente.SolaLettura)
                {
                    // se è sola lettura cancella anche il salvataggio dei dati personali
                    btn_SalvataggioUtente.Visibility = Visibility.Hidden;
                }
            }
        }
        private void riempiListBoxComputer(List<Computer> lista)
        {
            lstComputer.Items.Clear();
            // riempimento del listbox con i computer che stanno in listaCorrenteComputer
            foreach (Computer c in lista)
            {
                lstComputer.Items.Add(c);
            };
        }
        internal void ClickSuCartiglio(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Label daPrendere = (Label)sender;
                isDragging = true;
                startPosition = e.GetPosition((IInputElement)this);
                daPrendere.CaptureMouse();
            }
        }
        // evento per continuare il drag and drop, quando il mouse si muove con il tasto premuto
        internal void MovimentoSuCartiglio(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label label = (Label)sender;
            if (isDragging)
            {
                System.Windows.Point currentPosition = e.GetPosition((IInputElement)this);
                double offsetX = currentPosition.X - startPosition.X;
                double offsetY = currentPosition.Y - startPosition.Y;

                Canvas.SetLeft(label, Canvas.GetLeft(label) + offsetX);
                Canvas.SetTop(label, Canvas.GetTop(label) + offsetY);

                startPosition = currentPosition;
            }
        }
        // evento per terminare il drag and drop, quando il tasto del mouse viene rilasciato
        internal void MouseUpSuCartiglio(object sender, MouseButtonEventArgs e)
        {
            // lascio il cartiglio lì dove è
            Label label = (Label)sender;
            if (isDragging)
            {
                isDragging = false;
                label.ReleaseMouseCapture();
            }
        }
        private void MenuAula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula(null);
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow wnd = new AboutWindow();
            wnd.Show();
        }
        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string workingDirectory = @"Z:\banchi\Banchi\Banchi\bin\Debug\net7.0-windows"; // Imposta la tua directory di lavoro qui
                string workingDirectory = @"Z:\banchi\Banchi\Banchi\bin\Debug\net7.0-windows"; // Imposta la tua directory di lavoro qui

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    // TODO: cambiare il percorso del file index.html usandolo come risorsa (???? incorporata ????)
                    //FileName = "Banchi\\bin\\Debug\\net7.0-windows\\HelpHTML\\index.html", // Specifica il percorso del file da avviare
                    FileName = "Banchi\\bin\\Debug\\net7.0-windows\\HelpHTML\\index.html", // Specifica il percorso del file da avviare
                    UseShellExecute = true,
                    WorkingDirectory = workingDirectory // Imposta la directory di lavoro
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "..\\..\\..\\HelpHTML\\index.html",
                UseShellExecute = true
            });

        }
        private void MenuConfigurazione_Click(object sender, RoutedEventArgs e)
        {
            ConfigurazioniWindow wnd = new ConfigurazioniWindow();
            wnd.Show();
        }
        private void ApriFinestraBanchi()
        {
            if (cmbModelliAule.SelectedItem == null)
            {
                MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BanchiWindow wnd = new BanchiWindow((Aula)cmbModelliAule.SelectedItem);
            wnd.Show();
        }
        private void btn_Aula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula(aulaCorrente);
        }
        private void ApriFinestraAula(Aula aula)
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            AulaWindow wnd = new AulaWindow(aulaCorrente);
            wnd.Show();
        }
        private void btn_SalvataggioAulaClasse_Click(object sender, RoutedEventArgs e)
        {
            if (classeCorrente == null || aulaCorrente == null)
            {
                MessageBox.Show("Selezionare una classe e un'aula", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BusinessLayer.ScriviAulaEClasse(aulaCorrente, classeCorrente);
        }
        private void btn_Banchi_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraBanchi();
        }
        private void cmbModelliClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (cmbModelliClasse.SelectedItem != null)
                {
                    List<Studente> listaStudenti = BusinessLayer.LeggiStudentiDiUnaClasse((Classe)cmbModelliClasse.SelectedItem);
                    lstStudenti.ItemsSource = listaStudenti;
                    listaDistribuzioneBanco = listaStudenti;
                }
                else
                {
                    lstStudenti.ItemsSource = null;
                }
                chkStudenti.IsChecked = true;
            }
        }
        private void cmbModelliAule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // cancella tutti i controlli dell'aula precedente
            AreaDisegno.Children.Clear();
            if (cmbModelliAule.SelectedItem != null)
            {
                aulaCorrente = ((Aula)cmbModelliAule.SelectedItem);
                // creazione dei muri dell'aula, dei banchi e della rosa dei venti
                CreaAulaGrafica(aulaCorrente);
                // aggiunta della rosa dei venti al Canvas
                aulaCorrente.MettiInScalaAulaEBanchi();
            }
        }
        private void CreaAulaGrafica(Aula aula)
        {
            Label GraficaAula = new Label();
            AreaDisegno.Children.Add(GraficaAula);
            if (aula == null)
                aula = new Aula("prova", 8000, 12000, 60, null, null, GraficaAula);
            else
                aula.GraficaAula = GraficaAula;
            // se abbiamo un valore per la direzione del nord,
            // creiamo la grafica della rosa dei venti
            if (aula.DirezioneNord != null)
            {
                graficaRosaDeiVenti = new Image();
                AreaDisegno.Children.Add(graficaRosaDeiVenti);
                aula.RosaDeiVenti = new RosaDeiVenti((double)aula.DirezioneNord, graficaRosaDeiVenti);
            }
            // creazione di tutti i nuovi banchi grafici
            foreach (Banco b in aula.Banchi)
            {
                Label GraficaBanco = new();
                // metodo delegato per gestione click
                GraficaBanco.MouseDown += ClickSuBanco;
                b.GraficaBanco = GraficaBanco;
                AreaDisegno.Children.Add(GraficaBanco);
                b.AggiungiTestoAGrafica();
                if (b.IsCattedra && aula.RosaDeiVentiX == null && aula.RosaDeiVentiY == null)
                {
                    aula.RosaDeiVenti.PosizioneXInCentimetri = b.PosizioneXInCentimetri + 200;
                    aula.RosaDeiVenti.PosizioneYInCentimetri = b.PosizioneYInCentimetri + 50;
                }
            }
        }
        private void chkStudenti_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstStudenti.Visibility = Visibility.Visible;
        }
        private void chkStudenti_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstStudenti.Visibility = Visibility.Collapsed;
        }
        private void chkComputer_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstComputer.Visibility = Visibility.Visible;
        }
        private void chkComputer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstComputer.Visibility = Visibility.Collapsed;
        }
        private void btn_Computer_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraComputer((Aula)cmbModelliAule.SelectedItem, (Computer)lstComputer.SelectedItem);
        }
        private void ApriFinestraComputer(Aula aula, Computer computer)
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Computer computer;
            //computer = (Computer)lstComputer.SelectedItem;

            MessageBoxButton bottone = MessageBoxButton.YesNo;
            MessageBoxResult result;
            ComputerWindow wnd = new ComputerWindow(aula, computer);
            wnd.Show();

            string messageboxtext = "Non hai selezionato un computer, vuoi crearne uno nuovo?";
            string messageboxcaption = "errore";

            MessageBoxImage messageBoxImage = MessageBoxImage.Question;
            //if (computer == null)
            //{
            //    result = MessageBox.Show(messageboxtext, messageboxcaption, bottone, messageBoxImage, MessageBoxResult.No);
            //    if (result == MessageBoxResult.Yes)
            //    {
            //        wnd.Show();
            //    }
            //}
            //if (computer != null)
            //{
            //wnd.Show();
            //}
        }
        private void btn_Classe_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraClasse((Classe)cmbModelliClasse.SelectedItem);
        }
        private void ApriFinestraClasse(Classe classe)
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            // se non c'è nulla di selezionato, la finestra aperta dovrà creare una nuova classe 
            ClasseWindow wnd = new ClasseWindow(classe);
            wnd.Show();
        }
        private void btn_AssociaStudente_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudenti.SelectedItem != null && bancoCorrente != null)
            {
                bancoCorrente.Studente = (Studente)lstStudenti.SelectedItem;
                bancoCorrente.AggiungiTestoAGrafica();
                bancoCorrente.GraficaBanco.BorderBrush = System.Windows.Media.Brushes.LightCoral;
            }
        }
        private void btn_AssociaComputer_Click(object sender, RoutedEventArgs e)
        {
            if (lstComputer.SelectedItem != null && bancoCorrente != null)
            {
                bancoCorrente.Computer = (Computer)lstComputer.SelectedItem;
                bancoCorrente.AggiungiTestoAGrafica();
                bancoCorrente.GraficaBanco.BorderBrush = System.Windows.Media.Brushes.LightCoral;
            }
        }
        private void ClickSuBanco(object sender, RoutedEventArgs e)
        {
            labelSelezionata = (Label)sender;
            if (aulaCorrente == null)
                return;
            // cerca nei banchi dell'aula quello che è stato cliccato
            foreach (Banco b in aulaCorrente.Banchi)
            {
                if (b.GraficaBanco == labelSelezionata)
                {
                    bancoCorrente = b;
                    labelSelezionata.BorderBrush = System.Windows.Media.Brushes.Red;
                    if (b.Studente != null)
                    {
                        studenteCorrente = b.Studente;
                        txtStudente.Text = studenteCorrente.ToString();
                    }
                    else
                    {
                        txtStudente.Text = "";
                    }
                    if (b.Computer != null)
                    {
                        computerCorrente = b.Computer;
                        txtComputer.Text = computerCorrente.NomeDispositivo;
                    }
                    else
                    {
                        txtComputer.Text = "";
                    }
                }
                else
                {
                    b.GraficaBanco.BorderBrush = System.Windows.Media.Brushes.Black;
                }
            }
        }
        private void cmbAulaEClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAulaEClasse.SelectedItem != null)
            {
                // visualizzo i file delle aule che hanno anche gli studenti 
                aulaCorrente = (Aula)cmbAulaEClasse.SelectedItem;
                classeCorrente = aulaCorrente.Classe;
                // cancella tutti i controlli dell'aula precedente
                AreaDisegno.Children.Clear();
                CreaAulaGrafica(aulaCorrente);
                aulaCorrente.MettiInScalaAulaEBanchi();
            }
        }
        private void btn_SalvataggioUtente_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_DistribuisciStudenti_Click(object sender, RoutedEventArgs e)
        {
            if (listaDistribuzioneBanco != null && aulaCorrente != null)
            {
                ////queste righe commentate servono a vedere se l'ordinamento voti funziona
                //listaDistribuzioneBanco[0].Media = 0.0;
                //listaDistribuzioneBanco[1].Media = 1.0;
                //listaDistribuzioneBanco[2].Media = 2.0;
                //listaDistribuzioneBanco[3].Media = 3.0;
                //listaDistribuzioneBanco[4].Media = 4.0;
                //listaDistribuzioneBanco[5].Media = 5.0;
                //listaDistribuzioneBanco[6].Media = 6.0;
                //listaDistribuzioneBanco[7].Media = 7.0;
                //listaDistribuzioneBanco[8].Media = 8.0;
                //listaDistribuzioneBanco[9].Media = 9.0;
                //listaDistribuzioneBanco[10].Media = 10.0;
                //listaDistribuzioneBanco[11].Media = 11.0;
                //listaDistribuzioneBanco[12].Media = 12.0;
                //listaDistribuzioneBanco[13].Media = 13.0;
                //listaDistribuzioneBanco[14].Media = 14.0;
                //listaDistribuzioneBanco[15].Media = 15.0;
                //listaDistribuzioneBanco[16].Media = 16.0;

                if (rdbCasuale.IsChecked == true)
                    listaDistribuzioneBanco = BusinessLayer.OrdinamentoCasualeListaStudenti(listaDistribuzioneBanco);
                else
                {
                    if (rdbAlfabetico.IsChecked == true)
                        listaDistribuzioneBanco = BusinessLayer.OrdinamentoAlfabeticoListaStudenti(listaDistribuzioneBanco);
                    else
                        listaDistribuzioneBanco = BusinessLayer.OrdinamentoVotoListaStudenti(listaDistribuzioneBanco);
                }
                // distribuzione degli studenti nei banchi
                int minimoLunghezzaListe = aulaCorrente.Banchi.Count();
                if (listaDistribuzioneBanco.Count() < minimoLunghezzaListe)
                    minimoLunghezzaListe = listaDistribuzioneBanco.Count();
                for (int i = 0; i < minimoLunghezzaListe; i++)
                {
                    // non mettiamo studenti nelle cattedre..
                    if (!aulaCorrente.Banchi[i].IsCattedra)
                    {
                        aulaCorrente.Banchi[i].Studente = listaDistribuzioneBanco[i];
                        aulaCorrente.Banchi[i].AggiungiTestoAGrafica();
                    }
                }
            }
        }
        private void txtFiltroComputer_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Computer> listaFiltrati = BusinessLayer.ComputerFiltrati(txtFiltroComputer.Text, listaComputer);
            riempiListBoxComputer(listaFiltrati);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.IsLoaded && aulaCorrente != null)
                aulaCorrente.MettiInScalaAulaEBanchi();
        }
        //private Aula CreaAulaDiProva()
        //{
        //    // QUESTO METODO CREA UN'AULA DI PROVA E DOVRA' ESSERE CANCELLATO QUANDO LE 
        //    // AULE CARICATE NEL COMBOBOX AVRANNO AL LORO INTERNO LE INFORMAZIONI
        //    // CHE METTIAMO NEL SEGUENTE CODICE 
        //    // creazione aula
        //    Label GraficaAula = new Label();
        //    AreaDisegno.Children.Add(GraficaAula);

        //    Aula aula = new Aula("prova", 8000, 12000, GraficaAula);
        //    // creazione di un nuovo banco
        //    Label GraficaBanco = new();
        //    // metodo delegato per gestione click
        //    GraficaBanco.MouseDown += ClickSuBanco;
        //    AreaDisegno.Children.Add(GraficaBanco);
        //    Banco bancoNuovo = new Banco(false, 100, 80, 250, 100, GraficaBanco);
        //    // aggiunta del banco appena fatto all'aula
        //    aula.Banchi.Add(bancoNuovo);
        //    Panel.SetZIndex(GraficaBanco, 100);

        //    GraficaBanco = new();
        //    // metodo delegato per gestione click
        //    GraficaBanco.MouseDown += ClickSuBanco;
        //    AreaDisegno.Children.Add(GraficaBanco);
        //    bancoNuovo = new Banco(false, 100, 80, 250, 200, GraficaBanco);
        //    // aggiunta del banco appena fatto all'aula
        //    aula.Banchi.Add(bancoNuovo);
        //    Panel.SetZIndex(GraficaBanco, 101);

        //    return aula;
        //}
        private void chkCartiglio_Checked(object sender, RoutedEventArgs e)
        {
            //aulaCorrente = (Aula)(cmbModelliAule.SelectedItem);
            if (aulaCorrente == null)
            {
                MessageBox.Show("Selezionare un'aula, se si vuole avere il cartiglio");
                chkCartiglio.IsChecked = false;
                return;
            }
            //classeCorrente = (Classe)(cmbModelliClasse.SelectedItem);
            //if (classeCorrente == null)
            //{
            //    MessageBox.Show("Selezionare una classe, se si vuole avere il cartiglio");
            //    chkCartiglio.IsChecked = false;
            //    return;
            //}

            graficaCartiglio = new();
            // metodi delegati per gestione drag and drop del cartiglio
            graficaCartiglio.MouseDown += ClickSuCartiglio;
            graficaCartiglio.MouseMove += MovimentoSuCartiglio;
            graficaCartiglio.MouseUp += MouseUpSuCartiglio;
            AreaDisegno.Children.Add(graficaCartiglio);

            cartiglio = new Cartiglio(aulaCorrente, classeCorrente, Utente.Username, graficaCartiglio);
            aulaCorrente.Cartiglio = cartiglio;
        }
        private void chkCartiglio_Unchecked(object sender, RoutedEventArgs e)
        {
            AreaDisegno.Children.Remove(graficaCartiglio);
            graficaCartiglio = null;
            cartiglio = null;
        }
        private void lstStudenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem != null)
            {
                studenteCorrente = (Studente)lb.SelectedItem;
                txtStudente.Text = studenteCorrente.ToString();
            }
        }
        private void lstComputer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem != null)
            {
                computerCorrente = (Computer)lb.SelectedItem;
                txtComputer.Text = computerCorrente.NomeDispositivo;
            }
        }
        private void cmbClasseUtente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classeCorrente = (Classe)cmbClasseUtente.SelectedItem;
        }
        private void cmbAuleUtente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aulaCorrente = (Aula)cmbAuleUtente.SelectedItem;
        }
        private void btnStampa_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                System.Windows.Size pageSize = new System.Windows.Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
                System.Windows.Size s = new System.Windows.Size(aulaCorrente.BaseInCentimetri * aulaCorrente.FattoreDiScala,
                    aulaCorrente.AltezzaInCentimetri * aulaCorrente.FattoreDiScala);
                AreaDisegno.Arrange(new Rect(s));
                printDlg.PrintVisual(AreaDisegno, "Stampa Aule e Banchi");
            }
        }
        private void MenuEsci_Click(object sender, RoutedEventArgs e)
        {
            // Chiude l'applicazione
            Application.Current.Shutdown();
        }
        private void MenuImportExport_Click(object sender, RoutedEventArgs e)
        {
            ImportExportWindow wnd = new ImportExportWindow();
            wnd.Show();
        }
        private void MenuNuovaAula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula(null);
        }
        private void MenuNuovaClasse_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraClasse(null);
        }
        private void MenuNuovoComputer_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraComputer(null, null);
        }
        private void MenuReport_Click(object sender, RoutedEventArgs e)
        {
            // spedisce la email aprendo il mailer 
            Banchi.BusinessLayer.SpeditoreEmail.LancioDelMailerDiDefault("gamon@ingmonti.it", "Prova Oggetto",
                "Corpo del messaggio solo testuale che sto provando a mandare.");
        }
    }
}