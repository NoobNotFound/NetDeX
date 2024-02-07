namespace NetDeX.App.Winforms
{
    partial class Ondevice
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ShuffleBtn = new Button();
            ShuffleTimes = new NumericUpDown();
            btnNewGame = new Button();
            btnShare = new Button();
            listBox1 = new ListBox();
            TrumP = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            Team1WinsC = new Label();
            Team1TradesGiven = new Label();
            label10 = new Label();
            Team1TradesHave = new Label();
            Team1Loses = new Label();
            Team1Draws = new Label();
            Team1Wins = new Label();
            listBox5 = new ListBox();
            label3 = new Label();
            listBox2 = new ListBox();
            label4 = new Label();
            listBox3 = new ListBox();
            label5 = new Label();
            listBox4 = new ListBox();
            groupBox2 = new GroupBox();
            Team2WinsC = new Label();
            Team2TradesGiven = new Label();
            label13 = new Label();
            Team2TradesHave = new Label();
            Team2Loses = new Label();
            Team2Draws = new Label();
            Team2Wins = new Label();
            comboBox1 = new ComboBox();
            btnTrump = new Button();
            btnAddToDeck = new Button();
            CurrentP = new Label();
            CurrentType = new Label();
            pnlDecks = new Panel();
            pnlTrump = new Panel();
            pnlShare = new Panel();
            ((System.ComponentModel.ISupportInitialize)ShuffleTimes).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            pnlDecks.SuspendLayout();
            pnlTrump.SuspendLayout();
            pnlShare.SuspendLayout();
            SuspendLayout();
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
            // btnNewGame
            // 
            btnNewGame.Location = new Point(12, 12);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(75, 60);
            btnNewGame.TabIndex = 2;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
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
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(6, 22);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(174, 169);
            listBox1.TabIndex = 4;
            // 
            // TrumP
            // 
            TrumP.AutoSize = true;
            TrumP.Location = new Point(600, 24);
            TrumP.Name = "TrumP";
            TrumP.Size = new Size(93, 15);
            TrumP.TabIndex = 9;
            TrumP.Text = "Who said Trump";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 4);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 10;
            label2.Text = "Player 1";
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
            groupBox1.Location = new Point(30, 294);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(223, 148);
            groupBox1.TabIndex = 14;
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
            // listBox5
            // 
            listBox5.FormattingEnabled = true;
            listBox5.ItemHeight = 15;
            listBox5.Location = new Point(273, 280);
            listBox5.Name = "listBox5";
            listBox5.Size = new Size(242, 169);
            listBox5.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(186, 2);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 16;
            label3.Text = "Player 2";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(186, 20);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(174, 169);
            listBox2.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(366, 2);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 18;
            label4.Text = "Player 3";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(366, 20);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(174, 169);
            listBox3.TabIndex = 17;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(546, 2);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 20;
            label5.Text = "Player 4";
            // 
            // listBox4
            // 
            listBox4.FormattingEnabled = true;
            listBox4.ItemHeight = 15;
            listBox4.Location = new Point(546, 20);
            listBox4.Name = "listBox4";
            listBox4.Size = new Size(174, 169);
            listBox4.TabIndex = 19;
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
            groupBox2.Location = new Point(548, 294);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(223, 148);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Team 2";
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
            // btnAddToDeck
            // 
            btnAddToDeck.Location = new Point(726, 20);
            btnAddToDeck.Name = "btnAddToDeck";
            btnAddToDeck.Size = new Size(56, 169);
            btnAddToDeck.TabIndex = 24;
            btnAddToDeck.Text = "Add to deck";
            btnAddToDeck.UseVisualStyleBackColor = true;
            btnAddToDeck.Click += btnAddToDeck_Click;
            // 
            // CurrentP
            // 
            CurrentP.AutoSize = true;
            CurrentP.Location = new Point(600, 45);
            CurrentP.Name = "CurrentP";
            CurrentP.Size = new Size(94, 15);
            CurrentP.TabIndex = 25;
            CurrentP.Text = "Current Player: 0";
            // 
            // CurrentType
            // 
            CurrentType.AutoSize = true;
            CurrentType.Location = new Point(402, 35);
            CurrentType.Name = "CurrentType";
            CurrentType.Size = new Size(163, 15);
            CurrentType.TabIndex = 26;
            CurrentType.Text = "Current Card Type: Undefined";
            // 
            // pnlDecks
            // 
            pnlDecks.Controls.Add(listBox1);
            pnlDecks.Controls.Add(label2);
            pnlDecks.Controls.Add(listBox2);
            pnlDecks.Controls.Add(btnAddToDeck);
            pnlDecks.Controls.Add(label3);
            pnlDecks.Controls.Add(listBox3);
            pnlDecks.Controls.Add(label4);
            pnlDecks.Controls.Add(listBox4);
            pnlDecks.Controls.Add(label5);
            pnlDecks.Location = new Point(9, 78);
            pnlDecks.Name = "pnlDecks";
            pnlDecks.Size = new Size(789, 196);
            pnlDecks.TabIndex = 27;
            // 
            // pnlTrump
            // 
            pnlTrump.Controls.Add(comboBox1);
            pnlTrump.Controls.Add(btnTrump);
            pnlTrump.Location = new Point(252, 12);
            pnlTrump.Name = "pnlTrump";
            pnlTrump.Size = new Size(125, 61);
            pnlTrump.TabIndex = 28;
            // 
            // pnlShare
            // 
            pnlShare.Controls.Add(btnShare);
            pnlShare.Controls.Add(ShuffleBtn);
            pnlShare.Controls.Add(ShuffleTimes);
            pnlShare.Location = new Point(91, 12);
            pnlShare.Name = "pnlShare";
            pnlShare.Size = new Size(158, 61);
            pnlShare.TabIndex = 29;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 458);
            Controls.Add(pnlShare);
            Controls.Add(pnlTrump);
            Controls.Add(pnlDecks);
            Controls.Add(CurrentType);
            Controls.Add(CurrentP);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(TrumP);
            Controls.Add(listBox5);
            Controls.Add(btnNewGame);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)ShuffleTimes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            pnlDecks.ResumeLayout(false);
            pnlDecks.PerformLayout();
            pnlTrump.ResumeLayout(false);
            pnlShare.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ShuffleBtn;
        private NumericUpDown ShuffleTimes;
        private Button btnNewGame;
        private Button btnShare;
        private ListBox listBox1;
        private Label TrumP;
        private Label label2;
        private GroupBox groupBox1;
        private Label Team1TradesGiven;
        private Label label10;
        private Label Team1TradesHave;
        private Label Team1Loses;
        private Label Team1Draws;
        private Label Team1Wins;
        private ListBox listBox5;
        private Label label3;
        private ListBox listBox2;
        private Label label4;
        private ListBox listBox3;
        private Label label5;
        private ListBox listBox4;
        private GroupBox groupBox2;
        private Label Team2TradesGiven;
        private Label label13;
        private Label Team2TradesHave;
        private Label Team2Loses;
        private Label Team2Draws;
        private Label Team2Wins;
        private ComboBox comboBox1;
        private Button btnTrump;
        private Button btnAddToDeck;
        private Label CurrentP;
        private Label Team1WinsC;
        private Label Team2WinsC;
        private Label CurrentType;
        private Panel pnlDecks;
        private Panel pnlTrump;
        private Panel pnlShare;
    }
}
