namespace Banchi
{
    internal static partial class BusinessLayer
    {
        internal static void Inizializzazioni()
        {
            DataLayer.Inizializzazioni();
        }
        internal static List<Classe> LeggiTutteLeClassi()
        {
            return DataLayer.LeggiTutteLeClassi();
        }
        internal static List<Aula> LeggiTutteLeAule()
        {
            return DataLayer.LeggiTutteLeAule();
        }
        internal static List<Studente> LeggiTuttiGliStudenti()
        {
            return DataLayer.LeggiTuttiGliStudenti();
        }
        internal static void LeggiStudentiClasse(Classe classe)
        {
            DataLayer.LeggiStudentiClasse(classe);
        }
        internal static void ScriviTutteLeAule(List<Aula> listaAule)
        {
            DataLayer.ScriviTutteLeAule(listaAule);
        }
        internal static void ScriviTutteLeClassi(List<Classe> listaClassi)
        {
            DataLayer.ScriviTutteLeClassi(listaClassi);
        }
        internal static void ScriviTuttiGliStudenti(List<Studente> listaStudenti)
        {
            DataLayer.ScriviTuttiGliStudenti(listaStudenti);
        }
        internal static void ImportazioneDatiNuovoAnno()
        {
            // legge dal file di Spaggiari i dati di tutte le classi
            // e tutti gli studenti del nuovo anno scolastico,
            // creando i file di testo che verranno usati da questo programma 
            throw new NotImplementedException();
        }
    }
}