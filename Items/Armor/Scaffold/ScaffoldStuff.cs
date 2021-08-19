using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace CastledsContent.Items.Armor.Scaffold
{
	[AutoloadEquip(EquipType.Legs)]
	public class ScaffoldTreads : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scaffold Treads");
			Tooltip.SetDefault("8% increased general movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}
        public override void UpdateEquip(Player player)
		{
			player.maxRunSpeed += 0.08f;
			player.moveSpeed += 0.08f;
		}
	}
	[AutoloadEquip(EquipType.Body)]
	public class ScaffoldPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scaffold Plate");
			Tooltip.SetDefault("4% increased damage reduction");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
			item.defense = 3;
		}
		public override void UpdateInventory(Player player)
		{
		}
		public override void UpdateEquip(Player player)
		{
			player.endurance += 0.04f;
		}
		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}
	}
	[AutoloadEquip(EquipType.Head)]
	public class ScaffoldHeadgear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scaffold Headgear");
			Tooltip.SetDefault("4% increased damage to all classes");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.04f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ScaffoldPlate>() && legs.type == ModContent.ItemType<ScaffoldTreads>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = $"Enchanted Scaffolding:\nEach day, you can block up to two attacks\nAvailable Blocks: {player.GetModPlayer<ScaffoldPlayer>().scaffoldSet[0]}";
			player.GetModPlayer<ScaffoldPlayer>().wearingSet = true;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
	public class ScaffoldPlayer : ModPlayer
    {
		public bool wearingSet = false;
		public int[] scaffoldSet = new int[3]
		{
			2,
			0,
			0
		};
        public override void PostUpdateEquips()
        {
			if (scaffoldSet[2]-- < 0)
				scaffoldSet[2] = 0;
			scaffoldSet[1] += Main.dayRate;
			if (scaffoldSet[1] > Main.dayLength)
			{
				scaffoldSet[1] = 0;
				scaffoldSet[0] = 2;
			}
			if (scaffoldSet[2] > 0)
				player.immune = true;
		}
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			if (wearingSet)
			{
				if (scaffoldSet[0] > 0)
				{
					scaffoldSet[0]--;
					scaffoldSet[2] = 90;
					Main.PlaySound(SoundID.Tink, player.Center);
					return false;
				}
			}
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override void ResetEffects()
		{
			wearingSet = false;
		}
	}
}
