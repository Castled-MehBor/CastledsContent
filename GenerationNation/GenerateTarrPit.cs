using System;
using System.Drawing;
//using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.ModLoader;
using CastledsContent.Tiles.TarrPit.Block;
using CastledsContent.Tiles.TarrPit.Deco;
using System.IO;

namespace CastledsContent.GenerationNation
{
    public static class GenerateTarrPit
    {
        static Point buildUpon;
        static int count;
        static Bitmap[] layouts = new Bitmap[1];
        public static bool generated = false;
        public static void CreateTarrPits(GenerationProgress progress)
        {
            progress.Message = "Plotting the plague...";
            count = Count();
            Array.Resize(ref layouts, Count());
            for (int a = 0; a < count; ++a)
                layouts[a] = TarrPitGeneration.GetLayout(count, progress, a + 1);
            progress.Message = "Plaguing the caverns...";
            PlaceTarrPits();
            int Count()
            {
                int height = Main.maxTilesY / 300;
                return Main.rand.Next(height - 1, height + 1);
            }
        }
        public static void PlaceTarrPits() 
        {
            for (int a = 0; a < count; ++a)
                TarrPitGen(layouts[a]);
        }
        static void TarrPitGen(Bitmap layout)
        {
            Point min = new Point(200, (int)Main.rockLayer);
            Point max = new Point(Main.maxTilesX - 200, Main.maxTilesY - 300);
            buildUpon = SetPoint(layout);
            SetTiles(layout);
            void SetTiles(Bitmap bmp)
            {
                for (int b = 0; b < bmp.Height; b++)
                {
                    for(int a = 0; a < bmp.Width; a++)
                    {
                        Tile tile = Main.tile[buildUpon.X + a, buildUpon.Y + b];
                        Point point = new Point(buildUpon.X + a, buildUpon.Y + b);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 35, 40, 45))
                            PlaceTile(ModContent.TileType<TarrockT>(), point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 45, 55, 65))
                            PlaceTile(ModContent.TileType<SludgeTarrock>(), point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 20, 10, 5))
                            PlaceTile(ModContent.TileType<IonyxT>(), point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 40, 20, 10))
                            PlaceTile(ModContent.TileType<SludgeIonyx>(), point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 125, 100, 85))
                            PlaceTile(ModContent.TileType<BlackSludgeT>(), point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 15, 20, 25))
                        {
                            Main.tile[buildUpon.X + a, buildUpon.Y + b].wall = (ushort)ModContent.WallType<TarrockWall>();
                            tile.active(false);
                        }
                        SlopedTilePlace(bmp.GetPixel(a, b), point);
                    }
                }
                for (int b = 0; b < bmp.Height; b++)
                {
                    for (int a = 0; a < bmp.Width; a++)
                    {
                        Point point = new Point(buildUpon.X + a, buildUpon.Y + b);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 47, 79, 79))
                            PlaceDeco(Main.rand.NextBool(3) ? "SludgeStalag" : "RockStalag", point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 106, 90, 205))
                            PlaceDeco(Main.rand.NextBool(9) ? "Pillar" : "1x1", point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 112, 128, 144))
                            PlaceDeco("2x1", new Point(point.X, point.Y));
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 46, 139, 87))
                            PlaceDeco("1x1Other", point);
                        if (bmp.GetPixel(a, b) == Color.FromArgb(255, 0, 128, 128))
                            PlaceDeco("Beacon", point);
                    }
                }
                //ExportTexture(layout);
            }
            void SlopedTilePlace(Color col, Point p)
            {
                //0 is no slope, 1 is down-left slope, 2 is down-right slope, 3 is up-left slope and 4 is up-right slope.
                //Red up left
                if (col == Color.FromArgb(255, 255, 0, 0))
                {
                    PlaceTile(ModContent.TileType<TarrockT>(), p);
                    Main.tile[p.X, p.Y].slope(3);
                }
                //Orange up right
                if (col == Color.FromArgb(255, 255, 165, 0))
                {
                    PlaceTile(ModContent.TileType<TarrockT>(), p);
                    Main.tile[p.X, p.Y].slope(4);
                }
                //Yellow down right
                if (col == Color.FromArgb(255, 255, 255, 0))
                {
                    PlaceTile(ModContent.TileType<TarrockT>(), p);
                    Main.tile[p.X, p.Y].slope(2);
                }
                //Green down left
                if (col == Color.FromArgb(255, 0, 128, 0))
                {
                    PlaceTile(ModContent.TileType<TarrockT>(), p);
                    Main.tile[p.X, p.Y].slope(1);
                }
            }
            void PlaceTile(int type, Point coor)
            {
                Tile tile = Main.tile[coor.X, coor.Y];
                tile.wall = (ushort)ModContent.WallType<TarrockWall>();
                tile.type = (ushort)type;
                tile.active(true);
                tile.slope(0);
            }
            void PlaceDeco(string type, Point coor)
            {
                switch(type)
                {
                    case "1x1Other":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            WorldGen.PlaceTile(coor.X, coor.Y, ModContent.TileType<Sludge1x1>(), false, true);
                            Main.tile[coor.X, coor.Y].frameX = RandFrame(2);
                        }
                        break;
                    case "1x1":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            bool rand = Main.rand.NextBool(7);
                            WorldGen.PlaceTile(coor.X, coor.Y, rand ? ModContent.TileType<Sludge1x1>() : ModContent.TileType<Tarrock1x1>(), false, true);
                            Main.tile[coor.X, coor.Y].frameX = RandFrame(rand ? 1 : 5);
                        }
                        break;
                    case "2x1":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            bool rand = Main.rand.NextBool(3);
                            short frame = RandFrame(rand ? 1 : 2);
                            WorldGen.PlaceTile(coor.X, coor.Y, rand ? ModContent.TileType<Sludge2x1>() : ModContent.TileType<Tarrock2x1>(), false, true);
                            Main.tile[coor.X, coor.Y].frameX = frame;
                            //Main.tile[coor.X + 1, coor.Y].frameX = frame;
                        }
                        break;
                    case "Beacon":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            WorldGen.PlaceTile(coor.X, coor.Y, Main.rand.NextBool(2) ? ModContent.TileType<TarrockBeacon1>() : ModContent.TileType<TarrockBeacon2>(), false, true);
                        }
                        break;
                    case "Pillar":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            WorldGen.PlaceTile(coor.X, coor.Y, Main.rand.NextBool(2) ? ModContent.TileType<SludgePillar1>() : ModContent.TileType<SludgePillar2>(), false, true);
                        }
                        break;
                    case "RockStalag":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            WorldGen.PlaceTile(coor.X, coor.Y, Main.rand.NextBool(2) ? ModContent.TileType<TarrockStalagtite1>() : ModContent.TileType<TarrockStalagtite2>(), false, true);
                        }
                        break;
                    case "SludgeStalag":
                        {
                            Main.tile[coor.X, coor.Y].ClearTile();
                            Main.tile[coor.X, coor.Y].wall = (ushort)ModContent.WallType<TarrockWall>();
                            WorldGen.PlaceTile(coor.X, coor.Y, Main.rand.NextBool(2) ? ModContent.TileType<SludgeStalagtite1>() : ModContent.TileType<SludgeStalagtite2>(), false, true);
                        }
                        break;
                }
                short RandFrame(int m)
                {
                    bool rand = Main.rand.NextBool(m);
                    if (!rand)
                        return (short)(16 * Main.rand.Next(m));
                    return 0;
                }
            }
            /*void ExportTexture(Bitmap tex)
            {
                using (FileStream stream = File.Create("Gen" + Main.rand.Next(99999) + ".bmp"))
                    tex.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            }*/
            Point SetPoint(Bitmap bmp)
            {
                Point set = new Point(1, 1);
                bool successful = false;
                while(!successful)
                {
                    set = new Point(Main.rand.Next(min.X, max.X), Main.rand.Next(min.Y, max.Y));
                    if (!Valid(set, bmp))
                        successful = true;
                }
                return set;
            }
            bool Valid(Point p, Bitmap bmp) 
            {
                if (p.X < min.X || p.X > max.X || p.Y < min.Y || p.Y > max.Y)
                {
                    for (int b = 0; b < bmp.Height; b++)
                    {
                        for (int a = 0; a < bmp.Width; a++)
                        {
                            Tile tile = Main.tile[buildUpon.X + a, buildUpon.Y + b];
                            if (InvalidTile(tile.type))
                                return false;
                        }
                    }
                    return true;
                }
                return false;
                bool InvalidTile(ushort type) => type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Ash || type == TileID.LihzahrdBrick;
            }
        }
    }
}
