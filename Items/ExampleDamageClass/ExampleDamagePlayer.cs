using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class ExampleDamagePlayer : ModPlayer
    {
        public static ExampleDamagePlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<ExampleDamagePlayer>();
        }

        public float exampleDamageAdd;
        public float exampleDamageMult = 1f;
        public float exampleKnockback;
        public int exampleCrit;

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            exampleDamageAdd = 0f;
            exampleDamageMult = 1f;
            exampleKnockback = 0f;
            exampleCrit = 7;
        }
    }
}