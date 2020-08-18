using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.Capsule
{
	public class DeathCap2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silver Capsule");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 28;
			projectile.CloneDefaults(ProjectileID.JavelinHostile);
			aiType = ProjectileID.JavelinHostile;
			projectile.timeLeft = 1200;
		}
		public override void Kill(int timeLeft)
		{
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("RobotPH"));
			}
			if (num == 1)
			{
				NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("RobotPH1"));
			}
			if (num == 2)
			{
				NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("PistonPH1"));
			}
		}
	}
}
