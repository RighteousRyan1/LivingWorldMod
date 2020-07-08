using Terraria.UI;
using Terraria.ModLoader;
using LivingWorldMod.UI;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LivingWorldMod.Items;
using Terraria.ID;

namespace LivingWorldMod
{
	class LivingWorldMod : Mod
	{
        public static LivingWorldMod Instance;

		internal UserInterface ClockworkHammerInterface;
		internal ClockworkHammerUI ClockworkHammerUI;

		public LivingWorldMod()
		{
            Instance = this;
		}

        public void ShowClockworkHammerUI(ClockworkHammer forHammer)
        {
            ClockworkHammerInterface.SetState(ClockworkHammerUI.ActivateFor(forHammer));
            Main.PlaySound(SoundID.MenuOpen);
        }

        public void CloseClockworkHammerUI()
        {
            ClockworkHammerInterface.SetState(null);
            Main.PlaySound(SoundID.MenuClose);
        }

        public bool HasClockworkHammerUI()
        {
            return ClockworkHammerInterface.CurrentState != null;
        }

        public override void Load()
        {
            if(!Main.dedServ)
            {
                ClockworkHammerInterface = new UserInterface();
                ClockworkHammerUI = new ClockworkHammerUI();
                ClockworkHammerUI.Activate();
            }
        }

        public override void Unload()
        {
            ClockworkHammerUI = null;
        }

        private GameTime _lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (ClockworkHammerInterface?.CurrentState != null)
            {
                ClockworkHammerInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "LivingWorldMod: ClockworkHammerInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && ClockworkHammerInterface?.CurrentState != null)
                        {
                            ClockworkHammerInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }
}
