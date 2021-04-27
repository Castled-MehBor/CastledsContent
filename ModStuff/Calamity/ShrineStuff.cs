using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace CastledsContent.ModStuff.Calamity
{
	public class ShrineFinder : ModItem
    {
		public const int Trinket = 0;
		public const int Luxor = 1;
		public const int Leash = 2;
		public const int Symbiote = 3;
		public const int Locket = 4;
		public const int Prism = 5;
		public const int EffigyCrim = 6;
		public const int EffigyCorr = 7;
		public const int Drill = 8;
		public override bool Autoload(ref string name) => CastledsContent.ModLoaded(CastledsContent.Calamity);
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Shrine Revealer");
			Tooltip.SetDefault("Reveals the location of the selected shrine\nRight-Click to change shrine type\nCertain conditions are required for each shrine\nDue to Calamity's world gen, certain shrines may not appear in your world.");
        }
        public override void SetDefaults()
        {
			item.width = 26;
			item.height = 38;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.noUseGraphic = true;
			item.rare = ItemRarityID.Orange;
        }
        public override bool UseItem(Player player)
        {
			if (CanPoint(player) && player.ownedProjectileCounts[ModContent.ProjectileType<ShrinePointer>()] < 1)
				CreatePointer(player);
			else if (player.ownedProjectileCounts[ModContent.ProjectileType<ShrinePointer>()] > 0)
				Main.projectile[player.GetModPlayer<CastledPlayer>().pointer].Kill();
			return true;
        }
        /*public override void UpdateInventory(Player player) 
		{
			if (player.HeldItem == item && CanPoint(player) && player.ownedProjectileCounts[ModContent.ProjectileType<ShrinePointer>()] < 1)
				CreatePointer(player);
			if (player.HeldItem != item) 
				Main.projectile[player.GetModPlayer<CastledPlayer>().pointer].timeLeft = 0;
		}*/
		void CreatePointer(Player player)
        {
			_ = player.GetModPlayer<CastledPlayer>().pointer;
            foreach (Chest c in Main.chest)
			{
				if (c != null && Main.tile[c.x, c.y].type == TileID.Containers)
				{
					if (Main.tile[c.x, c.y].frameX == 36 * ChestFrame() && c.item[0].type == ShrineItem())
					{
                        int pointer = Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<ShrinePointer>(), 0, 0, player.whoAmI);
                        Main.projectile[pointer].ai[0] = c.x * 16;
						Main.projectile[pointer].ai[1] = c.y * 16;
						break;
					}
				}
			}
			int ChestFrame()
            {
				switch(player.GetModPlayer<CastledPlayer>().shrineType)
                {
					case Trinket:
						return 0;
					case Luxor:
						return 30;
					case Leash:
						return 47;
					case Prism:
						return 50;
					case Locket:
						return 51;
					case EffigyCorr:
						return 3;
					case EffigyCrim:
						return 43;
					case Symbiote:
						return 32;
					case Drill:
						return 44;
                }
				return -1;
            }
		}
		bool CanPoint(Player player)
        {
			switch(player.GetModPlayer<CastledPlayer>().shrineType)
            {
				case Trinket:
					return player.statLifeMax >= 150;
				case Luxor:
					return player.statLifeMax >= 150 && NPC.downedSlimeKing;
				case Leash:
					return player.statLifeMax >= 300;
				case Prism:
					return player.statLifeMax >= 400;
				case Locket:
					return player.statLifeMax >= 400;
				case Symbiote:
					return player.statLifeMax >= 200 && NPC.downedBoss1;
				case EffigyCrim:
					return player.statLifeMax >= 300 && NPC.downedBoss2;
				case EffigyCorr:
					return player.statLifeMax >= 300 && NPC.downedBoss2;
				case Drill:
					return player.statLifeMax >= 400 && NPC.downedBoss3;
			}
			return false;
        }
		int ShrineItem()
        {
			Player player = Main.player[Main.myPlayer];
			Mod calamity = ModLoader.GetMod(CastledsContent.ModName(CastledsContent.Calamity));
			switch(player.GetModPlayer<CastledPlayer>().shrineType)
            {
				case Trinket:
					return calamity.ItemType("TrinketofChi");
				case Luxor:
					return calamity.ItemType("LuxorsGift");
				case Leash:
					return calamity.ItemType("TundraLeash");
				case Prism:
					return calamity.ItemType("UnstablePrism");
				case Locket:
					return calamity.ItemType("GladiatorsLocket");
				case Symbiote:
					return calamity.ItemType("FungalSymbiote");
				case EffigyCrim:
					return calamity.ItemType("CrimsonEffigy");
				case EffigyCorr:
					return calamity.ItemType("CorruptionEffigy");
				case Drill:
					return calamity.ItemType("OnyxExcavatorKey");
			}
			return 0;
        }
		public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
			CastledPlayer mP = player.GetModPlayer<CastledPlayer>();
			int pointer = mP.pointer;
			item.stack++;
			mP.shrineType++;
			if (mP.shrineType == EffigyCrim && !WorldGen.crimson)
				mP.shrineType = EffigyCorr;
			if (mP.shrineType == EffigyCorr && WorldGen.crimson)
				mP.shrineType = Drill;
			if (mP.shrineType > Drill)
				mP.shrineType = Trinket;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ShrinePointer>()] > 0 && Main.projectile[pointer] != null)
				Main.projectile[pointer].Kill();
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			Player player = Main.player[Main.myPlayer];
			foreach (TooltipLine item in tooltips)
			{
				if (item.mod == "Terraria" && item.Name == "ItemName")
				{
					item.overrideColor = new Color(150, 0, 0);
				}
			}
			int num = -1;
			int num2 = 0;
			while (num2 < tooltips.Count)
			{
				if (!tooltips[num2].Name.Equals("ItemName"))
				{
					num2++;
					continue;
				}
				num = num2;
				break;
			}
			tooltips.Insert(num + 3, new TooltipLine(mod, "ShrineCondition2", Condition()));
			tooltips.Insert(num + 4, new TooltipLine(mod, "ShrineCondition1", $"Conditions Met: " + (CanPoint(player) ? "[c/00FF00:Yes]" : "[c/FF0000:No]")));
			tooltips.Insert(num + 5, new TooltipLine(mod, "player.GetModPlayer<CastledPlayer>().shrineType", $"Shrine: {ItemType()}"));
			string Condition()
            {
				switch(player.GetModPlayer<CastledPlayer>().shrineType)
                {
					case Trinket:
						return "Max Life ≥ 150";
					case Luxor:
						return "Max Life ≥ 150 & King Slime is defeated";
					case Leash:
						return "Max Life ≥ 300";
					case Prism:
						return "Max Life ≥ 400";
					case Locket:
						return "Max Life ≥ 400";
					case Symbiote:
						return "Max Life ≥ 200 & Eye of Cthulhu is defeated";
					case EffigyCrim:
						return "Max Life ≥ 300 & Evil Boss (Brain of Cthulhu) is defeated";
					case EffigyCorr:
						return "Max Life ≥ 300 & Evil Boss (Eater of Worlds) is defeated";
					case Drill:
						return "Max Life ≥ 400 & Skeletron";
				}
				return string.Empty;
            }
			string ItemType()
            {
				switch(player.GetModPlayer<CastledPlayer>().shrineType)
                {
					case Trinket:
						return $"Trinket of Chi [i/1:{ShrineItem()}]";
					case Luxor:
						return $"Luxor's Gift [i/1:{ShrineItem()}]";						
					case Leash:
						return $"Tundra Leash [i/1:{ShrineItem()}]";						
					case Prism:
						return $"Unstable Prism [i/1:{ShrineItem()}]";					
					case Locket:
						return $"Gladiator's Locket [i/1:{ShrineItem()}]";						
					case Symbiote:
						return $"Fungal Symbiote [i/1:{ShrineItem()}]";						
					case EffigyCrim:
						return $"Crimson Effigy [i/1:{ShrineItem()}]";					
					case EffigyCorr:
						return $"Corruption Effigy [i/1:{ShrineItem()}]";
					case Drill:
						return $"Onyx Excavator Drill [i/1:{ShrineItem()}]";
				}
				return string.Empty;
			}
		}
    }
	public class ShrinePointer : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.aiStyle = 0;
			projectile.scale = 1f;
		}
		public override bool? CanCutTiles() => false;
        public override void AI()
		{
			Vector2 coords = new Vector2(projectile.ai[0], projectile.ai[1]);
			//Main.NewText(coords);
			Player val = Main.player[projectile.owner];
			val.GetModPlayer<CastledPlayer>().pointer = projectile.whoAmI;
			Lighting.AddLight(projectile.position, 0.3f, 0.1f, 0.1f);
			projectile.position.X = val.Center.X - 16f;
			projectile.position.Y = val.Center.Y - 16f - 42f;
			if (coords != -Vector2.One)
			{
				projectile.timeLeft = 2;
				projectile.alpha = 0;
				projectile.rotation = Utils.ToRotation(coords - projectile.Center);
				projectile.alpha = 225 - (int)(coords - projectile.Center).Length() / 25;
			}
			if (coords.X <= projectile.Center.X)
			{
				projectile.scale = -1f;
				projectile.spriteDirection = -1;
			}
			else
			{
				projectile.scale = 1f;
				projectile.spriteDirection = 1;
			}
		}
	}
}
