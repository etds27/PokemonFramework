using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge.GameInterface
{
    public abstract class IGameInterface
    {
        public abstract string GetRomName();
        public abstract string GetRomHash();
    }
}
