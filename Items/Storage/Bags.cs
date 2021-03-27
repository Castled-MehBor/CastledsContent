using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;

namespace CastledsContent.Items.Storage
{
    #region Old
    public class StarterBag : ModItem
    {
        BagPickup bag = new BagPickup(10, true);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leather Satchel");
            Tooltip.SetDefault("'A simple looking bag, that hides more than the eye can perceive."
            + "\nConverts to the newer version of this item when in your inventory");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.accessory = true;
            item.value = 15000;
            item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.04f;
            player.moveSpeed += 0.05f;
            player.maxRunSpeed += 0.05f;
            player.GetWeaponKnockback(item, 1.05f);
        }
        public override void UpdateInventory(Player player) { BagPickup.ReplaceItem(bag, item, 0); }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                {nameof(bag), bag }
            };
        }
        public override void Load(TagCompound tag)
        {
            bag = tag.Get<BagPickup>(nameof(bag));
        }
    }
    public class ApprenticeBag : ModItem
    {
        BagPickup bag = new BagPickup(25, false);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Duffel-bag");
            Tooltip.SetDefault("'Commonly used as backpacks for aspiring wizards and witches'"
            + "\nConverts to the newer version of this item when in your inventory");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.accessory = true;
            item.value = 22500;
            item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.08f;
            player.moveSpeed += 0.08f;
            player.maxRunSpeed += 0.08f;
            player.GetWeaponKnockback(item, 1.1f);
            player.lifeRegen += 2;
        }
        public override void UpdateInventory(Player player) { BagPickup.ReplaceItem(bag, item, 1); }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                {nameof(bag), bag }
            };
        }
        public override void Load(TagCompound tag)
        {
            bag = tag.Get<BagPickup>(nameof(bag));
        }
    }
    #endregion
    public class BagPickup : TagSerializable
    {
        public static readonly Func<TagCompound, BagPickup> DESERIALIZER = Load;
        public bool doPickup = false;
        public List<Item> contained = new List<Item>();
        public List<int> infStack = new List<int>();
        public int limit;
        public BagPickup(int limit1, bool pickup)
        {
            limit = limit1;
            doPickup = pickup;
        }
        public void Pickup(Player player)
        {
            Vector4 points = new Vector4(player.position.X - 50, player.position.X + 50, player.position.Y - 50, player.position.Y + 50);
            foreach (Item i in Main.item)
            {
                if (i != null && !i.IsAir && NotStorage(i))
                {
                    SGlobalItem item = i.GetGlobalItem<SGlobalItem>();
                    if (InRange(i) && !player.ItemSpace(i) && contained.Count < limit)
                        item.pickup = this;
                }
            }
            bool InRange(Item e) => e.position.X > points.X && e.position.X < points.Y && e.position.Y > points.Z && e.position.Y < points.W;
        }
        public void CheckRightClick(Player player, Item bag)
        {
            bool view = ModContent.GetInstance<ClientConfig>().bagDoubleClick;
            CastledPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
            Item item = Main.mouseItem;
            if (item != null && !item.IsAir && NotStorage(item) && !BagTag())
                ContainItem(item);
            if (BagTag())
            {
                foreach (Item i in player.inventory)
                    if (i != null && !i.IsAir && i.GetGlobalItem<SGlobalItem>().bagTag)
                        ContainItem(i);
            }
            else if (item == null || item.IsAir)
            {
                SetList(bag, modPlayer, this);
                if (contained.Count > 0)
                {
                    if (CheckOpenCondition(bag.GetGlobalItem<SGlobalItem>().storage))
                    {
                        for (int a = 0; a < player.inventory.Length - 9; a++)
                        {
                            if (player.inventory[a] == null || player.inventory[a].IsAir)
                            {
                                if (contained.Count > 0)
                                {
                                    Item slotItem = contained[0].Clone();
                                    if (ModContent.GetInstance<ClientConfig>().bagTagBoolean)
                                    player.inventory[a] = slotItem;
                                    {
                                        player.inventory[a].GetGlobalItem<SGlobalItem>().bagTag = true;
                                        player.inventory[a].GetGlobalItem<SGlobalItem>().tagExpire = ModContent.GetInstance<ClientConfig>().bagTagExpire * 1000;
                                    }
                                    contained.RemoveAt(0);
                                }
                            }
                        }
                        //modPlayer.hoverStorage.Clear();
                        SetList(bag, modPlayer, this);
                        modPlayer.listMade = false;
                    }
                }
                modPlayer.listMade = true;
            }
            void ContainItem(Item item1)
            {
                if (item1 != null && !item1.IsAir && NotStorage(item1))
                {
                    bool canStack = false;
                    for (int a = 0; a < contained.Count; a++)
                    {
                        if (contained[a].type == item1.type && contained[a].prefix == item1.prefix && EndlessStack(this, item1.type, a))
                        {
                            canStack = true;
                            contained[a].stack += item1.stack;
                            item1.SetDefaults(ItemID.None);
                            break;
                        }
                    }
                    if (contained.Count < limit && !canStack)
                    {
                        Item addBag = item1.Clone();
                        contained.Add(addBag);
                        item1.SetDefaults(ItemID.None);
                    }
                }
            }
            bool BagTag()
            {
                bool found = false;
                foreach (Item i in player.inventory)
                    if (i != null && !i.IsAir && i.GetGlobalItem<SGlobalItem>().bagTag)
                        found = true;
                return found;
            }
            bool CheckOpenCondition(BagPickup baggy)
            {
                if (view && baggy != null)
                    return modPlayer.listMade;
                else
                    return true;
            }
        }
        public static void SetList(Item bag, CastledPlayer modPlayer, BagPickup bagg)
        {
            if (bag != null && !bag.IsAir && bag.GetGlobalItem<SGlobalItem>().storage != null)
                modPlayer.SetHoverStorage(bag.GetGlobalItem<SGlobalItem>().storage.contained);
            else
                modPlayer.SetHoverStorage(bagg.contained);
        }
        public TagCompound SerializeData()
        {
            return new TagCompound
            {
                {nameof(contained), contained },
                {nameof(limit), limit },
                {nameof(doPickup), doPickup }
            };
        }
        public static BagPickup Load(TagCompound tag)
        {
            var bP = new BagPickup(0, false)
            {
                contained = tag.Get<List<Item>>(nameof(contained)),
                limit = tag.GetInt(nameof(limit)),
                doPickup = tag.GetBool(nameof(doPickup))
            };
            return bP;
        }
        public static void ReplaceItem(BagPickup bag, Item item, int type)
        {
            switch (type)
            {
                case 0:
                    {
                        item.SetDefaults(ModContent.ItemType<StarterBagNew>());
                        item.GetGlobalItem<SGlobalItem>().storage = bag;
                    }
                    break;
                case 1:
                    {
                        item.SetDefaults(ModContent.ItemType<ApprenticeBagNew>());
                        item.GetGlobalItem<SGlobalItem>().storage = bag;
                    }
                    break;
            }
        }
        public static bool EndlessStack(BagPickup swag, int type, int index)
        {
            if (!swag.infStack.Contains(type))
                return swag.contained[index].stack < swag.contained[index].maxStack;
            else
                return true;
        }
        public static bool NotStorage(Item item) => item != null && !item.IsAir && item.GetGlobalItem<SGlobalItem>().storage == null;
    }
    public partial class BagItem : ModItem
    {
        public override string Texture => "CastledsContent/Items/Storage/StarterBag";
        public virtual int BagLimit { get; protected set; }
        public virtual bool MagicPickup { get; protected set; }
        public virtual bool IsPackage { get; protected set; }
        public virtual List<string> EquipTooltips { get; protected set; }
        public virtual List<int> InfStack { get; protected set; }
        public BagPickup BagStorage 
        {
            get
            {
                return new BagPickup(BagLimit, MagicPickup);
            }
        }
        public override void RightClick(Player player)
        {
            item.stack++;
            item.GetGlobalItem<SGlobalItem>().storage.CheckRightClick(player, item);
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                //{nameof(bag), bag },
                {"b", item.GetGlobalItem<SGlobalItem>().storage }
            };
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Maintain();
            return base.GetAlpha(lightColor);
        }
        public override void Load(TagCompound tag)
        {
            item.GetGlobalItem<SGlobalItem>().storage = tag.Get<BagPickup>("b");
            //bag = tag.Get<BagPickup>(nameof(bag));
        }
        public override void UpdateInventory(Player player) 
        {
            Maintain();
            if (item.GetGlobalItem<SGlobalItem>().storage.doPickup)
                item.GetGlobalItem<SGlobalItem>().storage.Pickup(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            CastledPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
            bool click = ModContent.GetInstance<ClientConfig>().bagDoubleClick;
            Maintain();
            foreach (TooltipLine tip in tooltips)
            {
                if (tip.mod == "Terraria" && tip.Name == "ItemName")
                {
                    if (IsPackage)
                        tip.overrideColor = new Color(255, 115, 65);
                    if (item.type == ModContent.ItemType<TileSack>())
                        tip.overrideColor = new Color(50, 15, 0);
                    if (item.type == ModContent.ItemType<SlatestoneBag>() || item.type == ModContent.ItemType<RunicBag>())
                        tip.overrideColor = new Color(100, 15, 50);
                }
            }
            int num = -1;
            int num2 = 0;
            while (num2 < tooltips.Count)
            {
                if (!tooltips[num2].Name.Equals("ItemName"))
                {
                    num2++;
                    continue;
                }
                num = num2;
                break;
            }
            if (click && modPlayer.hoverStorage != null && modPlayer.listMade)
            {
                tooltips.Insert(num + 1, new TooltipLine(mod, "StorageShow", Storage()));
                if (BagLimit - modPlayer.hoverStorage.Count > 0)
                    tooltips.Insert(num + 2, new TooltipLine(mod, "StorageLeft", $"Available Space: {BagLimit - modPlayer.hoverStorage.Count} slots"));
                else
                    tooltips.Insert(num + 2, new TooltipLine(mod, "StorageLeft", "No space left"));
            }
            tooltips.Insert(num + 1, new TooltipLine(mod, "StorageBasic", "Holds a set amount of items.\nRight-Click with a selected item to put inside the bag"));
            tooltips.Insert(num + 2, new TooltipLine(mod, "StorageCount", $"Holds a maximum of {BagLimit} items"));
            if (MagicPickup)
                tooltips.Insert(num + 3, new TooltipLine(mod, "StorageMagic", $"Picks up and contains stray items when your inventory is full"));
            if (IsPackage)
            {
                tooltips.Insert(num + 3, new TooltipLine(mod, "StoragePackage1", $"Placeable storage that can be picked back up again"));
                tooltips.Insert(num + 4, new TooltipLine(mod, "StoragePackage2", $"Can only be placed if this item is in the current use slot"));
            }
            if (InfStack != null && InfStack.Count > 0)
            {
                tooltips.Insert(num + 3, new TooltipLine(mod, "StorageInfStack", $"This bag can hold the following items to near-infinite amounts"));
                tooltips.Insert(num + 4, new TooltipLine(mod, "StorageInfStackShow", EndlessStacks()));
            }
            if (EquipTooltips != null && EquipTooltips.Count > 0)
            {
                int tooltip = 1;
                foreach (string s in EquipTooltips)
                {
                    tooltips.Insert(num + tooltip, new TooltipLine(mod, $"StorageAccessoryTT{tooltip}", s));
                    tooltip++;
                }
            }
            string EndlessStacks()
            {
                int lines = InfStack.Count;
                int continuing = 0;
                string storage = string.Empty;
                foreach (int i in InfStack)
                {
                    if (i > -1)
                    {
                        continuing++;
                        lines--;
                        storage = storage.Insert(storage.Length, $"[i/s1:{i}] ");
                    }
                }
                if (continuing > 15)
                    storage = storage.Insert(storage.Length, $"... and {lines} more items");
                return storage;
            }
            string Storage()
            {
                int lines = modPlayer.hoverStorage.Count;
                int continuing = 0;
                string storage = "Empty Storage";
                if (modPlayer.hoverStorage.Count > 0)
                    storage = "";
                foreach (Item i in modPlayer.hoverStorage)
                {
                    if (i != null && !i.IsAir)
                    {
                        continuing++;
                        if (lines >= modPlayer.hoverStorage.Count - 15 && lines > 0)
                        {
                            lines--;
                            storage = storage.Insert(storage.Length, $"[i/s{i.stack}:{i.type}] ");
                        }
                    }
                }
                if (continuing > 15 && lines > 0)
                    storage = storage.Insert(storage.Length, $"... and {lines} more items");
                return storage;
            }
        }
        void Maintain()
        {
            if (item.GetGlobalItem<SGlobalItem>().storage == null)
                item.GetGlobalItem<SGlobalItem>().storage = BagStorage;
            if (InfStack != null && InfStack.Count > 0)
                item.GetGlobalItem<SGlobalItem>().storage.infStack = InfStack;
        }
        public override bool CanRightClick() => true;
        public override bool CanUseItem(Player player)
        {
            if (IsPackage)
                return (Main.mouseItem == null || Main.mouseItem.IsAir) && player.HeldItem == item;
            return base.CanUseItem(player);
        }
    }
    public class TestBag : BagItem
    {
        List<int> endless = new List<int>
        {
            ItemID.AlphabetStatueF,
            ItemID.AlphabetStatueU,
            ItemID.AlphabetStatueL,
            ItemID.AlphabetStatueO,
            ItemID.AlphabetStatueF,
            ItemID.AlphabetStatueX,
            ItemID.AlphabetStatueP
        };
        public override int BagLimit { get { return 20; } }
        public override bool MagicPickup { get { return true; } }
        public override List<int> InfStack { get { return endless; } }
        public override string Texture => "CastledsContent/Items/Storage/StarterBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Test Bag"); }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.value = 15000;
            item.rare = ItemRarityID.Expert;
        }
    }
    public class StarterBagNew : BagItem
    {
        public override int BagLimit { get { return 15; } }
        public override List<string> EquipTooltips 
        { 
            get
            {
                return new List<string>
                {
                    "Provides slight stat increases when equipped"
                };
            }
        }
        public override string Texture => "CastledsContent/Items/Storage/StarterBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Leather Sack"); }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.accessory = true;
            item.value = 15000;
            item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.04f;
            player.moveSpeed += 0.04f;
            player.maxRunSpeed += 0.04f;
            player.GetWeaponKnockback(item, 1.04f);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>());
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Leather);
            recipe.AddRecipe();
        }
    }
    public class ApprenticeBagNew : BagItem
    {
        public override int BagLimit { get { return 25; } }
        public override bool MagicPickup { get { return true; } }
        public override List<string> EquipTooltips
        {
            get
            {
                return new List<string>
                {
                    "Provides moderate stat increases when equipped"
                };
            }
        }
        public override string Texture => "CastledsContent/Items/Storage/ApprenticeBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Arcane Duffel-Bag"); }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.accessory = true;
            item.value = 22500;
            item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.08f;
            player.moveSpeed += 0.08f;
            player.maxRunSpeed += 0.08f;
            player.GetWeaponKnockback(item, 1.08f);
            player.lifeRegen += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CastledsContent:EvilBar", 10);
            recipe.AddRecipeGroup("CastledsContent:EvilDrop", 5);
            recipe.AddIngredient(ModContent.ItemType<StarterBagNew>());
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SkywareBag : BagItem
    {
        public override int BagLimit { get { return 50; } }
        public override string Texture => "CastledsContent/Items/Storage/SkywareBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Natural Skyware Bag"); }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.value = 15000;
            item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ModContent.ItemType<Material.HarpyFeather>(), 5);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class TileSack : BagItem
    {
        public override int BagLimit { get { return 15; } }
        public override bool MagicPickup { get { return true; } }
        public override List<int> InfStack 
        {
            get
            {
                return new List<int>
                {
                    ItemID.DirtBlock, //1
                    ItemID.StoneBlock, //2
                    ItemID.MudBlock, //3
                    ItemID.AshBlock, //4
                    ItemID.EbonstoneBlock, //5
                    ItemID.CrimstoneBlock, //6
                    ItemID.PearlstoneBlock, //7
                    ItemID.SandBlock, //8
                    ItemID.Sandstone, //9
                    ItemID.SnowBlock, //10
                    ItemID.IceBlock, //11
                    ItemID.Granite, //12
                    ItemID.Marble, //13
                    ItemID.SiltBlock,
                    ItemID.SlushBlock
                };
            }
        }
        public override string Texture => "CastledsContent/Items/Storage/MinerBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Excavator's Voiden Sack"); Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(14, 7)); }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 48;
            item.value = 15000;
            item.rare = ItemRarityID.LightRed;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientCloth, 8);
            recipe.AddIngredient(ItemID.Leather, 4);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SlatestoneBag : BagItem
    {
        public override int BagLimit { get { return 100; } }
        public override string Texture => "CastledsContent/Items/Storage/SlatestoneBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Slatestone Pitsack"); Tooltip.SetDefault("'A near-bottomless bag, sort of like a portable pothole'"); }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 36;
            item.value = 15000;
            item.rare = ItemRarityID.LightPurple;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientCloth, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 750);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class RunicBag : BagItem
    {
        public override int BagLimit { get { return 50; } }
        public override bool MagicPickup { get { return true; } }
        public override string Texture => "CastledsContent/Items/Storage/RuneBag";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Runic Duffel Bag"); Tooltip.SetDefault("'Only for the most elite of wizards'"); }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 24;
            item.value = 15000;
            item.rare = ItemRarityID.LightPurple;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientCloth, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SealedVaccuum : ModItem
    {
        public override string Texture => "CastledsContent/Items/Storage/SealedVaccuum";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sealed Vaccuum");
            Tooltip.SetDefault("'A 'mass' of coalescated gasses, orbiting a completely empty space'\nUsed to craft various storage related items\nCan be caught from fishing once per day, with a higher chance in the sky layers");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 12));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine item in tooltips)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(125, 255, 255);
                }
            }
        }
    }
    public class SealedVaccuum1 : ModItem
    {
        public override string Texture => "CastledsContent/Items/Storage/SealedVaccuumFish";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sealed Vaccuum");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
        }
        public override void UpdateInventory(Player player)
        {
            foreach (Item i in player.inventory)
            {
                if (i.type == ModContent.ItemType<SealedVaccuum>() && i.stack < i.maxStack)
                {
                    i.stack += item.stack;
                    item.SetDefaults(ItemID.None);
                    break;
                }
            }
            if (!item.IsAir)
                item.SetDefaults(ModContent.ItemType<SealedVaccuum>());
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine item in tooltips)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(125, 255, 255);
                }
            }
        }
    }
    public class Cardboard : ModItem
    {
        public override string Texture => "CastledsContent/Items/Storage/Boxes/Cardboard";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cardboard");
            Tooltip.SetDefault("Used to craft packages for storage");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.maxStack = 99;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 50);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 3);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ModContent.ItemType<Boxes.PackageTiny>(), 2);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ModContent.ItemType<Boxes.PackageSmall>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 5);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ModContent.ItemType<Boxes.PackageMedium>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 8);
            recipe.AddIngredient(ModContent.ItemType<SealedVaccuum>(), 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ModContent.ItemType<Boxes.PackageLarge>());
            recipe.AddRecipe();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine item in tooltips)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(255, 115, 65);
                }
            }
        }
    }
}
