using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class Cyberelf : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cyber Elf");
			Tooltip.SetDefault("Summons an Cyber Elf to Help");
		}

		public override void SetDefaults() {
			item.damage = 0;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>();
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item29;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.Pink;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 50, 50, 0);
			item.buffType = ModContent.BuffType<Buffs.CyberelfBuff>();
			item.scale = 0.5f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Firefly, 10);
			recipe.AddIngredient(ItemID.PixieDust, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 10);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void UseStyle(Player player) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}