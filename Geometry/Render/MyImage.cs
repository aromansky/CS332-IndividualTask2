using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class MyImage
    {
        private Bitmap image;
        private BitmapData data;
        private int width;
        private int height;
        private int stride;
        private byte[] pixels;
        private int bytesPerPixel;
        private bool locked = false;

        public int Width
        {
            get { return this.width; }
            private set { width = value; }
        }
        public Bitmap Img
        {
            get { return image; }
            private set { image = value; }
        }
        public int Height
        {
            get { return this.height; }
            private set { height = value; }
        }
        public MyImage(Bitmap img)
        {
            Img = img;
            Width = img.Width;
            Height = img.Height;
        }

        public MyImage(string fname)
        {
            Img = new Bitmap(fname);
            Width = Img.Width;
            Height = Img.Height;
        }

        public void Lock()
        {
            data = image.LockBits(
                new Rectangle(0, 0, Width, Height),
                ImageLockMode.ReadWrite,
                image.PixelFormat);

            this.locked = true;
            this.stride = data.Stride;
            bytesPerPixel = Image.GetPixelFormatSize(image.PixelFormat) / 8;

            int bytes = Math.Abs(stride) * Height;
            pixels = new byte[bytes];
            Marshal.Copy(data.Scan0, pixels, 0, bytes);
        }

        public void Unlock()
        {
            if (!locked)
            {
                return;
            }

            Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
            Img.UnlockBits(data);

            locked = false;
            pixels = null;
        }

        public Color GetRGB(int x, int y)
        {
            if (!locked) throw new InvalidOperationException("Bitmap not locked");

            int position = y * this.stride + x * bytesPerPixel;

            byte b = pixels[position];
            byte g = pixels[position + 1];
            byte r = pixels[position + 2];
            return Color.FromArgb(r, g, b);
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (!locked) throw new InvalidOperationException("Bitmap not locked");

            int position = y * this.stride + x * bytesPerPixel;

            pixels[position] = (byte)color.B;
            pixels[position + 1] = (byte)color.G;
            pixels[position + 2] = (byte)color.R;
        }

        public MyImage Copy()
        {
            return new MyImage(new Bitmap(Img));
        }

        public void Save(string path)
        {
            try
            {
                ImageFormat format = GetImageFormat(path);
                Img.Save(path, format);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении изображения: {ex.Message}", ex);
            }
        }


        private ImageFormat GetImageFormat(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            switch (extension)
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".tiff":
                case ".tif":
                    return ImageFormat.Tiff;
                default:
                    return ImageFormat.Bmp;
            }
        }

        public static MyImage CreateWhiteImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            return new MyImage(bitmap);
        }
    }
}
