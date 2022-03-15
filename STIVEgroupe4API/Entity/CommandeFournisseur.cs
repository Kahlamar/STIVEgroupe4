namespace STIVEgroupe4API.Entity;

public class CommandeFournisseur
{
    public int IdCommandeFournisseur { get; set; }
    public DateTime DateCommande { get; set; }
    public string SocieteFournisseur { get; set; }
    public int QuantiteRouge { get; set; }
    public int QuantiteRose { get; set; }
    public int QuantiteBlanc { get; set; }
    public int QuantitePetillant { get; set; }
    public int QuantiteDigestif { get; set; }
    
    public int IdFournisseur { get; set; }

}