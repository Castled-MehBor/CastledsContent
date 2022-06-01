using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using CastledsContent.Tiles.TarrPit.Block;
using Microsoft.Xna.Framework.Graphics;

namespace CastledsContent.Tiles.TarrPit.Deco
{
    #region Deco
    public class Tarrock1x1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(45, 60, 65));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
                TarrTileHitMethod.TarrockHit(i, j, true);
        }
    }
    public class Sludge1x1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(75, 90, 85));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
    }
    public class Tarrock2x1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(45, 60, 65));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
                TarrTileHitMethod.TarrockHit(i, j, true);
        }
    }
    public class Sludge2x1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(70, 90, 85));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
    }
    #region Sludge Pillars
    public class SludgePillar1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(70, 90, 85));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
    }
    public class SludgePillar2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);
            CreateMapEntryName("");
            AddMapEntry(new Color(70, 90, 85));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
    }
    #endregion
    #region Beacons
    public partial class TarrockBeacon : ModTile
    {
        public virtual int BeaconType { get; protected set; }
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);
            CreateMapEntryName("Tarrock Beacon");
            AddMapEntry(new Color(45, 60, 65));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile val = Main.tile[i, j];
            if (val.frameY == 36)
            {
                r = 0.4f;
                g = 1.1f;
                b = 0.7f;
            }
            if (Main.LocalPlayer.whoAmI == Main.myPlayer)
            {
                if (val.frameY == 36)
                {
                    Vector2 zero = new Vector2(Main.offScreenRange);
                    if (Main.drawToScreen)
                        zero = Vector2.Zero;
                    Vector2 pos = (new Vector2((i - 11) * 16, (j - 14) * 16) + zero) + new Vector2(-5, 5);
                    TarrPitsShaders shader = Main.LocalPlayer.GetModPlayer<TarrPitsShaders>();
                    if (shader.beacons.Contains(pos))
                        shader.beacons.Clear();
                    shader.beacons.Add(pos);
                    //Main.NewText((new Vector2((i - 11) * 16, (j - 14) * 16) + zero) + new Vector2(-5, 5));
                }
                /*Vector2 zero = new Vector2(Main.offScreenRange);
                if (val.frameX < 18)
                {
                    if (auraEmit++ > 100)
                    {
                        auraEmit = 0;
                        if (Main.drawToScreen)
                            zero = Vector2.Zero;
                        //int num = (val.frameY == 20) ? 18 : 16;
                        Projectile aura = NPCs.Flayke.Flayke.BlastEffect((new Vector2((i - 11) * 16, (j - 14) * 16) + zero) + new Vector2(-5, 5), new Color[2] { Color.White, Color.WhiteSmoke }, 0.75f);
                        if (aura.modProjectile is NPCs.Flayke.Blast blast)
                        {
                            blast.increment = 0.0025f;
                            blast.projectile.light = 0.5f;
                        }
                    }
                }*/
            }
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile val = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange);
            if (val.frameX < 18)
            {
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                int num = (val.frameY == 20) ? 18 : 16;
                Main.spriteBatch.Draw(ModContent.GetTexture($"CastledsContent/Tiles/TarrPit/Deco/TarrockBeacon{BeaconType}_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, (Rectangle?)new Rectangle((int)val.frameX, (int)val.frameY, 16, num), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
            }
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
            {
                TarrTileHitMethod.TarrockHit(i, j, true);
                if (Main.LocalPlayer.whoAmI == Main.myPlayer)
                    Main.LocalPlayer.GetModPlayer<TarrPitsShaders>().beacons.Clear();
            }
        }
    }
    public class TarrockBeacon1 : TarrockBeacon
    {
        public override int BeaconType { get { return 1; } }
    }
    public class TarrockBeacon2 : TarrockBeacon
    {
        public override int BeaconType { get { return 2; } }
    }
    #endregion
    #region Tarrock Stalagtites
    public class TarrockStalagtite1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            CreateMapEntryName("Stalagmite");
            AddMapEntry(new Color(50, 65, 80));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanPlace(int i, int j)
        {
            if (Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<TarrockStalagtite1>())
            {
                return false;
            }
            return true;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
                TarrTileHitMethod.TarrockHit(i, j, true);
        }
    }
    public class TarrockStalagtite2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            CreateMapEntryName("Stalagmite");
            AddMapEntry(new Color(50, 65, 80));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanPlace(int i, int j)
        {
            if (Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<TarrockStalagtite2>())
            {
                return false;
            }
            return true;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
                TarrTileHitMethod.TarrockHit(i, j, true);
        }
    }
    #endregion
    #region Sludge Stalagtites
    public class SludgeStalagtite1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            CreateMapEntryName("Dripping Sludge");
            AddMapEntry(new Color(70, 90, 85));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanPlace(int i, int j)
        {
            if (Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<SludgeStalagtite1>())
            {
                return false;
            }
            return true;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
    }
    public class SludgeStalagtite2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            CreateMapEntryName("Dripping Sludge");
            AddMapEntry(new Color(70, 90, 85));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanPlace(int i, int j)
        {
            if (Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<SludgeStalagtite2>())
            {
                return false;
            }
            return true;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.SludgeHit(i, j, false);
            else
                TarrTileHitMethod.SludgeHit(i, j, true);
        }
    }
    #endregion
    #endregion
}
