using MegamanXWeaponry.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class ZSaber : ModItem
	{
		public float charge;
		private bool MPDown = false;

		public override void SetDefaults()
		{
			base.item.damage = 30;
			base.item.crit = 10;
			base.item.melee = true;
			base.item.width = 45;
			base.item.height = 42;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			//base.item.value = 10000;
			base.item.value = Item.sellPrice(1, 0, 0);
			base.item.rare = 7;
			base.item.scale = 1f;
			//base.item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ZSaber");
			//item.shoot = base.mod.ProjectileType("XBullet");
			item.shoot = ProjectileID.None;
			item.shootSpeed = 8f;
			//item.channel = true;


		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Z-Saber");
			base.Tooltip.SetDefault("It is a powerful saber with a green energy blade\nHold to charge a powerfull special attack [c/3639bb: - Costs 10MP ]");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 20);
			recipe.AddIngredient(ItemID.Glowstick, 50);
			recipe.AddIngredient(ItemID.Glass, 10);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		
		public override void UseStyle(Player player)
		{
			if (player.itemAnimation == player.itemAnimationMax - 1 && player.controlUseItem)
			{
				if (charge < 120f)
				{
					////if (player.armor[0].type == base.mod.ItemType("KutKuHelmet") && player.armor[1].type == base.mod.ItemType("KutKuBreastplate") && player.armor[2].type == base.mod.ItemType("KutKuGreaves"))
					//{
					//	charge += 0.5f;
					//}
					charge += 1f;
					if (charge >= 120f)
					{
						Main.PlaySound(25);
					}
				}
				base.item.noMelee = true;
				//base.item.useAnimation = (int)(40f / (1f + charge / 60f));
				base.item.useAnimation = (int)(18f / (1f + charge / 60f));
				player.itemAnimationMax = (int)((float)base.item.useAnimation * player.meleeSpeed);
				player.itemAnimation = player.itemAnimationMax;
			}
			else
			{
				base.item.noMelee = false;
			}
			if (Main.rand.Next(3) == 0)
			{
				if (charge >= 40f)
				{
					//int dust3 = Dust.NewDust(new Vector2(player.Center.X - 30f, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust3].noGravity = true;
					//Main.dust[dust3].noLight = true;
					Dust dust1;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust1 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(199, 252, 252), 1f)];
					dust1.noGravity = true;
				}
				if (charge >= 80f)
				{
					//int dust2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust2].noGravity = true;
					//Main.dust[dust2].noLight = true;
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust2 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(186, 50, 6), 1.2f)];
					dust2.noGravity = true;
				}
				if (charge >= 120f)
				{
					//int dust = Dust.NewDust(new Vector2(player.Center.X + 30f, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust].noGravity = true;
					//Main.dust[dust].noLight = true;
					Dust dust;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(244, 255, 0), 1.5f)];
					dust.noGravity = true;

				}
			}
			if (charge < 120 && !player.controlUseItem)
			{
				Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaber"));
			}
			if (charge >= 120 && !player.controlUseItem)
            {
				//Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaberCharge"));
				if(player.statMana >= 10)
                {
                    if (!MPDown)
                    {
						player.statMana -= 10;
						MPDown = true;
                    }
					
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaberCharge"));
					item.shoot = base.mod.ProjectileType("ZSaberProjectile");
				}
				
			}
			if (player.itemAnimation == 0)
			{
				charge = 0f;
				item.shoot = 0;
				MPDown = false;
			}
		}

		

		

		public override void GetWeaponDamage(Player player, ref int damage)
		{
			damage = (int)((float)damage * (1f + charge / 60f));
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit = (int)((float)crit * (1f + charge / 60f));
		}

		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			knockback *= 1f + charge / 60f;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			Lighting.AddLight((new Vector2(hitbox.X, hitbox.Y)), 0 * 0.008f, 217 * 0.008f, 14 * 0.008f);
			if (Main.rand.Next((int)(100f / charge)) == 0)
			{
				//int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 32, player.velocity.X, player.velocity.Y, 200, Color.Yellow, 1f + charge / 120f);
				//Main.dust[dust].noGravity = true;
				//Main.dust[dust].noLight = true;
				//dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 32, player.velocity.X, player.velocity.Y, 200, Color.Yellow, 1f + charge / 120f);
				//Main.dust[dust].noGravity = true;
				//Main.dust[dust].noLight = true;
				//dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 32, player.velocity.X, player.velocity.Y, 200, Color.Yellow, 1f + charge / 120f);
				//Main.dust[dust].noGravity = true;
				//Main.dust[dust].noLight = true;
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Sparkle>());
				//Lighting.AddLight((new Vector2(hitbox.X, hitbox.Y)), 0 * 0.3f, 217 * 0.3f, 14 * 0.3f);
				

			}
		}


		/*
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (charge >= (float)(base.item.useTime * 3))
			{
				Main.PlaySound(SoundID.Item5, player.position);
				if (charge >= 100f)
				{
					//base.item.mana = 10;
					player.statMana -= 10;
					type = base.mod.ProjectileType("XBulletC");
					knockBack *= 3f;
					damage *= 10;
					charge = 0f;
					return true;

				}
				else if (type == base.mod.ProjectileType("XBullet"))
				{
					base.item.mana = 1;
					type = base.mod.ProjectileType("Fail");
					charge = 0f;
					return true;
				}
				charge = 0f;
				return true;
			}
			return true;
		}
		*/

	}
}
