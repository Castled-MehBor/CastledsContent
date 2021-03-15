using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Items.Vanity;
using CastledsContent.Items.Material;
using CastledsContent.Items.Placeable.Painting;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs
{
    public class GlobalCasNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            switch (type)
            {
                case NPCID.Clothier:

                    {
                        if (CastledWorld.downedHarpyQueen)
                        {
                            shop.item[nextSlot].SetDefaults(ItemType<HarpyBreastplate>());
                            shop.item[nextSlot].value = 125000;
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(ItemType<HarpyLeggings>());
                            shop.item[nextSlot].value = 125000;
                            nextSlot++;
                            shop.item[nextSlot].SetDefaults(ItemType<HarpyFeather>());
                            shop.item[nextSlot].value = 27500;
                            nextSlot++;
                        }
                        if (NPC.downedBoss3)
                        {
                            shop.item[nextSlot].SetDefaults(ItemType<Items.PresetItem>());
                            shop.item[nextSlot].value = 250000;
                            nextSlot++;
                        }
                    }

                    break;
                case NPCID.Painter:

                    {
                        if (CastledWorld.downedHarpyQueen)
                        {
                            shop.item[nextSlot].SetDefaults(ItemType<HQPainting>());
                            shop.item[nextSlot].value = 17500;
                            nextSlot++;
                        }
                    }

                    break;
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.FindBuffIndex(mod.BuffType("HarpyQueenDebuff")) != -1)
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }
    }
}