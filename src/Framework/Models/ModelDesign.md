# Framework Modules

A framework feature can be broken down into three general parts.

## 1. Module Object
This will be the game specific object that is created to represent some instance of a feature within the game.
An example of this would be an `IPokemonObject`. 
There can exist many different `IPokemonObjects` in the memory sapce that can be created and queried.

Each module object should inherit from `FrameworkObject` which will provide the class with an instance of the emulator bridge

The public facing API of these objects should be platform agnostic.
Any non public properties like memory queries should be implemented as abstract properties within the abstract class and then each generation can define their own internal properties
For static properties that will be exposed as a part of the public API, these should be defined in the Module Config

```
    public abstract class IModuleObject : FrameworkObject
    {
        public MemoryAddress Address;
        public IModuleObject(MemoryAddress address)
        {
            Address = address;
        }
    }
```

## 2. Module Config
Primarily used for providing game specific static properties for the API at runtime.
These properties are then exposed through the instance of the Factory that creates the config.

```
    public interface IModuleConfig
    {
        public int ConfigProperty1 { get; }
        public int ConfigProperty2 { get; }
        public int ConfigProperty3 { get; }
    }

    public class ModuleConfigGoldSilver : IModuleConfig
    {
        public int ConfigProperty1 => 1;
        public int ConfigProperty2 => 2;
        public int ConfigProperty3 => 3;
    }
```

## 3. Module Factory

The factory is resposible for creating instances of the Module object.
To create a factory, normally the following steps will need to be taken
1. Define a typealias for the constructor of the Module Object
1. Have the Module Factory inherit from ``TopLevelModule``
1. Provide the constructor and the Module Config interface as the generic types for the `TopLevelModule`
    - If there is no config for the Module, provide `object`
1. Make the Factory conform to the Module Config interface
1. Create the `GameConfigMap`
    - This will associate a specific Pokemon Game, to the Module Config that was written for that game
1. Create the `GameConstructorMap`
    - This will associate a specific Pokemon Game, with the Module Object constructor for that game
1. Make the `CreateObject` method that will create the game specific Module Object
1. conform to the Module Config interface by exposing the `ConfigInstance`'s properties

Example
```
    // Providing the constructor for the Module Object to be easily referenced later
    using Constructor = Func<MemoryAddress, IModuleObject>;

    // Having Factory class inherit from TopLevelModuel and passing in the new Constructor and Config interface
    public class ModuleFactory : TopLevelModule<Constructor, IModuleConfig>, IModuleConfig
    {
        // Define mapping of Game to Config
        internal new Dictionary<IGame, IModuleConfig> GameConfigMap = new()
        {
            { PokemonGame.GOLD, new ModuleConfigGoldSilver() },
            { PokemonGame.SILVER, new ModuleConfigGoldSilver() },
            { PokemonGame.CRYSTAL, new ModuleConfigCrystal() }
        };

        // Define mapping of game to Object Constructor
        internal override Dictionary<IGame, Constructor> GameConstructorMap => new()
        {
            { PokemonGame.GOLD, (address) => new ModuleGoldSilver(address) },
            { PokemonGame.SILVER, (address) => new ModuleGoldSilver(address) },
            { PokemonGame.CRYSTAL, (address) => new ModuleCrystal(address) }
        };

        // Create an instance of the module object using the constructors from `GameConstructorMap`
        public IModuleObject CreateObject(MemoryAddress address)
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject(address);
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

        // Conform to the Module Config interface by using the static `ConfigInstance`
        public int ConfigProperty1 => ConfigInstance.ConfigProperty1;
        public int ConfigProperty2 => ConfigInstance.ConfigProperty2;
        public int ConfigProperty3 => ConfigInstance.ConfigProperty3;
    }
```