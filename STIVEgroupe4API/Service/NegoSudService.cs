using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using STIVEgroupe4API.Controllers;
using STIVEgroupe4API.Entity;

namespace STIVEgroupe4API.Service;

public static class NegoSudService
{
    const string ConnexionString = @"Server=localhost;Database=NegoSud;Persist Security Info=True;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;";
   
    
//---------------------------------------------------------------------------------------------------------------------

    #region Vin

    public static IEnumerable<Vin>? GetAllVin()
    {
        IList<Vin> ?vins = null;

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdVin, Domaine, Couleur, Annee, DegreAlcool FROM Vins;");
        SqlDataReader? rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) 
            return vins;
        while (rdr.Read())
        {
            vins?.Add(ConvertToVin(rdr));
        }

        return vins;
    }

    public static Vin GetOneVin(int idVin)
    {
        using SqlConnection? con = new(ConnexionString);
        Vin vin = new();
            con.Open();
            SqlCommand? cmd = new(@"SELECT IdVin, Nom, Couleur, Annee, DegreAlcool FROM Vins WHERE IdVin = @IdVin;")
            {
                CommandType = CommandType.Text
            };
        cmd.Parameters.AddWithValue("@IdVin", idVin);
        SqlDataReader? rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            vin = ConvertToVin(rdr);
        }
        
        return vin;
    }
    
    public static ActionResult CreateOneVin(Vin vin)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"CreateOneVin", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Domaine", vin.Domaine);
        cmd.Parameters.AddWithValue("@Couleur", vin.Couleur);
        cmd.Parameters.AddWithValue("@Annee", vin.Annee);
        cmd.Parameters.AddWithValue("@DegreAlcool", vin.DegreAlcool);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult UpdateOneVin(Vin vin)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"UpdateOneVin", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdVin", vin.IdVin);
        cmd.Parameters.AddWithValue("@Domaine", vin.Domaine);
        cmd.Parameters.AddWithValue("@Couleur", vin.Couleur);
        cmd.Parameters.AddWithValue("@Annee", vin.Annee);
        cmd.Parameters.AddWithValue("@DegreAlcool", vin.DegreAlcool);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult DeleteOneVin(int idVin)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"DELETE FROM Vin WHERE IdVin = @IdVin;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdVin", idVin);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    
    private static Vin ConvertToVin(DbDataReader reader)
    {
        return new Vin
        {
            IdVin = reader.GetFieldValue<int>(0),
            Domaine = reader.GetFieldValue<string>(1),
            Couleur = reader.GetFieldValue<string>(2),
            Annee = reader.GetFieldValue<int>(3),
            DegreAlcool = reader.GetFieldValue<int>(4)
        };
    }
    #endregion
    
//---------------------------------------------------------------------------------------------------------------------

    // lsiteCLients = NegoSudService.GetAllClient();

    #region Client

    public static IEnumerable<Client>? GetAllClient()
    {
        List<Client> clients = new();

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdClient, Nom, Prenom, Societe, AdresseLivraison FROM Clients;", con);
        SqlDataReader? rdr = cmd.ExecuteReader();
        //if (!rdr.HasRows)
        //    return clients;
        while (rdr.Read())
        {
            clients.Add(ConvertToClient(rdr));
        }
        return clients;
    }
    
    public static Client GetOneClient(int idClient)
    {
        using SqlConnection? con = new(ConnexionString);

        Client client = new();
        con.Open();
        SqlCommand cmd = new(@"SELECT IdClient, Nom, Prenom, Societe, AdresseLivraison FROM Clients WHERE IdClient = @IdClient;", con)
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdClient", idClient);
        SqlDataReader? rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            client = ConvertToClient(rdr);
        }
        
        return client;
    }

    public static ActionResult DeleteOneClient(int idClient)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"DELETE FROM Client WHERE IdClient = @IdClient;", con)
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdClient", idClient);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneClient(Client client)
    {
        using SqlConnection con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"UpdateOneClient", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdClient", client.IdClient);
        cmd.Parameters.AddWithValue("@Nom", client.Nom);
        cmd.Parameters.AddWithValue("@Prenom", client.Prenom);
        cmd.Parameters.AddWithValue("@Societe", client.Societe);
        cmd.Parameters.AddWithValue("@AdresseLivraison", client.AdresseLivraison);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult CreateOneClient(Client client)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"CreateOneClient", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Nom", client.Nom);
        cmd.Parameters.AddWithValue("@Prenom", client.Prenom);
        cmd.Parameters.AddWithValue("@Societe", client.Societe);
        cmd.Parameters.AddWithValue("@AdresseLivraison", client.AdresseLivraison);
        cmd.ExecuteReader();
        return new OkResult();
    }

    private static Client ConvertToClient(DbDataReader reader)
    {
        return new Client
        {
            IdClient = reader.GetFieldValue<int>(0),
            Nom = reader.GetFieldValue<string>(1),
            Prenom = reader.GetFieldValue<string>(2),
            Societe = reader.GetFieldValue<string>(3),
            AdresseLivraison = reader.GetFieldValue<string>(4),
            
        };
    }
    
    
    #endregion
    
//---------------------------------------------------------------------------------------------------------------------

    #region Commande Client

    public static IEnumerable<CommandeClient>? GetAllCommandeClient()
    {
        IList<CommandeClient> ?commandeClients = null;

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdCommandeClient, IdClient, QuantiteRouge, QuantiteRose, QuantiteBlanc, 
                              QuantitePetillant, QuantiteDigestif FROM CommandeClients;");
        SqlDataReader? rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) 
            return commandeClients;
        while (rdr.Read())
        {
            commandeClients?.Add(ConvertToCommandeClient(rdr));
        }

        return commandeClients;
    }
    
    public static CommandeClient GetOneCommandeClient(int idCommandeClient)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdCommandeClient, IdClient, QuantiteRouge, QuantiteRose, QuantiteBlanc, 
                              QuantitePetillant, QuantiteDigestif FROM CommandeClients WHERE IdCommandeClient = @IdCommandeClient")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdCommandeClient", idCommandeClient);
        SqlDataReader? rdr = cmd.ExecuteReader();
        CommandeClient? commandeClient = ConvertToCommandeClient(rdr);
        return commandeClient;
    }
    
    public static ActionResult DeleteOneCommandeClient(int idCommandeClient)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"DELETE FROM CommandeClient WHERE IdCommandeClient = @IdCommandeClient;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdCommandeClient", idCommandeClient);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult CreateOneCommandeClient(CommandeClient commandeClient)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"CreateOneCommandeClient", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@QuantiteRouge", commandeClient.QuantiteRouge);
        cmd.Parameters.AddWithValue("@QuantiteRose", commandeClient.QuantiteRose);
        cmd.Parameters.AddWithValue("@QuantiteBlanc", commandeClient.QuantiteBlanc);
        cmd.Parameters.AddWithValue("@QuantitePetillant", commandeClient.QuantitePetillant);
        cmd.Parameters.AddWithValue("@QuantiteDigestif", commandeClient.QuantiteDigestif);
        cmd.Parameters.AddWithValue("@IdClient", commandeClient.IdClient);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult UpdateOneCommandeClient(CommandeClient commandeClient)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"UpdateOneCommandeClient", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@QuantiteRouge", commandeClient.QuantiteRouge);
        cmd.Parameters.AddWithValue("@QuantiteRose", commandeClient.QuantiteRose);
        cmd.Parameters.AddWithValue("@QuantiteBlanc", commandeClient.QuantiteBlanc);
        cmd.Parameters.AddWithValue("@QuantitePetillant", commandeClient.QuantitePetillant);
        cmd.Parameters.AddWithValue("@QuantiteDigestif", commandeClient.QuantiteDigestif);
        cmd.Parameters.AddWithValue("@IdCommandeClient", commandeClient.IdCommandeClient);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static IEnumerable<CommandeClient>? GetAllCommandesByClient(int idClient)
    {
        IList<CommandeClient> ?commandeClients = null;

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdCommandeClient, IdClient, QuantiteRouge, QuantiteRose, QuantiteBlanc, 
                              QuantitePetillant, QuantiteDigestif, DateCommande FROM CommandeClients WHERE IdClient = @IdClient;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue(@"IdClient", idClient);
        SqlDataReader? rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) 
            return commandeClients;
        while (rdr.Read())
        {
            commandeClients?.Add(ConvertToCommandeClient(rdr));
        }

        return commandeClients;
    }



    private static CommandeClient ConvertToCommandeClient(DbDataReader reader)
    {
        return new CommandeClient
        {
            IdCommandeClient = reader.GetFieldValue<int>(0),
            QuantiteRouge = reader.GetFieldValue<int>(1),
            QuantiteRose = reader.GetFieldValue<int>(2),
            QuantiteBlanc = reader.GetFieldValue<int>(3),
            QuantitePetillant = reader.GetFieldValue<int>(4),
            QuantiteDigestif = reader.GetFieldValue<int>(5),
            DateCommande = reader.GetFieldValue<DateTime>(6),
            IdClient = reader.GetFieldValue<int>(7)
        };
    }
    
    #endregion

//---------------------------------------------------------------------------------------------------------------------

    #region Commandes Fournisseurs

    public static IEnumerable<CommandeFournisseur>? GetAllCommmandeFournisseur()
    {
        IList<CommandeFournisseur>? commandesFournisseurs = null;

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdCommandeFournisseur, IdFournisseur, QuantiteRouge, QuantiteRose, QuantiteBlanc, 
                              QuantitePetillant, QuantiteDigestif FROM CommandeFournisseurs;");
        SqlDataReader? rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) 
            return commandesFournisseurs;
        while (rdr.Read())
        {
            commandesFournisseurs?.Add(ConvertToCommandeFournisseur(rdr));
        }
        return commandesFournisseurs;
    }

    public static CommandeFournisseur GetOneCommandeFournisseur(int idCommandeFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdCommandeFournisseur, IdFournisseur, QuantiteRouge, QuantiteRose, QuantiteBlanc, 
                              QuantitePetillant, QuantiteDigestif FROM CommandeFournisseurs WHERE IdCommandeFournisseur = @IdCommandeFournisseur")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdCommandeFournisseur", idCommandeFournisseur);
        SqlDataReader? rdr = cmd.ExecuteReader();
        CommandeFournisseur? commandeFournisseur = ConvertToCommandeFournisseur(rdr);
        return commandeFournisseur;
    }

    public static ActionResult DeleteOneCommandeFournisseur(int idCommandeFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"DELETE FROM CommandeFournisseurs WHERE IdCommandeFournisseur = @IdCommandeFournisseur;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdCommandeFournisseur", idCommandeFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneCommandeFournisseur(CommandeFournisseur commandeFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"UpdateOneCommandeFournisseur", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdCommandeFournisseur", commandeFournisseur.IdCommandeFournisseur);
        cmd.Parameters.AddWithValue("@QuantiteRouge", commandeFournisseur.QuantiteRouge);
        cmd.Parameters.AddWithValue("@QuantiteRose", commandeFournisseur.QuantiteRose);
        cmd.Parameters.AddWithValue("@QuantiteBlanc", commandeFournisseur.QuantiteBlanc);
        cmd.Parameters.AddWithValue("@QuantitePetillant", commandeFournisseur.QuantitePetillant);
        cmd.Parameters.AddWithValue("@QuantiteDigestif", commandeFournisseur.QuantiteDigestif);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult CreateOneCommandeFournisseur(CommandeFournisseur commandeFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"CreateOneCommandeFournisseur", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@QuantiteRouge", commandeFournisseur.QuantiteRouge);
        cmd.Parameters.AddWithValue("@QuantiteRose", commandeFournisseur.QuantiteRose);
        cmd.Parameters.AddWithValue("@QuantiteBlanc", commandeFournisseur.QuantiteBlanc);
        cmd.Parameters.AddWithValue("@QuantitePetillant", commandeFournisseur.QuantitePetillant);
        cmd.Parameters.AddWithValue("@QuantiteDigestif", commandeFournisseur.QuantiteDigestif);
        cmd.Parameters.AddWithValue("@IdFournisseur", commandeFournisseur.IdFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    private static CommandeFournisseur ConvertToCommandeFournisseur(DbDataReader reader)
    {
        return new CommandeFournisseur
        {
            IdCommandeFournisseur = reader.GetFieldValue<int>(0),
            QuantiteRouge = reader.GetFieldValue<int>(1),
            QuantiteRose = reader.GetFieldValue<int>(2),
            QuantiteBlanc = reader.GetFieldValue<int>(3),
            QuantitePetillant = reader.GetFieldValue<int>(3),
            QuantiteDigestif = reader.GetFieldValue<int>(3),
            DateCommande = reader.GetFieldValue<DateTime>(2),
            IdFournisseur = reader.GetFieldValue<int>(3)
        };
    }

    #endregion

//---------------------------------------------------------------------------------------------------------------------

    #region Fournisseur

    public static IEnumerable<Fournisseur>? GetAllFournisseurs()
    {
        IList<Fournisseur>? fournisseurs = null;

        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdFournisseur, SocieteFournisseur FROM Fournisseurs;");
        SqlDataReader? rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) 
            return fournisseurs;
        while (rdr.Read())
        {
            fournisseurs?.Add(ConvertToFournisseur(rdr));
        }
        return fournisseurs;
    }

    public static Fournisseur GetOneFournisseur(int idFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT IdFournisseur, SocieteFournisseur FROM Fournisseurs WHERE IdFournisseur = @IdFournisseur;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdFournisseur", idFournisseur);
        SqlDataReader? rdr = cmd.ExecuteReader();
        Fournisseur? fournisseur = ConvertToFournisseur(rdr);
        return fournisseur;
    }

    public static ActionResult DeleteOneFournisseur(int idFournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"DELETE FROM Fournisseur WHERE IdFournisseur = @IdFournisseur;")
        {
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@IdFournisseur", idFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneFournisseur(Fournisseur fournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"UpdateOneFournisseur", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdFournisseur", fournisseur.IdFournisseur);
        cmd.Parameters.AddWithValue("@SocieteFournisseur", fournisseur.SocieteFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult CreateOneFournisseur(Fournisseur fournisseur)
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"InsertOneFournisseur", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@SocieteFournisseur", fournisseur.SocieteFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    private static Fournisseur ConvertToFournisseur(DbDataReader reader)
    {
        return new Fournisseur
        {
            IdFournisseur = reader.GetFieldValue<int>(0),
            SocieteFournisseur = reader.GetFieldValue<string>(1)
        };
    }

    #endregion
    
//---------------------------------------------------------------------------------------------------------------------

    #region Stock

    public static Stock InventaireStock()
    {
        using SqlConnection? con = new(ConnexionString);
        con.Open();
        SqlCommand? cmd = new(@"SELECT QuantiteRouge, QuantiteRose, QuantiteBlanc, QuantitePetillant, QuantiteDigestif FROM Stock")
        {
            CommandType = CommandType.Text
        };
        SqlDataReader? rdr = cmd.ExecuteReader();
        Stock? stock = ConvertToStock(rdr);
        return stock;
    }
    private static Stock ConvertToStock(DbDataReader reader)
    {
        return new Stock
        {
            QuantiteRouge = reader.GetFieldValue<int>(0),
            QuantiteRose = reader.GetFieldValue<int>(1),
            QuantiteBlanc = reader.GetFieldValue<int>(2),
            QuantitePetillant = reader.GetFieldValue<int>(3),
            QuantiteDigestif = reader.GetFieldValue<int>(4)
        };
    }

    #endregion


}