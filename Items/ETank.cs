using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class ETank : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("E-Tank");
			Tooltip.SetDefault("It fully recovers the health of the user.");
		}

		public override void SetDefaults() {
			Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            //item.UseSound = SoundID.Item3;
            Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Custom/MegamanHealthRecovery");
            Item.maxStack = 9;
            Item.consumable = true;
            Item.rare = ItemRarityID.Cyan;
            Item.healLife = 100; // While we change the actual healing value in GetHealLife, item.healLife still needs to be higher than 0 for the item to be considered a healing item
            Item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
            Item.value = Item.buyPrice(gold: 5);
            Item.scale = 0.5f;
		}

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient(ItemID.HealingPotion, 1)
			.AddIngredient(ItemID.GreaterHealingPotion, 1)
			.AddTile(TileID.WorkBenches)
            .Register();

        }

		public override void GetHealLife(Player player, bool quickHeal, ref int healValue) {
			// Make the item heal half the player's max health normally, or one fourth if used with quick heal
			//healValue = player.statLifeMax2 / (quickHeal ? 4 : 2);
			healValue = player.statLifeMax2;
		}
	}
}
