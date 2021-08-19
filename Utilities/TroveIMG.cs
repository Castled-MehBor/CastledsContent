using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CastledsContent.Utilities
{
    public static class TroveIMG
    {
        const string Red = "Red";
        const string Orange = "Orange";
        const string Yellow = "Yellow";
        const string Green = "Green";
        const string Blue = "Blue";
        const string Purple = "Purple";
        const string Grey = "Grey";
        const string Primal = "Primal";
        const string Standard = "Standard";
        const string Metallic = "Metallic";
        const string Glass = "Glass";
        const string Glowing = "Glowing";
        const string Special = "Special";
        static readonly Dictionary<string, Color> TroveColor = new Dictionary<string, Color>
    {
        #region Primal Blocks
        {"PrimalRed", new Color(155, 39, 39) },
        {"PrimalOrange", new Color(255, 148, 25) },
        {"PrimalYellow", new Color(243, 211, 2) },
        {"PrimalGreen", new Color(0, 128, 0) },
        {"PrimalBlue", new Color(47, 72, 184) },
        {"PrimalPurple", new Color(74, 48, 126) },
        {"PrimalGrey", new Color(136, 136, 136) },
        #endregion
        #region Standard Blocks
        {"MidnightBlue", new Color(000, 022, 056) },
        {"DarkBlue", new Color(010, 058, 130) },
        {"BusinessBlue", new Color(094, 145, 222) },
        {"LightBlue", new Color(198, 221, 255) },
        {"BlueBlue", new Color(024, 106, 255) },
        {"LaserBlue", new Color(016, 213, 255) },
        {"MoarBlue", new Color(000, 054, 255) },
        {"BoiledDenim", new Color(063, 075, 095) },
        {"DarkBrown", new Color(042, 031, 015) },
        {"StillPrettyDarkBrown", new Color(073, 057, 035) },
        {"Sepiagram", new Color(182, 161, 132) },
        {"AlsoBrown", new Color(051, 042, 024) },
        {"Hazelish", new Color(103, 095, 073) },
        {"DarkChocolate", new Color(033, 029, 022) },
        {"CardboardBox", new Color(196, 171, 141) },
        {"KhakiPants", new Color(160, 133, 081) },
        {"BrownTown", new Color(106, 081, 042) },
        {"DarkGreen", new Color(032, 056, 000) },
        {"LimeGreen", new Color(131, 189, 049) },
        {"SlimeGreen", new Color(168, 222, 094) },
        {"ElectricGreen", new Color(000, 255, 210) },
        {"SuperGreen", new Color(000, 255, 006) },
        {"Greenyer", new Color(000, 138, 003) },
        {"PineGreen", new Color(000, 085, 044) },
        {"ArmyMan", new Color(073, 099, 055) },
        {"RadioactiveGreen", new Color(168, 255, 000) },
        {"MossyGreen", new Color(110, 130, 030) },
        {"Charcoal", new Color(043, 043, 043) },
        {"RegularOldGrey", new Color(164, 164, 164) },
        {"LightGrey", new Color(228, 228, 228) },
        {"UmberOrange", new Color(152, 084, 011) },
        {"FestiveOrange", new Color(239, 166, 089) },
        {"Peachy", new Color(234, 216, 188) },
        {"BurntOrange", new Color(083, 043, 000) },
        {"ConstructionOrange", new Color(221, 114, 000) },
        {"OrangeCreamsicle", new Color(255, 201, 117) },
        {"PumpkinOrange", new Color(255, 174, 000) },
        {"ReddishOrange", new Color(239, 091, 045) },
        {"OrangeSherbet", new Color(240, 200, 152) },
        {"SprayTan", new Color(201, 152, 095) },
        {"OminousPurple", new Color(023, 003, 062) },
        {"DarkPurple", new Color(053, 011, 136) },
        {"Violet", new Color(137, 092, 227) },
        {"Lilac", new Color(217, 199, 255) },
        {"VibrantPurple", new Color(156, 000, 255) },
        {"8BitGrape", new Color(096, 000, 186) },
        {"PastelPurple", new Color(178, 106, 211) },
        {"SeverePeriwinkle", new Color(078, 000, 255) },
        {"MagentaMagic", new Color(133, 000, 149) },
        {"DarkRed", new Color(060, 005, 006) },
        {"CadmiumRed", new Color(131, 015, 018) },
        {"SalmonRed", new Color(222, 097, 101) },
        {"Pink", new Color(255, 199, 201) },
        {"GrandmasLipstick", new Color(128, 069, 069) },
        {"RosyPink", new Color(255, 075, 075) },
        {"PrettyPink", new Color(240, 114, 255) },
        {"Registroxel", new Color(228, 000, 255) },
        {"DarkerYellow", new Color(056, 049, 000) },
        {"DarkYellow", new Color(130, 114, 010) },
        {"ButtercupYellow", new Color(189, 170, 049) },
        {"PaleYellow", new Color(255, 247, 198) },
        {"YellowyOrange", new Color(255, 174, 000) },
        {"YellowMustard", new Color(255, 210, 000) },
        {"FastFoodYellow", new Color(255, 246, 002) },
        {"ElectricLime", new Color(207, 238, 000) },
        {"HoneyMustard", new Color(245, 204, 093) },
        {"White", new Color(231, 235, 238) },
        {"Gold", new Color(255, 255, 085) },
        {"CornerstoneFoundation", new Color(066, 061, 062) },
        {"VerdantGreen", new Color(083, 175, 020) },
        #endregion
        #region Metallic Blocks
        {"MetalBlueJeans", new Color(064, 113, 155) },
        {"BlueSteel", new Color(018, 165, 197) },
        {"MechanicBlue", new Color(041, 048, 052) },
        {"AutoBodyBlue", new Color(035, 051, 168) },
        {"DeepBlueSea", new Color(005, 014, 073) },
        {"MetallicBrown", new Color(144, 127, 117) },
        {"MetallicGreyBlue", new Color(132, 135, 150) },
        {"MetallicPurple", new Color(104, 059, 160) },
        {"PolishedPurple", new Color(131, 040, 151) },
        {"PolishedPeriwinkle", new Color(044, 044, 117) },
        {"MetalMagenta", new Color(066, 005, 073) },
        {"MidnightMetal", new Color(001, 061, 059) },
        {"MetallicRed", new Color(145, 094, 094) },
        {"RedRust", new Color(160, 059, 059) },
        {"RustyRouge", new Color(073, 005, 005) },
        {"MetallicPink", new Color(198, 054, 162) },
        {"MetallicDarkPink", new Color(187, 034, 103) },
        {"MetallicDarkOrange", new Color(117, 087, 000) },
        {"MetallicGreen", new Color(132, 152, 119) },
        {"ShinyGreen", new Color(059, 119, 052) },
        {"GlisteningMoss", new Color(141, 177, 015) },
        {"MetalCamouflage", new Color(094, 100, 072) },
        {"PewterPine", new Color(000, 138, 003) },
        {"MetallicRustOrange", new Color(167, 137, 073) },
        {"MetallicYellow", new Color(224, 205, 076) },
        {"MetallicMustard", new Color(182, 165, 049) },
        {"MetallicWhite", new Color(243, 248, 250) },
        {"Silver", new Color(185, 194, 197) },
        {"BlackIron", new Color(013, 013, 013) },
        #endregion
        #region Glass Blocks
        {"BrownGlass", new Color(092, 076, 051) },
        {"TransparentBrown", new Color(119, 082, 035) },
        {"AlsoTransparentBrown", new Color(076, 058, 024) },
        {"FrostedLeather", new Color(091, 054, 000) },
        {"PurpleGlass", new Color(123, 079, 163) },
        {"CoolMagentaGlass", new Color(116, 011, 078) },
        {"GlassyGrape", new Color(170, 027, 253) },
        {"LightPurpleGlass", new Color(202, 124, 246) },
        {"TransparentPurple", new Color(050, 000, 124) },
        {"YellowGlass", new Color(208, 191, 049) },
        {"LightYellowGlass", new Color(255, 236, 111) },
        {"VibrantYellowGlass", new Color(249, 217, 000) },
        {"OrangeGlass", new Color(195, 110, 000) },
        {"PumpkinGlass", new Color(244, 121, 011) },
        {"PeachyGlaze", new Color(228, 165, 108) },
        {"BlueGlass", new Color(045, 059, 146) },
        {"SkylightBlue", new Color(063, 142, 239) },
        {"MoonRoofBlue", new Color(000, 055, 124) },
        {"DarkBlueGlass", new Color(026, 041, 066) },
        {"GreenGlass", new Color(078, 119, 069) },
        {"SeeThroughGreen", new Color(001, 061, 059) },
        {"GrassGlass", new Color(076, 151, 056) },
        {"SeaFoam", new Color(130, 228, 104) },
        {"LimeGlass", new Color(203, 246, 098) },
        {"IcyGreen", new Color(019, 134, 141) },
        {"RedGlass", new Color(144, 022, 022) },
        {"RevealingRed", new Color(106, 002, 002) },
        {"RedSunglasses", new Color(045, 000, 000) },
        {"PinkGlass", new Color(249, 146, 245) },
        {"PlexiglassPink", new Color(255, 010, 166) },
        {"BubblegumGlass", new Color(185, 87, 149) },
        {"TurquoiseGlass", new Color(111, 177, 185) },
        {"TransparentTurquoise", new Color(068, 241, 252) },
        {"WhiteGlass", new Color(255, 255, 255) },
        {"GreyGlass", new Color(150, 150, 150) },
        {"BlackIce", new Color(000, 000, 000) },
        #endregion
        #region Glowing Blocks
        {"GlowingBlue", new Color(073, 117, 228) },
        {"GlowJeans", new Color(002, 094, 117) },
        {"DeepBlue", new Color(020, 061, 087) },
        {"GlowingGreen", new Color(108, 249, 053) },
        {"PineBulb", new Color(002, 117, 091) },
        {"ToxicNeon", new Color(210, 255, 000) },
        {"LuminescentMoss", new Color(127, 180, 000) },
        {"GlowingYellow", new Color(249, 242, 053) },
        {"MustardLightning", new Color(180, 169, 000) },
        {"GlowingPink", new Color(245, 050, 234) },
        {"NeonPink", new Color(243, 000, 132) },
        {"GlowingRed", new Color(246, 059, 059) },
        {"RadiantRust", new Color(105, 039, 039) },
        {"GlowingCyan", new Color(096, 220, 242) },
        {"GlowingDarkBlue", new Color(004, 083, 228) },
        {"GlowingDarkRed", new Color(192, 000, 000) },
        {"GlowingOrange", new Color(255, 144, 046) },
        {"CreamyOrange", new Color(255, 201, 125) },
        {"EmittingOrange", new Color(255, 096, 000) },
        {"GlowingPurple", new Color(094, 000, 224) },
        {"BrightGrape", new Color(134, 000, 248) },
        {"SpooOOookyPurple", new Color(057, 000, 128) },
        {"NeonTurquoise", new Color(000, 255, 198) },
        {"GlowingWhite", new Color(255, 255, 255) }
        #endregion
    };
        static readonly List<BlockRecipe> recipes = new List<BlockRecipe>
        {
            #region Primal
            new BlockRecipe(Primal, Red),
            new BlockRecipe(Primal, Orange),
            new BlockRecipe(Primal, Yellow),
            new BlockRecipe(Primal, Green),
            new BlockRecipe(Primal, Blue),
            new BlockRecipe(Primal, Purple),
            new BlockRecipe(Primal, Grey),
            #endregion
            #region Standard
            new BlockRecipe(Standard, Blue), //Midnight Blue
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue),
            new BlockRecipe(Standard, Blue), //Boiled Denum
            new BlockRecipe(Standard, Orange), //Dark Brown
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange), //Brown Town
            new BlockRecipe(Standard, Green), //Dark Green
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green),
            new BlockRecipe(Standard, Green), // Mossy Green
            new BlockRecipe(Standard, Grey), //Charcoal
            new BlockRecipe(Standard, Grey),
            new BlockRecipe(Standard, Grey), // Light Grey
            new BlockRecipe(Standard, Orange), //Umber Orange
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange),
            new BlockRecipe(Standard, Orange), //Spray Tan
            new BlockRecipe(Standard, Purple), //Ominous Purple
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple),
            new BlockRecipe(Standard, Purple), //Magenta Magic
            new BlockRecipe(Standard, Red), //Dark Red
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red),
            new BlockRecipe(Standard, Red), // Registroxel
            new BlockRecipe(Standard, Yellow), //Darker Yellow
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow),
            new BlockRecipe(Standard, Yellow), //Honey Mustard
            new BlockRecipe(Special, Grey), //White
            new BlockRecipe(Metallic, Yellow), //Gold
            new BlockRecipe(Metallic, Grey), // Cornerstone Foundation
            new BlockRecipe(Standard, Green), //Verdant Green
            #endregion
            #region Metallic
            new BlockRecipe(Metallic, Blue), //Metal Blue Jeans
            new BlockRecipe(Metallic, Blue),
            new BlockRecipe(Metallic, Blue),
            new BlockRecipe(Metallic, Blue),
            new BlockRecipe(Metallic, Blue), //Deep Sea Blue
            new BlockRecipe(Metallic, Orange), //Metallic Brown
            new BlockRecipe(Metallic, Blue), //Metallic Grey-Blue
            new BlockRecipe(Metallic, Purple), //Metal Purple
            new BlockRecipe(Metallic, Purple),
            new BlockRecipe(Metallic, Purple),
            new BlockRecipe(Metallic, Purple),
            new BlockRecipe(Metallic, Purple), //Midnight Metal
            new BlockRecipe(Metallic, Red), //Metallic Red
            new BlockRecipe(Metallic, Red),
            new BlockRecipe(Metallic, Red),
            new BlockRecipe(Metallic, Red),
            new BlockRecipe(Metallic, Red), //Metallic Dark Pink
            new BlockRecipe(Metallic, Orange), //Metallic Dark Orange
            new BlockRecipe(Metallic, Green), //Pewter Pine
            new BlockRecipe(Metallic, Orange), //Metallic Rust Orange
            new BlockRecipe(Metallic, Yellow), //Metallic Yellow
            new BlockRecipe(Metallic, Yellow), //Metallic Mustard
            new BlockRecipe(Metallic, Grey), //Metallic White
            new BlockRecipe(Metallic, Grey),
            new BlockRecipe(Metallic, Grey), //Black Iron
            #endregion
            #region Glass
            new BlockRecipe(Glass, Orange), //Brown Glass
            new BlockRecipe(Glass, Orange),
            new BlockRecipe(Glass, Orange),
            new BlockRecipe(Glass, Orange), //Frosted Leather
            new BlockRecipe(Glass, Purple), //Purple Glass
            new BlockRecipe(Glass, Purple),
            new BlockRecipe(Glass, Purple),
            new BlockRecipe(Glass, Purple),
            new BlockRecipe(Glass, Purple), //Transparent Purple
            new BlockRecipe(Glass, Yellow), //Yellow Glass
            new BlockRecipe(Glass, Yellow),
            new BlockRecipe(Glass, Yellow), //Vibrant Yellow Glass
            new BlockRecipe(Glass, Orange), //Orange Glass
            new BlockRecipe(Glass, Orange),
            new BlockRecipe(Glass, Orange), //Peachy Glaze
            new BlockRecipe(Glass, Blue), //Blue Glass
            new BlockRecipe(Glass, Blue),
            new BlockRecipe(Glass, Blue),
            new BlockRecipe(Glass, Blue), //Dark Blue Glass
            new BlockRecipe(Glass, Green), //Green Glass
            new BlockRecipe(Glass, Green),
            new BlockRecipe(Glass, Green),
            new BlockRecipe(Glass, Green),
            new BlockRecipe(Glass, Green),
            new BlockRecipe(Glass, Green), //Icy Green
            new BlockRecipe(Glass, Red), //Red Glass
            new BlockRecipe(Glass, Red),
            new BlockRecipe(Glass, Red),
            new BlockRecipe(Glass, Red),
            new BlockRecipe(Glass, Red),
            new BlockRecipe(Glass, Red), //Bubblegum Glass
            new BlockRecipe(Glass, Blue), //Turquoise Glass
            new BlockRecipe(Glass, Blue), //Transparent Turquoise
            new BlockRecipe(Glass, Grey), //White Glass
            new BlockRecipe(Glass, Grey),
            new BlockRecipe(Glass, Grey), //Black Ice
            #endregion
            #region Glowing
            new BlockRecipe(Glowing, Blue), //Glowing Blue
            new BlockRecipe(Glowing, Blue),
            new BlockRecipe(Glowing, Blue), //Deep Blue
            new BlockRecipe(Glowing, Green), //Glowing Green
            new BlockRecipe(Glowing, Green),
            new BlockRecipe(Glowing, Green),
            new BlockRecipe(Glowing, Green), //Luminescent Moss
            new BlockRecipe(Glowing, Yellow), //Glowing Yellow
            new BlockRecipe(Glowing, Yellow), //Mustard Lightning
            new BlockRecipe(Glowing, Red), //Glowing Pink
            new BlockRecipe(Glowing, Red),
            new BlockRecipe(Glowing, Red),
            new BlockRecipe(Glowing, Red), //Radiant Rust
            new BlockRecipe(Glowing, Blue), //Glowing Cyan
            new BlockRecipe(Glowing, Blue), //Glowing Dark Blue
            new BlockRecipe(Glowing, Red), //Glowing Dark Red
            new BlockRecipe(Glowing, Orange), //Glowing Orange
            new BlockRecipe(Glowing, Orange),
            new BlockRecipe(Glowing, Orange), //Emitting Orange
            new BlockRecipe(Glowing, Purple), //Glowing Orange
            new BlockRecipe(Glowing, Purple),
            new BlockRecipe(Glowing, Purple), //Spooky Orange
            new BlockRecipe(Glowing, Blue), //Neon Turquoise
            new BlockRecipe(Glowing, Grey), //Glowing White

            #endregion
        };
        public static void ConvIMG(Texture2D tex)
        {
            int number = Main.rand.Next(9999999);
            Texture2D clone = new Texture2D(Main.graphics.GraphicsDevice, tex.Width, tex.Height);
            Color[] array = new Color[tex.Width * tex.Height];
            tex.GetData(array);
            //Create palettes
            Color[] palette = Palette(array);
            Color[] tPal = Clone(palette);
            //Convert Trove Palette colors
            for (int a = 0; a < tPal.Length; a++)
                tPal[a] = tPal[a] == Color.Transparent ? Color.Transparent : SetTroveColor(tPal[a]);
            //Convert original color array to Trove colors
            Dictionary<Color, Color> reference = new Dictionary<Color, Color>();
            for (int a = 0; a < palette.Length; a++)
                if (!reference.ContainsKey(palette[a]))
                    reference.Add(palette[a], tPal[a]);
            CheckPalettes(palette, tPal, "Palettes" + number);
            for (int a = 0; a < array.Length; a++)
                if (array[a] != Color.Transparent)
                    if (reference.ContainsKey(array[a]))
                        array[a] = reference[array[a]];
            //Create images: Layers, then entire image
            clone.SetData(array);
            ExportTexture(clone, "Complete" + number);
            foreach (Color c in tPal)
            {
                List<Color> check = new List<Color>();
                if (!check.Contains(c))
                {
                    check.Add(c);
                    if (c != Color.Transparent)
                        CheckerboardLayer(c, clone.Width, clone.Height, $"{number}");
                }
            }
            MaterialsTxt(tPal, array, $"{number}");
            void CheckerboardLayer(Color indexer, int width, int height, string num)
            {
                Texture2D tex2 = new Texture2D(Main.graphics.GraphicsDevice, width, height);
                Color[] arr = Clone(array);
                Color[] checker = Checkerboard(arr.Length);
                for (int a = 0; a < arr.Length; a++)
                    if (arr[a] != indexer)
                        arr[a] = Color.Transparent;
                for (int a = 0; a < arr.Length; a++)
                {
                    if (arr[a] == indexer)
                        checker[a] = indexer;
                }
                tex2.SetData(checker);
                ExportTexture(tex2, GetKey(indexer) + num);
            }
        }
        /// <summary>
        /// Gets the name of the color within TroveColors, otherwise returns string.Empty
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static string GetKey(Color c)
        {
            foreach (string s in TroveColor.Keys)
            {
                if (TroveColor[s] == c)
                    return s;
            }
            return string.Empty;
        }
        static void MaterialsTxt(Color[] palette, Color[] board, string name)
        {
            int[] material = new int[8];
            string[] primalName = new string[8] { "Primal Red", "Primal Orange", "Primal Yellow", "Primal Green", "Primal Blue", "Primal Purple", "Primal Grey", "Formicite Ore" };
            List<string> lines = new List<string>();
            List<MaterialListing> materials = new List<MaterialListing>();
            foreach (Color c in palette)
            {
                if (c != Color.Transparent)
                {
                    BlockRecipe data = GetRecipe(c);
                    materials.Add(new MaterialListing(c, data.colorType));
                    ScourBoard(c, data);
                }
            }
            foreach(MaterialListing listing in materials)
            {
                BlockRecipe data = GetRecipe(listing.identifier);
                lines.Add($"{GetKey(listing.identifier)} ({Insertion(1)}{Insertion(2)})");
                material[BlockType(listing)] += listing.materials[MaterialListing.Block];
                material[7] += listing.materials[MaterialListing.Formicite];
                string Insertion(int type)
                {
                    switch(type)
                    {
                        case 1:
                            if (listing.materials[MaterialListing.Block] > 0)
                                return BlockName();
                            break;
                        case 2:
                            if (listing.materials[MaterialListing.Formicite] > 0)
                                return $", {listing.materials[MaterialListing.Formicite]} Formicite Ore";
                            break;
                    }
                    return string.Empty;
                }
                string BlockName()
                {
                    if (data.blockType == Primal)
                        return $"{listing.materials[MaterialListing.Block]}";
                    return $"{primalName[BlockType(listing)]} x {listing.materials[MaterialListing.Block]}";
                }
            }
            for (int a = 0; a < material.Length; a++)
            {
                if (material[a] > 0)
                    lines.Add($"Total {primalName[a]}: {material[a]}");
            }
            void ScourBoard(Color check, BlockRecipe data)
            {
                MaterialListing listing = Data();
                int[] additive = new int[2] { 0, 0};
                foreach(Color c in board)
                {
                    if (c == check)
                    {
                        additive[1]++;
                        if (additive[1] > (IsFormiciteType(data) ? 2 : 0))
                        {
                            AddValue();
                            additive[1] = 0;
                        }
                    }
                }
                if (IsFormiciteType(data) && additive[1] < 3)
                    AddValue();
                void AddValue()
                {
                    switch(data.blockType)
                    {
                        case Primal:
                            listing.materials[MaterialListing.Block]++;
                            break;
                        case Standard:
                            listing.materials[MaterialListing.Block]++;
                            break;
                        case Metallic:
                            {
                                listing.materials[MaterialListing.Block] += 3;
                                listing.materials[MaterialListing.Formicite]++;
                            }
                            break;
                        case Glass:
                            {
                                listing.materials[MaterialListing.Block] += 3;
                                listing.materials[MaterialListing.Formicite]++;
                            }
                            break;
                        case Glowing:
                            {
                                listing.materials[MaterialListing.Block] += 3;
                                listing.materials[MaterialListing.Formicite] += 2;
                            }
                            break;
                    }
                }
                MaterialListing Data()
                {
                    foreach (MaterialListing m in materials)
                        if (m.identifier == check)
                            return m;
                    return null;
                }
            }
            int BlockType(MaterialListing data)
            {
                switch(data.color)
                {
                    case Red:
                        return 0;
                    case Orange:
                        return 1;
                    case Yellow:
                        return 2;
                    case Green:
                        return 3;
                    case Blue:
                        return 4;
                    case Purple:
                        return 5;
                    case Grey:
                        return 6;
                }
                return 0;
            }
            using (StreamWriter file = File.CreateText($"ImageOutput/Materials{name}.txt"))
            {
                foreach (string line in lines)
                    file.WriteLine(line);
            }
        }
        static bool IsFormiciteType(BlockRecipe data) => data.blockType == Metallic || data.blockType == Glass || data.blockType == Glowing;
        static BlockRecipe GetRecipe(Color indexer)
        {
            int adding = 0;
            foreach (Color check in TroveColor.Values)
            {
                adding++;
                if (check == indexer)
                    return recipes[adding];
            }
            return null;
        }
        static void CheckPalettes(Color[] palette1, Color[] palette2, string name)
        {
            Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, palette1.Length, 2);
            Color[] array = new Color[palette1.Length + palette2.Length];
            for (int a = 0; a < palette1.Length; a++)
                array[a] = palette1[a];
            for (int a = palette1.Length; a < array.Length; a++)
                array[a] = palette2[a - palette2.Length];
            tex.SetData(array);
            ExportTexture(tex, name);
        }
        static void ExportTexture(Texture2D tex, string name)
        {
            using (FileStream stream = File.Create("ImageOutput/" + name + ".png"))
                tex.SaveAsPng(stream, tex.Width, tex.Height);
        }
        static Color[] Checkerboard(int length)
        {
            //int[] run = new int[2] { 0, 0 };
            int run1 = 0;
            int run2 = 0;
            Color[] output = new Color[length];
            Check();
            return output;
            void Check()
            {
                if (run2++ > 1)
                {
                    run2 = 0;
                    output[run1] = Color.Black;
                }
                else
                    output[run1] = Color.Transparent;
                if (run1++ < length - 1)
                    Check();
            }
        }
        static Color SetTroveColor(Color color)
        {
            int[] cycles = new int[2] { 0, 30 };
            Color output = Color.Transparent;
            CheckColor(cycles[1]);
            return output;
            void CheckColor(int intensity)
            {
                foreach (Color c in TroveColor.Values)
                {
                    if (SimilarColor(color, c, cycles[1]))
                    {
                        output = c;
                        break;
                    }
                }
                if (output == Color.Transparent && cycles[0] < 5)
                {
                    cycles[1] += 5;
                    cycles[0]++;
                    CheckColor(cycles[1]);
                }
            }
        }
        static Color[] Clone(Color[] array)
        {
            Color[] output = new Color[array.Length];
            for (int a = 0; a < array.Length; a++)
                output[a] = array[a];
            return output;
        }
        /// <summary>
        /// Creates a palette with the given color array: an array that contains one of every unique color within the array
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        static Color[] Palette(Color[] arr)
        {
            Color[] array = new Color[0];
            List<Color> colors = new List<Color>();
            foreach (Color c in arr)
            {
                if (!colors.Contains(c))
                    colors.Add(c);
            }
            Array.Resize(ref array, colors.Count);
            for (int a = 0; a < colors.Count; a++)
                array[a] = colors[a];
            return array;
        }
        static bool SimilarColor(Color compare, Color subject, int power) => RangeValue(subject.R, compare.R, power) && RangeValue(subject.G, compare.G, power) && RangeValue(subject.B, compare.B, power);
        static bool RangeValue(byte value1, byte value2, int power) => value1 >= value2 - power && value1 <= value2 + power;
        internal class BlockRecipe
        {
            public string blockType;
            public string colorType;
            public BlockRecipe(string b, string c) { blockType = b; colorType = c; }
        }
        internal class MaterialListing
        {
            public const int Block = 0;
            public const int Formicite = 1;
            public Color identifier;
            public string color;
            public int[] materials = new int[2];
            public MaterialListing(Color i, string c) { identifier = i; color = c; }
        }
    }
}
