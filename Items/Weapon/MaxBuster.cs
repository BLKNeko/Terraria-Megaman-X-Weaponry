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
    public class MaxBuster : ModItem
    {
        public static float charge;

        public bool chargeinsfx = false;

        public bool canShoot = true;

        private bool alreadyFired = false;
        private int TimerShoot = 0;

        public static bool TFActive = false;

        private int tornadoFangPosition = 0;

        private int SkillIndex = 0;

        private bool ChangeIndex = false;

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 25;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<Projectiles.XBullet>();
            //base.item.useAmmo = base.mod.GetItem("Bullet").item.type;
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.scale = 0.85f;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 1;
            Item.noUseGraphic = true;
            Item.channel = true;
            //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
        }

        public override void SetStaticDefaults()
        {
            base.DisplayName.SetDefault("Max-Buster");
            base.Tooltip.SetDefault("3rd generation upgrade of the X-Buster, with enhanced firepower [c/3639bb: - Costs 1MP ] \n Can be [c/fca400:charged] to fire a powerful Cross Charge Shot [c/3639bb: - Costs 15MP ]");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override bool AltFunctionUse(Player player)
        {
            //SkillIndex++;
            //Mod.Logger.DebugFormat("Some debug info for SkillIndex: ({0})", SkillIndex);

            if (SkillIndex == 0 && player.HasBuff(ModContent.BuffType<Buffs.TunnelRhinoChip>()) && !ChangeIndex)
            {
                SkillIndex = 25;
                ChangeIndex = true;
            }


            if (SkillIndex == 25 && !ChangeIndex)
            {
                SkillIndex = 0;
                ChangeIndex = true;
            }

            //Mod.Logger.DebugFormat("Some debug info for SkillIndex: ({0})", SkillIndex);

            SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/X8Select")
            {
                Volume = 0.2f,
                Pitch = 0.1f,
                PitchVariance = 0.3f,
                MaxInstances = 2,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest,
            }, player.position);

            ChangeIndex = false;
            return true;
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

                
                Item.damage = 25;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 20;
                Item.height = 10;
                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.useStyle = 5;
                Item.knockBack = 2f;
                Item.value = Item.sellPrice(gold: 5);
                Item.rare = 1;
                Item.shoot = ModContent.ProjectileType<Projectiles.XBullet>();
                Item.useAmmo = AmmoID.None;
                //base.item.useAmmo = base.mod.GetItem("Bullet").item.type;
                Item.shootSpeed = 10f;
                Item.noMelee = true;
                Item.scale = 0.85f;
                Item.autoReuse = true;
                Item.DamageType = DamageClass.Magic;
                Item.mana = 3;
                Item.noUseGraphic = true;
                Item.channel = false;
                //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");
                

            }
            else
            {
                Item.damage = 25;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 20;
                Item.height = 10;
                Item.useTime = 25;
                Item.useAnimation = 20;
                Item.useStyle = 5;
                Item.knockBack = 2f;
                Item.value = Item.sellPrice(gold: 5);
                Item.rare = 1;
                Item.shoot = ModContent.ProjectileType<Projectiles.XBullet>();
                //base.item.useAmmo = base.mod.GetItem("Bullet").item.type;
                Item.shootSpeed = 10f;
                Item.noMelee = true;
                Item.scale = 0.85f;
                Item.autoReuse = false;
                Item.DamageType = DamageClass.Magic;
                Item.mana = 1;
                Item.noUseGraphic = true;
                Item.channel = true;
                //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/XShoot");

            }
            return base.CanUseItem(player);
        }

        */


        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FallenStar, 5)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddIngredient(ItemID.MeteoriteBar, 10)
            .AddIngredient(ItemID.Glass, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            player.handon = player.HeldItem.handOnSlot;
            player.cHandOn = SkillIndex;
            // 0 = none - 1 = red - 2 = darkred - 3 = lightred - 4 = brownred
            // 5 = orange - 6 = darkorange - 7 = lightorange - 8 = brownorange
            // 9 = yellow - 10 = darkyellow - 11 = lightyellow - 12 = brownyellow
            // 13 = green - 14 - darkgreen - 15 = lightgreen - 16 browngreen
            // 17 = lime - 18 = darklime - 19 = lightlime - 20 = brownlime
            // 21 = teal - 22 = darkteal - 23 = lightteal - 24 = brownteal
            // 25 = cyan - 26 = darkcyan - 27 =  lightcyan - 28 = browncyan
            // 29 = darksky - 30 = darkersky - 31 = lightdarksky - 32 browndarksky
            // 33 = blue - 34 = darkblue - 35 = lightblue - 36 = brownblue
            // 37 = purple - 38 = darkpurple - 39 = lightpurple - 40 = brownpurple
            // 41 = pink - 42 = darkpink - 43 - lightpink - 44 = brownpink
            // 45 = magenta - 46 = darkmagenta - 47 = lightmagenta - 48 = brownmagenta
            // 49 = brown - 50 = darkbrown - 51 = lightbrown - 52 = bronwbrown
            // 53 = black - 54 = darkblack - 55 = lightblack - 56 = pureblack
            // 57 = grey - 58 = vulcan - 59 = limered - 60 = grey - 61 = darkvulcan
            // 62 = ice - 63 = darkice - 64 = greyice - 65 = darkerice
            // 66 = greenlime - 67 = darkgreenlime - 68 = greygreenlime - 69 = darkergreenlime
            // 70 = mango - 71 = blueteal - 72 = purplepink - 73 = rainbow
            // 74 = darkrainbow - 75 = effectRGB
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            //Mod.Logger.DebugFormat("Some debug info for shoot type: ({0})", type);

            //Mod.Logger.DebugFormat("Some debug info for shoot charge: ({0})", charge);

            
            if (charge > 0f)
            {
                //Main.PlaySound(SoundID.Item5, player.position);


                //TORNADOFANG

                if (player.HasBuff(ModContent.BuffType<Buffs.TunnelRhinoChip>()) && charge >= (float)(base.Item.useTime * 4.3f) && !alreadyFired && player.altFunctionUse != 2 && SkillIndex == 25)
                {
                    //base.item.mana = 10;
                    //ChargeLoop.LP = false;
                    //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StormTornadoSFX"));

                    if (player.statMana >= 14)
                    {
                        //Main.PlaySound(SoundID.Item23, player.position);
                        SoundEngine.PlaySound(SoundID.Item22, position);

                        player.statMana -= 14;
                        //type = base.mod.ProjectileType("TornadoFangCharged");
                        //knockBack *= 2f;
                        //damage *= 2;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.TornadoFangCharged>(), damage * 3, knockback * 2, player.whoAmI);
                    }



                    alreadyFired = true;
                }
                else if (player.HasBuff(ModContent.BuffType<Buffs.TunnelRhinoChip>()) && !alreadyFired && player.altFunctionUse != 2 && SkillIndex == 25)
                {
                    //Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StormTornadoSFX"));

                    if (player.statMana >= 9)
                    {

                        if (player.direction == -1)
                        {
                            position.X -= 50f;
                        }
                        else
                            position.X += 50f;

                        switch (tornadoFangPosition)
                         {
                        	case 0:
                        		//position = position;
                        		tornadoFangPosition = 1;
                        		break;
                        	case 1:
                        		position = new Vector2(position.X, position.Y - 20);
                        		tornadoFangPosition = 2;
                        		break;
                        	case 2:
                        			position = new Vector2(position.X, position.Y + 20);
                        		tornadoFangPosition = 0;
                        		break;
                        
                         }

                        //Main.PlaySound(SoundID.Item22, player.position);
                        SoundEngine.PlaySound(SoundID.Item22, position);

                        player.statMana -= 9;
                        //type = base.mod.ProjectileType("TornadoFang");
                        //knockBack *= 1;
                        //damage *= 1;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.TornadoFang>(), damage, knockback, player.whoAmI);
                    }



                    alreadyFired = true;
                }





                // MAX-BUSTER
                if (charge >= (float)(Item.useTime * 4.3f) && !alreadyFired)
                {
                    //base.item.mana = 10;
                    //ChargeLoop.LP = false;

                    if (player.statMana >= 14)
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
                        player.statMana -= 14;

                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.MaxChargeShoot>(), damage * 4, knockback * 4, player.whoAmI);

                        float numberProjectiles = 8 + Main.rand.Next(8); // 3, 4, or 5 shots
                        float rotation = MathHelper.ToRadians(45);
                        position += Vector2.Normalize(velocity) * 45f;


                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .8f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<Projectiles.MaxExtraBullet>(), (damage / 2), knockback, player.whoAmI);
                        }
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
                canShoot = false;
                charge = 0f;
                TimerShoot = 0;
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
                    SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/ChargeIn")
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
            if (charge >= (float)(Item.useTime * 4.3f))
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
                SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Custom/ChargeLoop")
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
