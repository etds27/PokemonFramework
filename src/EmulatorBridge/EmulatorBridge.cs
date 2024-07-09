using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge
{
    public static class EmulatorBridge
    {
        public static IEmulatorInterface GetEmulator(string emulatorName)
        {
            return emulatorName switch
            {
                "BizHawk" => new BizHawkEmulatorBridge(),
                _ => new BizHawkEmulatorBridge(),
            };
        }
    }
}
