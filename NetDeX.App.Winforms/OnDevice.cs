using NetDeX.Games.Omi.Core;
using NetDeX.Games.Omi.Core.Helpers;
using System.Diagnostics;

namespace NetDeX.App.Winforms
{
    public partial class Ondevice : Form
    {
        public Engine OmiEngine { get; set; }
        public Ondevice()
        {
            InitializeComponent();
            Extentions.WriteToConsole = true;
            OmiEngine = new Engine(Games.Omi.Enums.Players.Four);
            OmiEngine.Initialize();
        }

        private void ShuffleBtn_Click(object sender, EventArgs e)
        {
            OmiEngine.Shuffle((int)ShuffleTimes.Value);
        }

        int clicks = 0;
        private void btnShare_Click(object sender, EventArgs e)
        {
            OmiEngine.Share();
            clicks++;
            if (clicks == 2)
            {
                clicks = 0;
                pnlShare.Enabled = false;
                pnlTrump.Enabled = false;
                pnlDecks.Enabled = true;
            }
            else
            {
                pnlShare.Enabled = false;
                pnlTrump.Enabled = false;
                pnlTrump.Enabled = true;
            }
            CurrentP.Text = "Current Player: " + OmiEngine.Data.CurrentPlayerPosition;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlShare.Enabled = false;
            pnlTrump.Enabled = false;
            pnlDecks.Enabled = false;

            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Spade);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Heart);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Diamond);
            comboBox1.Items.Add(NetDeX.Games.Enums.Types.Club);

            comboBox1.SelectedIndex = 0;

            foreach (var item in OmiEngine.Teams)
            {
                foreach (var p in item.Players)
                {
                    p.DeckChanged += P_DeckChanged;
                }
            }
            OmiEngine.Data.CurrentRound.CollectionChanged += (_, _) =>
            {
                listBox5.Items.Clear();
                foreach (var item in OmiEngine.Data.CurrentRound)
                {
                    listBox5.Items.Add(item);
                }
            };
            OmiEngine.GameEnded += OmiEngine_GameEnded;
            OmiEngine.MainGameEnded += OmiEngine_MainGameEnded;
            OmiEngine_GameEnded(null, (false, Guid.Empty));
            shareTrades = true;
        }

        private void OmiEngine_MainGameEnded(object? sender, Guid e)
        {
            MessageBox.Show(OmiEngine.Teams.FirstOrDefault(x => x.ID == e).Name + " won the whole game");
            this.Close();
            Application.Exit();
        }

        bool shareTrades;
        private void OmiEngine_GameEnded(object? sender, (bool isDraw, Guid TeamWon) e)
        {
            Team1Wins.Text = "Wins: " + OmiEngine.Teams[0].Wins;
            Team1Loses.Text = "Loses: " + OmiEngine.Teams[0].Loses;
            Team1Draws.Text = "Draws: " + OmiEngine.Teams[0].Draws;

            Team2Wins.Text = "Wins: " + OmiEngine.Teams[1].Wins;
            Team2Loses.Text = "Loses: " + OmiEngine.Teams[1].Loses;
            Team2Draws.Text = "Draws: " + OmiEngine.Teams[1].Draws;

            if(shareTrades)
            OmiEngine.ShareTrades();

            Team1TradesHave.Text = "Trades have: " + OmiEngine.Teams[0].TradesHave.Count;
            Team1TradesGiven.Text = "Trades given: " + OmiEngine.Teams[0].TradesGiven.Count;

            Team2TradesHave.Text = "Trades have: " + OmiEngine.Teams[1].TradesHave.Count;
            Team2TradesGiven.Text = "Trades given: " + OmiEngine.Teams[1].TradesGiven.Count;
        }

        private void P_DeckChanged(object? sender, (Card[] deck, int player) e)
        {
            if (e.player == 1)
            {
                listBox1.Items.Clear();
                foreach (var item in e.deck)
                {
                    listBox1.Items.Add(item);
                }
            }
            else if (e.player == 2)
            {
                listBox2.Items.Clear();
                foreach (var item in e.deck)
                {
                    listBox2.Items.Add(item);
                }
            }
            else if (e.player == 3)
            {
                listBox3.Items.Clear();
                foreach (var item in e.deck)
                {
                    listBox3.Items.Add(item);
                }
            }
            else if (e.player == 4)
            {
                listBox4.Items.Clear();
                foreach (var item in e.deck)
                {
                    listBox4.Items.Add(item);
                }
            }
        }

        private void btnTrump_Click(object sender, EventArgs e)
        {
            OmiEngine.Data.Trump = (Games.Enums.Types)comboBox1.SelectedItem;
            TrumP.Text = "Who said trump: Player " + OmiEngine.Data.WhoSaidTrump;

            pnlTrump.Enabled = false;
            pnlShare.Enabled = true;
        }

        private void btnAddToDeck_Click(object sender, EventArgs e)
        {

            switch (OmiEngine.Data.CurrentPlayerPosition)
            {
                case 1:
                    if(listBox1.SelectedItem != null)
                    OmiEngine.PlaceCard((Card)listBox1.SelectedItem, OmiEngine.Data.CurrentPlayerPosition);
                    break;
                case 2:
                    if (listBox2.SelectedItem != null)
                        OmiEngine.PlaceCard((Card)listBox2.SelectedItem, OmiEngine.Data.CurrentPlayerPosition);
                    break;
                case 3:
                    if (listBox3.SelectedItem != null)
                        OmiEngine.PlaceCard((Card)listBox3.SelectedItem, OmiEngine.Data.CurrentPlayerPosition);
                    break;
                default:
                    if (listBox4.SelectedItem != null)
                        OmiEngine.PlaceCard((Card)listBox4.SelectedItem, OmiEngine.Data.CurrentPlayerPosition);
                    break;
            }
            CurrentP.Text = "Current Player: " + OmiEngine.Data.CurrentPlayerPosition;
            Team1WinsC.Text = "Wins in current game: " + OmiEngine.Teams[0].RoundsWon.Count();
            Team2WinsC.Text = "Wins in current game: " + OmiEngine.Teams[1].RoundsWon.Count();

            CurrentType.Text = "Current Card Type: " + OmiEngine.Data.CurrentRoundType;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            OmiEngine.NewGame();
            TrumP.Text = "Who said trump: Player " + OmiEngine.Data.WhoSaidTrump;
            CurrentP.Text = "Current Player: " + OmiEngine.Data.CurrentPlayerPosition;

            pnlShare.Enabled = true;
            pnlTrump.Enabled = false;
            pnlDecks.Enabled = false;
            clicks = 0;

        }
    }
}