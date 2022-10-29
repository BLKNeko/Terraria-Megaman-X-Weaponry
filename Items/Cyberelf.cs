using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class Cyberelf : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cyber-Elf");
			Tooltip.SetDefault("Summons a Cyber-Elf to provide support.");
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shoot = ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item29;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.value = Item.sellPrice(gold: 55);
			Item.buffType = ModContent.BuffType<Buffs.CyberelfBuff>();
			Item.scale = 0.5f;
		}

		public override void AddRecipes() {
            CreateRecipe()
            .AddIngredient(ItemID.Firefly, 10)
			.AddIngredient(ItemID.PixieDust, 10)
			.AddIngredient(ItemID.CrystalShard, 10)
			.AddTile(TileID.TinkerersWorkbench)
            .Register();

            
                
        }

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			base.UseStyle(player, heldItemFrame);

            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 9600, true);
            }
        }

	}
}