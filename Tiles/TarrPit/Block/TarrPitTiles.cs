using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace CastledsContent.Tiles.TarrPit.Block
{
    public class Tarrock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarrock");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 3;
            item.useTime = 2;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.consumable = true;
            item.createTile = ModContent.TileType<TarrockT>();
        }
    }
    public class TarrockT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 25;
            AddMapEntry(new Color(35, 40, 45));
            drop = ModContent.ItemType<Tarrock>();
        }
        public override bool KillSound(int i, int j) => false;
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.TarrockHit(i, j, false);
            else
                TarrTileHitMethod.TarrockHit(i, j, true);
        }
    }
    public class BlackSludge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Sludge");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 3;
            item.useTime = 2;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.consumable = true;
            item.createTile = ModContent.TileType<BlackSludgeT>();
        }
    }
    public class BlackSludgeT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            minPick = 25;
            AddMapEntry(new Color(125, 100, 85));
            drop = ModContent.ItemType<BlackSludge>();
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
    public class SludgeTarrock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 25;
            AddMapEntry(new Color(40, 20, 10));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            TarrTileHitMethod.SludgeHit(i, j, true);
            Item.NewItem(new Rectangle(i * 16, j * 16, 8, 8), ModContent.ItemType<BlackSludge>());
            Main.tile[i, j].type = (ushort)ModContent.TileType<TarrockT>();
            Main.tile[i, j].active(true);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanKillTile(int i, int j, ref bool blockDamaged) { return false; }
    }
    public class Ionyx : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ionyx");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 3;
            item.useTime = 2;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.consumable = true;
            item.createTile = ModContent.TileType<IonyxT>();
        }
    }
    public class IonyxT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 25;
            AddMapEntry(new Color(20, 10, 5));
            drop = ModContent.ItemType<Ionyx>();
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
                TarrTileHitMethod.IonyxHit(i, j, false);
            else
                TarrTileHitMethod.IonyxHit(i, j, true);
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.NextBool(499))
            {
                string directory = Main.rand.NextBool(1) ? "Sounds/Custom/TarrPit/IonyxTwinkle" : "Sounds/Custom/TarrPit/IonyxAmbient";
                Main.PlaySound(SoundLoader.customSoundType, new Vector2(i * 16, j * 16), CastledsContent.instance.GetSoundSlot(SoundType.Custom, directory));
            }
        }
    }
    public class SludgeIonyx : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 25;
            AddMapEntry(new Color(40, 20, 10));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            TarrTileHitMethod.SludgeHit(i, j, true);
            Item.NewItem(new Rectangle(i * 16, j * 16, 8, 8), ModContent.ItemType<BlackSludge>());
            Main.tile[i, j].type = (ushort)ModContent.TileType<IonyxT>();
            Main.tile[i, j].active(true);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }
        public override bool KillSound(int i, int j) => false;
        public override bool CanKillTile(int i, int j, ref bool blockDamaged) { return false; }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.NextBool(999))
            {
                string directory = Main.rand.NextBool(1) ? "Sounds/Custom/TarrPit/IonyxTwinkle" : "Sounds/Custom/TarrPit/IonyxAmbient";
                Main.PlaySound(SoundLoader.customSoundType, new Vector2(i * 16, j * 16), CastledsContent.instance.GetSoundSlot(SoundType.Custom, directory));
            }
        }
    }
    public struct TarrTileHitMethod
    {
        public static void TarrockHit(int i, int j, bool breakTile)
        {
            Vector2 pos = new Vector2(i * 16, j * 16);
            string directory = breakTile ? "Sounds/Custom/TarrPit/TarrockBreak" : "Sounds/Custom/TarrPit/TarrockHit";
            Main.PlaySound(SoundLoader.customSoundType,pos, CastledsContent.instance.GetSoundSlot(SoundType.Custom, directory));
            for(int a = 0; a < (breakTile ? Main.rand.Next(6, 9) : Main.rand.Next(3, 4)); ++a)
                    Dust.NewDust(pos, 16, 16, ModContent.DustType<Dusts.TarrockDust>());
        }
        public static void SludgeHit(int i, int j, bool breakTile)
        {
            Vector2 pos = new Vector2(i * 16, j * 16);
            string directory = breakTile ? "Sounds/Custom/TarrPit/SludgeBreak" : "Sounds/Custom/TarrPit/SludgeHit";
            Main.PlaySound(SoundLoader.customSoundType, pos, CastledsContent.instance.GetSoundSlot(SoundType.Custom, directory));
            for(int a = 0; a < (breakTile ? Main.rand.Next(6, 9) : Main.rand.Next(3, 4)); ++a)
                Dust.NewDust(pos, 16, 16, ModContent.DustType<Dusts.SludgeDust>());
        }
        public static void IonyxHit(int i, int j, bool breakTile)
        {
            Vector2 pos = new Vector2(i * 16, j * 16);
            string directory = breakTile ? "Sounds/Custom/TarrPit/IonyxBreak" : "Sounds/Custom/TarrPit/IonyxHit";
            Main.PlaySound(SoundLoader.customSoundType, pos, CastledsContent.instance.GetSoundSlot(SoundType.Custom, directory));
            for(int a = 0; a < (breakTile ? Main.rand.Next(6, 9) : Main.rand.Next(3, 4)); ++a)
                Dust.NewDust(pos, 16, 16, ModContent.DustType<Dusts.IonyxDust>());
        }
    }
    public class TarrockWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = false;
            AddMapEntry(new Color(15, 20, 25));
        }
    }
}