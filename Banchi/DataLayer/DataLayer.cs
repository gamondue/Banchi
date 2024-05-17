using System.IO;

namespace Banchi
{
    internal static partial class DataLayer
    {
        static List<Aula> listaAule = new List<Aula>();
        static List<Classe> listaClassi = new List<Classe>();
        static List<Studente> listaStudenti = new List<Studente>();
        static string[] arraySupporto= new string[100];
        internal static List<Aula> LeggiTutteLeAule() //da fare tutti i leggi (abbiamo fatto dei fake)
        {
            Aula a = new Aula("L13", 10.2, 5.1);
            listaAule.Add(a);
            a = new Aula("P12", 10.2, 5.1);
            listaAule.Add(a);
            a = new Aula("L10", 10.2, 5.1);
            listaAule.Add(a);

            return listaAule;
        }

        internal static void ScriviTutteLeAule()
        {
            for(int i = 0; i< listaAule.Count; i++)
            {
                arraySupporto[i] = listaAule[i].NomeAula.ToString() + "\t" + listaAule[i].BaseInMetri.ToString() + "\t" + listaAule[i].AltezzaInMetri.ToString();
            }
            File.AppendAllLines("Aule.tsv", arraySupporto);
        }
          
        internal static List<Classe> LeggiTutteLeClassi() //da fare tutti i leggi (abbiamo fatto dei fake)
        {
            Classe c = new Classe("3N", "23-24");
            listaClassi.Add(c);
            c = new Classe("3B", "23-24");
            listaClassi.Add(c);
            c = new Classe("3H", "23-24");
            listaClassi.Add(c);

            return listaClassi;
        }

        internal static void ScriviTutteLeClassi()
        {
            for (int i = 0; i < listaClassi.Count; i++)
            {
                arraySupporto[i] = listaClassi[i].CodiceClasse.ToString() + "\t" + listaClassi[i].AnnoScolastico.ToString();
            }
            File.AppendAllLines("Classi.tsv", arraySupporto);
        }

        internal static List<Studente> LeggiTuttiGliStudenti() //da fare tutti i leggi (abbiamo fatto dei fake)
        {
            Studente s = new Studente("Michele", "Impagnatiello", "3N");
            listaStudenti.Add(s);
            s = new Studente("Michele", "Impagnatiello", "3N");
            listaStudenti.Add(s);
            s = new Studente("Michele", "Impagnatiello", "3N");
            listaStudenti.Add(s);

            return listaStudenti;
        }
        internal static void ScriviTuttiStudenti()
        {
            for (int i = 0; i < listaStudenti.Count; i++)
            {
                arraySupporto[i] = listaStudenti[i].Nome.ToString() + "\t" + listaStudenti[i].Cognome.ToString() + "\t" + listaStudenti[i].CodiceClasse.ToString();
            }
            File.AppendAllLines("Studenti.tsv", arraySupporto);
        }
    }
}
