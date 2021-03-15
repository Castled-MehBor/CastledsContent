using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System;

namespace CastledsContent
{
    public class PlayerPreset : TagSerializable
    {
        public static readonly Func<TagCompound, PlayerPreset> DESERIALIZER = LoadTag;

        public Player preview;
        public byte hairDye;
        public int variant = -1;
        /// <summary>
        /// 0 - Hair Dye | 1 - Skin | 2 - Hair | 3 - Eye | 4 - Shirt | 5 - Undershirt | 6 - Pants | 7 - Shoes
        /// </summary>
        public List<Color> colors = new List<Color>()
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
        public List<Item> armor = new List<Item>() { 
            new Item(), 
            new Item(), 
            new Item(), 
            new Item(), 
            new Item(), 
            new Item(), 
            new Item(), 
            new Item() 
        };
        public List<Item> dye = new List<Item>() 
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
        public int hairStyle = -1;
        public bool male = true;
        public bool made = false;
        public string name;
        #region Save and Load
        public void Save(List<Color> color, int hairStyle1, bool isMale, List<Item> armor1, List<Item> dye1, string name1, byte hairDye1, int skinVariant)
        {
            for (int a = 0; a < colors.Count; a++)
                colors[a] = color[a];
            for (int a = 0; a < armor.Count; a++)
            {
                if (!armor1[a].IsAir)
                    armor[a] = armor1[a].Clone();
                else
                    armor[a].SetDefaults(ItemID.None);
            }
            for (int a = 0; a < dye.Count; a++)
            {
                if (!dye1[a].IsAir)
                    dye[a] = dye1[a].Clone();
                else
                    dye[a].SetDefaults(ItemID.None);
            }
            hairStyle = hairStyle1;
            male = isMale;
            name = name1;
            variant = skinVariant;
            hairDye = hairDye1;
            made = true;
        }
        public void Load(Player player)
        {
            player.hairDye = hairDye;
            player.skinVariant = variant;
            player.hair = hairStyle;
            player.Male = male;
            for (int a = 0; a < colors.Count; a++) 
            {
                if (colors[a] != null || colors[a] != Color.Transparent)
                {
                    switch (a)
                    {
                        case 0:
                            player.hairDyeColor = colors[a];
                            break;
                        case 1:
                            player.skinColor = colors[a];
                            break;
                        case 2:
                            player.hairColor = colors[a];
                            break;
                        case 3:
                            player.eyeColor = colors[a];
                            break;
                        case 4:
                            player.shirtColor = colors[a];
                            break;
                        case 5:
                            player.underShirtColor = colors[a];
                            break;
                        case 6:
                            player.pantsColor = colors[a];
                            break;
                        case 7:
                            player.shoeColor = colors[a];
                            break;
                    }
                }
            }
            for (int a = 0; a < armor.Count; a++)
                player.armor[a + 10] = armor[a].Clone();
            for (int a = 0; a < dye.Count; a++)
                player.dye[a] = dye[a].Clone();
        }
        #endregion

        public TagCompound SerializeData()
        {
            return new TagCompound
            {
                //["preview"] = preview,
                ["hairDye"] = hairDye,
                ["variant"] = variant,
                ["colors"] = colors,
                ["armor"] = armor,
                ["dye"] = dye,
                ["hairStyle"] = hairStyle,
                ["male"] = male,
                ["made"] = made,
                ["name"] = name
            };
        }
        public static PlayerPreset LoadTag(TagCompound tag)
        {
            var pre = new PlayerPreset
            {
                //preview = tag.Get<Player>("preview"),
                hairDye = tag.GetByte("hairDye"),
                variant = tag.GetInt("variant"),
                colors = tag.Get<List<Color>>("colors"),
                armor = tag.Get<List<Item>>("armor"),
                dye = tag.Get<List<Item>>("dye"),
                hairStyle = tag.GetInt("hairStyle"),
                male = tag.GetBool("male"),
                made = tag.GetBool("made"),
                name = tag.GetString("name")
            };
            return pre;
        }
        public void LoadPreview()
        {
            Player player = new Player(true);
            Load(player);
            player.direction = -1;
            player.gravDir = 1f;
            player.PlayerFrame();
            player.socialIgnoreLight = true;
            preview = player;
        }
    }
}
