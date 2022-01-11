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
using TheFixer.DomainModel;
using System.Threading;

namespace TheFixer
{
    public partial class Glavna : Form
    {
        public GraphClient client;

        public Glavna(string ime, GraphClient cl)
        {
            InitializeComponent();
            client = cl;
            labUlogovan.Text = ime;
        }

        private void dugmeZatvoriGlavnu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dugmeOdjaviSe_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(formaZaPrijavu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }

        private void formaZaPrijavu()
        {
            Application.Run(new Form1());
        }

        private void dugmeDodajMajstora_Click(object sender, EventArgs e)
        {
            if (imeMajstora.Text == "" || prezimeMajstora.Text == "" || usernameMajstora.Text == "" || passwordMajstora.Text == "")
            {
                MessageBox.Show("Unesite lepo podatke o majstoru.");
                return;
            }
            Majstor m = new Majstor();
            m.ime = imeMajstora.Text;
            m.prezime = prezimeMajstora.Text;
            m.telefon = telefonMajstora.Text;
            m.mail = mailMajstora.Text;
            m.username = usernameMajstora.Text;
            m.password = passwordMajstora.Text;
            m.adresa = adresaMajstora.Text;
            m.popravke = new List<Popravka>();
            m.idMajstora = Int32.Parse(nadjiMaxIdMajstora()) + 1;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Majstor {idMajstora:'" + m.idMajstora + "', ime:'" + m.ime + "', prezime:'" + m.prezime
                                                    + "', username: '" + m.username + "', password: '" +m.password  + "', adresa: '" + m.adresa + 
                                                    "', telefon:'" + m.telefon + "', mail:'" + m.mail
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();
            MessageBox.Show("Uspesno ste dodali novog majstora. Id: " + m.idMajstora.ToString());
        }

        private string nadjiMaxIdMajstora()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Majstor) and exists(n.idMajstora) return max(n.idMajstora)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void dugmeDodajVlasnika_Click(object sender, EventArgs e)
        {
            if (imeVlasnika.Text == "" || prezimeVlasnika.Text == "")
            {
                MessageBox.Show("Unesite lepo podatke o vlasniku.");
                return;
            }
            Vlasnik v = new Vlasnik();
            v.ime = imeVlasnika.Text;
            v.prezime = prezimeVlasnika.Text;
            v.telefon = telefonVlasnika.Text;
            v.mail = mailVlasnika.Text;
            v.uredjaji = new List<Uredjaj>();
            v.idVlasnika = Int32.Parse(nadjiMaxIdVlasnik()) + 1;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Vlasnik {idVlasnika:'" + v.idVlasnika + "', ime:'" + v.ime + "', prezime:'" + v.prezime
                                                    + "', telefon:'" + v.telefon + "', mail:'" + v.mail
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();
            MessageBox.Show("Uspesno ste dodali novog vlasnika. Id: " + v.idVlasnika.ToString());
        }

        private string nadjiMaxIdVlasnik()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Vlasnik) and exists(n.idVlasnika) return max(n.idVlasnika)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void dugmeDodajUredjaj_Click(object sender, EventArgs e)
        {
            int idVlas;
            if (nazivUredjaja.Text == "" || vrstaUredjaja.Text == "" || !Int32.TryParse(uredjajIdVlasnika.Text, out idVlas))
            {
                MessageBox.Show("Unesite lepo podatke o uredjaju.");
                return;
            }
            Dictionary<string, object> queryDict2 = new Dictionary<string, object>();
            queryDict2.Add("idVlasnika", uredjajIdVlasnika.Text);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Vlasnik) where n.idVlasnika = '" + uredjajIdVlasnika.Text + "' return n",
                                        queryDict2, CypherResultMode.Set);

            List<Vlasnik> vla = ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();

            if (vla.Count == 0)
            {
                MessageBox.Show("Vlasnik sa zadatim ID ne postoji.");
                return;
            }

            Uredjaj u = new Uredjaj();
            u.vrsta = vrstaUredjaja.Text;
            u.naziv = nazivUredjaja.Text;
            u.opis = opisUredjaja.Text;
            u.popravke = new List<Popravka>();
            u.idUredjaja = Int32.Parse(nadjiMaxIdUredjaja()) + 1;
            u.vlasnik = vla[0];
            if (vla[0].uredjaji == null)
                vla[0].uredjaji = new List<Uredjaj>();
            vla[0].uredjaji.Add(u);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Uredjaj {idUredjaja:'" + u.idUredjaja + "', vrsta:'" + u.vrsta + "', naziv:'" + u.naziv
                                                    + "', opis:'" + u.opis
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Uredjaj>(query).ToList();

            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();

            string s = "MATCH (a:Uredjaj),(b:Vlasnik) WHERE a.idUredjaja = '" + u.idUredjaja + "' AND b.idVlasnika = '" + uredjajIdVlasnika.Text +
                                                       "' CREATE (a) -[r:PRIPADA]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict1, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Uredjaj>(query);

            MessageBox.Show("Uspesno ste dodali novi uredjaj. Id: " + u.idUredjaja.ToString());
        }

        private string nadjiMaxIdUredjaja()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Uredjaj) and exists(n.idUredjaja) return max(n.idUredjaja)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void dugmeNadjiMajstora_Click(object sender, EventArgs e)
        {
            if (nadjiMajstora.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + nadjiMajstora.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("majstor", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Majstor) and (exists(n.ime) and n.ime =~ {majstor}) " +
                "OR (exists(n.prezime) and n.prezime =~ {majstor}) OR (exists(n.idMajstora) and n.idMajstora =~ {majstor}) OR (exists(n.username) and n.username =~ {majstor}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Majstor> maj = ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();
            if (maj.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan majstor.");
                return;
            }
            string odgovor = "";
            foreach (Majstor m in maj)
            {
                odgovor += "Id: " + m.idMajstora + "\n\n" + "Ime: " + m.ime + "  Prezime: " + m.prezime + "\n\n" + "Tel: " + m.telefon + "\n\n" + "Mail: " + m.mail + "\n\n" +
                    "Username: " + m.username + "\n\n" +  "Adresa: " + m.adresa + "\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeNadjiVlasnika_Click(object sender, EventArgs e)
        {
            if (nadjiVlasnika.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + nadjiVlasnika.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("vlasnik", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Vlasnik) and (exists(n.ime) and n.ime =~ {vlasnik}) " +
                "OR (exists(n.prezime) and n.prezime =~ {vlasnik}) OR (exists(n.idVlasnika) and n.idVlasnika =~ {vlasnik}) OR (exists(n.mail) and n.mail =~ {vlasnik}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Vlasnik> vlasnici = ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();
            if (vlasnici.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan vlasnik.");
                return;
            }
            string odgovor = "";
            foreach (Vlasnik v in vlasnici)
            {
                odgovor += "Id: " + v.idVlasnika + "\n\n" + "Ime: " + v.ime +  "  Prezime: " + v.prezime + "\n\n" + "Tel: " + v.telefon + "\n\n" + "Mail: " + v.mail + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeNadjiUredjaj_Click(object sender, EventArgs e)
        {
            if (nadjiUredjaj.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + nadjiUredjaj.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("uredjaj", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Uredjaj) and (exists(n.naziv) and n.naziv =~ {uredjaj}) " +
                "OR (exists(n.vrsta) and n.vrsta =~ {uredjaj}) OR (exists(n.idUredjaja) and n.idUredjaja =~ {uredjaj}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Uredjaj> ure = ((IRawGraphClient)client).ExecuteGetCypherResults<Uredjaj>(query).ToList();
            if (ure.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan uredjaj.");
                return;
            }
            string odgovor = "";
            foreach (Uredjaj u in ure)
            {
                odgovor += "Id: " + u.idUredjaja + "\n\n" + "Vrsta: " + u.vrsta + "\n\n" + "Naziv: " + u.naziv + "\n\n" + "Opis: " + u.opis + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeSacuvajPopravku_Click(object sender, EventArgs e)
        {
            int idMajstora, idUredjaja;
            double cena;
            if (opisPopravke.Text == "" || !Double.TryParse(cenaPopravke.Text, out cena) || !Int32.TryParse(popravkaIdUredjaja.Text, out idUredjaja) || !Int32.TryParse(popravkaIdMajstora.Text, out idMajstora))
            {
                MessageBox.Show("Unesite lepo podatke o popravci.");
                return;
            }
            Dictionary<string, object> queryDict10 = new Dictionary<string, object>();
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Majstor) where n.idMajstora = '" + popravkaIdMajstora.Text + "' return n",
                                        queryDict10, CypherResultMode.Set);

            List<Majstor> maj = ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();

            if (maj.Count == 0)
            {
                MessageBox.Show("Majstor sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Uredjaj) where n.idUredjaja = '" + popravkaIdUredjaja.Text + "' return n",
                            queryDict10, CypherResultMode.Set);

            List<Uredjaj> ure = ((IRawGraphClient)client).ExecuteGetCypherResults<Uredjaj>(query).ToList();
            
            if (ure.Count == 0)
            {
                MessageBox.Show("Uredjaj sa zadatim ID ne postoji.");
                return;
            }

            Popravka p = new Popravka();
            p.cenaPopravke= cena;
            p.opis = opisPopravke.Text;
            p.datum = datumPopravke.Value.ToString();
            p.idPopravke = Int32.Parse(nadjiMaxIdPopravke()) + 1;

            if (maj[0].popravke == null)
                maj[0].popravke = new List<Popravka>();
            maj[0].popravke.Add(p);

            if (ure[0].popravke == null)
                ure[0].popravke = new List<Popravka>();
            ure[0].popravke.Add(p);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            string s = "CREATE (n:Popravka {idPopravke:'" + p.idPopravke + "', opis:'" + p.opis 
                                                     + "', datum: '" + p.datum + "', cenaPopravke: '" + p.cenaPopravke
                                                    + "'}) return n";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query).ToList();

            s = "MATCH (a:Popravka),(b:Uredjaj) WHERE a.idPopravke = '" + p.idPopravke + "' AND b.idUredjaja = '" + idUredjaja +
                                                       "' CREATE (a) -[r:IZVRSENA_NAD]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query);

            s = "MATCH (a:Popravka),(b:Majstor) WHERE a.idPopravke = '" + p.idPopravke + "' AND b.idMajstora = '" + idMajstora +
                                           "' CREATE (a) -[r:IZVRSENA_OD_STRANE]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query);

            MessageBox.Show("Uspesno ste dodali novu popravku. Id: " + p.idPopravke.ToString());
        }

        private string nadjiMaxIdPopravke()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Popravka) and exists(n.idPopravke) return max(n.idPopravke)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void dugmeSvePopravkeMajstora_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(svePopravkeMajstor.Text, out id) || id < 1)
            {
                MessageBox.Show("Id majstora mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("p", svePopravkeMajstor.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:IZVRSENA_OD_STRANE]->(m) " +
                "where exists(m.idMajstora) and m.idMajstora = {p} return n ORDER BY n.idPopravke",
                                                            queryDict, CypherResultMode.Set);

            List<Popravka> popravke = ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query).ToList();
            if (popravke.Count == 0)
            {
                MessageBox.Show("Nije pronadjena nijedna popravka za zeljenog majstora.");
                return;
            }
            double uk = 0;
            string odgovor = "";
            foreach (Popravka p in popravke)
            {
                odgovor += "Id: " + p.idPopravke + "\n\n" + "Datum: " + p.datum + "\n\n" + "Cena: " + p.cenaPopravke + "\n";
                uk += p.cenaPopravke;
            }
            odgovor += "\nUkupno je izvrseno: " + popravke.Count + " popravka/ke." + "\n\n"+ "Ukupni pazar: " + uk.ToString() + ".";
            MessageBox.Show(odgovor);
        }

        private void dugmeAzuriranjeLozinke_Click(object sender, EventArgs e)
        {
            if (azuriranjeIdMajstora.Text == "" || novaLozinka.Text == "")
            {
                MessageBox.Show("Lepo popunite podatke za izmenu.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("majstor", azuriranjeIdMajstora.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n:Majstor) where n.idMajstora = {majstor} return n",
                                                            queryDict, CypherResultMode.Set);

            List<Majstor> maj = ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();
            if (maj.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan majstor.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("match (n:Majstor) where n.idMajstora = {majstor} SET n.password = '" + novaLozinka.Text + "'",
                                                            queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste sacuvali izmene.\nKorisnicko ime : " + maj[0].username + "\nNova lozinka : " + novaLozinka.Text);
        }

        private void dugmeObrisiMajstora_Click(object sender, EventArgs e)
        {
            if (obrisiMajstorId.Text == "")
            {
                MessageBox.Show("Upisite id majstora koga zelite da obrisete.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("maj", obrisiMajstorId.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Majstor) and (exists(n.idMajstora) and n.idMajstora = {maj}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Majstor> ma = ((IRawGraphClient)client).ExecuteGetCypherResults<Majstor>(query).ToList();
            if (ma.Count == 0)
            {
                MessageBox.Show("Majstor sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Majstor) WHERE n.idMajstora = '" + obrisiMajstorId.Text + "' DETACH DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste obrisali zeljenog majstora. Id: " + obrisiMajstorId.Text);
        }

        private void dugmeNadjiSveZivotinje_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(nadjiUredjaje.Text, out id) || id < 1)
            {
                MessageBox.Show("Id vlasnika mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("vlasnik", nadjiUredjaje.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:PRIPADA]->(m) " +
                "where exists(m.idVlasnika) and m.idVlasnika = {vlasnik} return n",
                                                            queryDict, CypherResultMode.Set);

            List<Uredjaj> uredjaji = ((IRawGraphClient)client).ExecuteGetCypherResults<Uredjaj>(query).ToList();
            if (uredjaji.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan uredjaj.");
                return;
            }
            string odgovor = "";
            foreach (Uredjaj u in uredjaji)
            {
                odgovor += "Id: " + u.idUredjaja + "\n\n" + "Vrsta: " + u.vrsta + "\n\n" + "Naziv: " + u.naziv + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeNadjiPopravku_Click(object sender, EventArgs e)
        {
            if (nadjiPopravku.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + nadjiPopravku.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("popravka", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Popravka) and (exists(n.idPopravke) and n.idPopravke =~ {popravka}) " +
                "OR (exists(n.datum) and n.datum =~ {popravka}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Popravka> pop = ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query).ToList();
            if (pop.Count == 0)
            {
                MessageBox.Show("Nije pronadjena nijedna popravka.");
                return;
            }
            string odgovor = "";
            foreach (Popravka p in pop)
            {
                odgovor += "Id: " + p.idPopravke + "\n\n" + "Datum: " + p.datum + "\n\n" + "Cena: " + p.cenaPopravke + "\n\n" + "Opis: " + p.opis + "\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeNadjiPopravke_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(nadjiPopravke.Text, out id) || id < 1)
            {
                MessageBox.Show("Id uredjaja mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("u", nadjiPopravke.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:IZVRSENA_NAD]->(m) " +
                "where exists(m.idUredjaja) and m.idUredjaja = {u} return n ORDER BY n.idPopravke",
                                                            queryDict, CypherResultMode.Set);

            List<Popravka> popravke = ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query).ToList();
            if (popravke.Count == 0)
            {
                MessageBox.Show("Nije pronadjena nijedna popravka za zeljeni uredjaj.");
                return;
            }
            string odgovor = "";
            foreach (Popravka p in popravke)
            {
                odgovor += "Id: " + p.idPopravke + "\n\n" + "Datum: " + p.datum + "\n\n" + "Cena: " + p.cenaPopravke + "\n\n" + "Opis: " + p.opis + "\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void dugmeObrisiPopravku_Click(object sender, EventArgs e)
        {
            if (obrisiPopravkuId.Text == "")
            {
                MessageBox.Show("Upisite id popravke koju zelite da obrisete.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("popravka", obrisiPopravkuId.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Popravka) and (exists(n.idPopravke) and n.idPopravke = {popravka}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Popravka> pop = ((IRawGraphClient)client).ExecuteGetCypherResults<Popravka>(query).ToList();
            if (pop.Count == 0)
            {
                MessageBox.Show("Popravka sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Popravka) WHERE n.idPopravke = '" + obrisiPopravkuId.Text + "' DETACH DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste obrisali zeljenu popravku. Id: " + obrisiPopravkuId.Text);
        }

        private void dugmeObrisiSve_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Da li ste sigurni da zelite da obrisete sve podatke iz baze?",
                         "Brisanje svih podataka",
                         MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();

                var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n) DETACH DELETE n",
                                                                queryDict, CypherResultMode.Set);

                ((IRawGraphClient)client).ExecuteCypher(query);
                MessageBox.Show("Uspesno ste obrisali sve podatke iz baze!");
            }
            else
                return;
        }
    }
}
