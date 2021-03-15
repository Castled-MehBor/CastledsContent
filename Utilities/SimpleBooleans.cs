using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Utilities
{
    public struct BoolHelp
    {
        /// <summary>
        /// Determines if a given entity exists (isn't null and is active)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Exists(Entity e) => e != null && e.active;
        public bool IsNPC(Entity e) => Exists(e) && e is NPC;
        public bool IsPlayer(Entity e) => Exists(e) && e is Player;
        public bool IsItem(Entity e) => Exists(e) && e is Item;
        private NPC GetNPC(Entity e)
        {
            if (IsNPC(e))
            {
                if (e is NPC n)
                    return n;
            }
            return null;
        }
        private Player GetPlayer(Entity e)
        {
            if (IsPlayer(e))
            {
                if (e is Player n)
                    return n;
            }
            return null;
        }
        private Item GetItem(Entity e)
        {
            if (IsItem(e))
            {
                if (e is Item n)
                    return n;
            }
            return null;
        }
        /// <summary>
        /// Has Less Than: used for determining if an entity (NPC or Player) has less health than their maxHealth stat times lessThan. Returns an error message if the Entity provided isn't an NPC, nor a Player
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lessThan"></param>
        /// <returns></returns>
        public bool HLT(Entity e, float lessThan)
        {
            if (IsNPC(e))
            {
                NPC n = GetNPC(e);
                return n.life < n.lifeMax * lessThan;
            }
            else if (IsPlayer(e))
            {
                Player n = GetPlayer(e);
                return n.statLife < n.statLifeMax * lessThan;
            }
            else if (!IsNPC(e) && !IsPlayer(e))
            {
                DisplayError("The entity that has been set is not an NPC, nor a Player", "HLT");
                return false;
            }
            return false;
        }
        /// <summary>
        /// Has Buff: used for determining if an entity (NPC or Player) has a specific buff. Returns an error message if the Entity provided isn't an NPC, nor a Player    
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffIndex"></param>
        /// <returns></returns>
        public bool HB(Entity e, int buffIndex)
        {
            if (IsNPC(e))
            {
                NPC n = GetNPC(e);
                return n.HasBuff(buffIndex);
            }
            else if (IsPlayer(e))
            {
                Player n = GetPlayer(e);
                return n.HasBuff(buffIndex);
            }
            else if (!IsNPC(e) && !IsPlayer(e))
            {
                DisplayError("The entity that has been set is not an NPC, nor a Player", "HB");
                return false;
            }
            return false;
        }
        /// <summary>
        /// Returns if an NPC's type is included in types.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool NPCIs(NPC n, List<int> types) => types.Contains(n.type);
        /// <summary>
        /// Main Array Is: if type is 1 - Main.npc | if type is 2 - Main.player | if type is 3 - Main.item
        /// </summary>
        /// <param name="thing"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool MArrayIs(int type, bool condition)
        {
            switch (type)
            {
                case 1:
                    {
                        foreach (NPC n in Main.npc)
                        {
                            if (Exists(n) && condition)
                                return true;
                        }
                        return false;
                    }
                case 2:
                    {
                        foreach (Player p in Main.player)
                        {
                            if (Exists(p) && condition)
                                return true;
                        }
                        return false;
                    }
                case 3:
                    {
                        foreach (Item i in Main.item)
                        {
                            if (Exists(i) && condition)
                                return true;
                        }
                        return false;
                    }
            }
            return false;
        }
        /// <summary>
        /// Checks if an entity is in a range. low and high should generally have different values, for example, low subtracts X and Y by the same value that high adds to X and Y.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="low"></param>
        /// <param name="High"></param>
        /// <param name="equal"></param>
        /// <returns></returns>
        public bool InRange(Entity e, Vector2 low, Vector2 high) => Exists(e) && e.position.X > low.X && e.position.X < high.X && e.position.Y > low.Y && e.position.Y < high.Y;
        private void DisplayError(string message, string hook)
        {
            Main.NewText($"C.E.E. from {hook}: {message}", Color.OrangeRed);
        }
    }
}
