using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Projectiles
{
	public class ShotgunIceCharged : ModProjectile
	{
		private bool SpeedUp = false;
		private bool getOldSpeed = false;
		private bool rideIn = false;
		private int RideInTimer = 0;
		private int holdSpeed = 0;
		private int lifeTime = 0;
		private Vector2 oldSpeed;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("ShotgunIceCharged");     //The English name of the projectile
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 25;
			Projectile.friendly = true;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.alpha = 255;
			Projectile.penetrate = 100;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 30;
			Projectile.light = 0.8f;
			Projectile.scale = 0.8f;
			AIType = ProjectileID.WoodenArrowFriendly;
			Projectile.aiStyle = 1;

			//1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			//2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			//2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
			//5: projectile.usesLocalNPCImmunity = true;
			//5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			//5b: projectile.localNPCHitCooldown = 20; // o
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			base.ModifyDamageHitbox(ref hitbox);

			hitbox.X = hitbox.X - 10; //esquerda e direito
			//hitbox.Y = hitbox.Y - 225; //  + baixo  - cima
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			//projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				//projectile.Kill();
			}
			else
			{
				//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				//Main.PlaySound(SoundID.Item10, projectile.position);
				if (Projectile.velocity.X != oldVelocity.X)
				{
                    Projectile.velocity.X = -oldVelocity.X;
				}
				//if (projectile.velocity.Y != oldVelocity.Y)
				//{
				//	projectile.velocity.Y = -oldVelocity.Y;
				//}
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			//3a: target.immune[projectile.owner] = 20;
			//3b: target.immune[projectile.owner] = 5;
			target.AddBuff(BuffID.Frostburn, 180);
			target.AddBuff(BuffID.Slow, 300);
		}

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            base.OnHitPvp(target, damage, crit);
			target.AddBuff(BuffID.Frostburn, 180);
			target.AddBuff(BuffID.Slow, 300);
		}

        public override Color? GetAlpha(Color lightColor)
		{
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		}

		public override void AI()
		{
            Projectile.ai[0] += 1f;
			Player player = Main.player[Projectile.owner];

			

			if (!getOldSpeed)
            {
				oldSpeed = Projectile.velocity;
				getOldSpeed = true;
            }

			lifeTime++;
			if(lifeTime >= 500)
            {
                //player.AddBuff(BuffID.Featherfall, 10);
                Projectile.Kill();
				lifeTime = 0;
            }
			

            if (!SpeedUp)
            {
				holdSpeed++;
				if (holdSpeed < 10)
				{
                    Projectile.velocity *= 0f;
				}
				else
				{
                    Projectile.velocity = oldSpeed;
                    Projectile.velocity *= 1.15f;
					SpeedUp = true;
				}
			}

			

			

			RideInTimer++;

			//MOUNT
			if (RideInTimer <= 10 && player.controlJump && !rideIn)
				rideIn = true;


			//MAKE PLAYER MOVE WITH PROJECTILLE
			if (rideIn && Projectile.active && lifeTime <= 499)
            {
				player.position.X = Projectile.position.X;
				player.position.Y = Projectile.position.Y - 45;
				player.direction = Projectile.direction;

				player.AddBuff(BuffID.Featherfall, 60);
			}


			//DISMOUNT
			if (rideIn && RideInTimer >= 25  && player.controlJump)
				rideIn = false;
			



			Dust dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = Projectile.Center;
			dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 63, 0f, 0f, 0, new Color(255, 255, 255), 1f)];




			if (Projectile.ai[0] > 50f)
			{
				// Fade out
				//projectile.alpha += 25;
				//if (projectile.alpha > 255)
				//{
				//	projectile.alpha = 255;
				//}
			}
			else
			{
                // Fade in
                Projectile.alpha -= 25;
				if (Projectile.alpha < 100)
				{
                    Projectile.alpha = 100;
				}
			}
			// Slow down
			//projectile.velocity *= 0.98f;
			//projectile.velocity *= 1.002f;
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++Projectile.frameCounter >= 5)
			{
                Projectile.frameCounter = 0;
				if (++Projectile.frame >= 4)
				{
                    Projectile.frame = 3;
				}
			}
			// Kill this projectile after 1 second
			if (Projectile.ai[0] >= 300f)
			{
                Projectile.Kill();
			}
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				//projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
			{
                Projectile.rotation += MathHelper.Pi;
			}
		}


        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 20f;
            origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }

        /*

		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}

		*/

        public override void Kill(int timeLeft)
		{
            Projectile.frameCounter = 0;
			holdSpeed = 0;
			RideInTimer = 0;
			rideIn = false;
			getOldSpeed = false;

			
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			//Main.PlaySound(SoundID.Item50, projectile.position);
			//Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ShotgunIceBreak"));
            SoundEngine.PlaySound(new SoundStyle("MegamanXWeaponry/Sounds/Item/ShotgunIceBreak")
            {
                Volume = 0.8f,
                Pitch = 0.3f,
                PitchVariance = 0.2f,
                MaxInstances = 2,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            });

            for (int i = 0; i < 10; i++)
            {
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 80, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
			}

			for (int i = 0; i < 5; i++)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 51, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
			}

		}
	}

}

