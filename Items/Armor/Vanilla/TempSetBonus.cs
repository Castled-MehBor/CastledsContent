using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace CastledsContent.Items.Armor.Vanilla
{
    public class TempSetBonus
    {
        public void BuffPlayer(Player player, string setBonus)
        {
            if (setBonus.Contains("Shadow"))
            {
                if (setBonus.Contains("Melee"))
                {
                    player.meleeDamage += HorVel();
                    player.meleeSpeed += VerVel();
                }
                if (setBonus.Contains("Ranged"))
                {
                    player.rangedDamage += HorVel();
                    if (VerVel() > 0.25f)
                        player.ammoCost75 = true;
                }
                if (setBonus.Contains("Mage"))
                {
                    player.magicDamage += HorVel();
                    player.manaCost -= VerVel();
                }
                if (setBonus.Contains("Summon"))
                {
                    player.minionDamage += HorVel();
                    player.endurance += VerVel() / 2;
                }
            }
            if (setBonus.Contains("Crimson"))
            {
                if (setBonus.Contains("Melee"))
                {
                    player.meleeDamage += LifeLost();
                    player.meleeSpeed += player.lifeRegen / 3;
                }
                if (setBonus.Contains("Ranged"))
                {
                    player.rangedDamage += LifeLost();
                    if (player.lifeRegen >= 5)
                        player.ammoCost75 = true;
                }
                if (setBonus.Contains("Mage"))
                {
                    player.magicDamage += LifeLost();
                    player.manaCost -= player.lifeRegen / 50;
                }
                if (setBonus.Contains("Summon"))
                {
                    player.minionDamage += LifeLost();
                    player.endurance += player.lifeRegen / 50;
                }
            }
            float HorVel() => Math.Abs(player.velocity.X) / 30f;
            float VerVel() => Math.Abs(player.velocity.Y) / 30f;
            float LifeLost() => (float)(player.statLifeMax2 - player.statLife) / player.statLifeMax2;
        }
    }
}
