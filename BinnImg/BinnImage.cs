using Svg;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BinnImg
{
    public class BinnImage : IDisposable
    {
        /// <summary>
        /// The width of the image, in pixels
        /// </summary>
        public int Width
        {
            get
            {
                return ImageData.Width;
            }
        }

        /// <summary>
        /// The height of the image, in pixels
        /// </summary>
        public int Height
        {
            get
            {
                return ImageData.Height;
            }
        }


        public Bitmap ImageData { get; private set; } = null!;


        public BinnImage(int width, int height)
        {
            CreateImage(width, height, Color.White);
        }


        public BinnImage(int width, int height, Color backgroundColor)
        {
            CreateImage(width, height, backgroundColor);
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
        /// Attempts to return the color of the pixel at the specified coordinates.
        /// If the coordinates are outside of the image's dimensions, null is returned.
        /// </summary>
        public Color? TryGetPixel(int x, int y)
        {
            if (x < 0 || x >= Width
                || y < 0 || y >= Height)
            {
                return null;
            }
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
        /// Attempts to set the specified color to the pixel at the specified coordinates.
        /// If the coordinates are outside of the image's dimensions, the edit is not performed
        /// and no exception is thrown.
        /// </summary>
        public void TrySetPixel(int x, int y, Color color)
        {
            if(x < 0 || x >= Width
                || y < 0 || y >= Height)
            {
                return;
            }
            SetPixel(x, y, color);
        }


        /// <summary>
        /// Sets all pixels on the image to the specified color.
        /// </summary>
        public void ClearImage(Color color)
        {
            Rectangle rect = new(0, 0, ImageData.Width, ImageData.Height);
            var bitmapData = ImageData.LockBits(rect, ImageLockMode.ReadWrite, ImageData.PixelFormat);
            IntPtr sourcePointer = bitmapData.Scan0;
            unsafe
            {
                for(int y = 0; y < Height; y++)
                {
                    byte* source = (byte*)sourcePointer.ToPointer() + bitmapData.Stride * y;
                    for(int x = Width; x > 0; x--)
                    {
                        source[0] = color.B;
                        source[1] = color.G;
                        source[2] = color.R;
                        source[3] = color.A;
                        source += 4;
                    }
                }
                ImageData.UnlockBits(bitmapData);
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
                    result[x, y] = this[minX + x, minY + y];
                }
            }
            return result;
        }


        /// <summary>
        /// Maps the A, R, G, and B channels of the image to the specified colors. Transparency is not retained in the output image
        /// </summary>
        public void MapColorChannels(Color alphaChannel, Color redChannel, Color greenChannel, Color blueChannel)
        {
            Rectangle rect = new(0, 0, ImageData.Width, ImageData.Height);
            var bitmapData = ImageData.LockBits(rect, ImageLockMode.ReadWrite, ImageData.PixelFormat);
            IntPtr sourcePointer = bitmapData.Scan0;
            unsafe
            {
                for(int y = 0; y < Height; y++)
                {
                    byte* source = (byte*)sourcePointer.ToPointer() + bitmapData.Stride * y;
                    for(int x = Width; x > 0; x--)
                    {
                        float b = source[0];
                        float g = source[1];
                        float r = source[2];
                        float a = 255 - source[3];
                        float min = 0;
                        if (r + g + b > 255)
                        {
                            min = Math.Min(r, Math.Min(g, b)) / 255.0f;
                        }
                        r /= 255.0f;
                        g /= 255.0f;
                        b /= 255.0f;
                        a /= 255.0f;
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
                        source[0] = (byte)newBlue;
                        source[1] = (byte)newGreen;
                        source[2] = (byte)newRed;
                        source[3] = 255;
                        source += 4;
                    }
                }
                ImageData.UnlockBits(bitmapData);
            }
        }


        private Color MapColor(Color inputColor, Color alphaChannel, Color redChannel, Color greenChannel, Color blueChannel)
        {
            float r = inputColor.R;
            float g = inputColor.G;
            float b = inputColor.B;
            float a = 255 - inputColor.A;
            r /= 255.0f;
            g /= 255.0f;
            b /= 255.0f;
            a /= 255.0f;
            float min = 0;
            if (inputColor.R + inputColor.G + inputColor.B > 255)
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

            return new(255, (byte)newRed, (byte)newGreen, (byte)newBlue);
        }


        /// <summary>
        /// Maps the R, G, and B channels of the image to the specified colors. Transparency is retained in the output image
        /// </summary>
        public void MapColorChannels(Color redChannel, Color greenChannel, Color blueChannel)
        {
            Rectangle rect = new(0, 0, ImageData.Width, ImageData.Height);
            var bitmapData = ImageData.LockBits(rect, ImageLockMode.ReadWrite, ImageData.PixelFormat);
            IntPtr sourcePointer = bitmapData.Scan0;
            unsafe
            {
                for(int y = 0; y < Height; y++)
                {
                    byte* source = (byte*)sourcePointer.ToPointer() + bitmapData.Stride * y;
                    for(int x = Width; x > 0; x--)
                    {
                        float b = source[0];
                        float g = source[1];
                        float r = source[2];
                        float min = 0;
                        if(r + g + b > 255)
                        {
                            min = Math.Min(r, Math.Min(g, b)) / 255.0f;
                        }
                        r /= 255.0f;
                        g /= 255.0f;
                        b /= 255.0f;
                        r -= min;
                        g -= min;
                        b -= min;
                        min *= 255;
                        float newRed = (r * redChannel.R) + (g * greenChannel.R) + (b * blueChannel.R) + min;
                        float newGreen = (r * redChannel.G) + (g * greenChannel.G) + (b * blueChannel.G) + min;
                        float newBlue = (r * redChannel.B) + (g * greenChannel.B) + (b * blueChannel.B) + min;
                        // Awkwardly handles errors arising from white/brighter pixels
                        newRed = Math.Min(newRed, 255);
                        newGreen = Math.Min(newGreen, 255);
                        newBlue = Math.Min(newBlue, 255);
                        source[0] = (byte)newBlue;
                        source[1] = (byte)newGreen;
                        source[2] = (byte)newRed;
                        source += 4;
                    }
                }
                ImageData.UnlockBits(bitmapData);
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
                image.SaveToFile(pngPath);
                ApplyDecalImageFromFile(pngPath, applicationSettings);
            }
            catch (Exception)
            {
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
            ApplyDecalImageFromFile(filePath, new DecalApplicationSettings());
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
                            Color bottomPixel = this[x, y]!;
                            Color newColor = Color.OverlayColors(bottomPixel, topPixel);
                            this[x, y] = newColor;
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
        /// Saves the image to disk with the given file path and image format
        /// based off the file extension.
        /// </summary>
        public void SaveToFile(string filePath)
        {
            ImageFormat imageFormat;
            if(filePath.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
            {
                imageFormat = ImageFormat.Png;
            }
            else if(filePath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || filePath.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase))
            {
                imageFormat = ImageFormat.Jpeg;
            }
            else if (filePath.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase))
            {
                imageFormat = ImageFormat.Bmp;
            }
            else if (filePath.EndsWith(".ico", StringComparison.InvariantCultureIgnoreCase))
            {
                imageFormat = ImageFormat.Icon;
            }
            else if (filePath.EndsWith(".gif", StringComparison.InvariantCultureIgnoreCase))
            {
                imageFormat = ImageFormat.Gif;
            }
            else
            {
                throw new("Image format unknown");
            }
            SaveToFile(filePath, imageFormat);
        }


        /// <summary>
        /// Saves the image to disk with the given file path and image format
        /// </summary>
        public void SaveToFile(string filePath, ImageFormat format)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            ImageData.Save(filePath, format);
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
        public BinnImage Copy()
        {
            BinnImage result = new(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result[x, y] = this[x, y];
                }
            }
            return result;
        }

        #region Transformation Methods

        /// <summary>
        /// Scale an image by a given ratio
        /// </summary>
        /// <param name="multiplier"></param>
        public void ScaleImage(decimal multiplier)
        {
            int newWidth = (int)(Width * multiplier);
            int newHeight = (int)(Height * multiplier);
            ScaleImage(newWidth, newHeight);
        }


        /// <summary>
        /// Scale the image to the specified dimensions
        /// </summary>
        /// <param name="width">The width of the new image</param>
        /// <param name="height">The height of the new image</param>
        public void ScaleImage(int? width, int? height)
        {
            if (width.HasValue || height.HasValue)
            {
                if (!height.HasValue)
                {
                    height = (int)Math.Round((float)(Height * width!.Value) / Width);
                }
                if (!width.HasValue)
                {
                    width = (int)Math.Round((float)(Width * height!.Value) / Height);
                }
                ImageData = ScaleImage(ImageData, (width.Value, height.Value));
            }
        }


        /// <summary>
        /// Mirrors the image along the vertical axis
        /// </summary>
        public void MirrorHorizontal()
        {
            Bitmap newImage = new(ImageData);
            newImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            ImageData = newImage;
        }


        /// <summary>
        /// Rotates the image 90 degrees clockwise
        /// </summary>
        public void RotateClockwise()
        {
            RotateClockwise(1);
        }


        /// <summary>
        /// Rotates the image 90 degrees counterclockwise
        /// </summary>
        public void RotateCounterclockwise()
        {
            RotateCounterclockwise(1);
        }


        /// <summary>
        /// Rotates the image 90 degrees clockwise
        /// </summary>
        public void RotateClockwise(int iterations)
        {
            Bitmap newImage = new(ImageData);
            iterations %= 4;
            for(int i = 0; i < iterations; i++)
            {
                newImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            ImageData = newImage;
        }


        /// <summary>
        /// Rotates the image 90 degrees counterclockwise
        /// </summary>
        public void RotateCounterclockwise(int iterations)
        {
            Bitmap newImage = new(ImageData);
            iterations %= 4;
            for (int i = 0; i < iterations; i++)
            {
                newImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            ImageData = newImage;
        }


        /// <summary>
        /// Mirrors the image along the horizontal axis
        /// </summary>
        public void MirrorVertical()
        {
            Bitmap newImage = new(ImageData);
            newImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            ImageData = newImage;
        }


        /// <summary>
        /// Grayscales the image
        /// </summary>
        public void Grayscale()
        {
            MapColorChannels(new(76, 76, 76), new(150, 150, 150), new(29, 29, 29));
        }

        #endregion

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
                    Color a = this[x, y]!;
                    Color b = obj[x, y]!;
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
            GC.SuppressFinalize(this);
        }


        public Color? this[int x, int y]
        {
            get => TryGetPixel(x, y);
            set => TrySetPixel(x, y, value!);
        }


        public Color this[Range rangeX, int y]
        {
            set
            {
                for (int x = rangeX.Start.Value; x <= rangeX.End.Value; x++)
                {
                    this[x, y] = value;
                }
            }
        }


        public Color this[int x, Range rangeY]
        {
            set
            {
                for (int y = rangeY.Start.Value; y <= rangeY.End.Value; y++)
                {
                    this[x, y] = value;
                }
            }
        }


        public Color this[Range rangeX, Range rangeY]
        {
            set
            {
                for (int x = rangeX.Start.Value; x <= rangeX.End.Value; x++)
                {
                    for (int y = rangeY.Start.Value; y <= rangeY.End.Value; y++)
                    {
                        this[x, y] = value;
                    }
                }
            }
        }


        public static bool operator ==(BinnImage a, BinnImage b)
        {
            return a.Equals(b);
        }


        public static bool operator !=(BinnImage a, BinnImage b)
        {
            return !a.Equals(b);
        }


        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }


        public override int GetHashCode()
        {
            return ImageData.GetHashCode();
        }


        private void CreateImage(int width, int height, Color backgroundColor)
        {
            ImageData = new Bitmap(width, height);
            ClearImage(backgroundColor);
        }


        private static Bitmap ScaleImage(Bitmap image, (int, int) newDimensions)
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


        private static Bitmap RotateImageDegrees(Bitmap image, float rotationAngle)
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


        private static Bitmap MirrorImage(Bitmap image, bool mirrorHorizontal, bool mirrorVertical)
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
    }
}
