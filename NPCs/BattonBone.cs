using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace MegamanXWeaponry.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-NPC-NPC-clone-with-modified-projectile-hoplite
	public class BattonBone : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Batton Bone");
			//Main.NPCFrameCount[NPC.type] = Main.NPCFrameCount[NPCID.Zombie];
			Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 15;
			NPC.defense = 7;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath57;
			NPC.value = 60f;
			NPC.knockBackResist = 0.4f;
			NPC.aiStyle = 14;
			AIType = NPCID.GiantBat;
			//animationType = NPCID.Zombie;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
			NPC.buffImmune[BuffID.Poisoned] = true;
			NPC.buffImmune[BuffID.Bleeding] = true;
			NPC.buffImmune[BuffID.OnFire] = true;
			NPC.scale = 0.8f;

		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			base.ModifyNPCLoot(npcLoot);

            npcLoot.Add(ItemDropRule.Common(ItemID.IronBar, 30));

            npcLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 30));

            npcLoot.Add(ItemDropRule.Common(ItemID.MeteoriteBar, 40));
        }


		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			//return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
			return SpawnCondition.Cavern.Chance * 0.2f;
		}

        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);

			NPC.spriteDirection = NPC.direction;

			NPC.frameCounter++;
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
			else
			{
				NPC.frameCounter = 0;
			}

		}


        public override void HitEffect(int hitDirection, double damage)
		{

            var entitySource = NPC.GetSource_Death();

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

            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            int BatGoreType1 = Mod.Find<ModGore>("BatGore1").Type;
            int BatGoreType2 = Mod.Find<ModGore>("BatGore2").Type;
            int BatGoreType3 = Mod.Find<ModGore>("BatGore3").Type;

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

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BatGoreType1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BatGoreType2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BatGoreType3);
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
