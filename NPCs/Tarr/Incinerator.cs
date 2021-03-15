using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Terraria.DataStructures;

namespace CastledsContent.NPCs.Tarr
{
    public class Incinerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Ancient Developer Tablet");
            Tooltip.SetDefault("'Unlimited Power!'\nBind Special Hotkey before using this\nWhile in your inventory, grants you godmode\nPressing Special hotkey:\n- Holding: Kills every non-friendly NPC\n- Not Holding: Toggles *Magic Cursor\n*Grab NPCs, Items or Players, and damage/delete them by holding right-click");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.rare = ItemRarityID.Red;
        }
        public override void UpdateInventory(Player player)
        {
            IncPlayer ic = Main.player[Main.myPlayer].GetModPlayer<IncPlayer>();
            if (Press(1))
            {
                for (int a = 0; a < Main.npc.Length; a++)
                {
                    NPC n = Main.npc[a];
                    if (n != null && n.active && (!n.friendly || !n.townNPC))
                    {
                        if (n.type != NPCID.TargetDummy)
                        {
                            n.life = int.MinValue;
                            n.HitEffect();
                            n.checkDead();
                            if (Main.netMode == NetmodeID.Server)
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, a, 0f, 0f, 0f, 0);
                        }
                    }
                }
            }
            if (Press(2))
            {
                ic.magicCursor = !ic.magicCursor;
                Terraria.Audio.LegacySoundStyle type = ic.magicCursor ? SoundID.Item103 : SoundID.Item104;
                Main.PlaySound(type, player.position);
                int dustType = ic.magicCursor ? DustID.AncientLight : DustID.ApprenticeStorm;
                for (int i = 0; i < 15; i++)
                {
                    Vector2 position = player.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i));
                    Dust dust = Dust.NewDustPerfect(position, dustType);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(dust.position - player.Center) * 4;
                    dust.fadeIn = 1f;
                }
            }

            bool Press(int type)
            {
                if (type == 1)
                    return CastledsContent.SpecialHotkey.JustPressed && player.HeldItem == item;
                if (type == 2)
                    return CastledsContent.SpecialHotkey.JustPressed && player.HeldItem != item;
                return false;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(202, 98, 42);
                }
            }
            int num = -1;
            int num2 = 0;
            while (num2 < list.Count)
            {
                if (!list[num2].Name.Equals("ItemName"))
                {
                    num2++;
                    continue;
                }
                num = num2;
                break;
            }
            list[num + 1].overrideColor = Main.DiscoColor;
        }
    }
    public class IncPlayer : ModPlayer
    {
        public bool magicCursor = false;
        public bool manipulate = false;
        public bool pTar = false;
        public bool nTar = false;
        public bool godMode = false;
        public bool passiveHold = false;
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (godMode)
                return false;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (godMode)
                return false;
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            bool HasTablet()
            {
                foreach(Item i in player.inventory)
                {
                    if (i.type == ModContent.ItemType<Incinerator>())
                        return true;
                }
                return false;
            }
            godMode = HasTablet();
            if (godMode)
            {
                if (ModContent.GetInstance<ClientConfig>().tabletUI)
                    CastledsContent.instance.TabletState.SetState(CastledsContent.instance.tabletUI);
                for (int a = 0; a < BuffLoader.BuffCount; a++)
                {
                    if (Main.debuff[a])
                        player.buffImmune[a] = true;
                }
                player.statLife = player.statLifeMax2;
                player.statMana = player.statManaMax2;
                player.breath = player.breathMax;
                player.wingTime = player.wingTimeMax;
            }
            else
                CastledsContent.instance.TabletState.SetState(null);
            if (magicCursor)
            {
                foreach (NPC n in Main.npc)
                {
                    if (n.active && !pTar)
                    {
                        IncNPC inc = n.GetGlobalNPC<IncNPC>();
                        if (inc.InRangeOfCursor(n) && Main.mouseLeft)
                        {
                            inc.manipulate = true;
                            nTar = true;
                        }
                        if (inc.manipulate && Main.mouseLeft)
                        {
                            Vector2 position = new Vector2(Main.MouseWorld.X - (n.width / 2), Main.MouseWorld.Y - (n.height / 2));
                            n.velocity = new Vector2(0f, 0f);
                            n.position = position;
                            if (Main.mouseRight)
                                inc.Damaged(n);
                        }
                        if (!Main.mouseLeft && inc.manipulate)
                        {
                            nTar = false;
                            inc.manipulate = false;
                        }
                    }
                }
                foreach (Player p in Main.player)
                {
                    if (p.active && !p.dead && !nTar)
                    {
                        IncPlayer inc = p.GetModPlayer<IncPlayer>();
                        if (inc.InRangeOfCursor(p) && Main.mouseLeft)
                        {
                            inc.manipulate = true;
                            pTar = true;
                        }
                        if (inc.manipulate && Main.mouseLeft)
                        {
                            SnareBoolean sb = p.GetModPlayer<SnareBoolean>();
                            Vector2 position = new Vector2(Main.MouseWorld.X - (p.width / 2), Main.MouseWorld.Y - (p.height / 2));
                            p.velocity = new Vector2(0f, 0f);
                            p.position = position;
                            if (Main.mouseRight)
                                inc.Damaged(p);
                        }
                        if (!Main.mouseLeft && inc.manipulate)
                        {
                            pTar = false;
                            inc.manipulate = false;
                        }
                    }
                }
                foreach (Item i in Main.item)
                {
                    if (i.active)
                    {
                        IncItem inc = i.GetGlobalItem<IncItem>();
                        if (inc.InRangeOfCursor(i) && Main.mouseLeft)
                            inc.manipulate = true;
                        if (inc.manipulate && Main.mouseLeft)
                        {
                            Vector2 position = new Vector2(Main.MouseWorld.X - (i.width / 2), Main.MouseWorld.Y - (i.height / 2));
                            i.velocity = new Vector2(0f, 0f);
                            i.position = position;
                            if (Main.mouseRight && !inc.smiteImmune)
                                i.SetDefaults(ItemID.None);
                        }
                        if (!Main.mouseLeft && inc.manipulate)
                            inc.manipulate = false;
                    }
                }

            }
        }
        public bool InRangeOfCursor(Player player)
        {
            Vector2 low = new Vector2(player.position.X - player.width, player.position.Y - player.height);
            Vector2 high = new Vector2(player.position.X + player.width, player.position.Y + player.height);
            return high.X > Main.MouseWorld.X && low.X < Main.MouseWorld.X && high.Y > Main.MouseWorld.Y && low.Y < Main.MouseWorld.Y;
        }
        public void Damaged(Player player)
        {
            player.statLife -= player.statLife / 100;
            if (player.statLife < 1 && !magicCursor)
            {
                PlayerDeathReason dR = new PlayerDeathReason
                {
                    SourceCustomReason = $"{player.name} fell to the wrath of power..."
                };
                player.KillMe(dR, double.MaxValue, 0, false);
            }
            if (player.statLife < 1 && magicCursor)
            {
                PlayerDeathReason dR = new PlayerDeathReason
                {
                    SourceCustomReason = $"{player.name} played with themself... in godmode."
                };
                player.KillMe(dR, double.MaxValue, 0, false);
            }
        }
        public override void UpdateDead()
        {
            manipulate = false;
        }
    }
    public class IncNPC : GlobalNPC
    {
        public bool manipulate = false;
        public override bool InstancePerEntity => true;
        public bool InRangeOfCursor(NPC npc)
        {
            Vector2 low = new Vector2(npc.position.X - npc.width, npc.position.Y - npc.height);
            Vector2 high = new Vector2(npc.position.X + npc.width, npc.position.Y + npc.height);
            return high.X > Main.MouseWorld.X && low.X < Main.MouseWorld.X && high.Y > Main.MouseWorld.Y && low.Y < Main.MouseWorld.Y;
        }
        public void Damaged(NPC npc)
        {
            int damage = npc.lifeMax / 50;
            if (damage < 1)
                damage = 50;
            npc.life -= damage;
            if (npc.life < 1)
            {
                npc.HitEffect();
                npc.checkDead();
            }
        }
        public override bool CheckDead(NPC npc)
        {
            foreach (Player p in Main.player)
            {
                if (p.active)
                {
                    IncPlayer inc = p.GetModPlayer<IncPlayer>();
                    if (inc.magicCursor)
                        inc.nTar = false;
                }
            }
            manipulate = false;
            if (manipulate)
                return true;
            return base.CheckDead(npc);
        }
    }
    public class IncItem : GlobalItem
    {
        public bool manipulate = false;
        public bool smiteImmune = true;
        public int smiteImpervious = 0;
        public override bool InstancePerEntity => true;
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (smiteImmune)
            {
                smiteImpervious++;
                if (smiteImpervious > 180)
                {
                    smiteImmune = false;
                    smiteImpervious = 0;
                }
            }
        }
        public override GlobalItem NewInstance(Item item)
        {
            return base.NewInstance(item);
        }
        public bool InRangeOfCursor(Item item)
        {
            Vector2 low = new Vector2(item.position.X - item.width, item.position.Y - item.height);
            Vector2 high = new Vector2(item.position.X + item.width, item.position.Y + item.height);
            return high.X > Main.MouseWorld.X && low.X < Main.MouseWorld.X && high.Y > Main.MouseWorld.Y && low.Y < Main.MouseWorld.Y;
        }
    }
}
