using MegamanXWeaponry.Items.Armor;
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
	public class SigmaVirus : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sigma Virus");
			//Main.NPCFrameCount[NPC.type] = Main.NPCFrameCount[NPCID.Zombie];
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults() {
			NPC.width = 30;
			NPC.height = 40;
			NPC.damage = 25;
			NPC.defense = 20;
			NPC.lifeMax = 350;
			NPC.HitSound = SoundID.NPCHit49;
			NPC.DeathSound = SoundID.NPCDeath51;
			NPC.value = 3500f;
			NPC.knockBackResist = 0.1f;
			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.velocity *= 1.5f;
			//animationType = NPCID.Wraith;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			
			//return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
			return (!Main.dayTime && Main.hardMode) ? 0.1f : 0f;
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(BuffID.Poisoned, 600, true);
			}
            else
            {
				player.AddBuff(BuffID.Poisoned, 300, true);
			}
		}


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);

            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 25));

            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 35));

        }



		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 15; i++)
			{
				//int dustType = Main.rand.Next(139, 143);
				//int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				//Dust dust = Main.dust[dustIndex];
				//dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				//dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				//dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;

				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = NPC.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 60, 0f, -10f, 0, new Color(177, 0, 255), 0.5813954f)];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1.5f + Main.rand.Next(-30, 31) * 0.01f;

			}
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
				else if (NPC.frameCounter < 30)
				{
					NPC.frame.Y = 3 * frameHeight;
				}
				else
				{
					NPC.frameCounter = 0;
				}
			

			


		}



	}
}
