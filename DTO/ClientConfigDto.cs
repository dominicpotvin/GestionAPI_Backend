namespace GestEase.DTO
{
    public class ClientConfigDto
    {
        public string ClientId { get; set; } = "ClientDemoFullAccess";
        public List<string> Features { get; set; } = new() {
            "invoices", "customers", "commandes", "commandes-client", "suppliers", "inventory"
        };
        public string Theme { get; set; } = "light";
        public string LogoUrl { get; set; } = "/assets/logos/client-full.png";
    }
}
