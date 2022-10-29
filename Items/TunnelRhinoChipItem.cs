using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MegamanXWeaponry.Items
{
	public class TunnelRhinoChipItem : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("TunnelRhino-Chip");
			Tooltip.SetDefault(" Battle Chip for Max-Buster \n Tornado Fang [c/3639bb: - Costs 10MP ] \n Tornado Fang Charged [c/3639bb: - Costs 15MP ] \n [c/bd0000: Right Click the TornadoFang icon below to remove the effect] ");
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.HoldUp;
			//Item.shoot = ModContent.ProjectileType<Projectiles.Pets.CyberelfPet>();
			Item.width = 16;
			Item.height = 30;
            Item.UseSound = new SoundStyle("MegamanXWeaponry/Sounds/Item/UpgradeCompleteSFX");
            Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 50, 50, 0);
			Item.buffType = ModContent.BuffType<Buffs.TunnelRhinoChip>();
			Item.scale = 0.5f;
		}

		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.TitaniumBar, 1)
			.AddIngredient(ItemID.Chain, 1)
			.AddIngredient(ItemID.SoulofMight, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();


			CreateRecipe()
			.AddIngredient(ItemID.AdamantiteBar, 1)
			.AddIngredient(ItemID.Chain, 1)
			.AddIngredient(ItemID.SoulofMight, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			base.UseStyle(player, heldItemFrame);


            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }

        }

	}
}