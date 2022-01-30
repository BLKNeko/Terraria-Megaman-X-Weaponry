using MegamanXWeaponry.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class AwekenSigmaBlade : ModItem
	{
		public float charge;
		private bool MPDown = false;
		private bool Sfx1 = false;
		private bool MPD1 = false;
		private bool MPD2 = false;
		private bool MPD3 = false;
		private bool CanAscend = true;

		public override void SetDefaults()
		{
			base.item.damage = 90;
			base.item.crit = 13;
			base.item.melee = true;
			base.item.width = 55;
			base.item.height = 55;
			base.item.useTime = 35;
			base.item.useAnimation = 35;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			//base.item.value = 10000;
			base.item.value = Item.sellPrice(1, 0, 0);
			base.item.rare = 11;
			base.item.scale = 2.5f;
			//base.item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ZSaber");
			//item.shoot = base.mod.ProjectileType("XBullet");
			item.shoot = ProjectileID.None;
			item.shootSpeed = 8f;
			//item.channel = true;


		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("AwakenSigmaBlade");
			base.Tooltip.SetDefault("The true power of this sword can now be achieved by charge his swing attack [c/3639bb: - Total costs 25MP ]");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("SigmaBlade"));
			recipe.AddIngredient(ItemID.MeteoriteBar, 20);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.OrichalcumBar, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Slow, 120);
		}


		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = ItemUseStyleID.Stabbing;
				item.useTime = 20;
				item.useAnimation = 20;
				item.damage = 90;
				item.knockBack = 1;
				//item.shoot = ProjectileID.Bee;
				item.shoot = ProjectileID.VenomFang;
				item.shootSpeed = 5f;

			}
			else
			{
				base.item.damage = 90;
				base.item.crit = 13;
				base.item.melee = true;
				base.item.width = 55;
				base.item.height = 55;
				base.item.useTime = 40;
				base.item.useAnimation = 40;
				base.item.useStyle = 1;
				base.item.knockBack = 4f;
				//base.item.value = 10000;
				base.item.value = Item.sellPrice(1, 0, 0);
				base.item.rare = 7;
				base.item.scale = 2.5f;
				//base.item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ZSaber");
				//item.shoot = base.mod.ProjectileType("XBullet");
				item.shoot = ProjectileID.None;
				item.shootSpeed = 8f;
				//item.channel = true;
				//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade");
				

			}
			return base.CanUseItem(player);
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
						//Main.PlaySound(25);
						Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SigmaBladeReady"));
					}
				}
				base.item.noMelee = true;
				//base.item.useAnimation = (int)(40f / (1f + charge / 60f));
				base.item.useAnimation = (int)(40f / (1f + charge / 60f));
				player.itemAnimationMax = (int)((float)base.item.useAnimation * player.meleeSpeed);
				player.itemAnimation = player.itemAnimationMax;
			}
			else
			{
				base.item.noMelee = false;
			}
			if (Main.rand.Next(3) == 0)
			{
				if (charge >= 20f && CanAscend)
				{
					base.item.scale = 3.5f;
				}
				if (charge >= 30f && CanAscend)
				{
					base.item.scale = 4.25f;

					if(player.statMana < 5f)
                    {
						CanAscend = false;
                    }
				}
				if (charge >= 40f && CanAscend)
				{
					//int dust3 = Dust.NewDust(new Vector2(player.Center.X - 30f, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust3].noGravity = true;
					//Main.dust[dust3].noLight = true;
					Dust dust1;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust1 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(199, 252, 252), 1f)];
					dust1.noGravity = true;
					base.item.scale = 5f;
					if(!MPD1)
                    {
						player.statMana -= 5;
						MPD1 = true;
					}
				}
				if (charge >= 50f && CanAscend)
				{
					base.item.scale = 5.5f;
				}
				if (charge >= 60f && CanAscend)
				{
					base.item.scale = 6.25f;
					if (player.statMana < 10f)
					{
						CanAscend = false;
					}
				}
				if (charge >= 80f && CanAscend)
				{
					//int dust2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust2].noGravity = true;
					//Main.dust[dust2].noLight = true;
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust2 = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(186, 50, 6), 1.2f)];
					dust2.noGravity = true;
					base.item.scale = 7f;
					if (!MPD2)
					{
						player.statMana -= 10;
						MPD2 = true;
					}
				}
				if (charge >= 90f && CanAscend)
				{
					base.item.scale = 7.5f;
				}
				if (charge >= 100f && CanAscend)
				{
					base.item.scale = 8f;
				}
				if (charge >= 110f && CanAscend)
				{
					base.item.scale = 9f;
					if (player.statMana < 10f)
					{
						CanAscend = false;
					}
				}
				if (charge >= 120f && CanAscend)
				{
					//int dust = Dust.NewDust(new Vector2(player.Center.X + 30f, player.Center.Y - 40f), 0, 0, 6, player.velocity.X, player.velocity.Y, 200, Color.White, 2f);
					//Main.dust[dust].noGravity = true;
					//Main.dust[dust].noLight = true;
					Dust dust;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.LocalPlayer.Center;
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(player.Center.X - 20f, player.Center.Y - 20f), 40, 40, 0, 0f, 0f, 0, new Color(244, 255, 0), 1.5f)];
					dust.noGravity = true;
					base.item.scale = 10f;
					if (!MPD3)
					{
						player.statMana -= 10;
						MPD3 = true;
					}

				}
			}
			if (charge < 120 && !player.controlUseItem && !Sfx1)
			{
				Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade"));
				Sfx1 = true;
			}
			if (charge >= 120 && !player.controlUseItem)
            {
				//Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaberCharge"));
				Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade"));
				if (player.statMana >= 10)
                {
                    if (!MPDown)
                    {
						player.statMana -= 10;
						MPDown = true;
                    }
				
				}
				
			}
			if (player.itemAnimation == 0)
			{
				charge = 0f;
				base.item.scale = 2.25f;
				item.shoot = 0;
				MPDown = false;
				Sfx1 = false;
				MPD1 = false;
				MPD2 = false;
				MPD3 = false;
				CanAscend = true;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Fix the speedX and Y to point them horizontally.
			speedX = new Vector2(speedX, speedY).Length() * (speedX > 0 ? 1 : -1);
			speedY = 0;
			// Add random Rotation
			Vector2 speed = new Vector2(speedX, speedY);
			//speed = speed.RotatedByRandom(MathHelper.ToRadians(30));
			// Change the damage since it is based off the weapons damage and is too high
			damage = (int)(damage * .4f);
			speedX = speed.X;
			speedY = speed.Y;
			return true;
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
			Lighting.AddLight((new Vector2(hitbox.X, hitbox.Y)), 68 * 0.008f, 7 * 0.008f, 173 * 0.008f);
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
