using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Buffs;
using CastledsContent.Utilities;
using CastledsContent.NPCs.Boss.HarpyQueen;
using CastledsContent.Items.Accessories.RobotInvasion;
using CastledsContent.NPCs;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace CastledsContent
{
    public class CastledPlayer : ModPlayer
    {
        private const int saveVersion = 0;

        public AccessoryInfo info;
        //Armor Sets
        public bool lunatic;
        public bool havocWasp;
        public bool robinHood;
        public bool plagueKeeper;
        public bool noble;
        public bool moltenMantis;
        public bool apprentice;
        public bool antArchy;
        //Extra Values
        public DeadeyeCrosshairInfo deadeye = new DeadeyeCrosshairInfo();
        public bool antRage;
        public int antRageTimer = 0;
        public int antRageBuff = 0;
        public int spikeArmCooldown;
        public int superintendentDelay;
        //Accessories
        public bool harpyCrown;
        public bool darkCrown;
        public bool aimBot;
        #region Spike Exo Vanity
        //public bool ExoAccessory;
        //public bool ExoHideVanity;
        #endregion
        #region Zeno Vanity
        //public bool ZenoAccessory;
        //public bool ZenoHideVanity;
        #endregion
        #region Preset Values
        public int preset;
        public bool drawUI = false;
        public bool drawPreview = false;
        public bool writeName = false;
        public bool changeName = false;
        //public PlayerPreset[] presets = new PlayerPreset[5];
        public List<PlayerPreset> presets;
        #endregion
        #region Testing Bools
        public bool droneAccessory;
        #endregion
        #region Variables
        private static Item anItem;
        public static bool hasContra;
        public bool hasQuota;
        public bool parti = false;
        public bool devPerks;
        public bool setUp = false;
        public static int serves;
        public static int count;
        public List<Item> contrabande;
        public static string[] banks = { "Piggy Bank", "Safe", "Defender's Forge" };
        public readonly static List<int> bankIndex = new List<int>() { };
        #endregion
        #region Saving Stuff
        public override void Initialize()
        {
            contrabande = new List<Item>();
            superintendentDelay = 0;
            info = new AccessoryInfo();
            preset = 0;
            presets = new List<PlayerPreset>()
            {
                new PlayerPreset(),
                new PlayerPreset(),
                new PlayerPreset(),
                new PlayerPreset(),
                new PlayerPreset()
            };
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                {nameof(preset), preset },
                {nameof(presets), presets },
                {nameof(superintendentDelay), superintendentDelay },
                {nameof(contrabande), contrabande }
            };
        }
        public override void Load(TagCompound tag)
        {
            contrabande = tag.Get<List<Item>>(nameof(contrabande));
            superintendentDelay = tag.GetInt(nameof(superintendentDelay));
            preset = tag.GetInt(nameof(preset));
            if (tag.Get<List<PlayerPreset>>(nameof(presets)) != null && tag.Get<List<PlayerPreset>>(nameof(presets)).Count > 0)
                presets = tag.Get<List<PlayerPreset>>(nameof(presets));
            //if (tag.Get<PlayerPreset[]>(nameof(presets)) != null)
            //presets = tag.Get<List<PlayerPreset>>(nameof(presets));
        }
        #endregion
        public override void ResetEffects()
        {
            info.active.Clear();
            for (int a = 0; a < info.visual.Count; a++)
                info.visual[a] = false;
            lunatic = false;
            havocWasp = false;
            robinHood = false;
            plagueKeeper = false;
            noble = false;
            moltenMantis = false;
            apprentice = false;
            antArchy = false;
            harpyCrown = false;
            darkCrown = false;
            //ironShield = false;
            //spikeExo = false;
            aimBot = false;
            //ExoAccessory = ExoHideVanity = false;
            //ZenoAccessory = ZenoHideVanity = false;
        }
        public override void PostUpdateEquips()
        {
            if (!Main.dedServ && superintendentDelay > 0)
                superintendentDelay -= Main.dayRate;
            info.UpdatePlayer(player);
            #region Superintendent Contarbande Setup
            //player.dead = false;
            if (hasContra && LMan.counter > 0 && LMan.hasSucceeded)
                CastledWorld.determineContraSp = true;
            if (!setUp)
            {
                for (int a = 0; a < 254; a++)
                    contrabande.Add(new Item());
                setUp = true;
            }
            #endregion
            //ironShieldCooldown--;
            spikeArmCooldown--;
            if (spikeArmCooldown < 0)
            {
                spikeArmCooldown = 0;
            }
            /*
            if (ironShieldCooldown < 0)
            {
                ironShieldCooldown = 0;
            }*/
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
            if (NPC.AnyNPCs(ModContent.NPCType<HarpyQueen>()))
            {
                player.AddBuff(ModContent.BuffType<HarpyQueenDebuff>(), 2);
            }
            /*
            if (spikeExo)
            {
                player.statDefense += 2;
                player.thorns = 2f;
                player.endurance += 0.02f;
            }*/
            /*
            if (ironShield)
            {
                player.statDefense += 5;
            }*/
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CastledWorld.waitParti)
            {
                if (CastledsContent.JoinMinigame.JustPressed && !parti)
                {
                    Main.NewText($"{player.name} is participating!");
                    parti = true;
                }
                if (CastledsContent.JoinMinigame.JustPressed && parti)
                {
                    Main.NewText($"{player.name} has withdrawn!");
                    parti = false;
                }
            }

        }
        public void PickAmmo(Item sItem, ref int shoot, ref float speed, ref bool canShoot, ref int Damage, ref float KnockBack, bool dontConsume = false)
        {
            if (robinHood && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake))
            {
                speed *= 1.1f;
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

            if (Main.rand.NextBool(6) && info.active.Contains("IronShield") && damage > player.statLifeMax2 * 0.1f && info.values[0] <= 0)
                info.TriggerShield(1, player);
            if (info.active.Contains("ReinforcedExoskeleton"))
            {
                if (damage > player.statLifeMax2 * 0.1f && info.values[2] <= 0 && info.values[0] <= 0)
                    info.TriggerShield(2, player);
                else if (damage > player.statLifeMax2 * 0.1f &&  info.values[2] > 0)
                    info.values[2] = info.SetDuration();
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (moltenMantis)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileID.MolotovFire, 0, 0f, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileID.MolotovFire2, 0, 0f, player.whoAmI, 0f, 0f);
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
                caughtType = ModContent.ItemType<Items.Weapons.Ranged.Maradon>();
            }
        }
        /*
        public override void UpdateVanityAccessories()
        {
            for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == ModContent.ItemType<Items.Accessories.RobotInvasion.SpikeExoskeleton>())
                {
                    ExoAccessory = true;
                }
            }
        }*/
        public override void FrameEffects()
        {
            if (info.visual[0])
                SetVanity(1);
            if (info.visual[1])
                SetVanity(2);
            void SetVanity(int type)
            {
                switch (type)
                {
                    case 1:
                        {
                            player.body = mod.GetEquipSlot("ExoBody", EquipType.Body);
                            player.legs = player.Male ? mod.GetEquipSlot("ExoLegs", EquipType.Legs) : mod.GetEquipSlot("ExoLegsF", EquipType.Legs);
                        }
                        break;
                    case 2:
                        {
                            player.body = player.Male ? mod.GetEquipSlot("ExoBodyX", EquipType.Body) : mod.GetEquipSlot("ExoBodyFX", EquipType.Body);
                            player.legs = player.Male ? mod.GetEquipSlot("ExoLegsX", EquipType.Legs) : mod.GetEquipSlot("ExoLegsFX", EquipType.Legs);
                        }
                        break;
                }
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (info.visual.Count > 0)
            {
                if (info.visual[0] || info.visual[1])
                {
                    drawInfo.drawHands = true;
                    drawInfo.drawArms = true;
                }
            }
        }
        #region Algorithmo Junk
        /// <summary>
        /// Mod Item setup for ItemLotteryNPC
        /// </summary>
        /// <param name="player"></param>
        /// <param name="type"></param>
        public static void ModItemSetup(Player player)
        {
            anItem = player.inventory[player.selectedItem];
            int nullify = 3930;
            for (int c = 3930; c < ItemLoader.ItemCount; c++)
            {
                Item item = new Item();
                item.SetDefaults(c);
                player.inventory[player.selectedItem] = item;
                LMan.numList.Add(item.type);
                LMan.nameList.Add(item.Name);
                if (item.Name.Contains("Banner") || item.Name.Contains("Music Box") || item.Name.Contains("Trophy"))
                    LMan.rareList.Add(-12);
                else if (item.modItem.item.createTile != -1 || item.modItem.item.createWall != -1 && item.modItem.item.rare == ItemRarityID.White && !item.Name.Contains("Ore") && !item.Name.Contains("Bar") && !item.Name.Contains("Crate"))
                    LMan.rareList.Add(-13);
                else if (item.modItem.item.vanity || item.modItem.item.dye != 0 || item.modItem.item.hairDye != -1)
                    LMan.rareList.Add(-14);
                else if (item.modItem.item.consumable && item.modItem.item.buffType != -1)
                    LMan.rareList.Add(-15);
                else
                    LMan.rareList.Add(item.modItem.item.rare);
                nullify++;
            }
            player.inventory[player.selectedItem] = anItem;
        }
        /// <summary>
        /// Checks all slots (besides banks), and the cumilative of non-air slots will determine the quota
        /// </summary>
        /// <param name="player"></param>
        public static void CheckQuota(Player player)
        {
            if (count > 0)
                count = 0;
            for (int i = 0; i < 58; i++)
            {
                if (!player.inventory[i].IsAir)
                    count++;
            }
            for (int i = 0; i < 19; i++)
            {
                if (!player.armor[i].IsAir)
                    count++;
            }
            for (int i = 0; i < 9; i++)
            {
                if (!player.dye[i].IsAir)
                    count++;
            }
            for (int i = 0; i < 4; i++)
            {
                if (!player.miscEquips[i].IsAir)
                    count++;
            }
            for (int i = 0; i < 4; i++)
            {
                if (!player.miscDyes[i].IsAir)
                    count++;
            }
            //if (count == LMan.minItemQuota || count > LMan.minItemQuota)
            //RobotWorld.hasItemQuota = true;
            //hasQuota = true;
        }
        /// <summary>
        /// Plays a sound effect, and your contrabande serves increases
        /// </summary>
        public static void PunishContra()
        {
            //BodPlayer bod = new BodPlayer();
            Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
            serves++;
            hasContra = false;
        }
        /// <summary>
        /// Cycles through all banks, more or less acting as IsContrabande for these arrays.
        /// </summary>
        /// <param name="player"></param>
        public static void CheckBanks(Player player)
        {
            hasContra = false;
            bankIndex.Clear();
            for (int a = 0; a < 40; a++)
            {
                if (LMan.hasSucceeded && LMan.counter > 0)
                {
                    if (!player.bank.item[a].IsAir)
                    {
                        if (LMan.synci1 == player.bank.item[a].type || LMan.synci2 == player.bank.item[a].type || LMan.synci3 == player.bank.item[a].type || LMan.ib1 == player.bank.item[a].type || LMan.ib2 == player.bank.item[a].type)
                        {
                            player.bank.item[a].GetGlobalItem<SGlobalItem>().ContraColor(player.bank.item[a]);
                            if (!bankIndex.Contains(0))
                                bankIndex.Add(0);
                            //hasContra = true;
                            //Main.NewText(player.bank.item[a].Name);
                        }
                    }
                    if (!player.bank2.item[a].IsAir)
                    {
                        if (LMan.synci1 == player.bank2.item[a].type || LMan.synci2 == player.bank2.item[a].type || LMan.synci3 == player.bank2.item[a].type || LMan.ib1 == player.bank2.item[a].type || LMan.ib2 == player.bank2.item[a].type)
                        {
                            player.bank2.item[a].GetGlobalItem<SGlobalItem>().ContraColor(player.bank2.item[a]);
                            if (!bankIndex.Contains(1))
                                bankIndex.Add(1);
                            //hasContra = true;
                            //Main.NewText(player.bank.item[a].Name);
                        }
                    }
                    if (!player.bank3.item[a].IsAir)
                    {
                        if (LMan.synci1 == player.bank3.item[a].type || LMan.synci2 == player.bank3.item[a].type || LMan.synci3 == player.bank3.item[a].type || LMan.ib1 == player.bank3.item[a].type || LMan.ib2 == player.bank3.item[a].type)
                        {
                            player.bank3.item[a].GetGlobalItem<SGlobalItem>().ContraColor(player.bank3.item[a]);
                            if (!bankIndex.Contains(2))
                                bankIndex.Add(2);
                            //hasContra = true;
                            //Main.NewText(player.bank.item[a].Name);
                        }
                    }
                }
            }
            for (int b = 0; b < 4; b++)
            {
                if (LMan.hasSucceeded && LMan.counter > 0)
                {
                    if (!player.miscEquips[b].IsAir)
                    {
                        if (LMan.synci1 == player.miscEquips[b].type || LMan.synci2 == player.miscEquips[b].type || LMan.synci3 == player.miscEquips[b].type || LMan.ib1 == player.miscEquips[b].type || LMan.ib2 == player.miscEquips[b].type)
                        {
                            player.miscEquips[b].GetGlobalItem<SGlobalItem>().ContraColor(player.miscEquips[b]);
                        }
                    }
                    if (!player.miscDyes[b].IsAir)
                    {
                        if (LMan.synci1 == player.miscDyes[b].type || LMan.synci2 == player.miscDyes[b].type || LMan.synci3 == player.miscDyes[b].type || LMan.ib1 == player.miscDyes[b].type || LMan.ib2 == player.miscDyes[b].type)
                        {
                            player.miscDyes[b].GetGlobalItem<SGlobalItem>().ContraColor(player.miscDyes[b]);
                        }
                    }
                }
            }
            //RobotWorld.determineContraSp = false;
        }
        /// <summary>
        /// Determines a string value that the subtitle of the timer will display if you have contrabande in any of your banks.
        /// </summary>
        /// <returns></returns>
        public static string BankContra()
        {
            switch (bankIndex.Count)
            {
                case 1:
                    return $" in your {banks[bankIndex[0]]}";
                case 2:
                    return $" in your {banks[bankIndex[0]]} and {banks[bankIndex[1]]}";
                case 3:
                    return $" in your {banks[bankIndex[0]]}, {banks[bankIndex[1]]} and {banks[bankIndex[2]]}";
            }
            return "";
        }
        /// <summary>
        /// Unused
        /// </summary>
        public static void ContraReset()
        {
            hasContra = false;
            CastledWorld.determineContraSp = false;
            Main.NewText("Clear");
            //bankIndex.Clear();
        }
        #endregion
    }
    public class AccessoryInfo
    {
        public List<string> active = new List<string>();
        public List<string> buffs = new List<string>();
        public List<bool> visual = new List<bool>
        {
            false,
            false
        };
        public float[] values = new float[10];
        public Player player;
        public CastledPlayer casP;
        public const short Inventory = 0;
        public const short Armor = 1;
        public void UpdatePlayer(Player player)
        {
            if (active.Contains("ReinforcedExoskeleton") && CheckForItem(player, ModContent.ItemType<AimbotArrow>(), Inventory))
            {
                if (!RestrictAimbot())
                    player.GetModPlayer<CastledPlayer>().aimBot = true;
            }
            if (active.Contains("SpikeExoskeleton"))
            {
                player.statDefense += 3;
                player.thorns = 0.25f;
                player.endurance += 0.02f;
            }
            if (active.Contains("ReinforcedExoskeleton"))
            {
                player.statDefense += 8;
                player.thorns = 0.5f;
                player.endurance += 0.1f;
            }
            if (active.Contains("IronShield"))
                player.statDefense += 5;
            /*if (!active.Contains("IronShield") || !active.Contains("ReinforcedExoskeleton"))
            {
                for (int a = 0; a < values.Length; a++)
                    values[a] = 0;
                
            }*/
            #region Iron Shield
            if (buffs.Contains("IronShield") && values[1] > 0)
            {
                values[1]--;
                player.lifeRegen += 10;
                player.noKnockback = true;
            }
            if (buffs.Contains("IronShield") && values[1] <= 0)
            {
                values[1] = 0;
                buffs.Remove("IronShield");
                for (int i = 0; i < 8; i++)
                {
                    Vector2 position = player.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 8 * i));
                    Dust dust = Dust.NewDustPerfect(position, DustID.AmberBolt);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(dust.position - player.Center) * 1.25f;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                    dust.color = Color.LightCyan;
                }
            }
            if (!buffs.Contains("IronShield") && values[0] >= 0)
                values[0]--;
            #endregion
            #region Reinforced Exoskeleton
            if (values[2] >= 0)
                values[2]--;
            if (buffs.Contains("ReinforcedExoskeleton") && values[3] > 0)
            {
                values[3]--;
                player.lifeRegen += 15;
                player.immune = true;
            }
            if (buffs.Contains("ReinforcedExoskeleton") && values[3] <= 0)
            {
                values[2] = SetDuration();
                values[3] = 0;
                buffs.Remove("ReinforcedExoskeleton");
                for (int i = 0; i < 8; i++)
                {
                    Vector2 position = player.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 8 * i));
                    Dust dust = Dust.NewDustPerfect(position, DustID.AmberBolt);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(dust.position - player.Center) * 1.25f;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                    dust.color = new Color(100, 255, 200);
                }
            }
            if (!buffs.Contains("ReinforcedExoskeleton") && values[2] >= 0)
                values[2]--;
            bool RestrictAimbot()
            {
                List<int> restrict = new List<int>
            {
                ItemID.DaedalusStormbow,
                ItemID.Tsunami,
                ItemID.ChlorophyteShotbow,
                ItemID.Phantasm
            };
                foreach (Item item in player.inventory)
                {
                    if (restrict.Contains(item.type))
                        return true;
                }
                return false;
            }
            #endregion
        }
        public void TriggerShield(int type, Player player1)
        {
            switch (type)
            {
                case 1:
                    {
                        buffs.Add("IronShield");
                        Main.PlaySound(SoundID.Item67, player1.position);
                        values[0] = 900f;
                        values[1] = 300f;
                    }
                    break;
                case 2:
                    {
                        buffs.Add("ReinforcedExoskeleton");
                        Main.PlaySound(SoundID.Item67, player1.position);
                        values[2] = -1f;
                        values[3] = 300f;
                    }
                    break;
            }
        }
        public float SetDuration()
        {
            foreach (NPC n in Main.npc)
            {
                if (n.boss)
                    return 1800;
            }
            return 3600;
        }
        bool CheckForItem(Player player, int itemType, int inventoryType)
        {
            switch (inventoryType)
            {
                case Inventory:
                {
                    foreach (Item i in player.inventory)
                        if (i.type == itemType)
                            return true;
                }
                break;
                case Armor:
                    {
                        foreach (Item i in player.armor)
                            if (i.type == itemType)
                                return true;
                    }
                    break;
            }
            return false;
        }
    }
    public class DeadeyeCrosshairInfo
    {
        public int[] dmgCrit = new int[2];
        public Texture2D crosshair = ModContent.GetTexture("CastledsContent/Content/Deadeye_Crosshair");
        public Texture2D crosshairFX = ModContent.GetTexture("CastledsContent/Content/Deadeye_CrosshairEffect");
        public PossessColor filter = new PossessColor();
        public bool aiming = false;
        public bool drawAim = false;
        public Vector4 points = new Vector4(Main.MouseWorld.X - 50, Main.MouseWorld.X + 50, Main.MouseWorld.Y - 50, Main.MouseWorld.Y + 50);
        public void SetPoints()
        {
            points = new Vector4(Main.MouseWorld.X - 50, Main.MouseWorld.X + 50, Main.MouseWorld.Y - 50, Main.MouseWorld.Y + 50);
        }
        public void SeePoints()
        {
            Dust.NewDustPerfect(new Vector2(points.X, points.Z), DustID.AncientLight, Vector2.Zero);
            Dust.NewDustPerfect(new Vector2(points.X, points.W), DustID.AncientLight, Vector2.Zero);
            Dust.NewDustPerfect(new Vector2(points.Y, points.Z), DustID.AncientLight, Vector2.Zero);
            Dust.NewDustPerfect(new Vector2(points.Y, points.W), DustID.AncientLight, Vector2.Zero);
        }
        public void SetDmgCrit(Item item, Player player)
        {
            //dmgCrit[0] = item.damage * player.rangedDamageMult;
        }
        public void Fire(int damage, int crit)
        {
            List<NPC> targets = new List<NPC>();
            foreach(NPC n in Main.npc)
            {
                if (InRange(n) && (!n.friendly || !n.townNPC))
                    targets.Add(n);
            }
            foreach(NPC n in targets)
            {
                n.StrikeNPCNoInteraction(damage, 0f, 0, Main.rand.NextBool(100 - crit));
                n.AddBuff(BuffID.CursedInferno, 300);
            }
            Main.PlaySound(SoundID.Item40, Main.MouseWorld);
            Main.PlaySound(SoundID.Item14, Main.MouseWorld);
            bool InRange(NPC e) => e.position.X > points.X && e.position.X < points.Y && e.position.Y > points.Z && e.position.Y < points.W;
        }
    }
}