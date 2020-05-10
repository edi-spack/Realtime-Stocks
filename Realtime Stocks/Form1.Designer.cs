namespace Realtime_Stocks
{
    partial class Form1
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
            this.stocksComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // stocksComboBox
            // 
            this.stocksComboBox.FormattingEnabled = true;
            this.stocksComboBox.Items.AddRange(new object[] {
            "AAPL",
            "GOOGL",
            "MSFT",
            "AMZN",
            "FB",
            "BRK.A",
            "BABA",
            "JNJ",
            "JPM",
            "XOM"});
            this.stocksComboBox.Location = new System.Drawing.Point(700, 20);
            this.stocksComboBox.Name = "stocksComboBox";
            this.stocksComboBox.Size = new System.Drawing.Size(121, 24);
            this.stocksComboBox.TabIndex = 0;
            this.stocksComboBox.SelectedIndexChanged += new System.EventHandler(this.OnStockChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 453);
            this.Controls.Add(this.stocksComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Realtime Stocks";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox stocksComboBox;
    }
}

