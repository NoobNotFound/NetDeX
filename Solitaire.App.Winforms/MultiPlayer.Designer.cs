namespace Solitaire.App.Winforms
{
    partial class MultiPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Team2WinsC = new Label();
            Team2TradesGiven = new Label();
            label13 = new Label();
            Team2TradesHave = new Label();
            Team2Loses = new Label();
            Team2Draws = new Label();
            Team2Wins = new Label();
            comboBox1 = new ComboBox();
            btnTrump = new Button();
            listBox1 = new ListBox();
            btnAddToDeck = new Button();
            pnlShare = new Panel();
            btnShare = new Button();
            ShuffleBtn = new Button();
            ShuffleTimes = new NumericUpDown();
            pnlDecks = new Panel();
            lblPlayer = new Label();
            pnlTrump = new Panel();
            btnRequestAc = new Button();
            listBox2 = new ListBox();
            CurrentType = new Label();
            CurrentP = new Label();
            groupBox2 = new GroupBox();
            btnNewGame = new Button();
            groupBox1 = new GroupBox();
            Team1WinsC = new Label();
            Team1TradesGiven = new Label();
            label10 = new Label();
            Team1TradesHave = new Label();
            Team1Loses = new Label();
            Team1Draws = new Label();
            Team1Wins = new Label();
            TrumP = new Label();
            listBox5 = new ListBox();
            gameBindingSource = new BindingSource(components);
            TrumpT = new Label();
            pnlShare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ShuffleTimes).BeginInit();
            pnlDecks.SuspendLayout();
            pnlTrump.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gameBindingSource).BeginInit();
            SuspendLayout();
            // 
            // Team2WinsC
            // 
            Team2WinsC.AutoSize = true;
            Team2WinsC.Location = new Point(6, 112);
            Team2WinsC.Name = "Team2WinsC";
            Team2WinsC.Size = new Size(123, 15);
            Team2WinsC.TabIndex = 7;
            Team2WinsC.Text = "Wins in current game:";
            // 
            // Team2TradesGiven
            // 
            Team2TradesGiven.AutoSize = true;
            Team2TradesGiven.Location = new Point(6, 85);
            Team2TradesGiven.Name = "Team2TradesGiven";
            Team2TradesGiven.Size = new Size(73, 15);
            Team2TradesGiven.TabIndex = 5;
            Team2TradesGiven.Text = "Trades Given";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 19);
            label13.Name = "label13";
            label13.Size = new Size(65, 15);
            label13.TabIndex = 4;
            label13.Text = "Players: 2,4";
            // 
            // Team2TradesHave
            // 
            Team2TradesHave.AutoSize = true;
            Team2TradesHave.Location = new Point(6, 70);
            Team2TradesHave.Name = "Team2TradesHave";
            Team2TradesHave.Size = new Size(70, 15);
            Team2TradesHave.TabIndex = 3;
            Team2TradesHave.Text = "Trades Have";
            // 
            // Team2Loses
            // 
            Team2Loses.AutoSize = true;
            Team2Loses.Location = new Point(85, 44);
            Team2Loses.Name = "Team2Loses";
            Team2Loses.Size = new Size(36, 15);
            Team2Loses.TabIndex = 2;
            Team2Loses.Text = "Loses";
            // 
            // Team2Draws
            // 
            Team2Draws.AutoSize = true;
            Team2Draws.Location = new Point(152, 44);
            Team2Draws.Name = "Team2Draws";
            Team2Draws.Size = new Size(39, 15);
            Team2Draws.TabIndex = 1;
            Team2Draws.Text = "Draws";
            // 
            // Team2Wins
            // 
            Team2Wins.AutoSize = true;
            Team2Wins.Location = new Point(6, 44);
            Team2Wins.Name = "Team2Wins";
            Team2Wins.Size = new Size(33, 15);
            Team2Wins.TabIndex = 0;
            Team2Wins.Text = "Wins";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 22;
            // 
            // btnTrump
            // 
            btnTrump.Location = new Point(0, 31);
            btnTrump.Name = "btnTrump";
            btnTrump.Size = new Size(121, 27);
            btnTrump.TabIndex = 23;
            btnTrump.Text = "Set Trump";
            btnTrump.UseVisualStyleBackColor = true;
            btnTrump.Click += btnTrump_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(10, 74);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(156, 169);
            listBox1.TabIndex = 4;
            // 
            // btnAddToDeck
            // 
            btnAddToDeck.Location = new Point(176, 128);
            btnAddToDeck.Name = "btnAddToDeck";
            btnAddToDeck.Size = new Size(153, 60);
            btnAddToDeck.TabIndex = 24;
            btnAddToDeck.Text = "Add to deck";
            btnAddToDeck.UseVisualStyleBackColor = true;
            btnAddToDeck.Click += btnAddToDeck_Click;
            // 
            // pnlShare
            // 
            pnlShare.Controls.Add(btnShare);
            pnlShare.Controls.Add(ShuffleBtn);
            pnlShare.Controls.Add(ShuffleTimes);
            pnlShare.Location = new Point(11, 10);
            pnlShare.Name = "pnlShare";
            pnlShare.Size = new Size(158, 61);
            pnlShare.TabIndex = 39;
            // 
            // btnShare
            // 
            btnShare.Location = new Point(3, 31);
            btnShare.Name = "btnShare";
            btnShare.Size = new Size(152, 27);
            btnShare.TabIndex = 3;
            btnShare.Text = "Share";
            btnShare.UseVisualStyleBackColor = true;
            btnShare.Click += btnShare_Click;
            // 
            // ShuffleBtn
            // 
            ShuffleBtn.Location = new Point(3, 3);
            ShuffleBtn.Name = "ShuffleBtn";
            ShuffleBtn.Size = new Size(75, 27);
            ShuffleBtn.TabIndex = 0;
            ShuffleBtn.Text = "Shuffle";
            ShuffleBtn.UseVisualStyleBackColor = true;
            ShuffleBtn.Click += ShuffleBtn_Click;
            // 
            // ShuffleTimes
            // 
            ShuffleTimes.Location = new Point(84, 5);
            ShuffleTimes.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            ShuffleTimes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ShuffleTimes.Name = "ShuffleTimes";
            ShuffleTimes.Size = new Size(71, 23);
            ShuffleTimes.TabIndex = 1;
            ShuffleTimes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pnlDecks
            // 
            pnlDecks.Controls.Add(lblPlayer);
            pnlDecks.Controls.Add(listBox1);
            pnlDecks.Controls.Add(btnAddToDeck);
            pnlDecks.Controls.Add(pnlShare);
            pnlDecks.Controls.Add(pnlTrump);
            pnlDecks.Location = new Point(12, 12);
            pnlDecks.Name = "pnlDecks";
            pnlDecks.Size = new Size(339, 256);
            pnlDecks.TabIndex = 37;
            // 
            // lblPlayer
            // 
            lblPlayer.AutoSize = true;
            lblPlayer.Location = new Point(199, 85);
            lblPlayer.Name = "lblPlayer";
            lblPlayer.Size = new Size(79, 15);
            lblPlayer.TabIndex = 40;
            lblPlayer.Text = "Players Name";
            // 
            // pnlTrump
            // 
            pnlTrump.Controls.Add(comboBox1);
            pnlTrump.Controls.Add(btnTrump);
            pnlTrump.Location = new Point(172, 10);
            pnlTrump.Name = "pnlTrump";
            pnlTrump.Size = new Size(125, 61);
            pnlTrump.TabIndex = 38;
            // 
            // btnRequestAc
            // 
            btnRequestAc.Location = new Point(541, 140);
            btnRequestAc.Name = "btnRequestAc";
            btnRequestAc.Size = new Size(156, 60);
            btnRequestAc.TabIndex = 26;
            btnRequestAc.Text = "Request access";
            btnRequestAc.UseVisualStyleBackColor = true;
            btnRequestAc.Click += btnRequestAc_Click;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(369, 86);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(156, 169);
            listBox2.TabIndex = 25;
            // 
            // CurrentType
            // 
            CurrentType.AutoSize = true;
            CurrentType.Location = new Point(399, 30);
            CurrentType.Name = "CurrentType";
            CurrentType.Size = new Size(163, 15);
            CurrentType.TabIndex = 36;
            CurrentType.Text = "Current Card Type: Undefined";
            // 
            // CurrentP
            // 
            CurrentP.AutoSize = true;
            CurrentP.Location = new Point(566, 229);
            CurrentP.Name = "CurrentP";
            CurrentP.Size = new Size(94, 15);
            CurrentP.TabIndex = 35;
            CurrentP.Text = "Current Player: 0";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Team2WinsC);
            groupBox2.Controls.Add(Team2TradesGiven);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(Team2TradesHave);
            groupBox2.Controls.Add(Team2Loses);
            groupBox2.Controls.Add(Team2Draws);
            groupBox2.Controls.Add(Team2Wins);
            groupBox2.Location = new Point(531, 288);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(223, 148);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "Team 2";
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(713, 6);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(75, 60);
            btnNewGame.TabIndex = 30;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Team1WinsC);
            groupBox1.Controls.Add(Team1TradesGiven);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(Team1TradesHave);
            groupBox1.Controls.Add(Team1Loses);
            groupBox1.Controls.Add(Team1Draws);
            groupBox1.Controls.Add(Team1Wins);
            groupBox1.Location = new Point(13, 288);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(223, 148);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Team 1";
            // 
            // Team1WinsC
            // 
            Team1WinsC.AutoSize = true;
            Team1WinsC.Location = new Point(6, 112);
            Team1WinsC.Name = "Team1WinsC";
            Team1WinsC.Size = new Size(123, 15);
            Team1WinsC.TabIndex = 6;
            Team1WinsC.Text = "Wins in current game:";
            // 
            // Team1TradesGiven
            // 
            Team1TradesGiven.AutoSize = true;
            Team1TradesGiven.Location = new Point(6, 85);
            Team1TradesGiven.Name = "Team1TradesGiven";
            Team1TradesGiven.Size = new Size(73, 15);
            Team1TradesGiven.TabIndex = 5;
            Team1TradesGiven.Text = "Trades Given";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 19);
            label10.Name = "label10";
            label10.Size = new Size(65, 15);
            label10.TabIndex = 4;
            label10.Text = "Players: 1,3";
            // 
            // Team1TradesHave
            // 
            Team1TradesHave.AutoSize = true;
            Team1TradesHave.Location = new Point(6, 70);
            Team1TradesHave.Name = "Team1TradesHave";
            Team1TradesHave.Size = new Size(70, 15);
            Team1TradesHave.TabIndex = 3;
            Team1TradesHave.Text = "Trades Have";
            // 
            // Team1Loses
            // 
            Team1Loses.AutoSize = true;
            Team1Loses.Location = new Point(85, 44);
            Team1Loses.Name = "Team1Loses";
            Team1Loses.Size = new Size(36, 15);
            Team1Loses.TabIndex = 2;
            Team1Loses.Text = "Loses";
            // 
            // Team1Draws
            // 
            Team1Draws.AutoSize = true;
            Team1Draws.Location = new Point(152, 44);
            Team1Draws.Name = "Team1Draws";
            Team1Draws.Size = new Size(39, 15);
            Team1Draws.TabIndex = 1;
            Team1Draws.Text = "Draws";
            // 
            // Team1Wins
            // 
            Team1Wins.AutoSize = true;
            Team1Wins.Location = new Point(6, 44);
            Team1Wins.Name = "Team1Wins";
            Team1Wins.Size = new Size(33, 15);
            Team1Wins.TabIndex = 0;
            Team1Wins.Text = "Wins";
            // 
            // TrumP
            // 
            TrumP.AutoSize = true;
            TrumP.Location = new Point(567, 253);
            TrumP.Name = "TrumP";
            TrumP.Size = new Size(93, 15);
            TrumP.TabIndex = 32;
            TrumP.Text = "Who said Trump";
            // 
            // listBox5
            // 
            listBox5.FormattingEnabled = true;
            listBox5.ItemHeight = 15;
            listBox5.Location = new Point(256, 274);
            listBox5.Name = "listBox5";
            listBox5.Size = new Size(242, 169);
            listBox5.TabIndex = 31;
            // 
            // gameBindingSource
            // 
            gameBindingSource.DataSource = typeof(Games.Omi.Core.Game);
            // 
            // TrumpT
            // 
            TrumpT.AutoSize = true;
            TrumpT.Location = new Point(399, 53);
            TrumpT.Name = "TrumpT";
            TrumpT.Size = new Size(129, 15);
            TrumpT.TabIndex = 38;
            TrumpT.Text = "Trump Type: Undefined";
            // 
            // MultiPlayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 462);
            Controls.Add(TrumpT);
            Controls.Add(btnRequestAc);
            Controls.Add(listBox2);
            Controls.Add(pnlDecks);
            Controls.Add(CurrentType);
            Controls.Add(CurrentP);
            Controls.Add(groupBox2);
            Controls.Add(btnNewGame);
            Controls.Add(groupBox1);
            Controls.Add(TrumP);
            Controls.Add(listBox5);
            Name = "MultiPlayer";
            Text = "MultiPlayer";
            Load += MultiPlayer_Load;
            pnlShare.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ShuffleTimes).EndInit();
            pnlDecks.ResumeLayout(false);
            pnlDecks.PerformLayout();
            pnlTrump.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gameBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Team2WinsC;
        private Label Team2TradesGiven;
        private Label label13;
        private Label Team2TradesHave;
        private Label Team2Loses;
        private Label Team2Draws;
        private Label Team2Wins;
        private ComboBox comboBox1;
        private Button btnTrump;
        private ListBox listBox1;
        private Button btnAddToDeck;
        private Panel pnlShare;
        private Button btnShare;
        private Button ShuffleBtn;
        private NumericUpDown ShuffleTimes;
        private Panel pnlDecks;
        private Label CurrentType;
        private Label CurrentP;
        private GroupBox groupBox2;
        private Button btnNewGame;
        private GroupBox groupBox1;
        private Label Team1WinsC;
        private Label Team1TradesGiven;
        private Label label10;
        private Label Team1TradesHave;
        private Label Team1Loses;
        private Label Team1Draws;
        private Label Team1Wins;
        private Label TrumP;
        private Panel pnlTrump;
        private ListBox listBox5;
        private Button btnRequestAc;
        private ListBox listBox2;
        private BindingSource gameBindingSource;
        private Label lblPlayer;
        private Label TrumpT;
    }
}