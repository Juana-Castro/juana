using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ZXing.QrCode;

namespace PGE.Util
{
    public enum FormatoLiteral { Simple, Factura }

    public static class Funciones
    {
        public static bool Contiene(IList lista, params object[] objetos)
        {
            bool resultado = false;
            if (lista != null)
            {
                foreach (object objeto in objetos)
                {
                    resultado = (resultado | lista.Contains(objeto));
                }
            }
            return resultado;
        }

        public static string ObtenerSHA1(string cadena)
        {
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(cadena);

            var sha = new SHA1CryptoServiceProvider();
            var shaBytes = sha.ComputeHash(bytes);
            var sb = new StringBuilder();
            int logintud = shaBytes.Length;

            for (int i = 0; i < logintud - 1; i++)
            {
                if (shaBytes[i] < 16)
                    sb.Append("0");
                sb.Append(shaBytes[i].ToString("x"));
            }
            return sb.ToString().ToUpper();
        }

        public static string GenerarCadenaAleatoria(int longitud, bool incluirMiniusculas = true, bool incluirMayusculas = true, bool incluirNumeros = true, bool incluirSimbolos = true)
        {
            Random rdn = new Random();
            string minusculas = "abcdefghijklmnopqrstuvwxyz";
            string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "1234567890";
            string simbolos = "%$#@";
            string caracteres = string.Empty;

            if (incluirMiniusculas)
            {
                caracteres += minusculas;
            }
            if (incluirMayusculas)
            {
                caracteres += mayusculas;
            }
            if (incluirNumeros)
            {
                caracteres += numeros;
            }
            if (incluirSimbolos)
            {
                caracteres += simbolos;
            }

            int longitudCaracteres = caracteres.Length;
            char letra;

            string cadenaAleatoria = string.Empty;
            for (int i = 0; i < longitud; i++)
            {
                letra = caracteres[rdn.Next(longitudCaracteres)];
                cadenaAleatoria += letra.ToString();
            }

            return cadenaAleatoria;
        }

        [Obsolete("Se recomineda usar el método GenerarCadenaAleatoria", true)]
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        [Obsolete("Se recomineda usar el método GenerarCadenaAleatoria", true)]
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        [Obsolete("Se recomineda usar el método GenerarCadenaAleatoria", true)]
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string ObtenerLiteral(decimal numero, FormatoLiteral formato = FormatoLiteral.Simple, string moneda = "")
        {
            string literalParteDecimal = string.Empty;

            var parteEntera = Convert.ToInt64(Math.Truncate(numero));
            var parteDecimal = Convert.ToInt32(Math.Round((numero - parteEntera) * 100, 2));

            if (parteDecimal > 0)
            {
                switch (formato)
                {
                    case FormatoLiteral.Simple:
                        literalParteDecimal = " CON " + GenerarLiteral(Convert.ToDouble(parteDecimal));
                        break;
                    case FormatoLiteral.Factura:
                        literalParteDecimal = $" {moneda.ToUpper()} {parteDecimal:0,0}/100";
                        break;
                }
            }

            return GenerarLiteral(Convert.ToDouble(parteEntera)) + literalParteDecimal;
        }

        public static string ObtenerLiteral(int numero)
        {
            return GenerarLiteral(Convert.ToDouble(numero));
        }

        public static string ObtenerBase64CodigoQR(string cadena, int ancho = 300, int alto = 300, int margen = 0)
        {
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions { Height = alto, Width = ancho, Margin = margen }
            };

            var pixelData = qrWriter.Write(cadena.Trim());

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string ObtenerBase64CodigoPDF417(string cadena, int ancho = 300, int alto = 300, int margen = 0)
        {
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.PDF_417,
                Options = new QrCodeEncodingOptions { Height = alto, Width = ancho, Margin = margen }
            };

            var pixelData = qrWriter.Write(cadena.Trim());

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string RedimensionarImagenBase64(string imagenBase64, int ancho)
        {
            byte[] bytesImage = System.Convert.FromBase64String(imagenBase64);

            Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(bytesImage);

            decimal porcentajeAjuste = (decimal)ancho / (decimal)image.Width;

            if (porcentajeAjuste < 1)
            {
                image.Mutate(x => x.Resize((int)Math.Round(image.Width * porcentajeAjuste, 0), (int)Math.Round(image.Height * porcentajeAjuste, 0)));
            }

            using (var outputStream = new MemoryStream())
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 80,
                };
                image.Save(outputStream, encoder);
                var bytes = outputStream.ToArray();
                return Convert.ToBase64String(bytes);
            }

            //using (Image<Rgba32> image = Image.Load(bytes))
            //{
            //    image.Mutate(x => x
            //         .Resize(image.Width / 2, image.Height / 2)
            //         .Grayscale());
            //    //image.Save("bar.jpg"); // Automatic encoder selected based on extension.

            //    return image.ToBase64String(  ;
            //}
        }

        public static string RedimensionarImagenBase64(string imagenBase64, int ancho, int alto)
        {
            byte[] bytesImage = System.Convert.FromBase64String(imagenBase64);

            Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(bytesImage);

            decimal porcentajeAjuste = 0;

            if ((decimal)alto / (decimal)image.Height <= (decimal)ancho / (decimal)image.Width)
            {
                porcentajeAjuste = (decimal)alto / (decimal)image.Height;
            }
            else
            {
                porcentajeAjuste = (decimal)ancho / (decimal)image.Width;
            }

            if (porcentajeAjuste < 1)
            {
                image.Mutate(x => x.Resize((int)Math.Round(image.Width * porcentajeAjuste, 0), (int)Math.Round(image.Height * porcentajeAjuste, 0)));
            }

            using (var outputStream = new MemoryStream())
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 80,
                };
                image.Save(outputStream, encoder);
                var bytes = outputStream.ToArray();
                return Convert.ToBase64String(bytes);
            }

            //using (Image<Rgba32> image = Image.Load(bytes))
            //{
            //    image.Mutate(x => x
            //         .Resize(image.Width / 2, image.Height / 2)
            //         .Grayscale());
            //    //image.Save("bar.jpg"); // Automatic encoder selected based on extension.

            //    return image.ToBase64String(  ;
            //}
        }

        public static string Encriptar(string cadena, string clave)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(cadena);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.Key = Convert.FromBase64String(RestaurarCadenaBase64(clave));
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform cryptoTransform = aes.CreateEncryptor();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                string encrypted = LimpiarCadenaBase64(Convert.ToBase64String(ms.ToArray()));
                aes.Clear();
                cs.Dispose();
                ms.Dispose();
                return encrypted;
            }
            catch (Exception ex)
            {
                throw new Exception("Encriptacion", ex);
            }
        }

        public static string Desencriptar(string cadenaEncriptada, string clave)
        {
            try
            {
                byte[] bytesEncriptados = Convert.FromBase64String(RestaurarCadenaBase64(cadenaEncriptada));
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.Key = Convert.FromBase64String(RestaurarCadenaBase64(clave));
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform cryptoTransform = aes.CreateDecryptor();
                MemoryStream ms = new MemoryStream(bytesEncriptados);
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read);
                byte[] bytes = new byte[bytesEncriptados.Length];
                int readByteCount = cs.Read(bytes, 0, bytes.Length);
                string cadenaDesencriptada = Encoding.UTF8.GetString(bytes, 0, readByteCount);
                aes.Clear();
                cs.Dispose();
                ms.Dispose();
                return cadenaDesencriptada;
            }
            catch (Exception ex)
            {
                throw new Exception("Desencriptacion", ex);
            }
        }

        /*Funciones privadas*/
        private static string GenerarLiteral(double numero)
        {
            string literal;
            numero = Math.Truncate(numero);
            if (numero == 0) literal = "CERO";
            else if (numero == 1) literal = "UNO";
            else if (numero == 2) literal = "DOS";
            else if (numero == 3) literal = "TRES";
            else if (numero == 4) literal = "CUATRO";
            else if (numero == 5) literal = "CINCO";
            else if (numero == 6) literal = "SEIS";
            else if (numero == 7) literal = "SIETE";
            else if (numero == 8) literal = "OCHO";
            else if (numero == 9) literal = "NUEVE";
            else if (numero == 10) literal = "DIEZ";
            else if (numero == 11) literal = "ONCE";
            else if (numero == 12) literal = "DOCE";
            else if (numero == 13) literal = "TRECE";
            else if (numero == 14) literal = "CATORCE";
            else if (numero == 15) literal = "QUINCE";
            else if (numero < 20) literal = "DIECI" + GenerarLiteral(numero - 10);
            else if (numero == 20) literal = "VEINTE";
            else if (numero < 30) literal = "VEINTI" + GenerarLiteral(numero - 20);
            else if (numero == 30) literal = "TREINTA";
            else if (numero == 40) literal = "CUARENTA";
            else if (numero == 50) literal = "CINCUENTA";
            else if (numero == 60) literal = "SESENTA";
            else if (numero == 70) literal = "SETENTA";
            else if (numero == 80) literal = "OCHENTA";
            else if (numero == 90) literal = "NOVENTA";
            else if (numero < 100) literal = GenerarLiteral(Math.Truncate(numero / 10) * 10) + " Y " + GenerarLiteral(numero % 10);
            else if (numero == 100) literal = "CIEN";
            else if (numero < 200) literal = "CIENTO " + GenerarLiteral(numero - 100);
            else if ((numero == 200) || (numero == 300) || (numero == 400) || (numero == 600) || (numero == 800)) literal = GenerarLiteral(Math.Truncate(numero / 100)) + "CIENTOS";
            else if (numero == 500) literal = "QUINIENTOS";
            else if (numero == 700) literal = "SETECIENTOS";
            else if (numero == 900) literal = "NOVECIENTOS";
            else if (numero < 1000) literal = GenerarLiteral(Math.Truncate(numero / 100) * 100) + " " + GenerarLiteral(numero % 100);
            else if (numero == 1000) literal = "MIL";
            else if (numero < 2000) literal = "MIL " + GenerarLiteral(numero % 1000);
            else if (numero < 1000000)
            {
                literal = GenerarLiteral(Math.Truncate(numero / 1000)) + " MIL";
                if ((numero % 1000) > 0)
                {
                    literal = literal + " " + GenerarLiteral(numero % 1000);
                }
            }
            else if (numero == 1000000)
            {
                literal = "UN MILLON";
            }
            else if (numero < 2000000)
            {
                literal = "UN MILLON " + GenerarLiteral(numero % 1000000);
            }
            else if (numero < 1000000000000)
            {
                literal = GenerarLiteral(Math.Truncate(numero / 1000000)) + " MILLONES ";
                if ((numero - Math.Truncate(numero / 1000000) * 1000000) > 0)
                {
                    literal = literal + " " + GenerarLiteral(numero - Math.Truncate(numero / 1000000) * 1000000);
                }
            }
            else if (numero == 1000000000000) literal = "UN BILLON";
            else if (numero < 2000000000000) literal = "UN BILLON " + GenerarLiteral(numero - Math.Truncate(numero / 1000000000000) * 1000000000000);
            else
            {
                literal = GenerarLiteral(Math.Truncate(numero / 1000000000000)) + " BILLONES";
                if ((numero - Math.Truncate(numero / 1000000000000) * 1000000000000) > 0)
                {
                    literal = literal + " " + GenerarLiteral(numero - Math.Truncate(numero / 1000000000000) * 1000000000000);
                }
            }
            return literal;
        }

        private static string LimpiarCadenaBase64(string cadenaBase64)
        {
            string cadenaLimpia = cadenaBase64.Trim();
            cadenaLimpia = cadenaLimpia.Replace("+", "-");
            cadenaLimpia = cadenaLimpia.Replace("/", "_");
            if (cadenaLimpia.Contains("="))
            {
                cadenaLimpia = cadenaLimpia.Substring(0, cadenaLimpia.IndexOf("="));
            }
            return cadenaLimpia;
        }

        private static string RestaurarCadenaBase64(string cadenaBase64Limpia)
        {
            string cadenaRestaurada = cadenaBase64Limpia.Trim();
            cadenaRestaurada = cadenaRestaurada.Replace("-", "+");
            cadenaRestaurada = cadenaRestaurada.Replace("_", "/");
            if (cadenaRestaurada.Length % 4 != 0)
            {
                cadenaRestaurada = cadenaRestaurada.PadRight(((int)(cadenaRestaurada.Length / 4) + 1) * 4, '=');
            }
            return cadenaRestaurada;
        }
    }

    public static class Funciones<T>
        where T : class, new()
    {
        public static List<T> ObtenerLista(T objeto)
        {
            List<T> lista = new List<T>();
            lista.Add(objeto);
            return lista;
        }

        public static void Copiar(T origen, ref T destino)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
            PropertyInfo[] propiedadesOrigen = origen.GetType().GetProperties(bindingFlags);
            foreach (PropertyInfo propiedad in propiedadesOrigen)
            {
                if (destino.GetType().GetProperty(propiedad.Name) != null && destino.GetType().GetProperty(propiedad.Name).CanWrite)
                {
                    destino.GetType().GetProperty(propiedad.Name).SetValue(destino, origen.GetType().GetProperty(propiedad.Name).GetValue(origen, null), new object[] { });
                }
            }
        }

        public static void Copiar(List<T> listaOrigen, ref List<T> listaDestino)
        {
            foreach (T origen in listaOrigen)
            {
                T destino = new T();
                Copiar(origen, ref destino);
                listaDestino.Add(destino);
            }
        }

        public static void LimpiarEspacios(T objeto)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
            List<PropertyInfo> propiedadesObjeto = objeto.GetType().GetProperties(bindingFlags).Where(prop => prop.PropertyType == typeof(string)).Where(prop => prop.CanWrite && prop.CanRead).ToList();
            foreach (PropertyInfo propiedad in propiedadesObjeto)
            {
                if (objeto.GetType().GetProperty(propiedad.Name).GetValue(objeto, null) != null)
                {
                    objeto.GetType().GetProperty(propiedad.Name).SetValue(objeto, objeto.GetType().GetProperty(propiedad.Name).GetValue(objeto, null).ToString().Trim(), new object[] { });
                }
            }
        }

        public static void LimpiarEspacios(List<T> lista)
        {
            foreach (T objeto in lista)
            {
                LimpiarEspacios(objeto);
            }
        }

        public static void Mayusculas(T objeto, bool limpiarEspacios = true)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
            List<PropertyInfo> propiedadesObjeto = objeto.GetType().GetProperties(bindingFlags).Where(prop => prop.PropertyType == typeof(string)).Where(prop => prop.CanWrite && prop.CanRead).ToList();
            foreach (PropertyInfo propiedad in propiedadesObjeto)
            {
                if (objeto.GetType().GetProperty(propiedad.Name).GetValue(objeto, null) != null)
                {
                    if (limpiarEspacios)
                    {
                        objeto.GetType().GetProperty(propiedad.Name).SetValue(objeto, objeto.GetType().GetProperty(propiedad.Name).GetValue(objeto, null).ToString().Trim().ToUpper(), new object[] { });
                    }
                    else
                    {
                        objeto.GetType().GetProperty(propiedad.Name).SetValue(objeto, objeto.GetType().GetProperty(propiedad.Name).GetValue(objeto, null).ToString().ToUpper(), new object[] { });
                    }
                }
            }
        }

        public static void Mayusculas(List<T> lista)
        {
            foreach (T objeto in lista)
            {
                Mayusculas(objeto);
            }
        }

    }
}
