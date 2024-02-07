using NetDeX.Games.Omi.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetDeX.App.Winforms
{
    public partial class MultiPlayer : Form
    {
        public int MyP { get; set; } = 0;
        public MultiPlayer()
        {
            InitializeComponent();
            pnlDecks.Enabled = false;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (Program.ServerWithClient.IsHosted)
                Program.ServerWithClient.NewGame();
        }

        private async void btnShare_Click(object sender, EventArgs e)
        {
            ShuffleBtn.Enabled = false;
            await Program.ServerWithClient.Share();
            ShuffleBtn.Enabled = true;
        }

        private async void ShuffleBtn_Click(object sender, EventArgs e)
        {
            ShuffleBtn.Enabled = false;
            await Program.ServerWithClient.Shuffle((int)ShuffleTimes.Value);
            ShuffleBtn.Enabled = true;
        }

        private async void btnTrump_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            ShuffleBtn.Enabled = false;
            await Program.ServerWithClient.SetTrump((Games.Enums.Types)comboBox1.SelectedItem);
            ShuffleBtn.Enabled = true;
        }

        private async void btnAddToDeck_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                btnAddToDeck.Enabled = false;
                await Program.ServerWithClient.PlaceCard((Card)listBox1.SelectedItem);
                btnAddToDeck.Enabled = true;

            }
        }

        private void MultiPlayer_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Spade);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Heart);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Diamond);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Club);

            Program.ServerWithClient.Engine.Data.PropertyChanged += EngineDataChanged;
            Program.ServerWithClient.Engine.TeamDataChanged += TeamsDataChanged;
            TeamsDataChanged(null, null);
            Program.ServerWithClient.Players.CollectionChanged += (_, _) =>
            {
                listBox2.Items.Clear();
                foreach (var item in Program.ServerWithClient.Players)
                    listBox2.Items.Add(item);
            };

            listBox2.Items.Clear();
            foreach (var item in Program.ServerWithClient.Players)
                listBox2.Items.Add(item);

            Program.ServerWithClient.Engine.Data.CurrentRound.CollectionChanged += (_, _) =>
            {
                listBox5.Items.Clear();
                foreach (var item in Program.ServerWithClient.Engine.Data.CurrentRound)
                    listBox5.Items.Add(item);
            };


            Program.ServerWithClient.Messages.CollectionChanged += (_, _) =>
            {
                listBox3.Items.Clear();
                foreach (var item in Program.ServerWithClient.Messages)
                    listBox3.Items.Add(item);
            };

            listBox5.Items.Clear();
            foreach (var item in Program.ServerWithClient.Engine.Data.CurrentRound)
                listBox5.Items.Add(item);

            Program.ServerWithClient.JoinPlayerSuccess += ServerWithClient_JoinPlayerSuccess;
        }


        private void ServerWithClient_JoinPlayerSuccess(object? sender, int e)
        {
            lblPlayer.Text = "Player " + e;
            MyP = e;

            pnlDecks.Enabled = true;
        }

        private void EngineDataChanged(object? sender, PropertyChangedEventArgs e)
        {
            TrumpT.Text = "Trump Type: " + Program.ServerWithClient.Engine.Data.Trump;
            TrumP.Text = "Who said trump: Player " + Program.ServerWithClient.Engine.Data.WhoSaidTrump;
            CurrentP.Text = "Current Player: " + Program.ServerWithClient.Engine.Data.CurrentPlayerPosition;
            CurrentType.Text = "Current Card Type: " + Program.ServerWithClient.Engine.Data.CurrentRoundType;
        }

        private void TeamsDataChanged(object? sender, TeamsData e)
        {
            Team1Wins.Text = "Wins: " + Program.ServerWithClient.Engine.Teams[0].Wins;
            Team1Loses.Text = "Loses: " + Program.ServerWithClient.Engine.Teams[0].Loses;
            Team1Draws.Text = "Draws: " + Program.ServerWithClient.Engine.Teams[0].Draws;

            Team2Wins.Text = "Wins: " + Program.ServerWithClient.Engine.Teams[1].Wins;
            Team2Loses.Text = "Loses: " + Program.ServerWithClient.Engine.Teams[1].Loses;
            Team2Draws.Text = "Draws: " + Program.ServerWithClient.Engine.Teams[1].Draws;

            Team1WinsC.Text = "Wins in current game: " + Program.ServerWithClient.Engine.Teams[0].RoundsWon.Count();
            Team2WinsC.Text = "Wins in current game: " + Program.ServerWithClient.Engine.Teams[1].RoundsWon.Count();

            Team1TradesHave.Text = "Trades have: " + Program.ServerWithClient.Engine.Teams[0].TradesHave.Count;
            Team1TradesGiven.Text = "Trades given: " + Program.ServerWithClient.Engine.Teams[0].TradesGiven.Count;

            Team2TradesHave.Text = "Trades have: " + Program.ServerWithClient.Engine.Teams[1].TradesHave.Count;
            Team2TradesGiven.Text = "Trades given: " + Program.ServerWithClient.Engine.Teams[1].TradesGiven.Count;

            foreach (var item in Program.ServerWithClient.Engine.Teams)
                foreach (var p in item.Players)
                {
                    if (p.Position == MyP)
                    {
                        lblPlayer.Text = p.Name;
                        listBox1.Items.Clear();
                        foreach (var i in p.Deck)
                            listBox1.Items.Add(i);
                    }
                }
        }

        private async void btnRequestAc_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;

            var d = ((string ipPort, int place, string name))listBox2.SelectedItem;

            if (d.ipPort != "") return;

            btnRequestAc.Enabled = false;

            await Program.ServerWithClient.RequestPlayer(d.place);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            btnSend.Enabled = false;
            await Program.ServerWithClient.Chat(textBox1.Text);
            btnSend.Enabled = true;
        }
    }
}