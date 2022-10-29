using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

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
			Item.width = 28;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.maxStack = 1;
        }

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
		}

	}
}