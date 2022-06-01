using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CastledsContent
{
    public static class Helpers
    {
        /// <summary>
        /// Finds the closest npc to the specified position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>The NPC index + 1, if no NPC is found, 0 is returned</returns>
        public static int ClosestHostileNPCTo(Vector2 position, float minDistance = -1)
        {
            int npcs = Main.npc.Length - 1;
            float closestDistance = -1;
            int npct = 0;
            for (int i = 0; i < npcs; i++)
            {
                NPC npc = Main.npc[i];

                float distSQ = npc.DistanceSQ(position);
                if (!(npc?.active is true && npc.CanBeChasedBy() && (minDistance == -1 || npc.WithinRange(position, minDistance))))
                    continue;

                if (closestDistance == -1 || distSQ < closestDistance)
                {
                    closestDistance = distSQ;
                    npct = npc.whoAmI + 1;
                }
            }
            return npct;
        }

        public static int NewNPC<T>(int X, int Y, int Type, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => NPC.NewNPC(X, Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target);
        public static int NewNPC<T>(out T modnpc, int X, int Y, int Type, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC
        {
            int npc = NPC.NewNPC(X, Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target);
            modnpc = Main.npc[npc].modNPC as T;
            return npc;

        }

        public static NPC NewNPCDirect<T>(Point position, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => Main.npc[NPC.NewNPC(position.X, position.Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target)];
        public static NPC NewNPCDirect<T>(Vector2 position, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => Main.npc[NPC.NewNPC((int)position.X, (int)position.Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target)];
        public static NPC NewNPCDirect<T>(int X, int Y, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => Main.npc[NPC.NewNPC(X, Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target)];
        public static NPC NewNPCDirect<T>(out T modNPC, Point position, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => NewNPCDirect(out modNPC, position.X, position.Y, Start, ai0, ai1, ai2, ai3, Target);
        public static NPC NewNPCDirect<T>(out T modNPC, Vector2 position, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC => NewNPCDirect(out modNPC, (int)position.X, (int)position.Y, Start, ai0, ai1, ai2, ai3, Target);
        public static NPC NewNPCDirect<T>(out T modNPC, int X, int Y, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int Target = 255) where T : ModNPC
        {
            NPC npc = Main.npc[NPC.NewNPC(X, Y, ModContent.NPCType<T>(), Start, ai0, ai1, ai2, ai3, Target)];
            modNPC = npc.modNPC as T;
            return npc;
        }
        #region Primitive Testing
        public static bool HasParameter(this Effect effect, string parameterName)
        {
            foreach (EffectParameter parameter in effect.Parameters)
            {
                if (parameter.Name == parameterName)
                {
                    return true;
                }
            }

            return false;
        }
        public static void SetEffectMatrices(ref Effect effect)
        {
            GetWorldViewProjection(out Matrix view, out Matrix projection);

            if (effect.HasParameter("WorldViewProjection"))
                effect.Parameters["WorldViewProjection"].SetValue(view * projection);
        }
        public static void GetWorldViewProjection(out Matrix view, out Matrix projection) => GetWorldViewProjection(Main.GameViewMatrix.Zoom, out view, out projection);
        public static void GetWorldViewProjection(Vector2 zoom, out Matrix view, out Matrix projection)
        {
            int width = Main.graphics.GraphicsDevice.Viewport.Width;
            int height = Main.graphics.GraphicsDevice.Viewport.Height;

            view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) *
                          Matrix.CreateTranslation(width / 2f, height / -2f, 0) * Matrix.CreateRotationZ(MathHelper.Pi) *
                          Matrix.CreateScale(zoom.X, zoom.Y, 1f);

            projection = Matrix.CreateOrthographic(width, height, 0, 1000);
        }
        #endregion
    }
}