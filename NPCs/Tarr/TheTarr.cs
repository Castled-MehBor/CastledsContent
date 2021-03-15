using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace CastledsContent.NPCs.Tarr
{
    public class TheTarr : ModNPC
    {
        private Entity target = null;
        //private int targetTimer = 0;
        private bool catchTarget = false;
        public bool replicate = false;
        public int rep = 0;
        public int appen = -1;
        private float mH = 0f;
        private bool hasJumped = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Tarr");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = int.MaxValue;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 58;
            npc.aiStyle = 26;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            //npc.dontTakeDamage = true;
            npc.netAlways = true;
            //npc.alpha = 255;
            npc.HitSound = SoundID.NPCHit1;
            //npc.DeathSound = SoundID.NPCDeath1;
            npc.takenDamageMultiplier = -50f;
        }
        public override void AI()
        {
            npc.direction = 1;
            //targetTimer++;
            if (npc.velocity.Y < 0 && !hasJumped)
                hasJumped = true;
            if (hasJumped && npc.velocity.Y == 0)
                hasJumped = false;
            if (hasJumped && npc.velocity.Y < 0)
            {
                npc.velocity.Y += 4f;
                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Bounce"));
                if (Main.rand.Next(15) == 0)
                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Grunt"));
            }
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RavenousRainbows");
            music = MusicID.GoblinInvasion;
            musicPriority = MusicPriority.BossHigh;
            if (Main.rand.Next(450) == 0)
            {
                int a = Main.rand.Next(2);
                if (a == 0)
                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Idle1"));
                if (a == 1)
                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Idle2"));
            }
            if (appen != -1 && Main.npc[appen].ai[3] == 30)
            {
                catchTarget = false;
                Main.npc[appen].life = int.MinValue;
                appen = -1;
            }
            if (appen != -1 && Main.npc[appen].ai[3] == 21f)
            {
                Main.npc[appen].life = int.MinValue;
                appen = -1;
                replicate = true;
            }
            //npc.aiAction = 69;
            if (target == null || !target.active)
            {
                SetTarget();
            }
            foreach (Item item in Main.item)
            {
                if (ItemNearTarr(item, npc))
                {
                    SGlobalItem gi = item.GetGlobalItem<SGlobalItem>();
                    gi.originalVelo = item.velocity;
                    gi.flingVelo = new Vector2(npc.velocity.X * 1.25f, npc.velocity.Y * 1.25f);
                    gi.flingTime = 45;
                    gi.flinged = true;
                }
            }
            if (target != null && target.active)
            {
                if (Main.rand.Next(750) == 0)
                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Grunt"));
                Vector2 velocity = new Vector2((float)Math.Sin((double)(Math.PI * 2 * mH / 300f)) * 0.5f, 0f);
                mH += 1f;
                velocity = Vector2.UnitX * velocity.Length();
                if (target.position.X > npc.position.X)
                    npc.velocity.X = velocity.X * 4;
                else if (target.position.X < npc.position.X)
                    npc.velocity.X = velocity.X * -4;
                if (catchTarget)
                {
                    npc.velocity.Y = 0f;
                    npc.velocity.X *= 0.5f;
                }
                #region
                /*
                if (!catchTarget)
                    npc.velocity.X = Direction();
                else
                {
                    npc.velocity.Y = 0f;
                    npc.velocity.X *= 0.5f;
                }
                float Direction()
                {
                    if (target.position.X > npc.position.X && target.active)
                        return 4f;
                    else if (target.position.X < npc.position.X && target.active)
                        return -4f;
                    return 0f;
                }
                */
                #endregion
            }
            if (!catchTarget)
                SnatchATarget();
            if (replicate)
            {
                rep++;
                if (rep > 120)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<TheTarr>());
                    rep = 0;
                    catchTarget = false;
                    replicate = false;
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore1"), 1f);
                    for (int a = 0; a < 3; a++)
                        Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore2"), 1f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore3"), 1f);
                    //int newTarr = NPC.NewNPC()
                }
            }
            if (npc.HasBuff(BuffID.Wet) || npc.wet)
                npc.takenDamageMultiplier = 250000f;
            else
            {
                npc.life = npc.lifeMax;
                npc.takenDamageMultiplier = -50f;
            }
            if (appen != -1 && Main.npc[appen].ai[3] == 69f)
            {
                catchTarget = false;
                replicate = false;
                Main.npc[appen].life = int.MinValue;
                appen = -1;
                replicate = true;
            }
            bool ItemNearTarr(Item item, NPC npc)
            {
                Vector2 high = new Vector2(npc.position.X + 50, npc.position.Y + 75);
                Vector2 low = new Vector2(npc.position.X - 50, npc.position.Y - 50);
                return item.position.X > low.X && item.position.X < high.X && item.position.Y > low.Y && item.position.Y < high.Y;
            }
        }
        private void SetTarget()
        {
            target = new NPC();
            List<Entity> targets = new List<Entity>();
            Vector2 min = new Vector2(npc.position.X - 1500, npc.position.Y - 1500);
            Vector2 max = new Vector2(npc.position.X + 1500, npc.position.Y + 1500);
            for (int a = 0; a < Main.npc.Length; a++)
            {
                if (InRange(min, max, 1, a) && Main.npc[a].type != NPCID.TargetDummy && Main.npc[a].type != ModContent.NPCType<TheTarr>() && Main.npc[a].type != ModContent.NPCType<Appendage>())
                    targets.Add(Main.npc[a]);
            }
            for (int a = 0; a < Main.player.Length; a++)
            {
                if (InRange(min, max, 2, a) && !HasSmite(Main.player[a]))
                    targets.Add(Main.player[a]);
            }
            if (targets.Count > 0)
                target = Main.rand.Next(targets);
            foreach (Entity e in targets)
            {
                if (e is Player p && IsRancher(p))
                    target = p;
            }
        }
        public bool IsRancher(Player player)
        {
            if (player.name == "Beatrix LeBeau" && !player.Male && !HasSmite(player))
                return true;
            else
                return false;
        }
        public bool HasSmite(Player player)
        {
            IncPlayer inc = player.GetModPlayer<IncPlayer>();
            if (inc.godMode)
                return true;
            return false;
        }
        private void SnatchATarget()
        {
            List<Entity> targets = new List<Entity>();
            Vector2 min = new Vector2(npc.position.X - 750, npc.position.Y - 750);
            Vector2 max = new Vector2(npc.position.X + 750, npc.position.Y + 750);
            for (int a = 0; a < Main.npc.Length; a++)
            {
                if (InRange(min, max, 1, a) && Main.npc[a].type != NPCID.TargetDummy && Main.npc[a].type != ModContent.NPCType<TheTarr>() && Main.npc[a].type != ModContent.NPCType<Appendage>())
                    targets.Add(Main.npc[a]);
            }
            for (int a = 0; a < Main.player.Length; a++)
            {
                if (InRange(min, max, 2, a) && !HasSmite(Main.player[a]) && Main.player[a].active)
                    targets.Add(Main.player[a]);
            }
            if (targets.Count > 0)
            {
                catchTarget = true;
                try
                {
                    target = null;
                    int appendage = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<Appendage>());
                    TarrHelper th = Main.npc[appendage].GetGlobalNPC<TarrHelper>();
                    th.parentTarr = npc;
                    th.snaredTarget = Main.rand.Next(targets);
                    foreach (Entity e in targets)
                    {
                        if (e is Player p && IsRancher(p))
                            target = p;
                    }
                    Main.npc[appendage].rotation = th.snaredTarget.position.ToRotation();
                    appen = appendage;
                    Entity targ = Main.npc[appendage].GetGlobalNPC<TarrHelper>().snaredTarget;
                    if (targ is NPC tar)
                        tar.GetGlobalNPC<TarrHelper>().isSnared = true;
                    if (targ is Player pla)
                        pla.GetModPlayer<SnareBoolean>().isSnared = true;
                }
                catch (IndexOutOfRangeException)
                {
                    if (target is NPC)
                    {
                        NPC targetN = (NPC)Main.rand.Next(targets);
                        targetN.life = int.MinValue;
                    }
                    if (target is Player)
                    {
                        Player targetP = (Player)Main.rand.Next(targets);
                        PlayerDeathReason dR = new PlayerDeathReason
                        {
                            SourceCustomReason = $"{targetP.name} was in a sticky, and unavoidable situation..."
                        };
                        targetP.KillMe(dR, double.MaxValue, 0, false);
                    }
                }
            }
            //TODO: create Appendage NPC
        }
        /// <summary>
        /// Type 1 - NPC, Type 2 - Player
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool InRange(Vector2 min, Vector2 max, int type, int index)
        {
            switch (type)
            {
                case 1:
                    {
                        if (min.X < Main.npc[index].position.X && max.X > Main.npc[index].position.X && min.Y < Main.npc[index].position.Y && max.Y > Main.npc[index].position.Y && Main.npc[index].active)
                            return !Main.npc[index].GetGlobalNPC<TarrHelper>().isSnared;
                    }
                    break;
                case 2:
                    {
                        if (min.X < Main.player[index].position.X && max.X > Main.player[index].position.X && min.Y < Main.player[index].position.Y && max.Y > Main.player[index].position.Y && !Main.player[index].dead)
                            return !Main.player[index].GetModPlayer<SnareBoolean>().isSnared;
                    }
                    break;
            }
            return false;
        }
        public override bool CheckActive() => false;
        public override bool CheckDead()
        {
            if (npc.HasBuff(BuffID.Wet) || npc.wet || Main.player[0].name == "Beatrix LeBeau")
            {
                npc.NPCLoot();
                return true;
            }
            else
            {
                npc.NPCLoot();
                npc.life = npc.lifeMax;
                return false;
            }
        }
        public override void NPCLoot()
        {
            if (target != null && target.active)
            {
                if (target is NPC tar)
                {
                    TarrHelper th = tar.GetGlobalNPC<TarrHelper>();
                    th.isSnared = false;
                    th.grasped = false;
                }
                if (target is Player pl)
                {
                    SnareBoolean sb = pl.GetModPlayer<SnareBoolean>();
                    sb.isSnared = false;
                    sb.grasped = false;
                }
            }
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore1"), 1f);
            for (int a = 0; a < 6; a++)
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore4"), 1f);
            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Death"));
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (npc.takenDamageMultiplier == -50)
                return false;
            else
                return base.DrawHealthBar(hbPosition, ref scale, ref position);
        }
        public override void FindFrame(int frameHeight)
        {
            int y = 0;
            if (!replicate)
            {
                y = 58;
                if (npc.velocity.Y != 0)
                    y = 174;
            }
            if (appen != -1)
            {
                if (catchTarget)
                {
                    if (DistanceForMunch(1, Main.npc[appen]))
                        y = 116;
                    if (DistanceForMunch(2, Main.npc[appen]))
                        y = 174;
                }
            }
            if (replicate)
            {
                if (rep <= 45)
                {
                    y = 0;
                    npc.scale += 0.005f;
                }
                else if (rep <= 60)
                {
                    y = 58;
                    npc.scale += 0.010f;
                }
                else if (rep <= 75)
                {
                    y = 116;
                    npc.scale += 0.015f;
                    if (npc.scale <= 1.5f)
                        npc.scale = 1.5f;
                }
                else if (rep <= 105)
                {
                    npc.scale -= 0.05f;
                    y = 174;
                }
                else if (rep <= 120)
                {
                    y = 116;
                    npc.scale += 0.25f;
                    if (npc.scale > 1)
                        npc.scale = 1;
                }
                else
                {
                    y = 58;
                    npc.scale = 1;
                }
                if (npc.scale < 1)
                    npc.scale = 1;
            }
            bool DistanceForMunch(int type, NPC n)
            {
                switch (type)
                {
                    case 1:
                        {
                            Vector2 vec1 = new Vector2(npc.position.X + 150, npc.position.Y + 150);
                            Vector2 vec2 = new Vector2(npc.position.X - 150, npc.position.Y - 150);
                            return n.position.X < vec1.X && n.position.X > vec2.X && n.position.Y < vec1.Y && n.position.Y > vec2.Y;
                        }
                    case 2:
                        {
                            Vector2 vec1 = new Vector2(npc.position.X + 75, npc.position.Y + 75);
                            Vector2 vec2 = new Vector2(npc.position.X - 75, npc.position.Y - 75);
                            return n.position.X < vec1.X && n.position.X > vec2.X && n.position.Y < vec1.Y && n.position.Y > vec2.Y;
                        }
                }
                return false;
            }
            Rectangle frame = new Rectangle(0, y, 50, 58);
            npc.frame = frame;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 offset = new Vector2(25, 25f);
            if (replicate)
                offset = new Vector2(25, 25 * npc.scale);
            Color tarrColor1 = new Color(Main.DiscoR - 15, Main.DiscoG - 175, Main.DiscoB - 85);
            Color tarrColor2 = new Color(Main.DiscoR - 145, Main.DiscoG - 45, Main.DiscoB - 12);
            Texture2D face = ModContent.GetTexture("CastledsContent/NPCs/Tarr/Textures/TarrFace");
            Texture2D glow1 = ModContent.GetTexture("CastledsContent/NPCs/Tarr/Textures/TarrGlow1");
            Texture2D glow2 = ModContent.GetTexture("CastledsContent/NPCs/Tarr/Textures/TarrGlow2");
            spriteBatch.Draw(face, npc.Center - Main.screenPosition, npc.frame, drawColor * 8f, 0f, offset, npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(glow1, npc.Center - Main.screenPosition, npc.frame, tarrColor1 * 2.25f, 0f, offset, npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(glow2, npc.Center - Main.screenPosition, npc.frame, tarrColor2 * 2.25f, 0f, offset, npc.scale, SpriteEffects.None, 0f);
        }
    }
    public class Appendage : ModNPC
    {
        private bool gripTarget = false;
        private bool wet = false;
        private float moveMult = 1f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarr's Grasp");
        }
        public override void SetDefaults()
        {
            npc.lifeMax = int.MaxValue;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = -1;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            //npc.alpha = 255;
            npc.HitSound = SoundID.NPCHit1;
            //npc.DeathSound = SoundID.NPCDeath1;
        }
        public override void AI()
        {
            Entity target = npc.GetGlobalNPC<TarrHelper>().snaredTarget;
            NPC tarr = npc.GetGlobalNPC<TarrHelper>().parentTarr;
            bool Manipulation()
            {
                if (target is Player p)
                {
                    IncPlayer inc = p.GetModPlayer<IncPlayer>();
                    if (inc.pTar || inc.nTar)
                        return true;
                }
                return false;
            }
            if (tarr != null && tarr.active)
                npc.life = npc.lifeMax;
            else
            {
                npc.life = int.MinValue;
                npc.checkDead();
            }
            if (npc.HasBuff(BuffID.Wet))
            {
                npc.ai[3] = 30f;
                wet = true;
                npc.life = int.MinValue;
                npc.checkDead();
            }
            if (!npc.active)
            {
                if (target is Player)
                {
                    Player pt = (Player)npc.GetGlobalNPC<TarrHelper>().snaredTarget;
                    pt.GetModPlayer<SnareBoolean>().isSnared = false;
                    pt.GetModPlayer<SnareBoolean>().grasped = false;
                }
                if (target is NPC n)
                {
                    n.GetGlobalNPC<TarrHelper>().drawArm = false;
                    n.GetGlobalNPC<TarrHelper>().armRot = 0f;
                    n.rotation = 0f;
                }
            }
            if (gripTarget)
            {
                if (target == null || !target.active)
                {
                    if (tarr == null || !tarr.active)
                    {
                        npc.life = int.MinValue;
                        npc.checkDead();
                    }
                    else
                        npc.ai[3] = 69f;
                }

                target.position = new Vector2(npc.Center.X - (target.width / 2), npc.Center.Y - (target.height / 2));
                target.velocity = new Vector2(0f, 0f);
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                if (target is NPC n)
                {
                    n.GetGlobalNPC<TarrHelper>().drawArm = true;
                    n.rotation = npc.position.ToRotation();
                    n.GetGlobalNPC<TarrHelper>().armRot = n.rotation;
                    n.frame = new Rectangle(0, 0, n.frame.Width, n.frame.Height);
                }
                if (target is Player p)
                {
                    bool extra = p.inventory[p.selectedItem].useStyle != 0 && Main.mouseLeft;
                    bool extra2 = p.inventory[p.selectedItem].useStyle != 0 && p.altFunctionUse > 0 && Main.mouseRight;
                    p.GetModPlayer<SnareBoolean>().grasped = true;
                    p.tileInteractionHappened = false;
                    if (!p.GetModPlayer<SnareBoolean>().useGrasp && !extra && !extra2)
                        p.bodyFrame.Y = 0;
                    p.ItemCheck(0);
                    if (p.position.X < 650)
                        p.position.X = 650;
                    if (p.position.X > 100000)
                        p.position.X = 100000;
                    if (p.position.Y < 650)
                        p.position.Y = 650;
                    if (p.position.Y > 28000)
                        p.position.Y = 28000;
                }
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (tarr.position.Y + (tarr.height * 0.5f)), (vector8.X) - (tarr.position.X + (tarr.width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * moveMult) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * moveMult) * -1;
                }
                if (NextToParentTarr())
                {
                    if (target is NPC a)
                    {
                        NPC npcTarget = (NPC)npc.GetGlobalNPC<TarrHelper>().snaredTarget;
                        TarrHelper th = npcTarget.GetGlobalNPC<TarrHelper>();
                        th.grasped = true;
                        th.Consumed(npcTarget);
                        npc.ai[3] = 21f;
                        a.GetGlobalNPC<TarrHelper>().drawArm = false;
                        a.GetGlobalNPC<TarrHelper>().armRot = 0f;
                        npcTarget.rotation = 0f;
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Bite"));
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Consume"));
                    }
                    if (target is Player)
                    {
                        Player pt = (Player)npc.GetGlobalNPC<TarrHelper>().snaredTarget;
                        List<string> messages = new List<string>()
                        {
                            "was eaten without the crust.",
                            "turned into a terrarian chicken nugget.",
                            "is currently approaching the intestines...",
                            "didn't know how to wrangle, and ended up mangled.",
                            "forgot to bring some water.",
                            "came too close to the exhibit",
                            "isn't a god."
                        };
                        if (pt.name == "Beatrix LeBeau" && !pt.Male)
                            messages.Add("was knocked out.");
                        PlayerDeathReason dR = new PlayerDeathReason
                        {
                            SourceCustomReason = $"{pt.name} {Main.rand.Next(messages)}"
                        };
                        pt.KillMe(dR, double.MaxValue, 0, false);
                        npc.ai[3] = 21f;
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Bite"));
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Consume"));
                        pt.GetModPlayer<SnareBoolean>().isSnared = false;
                        pt.GetModPlayer<SnareBoolean>().grasped = false;
                    }
                }
                if (!Manipulation() && moveMult <= 35)
                    moveMult *= 1.01f;
            }
            if (!gripTarget)
            {
                if (target == null || !target.active)
                {
                    if (tarr == null || !tarr.active)
                    {
                        npc.life = int.MinValue;
                        npc.checkDead();
                    }
                    else
                        npc.ai[3] = 69f;
                }
                else
                {
                    npc.velocity.X *= 0.98f;
                    npc.velocity.Y *= 0.98f;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    {
                        float rotation = (float)Math.Atan2((vector8.Y) - (target.position.Y + (target.height * 0.5f)), (vector8.X) - (target.position.X + (target.width * 0.5f)));
                        npc.velocity.X = (float)(Math.Cos(rotation) * moveMult) * -1;
                        npc.velocity.Y = (float)(Math.Sin(rotation) * moveMult) * -1;
                    }
                    if (NextToTarget())
                    {
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tarr_Grab"));
                        moveMult = 1f;
                        gripTarget = true;
                    }
                    if (moveMult <= 50)
                        moveMult *= 1.025f;
                }
            }
        }
        public bool NextToTarget()
        {
            Entity target = npc.GetGlobalNPC<TarrHelper>().snaredTarget;
            if (target.position.X + 50 > npc.position.X && target.position.X - 50 < npc.position.X && target.position.Y + 50 > npc.position.Y && target.position.Y - 50 < npc.position.Y)
            {
                if (target is NPC t)
                    Main.PlaySound(t.HitSound, t.position);
                if (target is Player p)
                {
                    if (p.Male)
                        Main.PlaySound(SoundID.PlayerHit, npc.position);
                    else
                        Main.PlaySound(SoundID.FemaleHit, npc.position);
                }
                return true;
            }
            return false;
        }
        public bool NextToParentTarr()
        {
            NPC tarr = npc.GetGlobalNPC<TarrHelper>().parentTarr;
            if (tarr.position.X + 25 > npc.position.X && tarr.position.X - 25 < npc.position.X - 25 && tarr.position.Y + 25 > npc.position.Y && tarr.position.Y - 25 < npc.position.Y)
                return true;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            NPC tarr = npc.GetGlobalNPC<TarrHelper>().parentTarr;
            Color tarrColor1 = new Color(Main.DiscoR - 15, Main.DiscoG - 175, Main.DiscoB - 85);
            Color tarrColor2 = new Color(Main.DiscoR - 145, Main.DiscoG - 45, Main.DiscoB - 12);
            if (tarr != null && tarr.active && npc.ai[3] != 21)
            {
                Vector2 tarrOrigin = new Vector2(tarr.Center.X, tarr.Center.Y);
                Vector2 center = npc.Center;
                Vector2 distToOrig = tarrOrigin - npc.Center;
                float projRotation = distToOrig.ToRotation() - 1.57f;
                float distance = distToOrig.Length();
                while (distance > 30f && !float.IsNaN(distance))
                {
                    Color shade = Lighting.GetColor((int)center.X / 16, (int)center.Y / 16);
                    distToOrig.Normalize();
                    distToOrig *= 30f;
                    center += distToOrig;
                    distToOrig = tarrOrigin - center;
                    distance = distToOrig.Length();

                    spriteBatch.Draw(mod.GetTexture("NPCs/Tarr/Textures/AppendageTex"), center - Main.screenPosition,
                        new Rectangle(0, 0, 14, 36), shade, projRotation,
                        new Vector2(14 * 0.5f, 36 * 0.5f), 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(mod.GetTexture("NPCs/Tarr/Textures/AppendageTex_Glow"), center - Main.screenPosition,
                        new Rectangle(0, 0, 14, 36), tarrColor1, projRotation,
                        new Vector2(14 * 0.5f, 36 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
            }
            if (!gripTarget)
            {
                Texture2D appen = mod.GetTexture("NPCs/Tarr/Textures/Appendage_Glow");
                spriteBatch.Draw(appen, npc.position - Main.screenPosition, new Rectangle(npc.frame.X, npc.frame.Y, appen.Width, appen.Height), tarrColor2, npc.rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                return true;
            }
            else
                return false;
        }
        public override void NPCLoot()
        {
            Entity target = npc.GetGlobalNPC<TarrHelper>().snaredTarget;
            if (target is NPC n)
            {
                TarrHelper th = n.GetGlobalNPC<TarrHelper>();
                th.grasped = false;
                th.isSnared = false;
                th.drawArm = false;
                th.armRot = 0f;
                n.rotation = 0f;
            }
            if (target is Player p)
            {
                p.GetModPlayer<SnareBoolean>().isSnared = false;
                if (!wet)
                    p.GetModPlayer<SnareBoolean>().grasped = false;
            }
            if (npc.ai[3] < 1 || wet)
            {
                Main.PlaySound(SoundID.NPCDeath12, npc.position);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore1"), 1f);
                for (int a = 0; a < 2; a++)
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore2"), 1f);
            }
        }
        public override bool CheckDead()
        {
            NPC tarr = npc.GetGlobalNPC<TarrHelper>().parentTarr;
            if (tarr != null && tarr.active && !wet)
            {
                npc.life = npc.lifeMax;
                return false;
            }
            return true;
        }
        public override bool CheckActive() => false;
    }
    public class TarrHelper : GlobalNPC
    {
        public NPC parentTarr;
        public Entity snaredTarget;
        public bool grasped;
        public bool isSnared;
        public bool drawArm;
        public float armRot;
        public override bool InstancePerEntity => true;
        public void Consumed(NPC npc)
        {
            npc.life = int.MinValue;
            npc.HitEffect();
            npc.checkDead();
        }
        public override bool CheckDead(NPC npc)
        {
            if (grasped)
            {
                isSnared = false;
                return true;
            }
            isSnared = false;
            return base.CheckDead(npc);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (drawArm)
            {
                Color tarrColor2 = new Color(Main.DiscoR - 145, Main.DiscoG - 45, Main.DiscoB - 12);
                Texture2D tex = ModContent.GetTexture("CastledsContent/NPCs/Tarr/Appendage");
                Texture2D texGlow = ModContent.GetTexture("CastledsContent/NPCs/Tarr/Textures/Appendage_Glow");
                spriteBatch.Draw(tex, npc.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), drawColor, armRot, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(texGlow, npc.Center - Main.screenPosition, new Rectangle(npc.frame.X, npc.frame.Y, texGlow.Width, texGlow.Height), tarrColor2, armRot, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            }
        }
    }
    public class SnareBoolean : ModPlayer
    {
        public bool isSnared;
        public bool grasped;
        public bool useGrasp;
        public override void ResetEffects()
        {
            useGrasp = false;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (isSnared || grasped)
                return true;
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void UpdateDead()
        {
            isSnared = false;
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (grasped)
            {
                bool extra = player.inventory[player.selectedItem].useStyle != 0 && Main.mouseLeft;
                bool extra2 = player.inventory[player.selectedItem].useStyle != 0 && player.altFunctionUse > 0 && Main.mouseRight;
                if (useGrasp || !isSnared || extra || extra2)
                {
                    drawInfo.drawArms = true;
                    drawInfo.drawHands = true;
                }
            }
        }
        public override void FrameEffects()
        {
            if (grasped)
            {
                bool extra = player.inventory[player.selectedItem].useStyle != 0 && Main.mouseLeft;
                bool extra2 = player.inventory[player.selectedItem].useStyle != 0 && player.altFunctionUse > 0 && Main.mouseRight;
                if (isSnared && !useGrasp && !extra && !extra2)
                    player.body = mod.GetEquipSlot("TarrArm", EquipType.Body);
                else
                    player.body = mod.GetEquipSlot("TarrArmBreached", EquipType.Body);
            }
        }
    }
}