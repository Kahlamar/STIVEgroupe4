namespace STIVEgroupe4API.Entity;

public class CommandeClient
{
    public int IdCommandeClient { get; set; }
    public int QuantiteVin { get; set; }
    
    public DateTime DateCommande { get; set; }
    public int ?IdClient { get; set; }
}