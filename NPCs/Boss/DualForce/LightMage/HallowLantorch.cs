using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Boss.DualForce.LightMage
{
	public class HallowLantorch : ModNPC
	{
		public bool hasOpened = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Lantorch");
			Main.npcFrameCount[npc.type] = 9;
		}

		public override void SetDefaults()
		{
			npc.width = 22;
			npc.height = 44;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 50;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 60f;
			npc.dontTakeDamage = true;
			npc.noGravity = true;
			npc.knockBackResist = 1f;
			aiType = -1;
		}
		public float Timer
		{
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
		}

		public override void AI()
		{
			npc.ai[0]++;
			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
			}
			npc.netUpdate = true;
			{
				if (hasOpened == true)
                {
					Timer++;
					if (Timer == 60)
					{
						float Speed = 44;
						Vector2 vector8 = new Vector2(npc.position.X + (npc.width * Main.rand.Next(1, 5)), npc.position.Y * Main.rand.Next(1, 5));
						int damage = 20;
						int type = (mod.ProjectileType("HallowFireball"));
						Main.PlaySound(SoundID.Grass, (int)npc.position.X, (int)npc.position.Y, 17);
						float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
						npc.ai[1] = 0;
					}
					if (Timer == 200)
					{
						float Speed = 24f;
						Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
						int damage = 20;
						int type = (mod.ProjectileType("HallowFireball"));
						Main.PlaySound(SoundID.Grass, (int)npc.position.X, (int)npc.position.Y, 17);
						float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
						npc.ai[1] = 0;
					}
					if (Timer == 320)
					{
						float Speed = 28f;
						Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
						int damage = 20;
						int type = (mod.ProjectileType("HallowFireball"));
						Main.PlaySound(SoundID.Grass, (int)npc.position.X, (int)npc.position.Y, 17);
						float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
						npc.ai[1] = 0;
					}
					if (Timer == 400)
					{
						float Speed = 34f;
						Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
						int damage = 20;
						int type = (mod.ProjectileType("HallowFireball"));
						Main.PlaySound(SoundID.Grass, (int)npc.position.X, (int)npc.position.Y, 17);
						float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
						npc.ai[1] = 0;
					}
					if (Timer > 425)
					{
						float Speed = 40f;
						Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
						int damage = 20;
						int type = (mod.ProjectileType("HallowFireball"));
						Main.PlaySound(SoundID.Grass, (int)npc.position.X, (int)npc.position.Y, 17);
						float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
						npc.ai[1] = 0;
					}
					if (Timer > 425)
					{
						npc.life = 0;
						Main.PlaySound(SoundID.NPCDeath6);
						Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 1.2f);
						Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 1.2f);
						Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 1.2f);
					}
				}
			}
		}

		private const int LanternOpen1 = 0;
		private const int LanternOpen2 = 1;
		private const int LanternOpen3 = 2;
		private const int LanternOpen4 = 3;
		private const int LanternOpen5 = 4;
		private const int LanternOpen6 = 5;
		//Animation Frames
		private const int Levitate1 = 6;
		private const int Levitate2 = 7;
		private const int Levitate3 = 8;
		public override void FindFrame(int frameHeight)
		{
			if (hasOpened == false)
			{
				npc.frameCounter++;
				if (npc.frameCounter < 10)
				{
					npc.frame.Y = LanternOpen1 * frameHeight;
				}
				else if (npc.frameCounter < 20)
				{
					npc.frame.Y = LanternOpen2 * frameHeight;
				}
				else if (npc.frameCounter < 30)
				{
					npc.frame.Y = LanternOpen3 * frameHeight;
				}
				else if (npc.frameCounter < 40)
				{
					npc.frame.Y = LanternOpen4 * frameHeight;
				}
				else if (npc.frameCounter < 50)
				{
					npc.frame.Y = LanternOpen5 * frameHeight;
				}
				else if (npc.frameCounter < 60)
				{
					npc.frame.Y = LanternOpen6 * frameHeight;
				}
				else
				{
					npc.frameCounter = 0;
					hasOpened = true;
				}
				if (hasOpened == true)
				{
					npc.frameCounter++;
					if (npc.frameCounter < 10)
					{
						npc.frame.Y = Levitate1 * frameHeight;
					}
					else if (npc.frameCounter < 20)
					{
						npc.frame.Y = Levitate2 * frameHeight;
					}
					else if (npc.frameCounter < 30)
					{
						npc.frame.Y = Levitate3 * frameHeight;
					}
					else if (npc.frameCounter < 40)
					{
						npc.frame.Y = Levitate2 * frameHeight;
					}
					else if (npc.frameCounter < 50)
					{
						npc.frame.Y = Levitate1 * frameHeight;
					}
					{
						npc.frameCounter = 0;
					}
				}
			}
		}
	}
}