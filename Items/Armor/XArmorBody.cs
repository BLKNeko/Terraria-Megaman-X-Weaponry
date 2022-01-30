using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class XArmorBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("X Breastplate");
			Tooltip.SetDefault("This is Megaman X Breastplate"
				+ "\nAllow you to slide on walls"
				+ "\nHealth Regen +2");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 6000;
			item.rare = ItemRarityID.Blue;
			item.defense = 13;
		}

		public override void UpdateEquip(Player player)
		{
			player.AddBuff(BuffID.Shine, 2);
			player.spikedBoots += 1;
			player.lifeRegen += 2;


		}



		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 15);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddIngredient(ItemID.LeadBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
