using CsvHelper.TypeConversion;
using IL.Terraria.GameContent;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace MegamanXWeaponry.Items.Weapon
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class UltimateBuster : ModItem
	{
		public static float charge;

		private bool alreadyFired = false;

		public bool chargeinsfx = false;

		public override void SetDefaults()
		{
			Item.damage = 85;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
			Item.height = 10;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 40, 0, 0);
			Item.rare = -12;
			Item.shoot = ModContent.ProjectileType<Projectiles.XBullet>();
            //Item.useAmmo = base.mod.GetItem("Bullet").item.type;
            Item.shootSpeed = 10f;
			Item.noMelee = true;
			Item.scale = 0.85f;
			Item.autoReuse = true;
			Item.mana = 4;
			Item.channel = true;
            Item.noUseGraphic = true;
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


		/*
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.damage = 85;
				Item.ranged = true;
				Item.width = 20;
				Item.height = 10;
				Item.useTime = 12;
				Item.useAnimation = 12;
				Item.useStyle = 5;
				Item.knockBack = 2f;
				Item.value = Item.sellPrice(0, 40, 0, 0);
				Item.rare = -12;
				Item.shoot = base.mod.ProjectileType("XBullet");
				//Item.useAmmo = base.mod.GetItem("Bullet").item.type;
				Item.shootSpeed = 10f;
				Item.noMelee = true;
				Item.scale = 0.85f;
				Item.autoReuse = true;
				Item.magic = true;
				Item.mana = 5;
				//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
				item.noUseGraphic = true;
				item.channel = true;


			}
			else
			{
				Item.damage = 85;
				Item.ranged = true;
				Item.width = 20;
				Item.height = 10;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.useStyle = 5;
				Item.knockBack = 2f;
				Item.value = Item.sellPrice(0, 40, 0, 0);
				Item.rare = -12;
				Item.shoot = base.mod.ProjectileType("XBullet");
				//Item.useAmmo = base.mod.GetItem("Bullet").item.type;
				Item.shootSpeed = 10f;
				Item.noMelee = true;
				Item.scale = 0.85f;
				Item.autoReuse = true;
				Item.magic = true;
				Item.mana = 4;
				//item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
				item.noUseGraphic = true;
				item.channel = true;

			}
			return base.CanUseItem(player);
		}

		*/

		public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient<Weapon.XBuster>()
            .AddIngredient(ItemID.FallenStar, 10)
			.AddIngredient(ItemID.CrystalBlock, 10)
			.AddIngredient(ItemID.CrystalShard, 10)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddIngredient(ItemID.SoulofFlight, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		}


		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Slow, 60);
			//Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, base.mod.ProjectileType("MaxExtraBullet"), (damage / 2), knockBack, player.whoAmI);
			//Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, base.mod.ProjectileType("MaxChargeShoot"), (damage / 2), 0, player.whoAmI);


		}



		public override void HoldItem(Player player)
		{
			base.HoldItem(player);
			player.handon = player.HeldItem.handOnSlot;
			player.cHandOn = 0;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            //Mod.Logger.DebugFormat("Some debug info for shoot type: ({0})", type);

            //Mod.Logger.DebugFormat("Some debug info for shoot charge: ({0})", charge);


            if (charge > 0f)
            {
                //Main.PlaySound(SoundID.Item5, player.position);

                //----------------------------------FLASHLASER----------------------------------------
                if (player.HasBuff(ModContent.BuffType<Buffs.FlashLaserChip>()) && charge >= (float)(base.Item.useTime * 5.5f) && !alreadyFired && player.altFunctionUse != 2)
                {

                    //base.item.mana = 10;
                    //ChargeLoop.LP = false;
                    if (player.statMana >= 29)
                    {
                        //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ShotgunIceShoot"));
                        player.statMana -= 29;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.FlashLaserCharged>(), damage * 3, knockback * 5, player.whoAmI);
                    }

                    

                    alreadyFired = true;
                }
                else if (player.HasBuff(ModContent.BuffType<Buffs.FlashLaserChip>()) && !alreadyFired && player.altFunctionUse != 2)
                {


                    if (player.statMana >= 19)
                    {

                        if (player.direction == -1)
                        {
                            position.X -= 50f;
                        }
                        else
                            position.X += 50f;

                        //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ShotgunIceShoot"));
                        player.statMana -= 19;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.FlashLaser>(), damage * 2, knockback * 2, player.whoAmI);
                    }

                    alreadyFired = true;
                }



                // ULTIMATE-BUSTER
                if (charge >= (float)(Item.useTime * 5.5f) && !alreadyFired)
                {
                    //base.item.mana = 10;
                    //ChargeLoop.LP = false;

                    if (player.statMana >= 9)
                    {
                        //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/XChargeShoot"));
                        SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/XChargeShoot")
                        {
                            Volume = 0.8f,
                            Pitch = 0.3f,
                            PitchVariance = 0.2f,
                            MaxInstances = 2,
                            SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                        }, player.position);
                        player.statMana -= 9;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.UltimateChargeShoot>(), damage * 5, knockback * 5, player.whoAmI);
                    }



                }
                else if (charge <= (float)(Item.useTime * 4.15f) && charge >= (float)(Item.useTime * 1.85f) && !alreadyFired)
                {
                    if (player.statMana >= 4)
                    {
                        //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/XShoot"));
                        SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/XShoot")
                        {
                            Volume = 0.6f,
                            Pitch = 0.5f,
                            PitchVariance = 0.3f,
                            MaxInstances = 5,
                            SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                        }, player.position);
                        player.statMana -= 4;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.XHalfCharge>(), damage * 2, knockback * 1.5f, player.whoAmI);
                    }

                }
                else if ((type == ModContent.ProjectileType<Projectiles.XBullet>()) && !alreadyFired)
                {
                    //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/XShoot"));
                    SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/XShoot")
                    {
                        Volume = 0.5f,
                        Pitch = 0.5f,
                        PitchVariance = 0.3f,
                        MaxInstances = 5,
                        SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                    }, player.position);

                    Item.mana = 1;
                    //type = ModContent.ProjectileType<Projectiles.XBullet>();
                    Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.XBullet>(), damage, knockback, player.whoAmI);
                }
                chargeinsfx = false;
                alreadyFired = false;
                charge = 0f;
                //Mod.Logger.DebugFormat("Some debug info for shoot: ({0})", Shoot);
                return false;
            }
            return false;



        }


        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            base.UseStyle(player, heldItemFrame);


            if (player.itemAnimation == player.itemAnimationMax - 1 && player.controlUseItem)
            {
                player.direction = (((float)Main.mouseX + Main.screenPosition.X > player.Center.X) ? 1 : (-1));
                player.itemRotation = (float)Math.Atan2(((float)Main.mouseY + Main.screenPosition.Y - player.Center.Y) * (float)player.direction, ((float)Main.mouseX + Main.screenPosition.X - player.Center.X) * (float)player.direction);
                //Mod.Logger.DebugFormat("Some debug info for Item usetime: ({0})", Item.useTime);
                charge += (float)1f;
                //Mod.Logger.DebugFormat("Some debug info for charge: ({0})", charge);
                player.itemTime = 2; // Set item time to 2 frames while we are used
                //player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
                player.itemAnimation = player.itemAnimationMax;
            }
            //else if (charge > 1f && !canShoot)
            else if (charge > 1f)
            {

                player.itemTime = 0;

            }


            if (charge >= (float)Item.useTime)
            {
                if (!chargeinsfx)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargeIn"));
                    SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/X8ChargeIn")
                    {
                        Volume = 0.1f,
                        Pitch = 0f,
                        PitchVariance = 0.1f,
                        MaxInstances = 1,
                        SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                    }, player.position);
                    chargeinsfx = true;
                }


                if (Main.rand.NextFloat() < 0.35f)
                {
                    Dust dust1;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Main.LocalPlayer.Center;
                    position.X = Main.LocalPlayer.Center.X - 20f;
                    position.Y = Main.LocalPlayer.Center.Y - 20f;
                    dust1 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.58f)];
                    dust1.noGravity = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);
                }


            }
            if (charge >= (float)(Item.useTime * 2))
            {

                if (Main.rand.NextFloat() < 0.5f)
                {
                    Dust dust2;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Main.LocalPlayer.Center;
                    position.X = Main.LocalPlayer.Center.X - 20f;
                    position.Y = Main.LocalPlayer.Center.Y - 20f;
                    dust2 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.58f)];
                    dust2.noGravity = true;
                }



            }
            if (charge >= (float)(Item.useTime * 5.5f))
            {

                if (Main.rand.NextFloat() < 0.75f)
                {
                    Dust dust3;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Main.LocalPlayer.Center;
                    position.X = Main.LocalPlayer.Center.X - 20f;
                    position.Y = Main.LocalPlayer.Center.Y - 20f;
                    dust3 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.5813954f)];
                    dust3.noGravity = true;
                    dust3.shader = GameShaders.Armor.GetSecondaryShader(73, Main.LocalPlayer);
                }




                //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargeLoop"));
                SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/X8ChargeLoop")
                {
                    Volume = 0.1f,
                    Pitch = 0f,
                    PitchVariance = 0f,
                    MaxInstances = 1,
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                }, player.position);



            }






        }
	}
}
