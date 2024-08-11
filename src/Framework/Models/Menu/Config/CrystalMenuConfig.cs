using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Menu.Config
{
    internal class CrystalMenuConfig : IMenuConfig
    {
        MemoryQuery IMenuConfig.DefaultMenuCursorQuery => new(0xCFA9, 1, MemoryDomain.WRAM);

        MemoryQuery IMenuConfig.DefaultMultiCursorXQuery => new(0xCFA9, 1, MemoryDomain.WRAM);

        MemoryQuery IMenuConfig.DefaultMultiCursorYQuery => new(0xCFA8, 1, MemoryDomain.WRAM);

        MemoryQuery IMenuConfig.DefaultViewOffsetQuery => new(0xCA2B, 1, MemoryDomain.WRAM);

        MemoryQuery IMenuConfig.DefaultCursorOffsetQuery => new(0xCA2A, 1, MemoryDomain.WRAM);
    }
}
