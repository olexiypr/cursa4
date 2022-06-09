namespace cursachMVC
{
    partial class MenuForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.singleGameButton = new System.Windows.Forms.Button();
            this.timerCheckBox = new System.Windows.Forms.CheckBox();
            this.twoPlayersButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // singleGameButton
            // 
            this.singleGameButton.Location = new System.Drawing.Point(25, 50);
            this.singleGameButton.Name = "singleGameButton";
            this.singleGameButton.Size = new System.Drawing.Size(105, 37);
            this.singleGameButton.TabIndex = 0;
            this.singleGameButton.Text = "Грати проти комп\'ютера";
            this.singleGameButton.UseVisualStyleBackColor = true;
            // 
            // timerCheckBox
            // 
            this.timerCheckBox.AutoSize = true;
            this.timerCheckBox.Location = new System.Drawing.Point(116, 125);
            this.timerCheckBox.Name = "timerCheckBox";
            this.timerCheckBox.Size = new System.Drawing.Size(118, 17);
            this.timerCheckBox.TabIndex = 1;
            this.timerCheckBox.Text = "Грати з таймером";
            this.timerCheckBox.UseVisualStyleBackColor = true;
            // 
            // twoPlayersButton
            // 
            this.twoPlayersButton.Location = new System.Drawing.Point(205, 50);
            this.twoPlayersButton.Name = "twoPlayersButton";
            this.twoPlayersButton.Size = new System.Drawing.Size(105, 37);
            this.twoPlayersButton.TabIndex = 2;
            this.twoPlayersButton.Text = "Грати вдвох";
            this.twoPlayersButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(70, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Виберіть режим гри";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.twoPlayersButton);
            this.Controls.Add(this.timerCheckBox);
            this.Controls.Add(this.singleGameButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tic-tac-toe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button singleGameButton;
        private System.Windows.Forms.CheckBox timerCheckBox;
        private System.Windows.Forms.Button twoPlayersButton;
        private System.Windows.Forms.Label label1;
    }
}

