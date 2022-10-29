using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ZeroArmorBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zero Breastplate");
			Tooltip.SetDefault("This is Zero Breastplate"
				+ "\nAllow you to slide on walls"
				+ "\nHealth Regen +1");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.value = 6000;
            Item.rare = ItemRarityID.Red;
            Item.defense = 13;
		}

		public override void UpdateEquip(Player player)
		{
			player.AddBuff(BuffID.Shine, 2);
			player.spikedBoots += 1;
			player.lifeRegen += 1;
			player.GetDamage(DamageClass.Melee) += 0.5f;


		}



		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 15)
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.MeteoriteBar, 5)
			.AddTile(TileID.Anvils)
			.Register();


			CreateRecipe()
			.AddIngredient(ItemID.CrimtaneBar, 15)
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.MeteoriteBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

		}
	}
}
