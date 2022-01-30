using MegamanXWeaponry.Dusts;
using MegamanXWeaponry.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class SigmaBlade : ModItem
	{
		public override void SetStaticDefaults() {
			base.DisplayName.SetDefault("Sigma Blade");
			base.Tooltip.SetDefault("A powerful giant sword, you can swing and pirce with this sword");
		}

		public override void SetDefaults() {
			item.damage = 50; // The damage your item deals
			item.melee = true; // Whether your item is part of the melee class
			item.width = 90; // The item texture's width
			item.height = 90; // The item texture's height
			item.useTime = 35; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 35; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			item.knockBack = 15; // The force of knockback of the weapon. Maximum is 20
			item.value = Item.buyPrice(gold: 3); // The value of the weapon in copper coins
			item.rare = ItemRarityID.Green; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
			//item.UseSound = SoundID.Item1; // The sound when the weapon is being used
			item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade");
			item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
			item.scale = 2.25f;
			//The useStyle of the item. 
			//Use useStyle 1 for normal swinging or for throwing
			//use useStyle 2 for an item that drinks such as a potion,
			//use useStyle 3 to make the sword act like a shortsword
			//use useStyle 4 for use like a life crystal,
			//and use useStyle 5 for staffs or guns
			item.useStyle = 1;//ItemUseStyleID.SwingThrow; // 1 is the useStyle
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 15);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddIngredient(ItemID.Glowstick, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox) {
			Lighting.AddLight((new Vector2(hitbox.X, hitbox.Y)), 0 * 0.008f, 217 * 0.008f, 14 * 0.008f);
			if (Main.rand.NextBool(3)) {
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Sparkle>());
			}
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
				item.useAnimation = 25;
				item.damage = 50;
				item.knockBack = 1;
				//item.shoot = ProjectileID.Bee;
				item.shoot = ProjectileID.VenomFang;
				item.shootSpeed = 5f;

			}
			else
			{
				item.noUseGraphic = false;
				item.noMelee = false;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.shoot = ProjectileID.None;
				item.damage = 50; // The damage your item deals
				item.melee = true; // Whether your item is part of the melee class
				item.width = 90; // The item texture's width
				item.height = 90; // The item texture's height
				item.useTime = 35; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
				item.useAnimation = 35; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
				item.knockBack = 15; // The force of knockback of the weapon. Maximum is 20
				item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins
				item.rare = ItemRarityID.Green; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
												//item.UseSound = SoundID.Item1; // The sound when the weapon is being used
				item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade");
				item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
				item.scale = 2.25f;
				//The useStyle of the item. 
				//Use useStyle 1 for normal swinging or for throwing
				//use useStyle 2 for an item that drinks such as a potion,
				//use useStyle 3 to make the sword act like a shortsword
				//use useStyle 4 for use like a life crystal,
				//and use useStyle 5 for staffs or guns
				item.useStyle = 1;//ItemUseStyleID.SwingThrow; // 1 is the useStyle

			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Slow, 60);
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

		public override Vector2? HoldoutOffset()
		// HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
		{
			return new Vector2(10, 0); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
		}

		// Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
		// See Source code for Star Wrath projectile to see how it passes through tiles.
		/*	The following changes to SetDefaults 
		 	item.shoot = 503;
			item.shootSpeed = 8f;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}*/
	}
}
