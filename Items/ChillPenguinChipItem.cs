using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class ChillPenguinChipItem : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("ChillPenguin-Chip");
			Tooltip.SetDefault(" Battle Chip for X-Buster \n ShotgunIce [c/3639bb: - Costs 20MP ] \n ShotgunIce Charged [c/3639bb: - Costs 30MP ] \n Jump immediately after using the charged ability to rideon \n [c/bd0000: Right Click the ShotgunIce icon below to remove the effect] ");
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
			Item.value = Item.sellPrice(silver: 55);
            Item.buffType = ModContent.BuffType<Buffs.ChillPenguinChip>();
			Item.scale = 0.5f;
		}

		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.IronBar, 1)
			.AddIngredient(ItemID.Chain, 1)
			.AddIngredient(ItemID.IceBrick, 20)
			.AddIngredient(ItemID.SoulofFlight, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			base.UseStyle(player, heldItemFrame);

            //player.ClearBuff(ModContent.BuffType<Buffs.StormEagleChip>());

            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }


	}
}