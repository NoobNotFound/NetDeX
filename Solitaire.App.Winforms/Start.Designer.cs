namespace Solitaire.App.Winforms
{
    partial class Start
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
            btnOndevice = new Button();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // btnOndevice
            // 
            btnOndevice.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOndevice.Location = new Point(140, 42);
            btnOndevice.Name = "btnOndevice";
            btnOndevice.Size = new Size(122, 53);
            btnOndevice.TabIndex = 0;
            btnOndevice.Text = "Solo";
            btnOndevice.UseVisualStyleBackColor = true;
            btnOndevice.Click += btnOndevice_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(140, 101);
            button1.Name = "button1";
            button1.Size = new Size(122, 53);
            button1.TabIndex = 1;
            button1.Text = "Host";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(140, 160);
            button2.Name = "button2";
            button2.Size = new Size(122, 53);
            button2.TabIndex = 2;
            button2.Text = "Join";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Start
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 263);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnOndevice);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Start";
            Text = "Start";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOndevice;
        private Button button1;
        private Button button2;
    }
}