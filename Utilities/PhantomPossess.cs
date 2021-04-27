using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace CastledsContent.Utilities
{
    /*
    public class PhantomPossess : GlobalNPC
    {
        //byte intensity = 0;
        //int timer = 0;
        PossessColor possess = new PossessColor();

        public override bool InstancePerEntity => true;

        public override void AI(NPC npc)
        {
            //if (npc.type == NPCID.Guide)
                //possess.GenerateTexture(Main.npcTexture[npc.type], ModContent.GetInstance<AConfig>().phantomTest);
        }
    }*/
    /// <summary>
    /// Applies an adjustable and semi-randomized "glitch" filter onto a texture.
    /// </summary>
    public class PossessColor
    {
        public Texture2D original;
        Operation[] operations = new Operation[0];
        Color[] sheet = new Color[0];
        bool operationsDetermined = false;
        public void GenerateTexture(FilterInfo info)
        {
            if (original == null)
                original = info.tex;
            if (info.restorative)
            {
                RestoreTexture(out info.tex);
                //operationsDetermined = false;
            }
            if (!operationsDetermined)
            {
                original = info.tex;
                Color[] bitmap = new Color[info.tex.Width * info.tex.Height];
                Array.Resize(ref sheet, bitmap.Length);
                sheet = bitmap;
                info.tex.GetData(sheet);
                Array.Resize(ref operations, sheet.Length);
                for (int a = 0; a < sheet.Length; a++)
                    DetermineOperation(sheet[a], a, info);
                operationsDetermined = true;
            }
            GetTex(info);
        }
        /// <summary>
        /// "Restores" a texture; turns the input texture into the "original" Texture2D found in this class, if any.
        /// </summary>
        public Texture2D RestoreTexture(out Texture2D tex)
        {
            tex = original;
            return tex;
        }
        void GetTex(FilterInfo info)
        {
            for (int a = 0; a < sheet.Length; a++)
                PerformOperation(operations, a, info);
            info.tex.SetData(sheet);
        }
        void DetermineOperation(Color color, int index, FilterInfo info)
        {
            string result;
            byte check = 0;
            int dex = index - 1;
            WeightedRandom<string> op = new WeightedRandom<string>();
            op.Add(nameof(color.R), color.R / 10);
            op.Add(nameof(color.G), color.G / 10);
            op.Add(nameof(color.B), color.B / 10);
            //op.Add(nameof(color.A), color.A / 10);
            result = op;
            switch (result)
            {
                case "R":
                    check = color.R;
                    break;
                case "G":
                    check = color.G;
                    break;
                case "B":
                    check = color.B;
                    break;
                case "A":
                    check = color.A;
                    break;
            }
            Operation member = new Operation();
            if (info.rarityType == FilterInfo.RarityOperation)
            {
                if (Main.rand.NextBool(info.rarity))
                    ApplyOperation();
            }
            else
                ApplyOperation();
            if (dex > -1 && operations[dex] != null && IsSimilar(operations[dex].target, color))
            {
                member.operation = operations[dex].operation;
                if (info.effectType.Contains("Miss"))
                {
                    member.operation.Insert(0, "Skp");
                    operations[dex].operation.Insert(0, "Skp");
                }
                if (info.effectType.Contains("Delete"))
                {
                    member.operation.Insert(0, "Del");
                    operations[dex].operation.Insert(0, "Del");
                }
            }
            member.target = color;
            operations[index] = member;
            void ApplyOperation()
            {
                if (!info.effectType.Contains("Abstain"))
                {
                    if (check <= 175)
                        member.operation = $"{result + "+"}";
                    else
                        member.operation = $"{result + "-"}";
                }
                if (info.effectType.Contains("Standard"))
                {
                    if (!member.operation.Contains("R"))
                    {
                        if (color.R <= 75)
                            member.operation.Insert(0, "R+");
                        if (color.R >= 215)
                            member.operation.Insert(0, "R-");
                    }
                    if (!member.operation.Contains("G"))
                    {
                        if (color.G <= 25)
                            member.operation.Insert(0, "G+");
                        if (color.G >= 175)
                            member.operation.Insert(0, "G-");
                    }
                    if (!member.operation.Contains("B"))
                    {
                        if (color.G <= 35)
                            member.operation.Insert(0, "B+");
                        if (color.G >= 125)
                            member.operation.Insert(0, "B-");
                    }
                }
            }
        }
        bool IsSimilar(Color refer, Color comp)
        {
            int rand = Main.rand.Next(30, 50);
            return (comp.R <= refer.R + rand && comp.R >= refer.R - rand) || (comp.G <= refer.G + rand && comp.G >= refer.G - rand) || (comp.B <= refer.B + rand && comp.B >= refer.B - rand);
        }
        void PerformOperation(Operation[] operations, int index, FilterInfo info)
        {
            if (info.rarityType == FilterInfo.RarityPerform)
            {
                if (Main.rand.NextBool(info.rarity))
                    Operation();
            }
            else
                Operation();
            void Operation()
            {
                if (sheet[index] != Color.Transparent)
                {
                    if (!operations[index].operation.Contains("Skp") || !operations[index].operation.Contains("Abstain"))
                    {
                        if (operations[index].operation.Contains("R+"))
                            sheet[index].R += info.intensity;
                        if (operations[index].operation.Contains("R-"))
                            sheet[index].R -= info.intensity;
                        if (operations[index].operation.Contains("G+"))
                            sheet[index].G += info.intensity;
                        if (operations[index].operation.Contains("G-"))
                            sheet[index].G -= info.intensity;
                        if (operations[index].operation.Contains("B+"))
                            sheet[index].B += info.intensity;
                        if (operations[index].operation.Contains("B-"))
                            sheet[index].B -= info.intensity;
                    }
                    if (operations[index].operation.Contains("Del"))
                        sheet[index] = Color.Transparent;
                    /*
                    if (operations[index].operation.Contains("A+"))
                        sheet[index].A += intensity;
                    if (operations[index].operation.Contains("A-"))
                        sheet[index].A -= intensity;
                    */
                }
            }
        }
        internal class Operation
        {
            public string operation = "";
            public Color target = new Color();
        }
    }
    /// <summary>
    /// Only meant for the "DisplayTest" texture; displays two specified items for a visual perk.
    /// </summary>
    public class ItemDisplay
    {
        public void GenerateTexture(Texture2D tex, int item1, int item2)
        {
            Texture2D one = Main.itemTexture[item1];
            Texture2D two = Main.itemTexture[item2];
            WipeTexture(tex);
            Color[] bitmap = new Color[tex.Width * tex.Height];
            tex.GetData(bitmap);
            bitmap = ApplyTexture(two, bitmap, tex.Width, 2);
            bitmap = ApplyTexture(one, bitmap, tex.Width, 1);
            tex.SetData(bitmap);
        }
        Color[] ApplyTexture(Texture2D texItem, Color[] tex, int texWidth, int type)
        {
            Color[] item = new Color[texItem.Width * texItem.Height];
            texItem.GetData(item);
            int center = (item.Length / 2) - (texItem.Width / 2);
            int texCenter = 0;
            switch (type)
            {
                case 1:
                    texCenter = 2010;
                    break;
                case 2:
                    texCenter = 1780;
                    break;
            }
            int filer = texItem.Width;
            int skip = 0;
            for (int a = center + (texItem.Width / 2); a > 0; a--)
            {
                int index = center - a;
                filer--;
                if (filer < 1)
                {
                    filer = texItem.Width;
                    skip = texWidth + texItem.Width;
                }
                //item[a] = tex[texCenter - index];
                tex[texCenter - index - skip] = item[a];
                skip = 0;
            }
            filer = texItem.Width;
            for (int a = center - (texItem.Width / 2); a < item.Length; a++)
            {
                filer--;
                if (filer < 1)
                {
                    filer = texItem.Width;
                    skip = texWidth;
                }
                //item[a] = tex[texCenter + a];
                tex[texCenter + a + skip] = item[a];
                skip = 0;
            }
            return tex;
        }
        void WipeTexture(Texture2D tex)
        {
            Color[] bitmap = new Color[tex.Width * tex.Height];
            tex.GetData(bitmap);
            for (int a = 0; a < bitmap.Length; a++)
                if (bitmap[a] != Color.Transparent)
                    bitmap[a] = Color.White;
            tex.SetData(bitmap);
        }
    }
    public class FilterInfo
    {
        public Texture2D tex;
        public byte intensity = 0;
        public string effectType = "Standard";
        public bool restorative = false;
        public int rarity = 0;
        public int rarityType = 0;
        public FilterInfo(Texture2D t, byte i, string e, bool r)
        {
            tex = t;
            intensity = i;
            effectType = e;
            restorative = r;
        }

        public const string Standard = "Standard";
        public const string Standard_Ignore = "StandardMiss";
        public const string Standard_Delete = "StandardDelete";
        public const string Ignore = "Miss";
        public const string Delete = "Delete";
        public const short RarityNone = 0;
        public const short RarityOperation = 1;
        public const short RarityPerform = 2;
    }
}
