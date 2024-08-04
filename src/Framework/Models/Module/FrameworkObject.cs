using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Module
{
    /// <summary>
    /// Class to represent objects whose properties can be directly found within memory
    /// </summary>
    public abstract class FrameworkObject : FrameworkModule, IMemoryObject
    {
        public abstract void Update();

    }

    public interface IMemoryObject
    {
        public abstract void Update();
    }
}
