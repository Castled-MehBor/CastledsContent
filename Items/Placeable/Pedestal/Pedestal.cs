using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using System;

namespace CastledsContent.Items.Placeable.Pedestal
{
	public class Pedestal : ModItem
	{
		public override void SetStaticDefaults() 
		{ 
			DisplayName.SetDefault("Jinxed Pedestal");
			Tooltip.SetDefault("Display your stuff in style'\nRight-click with an item in hand to display atop the pedestal\nHold control and right-click to mirror the direction of the displayed item");
		}
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<PedestalT>();
			item.width = 28;
			item.height = 18;
			item.rare = ItemRarityID.Green;
			item.maxStack = 99;
		}
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("Wood", 30);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.FallenStar);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    internal class PedestalT : ModTile
    {
		int FrameCounter = 0;
		int Frame = 0;
		int[] miscFrame = new int[2] { 0, 0 };
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Pedestal");
			AddMapEntry(new Color(137, 82, 25), name);
			dustType = DustID.t_LivingWood;
			TileObjectData.addTile(Type);
        }
		public override void PlaceInWorld(int i, int j, Item item)
		{
			PedestalData data = new PedestalData(new Item(), new Vector2(i, j));
			CastledWorld.pedestals.Add(data);
		}
		public override void NumDust(int i, int j, bool fail, ref int num) => num = 6;
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			PedestalData data = ThisPedestal(new Vector2(i, j));
			Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<Pedestal>());
			Main.LocalPlayer.QuickSpawnItem(data.item.Clone());
			CastledWorld.pedestals.Remove(data);
		}
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ControlDown() ? ItemID.PocketMirror : ModContent.ItemType<Pedestal>();
		}
		public override bool NewRightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			PedestalData data = ThisPedestal(new Vector2(i, j));
			if (ControlDown())
            {
				data.flipped = !data.flipped;
				Main.PlaySound(SoundID.Item1, TilePosition(i, j));
            }
			else
            {
				if (data.item == null || data.item.IsAir)
				{
					Item item = InteractItem(player);
					if (item != null && !item.IsAir)
					{
						data.item = item.Clone();
						item.stack--;
					}
				}
				else if (data.item != null && !data.item.IsAir)
				{
					Item item = InteractItem(player);
					player.QuickSpawnItem(data.item.Clone());
					data.item = new Item();
					if (item != null && !item.IsAir)
					{
						data.item = item.Clone();
						item.stack--;
					}
				}
			}
			//Main.player[Main.myPlayer].PickTile(i, j, 100);
			return true;
		}
		bool ControlDown() => Main.keyState.IsKeyDown(Keys.LeftControl) || Main.keyState.IsKeyDown(Keys.RightControl);
		public Vector2 TilePosition(int i, int j) => new Vector2(i * 16, j * 16);
		public Item InteractItem(Player player) => Main.mouseItem != null && !Main.mouseItem.IsAir ? Main.mouseItem : player.HeldItem;
		public static PedestalData ThisPedestal(Vector2 coord)
        {
			foreach (PedestalData p in CastledWorld.pedestals)
				if (EntireArea(p.coordinates, coord))
					return p;
			return null;
        }
		public static PedestalData ThisPedestalSpecific(Vector2 coord)
		{
			foreach (PedestalData p in CastledWorld.pedestals)
				if (p.coordinates == coord)
					return p;
			return null;
		}
		public static bool EntireArea(Vector2 v, Vector2 coord)
		{
			if (v == new Vector2(coord.X - 1, coord.Y))
				return true;
			if (v == new Vector2(coord.X, coord.Y))
				return true;
			if (v == new Vector2(coord.X + 1, coord.Y))
				return true;
			if (v == new Vector2(coord.X - 1, coord.Y - 1))
				return true;
			if (v == new Vector2(coord.X, coord.Y - 1))
				return true;
			if (v == new Vector2(coord.X + 1, coord.Y - 1))
				return true;
			return false;
		}
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
			PedestalData data = ThisPedestalSpecific(new Vector2(i, j));
			if (data != null && data.item != null && data.item.type > ItemID.None && Main.rand.NextBool(79))
			{
				Texture2D itemTex = Main.itemTexture[data.item.type];
				Rectangle itemTexRect = new Rectangle(0, 0, itemTex.Width, itemTex.Height);
				if (Main.itemAnimationsRegistered.Contains(data.item.type))
				{
					int FrameCount = Main.itemAnimations[data.item.type].FrameCount;
					int TicksPerFrame = Main.itemAnimations[data.item.type].TicksPerFrame;
					Update();
					void Update()
					{
						if (++miscFrame[0] >= TicksPerFrame / 6)
						{
							miscFrame[0] = 0;
							if (++miscFrame[1] >= FrameCount)
							{
								miscFrame[1] = 0;
							}
						}
					}
					itemTexRect = itemTex.Frame(1, FrameCount, 0, miscFrame[1]);
				}
				Dust dust = Dust.NewDustPerfect(new Vector2((i * 16) + Main.rand.Next(itemTexRect.Width / 2 * -1, itemTexRect.Width / 2), (j * 16) - CastledsContent.instance.pedestal[0] * 33.5f - Main.rand.Next(itemTexRect.Height / 4, itemTexRect.Height / 2)), DustID.SilverCoin, Vector2.Zero);
				dust.noGravity = true;
				dust.velocity = Vector2.Zero;
			}
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
			PedestalData data = ThisPedestalSpecific(new Vector2(i, j));
			if (data != null && data.item != null && data.item.type > ItemID.None)
            {
				SpriteEffects effects = data.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
					vector = Vector2.Zero;
				Texture2D itemTex = Main.itemTexture[data.item.type];
                Rectangle itemTexRect = new Rectangle(0, 0, itemTex.Width, itemTex.Height);
                Vector2 drawPos = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector;
				float imgScale = 1;
				imgScale *= (float)(Math.Sin((double)((Math.PI * 7.5) * CastledsContent.instance.pedestal[0] / 450f)) * 2.5f);
				if (Main.itemAnimationsRegistered.Contains(data.item.type))
                {
                    int FrameCount = Main.itemAnimations[data.item.type].FrameCount;
                    int TicksPerFrame = Main.itemAnimations[data.item.type].TicksPerFrame;
                    Update();
                    void Update()
                    {
                        if (++FrameCounter >= TicksPerFrame / 6)
                        {
							FrameCounter = 0;
                            if (++Frame >= FrameCount)
                            {
								Frame = 0;
                            }
                        }
                    }
                    itemTexRect = itemTex.Frame(1, FrameCount, 0, Frame);
                }
				Vector2 position = new Vector2(((itemTexRect.Width + -115) / 2) + 50, ((itemTexRect.Height - 25 + (CastledsContent.instance.pedestal[0] * 33.5f)) / 2) + 50);
				if (!CastledsContent.instance.images[data.item.type].voiden)
					Main.spriteBatch.Draw(CastledsContent.instance.images[data.item.type].tex, drawPos, itemTexRect, AlphaColorBehind(), 0f, new Vector2(((itemTexRect.Width + (-115 + ((imgScale + 1) * 3.5f))) / 2) + 50, ((itemTexRect.Height - 25 + (CastledsContent.instance.pedestal[0] * 20f)) / 2) + 42.5f), (imgScale + 1) * 1.25f, effects, 0f);
				Main.spriteBatch.Draw(itemTex, drawPos, itemTexRect, AlphaColor(), 0f, new Vector2(((itemTexRect.Width + (-115 + CastledsContent.instance.pedestal[3] * 6.25f)) / 2) + 50, ((itemTexRect.Height - 25 + (CastledsContent.instance.pedestal[0] * 33.5f)) / 2) + 50), 1f, effects, 0f);
				Main.spriteBatch.Draw(itemTex, drawPos, itemTexRect, AlphaColor(), 0f, new Vector2(((itemTexRect.Width + (-115 - CastledsContent.instance.pedestal[3] * 6.25f)) / 2) + 50, ((itemTexRect.Height - 25 + (CastledsContent.instance.pedestal[0] * 33.5f)) / 2) + 50), 1f, effects, 0f);
				Main.spriteBatch.Draw(itemTex, drawPos, itemTexRect, Lighting.GetColor(i, j) * 4.25f, 0f, position, 1f, effects, 0f);
				Color AlphaColor() => new Color(Color.Transparent.R + 75, Color.Transparent.G + 75, Color.Transparent.B + 75, 75);
				Color AlphaColorBehind()
                {
					float a = CastledsContent.instance.pedestal[0] * 1.75f;
					return new Color(Color.Transparent.R + a, Color.Transparent.G + a, Color.Transparent.B + a, a);
				}
			}
        }
    }
    public class PedestalData : TagSerializable
    {
        public static readonly Func<TagCompound, PedestalData> DESERIALIZER = Load;
        public Item item;
        public Vector2 coordinates;
		public bool flipped;
		public PedestalData(Item i, Vector2 coord)
        {
			item = i;
			coordinates = coord;
        }
		public TagCompound SerializeData()
		{
			return new TagCompound
			{
				{ nameof(item), item },
				{ nameof(coordinates), coordinates},
				{ nameof(flipped), flipped }
			};
		}
		public static PedestalData Load(TagCompound tag)
        {
			PedestalData data = new PedestalData(tag.Get<Item>(nameof(item)), tag.Get<Vector2>(nameof(coordinates)));
			data.flipped = tag.GetBool(nameof(flipped));
			return data;
		}
	}
}