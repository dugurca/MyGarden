using System.Collections.Generic;
using _Game.Source.Gameplay;

namespace _Game.Source.CS
{
    public static class InitialData
    {
        public static List<Plot> GetPlotsData()
        {
            var plotsList = new List<Plot>
            {
                new Plot(0, "Potato", 100, false, 1.5, 2.0, 1.0, 0.2, 2.0, 1.2, 2.0, 1.0, 2.0, 1.2),
                new Plot(1, "Cabbage", 250, false, 3, 2.0, 1.0, 0.2, 2.0, 1.2, 2.5, 1.5, 2.2, 1.2),
                new Plot(2, "Tomato", 5000, false, 6, 2.2, 1.2, 0.3, 2.2, 1.3, 5.0, 1.8, 2.4, 1.3),
                new Plot(3, "Apple", 25000, false, 16, 2.3, 1.3, 0.4, 2.3, 1.3, 7.5, 1.9, 2.5, 1.3),
                new Plot(4, "Melon", 1000000, false, 30, 2.4, 1.4, 0.5, 2.5, 1.3, 7.5, 2.0, 2.5, 1.4),
            };

            return plotsList;
        }

        public const double InitialCoins = 150;
    }
}