using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class XArmorLegs : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("X Legs");
			Tooltip.SetDefault("These are Mega Man X's Legs."
				+ "\nAllow you to perform a DASH!"
				+ "\nMove faster!");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxRunSpeed += 1.5f;
			player.AddBuff(BuffID.Shine, 2);
			player.dashType = 2;


		}





        public override void AddRecipes()
		{

            CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 5)
				.AddIngredient(ItemID.HellstoneBar, 5)
				.AddIngredient(ItemID.LeadBar, 5)
				.AddTile(TileID.Anvils)
				.Register();

            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 5)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.LeadBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
	}
}
