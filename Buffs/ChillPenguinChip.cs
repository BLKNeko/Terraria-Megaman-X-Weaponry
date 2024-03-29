using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Buffs
{
	public class ChillPenguinChip : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("ShotgunIceChip");
			Description.SetDefault("X fires a shard of ice, upon collision, smaller shards will be fanned out in the opposite direction. \n Jump immediately after using the charged ability to rideon  \n Right Click to remove the effect");
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