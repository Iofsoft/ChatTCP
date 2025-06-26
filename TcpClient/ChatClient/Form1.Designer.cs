namespace ChatClient
{
    partial class Form1
    {
        // Removed duplicate declarations of txtChat, txtMensagem, and btnEnviar
        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;

        private void InitializeComponent()
        {
            txtChat = new RichTextBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            // 
            // txtChat
            // 
            txtChat.Location = new Point(14, 14);
            txtChat.Margin = new Padding(4, 3, 4, 3);
            txtChat.Multiline = true;
            txtChat.Name = "txtChat";
            txtChat.ReadOnly = true;
            txtChat.ScrollBars = RichTextBoxScrollBars.Vertical; // Fixed type to match RichTextBoxScrollBars
            txtChat.Size = new Size(536, 346);
            txtChat.TabIndex = 0;
            txtChat.TabStop = false;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(14, 375);
            txtMensagem.Margin = new Padding(4, 3, 4, 3);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(419, 23);
            txtMensagem.TabIndex = 1;
            txtMensagem.KeyDown += txtMensagem_KeyDown;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(443, 373);
            btnEnviar.Margin = new Padding(4, 3, 4, 3);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(107, 27);
            btnEnviar.TabIndex = 2;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 417);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(txtChat);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Cliente de Chat TCP do BES";
            ResumeLayout(false);
            PerformLayout();
        }
        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnviar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}

