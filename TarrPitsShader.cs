using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Buffs;
using CastledsContent.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Effects;

namespace CastledsContent
{
    public class TarrPitsShaders : ModPlayer
    {
        public List<Vector2> beacons = new List<Vector2>();
        Vector2 targBeacon;
        float[] shaderQueue = new float[2];
        bool inside;
        bool hasBeen;
        bool[] outStep = new bool[2];
        bool locked;
        //int timer;
        public override void PostUpdateEquips()
        {
            /*
            if (Filters.Scene["Drugs"].Active)
                Filters.Scene["Drugs"].GetShader().UseColor(Color.Purple).UseImage(ModContent.GetTexture("CastledsContent/Content/floppa"), 0).UseImage(ModContent.GetTexture("CastledsContent/Content/bearger"), 1).UseSecondaryColor(Color.MediumVioletRed.MultiplyRGB(new Color(Main.DiscoR * 2, Main.DiscoG / 3, Main.DiscoB * 3)));
            else
                Filters.Scene.Activate("Drugs");*/
            if (beacons.Count > 0)
            {
                int closest = 0;
                for (int i = 0; i < beacons.Count; i++)
                {
                    if (i == 0 || Dist(i) < Dist(i - 1))
                        closest = i;
                    float Dist(int index) => Vector2.Distance(player.MountedCenter, beacons[index]);
                }
                targBeacon = beacons[closest];
            }
            else
                targBeacon = Vector2.Zero;
            if (!locked)
            {
                if (InTarrBiome())
                    inside = true;
                else
                    inside = false;
            }
            if (inside)
            {
                locked = true;
                hasBeen = true;
                if (shaderQueue[0] == 0)
                {
                    //Main.NewText("in 1");
                    shaderQueue[0] = 1;
                    shaderQueue[1] += 0.07f;
                    Filters.Scene.Activate("Transition");
                    UpdateShader(true);
                }
                if (shaderQueue[0] == 1)
                {
                    if (shaderQueue[1] < 1)
                    {
                        //Main.NewText($"in 2 | {shaderQueue[1]}");
                        shaderQueue[1] += 0.07f;
                        UpdateShader(true);
                    }
                    else
                    {
                        outStep[1] = true;
                        //Main.NewText("in 3");
                        shaderQueue[0] = 2;
                        shaderQueue[1] = 0;
                        Filters.Scene["Transition"].Deactivate();
                        Filters.Scene.Activate("InYourWalls");
                        UpdateShader(false);
                    }
                }
                if (shaderQueue[0] == 2)
                {
                    if (shaderQueue[1] < 1)
                    {
                        //Main.NewText($"in 4 | {shaderQueue[1]}");
                        shaderQueue[1] += 0.07f;
                        UpdateShader(false);
                    }
                    else
                    {
                        //Main.NewText("in 5");
                        shaderQueue[0] = 3;
                        shaderQueue[1] = 1;
                    }
                }
                if (shaderQueue[0] == 3)
                {
                    UpdateShader(false);
                    locked = false;
                }
            }
            else
            {
                if (hasBeen)
                {
                    locked = true;
                    if (!outStep[0])
                    {
                        if (outStep[1])
                        {
                            outStep[0] = true;
                            //Main.NewText("out 1");
                            shaderQueue[0] = -1;
                            shaderQueue[1] = 1;
                            shaderQueue[1] -= 0.07f;
                            Filters.Scene["Transition"].Deactivate();
                            Filters.Scene.Activate("InYourWalls");
                            UpdateShader(false);
                        }
                       else
                        {
                            outStep[0] = true;
                            shaderQueue[0] = -1;
                            shaderQueue[1] = 0;
                            //Main.NewText("out 1 alt");
                        }
                    }
                    if (shaderQueue[0] == -1)
                    {
                        if (shaderQueue[1] > 0)
                        {
                            //Main.NewText($"out 2 | {shaderQueue[1]}");
                            shaderQueue[1] -= 0.07f;
                            UpdateShader(false);
                            //UpdateShader(false);
                        }
                        else
                        {
                            //Main.NewText($"out 3");
                            shaderQueue[0] = -2;
                            shaderQueue[1] = 0;                            
                            CastledsContent.instance.transition[0] = 1;
                            CastledsContent.instance.transition[1] = shaderQueue[1];
                        }
                    }
                    if (shaderQueue[0] == -2 && CastledsContent.instance.transition[2] > 0)
                    {
                        if (shaderQueue[1] < 1)
                        {
                            if (shaderQueue[1] == 0)
                            {
                                Filters.Scene["InYourWalls"].Deactivate();
                                Filters.Scene.Activate("Transition");
                            }
                            //Main.NewText($"out 4 | {shaderQueue[1]}");
                            shaderQueue[1] += 0.07f;
                            CastledsContent.instance.transition[1] = shaderQueue[1];
                            Filters.Scene["Transition"].GetShader().UseOpacity(1 - shaderQueue[1]);
                        }
                        else
                        {
                            //Main.NewText($"out 5");
                            Filters.Scene["Transition"].Deactivate();
                            CastledsContent.instance.transition[0] = CastledsContent.instance.transition[1] = CastledsContent.instance.transition[2] = 0;
                            shaderQueue[0] = 0;
                            shaderQueue[1] = 0;
                            hasBeen = false;
                            outStep[0] = false;
                            outStep[1] = false;
                            locked = false;
                        }
                    }
                }
            }
            void UpdateShader(bool transition)
            {
                float scalar = (float)(200 / 2550f);
                Vector2 width = new Vector2(1.25f - (float)System.Math.Sin(CastledsContent.instance.pedestal[1] * 0.1f) * 0.05f);
                //.UseIntensity(Main.GameZoomTarget).UseDirection(Main.LocalPlayer.MountedCenter - relania.arenaOrigin).UseTargetPosition(relania.arenaOrigin).UseImageOffset(width).UseProgress(scalar / 2).UseColor(Color.Black)
                if (transition)
                    Filters.Scene["Transition"].GetShader().UseOpacity(shaderQueue[1]);
                else
                    Filters.Scene["InYourWalls"].GetShader().UseOpacity(shaderQueue[1]).UseIntensity(Main.GameZoomTarget).UseDirection(Main.LocalPlayer.MountedCenter - targBeacon).UseTargetPosition(targBeacon).UseImageOffset(width).UseProgress(scalar / 2).UseColor(Color.Black);
            }
        }
        public static bool InTarrBiome() => Main.tile[MiscUtilities.Round(Main.player[Main.myPlayer].position.X / 16), MiscUtilities.Round(Main.player[Main.myPlayer].position.Y / 16)].wall == ModContent.WallType<Tiles.TarrPit.Block.TarrockWall>();
    }
    /*public class TestShader : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            Filters.Scene.Activate("RegionTest");
            Filters.Scene.Activate("RegionTestOutline");
        }
        public override void PostUpdateEquips()
        {
            Vector2 stuff = Main.MouseWorld;
            Vector2 width = new Vector2(1, 1);
            float constant = 1;
            foreach(NPC n in Main.npc)
            {
                if (n.active && n.boss)
                {
                    stuff = n.Center;
                    constant = (float)System.Math.Sqrt(System.Math.Pow(7.5f / n.width, 2) + System.Math.Pow(7.5f / n.height, 2));
                    width  = new Vector2(width.X * constant, width.Y * constant);
                    break;
                }
            }
            float plrRelative = (float)(System.Math.Pow(Main.LocalPlayer.MountedCenter.X, 2) + System.Math.Pow(Main.LocalPlayer.MountedCenter.Y, 2));
            float scalar = (float)(200 / 2550f);
            float regionCheck = Vector2.Distance(Main.LocalPlayer.MountedCenter, stuff);
            if ((regionCheck / (System.Math.Sqrt(constant) * 20)) > (150 / (constant * 20)))
                Dust.NewDustPerfect(Main.LocalPlayer.Center, 16);
            Filters.Scene["RegionTest"].GetShader().UseIntensity(Main.GameZoomTarget).UseDirection(Main.LocalPlayer.MountedCenter - stuff).UseImage(ModContent.GetTexture("CastledsContent/Content/testBG"), 0).UseImage(ModContent.GetTexture("CastledsContent/Content/iminyourwalls"), 1).UseTargetPosition(stuff).UseImageOffset(width).UseProgress(scalar / 2).UseColor(Color.Black);
            Filters.Scene["RegionTestOutline"].GetShader().UseOpacity(Main.GameZoomTarget).UseTargetPosition(stuff).UseImageOffset(width).UseProgress(scalar / 2).UseIntensity((float)(115 / 255000f)).UseColor(Color.PaleGoldenrod);
        }
    }*/
}
