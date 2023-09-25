namespace BinnImg
{
    public class Color
    {
        #region Predefined Colors

        public static Color AliceBlue = new(240, 248, 255);

        public static Color AntiqueWhite = new(250, 235, 215);

        public static Color Aqua = new(0, 255, 255);

        public static Color Aquamarine = new(127, 255, 212);

        public static Color Azure = new(240, 255, 255);

        public static Color Beige = new(245, 245, 220);

        public static Color Bisque = new(255, 228, 196);

        public static Color Black = new(0, 0, 0);

        public static Color BlanchedAlmond = new(255, 235, 205);

        public static Color Blue = new(0, 0, 255);

        public static Color BlueViolet = new(138, 43, 226);

        public static Color Brown = new(165, 42, 42);

        public static Color BurlyWood = new(222, 184, 135);

        public static Color CadetBlue = new(95, 158, 160);

        public static Color Chartreuse = new(127, 255, 0);

        public static Color Chocolate = new(210, 105, 30);

        public static Color Coral = new(255, 127, 80);

        public static Color CornflowerBlue = new(100, 149, 237);

        public static Color Cornsilk = new(255, 248, 220);

        public static Color Crimson = new(220, 20, 60);

        public static Color Cyan = new(0, 255, 255);

        public static Color DarkBlue = new(0, 0, 139);

        public static Color DarkCyan = new(0, 139, 139);

        public static Color DarkGoldenrod = new(184, 134, 11);

        public static Color DarkGray = new(169, 169, 169);

        public static Color DarkGreen = new(0, 100, 0);

        public static Color DarkGrey = new(169, 169, 169);

        public static Color DarkKhaki = new(189, 183, 107);

        public static Color DarkMagenta = new(139, 0, 139);

        public static Color DarkOliveGreen = new(85, 107, 47);

        public static Color DarkOrange = new(255, 140, 0);

        public static Color DarkOrchid = new(153, 50, 204);

        public static Color DarkRed = new(139, 0, 0);

        public static Color DarkSalmon = new(233, 150, 122);

        public static Color DarkSeaGreen = new(143, 188, 143);

        public static Color DarkSlateBlue = new(72, 61, 139);

        public static Color DarkSlateGray = new(47, 79, 79);

        public static Color DarkSlateGrey = new(47, 79, 79);

        public static Color DarkTurquoise = new(0, 206, 209);

        public static Color DarkViolet = new(148, 0, 211);

        public static Color DeepPink = new(255, 20, 147);

        public static Color DeepSkyBlue = new(0, 191, 255);

        public static Color DimGray = new(105, 105, 105);

        public static Color DimGrey = new(105, 105, 105);

        public static Color DodgerBlue = new(30, 144, 255);

        public static Color FireBrick = new(178, 34, 34);

        public static Color FloralWhite = new(255, 250, 240);

        public static Color ForestGreen = new(34, 139, 34);

        public static Color Fuchsia = new(255, 0, 255);

        public static Color Gainsboro = new(220, 220, 220);

        public static Color GhostWhite = new(248, 248, 255);

        public static Color Gold = new(255, 215, 0);

        public static Color GoldenRod = new(218, 165, 32);

        public static Color Gray = new(128, 128, 128);

        public static Color Grey = new(128, 128, 128);

        public static Color Green = new(0, 128, 0);

        public static Color GreenYellow = new(173, 255, 47);

        public static Color Honeydew = new(240, 255, 240);

        public static Color HotPink = new(255, 105, 180);

        public static Color IndianRed = new(205, 92, 92);

        public static Color Indigo = new(75, 0, 130);

        public static Color Ivory = new(255, 255, 240);

        public static Color Khaki = new(240, 230, 140);

        public static Color Lavender = new(230, 230, 250);

        public static Color LavenderBlush = new(255, 240, 245);

        public static Color LawnGreen = new(124, 252, 0);

        public static Color LemonChiffon = new(255, 250, 205);

        public static Color LightBlue = new(173, 216, 230);

        public static Color LightCoral = new(240, 128, 128);

        public static Color LightCyan = new(224, 255, 255);

        public static Color LightGoldenrodYellow = new(250, 250, 210);

        public static Color LightGray = new(211, 211, 211);

        public static Color LightGreen = new(144, 238, 144);

        public static Color LightGrey = new(211, 211, 211);

        public static Color LightPink = new(255, 182, 193);

        public static Color LightSalmon = new(255, 160, 122);

        public static Color LightSeaGreen = new(32, 178, 170);

        public static Color LightSkyBlue = new(135, 206, 250);

        public static Color LightSlateGray = new(119, 136, 153);

        public static Color LightSlateGrey = new(119, 136, 153);

        public static Color LightSteelBlue = new(176, 196, 222);

        public static Color LightYellow = new(255, 255, 224);

        public static Color Lime = new(0, 255, 0);

        public static Color LimeGreen = new(50, 205, 50);

        public static Color Linen = new(250, 240, 230);

        public static Color Magenta = new(255, 0, 255);

        public static Color Maroon = new(128, 0, 0);

        public static Color MediumAquamarine = new(102, 205, 170);

        public static Color MediumBlue = new(0, 0, 205);

        public static Color MediumOrchid = new(186, 85, 211);

        public static Color MediumPurple = new(147, 112, 219);

        public static Color MediumSeaGreen = new(60, 179, 113);

        public static Color MediumSlateBlue = new(123, 104, 238);

        public static Color MediumSpringGreen = new(0, 250, 154);

        public static Color MediumTurquoise = new(72, 209, 204);

        public static Color MediumVioletRed = new(199, 21, 133);

        public static Color MidnightBlue = new(25, 25, 112);

        public static Color MintCream = new(245, 255, 250);

        public static Color MistyRose = new(255, 228, 225);

        public static Color Moccasin = new(255, 228, 181);

        public static Color NavajoWhite = new(255, 222, 173);

        public static Color Navy = new(0, 0, 128);

        public static Color OldLace = new(253, 245, 230);

        public static Color Olive = new(128, 128, 0);

        public static Color OliveDrab = new(107, 142, 35);

        public static Color Orange = new(255, 165, 0);

        public static Color OrangeRed = new(255, 69, 0);

        public static Color Orchid = new(218, 112, 214);

        public static Color PaleGoldenrod = new(238, 232, 170);

        public static Color PaleGreen = new(152, 251, 152);

        public static Color PaleTurquoise = new(175, 238, 238);

        public static Color PaleVioletRed = new(219, 112, 147);

        public static Color PapayaWhip = new(255, 239, 213);

        public static Color PeachPuff = new(255, 218, 185);

        public static Color Pery = new(205, 133, 63);

        public static Color Pink = new(255, 192, 203);

        public static Color Plum = new(221, 160, 221);

        public static Color PowderBlue = new(176, 224, 230);

        public static Color Purple = new(128, 0, 128);

        public static Color Red = new(255, 0, 0);

        public static Color RosyBrown = new(188, 143, 143);

        public static Color RoyalBlue = new(65, 105, 225);

        public static Color SaddleBrown = new(138, 69, 19);

        public static Color Salmon = new(250, 128, 114);

        public static Color SandyBrown = new(244, 164, 96);

        public static Color SeaGreen = new(46, 139, 87);

        public static Color Seashell = new(255, 245, 238);

        public static Color Sienna = new(160, 82, 45);

        public static Color Silver = new(192, 192, 192);

        public static Color SkyBlue = new(135, 206, 235);

        public static Color SlateBlue = new(106, 90, 205);

        public static Color SlateGray = new(112, 128, 144);

        public static Color SlateGrey = new(112, 128, 144);

        public static Color Snow = new(255, 250, 250);

        public static Color SpringGreen = new(0, 255, 127);

        public static Color SteelBlue = new(70, 130, 180);

        public static Color Tan = new(210, 180, 140);

        public static Color Teal = new(0, 128, 128);

        public static Color Thistle = new(216, 191, 216);

        public static Color Tomato = new(255, 99, 71);

        public static Color Turquoise = new(64, 224, 208);

        public static Color Violet = new(238, 130, 238);

        public static Color Wheat = new(245, 222, 179);

        public static Color White = new(255, 255, 255);

        public static Color Whitesmoke = new(245, 245, 245);

        public static Color Yellow = new(255, 255, 0);

        public static Color YellowGreen = new(154, 205, 50);

        public static Color TranslucentAliceBlue = new(127, 240, 248, 255);

        public static Color TranslucentAntiqueWhite = new(127, 250, 235, 215);

        public static Color TranslucentAqua = new(127, 0, 255, 255);

        public static Color TranslucentAquamarine = new(127, 127, 255, 212);

        public static Color TranslucentAzure = new(127, 240, 255, 255);

        public static Color TranslucentBeige = new(127, 245, 245, 220);

        public static Color TranslucentBisque = new(127, 255, 228, 196);

        public static Color TranslucentBlack = new(127, 0, 0, 0);

        public static Color TranslucentBlanchedAlmond = new(127, 255, 235, 205);

        public static Color TranslucentBlue = new(127, 0, 0, 255);

        public static Color TranslucentBlueViolet = new(127, 138, 43, 226);

        public static Color TranslucentBrown = new(127, 165, 42, 42);

        public static Color TranslucentBurlyWood = new(127, 222, 184, 135);

        public static Color TranslucentCadetBlue = new(127, 95, 158, 160);

        public static Color TranslucentChartreuse = new(127, 127, 255, 0);

        public static Color TranslucentChocolate = new(127, 210, 105, 30);

        public static Color TranslucentCoral = new(127, 255, 127, 80);

        public static Color TranslucentCornflowerBlue = new(127, 100, 149, 237);

        public static Color TranslucentCornsilk = new(127, 255, 248, 220);

        public static Color TranslucentCrimson = new(127, 220, 20, 60);

        public static Color TranslucentCyan = new(127, 0, 255, 255);

        public static Color TranslucentDarkBlue = new(127, 0, 0, 139);

        public static Color TranslucentDarkCyan = new(127, 0, 139, 139);

        public static Color TranslucentDarkGoldenrod = new(127, 184, 134, 11);

        public static Color TranslucentDarkGray = new(127, 169, 169, 169);

        public static Color TranslucentDarkGreen = new(127, 0, 100, 0);

        public static Color TranslucentDarkGrey = new(127, 169, 169, 169);

        public static Color TranslucentDarkKhaki = new(127, 189, 183, 107);

        public static Color TranslucentDarkMagenta = new(127, 139, 0, 139);

        public static Color TranslucentDarkOliveGreen = new(127, 85, 107, 47);

        public static Color TranslucentDarkOrange = new(127, 255, 140, 0);

        public static Color TranslucentDarkOrchid = new(127, 153, 50, 204);

        public static Color TranslucentDarkRed = new(127, 139, 0, 0);

        public static Color TranslucentDarkSalmon = new(127, 233, 150, 122);

        public static Color TranslucentDarkSeaGreen = new(127, 143, 188, 143);

        public static Color TranslucentDarkSlateBlue = new(127, 72, 61, 139);

        public static Color TranslucentDarkSlateGray = new(127, 47, 79, 79);

        public static Color TranslucentDarkSlateGrey = new(127, 47, 79, 79);

        public static Color TranslucentDarkTurquoise = new(127, 0, 206, 209);

        public static Color TranslucentDarkViolet = new(127, 148, 0, 211);

        public static Color TranslucentDeepPink = new(127, 255, 20, 147);

        public static Color TranslucentDeepSkyBlue = new(127, 0, 191, 255);

        public static Color TranslucentDimGray = new(127, 105, 105, 105);

        public static Color TranslucentDimGrey = new(127, 105, 105, 105);

        public static Color TranslucentDodgerBlue = new(127, 30, 144, 255);

        public static Color TranslucentFireBrick = new(127, 178, 34, 34);

        public static Color TranslucentFloralWhite = new(127, 255, 250, 240);

        public static Color TranslucentForestGreen = new(127, 34, 139, 34);

        public static Color TranslucentFuchsia = new(127, 255, 0, 255);

        public static Color TranslucentGainsboro = new(127, 220, 220, 220);

        public static Color TranslucentGhostWhite = new(127, 248, 248, 255);

        public static Color TranslucentGold = new(127, 255, 215, 0);

        public static Color TranslucentGoldenRod = new(127, 218, 165, 32);

        public static Color TranslucentGray = new(127, 128, 128, 128);

        public static Color TranslucentGrey = new(127, 128, 128, 128);

        public static Color TranslucentGreen = new(127, 0, 128, 0);

        public static Color TranslucentGreenYellow = new(127, 173, 255, 47);

        public static Color TranslucentHoneydew = new(127, 240, 255, 240);

        public static Color TranslucentHotPink = new(127, 255, 105, 180);

        public static Color TranslucentIndianRed = new(127, 205, 92, 92);

        public static Color TranslucentIndigo = new(127, 75, 0, 130);

        public static Color TranslucentIvory = new(127, 255, 255, 240);

        public static Color TranslucentKhaki = new(127, 240, 230, 140);

        public static Color TranslucentLavender = new(127, 230, 230, 250);

        public static Color TranslucentLavenderBlush = new(127, 255, 240, 245);

        public static Color TranslucentLawnGreen = new(127, 124, 252, 0);

        public static Color TranslucentLemonChiffon = new(127, 255, 250, 205);

        public static Color TranslucentLightBlue = new(127, 173, 216, 230);

        public static Color TranslucentLightCoral = new(127, 240, 128, 128);

        public static Color TranslucentLightCyan = new(127, 224, 255, 255);

        public static Color TranslucentLightGoldenrodYellow = new(127, 250, 250, 210);

        public static Color TranslucentLightGray = new(127, 211, 211, 211);

        public static Color TranslucentLightGreen = new(127, 144, 238, 144);

        public static Color TranslucentLightGrey = new(127, 211, 211, 211);

        public static Color TranslucentLightPink = new(127, 255, 182, 193);

        public static Color TranslucentLightSalmon = new(127, 255, 160, 122);

        public static Color TranslucentLightSeaGreen = new(127, 32, 178, 170);

        public static Color TranslucentLightSkyBlue = new(127, 135, 206, 250);

        public static Color TranslucentLightSlateGray = new(127, 119, 136, 153);

        public static Color TranslucentLightSlateGrey = new(127, 119, 136, 153);

        public static Color TranslucentLightSteelBlue = new(127, 176, 196, 222);

        public static Color TranslucentLightYellow = new(127, 255, 255, 224);

        public static Color TranslucentLime = new(127, 0, 255, 0);

        public static Color TranslucentLimeGreen = new(127, 50, 205, 50);

        public static Color TranslucentLinen = new(127, 250, 240, 230);

        public static Color TranslucentMagenta = new(127, 255, 0, 255);

        public static Color TranslucentMaroon = new(127, 128, 0, 0);

        public static Color TranslucentMediumAquamarine = new(127, 102, 205, 170);

        public static Color TranslucentMediumBlue = new(127, 0, 0, 205);

        public static Color TranslucentMediumOrchid = new(127, 186, 85, 211);

        public static Color TranslucentMediumPurple = new(127, 147, 112, 219);

        public static Color TranslucentMediumSeaGreen = new(127, 60, 179, 113);

        public static Color TranslucentMediumSlateBlue = new(127, 123, 104, 238);

        public static Color TranslucentMediumSpringGreen = new(127, 0, 250, 154);

        public static Color TranslucentMediumTurquoise = new(127, 72, 209, 204);

        public static Color TranslucentMediumVioletRed = new(127, 199, 21, 133);

        public static Color TranslucentMidnightBlue = new(127, 25, 25, 112);

        public static Color TranslucentMintCream = new(127, 245, 255, 250);

        public static Color TranslucentMistyRose = new(127, 255, 228, 225);

        public static Color TranslucentMoccasin = new(127, 255, 228, 181);

        public static Color TranslucentNavajoWhite = new(127, 255, 222, 173);

        public static Color TranslucentNavy = new(127, 0, 0, 128);

        public static Color TranslucentOldLace = new(127, 253, 245, 230);

        public static Color TranslucentOlive = new(127, 128, 128, 0);

        public static Color TranslucentOliveDrab = new(127, 107, 142, 35);

        public static Color TranslucentOrange = new(127, 255, 165, 0);

        public static Color TranslucentOrangeRed = new(127, 255, 69, 0);

        public static Color TranslucentOrchid = new(127, 218, 112, 214);

        public static Color TranslucentPaleGoldenrod = new(127, 238, 232, 170);

        public static Color TranslucentPaleGreen = new(127, 152, 251, 152);

        public static Color TranslucentPaleTurquoise = new(127, 175, 238, 238);

        public static Color TranslucentPaleVioletRed = new(127, 219, 112, 147);

        public static Color TranslucentPapayaWhip = new(127, 255, 239, 213);

        public static Color TranslucentPeachPuff = new(127, 255, 218, 185);

        public static Color TranslucentPery = new(127, 205, 133, 63);

        public static Color TranslucentPink = new(127, 255, 192, 203);

        public static Color TranslucentPlum = new(127, 221, 160, 221);

        public static Color TranslucentPowderBlue = new(127, 176, 224, 230);

        public static Color TranslucentPurple = new(127, 128, 0, 128);

        public static Color TranslucentRed = new(127, 255, 0, 0);

        public static Color TranslucentRosyBrown = new(127, 188, 143, 143);

        public static Color TranslucentRoyalBlue = new(127, 65, 105, 225);

        public static Color TranslucentSaddleBrown = new(127, 139, 69, 19);

        public static Color TranslucentSalmon = new(127, 250, 128, 114);

        public static Color TranslucentSandyBrown = new(127, 244, 164, 96);

        public static Color TranslucentSeaGreen = new(127, 46, 139, 87);

        public static Color TranslucentSeashell = new(127, 255, 245, 238);

        public static Color TranslucentSienna = new(127, 160, 82, 45);

        public static Color TranslucentSilver = new(127, 192, 192, 192);

        public static Color TranslucentSkyBlue = new(127, 135, 206, 235);

        public static Color TranslucentSlateBlue = new(127, 106, 90, 205);

        public static Color TranslucentSlateGray = new(127, 112, 128, 144);

        public static Color TranslucentSlateGrey = new(127, 112, 128, 144);

        public static Color TranslucentSnow = new(127, 255, 250, 250);

        public static Color TranslucentSpringGreen = new(127, 0, 255, 127);

        public static Color TranslucentSteelBlue = new(127, 70, 130, 180);

        public static Color TranslucentTan = new(127, 210, 180, 140);

        public static Color TranslucentTeal = new(127, 0, 128, 128);

        public static Color TranslucentThistle = new(127, 216, 191, 216);

        public static Color TranslucentTomato = new(127, 255, 99, 71);

        public static Color TranslucentTurquoise = new(127, 64, 224, 208);

        public static Color TranslucentViolet = new(127, 238, 130, 238);

        public static Color TranslucentWheat = new(127, 245, 222, 179);

        public static Color TranslucentWhite = new(127, 255, 255, 255);

        public static Color TranslucentWhitesmoke = new(127, 245, 245, 245);

        public static Color TranslucentYellow = new(127, 255, 255, 0);

        public static Color TranslucentYellowGreen = new(127, 154, 205, 50);

        public static Color TransparentWhite = new(0, 255, 255, 255);

        public static Color TransparentBlack = new(0, 0, 0, 0);

        #endregion

        /// <summary>
        /// The Alpha value of the color
        /// </summary>
        public byte A { get; set; } = 255;

        /// <summary>
        /// The Blue value of the color
        /// </summary>
        public byte B { get; set; } = 255;

        /// <summary>
        /// The Green value of the color
        /// </summary>
        public byte G { get; set; } = 255;

        /// <summary>
        /// The Red value of the color
        /// </summary>
        public byte R { get; set; } = 255;

        #region Constructors and helper methods

        /// <summary>
        /// Parameterless constructor, creating white.
        /// </summary>
        public Color() { }


        /// <summary>
        /// Constructor setting an opaque RGB color using a 0-255 scale
        /// </summary>
        public Color(byte red, byte green, byte blue)
        {
            SetColorValues(255, red, green, blue);
        }


        /// <summary>
        /// Constructor setting an ARGB color using a 0-255 scale
        /// </summary>
        public Color(byte alpha, byte red, byte green, byte blue)
        {
            SetColorValues(alpha, red, green, blue);
        }


        /// <summary>
        /// Constructor setting an opaque RGB color using a 0-1 scale
        /// </summary>
        public Color(float red, float green, float blue)
        {
            SetColorValues(1f, red, green, blue);
        }


        /// <summary>
        /// Constructor setting an ARGB color using a 0-1 scale
        /// </summary>
        public Color(float alpha, float red, float green, float blue)
        {
            SetColorValues(alpha, red, green, blue);
        }


        private void SetColorValues(byte alpha, byte red, byte green, byte blue)
        {
            A = alpha;
            R = red;
            G = green;
            B = blue;
        }


        private void SetColorValues(float alpha, float red, float green, float blue)
        {
            alpha = Math.Abs(alpha);
            red = Math.Abs(red);
            green = Math.Abs(green);
            blue = Math.Abs(blue);
            A = (byte)Math.Min((int)(255 * alpha), 255);
            R = (byte)Math.Min((int)(255 * red), 255);
            G = (byte)Math.Min((int)(255 * green), 255);
            B = (byte)Math.Min((int)(255 * blue), 255);
        }

        #endregion

        #region From Hex methods

        /// <summary>
        /// Returns a Color object from a string representation of its hex value
        /// </summary>
        public static Color FromHex(string hexValue)
        {
            hexValue = StandardizeHexValue(hexValue);
            byte alpha = 255;
            byte red = GetByteFromHexString(hexValue.Substring(0, 2));
            byte green = GetByteFromHexString(hexValue.Substring(2, 2));
            byte blue = GetByteFromHexString(hexValue.Substring(4, 2));
            if (hexValue.Length == 8)
            {
                alpha = red;
                red = green;
                green = blue;
                blue = GetByteFromHexString(hexValue.Substring(6, 2));
            }
            Color result = new Color(alpha, red, green, blue);
            return result;
        }


        private static string StandardizeHexValue(string hexValue)
        {
            hexValue = hexValue.ToUpper();
            if (hexValue.StartsWith("#"))
            {
                hexValue = hexValue[1..];
            }
            if (hexValue.Length != 3
                && hexValue.Length != 4
                && hexValue.Length != 6
                && hexValue.Length != 8
            )
            {
                throw new FormatException($"Hex value {hexValue} is in an incorrect format");
            }
            if (hexValue.Length == 3 || hexValue.Length == 4)
            {
                string newHex = string.Empty;
                foreach (char c in hexValue)
                {
                    newHex += c + c;
                }
                hexValue = newHex;
            }
            return hexValue;
        }


        private static byte GetByteFromHexString(string hexValue)
        {
            string hexChars = "0123456789ABCDEF";
            if (hexValue.Length != 2)
            {
                throw new FormatException($"{hexValue} must be exactly two characters long (this error should never be thrown)");
            }
            byte value = (byte)(16 * hexChars.IndexOf(hexValue[0], StringComparison.InvariantCultureIgnoreCase));
            value += (byte)hexChars.IndexOf(hexValue[1], StringComparison.InvariantCultureIgnoreCase);
            return value;
        }

        #endregion

        #region Operator Functions

        public static implicit operator Color(string s)
        {
            return FromHex(s);
        }

        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }

        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Color a, Color b)
        {
            return !a.Equals(b);
        }

        #endregion

        public bool Equals(Color obj)
        {
            return
                (A == obj.A)
                && (R == obj.R)
                && (G == obj.G)
                && (B == obj.B);
        }
    }
}