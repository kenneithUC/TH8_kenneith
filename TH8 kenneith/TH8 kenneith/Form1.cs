using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TH8_kenneith
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;uid=root;pwd=;database=premier_league";
        MySqlConnection sqlConnection;
        MySqlCommand mysqlcommand;
        MySqlDataAdapter MySqlDataAdapter;
        string team="";
        string player = "";
        string playerprofile = "";
        string playerprofile2 = "";
        string teammatch = "";
        string vsmatch="";
        string matchdetail = "";
        string teamhome = "";
        string teamaway = "";
        Label[] label1;
        Label[] label3; 
        Label[] label4;
        DataTable dtteam=new DataTable();
        DataTable dtplayer=new DataTable();
        DataTable dtplayerprofile=new DataTable();
        DataTable dtplayerprofile2=new DataTable();
        DataTable dtteammatch = new DataTable();
        DataTable dtvsmatch = new DataTable();
        DataTable dtmatchdetail = new DataTable();
        DataTable dtteamhome = new DataTable();
        DataTable dtteamaway = new DataTable();


        string label= "Player Name:,Team Name:,Nationality:,Position:,Yellow Card:,Red Card:,Goal Scored:,Own Goal:,Goal Penalty:,Penalty Miss:";
        private void playerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtplayer.Clear();
            dtteam.Clear();
            panel3.Visible = false;
            panel4.Visible = false;
            panel1.Visible = true;
            panel2.Visible = true;
            team = "select team_id as 'ID',team_name as'Team Name' from team";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(team,sqlConnection);
            MySqlDataAdapter=new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtteam);
            comboBox1.DataSource = dtteam;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Team Name";
            comboBox1.Text = "";
        }
        private void findMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            teammatch = "select team_id as 'ID',team_name as'Team Name' from team";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(teammatch, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtteammatch);
            comboBox3.DataSource = dtteammatch;
            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "Team Name";
            comboBox3.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = 400;
            int y = 70;
            label1 = new Label[10];
            string[] label2 = label.Split(',');
            for (int i = 0; i < 10; i++)
            {
                label1[i] = new Label();
                label1[i].Text = label2[i];
                label1[i].Location = new Point(x, y);
                label1[i].Size = new Size(200, 30);
                label1[i].Font = new Font("Arial", 20, FontStyle.Bold);
                this.panel1.Controls.Add(label1[i]);
                y += 60;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtplayer.Clear();
            comboBox2.Text = "";
            player = "select p.player_name as 'name',p.player_id as 'IDplayer' from player p,team t where p.team_id=t.team_id and t.team_id=" + $"'{comboBox1.SelectedValue.ToString()}';";
            mysqlcommand = new MySqlCommand(player, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtplayer);
            comboBox2.DataSource = dtplayer;
            comboBox2.ValueMember = "IDplayer";
            comboBox2.DisplayMember = "name";
            comboBox2.Text = "";
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label3 = new Label[10];
            label4 = new Label[10];
            this.panel2.Controls.Clear();
            dtplayerprofile2.Clear();
            dtplayerprofile.Clear();
            playerprofile = "select p.player_name as 'player name',t.team_name as 'team name',n.nation as 'nationality',p.team_number as 'pemain number' from player p ,team t,nationality n where t.team_id=p.team_id and p.nationality_id=n.nationality_id and p.player_id =" + $"'{comboBox2.SelectedValue.ToString()}';";
            mysqlcommand = new MySqlCommand(playerprofile, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtplayerprofile);
            playerprofile2 = "select COALESCE(sum(if(d.type='CY',1,0)),0) as 'Yellow Card',COALESCE(sum(if(d.type='CR',1,0)),0) as'Red Card',COALESCE(sum(if(d.type='GO',1,0)),0) as 'goal',COALESCE(sum(if(d.type='GW',1,0)),0) as 'Own Goal' ,COALESCE(sum(if(d.type='GP',1,0)),0) as 'Goal Penalty',COALESCE(sum(if(d.type='PM',1,0)),0) as 'pm'from dmatch d,player p where p.player_id = d.player_id and p.player_id=" + $"'{comboBox2.SelectedValue.ToString()}';";
            mysqlcommand = new MySqlCommand(playerprofile2, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtplayerprofile2);
            int x = 80;
            int y = 70;
            for (int i = 0; i < 4; i++)
            {
                label3[i] = new Label();
                label3[i].Text = dtplayerprofile.Rows[0][i].ToString();
                label3[i].Location = new Point(x, y);
                label3[i].Size = new Size(200, 30);
                label3[i].Font = new Font("Arial", 16, FontStyle.Bold);
                this.panel2.Controls.Add(label3[i]);
                y += 60;
            }
            int t = 80;
            int p = 310;
            for(int u = 0; u < 6; u++)
            {
                label4[u] = new Label();
                label4[u].Text = dtplayerprofile2.Rows[0][u].ToString();
                label4[u].Location = new Point(t, p);
                label4[u].Size = new Size(200, 30);
                label4[u].Font = new Font("Arial", 16, FontStyle.Bold);
                this.panel2.Controls.Add(label4[u]);
                p += 60;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtvsmatch.Clear();
            vsmatch = "select t1.team_id as 'teamid', m.match_id as'matchid',m.match_date as'match date',t1.team_name as'home team',t2.team_name as'away team' from `match`m,dmatch d,team t1,team t2 where m.team_home=t1.team_id and m.team_away=t2.team_id and m.match_id=d.match_id and (t1.team_id="+$"'{comboBox3.SelectedValue.ToString()}'or t2.team_id="+$"'{comboBox3.SelectedValue.ToString()}') group by m.match_id;";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(vsmatch, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtvsmatch);
            comboBox4.DataSource = dtvsmatch;
            comboBox4.ValueMember ="matchid";
            comboBox4.DisplayMember = "matchid";

        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtteamaway.Clear();
            dtteamhome.Clear();
            dtmatchdetail.Clear();
            matchdetail = "select d.`minute` as'minute',t.team_name as'team name',p.player_name as 'playername',if(d.type='CY','Yellow Card',if(d.type='CR','Red Card',if(d.type='GO','Goal',if(d.type='GW','OwnGoal',if(d.type='Gp','Goal Penalty','Penalty Miss')))))from dmatch d,team t,player p where d.team_id=t.team_id and d.player_id=p.player_id and d.match_id=" + $"'{comboBox4.SelectedValue.ToString()}';";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(matchdetail, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtmatchdetail);
            dataGridView1.DataSource = dtmatchdetail;
            teamhome = "select t.team_name as'home team',p.player_name as 'home player',p.playing_pos as'position'from `match`m,team t,player p where p.team_id=t.team_id and t.team_id=m.team_home and m.match_id=" + $"'{comboBox4.SelectedValue.ToString()}';";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(teamhome, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtteamhome);
            dataGridView2.DataSource = dtteamhome;
            teamaway = "select t.team_name as'away team',p.player_name as 'away player',p.playing_pos as'position'from `match`m,team t,player p where p.team_id=t.team_id and t.team_id=m.team_away and m.match_id=" + $"'{comboBox4.SelectedValue.ToString()}';";
            sqlConnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(teamaway, sqlConnection);
            MySqlDataAdapter = new MySqlDataAdapter(mysqlcommand);
            MySqlDataAdapter.Fill(dtteamaway);
            dataGridView3.DataSource = dtteamaway;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
