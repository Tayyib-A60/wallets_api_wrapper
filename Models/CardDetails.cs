namespace wallets_api_wrapper.Models
{
    public class CardDetails
    {
        public string Scheme { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public bool Prepaid { get; set; }
        public Country Country { get; set; }
        public Bank Bank { get; set; }

    }

    public class Country
    {
        public string Name { get; set; }
        public string Currency { get; set; }
    }
    public class Bank
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}