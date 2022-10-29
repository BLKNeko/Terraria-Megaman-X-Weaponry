using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Phanteon_Mask : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Pantheon Mask");
			Tooltip.SetDefault("This is Pantheon Mask... Creepy~");
		}

		public override void SetDefaults() {
			Item.width = 28;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.value = Item.sellPrice(silver: 75);
            Item.maxStack = 1;
        }
	}
}