using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo4jClient;
using Neo4jClient.Cypher;
using System.Threading;
using TheFixer.DomainModel;

namespace TheFixer
{
    public partial class Form1 : Form
    {
        private GraphClient client;
        string ime;
        Thread th;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "pass");
            
            try
            {
                client.Connect();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void dugmeZatvoriPrijavu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void korisnickoIme_TextChanged(object sender, EventArgs e)
        {
            if (korisnickoIme.Text == "Unesite korisnicko ime")
                korisnickoIme.Text = "";
        }

        private void lozinka_TextChanged(object sender, EventArgs e)
        {
            if (lozinka.Text == "Unesite lozinku")
                lozinka.Text = "";
        }

        private void dugmePrijava_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Majstor) where n.username = '" + korisnickoIme.Text + "' AND n.password = '" + lozinka.Text + "' return n",
                                        queryDict, CypherResultMode.Set);

            List<Majstor> maj = ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();

            if (maj.Count == 0)
            {
                labGreska.Visible = true;
                return;
            }
            else
            {
                ime = maj[0].username;
                th = new Thread(otvoriNovuFormu);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
        }

        private void otvoriNovuFormu()
        {
            Application.Run(new Glavna(ime, client));
        }
    }
}
