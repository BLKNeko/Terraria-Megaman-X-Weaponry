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
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 6); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 13; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Shine, 2);
            player.spikedBoots += 1;
            player.lifeRegen += 2;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 15)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.LeadBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
