using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace PGE.Util
{
    public static class ImagenHelper
    {
        [Obsolete("Se recomineda usar el método Funciones.RedimensionarImagenBase64", true)]
        public static string RedimensionarImagenBase64(string imagenBase64, int ancho)
        {
            byte[] bytesImage = System.Convert.FromBase64String(imagenBase64);

            Image<Rgba32> image = Image.Load(bytesImage);

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

        [Obsolete("Se recomineda usar el método Funciones.RedimensionarImagenBase64", true)]
        public static string RedimensionarImagenBase64(string imagenBase64, int ancho, int alto)
        {
            byte[] bytesImage = System.Convert.FromBase64String(imagenBase64);

            Image<Rgba32> image = Image.Load(bytesImage);

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
    }
}
