using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class ShiningFireflyChipItem : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Shining Firefly-Chip");
			Tooltip.SetDefault(" Battle Chip for Ultimate-Buster \n Flash Laser [c/3639bb: - Costs 10MP ] \n Flash Laser Charged [c/3639bb: - Costs 15MP ] \n [c/bd0000: Right Click the Flash Laser icon below to remove the effect] ");
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.HoldUp;
			//Item.shoot = ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/UpgradeCompleteSFX");
            Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 50, 50, 0);
			Item.buffType = ModContent.BuffType<Buffs.FlashLaserChip>();
			Item.scale = 0.5f;
		}

		public override void AddRecipes() {

			/*

			CreateRecipe()
			.AddIngredient(ItemID.HallowedBar, 1)
			.AddIngredient(ItemID.Lens, 5)
			.AddIngredient(ItemID.SoulofFright, 2)
			.AddIngredient(ItemID.SoulofFlight, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

			*/

		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			base.UseStyle(player, heldItemFrame);

            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }

        }

	}
}