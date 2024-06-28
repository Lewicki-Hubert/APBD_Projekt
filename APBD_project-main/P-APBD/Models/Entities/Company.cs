namespace Projekt.Entities
{
    public class Company
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public virtual Customer Customer { get; set; }
    }
}