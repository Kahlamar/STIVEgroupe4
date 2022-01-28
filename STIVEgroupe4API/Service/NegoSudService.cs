using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using STIVEgroupe4API.Entity;

namespace STIVEgroupe4API.Service;

public static class NegoSudService
{
    const string ConnexionString = @"Server=localhost;Database=NegoSud;Persist Security Info=True;Integrated Security=SSPI;";
   
    
//---------------------------------------------------------------------------------------------------------------------

    #region Vin

    public static IEnumerable<Vin>? GetAllVin()
    {
        IList<Vin> ?vins = null;

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdVin, Nom, Couleur, Annee, DegreAlcool FROM Vins;");
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return vins;
        while (rdr.Read())
        {
            vins?.Add(ConvertToVin(rdr));
        }

        return vins;
    }
    
    public static Vin GetOneVin(int idVin)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdVin, Nom, Couleur, Annee, DegreAlcool FROM Vins WHERE IdVin = @IdVin;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@IdVin", idVin);
        var rdr = cmd.ExecuteReader();
        var vin = ConvertToVin(rdr);
        return vin;
    }
    
    public static ActionResult CreateOneVin(Vin vin)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"CreateOneVin", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Nom", vin.Domaine);
        cmd.Parameters.AddWithValue("@Couleur", vin.Couleur);
        cmd.Parameters.AddWithValue("@Annee", vin.Annee);
        cmd.Parameters.AddWithValue("@DegreAlcool", vin.DegreAlcool);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult UpdateOneVin(Vin vin)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"UpdateOneVin", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdVin", vin.IdVin);
        cmd.Parameters.AddWithValue("@Nom", vin.Domaine);
        cmd.Parameters.AddWithValue("@Couleur", vin.Couleur);
        cmd.Parameters.AddWithValue("@Annee", vin.Annee);
        cmd.Parameters.AddWithValue("@DegreAlcool", vin.DegreAlcool);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult DeleteOneVin(int idVin)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"DELETE FROM Vin WHERE IdVin = @IdVin;");
        cmd.CommandType = CommandType.Text;
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

    #region Client

    public static IEnumerable<Client>? GetAllClient()
    {
        IList<Client> ?clients = null;

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdClient, Nom, Prenom, Societe, AdresseLivraison FROM Client;");
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return clients;
        while (rdr.Read())
        {
            clients?.Add(ConvertToClient(rdr));
        }

        return clients;
    }
    
    public static Client GetOneClient(int idClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdClient, Nom, Prenom, Societe, AdresseLivraison FROM Client WHERE IdClient = @idClient;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idClient", idClient);
        var rdr = cmd.ExecuteReader();
        var client = ConvertToClient(rdr);
        return client;
    }

    public static ActionResult DeleteOneClient(int idClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"DELETE FROM Client WHERE IdClient = @idClient;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idClient", idClient);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneClient(Client client)
    {
        using SqlConnection con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"UpdateOneClient", con);
        cmd.CommandType = CommandType.StoredProcedure;
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
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"CreateOneClient", con);
        cmd.CommandType = CommandType.StoredProcedure;
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

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdCommandeClient, QuantiteVin, IdClient FROM CommandeClients;");
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return commandeClients;
        while (rdr.Read())
        {
            commandeClients?.Add(ConvertToCommandeClient(rdr));
        }

        return commandeClients;
    }
    
    public static CommandeClient GetOneCommandeClient(int idCommandeClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdCommandeClient, QuantiteVin, IdClient FROM CommandeClients WHERE IdCommandeClient = @idCommandeClient");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idCommandeClient", idCommandeClient);
        var rdr = cmd.ExecuteReader();
        var commandeClient = ConvertToCommandeClient(rdr);
        return commandeClient;
    }
    
    public static ActionResult DeleteOneCommandeClient(int idCommandeClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"DELETE FROM CommandeClient WHERE IdCommandeClient = @idCommandeClient;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idCommandeClient", idCommandeClient);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult CreateOneCommandeClient(CommandeClient commandeClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"CreateOneCommandeClient", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QuantiteVin", commandeClient.QuantiteVin);
        cmd.Parameters.AddWithValue("@IdClient", commandeClient.IdClient);
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static ActionResult UpdateOneCommandeClient(CommandeClient commandeClient)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"UpdateOneCommandeClient", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QuantiteVin", commandeClient.QuantiteVin);
        cmd.Parameters.AddWithValue("@IdCommandeClient", commandeClient.IdCommandeClient);
                
        cmd.ExecuteReader();
        return new OkResult();
    }
    
    public static IEnumerable<CommandeClient>? GetAllCommandesByClient(int idClient)
    {
        IList<CommandeClient> ?commandeClients = null;

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdCommandeClient, QuantiteVin, DateCommande FROM CommandeClients WHERE IdClient = @idClient;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue(@"idClient", idClient);
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return commandeClients;
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
            QuantiteVin = reader.GetFieldValue<int>(1),
            DateCommande = reader.GetFieldValue<DateTime>(2),
            IdClient = reader.GetFieldValue<int>(3)
        };
    }
    
    #endregion

//---------------------------------------------------------------------------------------------------------------------

    #region Commandes Fournisseurs

    public static IEnumerable<CommandeFournisseur>? GetAllCommmandeFournisseur()
    {
        IList<CommandeFournisseur>? commandesFournisseurs = null;

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdCommandeFournisseur, QuantiteVin, IdFournisseur FROM CommandeFournisseurs;");
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return commandesFournisseurs;
        while (rdr.Read())
        {
            commandesFournisseurs?.Add(ConvertToCommandeFournisseur(rdr));
        }

        return commandesFournisseurs;
    }

    public static CommandeFournisseur GetOneCommandeFournisseur(int idCommandeFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdCommandeFournisseur, QuantiteVin, IdFournisseur FROM CommandeFournisseurs WHERE IdCommandeFournisseur = @idCommandeFournisseur");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idCommandeFournisseur", idCommandeFournisseur);
        var rdr = cmd.ExecuteReader();
        var commandeFournisseur = ConvertToCommandeFournisseur(rdr);
        return commandeFournisseur;
    }

    public static ActionResult DeleteOneCommandeFournisseur(int idCommandeFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"DELETE FROM CommandeFournisseurs WHERE IdCommandeFournisseur = @idCommandeFournisseur;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@idCommandeFournisseur", idCommandeFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneCommandeFournisseur(CommandeFournisseur commandeFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"UpdateOneCommandeFournisseur", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdCommandeFournisseur", commandeFournisseur.IdCommandeFournisseur);
        cmd.Parameters.AddWithValue("@QuantiteVin", commandeFournisseur.QuantiteVin);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult CreateOneCommandeFournisseur(CommandeFournisseur commandeFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"CreateOneCommandeFournisseur", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QuantiteVin", commandeFournisseur.QuantiteVin);
        cmd.Parameters.AddWithValue("@IdFournisseur", commandeFournisseur.IdFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    private static CommandeFournisseur ConvertToCommandeFournisseur(DbDataReader reader)
    {
        return new CommandeFournisseur
        {
            IdCommandeFournisseur = reader.GetFieldValue<int>(0),
            QuantiteVin = reader.GetFieldValue<int>(1),
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

        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdFournisseur, SocieteFournisseur FROM Fournisseurs;");
        var rdr = cmd.ExecuteReader();
        if (!rdr.HasRows) return fournisseurs;
        while (rdr.Read())
        {
            fournisseurs?.Add(ConvertToFournisseur(rdr));
        }

        return fournisseurs;
    }

    public static Fournisseur GetOneFournisseur(int idFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT IdFournisseur, SocieteFournisseur FROM Fournisseurs WHERE IdFournisseur = @IdFournisseur;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@IdFournisseur", idFournisseur);
        var rdr = cmd.ExecuteReader();
        var fournisseur = ConvertToFournisseur(rdr);
        return fournisseur;
    }

    public static ActionResult DeleteOneFournisseur(int idFournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"DELETE FROM Fournisseur WHERE IdFournisseur = @IdFournisseur;");
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@IdFournisseur", idFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult UpdateOneFournisseur(Fournisseur fournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"UpdateOneFournisseur", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdFournisseur", fournisseur.IdFournisseur);
        cmd.Parameters.AddWithValue("@SocieteFournisseur", fournisseur.SocieteFournisseur);
        cmd.ExecuteReader();
        return new OkResult();
    }

    public static ActionResult CreateOneFournisseur(Fournisseur fournisseur)
    {
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"InsertOneFournisseur", con);
        cmd.CommandType = CommandType.StoredProcedure;
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
        using var con = new SqlConnection(ConnexionString);
        con.Open();
        var cmd = new SqlCommand(@"SELECT QuantiteRouge, QuantiteRose, QuantiteBlanc, QuantitePetillant, QuantiteDigestif FROM Stock");
        cmd.CommandType = CommandType.Text;
        var rdr = cmd.ExecuteReader();
        var stock = ConvertToStock(rdr);
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