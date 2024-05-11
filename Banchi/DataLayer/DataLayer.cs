namespace Banchi.DataLayer
{
    internal static partial class DataLayer
    {
        internal static List<Aula> ListLeggiTutteLeAule()
        {
            List<Aula> lista = new List<Aula>();
            Aula a = new Aula("L13", 10.2, 5.1);
            lista.Add(a);
            a = new Aula("P12", 10.2, 5.1);
            lista.Add(a);
            a = new Aula("L10", 10.2, 5.1);
            lista.Add(a);

            return lista;
        }
        internal static List<Classe> ListLeggiTutteLeClassi()
        {
            throw new NotImplementedException();
        }
    }
}
