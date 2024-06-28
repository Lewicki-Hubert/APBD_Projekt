namespace Projekt.Entities
{
    public class Individual
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? HomeAddress { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? NationalId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}