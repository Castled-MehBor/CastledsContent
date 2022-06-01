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
using CastledsContent.Items.Placeable;
using CastledsContent.Items.Placeable.Pedestal;
using CastledsContent.Utilities;
using CastledsContent.Items.Storage.Boxes;
using CastledsContent.GenerationNation;

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
        public static bool downedDualForce;
        public static bool downedHarpyQueen;
        public static bool downedAlgorithmo;
        public static bool downedFlayke;
        //Extra Values
        public static int dualForceEncounter;
        public static bool hasMetDualForce;

        public static string isSafeContra;
        public static bool waitParti = false;
        public static bool finishParti = false;
        public static bool determineContraSp;
        public static int[] pirateIndex = new int[2];
        //public static bool hasItemQuota;
        //public static bool lotteryIsAlive;
        public List<int> tarr1 = new List<int>();
        public List<Vector2> tarr2 = new List<Vector2>();
        public static List<PackageData> packages = new List<PackageData>();
        public static List<PedestalData> pedestals = new List<PedestalData>();
        public override void PostUpdate()
        {
            if (determineContraSp)
                isSafeContra = "[c/FF0000:Detected]";
            //lotteryIsAlive = false;
        }
        public override TagCompound Save()
        {
            tarr1.Clear();
            tarr2.Clear();
            foreach(NPC n in Main.npc)
            {
                if (n != null && n.active && n.type == mod.NPCType("TheTarr"))
                {
                    tarr1.Add(n.type);
                    tarr2.Add(n.position);
                }
            }
            var downed = new List<string>();
            if (downedDualForce)
                downed.Add("DualForce");
            if (downedHarpyQueen)
                downed.Add("HarpyQueen");
            if (downedAlgorithmo)
                downed.Add("Algorithmo");
            if (downedFlayke)
                downed.Add("Flayke");

            return new TagCompound
            {
                ["downed"] = downed,
                ["tarr1"] = tarr1,
                ["tarr2"] = tarr2,
                ["botShop"] = LMan.setupShop,
                [nameof(dualForceEncounter)] = dualForceEncounter,
                [nameof(packages)] = packages,
                [nameof(pirateIndex)] = pirateIndex,
                [nameof(pedestals)] = pedestals
                //[nameof(mailboxes)] = mailboxes
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedDualForce = downed.Contains("DualForce");
            downedHarpyQueen = downed.Contains("HarpyQueen");
            downedAlgorithmo = downed.Contains("Algorithmo");
            downedFlayke = downed.Contains("Flayke");
            tarr1 = tag.Get<List<int>>("tarr1");
            tarr2 = tag.Get<List<Vector2>>("tarr2");
            LMan.setupShop = tag.Get<List<int>>("botShop");
            dualForceEncounter = tag.GetInt(nameof(dualForceEncounter));
            packages = tag.Get<List<PackageData>>(nameof(packages));
            pirateIndex = tag.GetIntArray(nameof(pirateIndex));
            pedestals = tag.Get<List<PedestalData>>(nameof(pedestals));
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedDualForce = flags[0];
                downedHarpyQueen = flags[1];
                //downedAlgorithmo = flags[2];
            }
            else
            {
                mod.Logger.WarnFormat("CastledsContent: Unknown loadVersion: {0}", loadVersion);
            }
        }

        public override void Initialize()
        {
            if (tarr1.Count > 0)
            {
                for (int a = 0; a < tarr1.Count; a++)
                    NPC.NewNPC((int)tarr2[a].X, (int)tarr2[a].Y, mod.NPCType("TheTarr"));
            }
            downedDualForce = false;
            downedHarpyQueen = false;
            downedAlgorithmo = false;
            downedFlayke = false;
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedDualForce;
            flags[1] = downedHarpyQueen;
            flags[2] = downedAlgorithmo;
            flags[3] = downedFlayke;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedDualForce = flags[0];
            downedHarpyQueen = flags[1];
            downedAlgorithmo = flags[2];
            downedFlayke = flags[3];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int cleanUp = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (cleanUp != -1 && ModContent.GetInstance<ClientConfig>().tarrPits)
            {
                tasks.Insert(cleanUp + 1, new PassLegacy("Tarr Pit Generation", delegate (GenerationProgress progress) {
                    progress.Message = "Plaguing the caverns...";
                    GenerateTarrPit.CreateTarrPits(progress);
                }));
            }
        }
        public override void PostWorldGen()
        {
            #region Chests
            int[] artifactPlacement = { ModContent.ItemType<SkywareArtifact>() };
            int artifactPlacementChoice = 0;
            int[] goldChestItem = { ModContent.ItemType<Items.Accessories.SamuraiInstincts>(), ModContent.ItemType<Items.Accessories.JuggernautEmblem>() };
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers)
                {
                    if (Main.tile[chest.x, chest.y].frameX == 13 * 36)
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
                    if (Main.tile[chest.x, chest.y].frameX == 36)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                if (Main.rand.NextBool(3))
                                    chest.item[inventoryIndex].SetDefaults(Main.rand.Next(goldChestItem));
                                break;
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }
}