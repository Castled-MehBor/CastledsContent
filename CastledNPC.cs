using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Projectiles;

namespace CastledsContent
{
    public class CastledNPC : GlobalNPC
    {
        public bool lightfull;
        public override bool InstancePerEntity => true;
        public override void ResetEffects(NPC npc)
        {
            lightfull = false;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (npc.HasBuff(BuffType<Buffs.Lightful>()) && projectile.GetGlobalProjectile<CastledProjectile>().isLightful)
            {
                Player player = Main.player[projectile.owner];
                for (int i = 0; i < 2; i++)
                {
                    npc.StrikeNPC(damage, knockback, npc.direction, crit, false, false);
                    player.addDPS(damage);
                }
            }
            if (projectile.GetGlobalProjectile<CastledProjectile>().returnLightful() == true)
            {
                npc.StrikeNPC(9999, knockback, npc.direction, crit, false, false);
            }
        }

    }
}