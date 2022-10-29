using MegamanXWeaponry.Dusts;
using MegamanXWeaponry.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
			Item.damage = 50; // The damage your item deals
            Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class
            Item.width = 90; // The item texture's width
			Item.height = 90; // The item texture's height
			Item.useTime = 35; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.knockBack = 15; // The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 8); // The value of the weapon in copper coins
			Item.rare = ItemRarityID.Green; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
			//item.UseSound = SoundID.Item1; // The sound when the weapon is being used
			Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
			Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/SigmaBlade")
            {
                Volume = 0.7f,
                Pitch = 0f,
                PitchVariance = 0.3f,
                MaxInstances = 1,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
            };
            Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
			Item.scale = 2.25f;
			//The useStyle of the item. 
			//Use useStyle 1 for normal swinging or for throwing
			//use useStyle 2 for an item that drinks such as a potion,
			//use useStyle 3 to make the sword act like a shortsword
			//use useStyle 4 for use like a life crystal,
			//and use useStyle 5 for staffs or guns
			Item.useStyle = 1;//ItemUseStyleID.SwingThrow; // 1 is the useStyle
		}

		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.MeteoriteBar, 15)
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.Glowstick, 50)
			.AddTile(TileID.Anvils)
			.Register();
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
				Item.useStyle = ItemUseStyleID.Thrust;
				Item.useTime = 20;
				Item.useAnimation = 25;
				Item.damage = 50;
				Item.knockBack = 1;
				//item.shoot = ProjectileID.Bee;
				Item.shoot = ProjectileID.VenomFang;
				Item.shootSpeed = 5f;
                Item.autoReuse = true;

            }
			else
			{
				Item.noUseGraphic = false;
				Item.noMelee = false;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shoot = ProjectileID.None;
				Item.damage = 50; // The damage your item deals
                Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class
                Item.width = 90; // The item texture's width
				Item.height = 90; // The item texture's height
				Item.useTime = 35; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
				Item.knockBack = 15; // The force of knockback of the weapon. Maximum is 20
				Item.value = Item.buyPrice(0, 0, 20, 0); // The value of the weapon in copper coins
				Item.rare = ItemRarityID.Green; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
												//item.UseSound = SoundID.Item1; // The sound when the weapon is being used
				Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
				//Item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SigmaBlade");
				Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
				Item.scale = 2.25f;
				//The useStyle of the item. 
				//Use useStyle 1 for normal swinging or for throwing
				//use useStyle 2 for an item that drinks such as a potion,
				//use useStyle 3 to make the sword act like a shortsword
				//use useStyle 4 for use like a life crystal,
				//and use useStyle 5 for staffs or guns
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Slow, 60);
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
