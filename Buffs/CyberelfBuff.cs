using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Buffs
{
	public class CyberelfBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cyber-Elf");
			Description.SetDefault("Emits light, increase vitality");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.statLifeMax2 += 20;
			player.lifeRegen += 5;


            player.buffTime[buffIndex] = 18000;

            int projType = ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>();

            // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                var entitySource = player.GetSource_Buff(buffIndex);

                Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }


            /*

			//player.GetModPlayer<ExamplePlayer>().exampleLightPet = true;
			Projectiles.Pets.CyberelfPet.exampleLightPet = true;
			player.buffTime[buffIndex] = 300000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			if (player.controlDown && player.releaseDown) {
				if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15) {
					for (int j = 0; j < 1000; j++) {
						if (Main.projectile[j].active && Main.projectile[j].type == ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>() && Main.projectile[j].owner == player.whoAmI) {
							Projectile lightpet = Main.projectile[j];
							Vector2 vectorToMouse = Main.MouseWorld - lightpet.Center;
							lightpet.velocity += 5f * Vector2.Normalize(vectorToMouse);
						}
					}
				}
			}

			*/
        }
	}
}