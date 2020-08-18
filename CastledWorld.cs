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
        //Extra Values
        public static int dualForceEncounter;
        public static bool hasMetDualForce;

        public override void Initialize()
        {
            downedbossHead = false;
            downedCorruptGuardians = false;
            downedCrimsonPrisoners = false;
            downedDualForce = false;
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
            }
            else
            {
                mod.Logger.WarnFormat("TestMod: Unknown loadVersion: {0}", loadVersion);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedbossHead;
            flags[1] = downedCorruptGuardians;
            flags[2] = downedCrimsonPrisoners;
            flags[3] = downedDualForce;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedbossHead = flags[0];
            downedCorruptGuardians = flags[1];
            downedCrimsonPrisoners = flags[2];
            downedDualForce = flags[3];
        }
        //Everything beyond this point may be removed.
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
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
    }
}