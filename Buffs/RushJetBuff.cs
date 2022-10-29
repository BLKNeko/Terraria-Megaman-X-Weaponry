using Terraria;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Buffs
{
	public class RushJetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Rush Jet");
			Description.SetDefault("When used, Rush appears and transforms into a flying sled which you can ride, his rear legs becoming jet thrusters.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.mount.SetMount(ModContent.MountType<Mounts.RushJet>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
