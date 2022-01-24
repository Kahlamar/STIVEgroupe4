namespace STIVEgroupe4API.Entity;

public class Vin
{
    public int IdVin { get; set; }
    public string ?Nom { get; set; }

    public string ?Couleur { get; set; }
    
    public int Annee { get; set; }
    
    public int DegreAlcool { get; set; }
    
}