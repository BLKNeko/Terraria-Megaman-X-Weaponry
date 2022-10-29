using MegamanXWeaponry.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
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
			Item.damage = 90;
			Item.crit = 13;
            Item.DamageType = DamageClass.Melee;
            Item.width = 55;
			Item.height = 55;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.knockBack = 4f;
			//base.item.value = 10000;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = 11;
			//Item.scale = 2.5f;
            /*
			Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/SigmaBlade")
            {
                Volume = 0.7f,
                Pitch = 0f,
                PitchVariance = 0.3f,
                MaxInstances = 1,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
            };
            */
            //item.shoot = base.mod.ProjectileType("XBullet");
            Item.shoot = ProjectileID.None;
			Item.shootSpeed = 8f;
			Item.useTurn = true;
			//item.channel = true;


		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("SigmaBlade+");
			base.Tooltip.SetDefault("A powerful giant sword, you can swing and pirce with this sword \n Charging enhances its power and size [c/3639bb: - Total costs 30MP ]");
		}

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient<Weapon.SigmaBlade>()
            .AddIngredient(ItemID.MeteoriteBar, 20)
            .AddIngredient(ItemID.CrystalShard, 15)
            .AddIngredient(ItemID.OrichalcumBar, 10)
            .AddIngredient(ItemID.ChlorophyteBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();



            CreateRecipe()
            .AddIngredient<Weapon.SigmaBlade>()
			.AddIngredient(ItemID.MeteoriteBar, 20)
			.AddIngredient(ItemID.CrystalShard, 15)
			.AddIngredient(ItemID.MythrilBar, 10)
			.AddIngredient(ItemID.ChlorophyteBar, 10)
			.AddTile(TileID.MythrilAnvil)
            .Register();

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
				Item.useStyle = ItemUseStyleID.Thrust;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.damage = 90;
				Item.knockBack = 1;
				//item.shoot = ProjectileID.Bee;
				Item.shoot = ProjectileID.VenomFang;
				Item.shootSpeed = 5f;
                Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/SigmaBlade")
                {
                    Volume = 0.7f,
                    Pitch = 0f,
                    PitchVariance = 0.3f,
                    MaxInstances = 1,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                };

            }
			else
			{
				Item.damage = 90;
				Item.crit = 13;
                Item.DamageType = DamageClass.Melee;
                Item.width = 55;
				Item.height = 55;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.useStyle = 1;
				Item.knockBack = 4f;
				Item.value = Item.sellPrice(0, 4, 0, 0);
				Item.rare = 7;
				//Item.scale = 2.5f;
				//base.item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ZSaber");
				//item.shoot = base.mod.ProjectileType("XBullet");
				Item.shoot = ProjectileID.None;
				Item.shootSpeed = 8f;
                //item.channel = true;
                //Item.UseSound = 0;
				

			}
			return base.CanUseItem(player);
		}



		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			base.UseStyle(player, heldItemFrame);


            if (player.itemAnimation == player.itemAnimationMax - 1 && player.controlUseItem)
            {
                if (charge < 120f && CanAscend)
                {
                    ////if (player.armor[0].type == base.mod.ItemType("KutKuHelmet") && player.armor[1].type == base.mod.ItemType("KutKuBreastplate") && player.armor[2].type == base.mod.ItemType("KutKuGreaves"))
                    //{
                    //	charge += 0.5f;
                    //}
                    charge += 1f;
                    if (charge >= 120f)
                    {
                        //Main.PlaySound(25);
                        //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SigmaBladeReady"));
                        SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/SigmaBladeReady")
                        {
                            Volume = 1f,
                            Pitch = 0.2f,
                            PitchVariance = 0.2f,
                            MaxInstances = 1,
                            SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                        }, player.position);
                    }
                }

                player.itemAnimationMax = (int)((float)base.Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee));
                player.itemAnimation = player.itemAnimationMax;
            }
            else
            {
                base.Item.noMelee = false;
            }


            if (charge >= 120 && CanAscend)
            {
                if (!MPD3)
                {
                    if (player.statMana > 15f)
                    {
                        player.statMana -= 15;
                        Item.scale = 15f;
                        Item.damage = 280;
                    }
                    else
                    {
                        CanAscend = false;
                    }

                    MPD3 = true;
                }

                Dust dust3;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                position.X = Main.LocalPlayer.Center.X - 20f;
                position.Y = Main.LocalPlayer.Center.Y - 20f;
                dust3 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.5813954f)];
                dust3.noGravity = true;
                dust3.shader = GameShaders.Armor.GetSecondaryShader(73, Main.LocalPlayer);

            }
            else if (charge >= 110f && CanAscend)
            {
                Item.scale = 11f;
                Item.damage = 245;
            }
            else if (charge >= 100f && CanAscend)
            {
                Item.scale = 10f;
                Item.damage = 230;
            }
            else if (charge >= 90f && CanAscend)
            {
                Item.scale = 9f;
                Item.damage = 215;
            }
            else if (charge >= 80f && CanAscend)
            {
                Item.scale = 8f;
                Item.damage = 200;
                if (!MPD2)
                {
                    if (player.statMana > 10f)
                    {
                        player.statMana -= 10;
                    }
                    else
                    {
                        CanAscend = false;
                    }

                    MPD2 = true;
                }

                Dust dust2;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                position.X = Main.LocalPlayer.Center.X - 20f;
                position.Y = Main.LocalPlayer.Center.Y - 20f;
                dust2 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.58f)];
                dust2.noGravity = true;
                dust2.shader = GameShaders.Armor.GetSecondaryShader(02, Main.LocalPlayer);

            }
            else if (charge >= 70f && CanAscend)
            {
                Item.scale = 7f;
                Item.damage = 185;
            }
            else if (charge >= 60f && CanAscend)
            {
                Item.scale = 6f;
                Item.damage = 170;
            }
            else if (charge >= 50f && CanAscend)
            {
                Item.scale = 5f;
                Item.damage = 155;
            }
            else if (charge >= 40f && CanAscend)
            {
                Item.scale = 4f;
                Item.damage = 140;
                if (!MPD1)
                {
                    if (player.statMana > 5f)
                    {
                        player.statMana -= 5;
                    }
                    else
                    {
                        CanAscend = false;
                    }

                    MPD1 = true;
                }

                Dust dust1;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                position.X = Main.LocalPlayer.Center.X - 20f;
                position.Y = Main.LocalPlayer.Center.Y - 20f;
                dust1 = Main.dust[Terraria.Dust.NewDust(position, 55, 46, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.58f)];
                dust1.noGravity = true;
                dust1.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);

            }
            else if (charge >= 30f && CanAscend)
            {
                Item.scale = 3f;
                Item.damage = 110;
            }
            else if (charge >= 20f && CanAscend)
            {
                Item.scale = 2.8f;
                Item.damage = 100;
            }
            else if (charge <= 19f)
            {
                Item.scale = 2.5f;
                Item.damage = 90;
            }



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

        }


        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {

            //Item.noMelee = (player.controlUseItem) ? true : false;


            if (player.controlUseItem)
            {
                if (charge < 120f && CanAscend)
                    charge += 1f;


                Item.noMelee = true;





                if (charge >= 120 && CanAscend)
                {
                    if (!MPD3)
                    {
                        if (player.statMana > 15f)
                        {
                            player.statMana -= 15;
                            Item.scale = 15f;
                            Item.damage = 280;
                        }
                        else
                        {
                            CanAscend = false;
                        }

                        MPD3 = true;
                    }
                }
                else if (charge >= 110f && CanAscend)
                {
                    Item.scale = 11f;
                    Item.damage = 245;
                }
                else if (charge >= 100f && CanAscend)
                {
                    Item.scale = 10f;
                    Item.damage = 230;
                }
                else if (charge >= 90f && CanAscend)
                {
                    Item.scale = 9f;
                    Item.damage = 215;
                }
                else if (charge >= 80f && CanAscend)
                {
                    Item.scale = 8f;
                    Item.damage = 200;
                    if (!MPD2)
                    {
                        if (player.statMana > 10f)
                        {
                            player.statMana -= 10;
                        }
                        else
                        {
                            CanAscend = false;
                        }

                        MPD2 = true;
                    }
                }
                else if (charge >= 70f && CanAscend)
                {
                    Item.scale = 7f;
                    Item.damage = 185;
                }
                else if (charge >= 60f && CanAscend)
                {
                    Item.scale = 6f;
                    Item.damage = 170;
                }
                else if (charge >= 50f && CanAscend)
                {
                    Item.scale = 5f;
                    Item.damage = 155;
                }
                else if (charge >= 40f && CanAscend)
                {
                    Item.scale = 4f;
                    Item.damage = 140;
                    if (!MPD1)
                    {
                        if (player.statMana > 5f)
                        {
                            player.statMana -= 5;
                        }
                        else
                        {
                            CanAscend = false;
                        }

                        MPD1 = true;
                    }
                }
                else if (charge >= 30f && CanAscend)
                {
                    Item.scale = 3f;
                    Item.damage = 110;
                }
                else if (charge >= 20f && CanAscend)
                {
                    Item.scale = 2.8f;
                    Item.damage = 100;
                }
                else if (charge <= 19f)
                {
                    Item.scale = 2.5f;
                    Item.damage = 90;
                }




                //Item.damage = 30 * (int)((float)(1f + charge / 60f));

            }
            else
            {
                Item.noMelee = false;

                if (charge >= 2 && player.itemAnimation == player.itemAnimationMax - 1)
                {

                    SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/SigmaBlade")
                    {
                        Volume = 1f,
                        Pitch = 0f,
                        PitchVariance = 0.1f,
                        MaxInstances = 1,
                        SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
                    }, player.position);


                }

                    

                charge = 1f;
                MPDown = false;
                Sfx1 = false;
                MPD1 = false;
                MPD2 = false;
                MPD3 = false;
                CanAscend = true;
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
