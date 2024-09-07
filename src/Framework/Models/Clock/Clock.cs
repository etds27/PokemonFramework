using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Clock.Object;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Framework.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Clock
{
    using Constructor = Func<IClockObject>;

    public sealed class TimeOfDay(int startTime, int endTime) : IEquatable<GameTime>
    {
        public class TimeOfDayException : Exception
        {
            public TimeOfDayException() : base() { }
            public TimeOfDayException(string message) : base(message) { }
        }

        public int StartTime = startTime;
        public int EndTime = endTime;


        public static TimeOfDay Morning = new (4, 10);
        public static TimeOfDay Day = new (10, 18);
        public static TimeOfDay Night = new (18, 4);


        public static bool IsMorning(GameTime gameTime)
        {
            return Morning.Equals(gameTime);
        }

        public static bool IsDay(GameTime gameTime)
        {
            return Day.Equals(gameTime);
        }

        public static bool IsNight(GameTime gameTime)
        {
            return Night.Equals(gameTime);
        }

        public bool Equals(GameTime gameTime)
        {
            return gameTime.Hour >= StartTime && gameTime.Hour < EndTime;
        }

        public static TimeOfDay GetTimeOfDay(GameTime gameTime)
        {
            if (IsMorning(gameTime)) return Morning;
            if (IsDay(gameTime)) return Day;
            if (IsNight(gameTime));
            throw new TimeOfDayException($"Unable to determine the Time Of Day for time: {gameTime}");
        }
    }

    public struct GameTime
    {
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
    }

    public abstract class IClockObject : FrameworkObject
    {
        public GameTime Time => new ()
            {
                Day = Day,
                Hour = Hour,
                Minute = Minute,
                Second = Second
            };

        public TimeOfDay TimeOfDay => TimeOfDay.GetTimeOfDay(Time);

        internal abstract MemoryQuery DayQuery { get; }

        /// <summary>
        /// The current day within the game's clock
        /// </summary>
        public int Day => API.Memory.ReadInt(DayQuery);


        internal abstract MemoryQuery HourQuery { get; }

        /// <summary>
        /// The current hour within the game's clock
        /// </summary>
        public int Hour => API.Memory.ReadInt(HourQuery);
        

        internal abstract MemoryQuery MinuteQuery { get; }

        /// <summary>
        /// The current minute within the game's clock
        /// </summary>
        public int Minute => API.Memory.ReadInt(MinuteQuery);


        internal abstract MemoryQuery SecondQuery { get; }

        /// <summary>
        /// The current second within the game's clock
        /// </summary>
        public int Second => API.Memory.ReadInt(SecondQuery);
    }


    public class ClockFactory() : TopLevelModule<Constructor, object>
    {
        internal override Dictionary<IGame, Constructor> GameConstructorMap => new()
        {
            { PokemonGame.GOLD, () => new ClockGoldSilver() },
            { PokemonGame.SILVER, () => new ClockGoldSilver() },
            { PokemonGame.CRYSTAL, () => new ClockCrystal() }
        };

        public IClockObject CreateObject()
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject();
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
