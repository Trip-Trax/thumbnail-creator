using System;
using System.Drawing;

namespace ThumbnailCreator.Utilities {
    public static class ImageProcessingUtility {
        public static Bitmap ScaleImage(Image image, int maxWidth, int maxHeight) {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            var bmp = new Bitmap(newImage);

            return bmp;
        }
    }
}
