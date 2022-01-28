namespace STIVEgroupe4API.Entity;

public class CommandeFournisseur
{
    public int IdCommandeFournisseur { get; set; }
    public int QuantiteVin { get; set; }
    
    public DateTime DateCommande { get; set; }
    public int ?IdFournisseur { get; set; }
    
}