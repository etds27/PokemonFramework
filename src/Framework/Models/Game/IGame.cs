namespace PokemonFramework.Framework.Models.Game
{
    public abstract class IGame
    {
        public abstract string Name { get; }
        public abstract string RomHash { get; }
    }
}
