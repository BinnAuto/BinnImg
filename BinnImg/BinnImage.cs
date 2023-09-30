using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Svg;

namespace BinnImg
{
    public class BinnImage : IDisposable
    {
        /// <summary>
        /// The width of the image, in pixels
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the image, in pixels
        /// </summary>
        public int Height { get; private set; }

        public Bitmap ImageData { get; private set; }


        public BinnImage(int width, int height)
        {
            CreateImage(width, height, Color.White);
        }


        public BinnImage(int width, int height, Color backgroundColor)
        {
            CreateImage(width, height, backgroundColor);
        }


        private void CreateImage(int width, int height, Color backgroundColor)
        {
            Width = width;
            Height = height;
            ImageData = new Bitmap(width, height);
            ClearImage(backgroundColor);
        }

        /// <summary>
        /// Returns the color of the pixel at the specified coordinates.
        /// </summary>
        public Color GetPixel(int x, int y)
        {
            var color = ImageData.GetPixel(x, y);
            return new(color.A, color.R, color.G, color.B);
        }


        /// <summary>
        /// Sets the specified color to the pixel at the specified coordinates.
        /// </summary>
        public void SetPixel(int x, int y, Color color)
        {
            var systemColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            ImageData.SetPixel(x, y, systemColor);
        }


        /// <summary>
        /// Sets all pixels on the image to the specified color.
        /// </summary>
        public void ClearImage(Color color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, color);
                }
            }
        }


        /// <summary>
        /// Creates an BinnImage instance from a Bitmap object.
        /// </summary>
        public static BinnImage FromBitmap(Bitmap bitmapData)
        {
            return new(bitmapData.Width, bitmapData.Height)
            {
                ImageData = bitmapData
            };
        }


        /// <summary>
        /// Creates a BinnImage instance from a Stream object.
        /// </summary>
        public static BinnImage FromStream(Stream stream)
        {
            MemoryStream ms = new();
            stream.CopyTo(ms);
            Bitmap bitmap = new(ms);
            return FromBitmap(bitmap);
        }


        /// <summary>
        /// Creates a BinnImage instance from a file on disk.
        /// </summary>
        /// <param name="filePath">The path of the image file</param>
        /// <param name="width">The width of the rasterized image when loading SVG files</param>
        public static BinnImage FromFile(string filePath, int width = 1000)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            Bitmap imageFile;
            BinnImage result;
            switch (fileExtension)
            {
                case ".bmp":
                case ".gif":
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".tiff":
                {

                    imageFile = (Bitmap)System.Drawing.Image.FromFile(filePath);
                    result = FromBitmap(imageFile);
                    return result;
                }

                case ".svg":
                    var svgDocument = SvgDocument.Open(filePath);
                    float aspectRatio = svgDocument.Height.Value / svgDocument.Width.Value;
                    int height = (int)(width * aspectRatio);
                    imageFile = svgDocument.Draw(width, height);
                    result = FromBitmap(imageFile);
                    return result;

                default:
                    throw new($"File extension \"{fileExtension}\" is not supported");
            }
        }


        /// <summary>
        /// Creates a BinnImage instance that is a subset of an original BinnImage.
        /// </summary>
        public BinnImage GetCroppedImage(int startX, int startY, int endX, int endY)
        {
            int minX = Math.Min(startX, endX);
            int maxX = Math.Max(startX, endX);
            int minY = Math.Min(startY, endY);
            int maxY = Math.Max(startY, endY);
            if (minX < 0 || maxX > Width
                || minY < 0 || maxY > Height)
            {
                throw new("Invalid crop dimensions");
            }

            BinnImage result = new(1 + maxX - minX, 1 + maxY - minY);
            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    result.SetPixel(x, y, GetPixel(minX + x, minY + y));
                }
            }
            return result;
        }


        /// <summary>
        /// Maps the A, R, G, and B channels of the image to the specified colors. Transparency is not retained in the output image
        /// </summary>
        public void MapColorChannels(Color alphaChannel, Color redChannel, Color greenChannel, Color blueChannel)
        {
            var colorCache = new Dictionary<Color, Color>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color input = GetPixel(x, y);
                    if (!colorCache.ContainsKey(input))
                    {

                        float r = input.R;
                        float g = input.G;
                        float b = input.B;
                        float a = 255 - input.A;
                        r /= 255.0f;
                        g /= 255.0f;
                        b /= 255.0f;
                        a /= 255.0f;
                        float min = 0;
                        if (input.R + input.G + input.B > 255)
                        {
                            min = Math.Min(r, Math.Min(g, b));
                        }
                        r -= min;
                        g -= min;
                        b -= min;
                        min *= 255;
                        float newBlue = (a * alphaChannel.B) + (1 - a) * ((r * redChannel.B) + (g * greenChannel.B) + (b * blueChannel.B) + min);
                        float newRed = (a * alphaChannel.R) + (1 - a) * ((r * redChannel.R) + (g * greenChannel.R) + (b * blueChannel.R) + min);
                        float newGreen = (a * alphaChannel.G) + (1 - a) * ((r * redChannel.G) + (g * greenChannel.G) + (b * blueChannel.G) + min);

                        // Awkwardly handles errors arising from white/brighter pixels
                        newRed = Math.Min(newRed, 255);
                        newGreen = Math.Min(newGreen, 255);
                        newBlue = Math.Min(newBlue, 255);

                        Color newColor = new(input.A, (byte)newRed, (byte)newGreen, (byte)newBlue);
                        if (colorCache.Count < 10)
                        {
                            colorCache[input] = newColor;
                        }
                        SetPixel(x, y, newColor);
                    }
                    else
                    {
                        SetPixel(x, y, colorCache[input]);
                    }
                }
            }
        }

        /// <summary>
        /// Maps the R, G, and B channels of the image to the specified colors. Transparency is retained in the output image
        /// </summary>
        public void MapColorChannels(Color redChannel, Color greenChannel, Color blueChannel)
        {
            var colorCache = new Dictionary<Color, Color>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color input = GetPixel(x, y);
                    if (!colorCache.ContainsKey(input))
                    {
                        if (input.A == 0)
                        {
                            SetPixel(x, y, Color.TransparentWhite);
                            continue;
                        }
                        float r = input.R;
                        float g = input.G;
                        float b = input.B;
                        r /= 255.0f;
                        g /= 255.0f;
                        b /= 255.0f;
                        float min = 0;
                        if (input.R + input.G + input.B > 255)
                        {
                            min = Math.Min(r, Math.Min(g, b));
                        }
                        r -= min;
                        g -= min;
                        b -= min;
                        min *= 255;
                        float newBlue = (r * redChannel.B) + (g * greenChannel.B) + (b * blueChannel.B) + min;
                        float newRed = (r * redChannel.R) + (g * greenChannel.R) + (b * blueChannel.R) + min;
                        float newGreen = (r * redChannel.G) + (g * greenChannel.G) + (b * blueChannel.G) + min;

                        // Awkwardly handles errors arising from white/brighter pixels
                        newRed = Math.Min(newRed, 255);
                        newGreen = Math.Min(newGreen, 255);
                        newBlue = Math.Min(newBlue, 255);

                        Color newColor = new(input.A, (byte)newRed, (byte)newGreen, (byte)newBlue);
                        if(colorCache.Count < 10)
                        {
                            colorCache[input] = newColor;
                        }
                        SetPixel(x, y, newColor);
                    }
                    else
                    {
                        SetPixel(x, y, colorCache[input]);
                    }
                }
            }
        }


        /// <summary>
        /// Applies a BinnImage on top of the base image, using the default application settings
        /// </summary>
        public void ApplyImageAsDecal(BinnImage image)
        {
            ApplyImageAsDecal(image, new());
        }


        /// <summary>
        /// Applies a BinnImage on top of the base image, using the specified transformation settings
        /// </summary>
        public void ApplyImageAsDecal(BinnImage image, DecalApplicationSettings applicationSettings)
        {
            Random random = new();
            string characterPool = "abCDef0123456789-_";
            string pngPath = "./temp001.png";
            try
            {
                while (File.Exists(pngPath))
                {
                    pngPath = "./";
                    for (int i = 0; i < 15; i++)
                    {
                        pngPath += characterPool[random.Next(characterPool.Length)];
                    }
                    pngPath += ".png";
                }
                image.SaveAsPNG(pngPath);
                ApplyDecalImageFromFile(pngPath, applicationSettings);
            }
            catch (Exception e)
            {
                if (File.Exists(pngPath))
                {
                    File.Delete(pngPath);
                }
                throw;
            }
            finally
            {
                if (File.Exists(pngPath))
                {
                    File.Delete(pngPath);
                }
            }
        }


        /// <summary>
        /// Applies an image on top of the base image, using the default application settings
        /// </summary>
        public void ApplyDecalImageFromFile(string filePath)
        {
            DecalApplicationSettings applicationSettings = new();
            ApplyDecalImageFromFile(filePath, applicationSettings);
        }


        /// <summary>
        /// Applies an image on top of the base image, using the specified transformation settings
        /// </summary>
        public void ApplyDecalImageFromFile(string filePath, DecalApplicationSettings applicationSettings)
        {
            FileStream fileStream = new(filePath, FileMode.Open);
            Bitmap imageToApply = (Bitmap)Image.FromStream(fileStream);
            try
            {
                if (applicationSettings.Size == (0, 0))
                {
                    applicationSettings.Size = (imageToApply.Width, imageToApply.Height);
                }
                imageToApply = MirrorImage(imageToApply, applicationSettings.MirrorHorizontally, applicationSettings.MirrorVertically);
                imageToApply = ScaleImage(imageToApply, applicationSettings.Size);
                imageToApply = RotateImageDegrees(imageToApply, applicationSettings.Rotation);

                for (int x = applicationSettings.Coordinates.Item1; x < applicationSettings.Coordinates.Item1 + imageToApply.Width; x++)
                {
                    for (int y = applicationSettings.Coordinates.Item2; y < applicationSettings.Coordinates.Item2 + imageToApply.Height; y++)
                    {
                        if (x < Width && y < Height)
                        {
                            Color topPixel = imageToApply.GetPixel(x - applicationSettings.Coordinates.Item1, y - applicationSettings.Coordinates.Item2);
                            Color bottomPixel = GetPixel(x, y);
                            Color newColor = OverlayColor(bottomPixel, topPixel);
                            SetPixel(x, y, newColor);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                imageToApply.Dispose();
                fileStream.Dispose();
            }
        }


        /// <summary>
        /// Saves the image as a PNG file
        /// </summary>
        public void SaveAsPNG(string filePath)
        {
            if (!filePath.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
            {
                filePath += ".png";
            }
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            ImageData.Save(filePath);
        }


        /// <summary>
        /// Converts the image to a byte array
        /// </summary>
        public byte[] ToPNGByteArray()
        {
            using MemoryStream stream = new();
            ImageData.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }


        /// <summary>
        /// Copies the BinnImage into a separate instance
        /// </summary>
        public BinnImage Copy(BinnImage source)
        {
            BinnImage result = new(source.Width, source.Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result.SetPixel(x, y, source.GetPixel(x, y));
                }
            }
            return result;
        }


        public void ScaleImage(int? width, int? height)
        {
            if (width != null || height != null)
            {
                if (height == null)
                {
                    height = (int)Math.Round((float)(Height * width.Value) / Width);
                }
                if (width == null)
                {
                    width = (int)Math.Round((float)(Width * height.Value) / Height);
                }
                Width = width.Value;
                Height = height.Value;
                ImageData = ScaleImage(ImageData, (width.Value, height.Value));
            }
        }


        private Bitmap ScaleImage(Bitmap image, (int, int) newDimensions)
        {
            if (newDimensions.Item1 == image.Width && newDimensions.Item2 == image.Height)
            {
                return image;
            }

            int newWidth = newDimensions.Item1;
            int newHeight = newDimensions.Item2;
            Bitmap resultImage = new(newWidth, newHeight);
            resultImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(resultImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    Rectangle newRectangle = new(0, 0, newWidth, newHeight);
                    graphics.DrawImage(image, newRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return resultImage;
        }


        private Bitmap RotateImageDegrees(Bitmap image, float rotationAngle)
        {
            if (rotationAngle == 0)
            {
                return image;
            }

            while (rotationAngle < 0)
            {
                rotationAngle += 360;
            }
            rotationAngle %= 360;

            float angleRadians = rotationAngle * (float)(Math.PI / 180);
            float sinAngle = (float)Math.Sin(angleRadians);
            float cosAngle = (float)Math.Cos(angleRadians);

            float initialWidth = image.Width;
            float initialHeight = image.Height;
            float newWidthTemp = Math.Abs(initialHeight * sinAngle) + Math.Abs(initialWidth * cosAngle);
            float newHeightTemp = Math.Abs(initialWidth * sinAngle) + Math.Abs(initialHeight * cosAngle);
            int newWidth = Convert.ToInt32(newWidthTemp);
            int newHeight = Convert.ToInt32(newHeightTemp);

            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.TranslateTransform(result.Width / 2, result.Height / 2);
                g.RotateTransform(rotationAngle);
                g.TranslateTransform(-result.Width / 2, -result.Height / 2);
                g.DrawImage(image, new System.Drawing.Point((newWidth - image.Width) / 2, (newHeight - image.Height) / 2));
            }
            return result;
        }


        private Bitmap MirrorImage(Bitmap image, bool mirrorHorizontal, bool mirrorVertical)
        {
            if (!mirrorHorizontal && !mirrorVertical)
            {
                return image;
            }

            Bitmap newImage = new(image);
            if (mirrorHorizontal)
            {
                newImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if (mirrorVertical)
            {
                newImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return newImage;
        }


        public static Color OverlayColor(Color bottom, Color top)
        {
            if (top.A == 255)
            {
                return top;
            }
            if (top.A == 0)
            {
                return bottom;
            }
            if (bottom.A == 0)
            {
                return top;
            }

            float bottomA01 = bottom.A / 255.0f;
            float bottomR01 = bottom.R / 255.0f;
            float bottomG01 = bottom.G / 255.0f;
            float bottomB01 = bottom.B / 255.0f;
            float topA01 = top.A / 255.0f;
            float topR01 = top.R / 255.0f;
            float topG01 = top.G / 255.0f;
            float topB01 = top.B / 255.0f;

            float alphaConjugate = 1 - topA01;
            float resultAlpha = 1 - alphaConjugate * (1 - bottomA01);
            float resultRed = (topR01 * topA01) + (bottomR01 * bottomA01 * alphaConjugate);
            float resultGreen = (topG01 * topA01) + (bottomG01 * bottomA01 * alphaConjugate);
            float resultBlue = (topB01 * topA01) + (bottomB01 * bottomA01 * alphaConjugate);

            byte resultAlphaByte = (byte)(255 * resultAlpha);
            byte resultRedByte = (byte)(255 * resultRed);
            byte resultGreenByte = (byte)(255 * resultGreen);
            byte resultBlueByte = (byte)(255 * resultBlue);
            return new(resultAlphaByte, resultRedByte, resultGreenByte, resultBlueByte);
        }


        public bool Equals(BinnImage obj)
        {
            if (Width != obj.Width)
            {
                return false;
            }
            if (Height != obj.Height)
            {
                return false;
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color a = GetPixel(x, y);
                    Color b = obj.GetPixel(x, y);
                    if (a != b)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public void Dispose()
        {
            ImageData.Dispose();
        }


        public static bool operator ==(BinnImage a, BinnImage b)
        {
            return a.Equals(b);
        }


        public static bool operator !=(BinnImage a, BinnImage b)
        {
            return !a.Equals(b);
        }
    }
}
