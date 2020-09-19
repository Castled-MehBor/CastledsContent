using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Items.Placeable;

namespace CastledsContent
{
    public class CastledWorld : ModWorld
    {
        //Robot Invasion Values
        public static float globalSpinSpeed = 1.5f;
        public static int invasionPoints = 0;
        public static int numberOfEnemies = 0;
        public static int waveDelayCountdown = 0;
        public static int counterType = 1; //counterType 1 = Enemy Counter. counterType 2 = wave countdown
        public static int leftOrRight = 0;
        public static bool spinMe = false;
        public static bool eventSpawnMethod = false;
        public static int globalWave = 0;
        public static int rubCount = 0;
        public static int droneCount = 0;
        public static int doomsdayCapsuleIntensity = 0;
        public static bool doomsdayCapsule = false;
        //Downed
        public static bool downedbossHead;
        public static bool downedCorruptGuardians;
        public static bool downedCrimsonPrisoners;
        public static bool downedDualForce;
        public static bool downedHarpyQueen;
        //Extra Values
        public static int dualForceEncounter;
        public static bool hasMetDualForce;

        public override void Initialize()
        {
            downedbossHead = false;
            downedCorruptGuardians = false;
            downedCrimsonPrisoners = false;
            downedDualForce = false;
            downedHarpyQueen = false;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedbossHead)
            {
                downed.Add("bossHead");
            }

            if (downedCorruptGuardians)
            {
                downed.Add("CorruptGuardians");
            }

            if (downedCrimsonPrisoners)
            {
                downed.Add("CrimsonPrisoners");
            }
            if (downedDualForce)
            {
                downed.Add("DualForce");
            }
            if (downedHarpyQueen)
            {
                downed.Add("HarpyQueen");
            }

            return new TagCompound
            {
                ["downed"] = downed,
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedbossHead = downed.Contains("bossHead");
            downedCorruptGuardians = downed.Contains("CorruptGuardians");
            downedCrimsonPrisoners = downed.Contains("CrimsonPrisoners");
            downedDualForce = downed.Contains("DualForce");
            downedHarpyQueen = downed.Contains("HarpyQueen");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedbossHead = flags[0];
                downedCorruptGuardians = flags[1];
                downedCrimsonPrisoners = flags[2];
                downedDualForce = flags[3];
                downedHarpyQueen = flags[4];
            }
            else
            {
                mod.Logger.WarnFormat("CastledsContent: Unknown loadVersion: {0}", loadVersion);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedbossHead;
            flags[1] = downedCorruptGuardians;
            flags[2] = downedCrimsonPrisoners;
            flags[3] = downedDualForce;
            flags[4] = downedHarpyQueen;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedbossHead = flags[0];
            downedCorruptGuardians = flags[1];
            downedCrimsonPrisoners = flags[2];
            downedDualForce = flags[3];
            downedHarpyQueen = flags[4];
        }
        public override void PostWorldGen()
        {
            int[] artifactPlacement = { ItemType<SkywareArtifact>() };
            int artifactPlacementChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 13 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(artifactPlacement[artifactPlacementChoice]);
                            artifactPlacementChoice = (artifactPlacementChoice + 1) % artifactPlacement.Length;
                            break;
                        }
                    }
                }
            }
        }
        #region Everything beyond this point may be removed.
        //Everything beyond this point may be removed.
        /*
         * 
         *         public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (LivingTreesIndex != -1)
            {
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                { 
                    progress.Message = "Big Big Chungus, Big Chungus! Big Chungus!";
                    MakeJunk();
                }));
            }
        }
        //Test Structure
        private void MakeJunk()
        {
            float widthScale = Main.maxTilesX / 4200f;
            int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f * widthScale));
            for (int k = 0; k < numberToGenerate; k++)
            {
                bool success = false;
                int attempts = 0;
                while (!success)
                {
                    attempts++;
                    if (attempts > 1000)
                    {
                        success = true;
                        continue;
                    }
                    int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                    if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 + 50)
                    {
                        int j = 0;
                        while (!Main.tile[i, j].active() && (double)j < Main.worldSurface)
                        {
                            j++;
                        }
                        if (Main.tile[i, j].type == TileID.Dirt)
                        {
                            j--;
                            if (j > 150)
                            {
                                bool placementOK = true;
                                for (int l = i - 4; l < i + 4; l++)
                                {
                                    for (int m = j - 6; m < j + 20; m++)
                                    {
                                        if (Main.tile[l, m].active())
                                        {
                                            int type = (int)Main.tile[l, m].type;
                                            if (type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Cloud || type == TileID.RainCloud)
                                            {
                                                placementOK = false;
                                            }
                                        }
                                    }
                                }
                                if (placementOK)
                                {
                                    success = PlaceJunk(i, j);
                                }
                            }
                        }
                    }
                }
            }
        }
        private readonly int[,] _junkshape = {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,6,0,0,7,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,6,4,0,0,4,7,0,0,0,0,0,0,0 },
            {0,0,0,0,0,2,1,4,4,5,5,4,4,1,3,0,0,0,0,0 },
            {0,0,0,2,1,1,1,4,4,5,5,4,4,1,1,1,3,0,0,0 },
            {0,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0 },
        };
        private readonly int[,] _junkshapeWall = {
            {0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,2,2,2,0,0,1,1,0,0,2,2,2,0,0,0,0 },
            {0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0 },
            {2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2 },
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
        };

        public bool PlaceJunk(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1))
            {
                return false;
            }
            if (Main.tile[i, j].active())
            {
                return false;
            }
            if (j < 150)
            {
                return false;
            }

            for (int y = 0; y < _junkshape.GetLength(0); y++)
            {
                for (int x = 0; x < _junkshape.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_junkshape[y, x])
                        {
                            case 1:
                                tile.type = TileID.IridescentBrick;
                                tile.active(true);
                                break;
                            case 2:
                                tile.type = TileID.IridescentBrick;
                                tile.active(true);
                                tile.slope(1);
                                break;
                            case 3:
                                tile.type = TileID.IridescentBrick;
                                tile.active(true);
                                tile.slope(2);
                                break;
                            case 4:
                                tile.type = TileID.ObsidianBrick;
                                tile.active(true);
                                break;
                            case 5:
                                tile.type = TileID.Hellstone;
                                tile.active(true);
                                break;
                            case 6:
                                tile.type = TileID.ObsidianBrick;
                                tile.active(true);
                                tile.slope(1);
                                break;
                            case 7:
                                tile.type = TileID.ObsidianBrick;
                                tile.active(true);
                                tile.slope(2);
                                break;
                        }
                        switch (_junkshapeWall[y, x])
                        {
                            case 1:
                                tile.wall = WallID.ObsidianBrick;
                                break;
                            case 2:
                                tile.wall = WallID.IronFence;
                                break;
                        }
                    }
                }
            }
            return true;
        }
        }*/
        #endregion
    }
}

#region
//
/* 
 * Making world-gen :kek:
 * 
 * Tiles
 *{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,0,0,0,4,0,0,4,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,0,0,4,4,0,0,4,4,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,2,1,4,4,5,5,4,4,1,3,0,0,0,0,0 },
 *{0,0,0,2,1,1,1,4,4,5,5,4,4,1,1,1,3,0,0,0 },
 *{0,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0 },
 * Walls
 *{0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0 },
 *{0,0,0,0,2,2,2,0,0,1,1,0,0,2,2,2,0,0,0,0 },
 *{0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0 },
 *{2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2 },
 *{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
 *
 *So I'm making mac and cheese world gen for the first time.
 *I just want to make sure that these are what each number for tile.slope(#); mean:
 *2: SlopeDownRight 
 *3: SlopeDownLeft 
 *4: SlopeUpRight 
 *5: SlopeUpLeft
 *
}*/
#region
//
/* Old Colde
 *
 * public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (LivingTreesIndex != -1)
            {
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                { 
                    progress.Message = "Big Big Chungus, Big Chungus! Big Chungus!";
                    MakeJunk();
                }));
            }
        }
        //Test Structure
        private void MakeJunk()
        {
            float widthScale = Main.maxTilesX / 4200f;
            int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f * widthScale));
            for (int k = 0; k < numberToGenerate; k++)
            {
                bool success = false;
                int attempts = 0;
                while (!success)
                {
                    attempts++;
                    if (attempts > 1000)
                    {
                        success = true;
                        continue;
                    }
                    int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                    if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 + 50)
                    {
                        int j = 0;
                        while (!Main.tile[i, j].active() && (double)j < Main.worldSurface)
                        {
                            j++;
                        }
                        if (Main.tile[i, j].type == TileID.Dirt)
                        {
                            j--;
                            if (j > 150)
                            {
                                bool placementOK = true;
                                for (int l = i - 4; l < i + 4; l++)
                                {
                                    for (int m = j - 6; m < j + 20; m++)
                                    {
                                        if (Main.tile[l, m].active())
                                        {
                                            int type = (int)Main.tile[l, m].type;
                                            if (type == TileID.AmberGemspark || type == TileID.DiamondGemspark || type == TileID.SapphireGemspark || type == TileID.TopazGemspark || type == TileID.StoneSlab)
                                            {
                                                placementOK = false;
                                            }
                                        }
                                    }
                                }
                                if (placementOK)
                                {
                                    success = PlaceJunk(i, j);
                                }
                            }
                        }
                    }
                }
            }
        }
        private readonly int[,] _junkshape = {
            {0,0,3,1,4,0,0 },
            {0,3,1,1,1,4,0 },
            {3,1,1,1,1,1,4 },
            {5,5,5,6,5,5,5 },
            {5,5,5,6,5,5,5 },
            {5,5,5,6,5,5,5 },
            {2,1,5,6,5,1,2 },
            {1,1,5,5,5,1,1 },
            {1,1,5,5,5,1,1 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,5,5,5,1,0 },
            {0,1,1,1,1,1,0 },
        };
        private readonly int[,] _junkshapeWall = {
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
        };
        private readonly int[,] _junkshapeWater = {
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,0,0,0,0,0 },
        };

        public bool PlaceJunk(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1))
            {
                return false;
            }
            if (Main.tile[i, j].active())
            {
                return false;
            }
            if (j < 150)
            {
                return false;
            }

            for (int y = 0; y < _junkshape.GetLength(0); y++)
            {
                for (int x = 0; x < _junkshape.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_junkshape[y, x])
                        {
                            case 1:
                                tile.type = TileID.DiamondGemspark;
                                tile.active(true);
                                break;
                            case 2:
                                tile.type = TileID.DiamondGemspark;
                                tile.active(true);
                                tile.halfBrick(true);
                                break;
                            case 3:
                                tile.type = TileID.DiamondGemspark;
                                tile.active(true);
                                tile.slope(2);
                                break;
                            case 4:
                                tile.type = TileID.DiamondGemspark;
                                tile.active(true);
                                tile.slope(1);
                                break;
                            case 5:
                                tile.active(false);
                                break;
                            case 6:
                                tile.type = TileID.RedStucco;
                                tile.active(true);
                                break;
                        }
                        switch (_junkshapeWall[y, x])
                        {
                            case 1:
                                tile.wall = WallID.DiamondGemspark;
                                break;
                        }
                        switch (_junkshapeWater[y, x])
                        {
                            case 1:
                                tile.liquid = 255;
                                break;
                        }
                    }
                }
            }
            return true;
        }
}*/
#endregion
#endregion