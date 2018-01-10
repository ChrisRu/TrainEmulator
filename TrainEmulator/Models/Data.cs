using System;

namespace TrainEmulator.Models
{
    public static class Data
    {
        public static readonly string[] Stations = {
            "Den Haag HS",
            "Den Haag Centraal",
            "Arnhem",
            "Amersfoort",
            "'s Hertogenbosch",
            "Eindhoven",
            "Schiphol",
            "Leiden Centraal",
            "Rotterdam Centraal",
            "Amsterdam Centraal",
            "Utrecht Centraal",
            "Amsterdam Sloterdijk",
            "Groningen",
            "Zwolle",
            "Nijmegen",
            "Haarlem",
            "Tilburg",
            "Breda",
            "Amsterdam Zuid"
        };

        private static Random Random = new Random();

        /// <summary>
        /// Get a random station name from the Stations array
        /// </summary>
        /// <returns>Station Name</returns>
        public static string GetRandomStation()
        {
            return Stations[Random.Next(0, Stations.Length)];
        }
    }
}
