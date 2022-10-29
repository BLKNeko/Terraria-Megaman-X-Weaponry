using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Projectiles
{
	public class XBulletEnemy : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("XBulletEnemy");     //The English name of the Projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 10;               //The width of Projectile hitbox
			Projectile.height = 10;              //The height of Projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the Projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the Projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the Projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;          //Is the Projectile shoot by a ranged weapon?
            Projectile.penetrate = 1;           //How many monsters the Projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 200;             //The transparency of the Projectile, 255 for completely transparent. (aiStyle 1 quickly fades the Projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your Projectile is invisible.
			Projectile.light = 0.9f;            //How much light emit around the Projectile
			Projectile.ignoreWater = true;          //Does the Projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the Projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the Projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
			Projectile.scale = 0.85f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the Projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				//.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		public override void AI()
		{
			
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();

			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}
		}




        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }


        public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			//Main.PlaySound(SoundID.Item10, Projectile.position);
			//Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/XShoot"));
		}
	}
}
