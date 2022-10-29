using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ZeroArmorHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zero Helmet");
			Tooltip.SetDefault("This is Zero's helmet."
				+ "\nMana Regen +5"
				+ "\nMana Max +10"
				+ "\nCan detect traps");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.value = 5000;
            Item.rare = ItemRarityID.Red;
            Item.defense = 11;
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
			return body.type == ModContent.ItemType<ZeroArmorBody>() && legs.type == ModContent.ItemType<ZeroArmorLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.2f;
			player.GetDamage(DamageClass.Melee) += 0.5f;
			player.lifeRegen += 2;

			if (player.statLife <= (player.statLifeMax / 2))
			{
				player.GetDamage(DamageClass.Generic) += 2;
			}
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
            CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 10)
			.AddIngredient(ItemID.HellstoneBar, 5)
			.AddIngredient(ItemID.MeteoriteBar, 3)
			.AddTile(TileID.Anvils)
			.Register();


			CreateRecipe()
			.AddIngredient(ItemID.CrimtaneBar, 10)
			.AddIngredient(ItemID.HellstoneBar, 5)
			.AddIngredient(ItemID.MeteoriteBar, 3)
			.AddTile(TileID.Anvils)
			.Register();

		}
	}
}
