using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace CastledsContent.Items
{
    public class PresetItem : ModItem
    {
        private bool ui = false;
        private bool save = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Vanity Valkyrie");
            Tooltip.SetDefault("'The latest, and last, in fashion; you can't beat perfection!'\nA special tool for saving five Vanity Presets\nVanity Presets will save social equipment, social accessories, dyes, and visual characteristics of your player\nSimply activate the preset menu, select/create a preset, and close the menu to select it\nUse your navigation hotkey and the O key to toggle the Preset menu\nMore information is displayed at certain menus");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 25));
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 48;
            item.rare = ItemRarityID.LightRed;
            item.value = 750000;
        }
        public override void UpdateInventory(Player player)
        {
            CastledPlayer mod = player.GetModPlayer<CastledPlayer>();
            if (Press(Keys.O))
            {
                for (int a = 0; a < mod.presets.Count; a++)
                {
                    if (mod.presets[a].made)
                        mod.presets[a].LoadPreview();
                }
                if (!ui)
                {
                    if (mod.presets[mod.preset].made)
                    {
                        SavePreset(mod, player);
                        ClearSlots(player);
                    }
                    Main.PlaySound(SoundID.MenuOpen);
                    ui = true;
                }
                else
                {
                    if (mod.presets[mod.preset].made)
                        mod.presets[mod.preset].Load(player);
                    Main.PlaySound(SoundID.MenuClose);
                    ui = false;
                }
            }
            if (ui)
            {
                #region UI
                mod.drawUI = true;
                if (!mod.presets[mod.preset].made)
                {
                    mod.writeName = true;
                    mod.drawPreview = false;
                    save = false;
                }
                if (mod.presets[mod.preset].made && !mod.changeName)
                {
                    mod.writeName = false;
                    mod.drawPreview = true;
                    mod.changeName = false;
                }
                #endregion
                if (Press(Keys.Up))
                {
                    mod.preset++;
                    if (mod.preset > 4)
                        mod.preset = 0;
                }
                if (Press(Keys.Down))
                {
                    mod.preset--;
                    if (mod.preset < 0)
                        mod.preset = 4;
                }
                if (Main.keyState.IsKeyDown(Keys.Left) && Main.mouseRight && !save)
                {
                    save = true;
                    SavePreset(mod, player);
                }
                if (Press(Keys.Right) && !mod.writeName && mod.presets[mod.preset].made)
                {
                    mod.drawPreview = false;
                    mod.changeName = true;
                    save = false;
                }
                if (Press(Keys.Delete) && !mod.changeName && mod.presets[mod.preset].made)
                    mod.presets[mod.preset] = new PlayerPreset();
                /*
                if (Press(Keys.Left) && !mod.writeName)
                {
                    Main.PlaySound(SoundID.MenuClose);
                    mod.presets[mod.preset].Load(player);
                    ui = false;
                }*/
                RejectItem(player);
            }
            else
            {
                mod.drawUI = false;
                mod.writeName = false;
                mod.drawPreview = false;
                mod.changeName = false;
            }
        }
        void SavePreset(CastledPlayer mod, Player player)
        {
            List<Color> colors = new List<Color>
            {
                new Color(),
                new Color(),
                new Color(),
                new Color(),
                new Color(),
                new Color(),
                new Color(),
                new Color()
            };
            List<Item> vanity = new List<Item>
            {
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item()
            };
            List<Item> dye = new List<Item>
            {
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item(),
                new Item()
            };
            string input = "";
            for (int a = 0; a < 8; a++)
            {
                switch (a)
                {
                    case 0:
                        if (player.hairDyeColor != Color.Transparent)
                            colors[a] = player.hairDyeColor;
                        else
                            colors[a] = Color.Transparent;
                        break;
                    case 1:
                        colors[a] = player.skinColor;
                        break;
                    case 2:
                        colors[a] = player.hairColor;
                        break;
                    case 3:
                        colors[a] = player.eyeColor;
                        break;
                    case 4:
                        colors[a] = player.shirtColor;
                        break;
                    case 5:
                        colors[a] = player.underShirtColor;
                        break;
                    case 6:
                        colors[a] = player.pantsColor;
                        break;
                    case 7:
                        colors[a] = player.shoeColor;
                        break;
                }
            }
            for (int a = 0; a < 7; a++)
            {
                if (!player.armor[a + 10].IsAir)
                    vanity[a] = player.armor[a + 10].Clone();
            }
            for (int a = 0; a < 9; a++)
            {
                if (!player.dye[a].IsAir)
                    dye[a] = player.dye[a].Clone();
            }
            if (mod.writeName || mod.changeName)
                input = Main.chatText;
            else if (!mod.writeName && !mod.changeName)
                input = mod.presets[mod.preset].name;
            if (!mod.presets[mod.preset].made || !mod.changeName)
                mod.presets[mod.preset].Save(colors, player.hair, player.Male, vanity, dye, input, player.hairDye, player.skinVariant);
            else
                mod.presets[mod.preset].name = input;
            Main.chatText = "";
            mod.writeName = false;
            if (!mod.changeName)
                ui = false;
            mod.changeName = false;
            Main.drawingPlayerChat = false;
        }
        bool Press(Keys keys) => CastledsContent.PresetNavigate.JustPressed && Main.keyState.IsKeyDown(keys);
        /// <summary>
        /// Duplication Exploit Prevention
        /// </summary>
        void ClearSlots(Player player)
        {
            for (int a = 0; a < 7; a++)
                player.armor[a + 10].SetDefaults(ItemID.None);
            for (int a = 0; a < 9; a++)
                player.dye[a].SetDefaults(ItemID.None);
        }
        void RejectItem(Player p)
        {
            for (int a = 10; a < p.armor.Length; a++)
            {
                if (!p.armor[a].IsAir)
                {
                    Item clone = p.armor[a].Clone();
                    p.armor[a].SetDefaults(ItemID.None);
                    PlaceItem(clone);
                }
            }
            for (int a = 0; a < p.dye.Length; a++)
            {
                if (!p.dye[a].IsAir)
                {
                    Item clone = p.dye[a].Clone();
                    p.dye[a].SetDefaults(ItemID.None);
                    PlaceItem(clone);
                }
            }
            void PlaceItem(Item item)
            {
                if (!PlaceItemMain(item))
                    Item.NewItem((int)p.position.X, (int)p.position.Y, p.width, p.height, item.type, item.stack, false, item.prefix, true, false);
            }
            bool PlaceItemMain(Item item)
            {
                for (int i = 0; i < (p.inventory.Length - 8); i++)
                {
                    if (p.inventory[i].IsAir)
                    {
                        p.inventory[i] = item;
                        Main.PlaySound(SoundID.Grab, p.position);
                        return true;
                    }
                }
                Item.NewItem((int)p.position.X, (int)p.position.Y, p.width, p.height, item.type, item.stack, false, item.prefix, true, false);
                return false;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(260, 200, 180);
                }
            }
            /*
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
            }*/
            //list.Insert(num + 1, new TooltipLine(mod, "RobotInvasionTag", "Droid Dreadnaught"));
        }
    }
}
