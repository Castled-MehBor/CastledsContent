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
        public int leftOrRight = 0;
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
    }
}