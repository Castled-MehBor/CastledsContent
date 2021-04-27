using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace CastledsContent.Sounds.Custom
{
	public class Achievement : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0f;
			return soundInstance;
		}
	}
}