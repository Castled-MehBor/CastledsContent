using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CastledsContent.Projectiles.Friendly;
using CastledsContent.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using System.Collections.Generic;

namespace CastledsContent.NPCs.ItemLotteryNPC.TownNPC
{
    [AutoloadHead]
    public class JanitorModule : ModNPC
    {
        private int cooldown = 0;
        private int[] yArmTimer = new int[2];
        private int[] yArm = new int[2];
        private Rectangle[] yRect = new Rectangle[2];
        private bool[] yDraw = new bool[2];
        private float[] yRot = new float[2];
        private bool arraysSetup = false;
        public override string Texture
        {
            get
            {
                return "CastledsContent/NPCs/ItemLotteryNPC/TownNPC/JanitorModule";
            }
        }

        public override bool Autoload(ref string name)
        {
            name = "Janitor Module";
            return mod.Properties.Autoload;
        }
        //public override bool UsesPartyHat() => false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Janitor Module");
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 5;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 750;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 35;
            NPCID.Sets.AttackAverageChance[npc.type] = 10;
            NPCID.Sets.HatOffsetY[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 25;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.45f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = 1f;
            animationType = NPCID.Guide;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = npc.life > 0 ? 4 : 25;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
            }
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => NPC.downedSlimeKing;
        public override void PostAI()
        {
            if (cooldown > 0)
                cooldown--;
            if (cooldown < 0)
                cooldown = 0;
            if (!arraysSetup)
            {
                npc.ai[3] = 3f;
                for (int a = 0; a < 2; a++)
                {
                    yArmTimer[a] = 0;
                    yArm[a] = 0;
                    yDraw[a] = true;
                    yRot[a] = 135f;
                    yRect[a] = new Rectangle(0, 0, 6, 22);
                }
                arraysSetup = true;
            }
            if (npc.ai[0] == 2)
            {
                npc.ai[0] = Main.rand.Next(0, 1);
                npc.ai[1] = Main.rand.Next(120, 240);
            }
            if (npc.ai[0] == 5)
            {
                npc.ai[0] = Main.rand.Next(0, 1);
                npc.ai[1] = Main.rand.Next(240, 420);
            }
            if (yDraw[0])
            {
                yArmTimer[0]++;
                if (yArmTimer[0] > 12)
                {
                    yArm[0] += 22;
                    yArmTimer[0] = 0;
                }
                if (yArm[0] > 66)
                    yArm[0] = 0;
                yRect[0].Y = yArm[0];
            }
            if (yDraw[1])
            {
                yArmTimer[1]++;
                if (yArmTimer[1] > 12)
                {
                    yArm[1] += 22;
                    yArmTimer[1] = 0;
                }
                if (yArm[1] > 66)
                    yArm[1] = 0;
                yRect[1].Y = yArm[1];
            }
            switch (npc.ai[3])
            {
                case 0:
                    {
                        yDraw[0] = false;
                        yDraw[1] = false;
                    }
                    break;
                case 4:
                    {
                        yDraw[0] = false;
                        yDraw[1] = true;
                    }
                    break;
                case 5:
                    {
                        yDraw[0] = true;
                        yDraw[1] = false;
                    }
                    break;
                case 3:
                    {
                        yDraw[0] = true;
                        yDraw[1] = true;
                    }
                    break;
            }
            //Main.NewText($"{yArmTimer[0]} {yArmTimer[1]}");
        }
        public override string TownNPCName()
        {
            return $"Janitor Module #{Main.rand.Next(200)}";
        }
        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            Item item = GetItem();
            string held = $"{item.Name} [i/1:{item.type}]";
            WeightedRandom<string> chat = new WeightedRandom<string>();
            Dialogue();
            Item GetItem()
            {
                List<Item> items = new List<Item>();
                foreach(Item i in player.inventory)
                {
                    if (!i.IsAir && i.damage > 0)
                        items.Add(i);
                }
                return Main.rand.Next(items);
            }
            bool DirtMan()
            {
                int dirt = 0;
                foreach (Item i in player.inventory)
                    if (i.type == ItemID.DirtBlock)
                        dirt += i.stack;
                if (dirt >= 5994)
                    return true;
                return false;
            }
            void Dialogue()
            {
                if (Main.hardMode)
                    chat.Add("No, I do NOT sell Allen wrenches or gerbil feeders or toilet seats or electric heaters, nor do I sell trash compactors or juice extractors or shower rods or water meters or...", 0.25);
                chat.Add($"Hold on, I'm looking at my watch... ah, it's {Main.time}", 0.85);
                chat.Add("Check your chests, check your savings. You never know when it'll all vanish into thin air.", 1.0);
                    if (DirtMan())
                chat.Add("Filthy. Absolutely filthy.", 10.0);
                if (item.melee)
                {
                    if (item.shoot == ProjectileID.None)
                        chat.Add($"That {item.Name} {held} is a primitive tool, unfit for the dangerous lifeforms of this strange planet.", 3.5);
                    if (item.damage < 30)
                        chat.Add($"The {item.Name} {held} in your possesion is uneffective against 87% of the lifeforms currently discovered in this planet.", 1.55);
                    if (item.damage > 129)
                        chat.Add($"That {item.Name} {held} in your possesion will be promptly confiscated if any attempts to attack me are taken.", 1.55);
                }
                if (item.magic)
                {
                    if (item.mana < 3)
                        chat.Add($"That {item.Name} {held} emmanates miniscule amounts of energy; enough energy to spin a pinwheel approximately 2.37 times", 1.55);
                    if (item.mana > 10)
                        chat.Add($"That {item.Name} {held} emmanates massive amounts of energy; you're a walking tesla tower", 1.55);
                    if (item.damage < 15)
                        chat.Add($"Your {item.Name} {held} is a barely functional tool. You need to take it to an individual with proficiency in the field of mana.", 1.55);
                    if (item.damage > 99)
                        chat.Add($"This {item.Name} {held} is dangerous and unstable. The likelyhood of you succumbing to its volatile nature is 73%", 1.55);
                }
                if (item.ranged)
                {
                    int total = 0;
                    for (int a = 0; a < player.inventory.Length; a++)
                        if (player.inventory[a].ammo > 0)
                            total += player.inventory[a].stack;
                    if (total < 1998)
                        chat.Add($"You have an insufficient amount of ammunition for your {held}, it is reccomended you acquire more.", 3.0);
                    if (item.damage < 20 && item.useAmmo == AmmoID.Bullet && item.shootSpeed > 9)
                        chat.Add($"That {item.Name} {held} is uneffcient, and uneffective, You need to spend your ammunition on a better firearm.", 1.55);
                    if (item.useAmmo == AmmoID.Rocket)
                        chat.Add($"Stay away, and put down your {item.Name} {held}. you are currently carrying a tremendously dangerous amount of explosives with you. Your shocking stupidity has made you into a living explosive.", 1.55);

                }
            }
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = $"Recover{RecoveryCostText(RecoveryCost())}";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
                GiveBackContra();
        }
        void GiveBackContra()
        {
            Player player = Main.LocalPlayer;
            List<Item> contra = player.GetModPlayer<CastledPlayer>().contrabande;
            bool contraBack = false;
            if (!IntendentPresent())
            {
                bool slotFilled = false;
                for (int a = 0; a < player.inventory.Length; a++)
                {
                    if (player.inventory[a].IsAir)
                    {
                        for (int b = 0; b < contra.Count; b++)
                        {
                            if (contra[b] != null && !contra[b].IsAir)
                            {
                                if (player.CanBuyItem(contra[b].value / 4) && !slotFilled)
                                {
                                    slotFilled = true;
                                    contraBack = true;
                                    player.BuyItem(contra[b].value / 4);
                                    player.inventory[a] = contra[b].Clone();
                                    contra[b] = new Item();
                                }
                            }
                        }
                        slotFilled = false;
                    }
                }
                if (contraBack)
                    Main.npcChatText = "Here's everything that I'm able to give back. If you don't see everything in your inventory, it's either because I couldn't pay for your stuff back with what you gave me, or your inventory is full. You can still pay me again to get the rest of your junk if you need to.";
                else
                    Main.npcChatText = "It seems that I wasn't able to get anything back. Some reasons that I've calculated may be either the cost you payed me isn't enough for me to retrieve anything, or my intendent hasn't found anything from you. If it's the former, try finding some more coins, and you can ask me again.";
            }
            else
                Main.npcChatText = "Sorry, can't give you anything back right now. My big ol' assistant over there is collecting some leftovers from everyone.";
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.MinigameItem>());
            shop.item[nextSlot].value = 75000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.RobotInvasion.RobotPlate>());
            shop.item[nextSlot].value = 15000;
            nextSlot++;
            if (!NPC.AnyNPCs(mod.NPCType("ItemLotteryNPC")))
            {
                for (int a = 0; a < LMan.setupShop.Count; a++)
                {
                    if (LMan.setupShop[a] != -1)
                    {
                        shop.item[nextSlot].SetDefaults(LMan.setupShop[a]);
                        shop.item[nextSlot].value = shop.item[nextSlot].value * 4;
                        nextSlot++;
                    }
                }
            }
            //shop.item[nextSlot].SetDefaults(ItemID.SandBlock);
            //nextSlot++;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            knockback = 0;
            if (Main.hardMode)
            {
                damage = 30;
                if (NPC.downedMechBossAny)
                    damage += 10;
                if (NPC.downedPlantBoss)
                    damage += 10;
                if (NPC.downedGolemBoss)
                    damage += 10;
                if (NPC.downedMoonlord)
                    damage += 15;
            }
            else
            {
                damage = 10;
                if (NPC.downedBoss1)
                    damage += 5;
                if (NPC.downedBoss2)
                    damage += 5;
                if (NPC.downedBoss3)
                    damage += 5;
            }
            npc.damage = damage;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 60;
            randExtraCooldown = 30;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (npc.ai[3] != 0 && cooldown < 1 && CheckCount())
            {
                //ModContent.ProjectileType<Projectiles.JanitorMissile>()
                Main.PlaySound(SoundID.Item11.WithVolume(0.55f), npc.position);
                int missile = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5f, 5f, ModContent.ProjectileType<JanitorMissile>(), npc.damage, 0f, Main.myPlayer, 0f, 0f);
                Main.projectile[missile].GetGlobalProjectile<MissileClass>().owner = npc;
                Main.projectile[missile].GetGlobalProjectile<MissileClass>().range1 = new Vector2(npc.position.X - 750, npc.position.Y - 750);
                Main.projectile[missile].GetGlobalProjectile<MissileClass>().range2 = new Vector2(npc.position.X + 750, npc.position.Y + 750);
                attackDelay = 60;
                switch (npc.ai[3])
                {
                    case 4:
                        npc.ai[3] = 0;
                        break;
                    case 5:
                        npc.ai[3] = 0;
                        break;
                    case 3:
                        npc.ai[3] = Main.rand.Next(3, 6);
                        break;
                }
                cooldown = 60;
            }
            bool CheckCount()
            {
                int count = 0;
                for (int a = 0; a < Main.projectile.Length; a++)
                    if (Main.projectile[a].type == ModContent.ProjectileType<JanitorMissile>() && Main.projectile[a].active)
                        count++;
                //Main.NewText(cooldown);
                //Main.NewText(count);
                return count < 2;
            }
        }
        public Color GlowColor()
        {
            switch (npc.ai[0])
            {
                case 0:
                    return new Color(Main.DiscoR - 50, Main.DiscoG - 50, Main.DiscoB - 50);
                case 1:
                    return new Color(Main.DiscoR - 230, Main.DiscoG - 210, Main.DiscoB - 190);
                case 3:
                    return new Color(Main.DiscoR / 2, (Main.DiscoG + 45) * 0.85f, 75);
                case 4:
                    return new Color(Main.DiscoR / 2, (Main.DiscoG + 45) * 0.85f, 75);
                case 17:
                    return new Color(Main.DiscoR + 75, 70, 25);
            }
            return new Color(Main.DiscoR - 95, Main.DiscoG / 2, Main.DiscoB * 0.35f);
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 14f;
            gravityCorrection = 16f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects direction = SpriteEffects.None;
            if (npc.direction == -1)
                direction = SpriteEffects.None;
            if (npc.direction == 1)
                direction = SpriteEffects.FlipHorizontally;
            if (yDraw[0])
                spriteBatch.Draw(mod.GetTexture("NPCs/ItemLotteryNPC/TownNPC/JanitorModule_Arm"), npc.Center - Main.screenPosition, yRect[0], drawColor, yRot[0], new Vector2(5f, 10f), 1f, direction, 0f);
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            //Main.NewText(npc.direction);
            Vector2 origin = new Vector2(0f, 0f);
            SpriteEffects direction = SpriteEffects.None;
            if (npc.direction == -1)
            {
                origin = new Vector2(3f, 15f);
                direction = SpriteEffects.None;
            }
            if (npc.direction == 1)
            {
                origin = new Vector2(-3f, 15f);
                direction = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("NPCs/ItemLotteryNPC/TownNPC/JanitorModule_Glow"), npc.Center - Main.screenPosition, new Rectangle(0, 0, 40, 54), GlowColor() * 1.5f, npc.rotation, new Vector2(20f, 32f), 1f, direction, 0f);
            #region Arms
            if (yDraw[1])
                spriteBatch.Draw(mod.GetTexture("NPCs/ItemLotteryNPC/TownNPC/JanitorModule_Arm"), npc.Center - Main.screenPosition, yRect[1], drawColor, yRot[1], origin, 1f, direction, 0f);
            #endregion
            //Main.NewText($"{npc.ai[0]}");
        }
        public int RecoveryCost()
        {
            Player player = Main.LocalPlayer;
            int total = 0;
            List<Item> contra = player.GetModPlayer<CastledPlayer>().contrabande;
            for (int a = 0; a < contra.Count; a++)
            {
                if (contra[a] != null && contra[a].value > 0)
                    total += contra[a].value / 4;
            }
            return total;
        }
        public string RecoveryCostText(int cost)
        {
            int initial = cost;
            int platinum = 0;
            int gold = 0;
            int silver = 0;
            int copper = 0;
            string plat;
            string go;
            string sil;
            string cop;

            if (cost > 1000000)
            {
                platinum = cost / 1000000;
                cost -= 1000000 * platinum;
            }
            if (cost > 10000)
            {
                gold = cost / 10000;
                cost -= 10000 * gold;
            }
            if (cost > 100)
            {
                silver = cost / 100;
                cost -= 100 * silver;
            }
            if (cost > 1)
                copper = cost;
            if (platinum > 0)
                plat = $"{platinum} platinum";
            else
                plat = "";
            if (gold > 0)
            {
                if (platinum > 0)
                    go = $" {gold} gold";
                else
                    go = $"{gold} gold";
            }
            else
                go = "";
            if (silver > 0)
            {
                if (gold > 0)
                    sil = $" {silver} silver";
                else
                    sil = $"{silver} silver";
            }
            else
                sil = "";
            if (copper > 0)
            {
                if (silver > 0)
                    cop = $" {copper} copper";
                else
                    cop = $"{copper} copper";
            }
            else
                cop = "";
            //Main.NewText($"{cost} {copper} {silver} {gold} {platinum}");
            if (initial > 0 && !IntendentPresent())
                return $" ({plat + go + sil + cop})";
            else if (IntendentPresent())
                return " (Unavailable)";
            else
                return "";
        }
        public bool IntendentPresent() { return NPC.AnyNPCs(mod.NPCType("ItemLotteryNPC"));  }
    }
}