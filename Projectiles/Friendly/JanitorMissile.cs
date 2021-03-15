using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CastledsContent.Projectiles.Friendly
{
    public class JanitorMissile : ModProjectile
    {
        private int purgeTimer = 0;
        private int lifeTime = 0;
        private int alpha = 0;
        //private NPC[] npcArray = new NPC[0];
        private bool createArray = false;
        private bool isDone = false;
        private bool inPurge = false;
        private bool hitTarget = false;
        private bool lifeTimeQuota;
        private int hits = 0;
        private int animation = 0;
        private NPC currentTarget;
        //public NPC owner;
        private float velocity = 1.75f;
        //private bool arrayDone = false;
        //private List<int> players = new List<int>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Micro-Warp Missile");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = 1;
            projectile.width = 6;
            projectile.height = 22;
            projectile.alpha = 0;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 60;
            projectile.velocity.X = velocity;
            projectile.velocity.Y = velocity;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.netUpdate = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            animation++;
            if (animation > 8)
            {
                animation = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            if (!isDone)
                projectile.timeLeft = 60;
            if (!lifeTimeQuota)
            {
                lifeTime++;
                if (lifeTime > 30)
                    lifeTimeQuota = true;
            }
            if (lifeTimeQuota)
            {
                if (hitTarget)
                {
                    hitTarget = false;
                    createArray = false;
                    NewTarget();
                }
                //if (hasTarget)
                    //QuantumPurge(currentTarget);
                if (alpha < 256)
                {
                    alpha += 5;
                    projectile.alpha = alpha;
                }
                if (alpha > 255 || alpha == 255)
                {
                    if (!createArray)
                    {
                        for (int a = 0; a < Main.npc.Length; a++)
                            if (Main.npc[a].HasValidTarget && Main.npc[a].active && Main.npc[a].type != NPCID.TargetDummy && InRange(Main.npc[a]))
                                currentTarget = Main.npc[a];
                        createArray = true;
                        //CreateArray();
                    }
                    projectile.alpha = 255;
                    //projectile.velocity = new Vector2(0f, 0f);
                    if (currentTarget != null && currentTarget.active && InRange(currentTarget))
                    {
                        //currentTarget = Main.rand.Next(npcArray);
                        GoToPosition(currentTarget);

                    }
                    else
                        projectile.Kill();
                    if (currentTarget != null && currentTarget.active)
                        alpha = 0;
                }
                #region
                /*
                void CreateArray()
                {
                    NPC[] hostiles = new NPC[Main.npc.Length];
                    int size = 0;
                    int num = 0;
                    for (int a = 0; a < Main.npc.Length; a++)
                        hostiles[a] = new NPC();
                    for (int a = 0; a < Main.npc.Length; a++)
                    {
                        if (Main.npc[a].active && Main.npc[a].width > 0 && !Main.npc[a].friendly)
                        {
                            //size++;
                            hostiles[a] = Main.npc[a];
                            //Main.NewText(Main.npc[a].type);
                        }
                    }
                    for (int a = 0; a < Main.npc.Length; a++)
                    {
                        if (Main.npc[a].width > 0)
                        {
                            hostiles[num] = hostiles[a];
                            //hostiles[a] = new NPC();
                            Main.NewText($"{hostiles[num].type} {hostiles[a].type}");
                        }
                        num++;
                    }
                    for (int a = 0; a < Main.npc.Length; a++)
                    {
                        if (Main.npc[a].width > 0)
                            size++;
                    }
                    Array.Resize(ref hostiles, size);
                    Array.Resize(ref npcArray, size);
                    for (int a = 0; a < hostiles.Length; a++)
                        npcArray[a] = hostiles[a];
                }*/
                #endregion
                void GoToPosition(NPC target)
                {
                    DustPortal(DustID.PortalBolt, 3, Color.SkyBlue);
                    Main.PlaySound(SoundID.Item109.WithVolume(0.75f), projectile.position);
                    projectile.position = NewPosition();
                    //hasTarget = true;
                    if (currentTarget.active)
                        QuantumPurge(target);
                    else
                        NewTarget();
                    Vector2 NewPosition()
                    {
                        float x1 = target.position.X - target.frame.Width;
                        float x2 = target.position.X + target.frame.Width;
                        float y1 = target.position.Y - target.frame.Height;
                        float y2 = target.position.Y + target.frame.Height;
                        return new Vector2(Main.rand.NextFloat(x1, x2), Main.rand.NextFloat(y1, y2));
                    }
                }
                void QuantumPurge(NPC target)
                {
                    purgeTimer++;
                    int GetDelay()
                    {
                        if (hits < 1)
                            return 200;
                        else
                            return 200 / hits;
                    }
                    if (purgeTimer > GetDelay() && !inPurge)
                    {
                        DustPortal(DustID.PortalBolt, 2, Color.SkyBlue);
                        inPurge = true;
                        projectile.alpha = 0;
                        projectile.velocity.X = velocity;
                        projectile.velocity.Y = velocity;
                        #region stuff
                        Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                        projectile.rotation = (float)Math.Atan2(vector8.Y - (target.position.Y + (target.height * 0.5f)), vector8.X - (target.position.X + (target.width * 0.5f)));
                        projectile.ai[1] = 0;
                        #endregion
                        if (currentTarget.active)
                        purgeTimer = 0;
                    }
                    if (purgeTimer > 90 && inPurge)
                    {
                        projectile.alpha += 5;
                        //if (projectile.alpha > 254)
                        createArray = false;
                        NewTarget();
                    }
                }
                void NewTarget()
                {
                    DustPortal(DustID.PortalBolt, 3, Color.OrangeRed);
                    inPurge = false;
                    //hasTarget = false;
                    velocity *= 1.05f;
                    projectile.alpha = 255;
                    //projectile.velocity = new Vector2(0f, 0f);
                    if (hits > 10 || !currentTarget.active || !InRange(currentTarget))
                    {
                        velocity = 1.75f;
                        hits = 0;
                        //currentTarget = new NPC();
                        if (!createArray)
                        {
                            createArray = true;
                            for (int a = 0; a < Main.npc.Length; a++)
                                if (Main.npc[a].HasValidTarget && Main.npc[a].active && Main.npc[a].type != NPCID.TargetDummy && InRange(Main.npc[a]))
                                    currentTarget = Main.npc[a];
                            GoToPosition(currentTarget);
                            //CreateArray();
                        }
                    }
                    else if (currentTarget.active || currentTarget.life > 0)
                    {
                        GoToPosition(currentTarget);
                    }
                    else
                        projectile.Kill();

                }
                bool InRange(NPC target)
                {
                    return target.position.X < projectile.GetGlobalProjectile<MissileClass>().range2.X && target.position.X > projectile.GetGlobalProjectile<MissileClass>().range1.X && target.position.Y < projectile.GetGlobalProjectile<MissileClass>().range2.Y && target.position.Y > projectile.GetGlobalProjectile<MissileClass>().range1.Y;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target == currentTarget)
            {
                hitTarget = true;
                hits++;
            }
        }
        private void DustPortal(int ID, float velocity, Color color)
        {
            for (int i = 0; i < 20; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 20 * i));
                Dust dust = Dust.NewDustPerfect(position, ID);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * velocity;
                dust.noLight = false;
                dust.fadeIn = 1f;
                dust.color = color;
            }
        }
        public override void Kill(int timeLeft)
        {
            NPC owner = projectile.GetGlobalProjectile<MissileClass>().owner;
            DustPortal(DustID.PortalBolt, 2, Color.OrangeRed);
            if (owner != null && owner.active)
            {
                switch (owner.ai[3])
                {
                    case 0:
                        owner.ai[3] = Main.rand.Next(3, 6);
                        break;
                    case 4:
                        owner.ai[3] = 3;
                        break;
                    case 5:
                        owner.ai[3] = 3;
                        break;
                }
            }
        }
    }
    public class MissileClass : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public NPC owner;
        public Vector2 range1;
        public Vector2 range2;
    }
}
