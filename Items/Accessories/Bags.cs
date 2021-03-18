using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System;

namespace CastledsContent.Items.Accessories
{
    public class StarterBag : ModItem
    {
        BagPickup bag = new BagPickup(10);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leather Satchel");
            Tooltip.SetDefault("'A simple looking bag, that hides more than the eye can perceive.'\nWhen equipped:\nIncreases all damage types by 4%\nIncreases maximum & general movement speed, as well as knockback dealt by 5%\nWhile in inventory:\nPicks up any nearby items if your inventory is full\n10 item capacity\nRight Click to empty the bag");
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
        public override void RightClick(Player player)
        {
            item.stack++;
            bag.CheckRightClick(player);
        }
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
        public override void UpdateInventory(Player player) { bag.Pickup(player); }
        public override bool CanRightClick() => true;
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CastledsContent:OneTierBar", 2);
            recipe.AddRecipeGroup("CastledsContent:TwoTierBar", 3);
            recipe.AddRecipeGroup("CastledsContent:ThreeTierBar", 4);
            recipe.AddRecipeGroup("CastledsContent:FourTierBar", 5);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Leather);
            recipe.AddRecipe();
        }
    }
    public class ApprenticeBag : ModItem
    {
        BagPickup bag = new BagPickup(25);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Duffel-bag");
            Tooltip.SetDefault("'Commonly used as backpacks for aspiring wizards and witches'"
            + "\nWhen equipped:"
            + "\nIncreases all damage types by 8%"
            + "\nIncreases maximum and general movement speed by 8%"
            + "\nIncreases knockback dealt by 10%"
            + "\nLife regeneration is increased"
            + "\nWhile in inventory:\nPicks up any nearby items if your inventory is full\n25 item capacity\nRight Click to empty the bag");
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
        public override void RightClick(Player player)
        {
            item.stack++;
            bag.CheckRightClick(player);
        }
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
        public override void UpdateInventory(Player player) { bag.Pickup(player); }
        public override bool CanRightClick() => true;
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CastledsContent:EvilBar", 10);
            recipe.AddRecipeGroup("CastledsContent:EvilDrop", 5);
            recipe.AddIngredient(ModContent.ItemType<StarterBag>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class BagPickup : TagSerializable
    {
        public static readonly Func<TagCompound, BagPickup> DESERIALIZER = Load;
        public static List<int> bags = new List<int>
        {
            ModContent.ItemType<StarterBag>(),
            ModContent.ItemType<ApprenticeBag>()
        };
        public List<Item> contained = new List<Item>();
        public int limit;
        //public string storage = "Storage: ";
        public BagPickup(int limit1) => limit = limit1;
        public void Pickup(Player player)
        {
            Vector4 points = new Vector4(player.position.X - 50, player.position.X + 50, player.position.Y - 50, player.position.Y + 50);
            foreach (Item i in Main.item)
            {
                if (i != null && !i.IsAir)
                {
                    SGlobalItem item = i.GetGlobalItem<SGlobalItem>();
                    if (InRange(i) && !player.ItemSpace(i) && contained.Count < limit)
                        item.pickup = this;
                }
            }
            bool InRange(Item e) => e.position.X > points.X && e.position.X < points.Y && e.position.Y > points.Z && e.position.Y < points.W;
        }
        public void CheckRightClick(Player player)
        {
            Item item = Main.mouseItem;
            if (item != null && !item.IsAir && contained.Count < limit && !bags.Contains(item.type))
            {
                Item addBag = item.Clone();
                contained.Add(addBag);
                item.SetDefaults(ItemID.None);
            }
            else if (contained.Count > 0)
            {
                for (int a = 0; a < player.inventory.Length - 9; a++)
                {
                    if (player.inventory[a] == null || player.inventory[a].IsAir)
                    {
                        if (contained.Count > 0)
                        {
                            Item slotItem = contained[0].Clone();
                            player.inventory[a] = slotItem;
                            contained.RemoveAt(0);
                        }
                    }
                }
            }
        }
        /*public string StorageTooltip()
        {
            string output = "Storage: ";
            for (int a = 0; a < contained.Count; a++)
                output.Insert(output.Length, $"[i/s{contained[a].stack}:{contained[a].type}]");
            return output;
        }*/
        public TagCompound SerializeData()
        {
            return new TagCompound
            {
                {nameof(contained), contained },
                {nameof(limit), limit }
            };
        }
        public static BagPickup Load(TagCompound tag)
        {
            var bP = new BagPickup(0)
            {
                contained = tag.Get<List<Item>>(nameof(contained)),
                limit = tag.GetInt(nameof(limit))
            };
            return bP;
        }
    }
}
