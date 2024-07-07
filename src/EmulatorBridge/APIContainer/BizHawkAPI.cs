using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge.APIContainer
{
    internal class BizHawkAPI
    {
        public static ApiContainer? _APIContainer { get; set; }

        public static ApiContainer API => _APIContainer!;

        public static void SetAPIContainer(ApiContainer apiContainer)
        {
            _APIContainer = apiContainer;
        }
    }
}
