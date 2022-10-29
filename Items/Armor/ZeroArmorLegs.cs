using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ZeroArmorLegs : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zero Legs");
			Tooltip.SetDefault("These are Zero's Legs."
				+ "\nAllow you to perform a DASH!"
				+ "\nMove faster!"
				+ "\nAllow you to perform a double jump!");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.value = 5000;
            Item.rare = ItemRarityID.Red;
            Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxRunSpeed += 2f;
			player.AddBuff(BuffID.Shine, 2);
            player.dashType = 2;
            player.hasJumpOption_Cloud = true;


		}





        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.HellstoneBar, 5)
			.AddIngredient(ItemID.MeteoriteBar, 2)
			.AddTile(TileID.Anvils)
			.Register();


			CreateRecipe()
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.HellstoneBar, 5)
			.AddIngredient(ItemID.MeteoriteBar, 2)
			.AddTile(TileID.Anvils)
			.Register();

		}
	}
}
