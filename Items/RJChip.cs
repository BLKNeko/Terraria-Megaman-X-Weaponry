using MegamanXWeaponry.Mounts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class RJChip : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("RJ-Chip");
			Tooltip.SetDefault("Call Rush to aid!");
		}

		public override void SetDefaults() {
			Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.buyPrice(gold: 30);
            Item.rare = ItemRarityID.Red;
            //item.UseSound = SoundID.Item79;
            Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/rush_call");
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<RushJet>();
            Item.scale = 0.5f;
		}

		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.PalladiumBar, 10)
			.AddIngredient(ItemID.SoulofFlight, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

			CreateRecipe()
			.AddIngredient(ItemID.CobaltBar, 10)
			.AddIngredient(ItemID.SoulofFlight, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		}
	}
}