using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Placeable
{
	public class MegamanDoor : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This is Megaman like boss door.");
		}

		public override void SetDefaults() {
			Item.width = 14;
            Item.height = 28;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<Tiles.MegamanDoorClosed>();
            //Item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Item/XShoot");
		}


		public override void AddRecipes() {

            CreateRecipe()
            .AddIngredient(ItemID.IronDoor)
			.AddTile(TileID.HeavyWorkBench)
            .Register();
        }
	}
}