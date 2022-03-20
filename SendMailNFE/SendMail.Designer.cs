namespace SendMailNFE
{
    partial class SendMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMail));
            this.mnuSendMailNFE = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizarDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarEmailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDoSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDANFEPDF = new System.Windows.Forms.Button();
            this.lblpgbMain = new System.Windows.Forms.Label();
            this.pgbMail = new System.Windows.Forms.ProgressBar();
            this.btnEnviarEmail = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnDesmarcarTodos = new System.Windows.Forms.Button();
            this.btnSelecionarTodos = new System.Windows.Forms.Button();
            this.mnuSendMailNFE.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuSendMailNFE
            // 
            this.mnuSendMailNFE.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuSendMailNFE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.mnuSendMailNFE.Location = new System.Drawing.Point(0, 0);
            this.mnuSendMailNFE.Name = "mnuSendMailNFE";
            this.mnuSendMailNFE.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnuSendMailNFE.Size = new System.Drawing.Size(939, 24);
            this.mnuSendMailNFE.TabIndex = 0;
            this.mnuSendMailNFE.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configBDToolStripMenuItem,
            this.configXMLToolStripMenuItem,
            this.atualizarDadosToolStripMenuItem,
            this.enviarEmailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.arquivoToolStripMenuItem.Text = "&Controles NFE";
            // 
            // configBDToolStripMenuItem
            // 
            this.configBDToolStripMenuItem.Name = "configBDToolStripMenuItem";
            this.configBDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.configBDToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.configBDToolStripMenuItem.Text = "Configurações &Banco Dados";
            // 
            // configXMLToolStripMenuItem
            // 
            this.configXMLToolStripMenuItem.Name = "configXMLToolStripMenuItem";
            this.configXMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.configXMLToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.configXMLToolStripMenuItem.Text = "Configurações &XML";
            this.configXMLToolStripMenuItem.Click += new System.EventHandler(this.configXMLToolStripMenuItem_Click);
            // 
            // atualizarDadosToolStripMenuItem
            // 
            this.atualizarDadosToolStripMenuItem.Name = "atualizarDadosToolStripMenuItem";
            this.atualizarDadosToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.atualizarDadosToolStripMenuItem.Text = "&Atualizar Dados";
            this.atualizarDadosToolStripMenuItem.Click += new System.EventHandler(this.atualizarDadosToolStripMenuItem_Click);
            // 
            // enviarEmailsToolStripMenuItem
            // 
            this.enviarEmailsToolStripMenuItem.Name = "enviarEmailsToolStripMenuItem";
            this.enviarEmailsToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.enviarEmailsToolStripMenuItem.Text = "&Enviar E-mails";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(271, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.sairToolStripMenuItem.Text = "Sai&r";
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobreToolStripMenuItem,
            this.manualDoSistemaToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.ajudaToolStripMenuItem.Text = "&Ajuda";
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sobreToolStripMenuItem.Text = "&Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // manualDoSistemaToolStripMenuItem
            // 
            this.manualDoSistemaToolStripMenuItem.Name = "manualDoSistemaToolStripMenuItem";
            this.manualDoSistemaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.manualDoSistemaToolStripMenuItem.Text = "&Manual do Sistema";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.dgvMain);
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(939, 395);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvMain
            // 
            this.dgvMain.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(939, 395);
            this.dgvMain.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnDANFEPDF);
            this.pnlBottom.Controls.Add(this.lblpgbMain);
            this.pnlBottom.Controls.Add(this.pgbMail);
            this.pnlBottom.Controls.Add(this.btnEnviarEmail);
            this.pnlBottom.Controls.Add(this.btnAtualizar);
            this.pnlBottom.Controls.Add(this.btnDesmarcarTodos);
            this.pnlBottom.Controls.Add(this.btnSelecionarTodos);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 418);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(939, 57);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnDANFEPDF
            // 
            this.btnDANFEPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDANFEPDF.Location = new System.Drawing.Point(672, 14);
            this.btnDANFEPDF.Name = "btnDANFEPDF";
            this.btnDANFEPDF.Size = new System.Drawing.Size(85, 24);
            this.btnDANFEPDF.TabIndex = 6;
            this.btnDANFEPDF.Text = "DANFE PDF";
            this.btnDANFEPDF.UseVisualStyleBackColor = true;
            this.btnDANFEPDF.Click += new System.EventHandler(this.btnDANFEPDF_Click);
            // 
            // lblpgbMain
            // 
            this.lblpgbMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblpgbMain.AutoSize = true;
            this.lblpgbMain.Location = new System.Drawing.Point(430, 36);
            this.lblpgbMain.Name = "lblpgbMain";
            this.lblpgbMain.Size = new System.Drawing.Size(92, 13);
            this.lblpgbMain.TabIndex = 5;
            this.lblpgbMain.Text = "N/N - Enviados";
            // 
            // pgbMail
            // 
            this.pgbMail.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pgbMail.Location = new System.Drawing.Point(305, 16);
            this.pgbMail.Name = "pgbMail";
            this.pgbMail.Size = new System.Drawing.Size(325, 17);
            this.pgbMail.TabIndex = 4;
            // 
            // btnEnviarEmail
            // 
            this.btnEnviarEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviarEmail.Location = new System.Drawing.Point(763, 14);
            this.btnEnviarEmail.Name = "btnEnviarEmail";
            this.btnEnviarEmail.Size = new System.Drawing.Size(89, 23);
            this.btnEnviarEmail.TabIndex = 3;
            this.btnEnviarEmail.Text = "&Enviar Email";
            this.btnEnviarEmail.UseVisualStyleBackColor = true;
            this.btnEnviarEmail.Click += new System.EventHandler(this.btnEnviarEmail_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtualizar.Location = new System.Drawing.Point(858, 14);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(69, 23);
            this.btnAtualizar.TabIndex = 2;
            this.btnAtualizar.Text = "&Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnDesmarcarTodos
            // 
            this.btnDesmarcarTodos.Location = new System.Drawing.Point(138, 14);
            this.btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            this.btnDesmarcarTodos.Size = new System.Drawing.Size(120, 23);
            this.btnDesmarcarTodos.TabIndex = 1;
            this.btnDesmarcarTodos.Text = "&Desmarcar Todos";
            this.btnDesmarcarTodos.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodos.Click += new System.EventHandler(this.btnDesmarcarTodos_Click);
            // 
            // btnSelecionarTodos
            // 
            this.btnSelecionarTodos.Location = new System.Drawing.Point(12, 15);
            this.btnSelecionarTodos.Name = "btnSelecionarTodos";
            this.btnSelecionarTodos.Size = new System.Drawing.Size(120, 23);
            this.btnSelecionarTodos.TabIndex = 0;
            this.btnSelecionarTodos.Text = "&Selecionar Todos";
            this.btnSelecionarTodos.UseVisualStyleBackColor = true;
            this.btnSelecionarTodos.Click += new System.EventHandler(this.btnSelecionarTodos_Click);
            // 
            // SendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 475);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.mnuSendMailNFE);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuSendMailNFE;
            this.Name = "SendMail";
            this.Text = "Send Mail NFE - ComMarc Software Solution";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendMail_FormClosing);
            this.Load += new System.EventHandler(this.SendMail_Load);
            this.mnuSendMailNFE.ResumeLayout(false);
            this.mnuSendMailNFE.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuSendMailNFE;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configBDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enviarEmailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDoSistemaToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ToolStripMenuItem configXMLToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnDesmarcarTodos;
        private System.Windows.Forms.Button btnSelecionarTodos;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.ToolStripMenuItem atualizarDadosToolStripMenuItem;
        private System.Windows.Forms.Button btnEnviarEmail;
        private System.Windows.Forms.ProgressBar pgbMail;
        private System.Windows.Forms.Label lblpgbMain;
        private System.Windows.Forms.Button btnDANFEPDF;
    }
}

