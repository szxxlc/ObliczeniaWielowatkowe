using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("ImageProcessingGUI")]

namespace ObliczeniaWielowatkowe
{
    internal class ImageProcessing
    {
        private Bitmap image;

        public ImageProcessing(Bitmap bitmap)
        {
            image = (Bitmap)bitmap.Clone();
        }

        public void StartAllFilters(
            Action<Bitmap> onThreshold,
            Action<Bitmap> onNegative,
            Action<Bitmap> onPink,
            Action<Bitmap> onGrayscale)
        {
            Bitmap imageForThreshold = (Bitmap)image.Clone();
            Bitmap imageForNegative = (Bitmap)image.Clone();
            Bitmap imageForPink = (Bitmap)image.Clone();
            Bitmap imageForGrayscale = (Bitmap)image.Clone();

            Thread thresholdThread = new Thread(() =>
            {
                Bitmap result = ApplyThreshold(imageForThreshold, 128);
                onThreshold?.Invoke(result);
            });

            Thread negativeThread = new Thread(() =>
            {
                Bitmap result = ApplyNegative(imageForNegative);
                onNegative?.Invoke(result);
            });

            Thread pinkThread = new Thread(() =>
            {
                Bitmap result = ApplyPinkFilter(imageForPink);
                onPink?.Invoke(result);
            });

            Thread grayscaleThread = new Thread(() =>
            {
                Bitmap result = ApplyGrayscale(imageForGrayscale);
                onGrayscale?.Invoke(result);
            });

            thresholdThread.Start();
            negativeThread.Start();
            pinkThread.Start();
            grayscaleThread.Start();
        }

        private Bitmap ApplyGrayscale(Bitmap bmp)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
                    result.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return result;
        }

        private Bitmap ApplyNegative(Bitmap bmp)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            return result;
        }

        private Bitmap ApplyThreshold(Bitmap bmp, int threshold)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int brightness = (c.R + c.G + c.B) / 3;
                    Color color = brightness < threshold ? Color.Black : Color.White;
                    result.SetPixel(x, y, color);
                }
            }
            return result;
        }

        private Bitmap ApplyPinkFilter(Bitmap bmp)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int r = Math.Min(255, c.R + 50);
                    int g = Math.Max(0, c.G - 50);
                    int b = Math.Min(255, c.B + 50);
                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return result;
        }
    }
}
