using CastledsContent.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace CastledsContent.Items.Summon.DistortedFlask
{
    public class DistortedFlask : ModItem
    {
        int delay;
        readonly PossessColor filter = new PossessColor();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask of Distortion");
            Tooltip.SetDefault("'Throwing this at townsfolk might be much worse than a prank.'\nA glitch in a bottle; use it wisely and carefully.");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.maxStack = 999;
            item.useTime = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.noUseGraphic = true;
            item.value = 2012;
            item.rare = ItemRarityID.Quest;
            item.UseSound = SoundID.Item106;
            item.shoot = ModContent.ProjectileType<DistortedFlaskP>();
            item.shootSpeed = 5f;
        }
        public override void UpdateInventory(Player player) { item.consumable = player.name != "Beatrix LeBeau"; }
        public override Color? GetAlpha(Color lightColor)
        {
            if (delay++ > 10)
            {
                FilterInfo info = new FilterInfo(Main.projectileTexture[ModContent.ProjectileType<DistortedFlaskExplosion>()], 25, FilterInfo.Standard, false);
                filter.GenerateTexture(info);
                delay = 0;
            }
            return base.GetAlpha(lightColor);
        }
    }
    public class DistortedFlaskP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DistortedFlask");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 18;
            projectile.CloneDefaults(ProjectileID.ToxicFlask);
            aiType = ProjectileID.ToxicFlask;
            projectile.timeLeft = 1200;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft) 
        {
            Main.PlaySound(SoundID.Item107, projectile.position);
            Main.PlaySound(SoundID.Item117, projectile.position);
            Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<DistortedFlaskExplosion>(), 0, 0, projectile.owner, 0, 0); 
        }
    }
    public class DistortedFlaskExplosion : ModProjectile
    {
        int[] totality = new int[2];
        public override void SetStaticDefaults() { DisplayName.SetDefault("Distortion Blast"); }
        public static bool WithinRange(Entity target, Entity checkTar, bool water)
        {
            int add = water ? 50 : 0;
            return target.position.X < (checkTar.position.X + checkTar.width + add) && target.position.X > (checkTar.position.X - checkTar.width - add) && target.position.Y > (checkTar.position.Y - checkTar.height - add) && target.position.Y < (checkTar.position.Y + checkTar.height + add);
        }
        public override void SetDefaults()
        {
            projectile.width = 78;
            projectile.height = 78;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.light = 0.2f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
            Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
            if (totality[0]++ > 3)
            {
                totality[0] = 0;
                totality[1]++;
                projectile.frame++;
            }
            if (totality[1] > 4)
                projectile.Kill();
            foreach (NPC n in Main.npc)
            {
                if (DistortNPC.distortion.ContainsKey(n.type) && !n.GetGlobalNPC<DistortNPC>().distorted && WithinRange(n, projectile, false))
                {
                    if (IsWed())
                        WedCheck();
                    else
                    {
                        n.GetGlobalNPC<DistortNPC>().Convert(n);
                        Main.PlaySound(SoundID.Item105, projectile.position);
                    }
                    void WedCheck()
                    {
                        foreach(NPC npc in Main.npc)
                        {
                            if (DistortNPC.distortion.ContainsKey(n.type) && !n.GetGlobalNPC<DistortNPC>().distorted && WithinRange(n, projectile, false))
                            {
                                if (Compatable() && WithinRange(npc, n, false))
                                {
                                    n.GetGlobalNPC<DistortNPC>().Convert(n);
                                    npc.GetGlobalNPC<DistortNPC>().Convert(npc);
                                    n.GetGlobalNPC<DistortNPC>().lover = npc;
                                    npc.GetGlobalNPC<DistortNPC>().lover = n;
                                    Main.PlaySound(SoundID.Item104, projectile.position);
                                    break;
                                }
                            }
                            bool Compatable()
                            {
                                switch(n.type)
                                {
                                    case NPCID.ArmsDealer:
                                        return npc.type == NPCID.Nurse;
                                    case NPCID.Nurse:
                                        return npc.type == NPCID.ArmsDealer;
                                    case NPCID.GoblinTinkerer:
                                        return npc.type == NPCID.Mechanic;
                                    case NPCID.Mechanic:
                                        return npc.type == NPCID.GoblinTinkerer;
                                }
                                return false;
                            }
                        }
                    }
                }
                bool IsWed() => n.type == NPCID.ArmsDealer || n.type == NPCID.Nurse || n.type == NPCID.GoblinTinkerer || n.type == NPCID.Mechanic;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            switch (totality[1])
            {
                case 0:
                    {
                        hitbox.X += 33;
                        hitbox.Y += 33;
                        hitbox.Width = 14;
                        hitbox.Height = 14;
                    }
                    break;
                case 1:
                    {
                        hitbox.X += 24;
                        hitbox.Y += 24;
                        hitbox.Width = 30;
                        hitbox.Height = 30;
                        projectile.width = projectile.height = 30;
                    }
                    break;
                case 2:
                    {
                        hitbox.X += 16;
                        hitbox.Y += 16;
                        hitbox.Width = 46;
                        hitbox.Height = 46;
                        projectile.width = projectile.height = 46;
                    }
                    break;
                case 3:
                    {
                        hitbox.X += 8;
                        hitbox.Y += 8;
                        hitbox.Width = 62;
                        hitbox.Height = 62;
                        projectile.width = projectile.height = 62;
                    }
                    break;
                case 4:
                    {
                        hitbox.Width = 78;
                        hitbox.Height = 78;
                        projectile.width = projectile.height = 78;
                    }
                    break;
            }
        }
    }
    public class DistortNPC : GlobalNPC
    {
        public bool[] witch = new bool[3];
        readonly string Directory = "CastledsContent/Content/Testxures/HostileTownNPC/";
        public static Dictionary<int, int> distortion = new Dictionary<int, int>
        {
            { NPCID.Guide, NPCID.DoctorBones },
            { NPCID.ArmsDealer, NPCID.TheGroom },
            { NPCID.Nurse, NPCID.TheBride },
            { NPCID.GoblinTinkerer, NPCID.TheGroom },
            { NPCID.Mechanic, NPCID.TheBride },
            { NPCID.Wizard, NPCID.RuneWizard },
            { NPCID.Dryad, NPCID.Nymph },
            { NPCID.Demolitionist, NPCID.UndeadMiner },
            { NPCID.Pirate, NPCID.PirateCaptain },
            { NPCID.SkeletonMerchant, NPCID.Tim }
        };
        public NPC lover;
        bool aiWiz;
        int key;
        public bool distorted;
        int[] wedIdentifier = new int[4];
        readonly int Gangster = 0;
        readonly int Tinkerer = 1;
        readonly int Mechanic = 2;
        readonly int Nurse = 3;
        public override bool InstancePerEntity => true;
        Texture2D GetDistortTexture(int type)
        {
            switch(type)
            {
                case NPCID.Guide:
                    return ModContent.GetTexture(Directory + "GuideDrBones");
                case NPCID.ArmsDealer:
                    return ModContent.GetTexture(Directory + "GangsterGroom");
                case NPCID.GoblinTinkerer:
                    return ModContent.GetTexture(Directory + "GoblinGroom");
                case NPCID.Nurse:
                    return ModContent.GetTexture(Directory + "NurseBride");
                case NPCID.Mechanic:
                    return ModContent.GetTexture(Directory + "MechanicBride");
                case NPCID.Wizard:
                    return ModContent.GetTexture(Directory + "WizardRuneWiz");
                case NPCID.Dryad:
                    return ModContent.GetTexture(Directory + "DryadNymph");
                case NPCID.Demolitionist:
                    return ModContent.GetTexture(Directory + "DemoMiner");
                case NPCID.SkeletonMerchant:
                    return ModContent.GetTexture(Directory + "SkeletonTim");
            }
            return null;
        }
        public void Convert(NPC npc)
        {
            string message = npc.FullName + " was corrupted...";
            int type = npc.type;
            if (npc.type != NPCID.SkeletonMerchant)
            {
                if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(message, Color.Red);
                else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), Color.Red);
            }
            int[] wed = new int[4];
            Identify(wed);
            npc.SetDefaults(distortion[npc.type]);
            npc.lifeMax += npc.lifeMax;
            npc.life = npc.lifeMax;
            npc.GetGlobalNPC<DistortNPC>().distorted = true;
            npc.GetGlobalNPC<DistortNPC>().key = type;
            npc.GetGlobalNPC<DistortNPC>().wedIdentifier = wed;
            int[] Identify(int[] array)
            {
                switch(npc.type)
                {
                    case NPCID.ArmsDealer:
                        array[0] = 1;
                        break;
                    case NPCID.GoblinTinkerer:
                        array[1] = 1;
                        break;
                    case NPCID.Mechanic:
                        array[2] = 1;
                        break;
                    case NPCID.Nurse:
                        array[3] = 1;
                        break;
                }
                return array;
            }
        }
        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (distorted && !PureType(target, 1) && !PureType(npc, 2))
            {
                if (distortion.ContainsKey(target.type) && !target.GetGlobalNPC<DistortNPC>().distorted)
                    target.GetGlobalNPC<DistortNPC>().Convert(target);
            }
        }
        bool PureType(NPC npc, int type)
        {
            switch(type)
            {
                case 1:
                    return npc.type == NPCID.Dryad || npc.type == NPCID.Pirate || npc.type == NPCID.SkeletonMerchant;
                case 2:
                    return npc.type == NPCID.Nymph || npc.type == NPCID.PirateCaptain || npc.type == NPCID.Tim;
            }
            return false;
        }
        public static bool IsWitch(NPC npc) => npc.type == NPCID.Stylist && npc.GetGlobalNPC<DistortNPC>().witch[1];
        public override void AI(NPC npc)
        {
            Player player = !npc.friendly ? Main.player[npc.target] : Main.LocalPlayer;
            if (npc.type == NPCID.Stylist)
            {
                if (!witch[0] && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Witch.StylistWitch>()))
                {
                    witch[1] = Main.rand.NextBool(3);
                    witch[0] = true;
                }
                if (IsWitch(npc))
                {
                    npc.buffImmune[BuffID.Poisoned] = true;
                    npc.buffImmune[BuffID.Venom] = true;
                    if (!witch[2] && LineOfSight(player) && Main.rand.NextBool(70))
                        Dust.NewDust(npc.position, npc.width, npc.height, 172, 0, 4);
                    bool LineOfSight(Entity e)
                    {
                        Vector4 range = new Vector4(npc.position.X + (npc.direction == -1 ? -500 : 500), npc.position.Y - 250, npc.position.X + (npc.direction == -1 ? 50 : -50), npc.position.Y + 250);
                        if (!npc.HasBuff(BuffID.OnFire) && Collision.CanHitLine(npc.position, npc.width, npc.height, e.position, e.width, e.height))
                        {
                            if (npc.direction == -1)
                                return e.position.X > range.X && e.position.X <= range.Z;
                            if (npc.direction == 1)
                                return e.position.X < range.X && e.position.X >= range.Z;
                        }
                        return false;
                    }
                }
            }
            if (distorted)
            {
                if (npc.type == NPCID.RuneWizard)
                {
                    if (npc.ai[1] > 0 && !aiWiz)
                    {
                        aiWiz = true;
                        npc.ai[2]++;
                        foreach(float f in Rotations())
                            Projectile.NewProjectile(npc.position, new Vector2(5, 5).RotatedBy(f, default), Type(), player.statLifeMax2 / 4, 1f);
                    }
                    if (npc.ai[1] < 1)
                        aiWiz = false;
                    if (npc.ai[0] > 500)
                        npc.ai[2] = 0;
                    int Type()
                    {
                        switch (npc.ai[2])
                        {
                            case 2:
                                return Main.rand.Next(326, 328);
                            case 4:
                                return ProjectileID.InfernoHostileBolt;
                            case 6:
                                return ProjectileID.RuneBlast;
                        }
                        return ProjectileID.None;
                    }
                    float[] Rotations()
                    {
                        float random = Main.rand.Next(-90, 90);
                        switch (npc.ai[2])
                        {
                            case 2:
                                return new float[] { 0 + random, 90 + random, 180 + random, 270 + random };
                            case 4:
                                return new float[] { 45 + random, 135 + random, 225 + random, 315 + random };
                            case 6:
                                return new float[] { 0 + random, 45 + random, 90 + random, 135 + random, 180 + random, 225 + random, 270 + random, 315 + random };
                        }
                        return new float[4];
                    }
                }
                if (npc.type == NPCID.Tim)
                {
                    Vector4 range = new Vector4(npc.position.X - 400, npc.position.Y - 400, npc.position.X + 400, npc.position.Y + 400);
                    npc.dontTakeDamage = true;
                    npc.ai[2]++;
                    npc.ai[0] = 0;
                    if (npc.ai[2] == 30)
                        CombatText.NewText(npc.getRect(), Color.Purple, "Wooo!!");
                    if (npc.ai[2] == 100)
                        CombatText.NewText(npc.getRect(), Color.Purple, "I always wanted to be a wizard!");
                    if (npc.ai[2] == 200)
                        CombatText.NewText(npc.getRect(), Color.Purple, "Thanks " + (player.Male ? "lad." : "lass."));
                    if (npc.ai[2] > 240 || !WithinRange())
                    {
                        Main.PlaySound(SoundID.Item8.WithVolume(5f), npc.position);
                        int i = Item.NewItem(npc.position, ItemID.LifeCrystal);
                        ApplyProperties(Main.item[i]);
                        for (int a = 0; a < 20; a++)
                        {
                            Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 20 * a));
                            Dust dust = Dust.NewDustPerfect(position, DustID.Shadowflame);
                            dust.noGravity = true;
                            dust.velocity = Vector2.Normalize(dust.position - npc.Center) * 4;
                            dust.noLight = false;
                            dust.fadeIn = 1f;
                        }
                        npc.active = false;
                    }
                    bool WithinRange() => player.position.X > range.X && player.position.Y > range.Y && player.position.X < range.Z && player.position.Y < range.W;
                }
                if (IsWed())
                {
                    if (lover != null && lover.active)
                    {
                        if (Collision.CanHitLine(npc.position, npc.width, npc.height, lover.position, lover.width, lover.height))
                        {
                            npc.AddBuff(BuffID.Lovestruck, 2);
                            npc.defense = 75;
                        }
                    }
                    else
                    {
                        npc.buffImmune[BuffID.Lovestruck] = true;
                        npc.defense = 0;
                    }
                }
                bool IsWed() => npc.type == NPCID.TheGroom || npc.type == NPCID.TheBride;
            }
        }
        void ApplyProperties(Item item)
        {
            item.maxStack = 1;
            item.value = 0;
            item.color = Color.Firebrick;
            item.GetGlobalItem<DistortItem>().distorted = true;
            item.material = false;
        }
        public override void GetChat(NPC npc, ref string chat)
        {
            if (IsWitch(npc))
            {
                Mod split = ModLoader.GetMod("Split");
                Player player = Main.LocalPlayer;
                bool nun = player.HeldItem.type == ItemID.HolyWater;
                List<string> message = new List<string>
            {
                "Oh, uh... hello there! Can I... help you?",
                "What cross- oh, that's just... my, hammer! Yes! I'm currently working on improving my box of a home.",
                "You want lemonade? You do realise that lemonade is extremely acidic, right? ...What do you mean it's sour?",
                "I cut hair for a living... how pathe- I mean, perfect! Yes, perfect!",
                "What'll it be today, " + (player.Male ? "mister? A buzzcut? A mohawk? Or just, a standard cut?" : "miss? A ponytail? Or, actually, I do have a way to make your hair longer; please don't ask how, okay?"),
                "An imposter? What are you talking about, you silly "  + (player.Male ? "boy?" : "girl?"),
                "Funny story, actually: One time, I was out at the park, and I saw a peculiar monkey on one of the trees. I tried to get a closer look, and that's how I ended up with splinters for two weeks.",
                "Why are you looking at me like that?",
                "Imagine Dragons? Why imagine? Be the change that the world needs, am I right?",
                "Could you maybe back off a bit?"
            };
                if (Main.raining)
                    message.Add("On days like this, one of my customers asked me for 'any oil products that I have', so I gave them some oil grease, and they just dumped it all over themselves and dashed out of the door of the barbershop. The next thing that I saw was unremarkable...");
                if (nun)
                    chat = Main.rand.NextBool(2) ? "You're not gonna... use that on me, are you?" : "I'll do anything, I swear! Just please let me live!!";
                if (split != null)
                    message.Add("A wise old lady once taught me that tea can bring you miles forward. Unfortunately, all you want to drink is orange juice.");
                if (!nun)
                    chat = Main.rand.Next(message);
            }
        }
        public override void NPCLoot(NPC npc)
        {
            if (distorted)
            {
                Player player = Main.player[npc.target];
                if (npc.type == NPCID.DoctorBones)
                {
                    for (int a = 0; a < 2; a++)
                        Projectile.NewProjectile(npc.position, new Vector2(a == 1 ? 5 : -5, 0), ProjectileID.Boulder, player.statLifeMax2, 1f);
                }
                if (npc.type == NPCID.UndeadMiner)
                    for (int a = 0; a < 3; a++)
                    {
                        int b = Projectile.NewProjectile(npc.position, new Vector2(4, 4).RotatedBy(Main.rand.Next(-180, 180), default), ProjectileID.Grenade, npc.damage, 1f);
                        Main.projectile[b].timeLeft = 300;
                        Main.projectile[b].friendly = false;
                        Main.projectile[b].hostile = true;
                    }
                DropDistortedItem();
            }
            void DropDistortedItem()
            {
                int GetType()
                {
                    switch (npc.type)
                    {
                        case NPCID.DoctorBones:
                            return Item.NewItem(npc.position, ItemID.Book);
                        case NPCID.UndeadMiner:
                            return Item.NewItem(npc.position, ItemID.Skull);
                        case NPCID.Nymph:
                            return Item.NewItem(npc.position, ItemID.Ruby);
                        case NPCID.TheGroom:
                            if (Identifier(Gangster))
                                return Item.NewItem(npc.position, ItemID.IllegalGunParts);
                            if (Identifier(Tinkerer))
                                return Item.NewItem(npc.position, ItemID.Goggles);
                            return 0;
                        case NPCID.TheBride:
                            if (Identifier(Mechanic))
                                return Item.NewItem(npc.position, ItemID.LaserRuler);
                            if (Identifier(Nurse))
                                return Item.NewItem(npc.position, ItemID.JungleRose);
                            return 0;
                        case NPCID.PirateCaptain:
                            return Item.NewItem(npc.position, ItemID.EyePatch);
                        case NPCID.RuneWizard:
                            return Item.NewItem(npc.position, ItemID.WizardsHat);
                    }
                    return 0;
                }
                ApplyProperties(Main.item[GetType()]);
                bool Identifier(int goal)
                {
                    for (int a = 0; a < wedIdentifier.Length; a++)
                    {
                        if (wedIdentifier[a] == 0)
                            continue;
                        if (wedIdentifier[a] == 1)
                            return a == goal;
                    }
                    return false;
                }
            }
        }
        public override bool CheckDead(NPC npc)
        {
            if (distorted && npc.type == NPCID.Tim)
            {
                npc.life = npc.lifeMax;
                return false;
            }
            return base.CheckDead(npc);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (distorted && npc.type != NPCID.PirateCaptain)
            {
                bool Wizard() => npc.type == NPCID.RuneWizard || npc.type == NPCID.Tim;
                Vector2 origin = Wizard() ? new Vector2(20, 45) : new Vector2(20, 30);
                spriteBatch.Draw(GetDistortTexture(key), npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, origin, 1f, npc.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                return false;
            }
            return true;
        }
    }
    public class DistortItem : GlobalItem
    {
        public bool distorted;
        public override bool InstancePerEntity => true;
        public override GlobalItem NewInstance(Item item)
        {
            return base.NewInstance(item);
        }
        public override bool NeedsSaving(Item item) => item.GetGlobalItem<DistortItem>().distorted;
        public override TagCompound Save(Item item)
        {
            return new TagCompound
            {
                {"distorted", item.GetGlobalItem<DistortItem>().distorted }
            };
        }
        public override void Load(Item item, TagCompound tag) 
        {
            item.GetGlobalItem<DistortItem>().distorted = tag.GetBool("distorted");
            if (item.GetGlobalItem<DistortItem>().distorted)
            {
                item.maxStack = 1;
                item.value = 0;
            }
        }
    }
}
