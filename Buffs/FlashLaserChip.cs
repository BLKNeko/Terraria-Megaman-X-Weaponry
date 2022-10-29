using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Buffs
{
	public class FlashLaserChip : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FlashLaserChip");
			Description.SetDefault("sends out a remote-controlled firefly-like projectile. \n Rick click to start remote control or cancel the laser  \n Right Click this icon to remove the effect");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = false;
			
		}

		public override void Update(Player player, ref int buffIndex) {
			//player.statLifeMax2 += 20;
			//player.lifeRegen += 5;
			//player.GetModPlayer<ExamplePlayer>().exampleLightPet = true;
			//Projectiles.Pets.CyberelfPet.exampleLightPet = true;

			

			player.buffTime[buffIndex] = 300000;
			//bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>()] <= 0;
			//if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
			//	Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			//}
			//if (player.controlDown && player.releaseDown) {
			//	if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15) {
			//		for (int j = 0; j < 1000; j++) {
			//			if (Main.projectile[j].active && Main.projectile[j].type == ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>() && Main.projectile[j].owner == player.whoAmI) {
			//				Projectile lightpet = Main.projectile[j];
			//				Vector2 vectorToMouse = Main.MouseWorld - lightpet.Center;
			//				lightpet.velocity += 5f * Vector2.Normalize(vectorToMouse);
			//			}
			//		}
			//	}
			//}
		}
	}
}