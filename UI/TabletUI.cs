using Microsoft.Xna.Framework;
using CastledsContent.NPCs.Tarr;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;

namespace CastledsContent.UI
{
    public class TabletButton : UIState
    {
        private UIImageButton tabletKill;
        private UIImageButton tabletMove;
        public override void OnInitialize()
        {
            UIPanel panel = new UIPanel
            {
                BackgroundColor = new Color(225, 135, 75, 255),
                HAlign = 0.5f,
                VAlign = 0.25f
            };
            panel.Width.Set(80, 0);
            panel.Height.Set(40, 0);
            Append(panel);

            tabletKill = new UIImageButton(ModContent.GetTexture("CastledsContent/Content/TabletIcon_Butcher"));
            tabletKill.Left.Set(-2.5f, 0f);
            tabletKill.Top.Set(-7.5f, 0f);
            tabletKill.Width.Set(24, 0f);
            tabletKill.Height.Set(30, 0f);
            tabletKill.SetVisibility(1f, 0.75f);
            tabletKill.OnClick += new MouseEvent(Tablet1Clicked);
            panel.Append(tabletKill);

            tabletMove = new UIImageButton(ModContent.GetTexture("CastledsContent/Content/TabletIcon_Cursor"));
            tabletMove.Left.Set(32.5f, 0f);
            tabletMove.Top.Set(-7.5f, 0f);
            tabletMove.Width.Set(24, 0f);
            tabletMove.Height.Set(30, 0f);
            tabletMove.SetVisibility(1f, 0.75f);
            tabletMove.OnClick += new MouseEvent(Tablet2Clicked);
            panel.Append(tabletMove);
        }
        private void Tablet1Clicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.NewText("Test Butcher");
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
        private void Tablet2Clicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.NewText("Test Cursor");
            IncPlayer ic = Main.player[Main.myPlayer].GetModPlayer<IncPlayer>();
            ic.magicCursor = !ic.magicCursor;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (tabletKill.IsMouseHovering)
                Main.hoverItemName = "Hostile Entity Butcher";
            if (tabletMove.IsMouseHovering)
            {
                IncPlayer ic = Main.player[Main.myPlayer].GetModPlayer<IncPlayer>();
                string hover = ic.magicCursor ? "Disable Magic Cursor" : "Enable Magic Cursor";
                Main.hoverItemName = hover;
            }
        }
    }
}