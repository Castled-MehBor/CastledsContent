using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.Capsule
{
	public class DeathCap1 : ModProjectile
	{
		public bool decideAtributes = false;
		public int rotationSpeed = 0;
		public int velocity = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bronze Capsule");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 0;
			projectile.scale = 1f;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
		}

		public override void AI()
		{
			if (decideAtributes == false)
			{
				if (rotationSpeed == 0 && velocity == 0)
				{
					rotationSpeed = Main.rand.Next(-16, 16);
					velocity = Main.rand.Next(5, 16);
					decideAtributes = true;
				}
			}
			projectile.rotation += rotationSpeed;
			projectile.velocity.Y = velocity;
		}
		public override void Kill(int timeLeft)
		{
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("Robot"));
			}
			if (num == 1)
			{
				NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("Drone"));
			}
		}
	}
}
