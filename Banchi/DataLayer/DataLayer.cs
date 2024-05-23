using System.Windows.Controls;

namespace Banchi
{
    internal static partial class DataLayer
    {
        
        internal static List<Aula> LeggiTutteLeAule()
        {
            Label grafica = new Label();
            List<Aula> lista = new List<Aula>();
            Aula a = new Aula("L13", 10.2, 5.1, grafica);
            lista.Add(a);
            a = new Aula("P12", 10.2, 5.1, grafica);
            lista.Add(a);
            a = new Aula("L10", 10.2, 5.1, grafica);
            lista.Add(a);

            return lista;
        }
        internal static List<Classe> LeggiTutteLeClassi()
        {
            throw new NotImplementedException();
        }
    }
}
