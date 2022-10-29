using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Projectiles
{
	public class TornadoFang : ModProjectile
	{
		private bool SpeedUp = false;
		private bool getOldSpeed = false;
		private int holdSpeed = 0;
		private int HitTimer = 0;
		private Vector2 oldSpeed;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("TornadoFang");     //The English name of the Projectile
			//ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 45;
			Projectile.height = 25;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 0;
			Projectile.penetrate = 10;
			Projectile.damage = 30;
			Projectile.light = 0.2f;
			Projectile.scale = 0.8f;
            Projectile.DamageType = DamageClass.Ranged;

            //1: Projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: Projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: Projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: Projectile.usesLocalNPCImmunity = true;
            //5a: Projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: Projectile.localNPCHitCooldown = 20; // o
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			//3a: target.immune[Projectile.owner] = 20;
			//3b: target.immune[Projectile.owner] = 5;
			//HitTimer++;

			target.AddBuff(BuffID.BrokenArmor, 80);

			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}

			Projectile.velocity *= 0.25f;

			//if(HitTimer >= 45)
            //{
			//	Projectile.velocity *= 0.2f;
			//	HitTimer = 0;
            //}

		}

		//public override Color? GetAlpha(Color lightColor)
		//{
			//return Color.White;
		//	return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		//}

		public override void AI()
		{
			Projectile.ai[0] += 1f;
			Player player = Main.player[Projectile.owner];



			if (!getOldSpeed)
			{
				oldSpeed = Projectile.velocity;
				getOldSpeed = true;
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
					Projectile.velocity *= 0.9f;
					SpeedUp = true;
				}
			}


			if (Projectile.ai[0] > 50f)
			{
				// Fade out
				//Projectile.alpha += 25;
				//if (Projectile.alpha > 255)
				//{
				//	Projectile.alpha = 255;
				//}
			}
			else
			{
				// Fade in
				Projectile.alpha -= 25;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 0;
				}
			}
			// Slow down
			//Projectile.velocity *= 0.98f;
			//Projectile.velocity *= 1.002f;
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 4)
				{
					Projectile.frame = 0;
				}
			}
			// Kill this Projectile after 1 second
			if (Projectile.ai[0] >= 200f)
			{
				Projectile.Kill();
			}
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				//Projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}
		}

        // Some advanced drawing because the texture image isn't centered or symetrical.
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

        public override void Kill(int timeLeft)
		{
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            int TFGoreType1 = Mod.Find<ModGore>("TornadoFangGore1").Type;
            var entitySource = Projectile.GetSource_Death();

            //Gore.NewGore(Projectile.position, Projectile.velocity, mod.GetGoreSlot("Gores/TornadoFangGore1"), 1.25f);

            Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), TFGoreType1, 1.25f);
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			//Main.PlaySound(SoundID.Item10, Projectile.position);
		}
	}

}
