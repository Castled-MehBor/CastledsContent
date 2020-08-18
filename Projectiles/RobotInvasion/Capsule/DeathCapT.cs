using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.Capsule
{
	public class DeathCapT : ModProjectile
	{
		public bool decideAtributes = false;
		public int rotationSpeed = 0;
		public int velocity = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Turrent Capsule");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 24;
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
					rotationSpeed = Main.rand.Next(-4, 4);
					velocity = Main.rand.Next(5, 9);
					decideAtributes = true;
				}
			}
			projectile.rotation += rotationSpeed;
			projectile.velocity.Y = velocity;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			projectile.velocity = Collision.AnyCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true);
			return false;
		}
        public override void Kill(int timeLeft)
        {
			NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y - 15, mod.NPCType("Turrent"));
		}
    }
}
