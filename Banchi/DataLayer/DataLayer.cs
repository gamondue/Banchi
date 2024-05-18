using System.IO;

namespace Banchi
{
    internal static partial class DataLayer
    {
        static List<Aula> listaAule = new List<Aula>();
        static List<Classe> listaClassi = new List<Classe>();
        static List<Studente> listaStudenti = new List<Studente>();
        static string[] arraySupporto= new string[100];

        internal static List<Aula> LeggiTutteLeAule()
        {
            string[] stringheLette = File.ReadAllLines("Aule.tsv");
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
            for(int i = 0; i< listaAule.Count; i++)
            {
                arraySupporto[i] = listaAule[i].NomeAula.ToString() + "\t" + listaAule[i].BaseInMetri.ToString() + "\t" + listaAule[i].AltezzaInMetri.ToString();
            }
            File.AppendAllLines("Aule.tsv", arraySupporto);  
        }
          
        internal static List<Classe> LeggiTutteLeClassi()
        {

            string[] stringheLette = File.ReadAllLines("Classi.tsv");
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
            File.AppendAllLines("Classi.tsv", arraySupporto);
        }

        internal static List<Studente> LeggiTuttiGliStudenti()
        {
            string[] stringheLette = File.ReadAllLines("Studenti.tsv");
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
            File.AppendAllLines("Studenti.tsv", arraySupporto);
        }

    }
}
