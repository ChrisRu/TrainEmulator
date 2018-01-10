using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TrainAPI.Models;

namespace TrainAPI.Controllers
{
    [Route("/")]
    public class TrainController : Controller
    {
        public List<Train> Trains { get; }

        private Random Random { get; }

        public TrainController()
        {
            this.Random = new Random();
            this.Trains = new List<Train>();

            for (int i = 0; i < this.Random.Next(4, 7); i++)
            {
                this.Trains.Add(this.CreateRandomTrain());
            }
        }

        // GET /
        // Returns all trains
        [HttpGet]
        public List<Train> Get()
        {
            return this.Trains;
        }

        // GET /{id}
        // Returns train matching ID
        [HttpGet("{id}")]
        public Train Get(int id)
        {
            return this.Trains.Find(train => train.Id == id);
        }

        /// <summary>
        /// Create a random train with random comparments
        /// </summary>
        /// <returns>The New Train</returns>
        private Train CreateRandomTrain()
        {
            int id = this.Trains.Count + 1;
            string startingPoint = Data.GetRandomStation();
            string destination = Data.GetRandomStation();

            Train train = new Train(id, startingPoint, destination);
            for (int i = 0; i < this.Random.Next(4, 8); i++)
            {
                train.SetCompartment(i, this.Random.Next(0, 14));
            }

            return train;
        }
    }
}
