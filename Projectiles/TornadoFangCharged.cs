
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Projectiles
{
	public class TornadoFangCharged : ModProjectile
	{

		private bool SpeedUp = false;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("TornadoFangCharged");     //The English name of the Projectile
			//ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 100;
			Projectile.height = 80;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 0;
			Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.damage = 30;
			Projectile.light = 0.8f;
			Projectile.scale = 0.55f;

			//1: Projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			//2a: Projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			//2b: Projectile.penetrate = 3; // Same, but max 3 hits before dying
			//5: Projectile.usesLocalNPCImmunity = true;
			//5a: Projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			//5b: Projectile.localNPCHitCooldown = 20; // o
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			base.ModifyDamageHitbox(ref hitbox);
			Player player = Main.player[Projectile.owner];
			

			hitbox.Y = hitbox.Y + 10;
			if (player.direction == -1)
            {
				hitbox.X = hitbox.X + 10; //esquerda e direito
				hitbox.Y = hitbox.Y - 5;
			}
            else
            {
				hitbox.X = hitbox.X + 15; //esquerda e direito
				hitbox.Y = hitbox.Y - 5;
			}

			

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            //3a: target.immune[Projectile.owner] = 20;
            //3b: target.immune[Projectile.owner] = 5;
            target.AddBuff(BuffID.BrokenArmor, 200);
        }

		//public override Color? GetAlpha(Color lightColor)
		//{
			//return Color.White;
			//return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		//}

		public override void AI()
		{
			Projectile.ai[0] += 1f;
			Player player = Main.player[Projectile.owner];
			Vector2 mousePosition = Main.MouseWorld;

			Vector2 truePosition = new Vector2();

            //Main.PlaySound(SoundID.Item22, Projectile.position);
            SoundEngine.PlaySound(SoundID.Item22, Projectile.position);

            //x.y of tp position
            //truePosition.X = ((int)mousePosition.X) - 8;
            //truePosition.Y = ((int)mousePosition.Y) - 40;
            truePosition.X = ((int)mousePosition.X);
			truePosition.Y = ((int)mousePosition.Y);


			if (!SpeedUp)
            {
				Projectile.velocity *= 0f;
				SpeedUp = true;
			}

			player.itemAnimation = player.itemAnimationMax;
			Projectile.direction = player.direction;
			Projectile.spriteDirection = player.direction;

			player.altFunctionUse = 2;
			


			//Projectile.position.Y = player.position.Y + 5;

			/*
            if(player.position.Y - truePosition.Y >= 60 )
            {
				//UPWARD
				Projectile.position.Y = player.position.Y - 25;
				Projectile.rotation = -0.8f;

			}
			else if (player.position.Y - truePosition.Y <= 90 && player.position.Y - truePosition.Y >= 549)
			{
				//CENTER
				Projectile.position.Y = player.position.Y + 5;
				Projectile.rotation = 0;

			}
			else if (player.position.Y - truePosition.Y <= 550)
			{
				//DOWNARD
				Projectile.position.Y = player.position.Y + 25;
				Projectile.rotation = 0.3f;

			}
			*/



			if (player.direction == -1)
			{
				

				//LEFT
				if (((int)player.position.Y - (int)truePosition.Y) >= 70)
				{
					//UPWARD
					Projectile.position.Y = player.position.Y - 30;
					Projectile.rotation = 0.9f;

					Projectile.position.X = player.position.X - 48;
				}
				else if ((((int)player.position.Y - (int)truePosition.Y) <= 69) && (((int)player.position.Y - (int)truePosition.Y) >= -90))
				{
					//CENTER
					Projectile.position.Y = player.position.Y;
					Projectile.rotation = 0;

					Projectile.position.X = player.position.X - 65;

				}
				else if ((((int)player.position.Y - (int)truePosition.Y) <= 170))
				{
					//DOWNARD
					Projectile.position.Y = player.position.Y + 35;
					Projectile.position.X = player.position.X - 63;
					Projectile.rotation = -0.8f;

				}
			}
			else
			{
				
				//RIGHT
				if (((int)player.position.Y - (int)truePosition.Y) >= 70)
				{
					//UPWARD
					Projectile.position.Y = player.position.Y - 30;
					Projectile.rotation = -0.9f;

					Projectile.position.X = player.position.X - 15;
				}
				else if ((((int)player.position.Y - (int)truePosition.Y) <= 69) && (((int)player.position.Y - (int)truePosition.Y) >= -90))
				{
					//CENTER
					Projectile.position.Y = player.position.Y ;
					Projectile.rotation = 0;

					Projectile.position.X = player.position.X + 3;

				}
				else if ((((int)player.position.Y - (int)truePosition.Y) <= 170))
				{
					//DOWNARD
					Projectile.position.Y = player.position.Y + 35;
					Projectile.position.X = player.position.X - 1;
					Projectile.rotation = 0.8f;

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
			if (Projectile.ai[0] >= 300f)
			{
				if(player.controlUseTile && player.statMana >= 11)
                {
					player.statMana -= 10 ;
					Projectile.ai[0] = 280;
                }
                else
                {
					Projectile.Kill();
				}
				
			}
			//Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			//Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				//Projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
			{
				//Projectile.rotation += MathHelper.Pi;
			}
		}

		/*
		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			//if (Projectile.spriteDirection == -1)
			//{
			//	spriteEffects = SpriteEffects.FlipHorizontally;
			//}
			Texture2D texture = Main.ProjectileTexture[Projectile.type];
			int frameHeight = Main.ProjectileTexture[Projectile.type].Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = Projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

			return false;
		}
		*/

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			//Main.PlaySound(SoundID.Item10, Projectile.position);
		}
	}

}

