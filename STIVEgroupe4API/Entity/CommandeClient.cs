namespace STIVEgroupe4API.Entity;

public class CommandeClient
{
    public int IdCommandeClient { get; set; }
    public int IdClient { get; set; }
    public DateTime DateCommande { get; set; }
    public string Societe { get; set; }
    public string AdresseLivraison { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int QuantiteRouge { get; set; }
    public int QuantiteRose { get; set; }
    public int QuantiteBlanc { get; set; }
    public int QuantitePetillant { get; set; }
    public int QuantiteDigestif { get; set; }
    
}