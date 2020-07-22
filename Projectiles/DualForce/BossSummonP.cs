using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce
{
    public class BossSummonP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparker");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public override void Kill(int timeLeft)
        {
            NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, mod.NPCType("BossSummon"));
        }
    }
}