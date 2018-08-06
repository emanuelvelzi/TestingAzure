namespace TestingAzure.Entities
{
    public class Stadium
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string Description { get; set; }
    }
}
