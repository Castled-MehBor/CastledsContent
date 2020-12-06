using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Buffs;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;

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
        public bool aimBot;
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
        //Accessory
        public bool ironShield;
        public int ironShieldCooldown;
        public bool spikeExo;
        //Extra Values
        public bool antRage;
        public int antRageTimer = 0;
        public int antRageBuff = 0;
        public int provokeCrimsonRange = 0;
        public int provokeCorruptRange = 0;
        public int decayTimer = 0;
        public int purge = 0;
        public int spikeArmCooldown;
        //Accessories
        public bool harpyCrown;
        public bool darkCrown;
        public bool restrictAimbot;
        #region Spike Exo Vanity
        public bool ExoAccessory;
        public bool ExoHideVanity;
        #endregion
        #region Zeno Vanity
        public bool ZenoAccessory;
        public bool ZenoHideVanity;
        #endregion

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
            harpyCrown = false;
            darkCrown = false;
            ironShield = false;
            spikeExo = false;
            aimBot = false;
            restrictAimbot = false;
            ExoAccessory = ExoHideVanity = false;
            ZenoAccessory = ZenoHideVanity = false;
        }
        public override void PostUpdateEquips()
        {
            ironShieldCooldown--;
            spikeArmCooldown--;
            if (spikeArmCooldown < 0)
            {
                spikeArmCooldown = 0;
            }
            if (ironShieldCooldown < 0)
            {
                ironShieldCooldown = 0;
            }
            #region Unused Set bonuses
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
            #endregion
            //Arena Debuffs
            if (NPC.AnyNPCs(mod.NPCType("HarpyQueen")))
            {
                player.AddBuff(BuffType<HarpyQueenDebuff>(), 2);
            }
            if (spikeExo)
            {
                player.statDefense += 2;
                player.thorns = 2f;
                player.endurance += 0.02f;
            }
            if (ironShield)
            {
                player.statDefense += 5;
            }
            if (restrictAimbot)
            {
                aimBot = false;
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
            if (harpyCrown)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.10f), player.position);
                    Main.PlaySound(SoundID.Item32.WithVolume(15f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 16f, mod.ProjectileType("HyperFeatherF"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-12, 12), 15f, mod.ProjectileType("GiantFeatherF"), (int)(proj.damage * 0.75), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 14f, mod.ProjectileType("HyperFeatherF"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                }
            }
            if (darkCrown)
            {
                if (Main.rand.Next(9) == 0)
                {
                    Main.PlaySound(SoundID.Item74.WithVolume(0.25f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 15f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 14f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-12, 12), -14f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-8, 8), -15f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-30, 30), 10f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-20, 20), -10f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-10, 10), -8f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 14f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-50, 50), -15f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-30, 30), -10f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 18f, mod.ProjectileType("CrowFlame"), (int)(proj.damage * 0.85), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-60, 60), -18f, mod.ProjectileType("CrowFlame"), (int)(proj.damage * 0.85), 0f, proj.owner, 0f, 0f);
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
                    Main.PlaySound(SoundID.Tink, player.position);
                }
                else
                {
                    Main.PlaySound(SoundID.NPCHit7, player.position);
                }
            }
            if (Main.rand.Next(9) == 0)
            {
                if (ironShield && ironShieldCooldown < 1)
                {
                    Main.PlaySound(SoundID.Item67, player.position);
                    player.AddBuff(BuffType<IronShieldBuff>(), 300);
                    ironShieldCooldown = 900;

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
            if (harpyCrown)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.10f), player.position);
                    Main.PlaySound(SoundID.Item32.WithVolume(15f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 16f, mod.ProjectileType("HyperFeatherF"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-12, 12), 15f, mod.ProjectileType("GiantFeatherF"), (int)(item.damage * 0.75), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 14f, mod.ProjectileType("HyperFeatherF"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                }
            }
            if (darkCrown)
            {
                if (Main.rand.Next(6) == 0)
                {
                    Main.PlaySound(SoundID.Item74.WithVolume(0.25f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 15f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 14f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-12, 12), -14f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-8, 8), -15f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-30, 30), 10f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-20, 20), -10f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-10, 10), -8f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 14f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-50, 50), -15f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-30, 30), -10f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 18f, mod.ProjectileType("CrowFlame"), (int)(item.damage * 0.85), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-60, 60), -18f, mod.ProjectileType("CrowFlame"), (int)(item.damage * 0.85), 0f, player.whoAmI, 0f, 0f);
                    }
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
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
            {
                return;
            }
            if (player.ZoneBeach && NPC.downedBoss3 && liquidType == 0 && Main.rand.NextBool(89))
            {
                caughtType = ItemType<Items.Weapons.Ranged.Maradon>();
            }
        }
        public override void UpdateVanityAccessories()
        {
            for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == ItemType<Items.Accessories.RobotInvasion.SpikeExoskeleton>())
                {
                    ExoAccessory = true;
                }
            }
        }
        public override void FrameEffects()
        {
            if (ExoAccessory && !ExoHideVanity)
            {
                if (player.Male)
                {
                    player.body = mod.GetEquipSlot("ExoStrap", EquipType.Body);
                }
                else
                {
                    player.body = mod.GetEquipSlot("ExoStrapF", EquipType.Body);
                }
            }
            if (ZenoAccessory && !ZenoHideVanity)
            {
                player.body = mod.GetEquipSlot("ZenoSuit", EquipType.Body);
            }
        }
    }
}
#region Old Code
/*
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
        //Extra Values
        public bool antRage;
        public int antRageTimer = 0;
        public int antRageBuff = 0;
        public int provokeCrimsonRange = 0;
        public int provokeCorruptRange = 0;
        public int decayTimer = 0;
        public int purge = 0;
        //Accessories
        public bool harpyCrown;
        public bool darkCrown;

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
            harpyCrown = false;
            darkCrown = false;
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
            //Arena Debuffs
            if (NPC.AnyNPCs(mod.NPCType("HarpyQueen")))
            {
                player.AddBuff(BuffType<HarpyQueenDebuff>(), 2);
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
            if (harpyCrown)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.10f), player.position);
                    Main.PlaySound(SoundID.Item32.WithVolume(15f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 16f, mod.ProjectileType("HyperFeatherF"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-12, 12), 15f, mod.ProjectileType("GiantFeatherF"), (int)(proj.damage * 0.75), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 14f, mod.ProjectileType("HyperFeatherF"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                }
            }
            if (darkCrown)
            {
                if (Main.rand.Next(9) == 0)
                {
                    Main.PlaySound(SoundID.Item74.WithVolume(0.25f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 15f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 14f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-12, 12), -14f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-8, 8), -15f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.45), 0f, proj.owner, 0f, 0f);

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-30, 30), 10f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-20, 20), -10f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-10, 10), -8f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.35), 0f, proj.owner, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 14f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-50, 50), -15f, mod.ProjectileType("DarkBolt"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-30, 30), -10f, mod.ProjectileType("DarkFireball"), (int)(proj.damage * 0.65), 0f, proj.owner, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 18f, mod.ProjectileType("CrowFlame"), (int)(proj.damage * 0.85), 0f, proj.owner, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-60, 60), -18f, mod.ProjectileType("CrowFlame"), (int)(proj.damage * 0.85), 0f, proj.owner, 0f, 0f);
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
            if (harpyCrown)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.10f), player.position);
                    Main.PlaySound(SoundID.Item32.WithVolume(15f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 16f, mod.ProjectileType("HyperFeatherF"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-12, 12), 15f, mod.ProjectileType("GiantFeatherF"), (int)(item.damage * 0.75), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 14f, mod.ProjectileType("HyperFeatherF"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                }
            }
            if (darkCrown)
            {
                if (Main.rand.Next(6) == 0)
                {
                    Main.PlaySound(SoundID.Item74.WithVolume(0.25f), player.position);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-24, 24), 15f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-18, 18), 14f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-12, 12), -14f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-8, 8), -15f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.45), 0f, player.whoAmI, 0f, 0f);

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-30, 30), 10f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-20, 20), -10f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-10, 10), -8f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.35), 0f, player.whoAmI, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 14f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-50, 50), -15f, mod.ProjectileType("DarkBolt"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-40, 40), 8f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-30, 30), -10f, mod.ProjectileType("DarkFireball"), (int)(item.damage * 0.65), 0f, player.whoAmI, 0f, 0f);
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 750, Main.rand.Next(-60, 60), 18f, mod.ProjectileType("CrowFlame"), (int)(item.damage * 0.85), 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y + 750, Main.rand.Next(-60, 60), -18f, mod.ProjectileType("CrowFlame"), (int)(item.damage * 0.85), 0f, player.whoAmI, 0f, 0f);
                    }
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
*/
#endregion