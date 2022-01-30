using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class XArmorHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("X Helmet");
			Tooltip.SetDefault("This is Megaman X Helmet"
				+ "\nMana Regen +5"
				+ "\nMana Max +10"
				+ "\nCan detect traps");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 5000;
			item.rare = ItemRarityID.Blue;
			item.defense = 11;
		}

		public override void UpdateEquip(Player player)
		{
			player.manaRegen += 5;
			player.statManaMax2 += 10;
			player.AddBuff(BuffID.Shine, 2);
			player.AddBuff(BuffID.Dangersense, 2);


		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<XArmorBody>() && legs.type == ModContent.ItemType<XArmorLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.allDamage += 0.2f;
			player.rangedDamage += 0.5f;
			player.lifeRegen += 2;
			/* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
		}



		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.AddIngredient(ItemID.LeadBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
