namespace WebChangeApi.Models
{
    public class ChangeResponse
    {
        public string Base_currency_code { get; set; }
        public string Amount { get; set; }
        public Rates Rates { get; set; }
    }
}