namespace Accounting.Models
{
    public class Party
    {
        public int PartyID { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Pan { get; set; }
        public bool IsVendor { get; set; }
        public bool IsCustomer { get; set; }
    }
}
