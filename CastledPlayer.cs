using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Buffs;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent
{
    public class CastledPlayer : ModPlayer
    {
        private const int saveVersion = 0;

        //Armor Sets
        public bool lunatic;
        public bool havocWasp;
        public bool robinHood;
        public bool plagueKeeper;
        public bool noble;
        public bool moltenMantis;
        public bool apprentice;
        public bool antArchy;
        //Crimson
        public bool CrimRange;
        public bool CrimMelee;
        public bool CrimMage;
        public bool CrimSummon;
        //Shadow
        public bool ShadowRange;
        public bool ShadowMelee;
        public bool ShadowMage;
        public bool ShadowSummon;
        //Extra Bools
        public bool antRage;
        public int antRageTimer = 0;
        public int antRageBuff = 0;
        public int provokeCrimsonRange = 0;
        public int provokeCorruptRange = 0;
        public int decayTimer = 0;
        public int purge = 0;

        public override void ResetEffects()
        {
            lunatic = false;
            havocWasp = false;
            robinHood = false;
            plagueKeeper = false;
            noble = false;
            moltenMantis = false;
            apprentice = false;
            antArchy = false;
            CrimRange = false;
            CrimMage = false;
            CrimMelee = false;
            CrimSummon = false;
            ShadowRange = false;
            ShadowMage = false;
            ShadowMelee = false;
            ShadowSummon = false;
        }
        public override void PostUpdateEquips()
        {
            if (lunatic)
            {
                player.moveSpeed += player.numMinions * 3f;
            }
            if (havocWasp)
            {
                player.hornetMinion = true;
                player.AddBuff(BuffID.HornetMinion, 2, false);
            }
            if (plagueKeeper)
            {
                player.bulletDamage += 0.12f;
            }
            if (noble && player.statLife < player.statLifeMax * 0.25)
            {
                player.statDefense += 4;
                player.meleeDamage += 15;
            }
            if (moltenMantis)
            {
                player.magmaStone = true;
            }
            if (apprentice)
            {
                player.manaRegen += player.statManaMax / 10;
            }
            if (antArchy)
            {
                if (player.statLife < player.statLifeMax * 0.33)
                {
                    antRageBuff = 15;
                }
                else
                {
                    antRageBuff = 0;
                }
            }
            if (purge > 15)
            {
                purge = 15;
            }
            if (CrimRange || ShadowRange)
            {
                decayTimer++;
                if (provokeCrimsonRange > 0)
                {
                    player.AddBuff(BuffType<RangeBuffC1>(), 60, false);
                }
                if (provokeCorruptRange > 0)
                {
                    player.AddBuff(BuffType<RangeBuffC>(), 60, false);
                }

                if (decayTimer > 60)
                {
                    provokeCrimsonRange -= 1;
                    provokeCorruptRange -= 1;
                    decayTimer = 0;
                }
            }
            if (decayTimer > 90)
            {
                purge--;
                decayTimer = 0;
            }
            if (purge < 0)
            {
                purge = 0;
            }
            if (antRage)
            {
                antRageTimer++;
                if (antRageTimer < 300)
                {
                    player.manaRegen += 15 + antRageBuff;
                    player.magicCrit += 15 + antRageBuff;
                    player.magicDamage += 0.15f;

                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)player.position.X, (int)(player.position.Y + ((player.height - player.width) / 2)), player.width, player.width);
                    int count = 3;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(player.position, rectangle.Width, rectangle.Height, 6, 0, 0, 100, color, 1.5f);
                    }
                }
                else
                {
                    antRageTimer = 0;
                    antRage = false;
                }
            }
        }

        public void PickAmmo(Item sItem, ref int shoot, ref float speed, ref bool canShoot, ref int Damage, ref float KnockBack, bool dontConsume = false)
        {
            if (robinHood && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake))
            {
                speed *= 1.1f;
            }
            if (CrimRange && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Bullet))
            {
                provokeCrimsonRange += 1;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if ((proj.minion || ProjectileID.Sets.MinionShot[proj.type]) && havocWasp && !proj.noEnchantments)
            {
                int num = Main.rand.Next(2);
                if (num == 0)
                {
                    target.AddBuff(BuffID.OnFire, 60 * Main.rand.Next(2, 4), false);
                }
                if (num == 1)
                {
                    target.AddBuff(BuffID.Poisoned, 180, false);
                }
            }
            if (plagueKeeper && !proj.noEnchantments)
            {
                int num = Main.rand.Next(2);
                if (num == 0)
                {
                    target.AddBuff(BuffID.Poisoned, 60 * Main.rand.Next(2, 4), false);
                }
                if (num == 1)
                {
                    if (Main.rand.Next(9) == 0)
                    {
                        target.AddBuff(BuffID.Venom, 180, false);
                    }
                }
            }
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (moltenMantis || antArchy || plagueKeeper || havocWasp)
            {
                if (player.Male)
                {
                    Main.PlaySound(SoundID.Tink);
                }
                else
                {
                    Main.PlaySound(SoundID.NPCHit7);
                }
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (moltenMantis)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileID.MolotovFire, 0, 0f, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileID.MolotovFire2, 0, 0f, player.whoAmI, 0f, 0f);
            }
            if (CrimMelee || ShadowMelee)
            {
                purge++;
                if (ShadowMelee)
                {
                    player.AddBuff(BuffType<CorruptPurge>(), 180, false);
                }
                if (CrimMelee)
                {
                    player.AddBuff(BuffType<CrimPurge>(), 180, false);
                }
            }
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (antArchy)
            {
                antRage = true;
            }
        }
    }
}