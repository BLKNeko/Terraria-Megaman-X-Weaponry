using Terraria;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Dusts
{
	public class Negative : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.noGravity = true;
		}
	}
}