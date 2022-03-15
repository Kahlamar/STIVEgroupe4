using Microsoft.Data.SqlClient;
using STIVEgroupe4API.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfaceBloc4STIVE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string connectionString = @"Server=localhost;Database=NegoSud;Persist Security Info=True;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;";
        
        
        public MainWindow()
        {
            InitializeComponent();
            GridCommandesClients.ItemsSource = GetListeCommandeClients();
            GridCommandesFournisseurs.ItemsSource = GetListeCommandeFournisseurs();
            Stock stock = GetStock();
            this.DataContext = stock;
        }

        public IList<CommandeClient> GetListeCommandeClients()
        {
            IList<CommandeClient> commandeClients = new List<CommandeClient>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand(
                            @"SELECT cc.IdCommandeClient, cl.IdClient, cc.DateCommande, cl.Societe, cl.AdresseLivraison,
                            cl.Nom, cl.Prenom, cc.QuantiteRouge, cc.QuantiteRose, cc.QuantiteBlanc,
                            cc.QuantiteDigestif, cc.QuantitePetillant FROM [CommandeClients] AS cc
                            INNER JOIN Clients AS cl ON cc.IdClient = cl.IdClient", conn);

                // execute the command
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    commandeClients.Add(ConvertToCommandeClients(rdr));
                }
                
                return commandeClients;
            }
        }


        public IList<CommandeFournisseur> GetListeCommandeFournisseurs()
        {
            IList<CommandeFournisseur> commandeFournisseur = new List<CommandeFournisseur>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand(
                            @"SELECT cf.IdCommandeFournisseur, cf.IdFournisseur, cf.DateCommande, f.SocieteFournisseur, cf.QuantiteRouge, cf.QuantiteRose, cf.QuantiteBlanc,
                            cf.QuantitePetillant, cf.QuantiteDigestif FROM CommandeFournisseurs AS cf
                            INNER JOIN Fournisseurs AS f ON cf.IdFournisseur = f.IdFournisseur", conn);

                // execute the command
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        commandeFournisseur.Add(ConvertToCommandeFournisseur(rdr));
                    }
                }
                return commandeFournisseur;
            }
        }

        public Stock GetStock()
        {
            Stock stock = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand(
                            @"SELECT TOP (1000) [QuantiteRouge]
                              ,[QuantiteRose]
                              ,[QuantiteBlanc]
                              ,[QuantitePetillant]
                              ,[QuantiteDigestif]
                          FROM [NegoSud].[dbo].[Stock]", conn);

                // execute the command
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        stock = ConvertToStock(rdr);
                    }
                }
                return stock;
            }
        }





        public CommandeClient ConvertToCommandeClients(DbDataReader reader)
        {
            return new CommandeClient
            {
                IdCommandeClient = reader.GetFieldValue<int>(0),
                IdClient = reader.GetFieldValue<int>(1),
                DateCommande = reader.GetFieldValue<DateTime>(2),
                Societe = reader.GetFieldValue<string>(3),
                AdresseLivraison = reader.GetFieldValue<string>(4),
                Nom = reader.GetFieldValue<string>(5),
                Prenom = reader.GetFieldValue<string>(6),
                QuantiteRouge = reader.GetFieldValue<int>(7),
                QuantiteRose = reader.GetFieldValue<int>(8),
                QuantiteBlanc = reader.GetFieldValue<int>(9),
                QuantiteDigestif = reader.GetFieldValue<int>(10),
                QuantitePetillant = reader.GetFieldValue<int>(11)               
            };

        }

        public CommandeFournisseur ConvertToCommandeFournisseur(DbDataReader reader)
        {
            return new CommandeFournisseur
            {
                IdCommandeFournisseur = reader.GetFieldValue<int>(0),
                IdFournisseur = reader.GetFieldValue<int>(1),
                DateCommande = reader.GetFieldValue<DateTime>(2),
                SocieteFournisseur = reader.GetFieldValue<string>(3),
                QuantiteRouge = reader.GetFieldValue<int>(4),
                QuantiteRose = reader.GetFieldValue<int>(5),
                QuantiteBlanc = reader.GetFieldValue<int>(6),
                QuantitePetillant = reader.GetFieldValue<int>(7),
                QuantiteDigestif = reader.GetFieldValue<int>(8),
                
            };

        }

        public Stock ConvertToStock(DbDataReader reader)
        {
            return new Stock
            {
                QuantiteRouge = reader.GetFieldValue<int>(0),
                QuantiteRose = reader.GetFieldValue<int>(1),
                QuantiteBlanc = reader.GetFieldValue<int>(2),
                QuantitePetillant = reader.GetFieldValue<int>(3),
                QuantiteDigestif = reader.GetFieldValue<int>(4),
            };
        }






    }
}
