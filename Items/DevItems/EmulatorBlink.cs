using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.DevItems
{
    public class EmulatorBlink : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Encounter Reset");
            Tooltip.SetDefault("'Undefeats'the Ancient Spirits. Right click to set your global Ancient Spirits Encounters to 0");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.useStyle = 4;
            item.rare = 4;
            item.useTime = 45;
            item.useAnimation = 45;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                CastledWorld.dualForceEncounter = 0;
                Main.NewText($"Total number of attempts: {CastledWorld.dualForceEncounter}.");
            }
            else
            {
                CastledWorld.downedDualForce = false;
            }
            return true;
        }
    }
}