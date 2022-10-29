using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using MegamanXWeaponry.Items.Armor;
using Mono.Cecil;

namespace MegamanXWeaponry.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-NPC-NPC-clone-with-modified-projectile-hoplite
	public class PantheonGuardian : ModNPC
	{
		private bool speedUp = false;

		private int state = 0;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Pantheon Guardian");
			//Main.NPCFrameCount[NPC.type] = Main.NPCFrameCount[NPCID.Zombie];
			Main.npcFrameCount[NPC.type] = 11;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

		public override void SetDefaults() {
			NPC.width = 40;
			NPC.height = 85;
			NPC.damage = 17;
			NPC.defense = 9;
			NPC.lifeMax = 100;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath57;
			NPC.value = 1600f;
			NPC.knockBackResist = 0.4f;
			NPC.aiStyle = 3;
			//NPC.aiStyle = -1;
			//aiType = NPCID.Zombie;
			//animationType = NPCID.Zombie;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
			NPC.buffImmune[BuffID.Poisoned] = true;
			NPC.buffImmune[BuffID.Bleeding] = true;
			NPC.buffImmune[BuffID.OnFire] = true;
			NPC.scale = 0.5f;

		}



		public override void AI()
		{


			NPC.TargetClosest(true);
			// Now we check the make sure the target is still valid and within our specified notice range (500)
			if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 65f)
			{
				state = 1;

				//NPC.velocity *= 0.1f;

				Vector2 position = NPC.Center;
				if (NPC.spriteDirection == -1)
                {
					position.X -= 30;
				}
                else
                {
					position.X += 30;
				}

					
				Vector2 targetPosition = Main.player[NPC.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				float speed = 10f;
				int type = ModContent.ProjectileType<Projectiles.FakeShot>();
                int damage = NPC.damage; //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
				var entitySource = NPC.GetSource_NaturalSpawn();
                //Projectile.NewProjectile(position, direction * speed, type, (damage /3), 0f, Main.myPlayer);

                Projectile.NewProjectile(entitySource, position, direction * speed, type, (damage / 3), 0f, Main.myPlayer);



            }

            NPC.TargetClosest(true);
			//if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 80f)
			if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 80f)
			{
				// Out targeted player seems to have left our range, so we'll go back to sleep.
				//NPC.velocity *= 1f;
				state = 0;
			}



			/*
			if (AI_State == State_Normal)
			{
				NPC.velocity = new Vector2(NPC.direction * 2, 0);
				// TargetClosest sets NPC.target to the player.whoAmI of the closest player. the faceTarget parameter means that NPC.direction will automatically be 1 or -1 if the targeted player is to the right or left. This is also automatically flipped if NPC.confused
				NPC.TargetClosest(true);
				// Now we check the make sure the target is still valid and within our specified notice range (500)
				if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 500f)
				{
					// Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
					AI_State = State_Notice;
					AI_Timer = 0;
				}
			}

			else if (AI_State == State_Notice)
			{
				NPC.velocity = new Vector2(NPC.direction * 3, 0);
				// If the targeted player is in attack range (250).
				if (Main.player[NPC.target].Distance(NPC.Center) < 250f)
				{
					// Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
					AI_Timer++;
					if (AI_Timer >= 20)
					{
						AI_State = State_Attack;
						AI_Timer = 0;
					}
				}
				else
				{
					NPC.TargetClosest(true);
					if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 500f)
					{
						// Out targeted player seems to have left our range, so we'll go back to sleep.
						AI_State = State_Normal;
						AI_Timer = 0;
					}
				}
			}

			else if (AI_State == State_Attack)
			{
				AI_Timer++;
				if (AI_Timer == 1)
				{
					// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up. 
					//NPC.velocity = new Vector2(NPC.direction * 2, -10f);
					NPC.velocity = new Vector2(NPC.direction * 1.1f, 0);
				}
				else if (AI_Timer > 40)
				{
					// after .66 seconds, we go to the hover state. // TODO, gravity?
					AI_State = State_Normal;
					AI_Timer = 0;
				}
			}

			*/

		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			base.ModifyNPCLoot(npcLoot);

            npcLoot.Add(ItemDropRule.Common(ItemID.IronBar, 25));

            npcLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 25));

            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 30));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Phanteon_Mask>(), 100));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Phanteon_Mask_Ver_D>(), 140));



        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return SpawnCondition.OverworldNightMonster.Chance * 0.04f;
		}


        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);

			NPC.spriteDirection = NPC.direction;

			NPC.frameCounter++;

			if(state == 1)
            {
				if (NPC.frameCounter < 0)
				{
					NPC.frame.Y = 6 * frameHeight;
				}
				else if (NPC.frameCounter < 10)
				{
					NPC.frame.Y = 7 * frameHeight;
				}
				else if (NPC.frameCounter < 20)
				{
					NPC.frame.Y = 8 * frameHeight;
				}
				else if (NPC.frameCounter < 30)
				{
					NPC.frame.Y = 9 * frameHeight;
				}
				else if (NPC.frameCounter < 40)
				{
					NPC.frame.Y = 10 * frameHeight;
				}
				else
				{
					NPC.frameCounter = 0;
				}
			}

            else
            {
				if (NPC.frameCounter < 0)
				{
					NPC.frame.Y = 0 * frameHeight;
				}
				else if (NPC.frameCounter < 10)
				{
					NPC.frame.Y = 1 * frameHeight;
				}
				else if (NPC.frameCounter < 20)
				{
					NPC.frame.Y = 2 * frameHeight;
				}
				else if (NPC.frameCounter < 30)
				{
					NPC.frame.Y = 3 * frameHeight;
				}
				else if (NPC.frameCounter < 40)
				{
					NPC.frame.Y = 4 * frameHeight;
				}
				else if (NPC.frameCounter < 50)
				{
					NPC.frame.Y = 5 * frameHeight;
				}
				else
				{
					NPC.frameCounter = 0;
				}
			}
			

		}


        public override void HitEffect(int hitDirection, double damage)
		{
            var entitySource = NPC.GetSource_Death();
            int PhanteonGoreType1 = Mod.Find<ModGore>("PhanteonGore1").Type;
            int PhanteonGoreType2 = Mod.Find<ModGore>("PhanteonGore2").Type;
            int PhanteonGoreType3 = Mod.Find<ModGore>("PhanteonGore3").Type;
            int PhanteonGoreType4 = Mod.Find<ModGore>("PhanteonGore4").Type;

            for (int i = 0; i < 10; i++)
			{
				int dustType2 = 6;//Main.rand.Next(139, 143);
				int dustIndex2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType2);
				Dust dust3 = Main.dust[dustIndex2];
				dust3.velocity.X = dust3.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust3.velocity.Y = dust3.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust3.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}

			int dustType = 8; //Main.rand.Next(139, 143);
			int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
			Dust dust = Main.dust[dustIndex];
			dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
			dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
			dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 6; i++)
				{
					int dustG = Dust.NewDust(NPC.position, NPC.width, NPC.height, 200, 2 * hitDirection, -2f);

					if (Main.rand.NextBool(2))
					{
						Main.dust[dustG].noGravity = true;
						Main.dust[dustG].scale = 1.2f * NPC.scale;
					}
					else
					{
						Main.dust[dustG].scale = 0.7f * NPC.scale;
					}
				}
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), PhanteonGoreType1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), PhanteonGoreType2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), PhanteonGoreType3);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), PhanteonGoreType4);
                //Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/PhanteonGore1"), NPC.scale);
                //Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/PhanteonGore2"), NPC.scale);
                //Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/PhanteonGore3"), NPC.scale);
                //Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/PhanteonGore4"), NPC.scale);
            }
		}

		/*
		public override void HitEffect(int hitDirection, double damage) {
			for (int i = 0; i < 10; i++) {
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
		*/
	}
}
