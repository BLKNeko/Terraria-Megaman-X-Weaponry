using MegamanXWeaponry.Dusts;
using MegamanXWeaponry.Sounds.Custom;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class UltimateBuster : ModItem
	{
		public float charge;

		public bool chargeinsfx = false;

		public override void SetDefaults()
		{
			base.item.damage = 85;
			base.item.ranged = true;
			base.item.width = 20;
			base.item.height = 10;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(40, 0, 0, 0);
			base.item.rare = -12;
			base.item.shoot = base.mod.ProjectileType("XBullet");
			//base.item.useAmmo = base.mod.GetItem("Bullet").item.type;
			base.item.shootSpeed = 10f;
			base.item.noMelee = true;
			base.item.scale = 0.85f;
			base.item.autoReuse = true;
			base.item.magic = true;
			base.item.mana = 4;
			//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("UltimateBuster");
			base.Tooltip.SetDefault("Ultimate Hand Cannon that can [c/fca400:charge] energy for a powerfull shoot [c/3639bb: - Costs 4MP ] \nHold to [c/fca400:charge] a powerfull wide area special attack [c/3639bb: - Costs 20MP ]");
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("MaxBuster"));
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(ItemID.CrystalBlock, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 10);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.SoulofFlight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Slow, 60);
			//Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, base.mod.ProjectileType("MaxExtraBullet"), (damage / 2), knockBack, player.whoAmI);
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, base.mod.ProjectileType("MaxChargeShoot"), (damage / 2), 0, player.whoAmI);


		}



		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (charge > 0f)
			{
				//Main.PlaySound(SoundID.Item5, player.position);
				
				if (charge >= (float)(base.item.useTime * 5f))
				{
					//base.item.mana = 10;
					//ChargeLoop.LP = false;
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/XChargeShoot"));
					player.statMana -= 20;
					type = base.mod.ProjectileType("UltimateChargeShoot");
					knockBack *= 4f;
					damage *= 4;

				}
				else if (type == base.mod.ProjectileType("XBullet"))
				{
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/X8Shoot"));
					base.item.mana = 1;
					type = base.mod.ProjectileType("XBullet");
				}
				chargeinsfx = false;
				charge = 0f;
				//ChargeLoop.LP = true;
				return true;
			}
			return false;
		}


		public override void UseStyle(Player player)
		{
			if (player.itemAnimation == player.itemAnimationMax - 1 && player.controlUseItem)
			{
				player.direction = (((float)Main.mouseX + Main.screenPosition.X > player.Center.X) ? 1 : (-1));
				player.itemRotation = (float)Math.Atan2(((float)Main.mouseY + Main.screenPosition.Y - player.Center.Y) * (float)player.direction, ((float)Main.mouseX + Main.screenPosition.X - player.Center.X) * (float)player.direction);
				if (charge < (float)(base.item.useTime * 6f))
				{
					charge += 1f;
					if (charge >= (float)(base.item.useTime * 6))
					{
						//Main.PlaySound(25);
					}
				}
				player.itemAnimation = player.itemAnimationMax;
				player.itemTime = base.item.useTime;
			}
			else if (charge > 0f)
			{
				player.itemTime = 0;
			}
			if (Main.rand.Next(3) == 0)
			{
				if (charge >= (float)base.item.useTime)
				{
                    if (!chargeinsfx)
                    {
						Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/X8ChargeIn"));
						chargeinsfx = true;
					}

					//Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X - 30f, player.Center.Y - 40f), 4, player.velocity, 200, new Color(50, 192, 255), 2f);
					//dust.noGravity = true;
					//dust.noLight = true;
					Dust dust1;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust1 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(199, 252, 252), 1f)];
					dust1.noGravity = true;
				}
				if (charge >= (float)(base.item.useTime * 2))
				{
					//Dust dust2 = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y - 40f), 4, player.velocity, 200, new Color(50, 192, 255), 2f);
					//dust2.noGravity = true;
					//dust2.noLight = true;
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust2 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(6, 99, 186), 1.2f)];
					dust2.noGravity = true;

				}
				if (charge >= (float)(base.item.useTime * 6f))
				{
					//Dust dust3 = Dust.NewDustPerfect(new Vector2(player.Center.X + 30f, player.Center.Y - 40f), 4, player.velocity, 200, new Color(50, 192, 255), 2f);
					//dust3.noGravity = true;
					//dust3.noLight = true;
					//ChargeLoop.LP = true;
					//Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Sparkle>());
					//Dust dust3 = Dust.NewDustPerfect(new Vector2(player.Center.X + 5f, player.Center.Y - 4f), 4, player.velocity, 200, new Color(50, 12, 255), 2f);
					Dust dust;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(244, 255, 0), 1.5f)];
					dust.noGravity = true;




					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/X8ChargeLoop"));
					


				}
			}
		}
	}
}
