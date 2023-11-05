namespace PGE.Util.Extensiones
{
    public static class Extensiones
    {
        public static string ObtenerSHA1(this string cadena)
        {
            return Funciones.ObtenerSHA1(cadena);
        }

        public static string ObtenerLiteral(this decimal numero, FormatoLiteral formato = FormatoLiteral.Simple, string moneda = "")
        {
            return Funciones.ObtenerLiteral(numero, formato, moneda);
        }

        public static string ObtenerLiteral(this int numero)
        {
            return Funciones.ObtenerLiteral(numero);
        }

    }
}
