using CsvHelper.TypeConversion;
using MegamanXWeaponry.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Weapon
{
	public class ZSaberBlack : ModItem
	{
		public static float charge;
		private bool MPDown = false;

		public override void SetDefaults()
		{
			Item.damage = 55;
			Item.crit = 14;
            Item.DamageType = DamageClass.Melee;
            Item.width = 45;
			Item.height = 42;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.knockBack = 5f;
			//Item.value = 10000;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = 11;
			Item.scale = 1f;
			//Item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ZSaber");
			//Item.shoot = mod.ProjectileType("XBullet");
			Item.shoot = ProjectileID.None;
			Item.shootSpeed = 8f;
			//Item.channel = true;
			Item.useTurn = true;


		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Z-Saber+");
			Tooltip.SetDefault("An enhanced Z-Saber with a purple energy blade. \nHold to [c/fca400:charge] to launch a long-range shockwave [c/3639bb: - Costs 25MP ]");
		}

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient<Weapon.ZSaber>()
            .AddIngredient(ItemID.MeteoriteBar, 20)
            .AddIngredient(ItemID.CrystalShard, 20)
            .AddIngredient(ItemID.OrichalcumBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();


            CreateRecipe()
            .AddIngredient<Weapon.ZSaber>()
            .AddIngredient(ItemID.MeteoriteBar, 20)
            .AddIngredient(ItemID.CrystalShard, 20)
            .AddIngredient(ItemID.MythrilBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();

		}


        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            //base.UseStyle(player, heldItemFrame);

            if (charge >= 20 && player.controlUseItem)
            {
                Item.noMelee = true;
            }
            else
            {
                Item.noMelee = false;
            }

            //Mod.Logger.DebugFormat("Some debug info for noMelee: ({0})", Item.noMelee);


            if (player.itemAnimation == player.itemAnimationMax - 1 && player.controlUseItem)
            {
                //Mod.Logger.DebugFormat("Some debug info for charge: ({0})", charge);
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
                    }
                }
                //Item.noMelee = true;
                //base.item.useAnimation = (int)(40f / (1f + charge / 60f));
                //Item.useAnimation = (int)(18f / (1f + charge / 60f));
                player.itemAnimationMax = (int)((float)base.Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee));
                player.itemAnimation = player.itemAnimationMax;
                //player.itemTime = 2; // Set item time to 2 frames while we are used
                //player.itemAnimation = 2; // Set item animation time to 2 frames while we are used

            }
            else
            {
                Item.noMelee = false;
                //charge = 1f;
            }
            //if (Main.rand.Next(3) == 0)
            //{
            if (charge >= 40f)
            {
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
            if (charge >= 80f)
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
                    dust2.shader = GameShaders.Armor.GetSecondaryShader(02, Main.LocalPlayer);
                }
            }
            if (charge >= 120f)
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

            }
            //}
            if (charge < 119 && !player.controlUseItem)
            {
                //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaber"));

                charge = 1f;
                Item.noMelee = false;
                //Item.damage = (int)((float)Item.damage * (1f + charge / 60f));
                //Item.crit = (int)((float)Item.crit * (1f + charge / 60f));
                //Item.knockBack = Item.knockBack * (1f + charge / 60f);

            }
            if (charge >= 120 && !player.controlUseItem)
            {
                //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ZSaberCharge"));

                charge = 1f;
                Item.noMelee = false;

            }

            if (player.itemAnimation == 0)
            {
                //charge = 1f;
                //Item.shoot = 0;

            }


        }





        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {

            //Item.noMelee = (player.controlUseItem) ? true : false;


            if (player.controlUseItem)
            {
                if (charge < 120f)
                    charge += 1f;


                Item.noMelee = true;



                if (charge >= 120f)
                {
                    Item.damage = 180;
                    //Item.shoot = ModContent.ProjectileType<Projectiles.XBullet>();
                    Item.shoot = ProjectileID.None;
                }
                else if (charge >= 80f)
                {
                    Item.damage = 80;
                    Item.shoot = ProjectileID.None;
                }
                else if (charge >= 40f)
                {
                    Item.damage = 60;
                    Item.shoot = ProjectileID.None;
                }
                else if (charge <= 39f)
                {
                    Item.damage = 55;
                    Item.shoot = ProjectileID.None;
                }

                //Item.damage = 30 * (int)((float)(1f + charge / 60f));

            }
            else
            {
                Item.noMelee = false;

                if (charge >= 2 && player.itemAnimation == player.itemAnimationMax - 1)
                {
                    if (charge >= 120)
                    {


                        if (player.statMana >= 25)
                        {
                            if (!MPDown)
                            {
                                player.statMana -= 25;
                                MPDown = true;
                            }

                            Item.shoot = ModContent.ProjectileType<Projectiles.ZSaberBlackProjectile>();

                            SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/ZSaberCharge")
                            {
                                Volume = 1f,
                                Pitch = 0f,
                                PitchVariance = 0.1f,
                                MaxInstances = 1,
                                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                            }, player.position);
                        }
                        else
                        {
                            SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/ZSaber")
                            {
                                Volume = 0.2f,
                                Pitch = 0f,
                                PitchVariance = 0.2f,
                                MaxInstances = 1,
                                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest,
                            }, player.position);
                        }


                    }
                    else
                    {
                        SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/ZSaber")
                        {
                            Volume = 0.2f,
                            Pitch = 0f,
                            PitchVariance = 0.4f,
                            MaxInstances = 1,
                            SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest,
                        }, player.position);
                    }

                }



                MPDown = false;
                charge = 1f;
            }





            //damage = StatModifier.Default;
            //Mod.Logger.DebugFormat("Some debug info for charge in modify: ({0})", charge);
            //damage *= (1f + charge / 60f);

        }


        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit = (int)((float)crit * (1f + charge / 60f));

        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
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
			if (charge >= (float)(Item.useTime * 3))
			{
				Main.PlaySound(SoundID.Item5, player.position);
				if (charge >= 100f)
				{
					//Item.mana = 10;
					player.statMana -= 10;
					type = mod.ProjectileType("XBulletC");
					knockBack *= 3f;
					damage *= 10;
					charge = 0f;
					return true;

				}
				else if (type == mod.ProjectileType("XBullet"))
				{
					Item.mana = 1;
					type = mod.ProjectileType("Fail");
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
