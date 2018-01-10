namespace TrainAPI.Models
{
    public class Compartment
    {
        public int Id { get; }

        public int PeopleCount { get; }

        public Compartment(int id, int peopleCount)
        {
            this.Id = id;
            this.PeopleCount = peopleCount;
        }
    }
}
