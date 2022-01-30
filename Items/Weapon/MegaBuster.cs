using MegamanXWeaponry.Dusts;
using MegamanXWeaponry.Sounds.Custom;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class MegaBuster : ModItem
	{
		public float charge;

		public bool chargeinsfx = false;

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.ranged = true;
			base.item.width = 20;
			base.item.height = 10;
			base.item.useTime = 25;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(1, 0, 0);
			base.item.rare = 1;
			base.item.shoot = base.mod.ProjectileType("MMBullet");
			//base.item.useAmmo = base.mod.GetItem("Bullet").item.type;
			base.item.shootSpeed = 10f;
			base.item.noMelee = true;
			base.item.scale = 0.85f;
			base.item.autoReuse = true;
			base.item.magic = true;
			base.item.mana = 1;
			//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MegaBuster");
			base.Tooltip.SetDefault("Small hand Cannon that can [c/fca400:charge] energy for a powerfull shoot [c/3639bb: - Costs 1MP ] \nHold to [c/fca400:charge] a powerfull special attack [c/3639bb: - Costs 10MP ]");
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddIngredient(ItemID.LeadBar, 10);
			recipe.AddIngredient(ItemID.Glass, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (charge > 0f)
			{
				//Main.PlaySound(SoundID.Item5, player.position);
				
				if (charge >= (float)(base.item.useTime * 3.55f))
				{
					//base.item.mana = 10;
					//ChargeLoop.LP = false;
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/XChargeShoot"));
					player.statMana -= 10;
					type = base.mod.ProjectileType("MMChargeShot");
					knockBack *= 3f;
					damage *= 3;
					
				}
				else if (type == base.mod.ProjectileType("MMBullet"))
				{
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/MMShoot"));
					base.item.mana = 1;
					type = base.mod.ProjectileType("MMBullet");
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
				if (charge < (float)(base.item.useTime * 3.55f))
				{
					charge += 1f;
					if (charge >= (float)(base.item.useTime * 3))
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
						Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MMChargeIn"));
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
				if (charge >= (float)(base.item.useTime * 3.55f))
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




					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MMChargeLoop"));
					


				}
			}
		}
	}
}
