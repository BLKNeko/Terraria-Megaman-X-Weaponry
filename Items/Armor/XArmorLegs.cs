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
			Tooltip.SetDefault("This is Megaman X Legs"
				+ "\nAllow you to perforam a DASH!"
				+ "\nMove faster!");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 5000;
			item.rare = ItemRarityID.Blue;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxRunSpeed += 1.5f;
			player.AddBuff(BuffID.Shine, 2);
			player.dash += 2;


		}





        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 5);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.AddIngredient(ItemID.LeadBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
