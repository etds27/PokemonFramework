using System.Reflection;

namespace PokemonFramework.EmulatorBridge
{
    /// <summary>
    /// Factory class to get the proper emulator interface for the running executable
    /// </summary>
    public class EmulatorFactory
    {
        /// <summary>
        /// Get the appropriate emulator for the current application
        /// 
        /// The emulator type can be overwritten by setting the "POKEMON_FRAMEWORK_EMULATOR" environment variable to a different emulator name
        /// If no env variable override is provided, the emulator is determined by the application's process name
        /// </summary>
        /// <returns>Emulator for the appropriate application</returns>
        public static IEmulatorInterface GetEmulator()
        {
            string emulatorName;

            // First check to see if there is an environment variable override
            string environmentVariable = System.Environment.GetEnvironmentVariable("POKEMON_FRAMEWORK_EMULATOR");
            if (environmentVariable != null) {
                emulatorName = environmentVariable;
                Serilog.Log.Information("Emulator name through environment variable");
            } else
            {
                emulatorName = Assembly.GetEntryAssembly().GetName().Name;
                Serilog.Log.Information("Emulator name through host process");
            }

            Serilog.Log.Information("Resolved emulator name: {Name}", emulatorName);


            if (emulatorName == "EmuHawk")
            {
                Serilog.Log.Information("Returning BizHawkEmulatorBridge");
                return new BizHawkEmulatorBridge();
            }

            // Default to BizHawk for now
            Serilog.Log.Information("Defaulting to BizHawkEmulatorBridge");
            return new BizHawkEmulatorBridge();
        }
    }
}
