using CastledsContent.Projectiles.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Melee
{
	public class QueenJudgement : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Queen's Judgment");
			Tooltip.SetDefault("'Her word is final.'"
			+ "\nFires a short-lived giant feather."
			+ "\nWhen attacking with the shortsword itself, it will cause the target's defense to be defective.");
		}

		public override void SetDefaults()
		{
			item.damage = 35;
			item.melee = true;
			item.width = 36;
			item.height = 34;
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 0;
			item.value = 27500;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<QueenJudgementP>();
			item.shootSpeed = 6f;
			item.autoReuse = true;
			item.crit = 20;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			target.AddBuff(BuffType<Buffs.QJDebuff>(), 900);
        }
    }
}