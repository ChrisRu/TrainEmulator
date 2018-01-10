using System.Collections.Generic;

namespace TrainEmulator.Models
{
    public class Train
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public string StartingPoint { get; set; }

        public List<Compartment> Compartments { get; set; }

        public Train(int id, string startingPoint, string destination)
        {
            this.Compartments = new List<Compartment>();
            this.Id = id;
            this.Destination = destination;
            this.StartingPoint = startingPoint;
        }

        public Compartment SetCompartment(int compartmentId, int peopleCount)
        {
            Compartment compartment = new Compartment(compartmentId, peopleCount);
            this.Compartments.Add(compartment);
            return compartment;
        }
    }
}
