using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Menu.Config;
using PokemonFramework.Framework.Models.Menu.Object;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Models.Pokemon;
using PokemonFramework.Framework.Models.Pokemon.PokemonConfig;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Menu
{
    using Constructor = Func<MenuType, MemoryQuery, bool, bool, IMenuObject>;

    public enum MenuType
    {
        /// <summary>
        /// Normal menu with single cursor that is stored as the offset from the beginning of the menu
        /// </summary>
        Single,
        /// <summary>
        /// 2D menu that has an X and Y component
        /// </summary>
        Multi,
        /// <summary>
        /// Menu whose cursor position is stored as a combination of window offset from start of list and as the offset within the visible window
        /// </summary>
        ViewOffset
    }

    public interface IMenuConfig
    {
        public abstract MemoryQuery DefaultMenuCursorQuery { get; }
        public abstract MemoryQuery DefaultMultiCursorXQuery { get; }
        public abstract MemoryQuery DefaultMultiCursorYQuery { get; }
        public abstract MemoryQuery DefaultViewOffsetQuery { get; }
        public abstract MemoryQuery DefaultCursorOffsetQuery { get; }
    }

    public abstract class IMenuObject(MenuType menuType,
            MemoryQuery cursorQuery,
            bool vertical = true,
            bool downIsUp = true) : FrameworkObject
    {

        public IMenuObject(MenuType menuType,
            MemoryAddress cursorAddress,
            int size = 1,
            MemoryDomain domain = MemoryDomain.WRAM,
            bool vertical = true,
            bool downIsUp = true) : this(menuType, new(address: cursorAddress, size: size, domain: domain), vertical, downIsUp)
        { }
        private const int MAX_PRESSES = 100;

        public MenuType MenuType = menuType;

        /// <summary>
        /// Query for the position of the menu cursor
        /// </summary>
        public MemoryQuery CursorQuery = cursorQuery;

        /// <summary>
        /// Set whether the menu is a vertical or horizontal menu
        /// Used to determine the buttons to be pressed for navigation
        /// </summary>
        public bool Vertical = vertical;

        /// <summary>
        /// Set which direction increases the cursor index
        /// When set to True, Down/Right will increase the cursor index
        /// </summary>
        public bool DownIsUp = downIsUp;

        /// <summary>
        /// Input options to use while navigating</param>
        /// </summary>
        public virtual InputOptions InputOptions => new(duration: 5, waitFrames: 10);

        /// <summary>
        /// Options for the default input configuration when interacting with a menu
        /// </summary>
        public static InputOptions DefaultMenuOptions { get; }

        /// <summary>
        /// Blind navigation method that will calculate the expected number of presses needed to navigate to a specific location within the menu
        /// 
        /// </summary>
        /// <param name="currentLocation">Starting location of the cursor within the menu</param>
        /// <param name="endLocation">Ending location of the cursor within the menu</param>
        /// <param name="maxPresses">*OPTIONAL* Maximum number of input actions to take</param>
        /// <returns></returns>
        public abstract void NavigateMenu(int currentLocation, int endLocation, int maxPresses = MAX_PRESSES);

        /// <summary>
        /// Navigate to a specific location within the menu given the query for the cursor position
        /// </summary>
        /// <param name="endLocation">Ending location of the cursor within the menu</param>
        /// <param name="actionOptions">*OPTIONAL* Input options to use while navigating</param>
        /// <param name="maxPresses">*OPTIONAL* Maximum number of input actions to take</param>
        /// <returns>If the cursorQuery returns the endLocation after navigating</returns>
        public bool NavigateMenu(int endLocation, int maxPresses = MAX_PRESSES)
        {
            int startLocation = API.Memory.ReadInt(CursorQuery);
            NavigateMenu(startLocation, endLocation, maxPresses);
            return GetCursorPosition() == endLocation;
        }

        /// <summary>
        /// Navigate to the specified end location by actively querying the cursor position after each input action
        /// </summary>
        /// <param name="cursorQuery">Query for the menu cursor that returns the current cursor position</param>
        /// <param name="endLocation">Ending location of the cursor within the menu</param>
        /// <param name="actionOptions">*OPTIONAL* Input options to use while navigating</param>
        /// <param name="maxPresses">*OPTIONAL* Maximum number of input actions to take</param>
        /// <returns>If the cursorQuery returns the endLocation after navigating</returns>
        public abstract bool ActiveNavigation(MemoryQuery cursorQuery, int endLocation, InputAction actionOptions, int maxPresses = MAX_PRESSES);

        /// <summary>
        /// Get the position of the current cursor
        /// </summary>
        /// <returns></returns>
        public int GetCursorPosition()
        {
            return API.Memory.ReadInt(CursorQuery);
        }

        /// <summary>
        /// Get the next direction button to press when navigating the menu
        /// </summary>
        /// <param name="currentLocation">Current position of the menu cursor</param>
        /// <param name="endLocation">End position of the menu cursor</param>
        /// <returns>Expected button to press</returns>
        internal Button GetButtonForMenuNavigation(int currentLocation, int endLocation)
        {
            Button increaseButton;
            Button decreaseButton;
            if (Vertical && DownIsUp)
            {
                increaseButton = Button.DOWN;
                decreaseButton = Button.UP;
            }
            else if (Vertical && !DownIsUp)
            {
                increaseButton = Button.UP;
                decreaseButton = Button.DOWN;
            }
            else if (!Vertical && DownIsUp)
            {
                increaseButton = Button.RIGHT;
                decreaseButton = Button.LEFT;
            }
            else
            {
                increaseButton = Button.LEFT;
                decreaseButton = Button.RIGHT;
            }

            if (endLocation - currentLocation > 0)
            {
                return increaseButton;
            }
            return decreaseButton;
        }
    }

    public class MenuFactory : TopLevelModule<Constructor, IMenuConfig>, IMenuConfig
    {
        internal override Dictionary<IGame, Constructor> GameConstructorMap => new() {
            { PokemonGame.GOLD, (menuType, cursorQuery, vertical, downIsUp) => new MenuGoldSilver(menuType, cursorQuery, vertical, downIsUp) },
            { PokemonGame.SILVER, (menuType, cursorQuery, vertical, downIsUp) => new MenuGoldSilver(menuType, cursorQuery, vertical, downIsUp) },
            { PokemonGame.CRYSTAL, (menuType, cursorQuery, vertical, downIsUp) => new MenuCrystal(menuType, cursorQuery, vertical, downIsUp) }
        };

        internal override Dictionary<IGame, IMenuConfig> GameConfigMap => new()
        {
            { PokemonGame.GOLD, new GoldSilverMenuConfig() },
            { PokemonGame.SILVER, new GoldSilverMenuConfig() },
            { PokemonGame.CRYSTAL, new CrystalMenuConfig() }
        };

        /// <summary>
        /// Default menu object that can be used for most menus in the game
        /// </summary>
        public IMenuObject DefaultMenu => CreateObject(MenuType.Single, DefaultMenuCursorQuery);

        /// <summary>
        /// Default cursor location for the menu cursor
        /// </summary>
        public MemoryQuery DefaultMenuCursorQuery => ConfigInstance.DefaultMenuCursorQuery;

        /// <summary>
        /// Default cursor location for the menu cursor X position
        /// </summary>
        public MemoryQuery DefaultMultiCursorXQuery => ConfigInstance.DefaultMultiCursorXQuery;

        /// <summary>
        /// Default cursor location for the menu cursor Y position
        /// </summary>
        public MemoryQuery DefaultMultiCursorYQuery => ConfigInstance.DefaultMultiCursorYQuery;

        /// <summary>
        /// Default cursor location for the scrolling window offset in the menu
        /// </summary>
        public MemoryQuery DefaultViewOffsetQuery => ConfigInstance.DefaultViewOffsetQuery;

        /// <summary>
        /// Default cursor location for the cursor offset in a scrolling window menu
        /// </summary>
        public MemoryQuery DefaultCursorOffsetQuery => ConfigInstance.DefaultCursorOffsetQuery;



        public IMenuObject CreateObject(MenuType menuType, MemoryQuery cursorQuery, bool vertical = true, bool downIsUp = true)
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject(menuType, cursorQuery, vertical, downIsUp);
                }
                else
                {
                    throw new SubModuleNotImplemented();
                }
            }
            else
            {
                throw new SubModuleNotFoundException();
            }
        }
    }
}
