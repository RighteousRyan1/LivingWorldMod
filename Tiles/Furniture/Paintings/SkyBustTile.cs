﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using LivingWorldMod.Items.Placeable.Paintings;

namespace LivingWorldMod.Tiles.Furniture.Paintings
{
    public class SkyBustTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);

            disableSmartCursor = true;

            ModTranslation mapName = CreateMapEntryName();
            mapName.SetDefault("Sky Bust Painting");
            AddMapEntry(new Color(139, 69, 19), mapName);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), ModContent.ItemType<SkyBustTileItem>(), 1);
        }
    }
}
