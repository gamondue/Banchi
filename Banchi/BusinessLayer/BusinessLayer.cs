namespace Banchi
{
    internal static partial class BusinessLayer
    {
        internal static List<Classe> LeggiTutteLeClassi()
        {
            return DataLayer.LeggiTutteLeClassi();
        }
        internal static List<Aula> LeggiTutteLeAule()
        {
            return DataLayer.LeggiTutteLeAule();
        }
    }
}
