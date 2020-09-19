using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Threading;

namespace CastledsContent.Items
{
	public abstract class CastledItem : ModItem
	{
		public static bool isSuggested;
		public static bool annonymous;
		public static string suggester = "";

		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			if (isSuggested)
            {
                foreach (TooltipLine item in list)
                {
                    if (item.mod == "Terraria" && item.Name == "ItemName")
                    {
                        item.overrideColor = new Color(60, 60, 6);
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
				if (!annonymous)
                {
					list.Insert(num + 1, new TooltipLine(mod, "UserSuggestionTag", $"Suggested by: {suggester}"));
					foreach (TooltipLine item2 in list)
					{
						if (item2.mod == "CastledsContent" && item2.Name == "UserSuggestionTag")
						{
							item2.overrideColor = new Color(120, 20 + Main.DiscoR / 2, 50);
						}
					}
				}
			}
		}
	}
}
