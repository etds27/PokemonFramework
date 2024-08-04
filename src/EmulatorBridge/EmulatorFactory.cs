using System.Reflection;

namespace PokemonFramework.EmulatorBridge
{
    public class EmulatorFactory
    {
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
