using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace PokemonFramework.Framework.Models.Menu.Object
{
    internal class MenuCrystal(MenuType menuType, MemoryQuery cursorQuery, bool vertical = true, bool downIsUp = true) : 
        IMenuObject(menuType, cursorQuery,  vertical, downIsUp)
    {
        public override bool ActiveNavigation(int endLocation, int maxPresses = 100)
        {
            Serilog.Log.Debug("Actively Navigating for location: {Location}", endLocation);
            for (int i = 0; i < maxPresses; i++)
            {
                int currentLocation = GetCursorPosition();
                if (currentLocation == endLocation)
                {
                    Serilog.Log.Debug("Successfully navigated to {Location} in menu", endLocation);
                    return true;
                }

                Button button = GetButtonForMenuNavigation(currentLocation, endLocation);
                InputAction action = new(buttons: [button], options: InputOptions);
                API.Input.PerformInputAction(action);
            }
            return false;
        }

        public override void NavigateMenu(int currentLocation, int endLocation, int maxPresses = 100)
        {
            Serilog.Log.Debug("Navigating from {CurrentLocation} to {EndLocation}", currentLocation, endLocation);
            int minimumPresses = Math.Min(Math.Abs(currentLocation - endLocation), maxPresses);
            Button button = GetButtonForMenuNavigation(currentLocation, endLocation);
            InputAction action = new(buttons: [button], options: InputOptions);
            for (int i = 0; i < minimumPresses; i++)
            {
                API.Input.PerformInputAction(action);
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
