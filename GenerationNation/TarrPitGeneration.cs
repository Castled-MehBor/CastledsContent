using System;
using System.Collections.Generic;
using CastledsContent.GenerationNation.Utilities;
using System.Drawing;
using Terraria.World.Generation;

namespace CastledsContent.GenerationNation
{
    public static class TarrPitGeneration
    {
        static bool success;
        static bool ceilingSuccessful;
        static int wallFailSafe;
        static int successfulIonyx;
        static Random rand = new Random();
        static Bitmap layoutSloping;
        static Bitmap layoutWall;
        static Bitmap layout;
        static Bitmap layoutE;
        static Bitmap layoutTop;
        static Point[] lastCur = new Point[2];
        static int[] curIndex = new int[2];
        static List<Point> surfaceP = new List<Point>();
        static List<Point> groundP = new List<Point>();
        static List<Point> topP = new List<Point>();
        static List<Point> ceilingP = new List<Point>();
        static List<Point> lakeP = new List<Point>();
        static Point[] resevoirPoints = new Point[2];
        public static Bitmap GetLayout(int total, GenerationProgress progress, int count)
        {
            success = false;
            ceilingSuccessful = false;
            rand = new Random();
            lastCur = new Point[2];
            curIndex = new int[2];
            groundP.Clear();
            surfaceP.Clear();
            lakeP.Clear();
            successfulIonyx = 0;
            Code(total, progress, count);
            return layout;
        }
        static void Code(int total, GenerationProgress progress, int count)
        {
            if (!success)
            {
                //Span from X:750-1150 tiles, Y:375:625
                Point borderExpand = new Point(rand.Next(95, 175), rand.Next(65, 115));
                if (!success)
                    layout = new Bitmap(borderExpand.X, borderExpand.Y);
                Point borderXPointLeft = new Point((int)Math.Round(layout.Width * Lerp(0.15f, 0.175f, (float)rand.NextDouble())), layout.Height - (int)Math.Round(layout.Height * Lerp(0.275f, 0.45f, (float)rand.NextDouble())));
                Point borderXPointRight = new Point(layout.Width - (int)Math.Round(layout.Width * Lerp(0.15f, 0.175f, (float)rand.NextDouble())), borderXPointLeft.Y);
                #region Surface Creation
                if (!success)
                {
                    layoutTop = layout;
                    //DrawBorders();
                    //Border Defining:
                    //Left: leftmost point, go right by (total width * 0.125/0.175) divide height of boundry by 5/8iply & round by 0.45-0.675x for ground height
                    layout = new Bitmap((int)Math.Round(borderExpand.X * 1.75f), (int)Math.Round(borderExpand.Y * 1.75f));
                    borderXPointLeft.X += layout.Width / 4;
                    borderXPointRight.X += layout.Width / 4;
                    borderXPointLeft.Y += layout.Height / 5;
                    borderXPointRight.Y += layout.Height / 5;
                    layoutSloping = new Bitmap(layout.Width, layout.Height);
                }
                #region Draw the bottom of the biome
                if (!success)
                {
                    Message(progress, "Creating the shape...", count, total);
                    DrawStepBottom(borderXPointLeft, true);
                    DrawStepBottom(borderXPointRight);
                }
                #endregion
                curIndex[1] = 0;
                curIndex[0] = 0;
                #region Draw the surface of the biome
                if (!success)
                    DrawStepSurface(new Point[2] { borderXPointLeft, borderXPointRight });
                #endregion
                #region Fill Surface Region
                Message(progress, "Filling the cavities...", count, total);
                Point peon = new Point((borderXPointLeft.X + borderXPointRight.X) / 2, (borderXPointLeft.Y + borderXPointRight.Y) / 2);
                while (!success && layout.GetPixel(peon.X, peon.Y) != Col("Transparent"))
                    peon.Y--;
                PaintBucket.FloodFill(layout, peon, Col("Transparent"), Col("Tarrock"));
                #endregion
                //MarkPoints();
                //Export the texture
                if (!success && layout.GetPixel(1, 1) == Col("Tarrock"))
                    Reset();
                if (!success && CheckArea())
                    Reset();
                layoutE = new Bitmap(layout.Width, layout.Height);
                Message(progress, "Creating ceiling...", count, total);
                while (!success && !ceilingSuccessful)
                    CheckCeiling();
                if (!success && layoutTop.GetPixel(2, 2) == Col("Transparent"))
                    ApplyCeiling();
                if (!success && layout.GetPixel(2, 2) == Col("Tarrock"))
                {
                    Reset();
                    ceilingP.Clear();
                    topP.Clear();

                }
                #endregion
                #region Ceiling Creation
                Message(progress, "Filling in the walls...", count, total);
                if (!success)
                    StartWall();
                while (!success && layoutWall.GetPixel(2, 2) == Col("Wall") && wallFailSafe <= 74)
                {
                    wallFailSafe++;
                    StartWall();
                    CreateWall(new Point[2] { borderXPointLeft, borderXPointRight });
                    if (wallFailSafe > 74)
                        layout.SetPixel(2, 2, Col("Transparent"));
                }
                if (!success && wallFailSafe > 74)
                    Reset();
                #endregion
                Message(progress, "Stashing away secrets...", count, total);
                #region Deco
                Message(progress, "Materializing Ionyx...", count, total);
                int ionyxQuota = rand.Next(5, 8);
                while (!success && successfulIonyx < ionyxQuota)
                    IonyxCrystals();
                if (!success)
                {
                    Message(progress, "Creating sludge...", count, total);
                    for (int a = 0; a < rand.Next(14, 18); ++a)
                        CreateSludge();
                    SludgeApply();
                    Message(progress, "Making it look bearable...", count, total);
                    Deco();
                    for (int a = 1; a < 3; a++)
                        layout.SetPixel(CR(lakeP[0].X, 0, layout.Width), CR(lakeP[0].Y - a, 0, layout.Height), Col("Transparent"));
                    layout.SetPixel(CR(lakeP[0].X, 0, layout.Width), CR(lakeP[0].Y - 1, 0, layout.Height), Color.Teal);
                }
                #endregion
                if (!success)
                {
                    for (int b = 0; b < layoutWall.Height; b++)
                    {
                        for (int a = 0; a < layoutWall.Width; a++)
                            if (layoutWall.GetPixel(a, b) != Col("Transparent") && layout.GetPixel(a, b) == Col("Transparent"))
                                layout.SetPixel(a, b, layoutWall.GetPixel(a, b));
                    }
                }
                if (!success)
                {
                    for (int a = 0; a < rand.Next(4, 7); a++)
                        TreasurePod();
                    SlopeApply();
                    success = true;
                    ceilingP.Clear();
                    topP.Clear();
                }
                void Reset()
                {
                    wallFailSafe = 0;
                    ceilingSuccessful = false;
                    rand = new Random();
                    lastCur = new Point[2];
                    curIndex = new int[2];
                    groundP.Clear();
                    surfaceP.Clear();
                    lakeP.Clear();
                    successfulIonyx = 0;
                    Code(total, progress, count);
                }
                void CheckCeiling()
                {
                    Point ceilingXPointLeft;
                    Point ceilingXPointRight;
                    Bitmap decoy2 = new Bitmap(layout.Width, layout.Height);
                    layoutTop.SetPixel(1, 1, Col("Tarrock"));
                    while (layoutTop.GetPixel(1, 1) == Col("Tarrock"))
                    {
                        ceilingP.Clear();
                        topP.Clear();
                        layoutTop.SetPixel(1, 1, Col("Tarrock"));
                        curIndex[0] = 0;
                        curIndex[1] = 0;
                        ceilingXPointLeft = new Point(CR(borderXPointLeft.X * 0.66f, 0, layout.Width), CR(surfaceP[surfaceP.Count / 2].Y * 0.675f, 0, layout.Height));
                        ceilingXPointRight = new Point(borderXPointRight.X, ceilingXPointLeft.Y);
                        //layout.SetPixel(CR(ceilingXPointLeft.X, 0, layout.Width), CR(ceilingXPointLeft.Y, 0, layout.Height), Color.MediumVioletRed);
                        //layout.SetPixel(CR(ceilingXPointRight.X, 0, layout.Width), CR(ceilingXPointRight.Y, 0, layout.Height), Color.AliceBlue);
                        DrawStepCeilingTop(ceilingXPointLeft, true);
                        DrawStepCeilingTop(ceilingXPointRight);
                        curIndex[0] = 0;
                        DrawStepCeiling(new Point[2] { ceilingXPointLeft, ceilingXPointRight });
                        decoy2 = layoutTop;
                        Point peon2 = new Point((ceilingXPointLeft.X + ceilingXPointRight.X) / 2, (ceilingXPointLeft.Y + ceilingXPointRight.Y) / 2);
                        while (decoy2.GetPixel(peon2.X, CR(peon2.Y, 0, decoy2.Height)) != Col("Transparent"))
                            peon2.Y++;
                        PaintBucket.FloodFill(decoy2, peon2, Col("Transparent"), Col("Tarrock"));
                        if (decoy2.GetPixel(2, 2) == Col("Tarrock"))
                        {
                            layoutTop = new Bitmap(layout.Width, layout.Height);
                            layoutTop.SetPixel(1, 1, Col("Tarrock"));
                        }
                        else
                            layoutTop.SetPixel(1, 1, Col("Transparent"));
                    }
                    if (decoy2.GetPixel(2, 2) != Col("Tarrock") && !CheckAreaCeiling())
                    {
                        for (int b = 0; b < decoy2.Height; b++)
                        {
                            for (int a = 0; a < decoy2.Width; a++)
                                if (decoy2.GetPixel(a, b) != Col("Transparent"))
                                    layoutTop.SetPixel(a, b, Col("Tarrock"));
                        }
                        if (layoutTop.GetPixel(2, 2) != Col("Tarrock"))
                            ceilingSuccessful = true;
                    }
                    bool CheckAreaCeiling()
                    {
                        int area = 0;
                        for (int a = 0; a < decoy2.Height; a++)
                        {
                            for (int b = 0; b < decoy2.Width; b++)
                            {
                                if (decoy2.GetPixel(b, a) == Col("Tarrock"))
                                    area++;
                            }
                        }
                        return area < 1500;
                    }
                }
                void ApplyCeiling()
                {
                    for (int b = 0; b < layoutTop.Height; b++)
                    {
                        for (int a = 0; a < layoutTop.Width; a++)
                            if (layoutTop.GetPixel(a, b) != Col("Transparent"))
                                layout.SetPixel(a, b, Col("Tarrock"));
                    }
                }
                void SludgeApply()
                {
                    int query = 0;
                    for (int b = 0; b < layoutE.Height; b++)
                    {
                        for (int a = 0; a < layoutE.Width; a++)
                        {
                            if (layoutE.GetPixel(a, b) == Col("Sludge"))
                            {
                                if (layoutE.GetPixel(a, b) != Col("Transparent"))
                                {
                                    query = 1;
                                    if (layout.GetPixel(a, b) == Col("Tarrock"))
                                        query = 2;
                                    if (layout.GetPixel(a, b) == Col("Ionyx"))
                                        query = 3;
                                }
                                layout.SetPixel(a, b, Fill());
                                query = 0;
                            }
                        }
                    }
                    Color Fill()
                    {
                        switch (query)
                        {
                            case 1:
                                return Col("Sludge");
                            case 2:
                                return Col("SludgeTarrock");
                            case 3:
                                return Col("SludgeIonyx");
                        }
                        return Col("Sludge");
                    }
                    for (int c = 0; c < layoutE.Width; c++)
                        layout.SetPixel(c, 0, Col("Transparent"));
                }
                bool CheckArea()
                {
                    int area = 0;
                    for (int a = 0; a < layout.Height; a++)
                    {
                        for (int b = 0; b < layout.Width; b++)
                        {
                            if (layout.GetPixel(b, a) == Col("Tarrock"))
                                area++;
                        }
                    }
                    return area < 2000 && area < 3250;
                }
                void StartWall()
                {
                    layoutWall = new Bitmap(layout.Width, layout.Height);
                    layoutWall.SetPixel(2, 2, Col("Wall"));
                }
                void SlopeApply()
                {
                    for (int b = 0; b < layoutSloping.Height; b++)
                    {
                        for (int a = 0; a < layoutSloping.Width; a++)
                            if (layoutSloping.GetPixel(CR(a, 0, layout.Width), CR(b, 0, layout.Width)) != Col("Transparent"))
                                layout.SetPixel(CR(a, 0, layout.Width), CR(b, 0, layout.Width), layoutSloping.GetPixel(CR(a, 0, layout.Width), CR(b, 0, layout.Width)));
                    }
                }
            }
        }
        static void TreasurePod()
        {
            Point designate = new Point();
            while (!Valid(designate))
                designate = new Point(rand.Next(1, layout.Width - 1), rand.Next(1, layout.Height - 1));
            Set();
            bool Valid(Point p)
            {
                int count = 0;
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 2; b++)
                    {
                        if (layout.GetPixel(CR(p.X + b, 1, layout.Width - 1), CR(p.Y + a, 1, layout.Height - 1)) == Col("Tarrock") || layout.GetPixel(CR(p.X + b, 1, layout.Width - 1), CR(p.Y + a, 1, layout.Height - 1)) == Col("SludgeTarrock"))
                            count++;
                    }
                }
                return count == 6;
            }
            void Set()
            {
                int count = 0;
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 2; b++)
                    {
                        count++;
                        layout.SetPixel(designate.X + b, designate.Y + a, Legend(count));
                    }
                }
            }
            Color Legend(int num)
            {
                switch (num)
                {
                    case 1:
                        return Color.FromArgb(255, 255, 150, 75);
                    case 2:
                        return Color.FromArgb(255, 200, 150, 125);
                    case 3:
                        return Color.FromArgb(255, 150, 200, 125);
                    case 4:
                        return Color.FromArgb(255, 150, 150, 175);
                }
                return Color.FromArgb(255, 255, 255, 255);
            }
        }
        static void CreateWall(Point[] borders)
        {
            float lerp = 0;
            layoutWall.SetPixel(2, 2, Col("Transparent"));
            for (int a = 0; a < groundP.Count - 1; a++)
            {
                DrawLine(a, groundP);
                lerp = 0;
            }
            for (int a = -1; a < topP.Count - 1; a++)
            {
                DrawLine(a, topP);
                lerp = 0;
            }
            for (int a = 0; a < 100; ++a)
            {
                lerp += 0.01f;
                layoutWall.SetPixel(CR(LerpRound(groundP[0].X, topP[0].X, lerp), 0, layoutTop.Width - 1), CR(LerpRound(groundP[0].Y, topP[0].Y, lerp), 0, layoutTop.Height - 1), Col("Wall"));
            }
            PaintBucket.FloodFill(layoutWall, new Point((borders[0].X + borders[1].X) / 2, (borders[0].Y + borders[1].Y) / 2), Col("Transparent"), Col("Wall"));
            void DrawLine(int point, List<Point> points)
            {
                Point[] use = new Point[2] { point < 0 ? groundP[(groundP.Count / 2) + 1] : points[point], point < 0 ? topP[topP.Count / 2] : points[point + 1] };
                if ((use[1].X - use[0].X < 25 && point > -1) || point < 0)
                {
                    for (int a = 0; a < 100; ++a)
                    {
                        lerp += 0.01f;
                        layoutWall.SetPixel(CR(LerpRound(use[0].X, use[1].X, lerp), 0, layoutTop.Width - 1), CR(LerpRound(use[0].Y, use[1].Y, lerp), 0, layoutTop.Height - 1), Col("Wall"));
                    }
                }
            }
        }
        /// <summary>
        /// Color method
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Color Col(string name)
        {
            switch (name)
            {
                case "Wall":
                    return Color.FromArgb(255, 15, 20, 25);
                case "Tarrock":
                    return Color.FromArgb(255, 35, 40, 45);
                case "Sludge":
                    return Color.FromArgb(255, 125, 100, 85);
                case "Ionyx":
                    return Color.FromArgb(255, 20, 10, 5);
                case "SludgeTarrock":
                    return Color.FromArgb(255, 45, 55, 65);
                case "SludgeIonyx":
                    return Color.FromArgb(255, 40, 20, 10);
                case "Transparent":
                    return Color.FromArgb(0, 0, 0, 0);
                case "White":
                    return Color.FromArgb(255, 255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0, 0);
        }
        #region Surface Methods
        static void Deco()
        {
            for (int b = 0; b < surfaceP.Count - 1; b++)
            {
                Point point = surfaceP[b];
                if (rand.Next(1) == 1 && layout.GetPixel(CR(point.X, 0, layout.Width), CR(point.Y - 1, 0, layout.Height)) == Col("Transparent"))
                {
                    Point[] area;
                    if (rand.Next(3) == 1)
                    {
                        area = new Point[4] { new Point(point.X, point.Y - 1), new Point(point.X + 1, point.Y - 1), new Point(point.X - 1, point.Y), point };
                        foreach (Point p in area)
                            layout.SetPixel(p.X, p.Y, Col("Transparent"));
                        for (int a = 0; a < 2; a++)
                            layout.SetPixel(point.X + (a < 1 ? 1 : 0), point.Y + 1, Col("Tarrock"));
                        layout.SetPixel(CR(point.X - 1, 0, layout.Width), CR(point.Y, 0, layout.Height), Color.SlateGray);
                    }
                    else
                        layout.SetPixel(CR(point.X, 0, layout.Width), CR(point.Y - 1, 0, layout.Height), Color.SlateBlue);
                }
            }
            for (int b = 0; b < lakeP.Count - 1; b++)
            {
                Point point = lakeP[b];
                for (int a = 0; a < 2; a++)
                    layout.SetPixel(point.X, point.Y - (a < 1 ? 1 : 2), Col("Transparent"));
                if (rand.Next(2) == 1 && layout.GetPixel(CR(point.X, 0, layout.Width), CR(point.Y - 1, 0, layout.Height)) == Col("Transparent"))
                    layout.SetPixel(CR(point.X, 0, layout.Width), CR(point.Y - 1, 0, layout.Height), Color.SeaGreen);
            }
            for (int b = 0; b < ceilingP.Count - 1; b++)
            {
                Point point = ceilingP[b];
                Point[] area;
                if (rand.Next(1) == 0 && layout.GetPixel(CR(point.X, 0, layout.Width), CR(point.Y + 1, 0, layout.Height)) == Col("Tarrock"))
                {
                    area = new Point[4] { new Point(point.X, point.Y + 1), new Point(point.X + 1, point.Y + 1), new Point(point.X + 1, point.Y), point };
                    foreach (Point p in area)
                        layout.SetPixel(p.X, p.Y, Col("Transparent"));
                    for (int a = 0; a < 2; a++)
                        layout.SetPixel(point.X + (a < 1 ? 1 : 0), point.Y - 1, Col("Tarrock"));
                    layout.SetPixel(CR(point.X, 0, layout.Width), CR(point.Y, 0, layout.Height), Color.DarkSlateGray);
                }
            }
        }
        static void CreateSludge()
        {
            Point[] length = new Point[2];
            Point l;
            int[] multiply = new int[2];
            int pointCount = rand.Next(5, 7);
            int multipland = pointCount * 24;
            //int original = multipland;
            Point[] sludgeVertex = new Point[pointCount];
            List<Point> sludgePoint = new List<Point>();
            void SetPoints()
            {
                length[0] = groundP[rand.Next(groundP.Count)];
                length[1] = surfaceP[rand.Next(surfaceP.Count)];
            }
            SetPoints();
            while (Round(Length(new Point((length[0].X + length[1].X) / 2, (length[0].Y + length[1].Y) / 2))) < 80)
                SetPoints();
            l = new Point((length[0].X + length[1].X) / 2, (length[0].Y + length[1].Y) / 2);
            //l.X -= Round(layoutE.Width * 0.1f);
            //l.Y += layout.Height / 6;
            for (int a = 0; a < pointCount - 1; a++)
            {
                multiply[0] = rand.Next(-8, 8);
                multiply[1] = rand.Next(-8, 8);
                multipland -= multiply[0] + multiply[1];
                if (multipland < 1)
                    multipland = 1;
                sludgeVertex[a] = new Point(multiply[0], multiply[1]);
            }
            for (int a = 0; a < sludgeVertex.Length - 1; a++)
            {
                Point p = sludgeVertex[a];
                l.X += p.X;
                l.Y += p.Y;
                sludgePoint.Add(l);
                //layout.SetPixel((int)Math.Round(Clamp(l.X, 0, layout.Width)), (int)Math.Round(Clamp(l.Y, 0, layout.Height)), LerpColor(Color.LightGoldenrodYellow, Color.MediumVioletRed, (float)index / max));
            }
            int loopy = sludgePoint.Count - 1;
            for (int a = 0; a < loopy; a++)
            {
                int get = a == loopy - 1 ? 1 : a + 1;
                Point p1 = sludgePoint[a];
                Point p2 = sludgePoint[get];
                ConnectSine(p1, p2);
            }
            void ConnectSine(Point p1, Point p2)
            {
                int ran;
                float x = 0;
                int[] random = new int[2] { rand.Next(1, 8), rand.Next(1, 8) };
                ran = rand.Next(1, 5);
                int loops = SineLoops(ran, random[0], random[1]);
                for (int b = 0; b < loops; b++)
                {
                    x += (float)Math.PI / 1000;
                    layoutE.SetPixel(CR(Lerp(p1.X, p2.X, (float)b / loops), 0, layoutE.Width), CR(CR(Lerp(p1.Y, p2.Y, ((float)b / loops) + SetSine(ran, random[0], random[1], x)), 0, layoutE.Height), 0, layoutE.Height), Col("Sludge"));
                }
            }
            int SineLoops(int ran, int exponent, int multipand)
            {
                bool[] extrema = new bool[2];
                int a = 0;
                float x = 0;
                float old = 0;
                while (!extrema[0] && old <= SetSine(ran, exponent, multipand, x))
                {
                    old = SetSine(ran, exponent, multipand, x);
                    x += (float)Math.PI / 1000;
                    if (old >= SetSine(ran, exponent, multipand, x))
                        extrema[0] = true;
                    a++;
                }
                while (!extrema[1] && old >= SetSine(ran, exponent, multipand, x))
                {
                    old = SetSine(ran, exponent, multipand, x);
                    x += (float)Math.PI / 1000;
                    if (old <= SetSine(ran, exponent, multipand, x))
                        extrema[0] = true;
                    a++;
                }
                return a;
            }
        }
        static void IonyxCrystals()
        {
            Bitmap decoy = new Bitmap(layout.Width, layout.Height);
            int index = 0;
            Point[] cL = new Point[2] { groundP[rand.Next(groundP.Count)], rand.Next(1) == 0 ? surfaceP[rand.Next(surfaceP.Count)] : lakeP[rand.Next(lakeP.Count)] };
            Point location;
            Point highlight;
            Point[] movement = new Point[5];
            int[] amount = new int[2];
            int capacitate = 125;
            int rO;
            void SetPoints()
            {
                index++;
                switch (index)
                {
                    case 1:
                        {
                            amount = new int[2] { CR(rand.Next(-15, 15) * (float)capacitate / 125, -5, 5), CR(rand.Next(-5, 5) * (float)capacitate / 125, -5, 5) };
                        }
                        break;
                    case 2:
                        {
                            amount = new int[2] { CR(rand.Next(-5, 5) * (float)capacitate / 125, -5, 5), CR(rand.Next(-5, 5) * (float)capacitate / 125, -5, 5) };
                        }
                        break;
                    case 3:
                        {
                            amount = new int[2] { CR(rand.Next(-5, 5) * (float)capacitate / 125, -5, 5), CR(rand.Next(-5, 5) * (float)capacitate / 125, -5, 5) };
                        }
                        break;
                    case 4:
                        {
                            amount = new int[2] { CR(rand.Next(-5, 5) * (float)capacitate / 125, -1, 1), CR(rand.Next(-15, 0) * (float)capacitate / 125, -15, -5) };
                        }
                        break;
                }
            }
            location = new Point(LerpRound(cL[0].X, cL[1].X, 0.5f), LerpRound(cL[0].Y, cL[1].Y, 0.5f));
            highlight = location;
            for (int a = 0; a < movement.Length - 1; a++)
            {
                SetPoints();
                rO = Round(amount[0] - amount[1]);
                capacitate -= rO < 0 ? rO * -1 : rO;
                movement[a] = new Point(amount[0], amount[1]);
            }
            for (int a = 0; a < movement.Length - 1; a++)
            {
                for (int b = 0; b < 100; ++b)
                    decoy.SetPixel(CR(Lerp(highlight.X, highlight.X + movement[a].X, (float)b / 100), 0, layout.Width), CR(CR(Lerp(highlight.Y, highlight.Y + movement[a].Y, (float)b / 100), 0, layout.Height), 0, layout.Height), Col("White"));
                highlight.X += movement[a].X;
                highlight.Y += movement[a].Y;
            }
            for (int b = 0; b < 100; ++b)
                decoy.SetPixel(CR(Lerp(highlight.X, location.X, (float)b / 100), 0, layout.Width), CR(CR(Lerp(highlight.Y, location.Y, (float)b / 100), 0, layout.Height), 0, layout.Height), Col("White"));
            bool successful = false;
            Point fill;
            float region = 0;
            Bitmap decoy2 = new Bitmap(decoy.Width, decoy.Height);
            bool[] noCon = new bool[2];
            while (!successful)
            {
                decoy2 = decoy;
                region += 0.01f;
                fill = new Point(CR(Lerp(highlight.X, location.X, region), 0, layout.Width), CR(Lerp(highlight.Y, location.Y, 1 - region), 0, layout.Height));
                if (decoy2.GetPixel(fill.X, fill.Y) == Col("White"))
                    noCon[0] = true;
                if (!noCon[0])
                {
                    PaintBucket.FloodFill(decoy2, fill, Col("Transparent"), Col("White"));
                    if (decoy2.GetPixel(1, 1) == Col("White"))
                        noCon[1] = true;
                }
                if (region > 0.99f)
                    break;
                if (!noCon[0] && !noCon[1])
                {
                    successful = true;
                    successfulIonyx++;
                }
            }
            if (successful)
                IonyxApply();
            void IonyxApply()
            {
                for (int b = 0; b < decoy2.Height; b++)
                {
                    for (int a = 0; a < decoy2.Width; a++)
                    {
                        if (decoy2.GetPixel(a, b) == Col("White"))
                            layout.SetPixel(a, b, Col("Ionyx"));
                    }
                }
            }
        }
        static float SetSine(int ran, int exponent, int multipand, float x)
        {
            switch (ran)
            {
                case 1:
                    return (float)Math.Sin(x);
                case 2:
                    return (float)Math.Sqrt(Math.Sin(Math.Pow(x, exponent)));
                case 3:
                    return (float)Math.Pow(Math.Sin(x * multipand), exponent);
                case 4:
                    return (float)(0.8515 + Math.Sin(Math.Cos(Math.Sqrt(Math.Pow(x, exponent + 4)))));
            }
            return 0;
        }
        //static Color LerpColor(Color c1, Color c2, float lerp) => Color.FromArgb((int)Math.Round(Lerp(c1.R, c2.R, lerp)), (int)Math.Round(Lerp(c1.G, c2.G, lerp)), (int)Math.Round(Lerp(c1.B, c2.B, lerp)), 255);
        static void DrawStepBottom(Point borderP, bool left = false)
        {
            float lerp = 0;
            Point highlight;
            Point current = borderP;
            Point[] points;
            #region Biome Edges
            for (int a = 0; a < 5; ++a)
            {
                points = GenStepBottom("EdgeBottom", left);
                for (int b = 0; b < points.Length; b++)
                {
                    highlight = current;
                    DrawLine(b);
                    lerp = 0;
                }
            }
            #endregion
            #region Bottom
            curIndex[0] = 5;
            for (int a = 0; a < 6; ++a)
            {
                points = GenStepBottom("Bottom", left);
                for (int b = 0; b < points.Length; b++)
                {
                    highlight = current;
                    DrawLine(b, curIndex[0]);
                    lerp = 0;
                }
            }
            curIndex[1]++;
            if (curIndex[1] == 2)
            {
                groundP.Add(lastCur[0]);
                groundP.Add(lastCur[1]);
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp));
                    layout.SetPixel(CR(current.X, 0, layout.Width - 1), CR(current.Y, 0, layout.Height - 1), Col("Tarrock"));
                }
            }
            #endregion
            void DrawLine(int index, int context = 0)
            {
                if (!groundP.Contains(new Point(highlight.X + points[index].X, highlight.Y + points[index].Y)))
                    groundP.Add(new Point(highlight.X + points[index].X, highlight.Y + points[index].Y));
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(highlight.X, highlight.X + points[index].X, lerp), LerpRound(highlight.Y, highlight.Y + points[index].Y, lerp));
                    layout.SetPixel(CR(current.X, 0, layout.Width - 1), CR(current.Y, 0, layout.Height - 1), Col("Tarrock"));
                }
                if (context == curIndex[0] && context != 0)
                    lastCur[curIndex[1]] = current;

            }
        }
        #endregion
        static float Clamp(float value, float min, float max)
        {
            value = ((value > max) ? max : value);
            value = ((value < min) ? min : value);
            return value;
        }
        /// <summary>
        /// Clamp Round
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static int CR(float value, int min, int max) => (int)Math.Round(Clamp(value, min, max));
        static float Lerp(float v1, float v2, float a) => v1 + (v2 - v1) * a;
        #region Surface shaping Methods
        /// <summary>
        /// Gets the gen steps with the said string
        /// </summary>
        /// <param name="type"></param>
        static Point[] GenStepBottom(string type, bool left = false)
        {
            //Base Operations
            Point[] bO = new Point[1];
            bO = GenSlopeGetBottom(type, bO, left);
            return bO;
        }
        /// <summary>
        /// Calls the methods of setting the gen points (for the bottom)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bO"></param>
        /// <returns></returns>
        static Point[] GenSlopeGetBottom(string type, Point[] bO, bool left)
        {
            switch (type)
            {
                case "EdgeBottom":
                    {
                        int random = rand.Next(1, 6);
                        bO = GenSlopePointsBottom(random, type, bO, left);
                        return bO;
                    }
                case "Bottom":
                    {
                        int random = rand.Next(1, 5);
                        bO = GenSlopePointsBottom(random, type, bO, left);
                        return bO;
                    }
            }
            return bO;
        }
        /// <summary>
        /// Actually sets the points of generation (for the bottom)
        /// </summary>
        /// <param name="type2"></param>
        /// <param name="bO"></param>
        static Point[] GenSlopePointsBottom(int random, string type2, Point[] bO, bool left)
        {
            int r = 1;
            int mult = left ? r : r * -1;
            switch (type2)
            {
                case "EdgeBottom":
                    {
                        switch (random)
                        {
                            //Bump
                            case 1:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 4));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Slight downward left
                                                bO[a] = new Point(RA(-2, -3), rand.Next(1, 3));
                                                break;
                                            case 1:
                                                //sharp downward right partial
                                                bO[a] = new Point(RA(-1, 2), rand.Next(4, 7));
                                                break;
                                            case 2:
                                                //slight downward right
                                                bO[a] = new Point(RA(2, 3), rand.Next(1, 2));
                                                break;
                                            case 3:
                                                //extra movement
                                                bO[a] = new Point(RA(1, 3), rand.Next(1, 3));
                                                break;
                                        }
                                    }
                                }

                                break;
                            //Hook Thorn
                            case 2:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //downward left
                                                bO[a] = new Point(RA(-2, -4), rand.Next(2, 4));
                                                break;
                                            case 1:
                                                //sharp downward left partial
                                                bO[a] = new Point(RA(-2, -1), rand.Next(3, 4));
                                                break;
                                            case 2:
                                                //slight upward right
                                                bO[a] = new Point(RA(0, 2), rand.Next(-3, -2));
                                                break;
                                            case 3:
                                                //sharp right slight vertical
                                                bO[a] = new Point(RA(2, 4), rand.Next(-2, 2));
                                                break;
                                            //Extensions
                                            case 4:
                                                //random movement 1
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-1, 2), rand.Next(0, 2));
                                                break;
                                            case 5:
                                                //random movement 2
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-2, 3), rand.Next(1, 3));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Spike
                            case 3:
                                {
                                    Array.Resize(ref bO, rand.Next(2, 4));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //sharp downward left
                                                bO[a] = new Point(RA(-2, -4), rand.Next(1, 3));
                                                break;
                                            case 1:
                                                //Extra: down dent rand(?)
                                                if (bO.Length > 2 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-2, 0), rand.Next(1, 2));
                                                break;
                                            case 2:
                                                //Return point
                                                bO[a] = new Point(RA(-2, -4), rand.Next(-2, 3));
                                                break;
                                            case 3:
                                                //Extra dent
                                                if (rand.Next(1) == 0)
                                                    bO[a] = new Point(RA(-2, 2), rand.Next(-2, 1));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Chipped Slope
                            case 4:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Slight downward right
                                                bO[a] = new Point(RA(-1, 2), rand.Next(1, 2));
                                                break;
                                            case 1:
                                                //sharp downward left partial
                                                bO[a] = new Point(RA(-2, 2), rand.Next(2, 4));
                                                break;
                                            case 2:
                                                //slight downward left
                                                bO[a] = new Point(RA(-2, -4), rand.Next(1, 2));
                                                break;
                                            case 3:
                                                //sharp downward right
                                                bO[a] = new Point(RA(-1, 2), rand.Next(2, 5));
                                                break;
                                            case 4:
                                                //Extra movement
                                                if (rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-2, 1), rand.Next(1, 2));
                                                break;
                                        }
                                    }
                                }

                                break;
                            //Slight dent
                            case 5:
                                {
                                    Array.Resize(ref bO, rand.Next(2, 4));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //very slight movement downward
                                                bO[a] = new Point(RA(-1, 1), rand.Next(1, 3));
                                                break;
                                            case 1:
                                                //cave in slight
                                                bO[a] = new Point(RA(1, 2), rand.Next(0, 2));
                                                break;
                                            case 2:
                                                //downward right
                                                bO[a] = new Point(RA(-1, 3), rand.Next(2, 4));
                                                break;
                                            case 3:
                                                //Extra movement
                                                if (rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-1, 1), rand.Next(1, 2));
                                                break;
                                            case 4:
                                                //Extra movement
                                                if (rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-2, 3), rand.Next(1, 3));
                                                break;
                                        }
                                    }
                                }

                                break;
                        }
                    }
                    break;
                case "Bottom":
                    {
                        switch (random)
                        {
                            //Bump
                            case 1:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //downward right
                                                bO[a] = new Point(RA(1, 3), rand.Next(1, 3));
                                                break;
                                            case 1:
                                                //sharp right slight down
                                                bO[a] = new Point(RA(4, 6), rand.Next(1, 2));
                                                break;
                                            case 2:
                                                //upward right
                                                bO[a] = new Point(RA(1, 3), rand.Next(-1, 1));
                                                break;
                                            case 3:
                                                if (rand.Next(1) == 0)
                                                    //extra down
                                                    bO[a] = new Point(RA(2, 4), rand.Next(1, 2));
                                                break;
                                            case 4:
                                                if (rand.Next(2) == 0)
                                                    //extra up
                                                    bO[a] = new Point(RA(1, 2), rand.Next(-1, 1));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Cave in
                            case 2:
                                {
                                    Array.Resize(ref bO, rand.Next(4, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //upward right
                                                bO[a] = new Point(RA(2, 4), rand.Next(-1, 2));
                                                break;
                                            case 1:
                                                //right slight up
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                            case 2:
                                                //down right
                                                bO[a] = new Point(RA(2, 4), rand.Next(1, 2));
                                                break;
                                            case 3:
                                                //slight up right
                                                bO[a] = new Point(RA(1, 3), rand.Next(-1, 1));
                                                break;
                                            case 4:
                                                if (rand.Next(2) == 0)
                                                    //extra up
                                                    bO[a] = new Point(RA(2, 3), rand.Next(-1, 2));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Geometric formation
                            case 3:
                                {
                                    Array.Resize(ref bO, rand.Next(4, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //slight down
                                                bO[a] = new Point(RA(1, 3), rand.Next(1, 2));
                                                break;
                                            case 1:
                                                //sharp up direction
                                                bO[a] = new Point(RA(-3, 3), rand.Next(2, 5));
                                                break;
                                            case 2:
                                                //downward right
                                                bO[a] = new Point(RA(2, 4), rand.Next(1, 2));
                                                break;
                                            case 3:
                                                //upward right
                                                bO[a] = new Point(RA(1, 3), rand.Next(-1, 2));
                                                break;
                                            case 4:
                                                //extra connection
                                                bO[a] = new Point(RA(2, 4), rand.Next(1, 3));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Spikes
                            case 4:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //slight left down
                                                bO[a] = new Point(RA(1, 3), rand.Next(-1, 2));
                                                break;
                                            case 1:
                                                //sharp down direction
                                                bO[a] = new Point(RA(-1, 2), rand.Next(2, 5));
                                                break;
                                            case 2:
                                                //sharp up direction
                                                bO[a] = new Point(RA(1, 3), rand.Next(-3, -1));
                                                break;
                                            case 3:
                                                //extra down
                                                bO[a] = new Point(RA(1, 3), rand.Next(2, 3));
                                                break;
                                            case 4:
                                                //extra up
                                                bO[a] = new Point(RA(0, 2), rand.Next(-1, 1));
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
            return bO;
            int RA(int a, int b)
            {
                int c = a * mult;
                int d = b * mult;
                if (c < d)
                    return rand.Next(c, d);
                return rand.Next(d, c);
            }
        }
        static void DrawStepSurface(Point[] border)
        {
            float lerp = 0;
            Point highlight = new Point();
            Point current;
            bool lake = false;
            Slot[] operations = new Slot[12];
            for (int a = 0; a < operations.Length; a++)
                operations[a] = new Slot(rand, 1, 5);
            operations[5].task = 5;
            operations[7].task = 5;
            for (int a = 0; a < operations.Length; a++)
                operations[a].SurfacePoints(rand);
            current = border[0];
            for (int a = 0; a < operations.Length; a++)
            {
                if (operations[a].task == 5)
                    curIndex[0]++;
                for (int b = 0; b < operations[a].slots.Length; b++)
                {
                    highlight = current;
                    DrawLine(operations[a].slots[b], operations[a].task == 5 ? 1 : 0);
                    lerp = 0;
                }
                if (curIndex[0] > 0)
                    break;
            }
            lake = true;
            for (int a = 0; a < operations.Length; a++)
            {
                if (operations[a].task == 5)
                {
                    for (int b = 0; b < operations[a].slots.Length; b++)
                    {
                        if (operations[a].slots[b].Y > 0 && resevoirPoints[0] == new Point())
                            resevoirPoints[0] = new Point(highlight.X + operations[a].slots[b].X, highlight.X + operations[a].slots[b].Y);
                        highlight = current;
                        DrawLine(operations[a].slots[b], operations[a].task == 5 ? 1 : 0);
                        lerp = 0;
                    }
                }
            }
            lake = false;
            lastCur[0] = current;
            current = border[1];
            for (int a = operations.Length - 1; a > 0; a--)
            {
                for (int b = 0; b < operations[a].slots.Length; b++)
                {
                    if (operations[a].task == 5 && operations[a].slots[b].Y > 0 && resevoirPoints[0] == new Point())
                        resevoirPoints[1] = new Point(highlight.X + operations[a].slots[b].X, highlight.X + operations[a].slots[b].Y);
                    highlight = current;
                    DrawLine(operations[a].slots[b], operations[a].task == 5 ? 1 : 0);
                    lerp = 0;
                }
                if (operations[a].task == 5)
                    break;
            }
            lastCur[1] = current;
            {
                surfaceP.Add(lastCur[0]);
                surfaceP.Add(lastCur[1]);
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp));
                    layout.SetPixel(CR(current.X, 0, layout.Width - 1), CR(current.Y, 0, layout.Height - 1), Col("Tarrock"));
                    if (lerp < 0.99)
                        Slope(current, new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp + 0.01f), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp + 0.01f)));
                }
            }
            void DrawLine(Point point, int context)
            {
                Point use = point;
                if (curIndex[0] > 0 && !lake)
                    use.X *= -1;
                if (context != 1 && !surfaceP.Contains(new Point(highlight.X + use.X, highlight.Y + use.Y)))
                    surfaceP.Add(new Point(highlight.X + use.X, highlight.Y + use.Y));
                else if (!lakeP.Contains(new Point(highlight.X + use.X, highlight.Y + use.Y)))
                    lakeP.Add(new Point(highlight.X + use.X, highlight.Y + use.Y));
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(highlight.X, highlight.X + use.X, lerp), LerpRound(highlight.Y, highlight.Y + use.Y, lerp));
                    layout.SetPixel(CR(current.X, 0, layout.Width - 1), CR(current.Y, 0, layout.Height - 1), Col("Tarrock"));
                    if (lerp < 0.99)
                        Slope(current, new Point(LerpRound(highlight.X, highlight.X + use.X, lerp + 0.01f), LerpRound(highlight.Y, highlight.Y + use.Y, lerp + 0.01f)));
                }
            }
        }
        /*static void MarkPoints()
        {
            foreach (Point p in groundP)
                layout.SetPixel(CR(p.X, 0, layout.Width - 1), CR(p.Y, 0, layout.Height - 1), Color.Red);
            foreach (Point p in surfaceP)
                layout.SetPixel(CR(p.X, 0, layout.Width - 1), CR(p.Y, 0, layout.Height - 1), Color.Yellow);
            foreach (Point p in lakeP)
                layout.SetPixel(CR(p.X, 0, layout.Width - 1), CR(p.Y, 0, layout.Height - 1), Color.SkyBlue);
        }*/

        #endregion
        #region Ceiling Methods
        static void DrawStepCeilingTop(Point borderP, bool left = false)
        {
            float lerp = 0;
            Point highlight;
            Point current = borderP;
            Point[] points;
            #region Biome Edges
            for (int a = 0; a < 1; ++a)
            {
                points = GenStepTop("EdgeTop", left);
                for (int b = 0; b < points.Length; b++)
                {
                    highlight = current;
                    DrawLine(b);
                    lerp = 0;
                }
            }
            #endregion
            #region Bottom
            curIndex[0] = 5;
            for (int a = 0; a < rand.Next(3, 4); ++a)
            {
                points = GenStepTop("Top", left);
                for (int b = 0; b < points.Length; b++)
                {
                    highlight = current;
                    DrawLine(b, curIndex[0]);
                    lerp = 0;
                }
            }
            curIndex[1]++;
            if (curIndex[1] == 2)
            {
                topP.Add(lastCur[0]);
                topP.Add(lastCur[1]);
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp));
                    layoutTop.SetPixel(CR(current.X, 0, layoutTop.Width - 1), CR(current.Y, 0, layoutTop.Height - 1), Col("Tarrock"));
                }
            }
            #endregion
            void DrawLine(int index, int context = 0)
            {
                if (!topP.Contains(new Point(highlight.X + points[index].X, highlight.Y + points[index].Y)))
                    topP.Add(new Point(highlight.X + points[index].X, highlight.Y + points[index].Y));
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(highlight.X, highlight.X + points[index].X, lerp), LerpRound(highlight.Y, highlight.Y + points[index].Y, lerp));
                    layoutTop.SetPixel(CR(current.X, 0, layoutTop.Width - 1), CR(current.Y, 0, layoutTop.Height - 1), Col("Tarrock"));
                }
                if (context == curIndex[0] && context != 0)
                    lastCur[curIndex[1]] = current;

            }
        }
        static void DrawStepCeiling(Point[] border)
        {
            float lerp = 0;
            Point highlight;
            Point current;
            Slot[] operations = new Slot[9];
            for (int a = 0; a < operations.Length; a++)
                operations[a] = new Slot(rand, 1, 5);
            for (int a = 0; a < operations.Length; a++)
                operations[a].CeilingPoints(rand);
            current = border[0];
            for (int a = 0; a < operations.Length; a++)
            {
                for (int b = 0; b < operations[a].slots.Length; b++)
                {
                    highlight = current;
                    DrawLine(operations[a].slots[b]);
                    lerp = 0;
                }
            }
            curIndex[0]++;
            for (int a = 0; a < operations.Length; a++)
                operations[a].CeilingPoints(rand);
            lastCur[0] = current;
            current = border[1];
            for (int a = operations.Length - 1; a > 0; a--)
            {
                for (int b = 0; b < operations[a].slots.Length; b++)
                {
                    highlight = current;
                    DrawLine(operations[a].slots[b]);
                    lerp = 0;
                }
            }
            lastCur[1] = current;
            {
                ceilingP.Add(lastCur[0]);
                ceilingP.Add(lastCur[1]);
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp));
                    layoutTop.SetPixel(CR(current.X, 0, layoutTop.Width - 1), CR(current.Y, 0, layoutTop.Height - 1), Col("Tarrock"));
                    if (lerp < 0.99)
                        Slope(current, new Point(LerpRound(lastCur[0].X, lastCur[1].X, lerp + 0.01f), LerpRound(lastCur[0].Y, lastCur[1].Y, lerp + 0.01f)), true);
                }
            }
            void DrawLine(Point point)
            {
                Point use = point;
                if (curIndex[0] > 0)
                    use.X *= -1;
                if (!ceilingP.Contains(new Point(highlight.X + use.X, highlight.Y + use.Y)))
                    ceilingP.Add(new Point(highlight.X + use.X, highlight.Y + use.Y));
                for (int a = 0; a < 100; ++a)
                {
                    lerp += 0.01f;
                    current = new Point(LerpRound(highlight.X, highlight.X + use.X, lerp), LerpRound(highlight.Y, highlight.Y + use.Y, lerp));
                    layoutTop.SetPixel(CR(current.X, 0, layoutTop.Width - 1), CR(current.Y, 0, layoutTop.Height - 1), Col("Tarrock"));
                    if (lerp < 0.99)
                        Slope(current, new Point(LerpRound(highlight.X, highlight.X + use.X, lerp + 0.01f), LerpRound(highlight.Y, highlight.Y + use.Y, lerp + 0.01f)), true);
                }
            }
        }
        #endregion
        #region Ceiling shaping Methods
        /// <summary>
        /// Gets the gen steps with the said string
        /// </summary>
        /// <param name="type"></param>
        static Point[] GenStepTop(string type, bool left = false)
        {
            //Base Operations
            Point[] bO = new Point[1];
            bO = GenSlopeGetTop(type, bO, left);
            return bO;
        }
        /// <summary>
        /// Calls the methods of setting the gen points (for the bottom)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bO"></param>
        /// <returns></returns>
        static Point[] GenSlopeGetTop(string type, Point[] bO, bool left)
        {
            switch (type)
            {
                case "EdgeTop":
                    {
                        int random = rand.Next(1, 4);
                        bO = GenSlopePointsTop(random, type, bO, left);
                        return bO;
                    }
                case "Top":
                    {
                        int random = rand.Next(1, 4);
                        bO = GenSlopePointsTop(random, type, bO, left);
                        return bO;
                    }
            }
            return bO;
        }
        /// <summary>
        /// Actually sets the points of generation (for the bottom)
        /// </summary>
        /// <param name="type2"></param>
        /// <param name="bO"></param>
        static Point[] GenSlopePointsTop(int random, string type2, Point[] bO, bool left)
        {
            int r = 1;
            int mult = left ? r : r * -1;
            switch (type2)
            {
                case "EdgeTop":
                    {
                        switch (random)
                        {
                            //Double Slope
                            case 1:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 4));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Slight upward left
                                                bO[a] = new Point(RA(-2, -2), rand.Next(-3, -2));
                                                break;
                                            case 1:
                                                //Upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                            case 2:
                                                //Slight upward left
                                                bO[a] = new Point(RA(-2, -2), rand.Next(-3, -2));
                                                break;
                                            case 3:
                                                //Extra upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Monospike Slope
                            case 2:
                                {
                                    Array.Resize(ref bO, rand.Next(4, 6));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                            case 1:
                                                //upward left
                                                bO[a] = new Point(RA(-3, -1), rand.Next(-3, -2));
                                                break;
                                            case 2:
                                                //slight upward right
                                                bO[a] = new Point(RA(1, 3), rand.Next(-3, -2));
                                                break;
                                            case 3:
                                                //upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                            //Extensions
                                            case 4:
                                                //Extra upward left
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-3, -1), rand.Next(-3, -2));
                                                break;
                                            case 5:
                                                //extra slight upward right
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(1, 3), rand.Next(-3, -2));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Spiked Slope
                            case 3:
                                {
                                    Array.Resize(ref bO, rand.Next(4, 6));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                            case 1:
                                                //slight upward left
                                                bO[a] = new Point(RA(-3, -1), rand.Next(-2, -1));
                                                break;
                                            case 2:
                                                //slight upward right
                                                bO[a] = new Point(RA(2, 4), rand.Next(-2, -1));
                                                break;
                                            case 3:
                                                //upward
                                                bO[a] = new Point(RA(-1, 0), rand.Next(-4, -2));
                                                break;
                                            //Extensions
                                            case 4:
                                                //Extra upward left
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(-3, -1), rand.Next(-2, -1));
                                                break;
                                            case 5:
                                                //extra slight upward right
                                                if (bO.Length > 3 && rand.Next(2) == 0)
                                                    bO[a] = new Point(RA(2, 4), rand.Next(-2, -1));
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case "Top":
                    {
                        switch (random)
                        {
                            //Flat Slope
                            case 1:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //Flat
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                            case 1:
                                                //Flat
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                            case 2:
                                                //Flat
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                            case 3:
                                                if (rand.Next(1) == 0)
                                                    //Flat
                                                    bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                            case 4:
                                                if (rand.Next(2) == 0)
                                                    //Flat
                                                    bO[a] = new Point(RA(3, 5), rand.Next(-1, 1));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Bumpy Slope
                            case 2:
                                {
                                    Array.Resize(ref bO, rand.Next(3, 5));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //upward right
                                                bO[a] = new Point(RA(2, 4), rand.Next(-4, -2));
                                                break;
                                            case 1:
                                                //right down
                                                bO[a] = new Point(RA(3, 5), rand.Next(2, 4));
                                                break;
                                            case 2:
                                                //slight upward right
                                                bO[a] = new Point(RA(2, 3), rand.Next(-3, -1));
                                                break;
                                            case 3:
                                                //Extra slight up right
                                                bO[a] = new Point(RA(2, 4), rand.Next(1, 3));
                                                break;
                                            case 4:
                                                if (rand.Next(2) == 0)
                                                    //extra up
                                                    bO[a] = new Point(RA(2, 4), rand.Next(-4, -2));
                                                break;
                                        }
                                    }
                                }
                                break;
                            //Cave in
                            case 3:
                                {
                                    Array.Resize(ref bO, rand.Next(4, 6));
                                    for (int a = 0; a < bO.Length; a++)
                                    {
                                        switch (a)
                                        {
                                            case 0:
                                                //slight up right
                                                bO[a] = new Point(RA(2, 4), rand.Next(-2, -1));
                                                break;
                                            case 1:
                                                //Flat
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 2));
                                                break;
                                            case 2:
                                                //downward right
                                                bO[a] = new Point(RA(2, 4), rand.Next(2, 5));
                                                break;
                                            case 3:
                                                //upward right
                                                bO[a] = new Point(RA(3, 5), rand.Next(-3, -1));
                                                break;
                                            case 4:
                                                //extra slight up right
                                                bO[a] = new Point(RA(2, 4), rand.Next(-2, -1));
                                                break;
                                            case 5:
                                                //extra flat
                                                bO[a] = new Point(RA(3, 5), rand.Next(-1, 2));
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
            return bO;
            int RA(int a, int b)
            {
                int c = a * mult;
                int d = b * mult;
                if (c < d)
                    return rand.Next(c, d);
                return rand.Next(d, c);
            }
        }
        #endregion
        static float Length(Point point)
        {
            float num = point.X * point.X + point.Y * point.Y;
            return (float)Math.Sqrt(num);
        }
        static int Round(float f) => (int)Math.Round(f);
        static int LerpRound(float f1, float f2, float scale) => Round(Lerp(f1, f2, scale));
        //static bool InBetween(float f1, float f2, float input) => input > f1 && input < f2;
        static void Slope(Point p1, Point p2, bool ceiling = false)
        {
            if (p2.X > p1.X)
            {
                if (p2.Y > p1.Y)
                    layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Yellow : Color.Green);
                else if (p2.Y < p1.Y)
                    layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Green : Color.Yellow);
            }
            if (p2.X < p1.X)
            {
                if (p2.Y > p1.Y)
                    layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Green : Color.Yellow);
                else if (p2.Y < p1.Y)
                    layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Yellow : Color.Green);
            }
                if (ceiling)
            {
                if (p2.X < p1.X)
                {
                    if (p2.Y > p1.Y)
                        layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Red : Color.Orange);
                    else if (p2.Y < p1.Y)
                        layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Orange : Color.Red);
                }
                if (p2.X > p1.X)
                {
                    if (p2.Y > p1.Y)
                        layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Orange : Color.Red);
                    else if (p2.Y < p1.Y)
                        layoutSloping.SetPixel(p2.X, p2.Y, ceiling ? Color.Red : Color.Orange);
                }
            }
        }
        static void Message(GenerationProgress progress, string message, int count, int total) { progress.Message = $"Plotting the plague... ({count} / {total}) - {message}"; }
    }
    class Slot
    {
        public Point[] slots;
        public int task;
        public Slot(Random rand, int min, int max)
        {
            task = rand.Next(min, max);
        }
        public void SurfacePoints(Random rand)
        {
            switch (task)
            {
                //Slope
                case 1:
                    {
                        Array.Resize(ref slots, rand.Next(3, 4));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //slight up right
                                    slots[a] = P(R(2, 4), R(-1, 2));
                                    break;
                                case 1:
                                    //sort of flat
                                    slots[a] = P(R(3, 5), R(-1, 1));
                                    break;
                                case 2:
                                    //slight down
                                    slots[a] = P(R(2, 4), R(0, 1));
                                    break;
                                case 3:
                                    //extra flat
                                    slots[a] = P(R(2, 4), R(-1, 1));
                                    break;
                            }
                        }
                    }
                    break;
                //Spiked slope
                case 2:
                    {
                        Array.Resize(ref slots, rand.Next(3, 5));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //slight up right
                                    slots[a] = P(R(1, 3), R(-1, 1));
                                    break;
                                case 1:
                                    //down
                                    slots[a] = P(R(2, 3), R(1, 2));
                                    break;
                                case 2:
                                    //flat
                                    slots[a] = P(R(-1, 2), R(-1, 1));
                                    break;
                                case 3:
                                    //extra spike
                                    slots[a] = P(R(1, 3), R(-1, 2));
                                    break;
                                case 4:
                                    //extra down
                                    slots[a] = P(R(2, 3), R(1, 2));
                                    break;
                            }
                        }
                    }
                    break;
                //Flat slope
                case 3:
                    {
                        Array.Resize(ref slots, rand.Next(2, 4));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //flat 1
                                    slots[a] = P(R(3, 5), R(-1, 1));
                                    break;
                                case 1:
                                    //sort of flat
                                    slots[a] = P(R(2, 6), R(-2, 1));
                                    break;
                                case 2:
                                    //extra flat 2
                                    slots[a] = P(R(3, 5), R(-1, 1));
                                    break;
                                case 3:
                                    //extra semi flat
                                    slots[a] = P(R(2, 6), R(-1, 2));
                                    break;
                            }
                        }
                    }
                    break;
                //Stalagmite
                case 4:
                    {
                        Array.Resize(ref slots, rand.Next(3, 5));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //spike up
                                    slots[a] = P(R(1, 3), R(-5, -2));
                                    break;
                                case 1:
                                    //spike down
                                    slots[a] = P(R(1, 3), R(2, 4));
                                    break;
                                case 2:
                                    //flat
                                    slots[a] = P(R(2, 4), R(-1, 1));
                                    break;
                                case 3:
                                    //extra spike up
                                    slots[a] = P(R(1, 3), R(-5, -2));
                                    break;
                                case 4:
                                    //extra spike down
                                    slots[a] = P(R(1, 3), R(2, 4));
                                    break;
                            }
                        }
                    }
                    break;
                //Lake
                case 5:
                    {
                        float sine = (float)Math.PI / 2;
                        float mult;
                        int length = 7;
                        Array.Resize(ref slots, length);
                        for (int a = 0; a < length; a++)
                        {
                            sine -= (float)Math.PI / length / 2;
                            mult = (float)Math.Sin(sine);
                            slots[a] = new Point((int)Math.Round(R(3, 4) * mult), (int)Math.Round(1.5f * mult));
                        }
                    }
                    break;
            }
            int R(int sl, int bg) => rand.Next(sl, bg);
        }
        public void CeilingPoints(Random rand)
        {
            switch (task)
            {
                //Flat slope
                case 1:
                    {
                        Array.Resize(ref slots, rand.Next(3, 5));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //slight down right
                                    slots[a] = P(R(2, 3), R(1, 2));
                                    break;
                                case 1:
                                    //slight up right
                                    slots[a] = P(R(2, 4), R(-2, -1));
                                    break;
                                case 2:
                                    //slight down right
                                    slots[a] = P(R(1, 3), R(1, 2));
                                    break;
                                case 3:
                                    //extra flat
                                    slots[a] = P(R(2, 3), R(-1, 1));
                                    break;
                                case 4:
                                    //slight up right
                                    slots[a] = P(R(2, 3), R(-2, -1));
                                    break;
                            }
                        }
                    }
                    break;
                //Cave in
                case 2:
                    {
                        Array.Resize(ref slots, rand.Next(3, 5));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //up right
                                    slots[a] = P(R(1, 3), R(-4, -2));
                                    break;
                                case 1:
                                    //flat
                                    slots[a] = P(R(2, 3), R(-2, 1));
                                    break;
                                case 2:
                                    //slight down
                                    slots[a] = P(R(2, 4), R(1, 2));
                                    break;
                                case 3:
                                    //extra flat
                                    slots[a] = P(R(1, 4), R(-2, 2));
                                    break;
                                case 4:
                                    //extra down
                                    slots[a] = P(R(2, 3), R(-1, 1));
                                    break;
                            }
                        }
                    }
                    break;
                //Stalagtite
                case 3:
                    {
                        Array.Resize(ref slots, rand.Next(5, 6));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //slight down right
                                    slots[a] = P(R(1, 3), R(1, 2));
                                    break;
                                case 1:
                                    //sharp down
                                    slots[a] = P(R(1, 3), R(2, 5));
                                    break;
                                case 2:
                                    //random tip direction
                                    slots[a] = P(R(-2, 2), R(1, 3));
                                    break;
                                case 3:
                                    //extra semi flat
                                    slots[a] = P(R(2, 3), R(-1, 2));
                                    break;
                                case 4:
                                    //up right
                                    slots[a] = P(R(2, 4), R(-4, -2));
                                    break;
                                case 5:
                                    //extra random movement
                                    slots[a] = P(R(2, 3), R(-3, 2));
                                    break;
                            }
                        }
                    }
                    break;
                //Lesser Stalagtite
                case 4:
                    {
                        Array.Resize(ref slots, rand.Next(4, 5));
                        for (int a = 0; a < slots.Length; a++)
                        {
                            switch (a)
                            {
                                case 0:
                                    //slight down right
                                    slots[a] = P(R(1, 2), R(0, 1));
                                    break;
                                case 1:
                                    //sharp down
                                    slots[a] = P(R(1, 2), R(2, 3));
                                    break;
                                case 2:
                                    //random tip direction
                                    slots[a] = P(R(-1, 2), R(1, 2));
                                    break;
                                case 3:
                                    //extra semi flat
                                    slots[a] = P(R(1, 2), R(-1, 1));
                                    break;
                                case 4:
                                    //up right
                                    slots[a] = P(R(1, 3), R(-2, -1));
                                    break;
                            }
                        }
                    }
                    break;
            }
            int R(int sl, int bg) => rand.Next(sl, bg);
        }
        public Point P(int a, int b) => new Point(a, b);
    }
}
