using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TrainAPI.Models;

namespace TrainAPI.Controllers
{
    [Route("/")]
    public class TrainController : Controller
    {
        private Random Random { get; }

        [EnableCors("MyPolicy")]
        [HttpGet]
        public List<Train> GetTrains()
        {
            return TrainManager.Trains;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        public Train GetTrain(int id)
        {
            return TrainManager.Trains.Find(train => train.Id == id);
        }

        [EnableCors("MyPolicy")]
        [HttpPost("/random")]
        public ActionResult ToggleRandom()
        {
            var data = this.Request.Form;

            if (!string.IsNullOrEmpty(data["enabled"]))
            {
                try
                {
                    bool enabled = bool.Parse(data["enabled"]);
                    TrainManager.RandomInterval = enabled;
                }
                catch (Exception)
                {
                    return BadRequest("Property enabled is not valid");
                }
            }

            return Ok(TrainManager.RandomInterval);
        }

        [EnableCors("MyPolicy")]
        [HttpPost("{id}/edit")]
        public ActionResult EditTrain(int id)
        {
            var train = this.GetTrain(id);
            if (train == null)
                return BadRequest("No such train exists");

            var data = this.Request.Form;

            if (!string.IsNullOrEmpty(data["destination"]))
                train.Destination = data["destination"];

            if (!string.IsNullOrEmpty(data["startingPoint"]))
                train.StartingPoint = data["startingPoint"];

            return Ok(train);
        }

        [EnableCors("MyPolicy")]
        [HttpPost("{id}/{compartment}/edit")]
        public ActionResult EditCompartment(int id, int compartmentId)
        {
            var train = this.GetTrain(id);
            if (train == null)
                return BadRequest("No such train exists");

            var compartment = train.Compartments.Find(comp => comp.Id == compartmentId);
            if (compartment == null)
                return BadRequest("No such compartment exists on this train");

            var data = this.Request.Form;

            if (!string.IsNullOrEmpty(data["peopleCount"]))
            {
                try
                {
                    int peopleCount = int.Parse(data["peopleCount"]);
                    compartment.PeopleCount = peopleCount;
                }
                catch (Exception)
                {
                    return BadRequest("Could not parse integer peopleCount");
                }
            }

            return Ok(train);
        }
    }
}
