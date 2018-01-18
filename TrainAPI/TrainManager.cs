using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using TrainAPI.Models;
using TrainAPI.Properties;

namespace TrainAPI
{
    public class TrainManager
    {
        public static List<Train> Trains { get; set; }

        private static List<string> Stations { get; set; }

        private static Random Random { get; set; }

        private Timer Timer { get; set; }

        public TrainManager()
        {
            Trains = new List<Train>();
            Random = new Random();
            Stations = JsonConvert.DeserializeObject<List<string>>(Resources.Stations.ToString());
            this.CreateTrains();
        }

        private async void CreateTrains()
        {
            for (int i = 0; i < Random.Next(4, 7); i++)
            {
                Trains.Add(await CreateRandomTrain());
            }

            this.ModifyTrain(null, EventArgs.Empty);
        }

        /// <summary>
        /// Modify the amount of passengers in a train
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        private void ModifyTrain(object source, EventArgs args)
        {
            if (Trains.Count > 0)
            {
                foreach (Compartment compartment in Trains[Random.Next(Trains.Count - 1)].Compartments)
                {
                    compartment.PeopleCount = Random.Next(20);
                }
            }

            if (this.Timer != null)
            {
                this.Timer.Stop();
                this.Timer.Enabled = false;
                this.Timer = null;
            }

            this.Timer = new Timer(Random.Next(5000));
            this.Timer.Elapsed += this.ModifyTrain;
            this.Timer.Enabled = true;
            this.Timer.Start();
        }

        /// <summary>
        /// Get a random Station name
        /// </summary>
        /// <returns>Name of a station picked at random</returns>
        public async static Task<string> GetRandomStation()
        {
            if (Stations.Count > 0)
            {
                return Stations[Random.Next(Stations.Count - 1)];
            }

            await Task.Delay(10);
            return await GetRandomStation();
        }

        /// <summary>
        /// Create a random train with random comparments
        /// </summary>
        /// <returns>The New Train</returns>
        private async static Task<Train> CreateRandomTrain()
        {
            int id = Trains.Count + 1;
            string startingPoint = await GetRandomStation();
            string destination = "";
            while (destination == "" || destination == startingPoint)
            {
                destination = await GetRandomStation();
            }

            Train train = new Train(id, startingPoint, destination);
            for (int i = 0; i < Random.Next(4, 8); i++)
            {
                train.SetCompartment(i, Random.Next(0, 14));
            }

            return train;
        }
    }
}
