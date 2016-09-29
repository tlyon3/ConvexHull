using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace _2_convex_hull
{
    class DirectBitmapWriter : IDisposable
    {
        private Bitmap bitmap;
        private byte[] imageBuffer;
        private BitmapData bitmapData;

        private const int pixelWidth = 3;

        public DirectBitmapWriter(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            InitializeBuffer();
        }

        /// <summary>
        /// Initializes the bitmap data and imagebuffer.
        /// </summary>
        private void InitializeBuffer()
        {
            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            imageBuffer = new byte[bitmapData.Stride * bitmap.Height];
        }

        /// <summary>
        /// Sets the pixel in the image buffer.
        /// </summary>
        /// <param name="point">Coordinates of the pixel</param>
        /// <param name="color">The color to be assigned to blue, green, and red.</param>
        public void SetPixel(Point point, Color color)
        {
            SetPixel(point.X, point.Y, color);
        }

        /// <summary>
        /// Sets the pixel in the image buffer.
        /// </summary>
        /// <param name="x">X-Coordinate of the pixel</param>
        /// <param name="y">Y-Coordinate of the pixel</param>
        /// <param name="color">The color to be assigned to blue, green, and red.</param>
        public void SetPixel(int x, int y, Color color)
        {
            imageBuffer[bitmapData.Stride * y + x * pixelWidth] = color.R;
            imageBuffer[bitmapData.Stride * y + x * pixelWidth + sizeof(byte)] = color.G;
            imageBuffer[bitmapData.Stride * y + x * pixelWidth + 2 * sizeof(byte)] = color.B;
        }

        public void Clear(Color color)
        {
            for (int y = 0; y < bitmap.Height; ++y)
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    SetPixel(x, y, color);
                }
        }

        #region IDisposable Members

        public void Dispose()
        {
            releaseArray();
        }

        /// <summary>
        /// Copy the image buffer back to bitmapdata, then copy it to the original bitmap (image).
        /// </summary>
        private void releaseArray()
        {
            Marshal.Copy(imageBuffer, 0, bitmapData.Scan0, bitmapData.Stride * bitmap.Height);
            bitmap.UnlockBits(bitmapData);
            bitmapData = null;
            imageBuffer = null;
        }

        #endregion
    }
}
