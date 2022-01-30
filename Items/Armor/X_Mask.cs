using Terraria.ModLoader;
using Terraria.ID;

namespace MegamanXWeaponry.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class X_Mask : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("X Mask");
			Tooltip.SetDefault("This is Megaman X Face Mask");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
			item.vanity = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool DrawHead() {
			return false;
		}
	}
}