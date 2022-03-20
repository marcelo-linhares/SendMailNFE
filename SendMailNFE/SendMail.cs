using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SendMailNFE.Connector;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Threading;
using SendMailNFE.Data;

namespace SendMailNFE
{
    public partial class SendMail : Form
    {

        #region Global Variables

        private String _CLASSNAME = "[SendMailNFE].[SendMail]";
        private String _MAIL_SUBJECT = "Envio de arquivo NFE";
        private String _MAIL_BODY = "Prezado Cliente, este e-mail refere-se a Nota Fiscal Eletrônica emitida pela ...";
        private String _MAIL_SERVER = "smtp.XXXX.com.br";
        private String _MAIL_USER = "XXXX@losinox.com.br";
        private String _MAIL_PASSWORD = "";
        private String _MAIL_FROM = "XXXX@losinox.com.br";
        private String _MAIL_CC1 = "XXXX@losinox.com.br";
        private String _MAIL_CC2 = "";
        private String _LOGINSQL = "";
        private String _SENHASQL = "";
        private BackgroundWorker thSend = new BackgroundWorker();
        private int TotalItens = 0;
        private ConfigSendMail _configSM = new ConfigSendMail();

        #endregion

        #region Starters
        public SendMail()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            thSend.DoWork += new DoWorkEventHandler(thSend_DoWork);
            thSend.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thSend_RunWorkerCompleted);
            thSend.ProgressChanged += new ProgressChangedEventHandler(thSend_ProgressChanged);
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Load do Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMail_Load(object sender, EventArgs e)
        {
            try
            {
                _MAIL_SUBJECT = _configSM.ConfigXML.MailSubject;
                _MAIL_BODY = _configSM.ConfigXML.MailBody;
                _MAIL_SERVER = _configSM.ConfigXML.MailServer;
                _MAIL_USER = _configSM.ConfigXML.MailUser;
                _MAIL_PASSWORD = _configSM.ConfigXML.MailPassword;
                _MAIL_FROM = _configSM.ConfigXML.MailFrom;
                _MAIL_CC1 = _configSM.ConfigXML.MailCC1;
                _MAIL_CC2 = _configSM.ConfigXML.MailCC2;

                pgbMail.Visible = false;
                lblpgbMain.Visible = false;

                this.thSend.WorkerReportsProgress = true;
                this.thSend.WorkerSupportsCancellation = true;

                atualizarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[SendMail_Load()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void configXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[configXMLToolStripMenuItem_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                atualizarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[btnAtualizar_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                atualizarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[atualizarDadosToolStripMenuItem_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelecionarTodos_Click(object sender, EventArgs e)
        {
            if (dgvMain.Rows.Count <= 0)
                return;

            foreach (DataGridViewRow dr in dgvMain.Rows)
            {
                dr.Cells[0].Value = Common.ValidRowToCheck(dr);
            }
        }

        private void btnDesmarcarTodos_Click(object sender, EventArgs e)
        {
            if (dgvMain.Rows.Count <= 0)
                return;

            foreach (DataGridViewRow dr in dgvMain.Rows)
            {
                dr.Cells[0].Value = false;
            }
        }

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                int sents = 0;
                TotalItens = totalLinhasSelecionadas();
                pgbMail.Maximum = 100;
                pgbMail.Minimum = 0;
                pgbMail.Value = 0;
                lblpgbMain.Text = "0 de " + TotalItens.ToString() + " email(s) enviado(s).";
                prepareControls(false);
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                thSend.RunWorkerAsync(sents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[btnEnviarEmail_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDANFEPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (GenerateReportPDF())
                    MessageBox.Show("PDF do DANFE gerado com sucesso.", "SendMail NFE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[btnDANFEPDF_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void SendMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thSend != null)
            {
                if (thSend.IsBusy)
                {
                    MessageBox.Show("Sistema em execução.", "SendMail NFE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutSendMail box = new AboutSendMail())
            {
                box.ShowDialog(this);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Deprecated
        /// </summary>
        private void listarNFE()
        {
            try
            {
                DirectoryInfo oDI = new DirectoryInfo(_configSM.ConfigXML.PathSource);
                DataTable oDT = new DataTable();
                oDT.Columns.Add("Check", System.Type.GetType("System.Boolean"));
                oDT.Columns.Add("FileName");
                oDT.Columns.Add("NF");
                oDT.Columns.Add("CodigoCliente");
                oDT.Columns.Add("NomeCliente");
                oDT.Columns.Add("EmailCliente");
                oDT.Columns.Add("DtEmissao");
                oDT.Columns.Add("IdNFE");

                foreach (FileInfo oFI in oDI.GetFiles("*.XML"))
                {
                    StreamReader oSR = new StreamReader(oFI.FullName);
                    String TextXML = oSR.ReadToEnd().Replace("xmlns=\"http://www.portalfiscal.inf.br/nfe\"", "");
                    // Utilizando o xml para preecher o objeto de dados
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(TextXML);

                    DataRow oDR = oDT.NewRow();
                    oDR["FileName"] = oFI.Name;
                    oDR["NF"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagNrNFE).InnerText;
                    oDR["NomeCliente"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagNmCliente).InnerText;
                    if (xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + "../@versao").InnerText.Substring(0, 1).Equals("3"))
                        oDR["DtEmissao"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagDtHrEmissao).InnerText;
                    else
                        oDR["DtEmissao"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagDtEmissao).InnerText;
                    oDR["IdNFE"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagIdNFE).InnerText.Replace("NFe", "");


                    if (xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente) == null)
                    {
                        oDR["EmailCliente"] = "Indisponível";
                    }
                    else
                    {
                        if (!xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente).InnerText.Equals(""))
                            oDR["EmailCliente"] = xmlDoc.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente).InnerText;
                        else
                            oDR["EmailCliente"] = "Indisponível";
                    }

                    oDT.Rows.Add(oDR);

                    xmlDoc = null;
                    oSR.Close();
                    oSR.Dispose();
                    oSR = null;
                }

                dgvMain.DataSource = oDT;
                oDI = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[listarNFE()]");
            }
            return;
        }

        private void listarNFEnew()
        {
            try
            {
                DirectoryInfo oDI = new DirectoryInfo(_configSM.ConfigXML.PathSource);
                DataTable oDT = new DataTable();
                oDT.Columns.Add("Check", System.Type.GetType("System.Boolean"));
                oDT.Columns.Add("FileName");
                oDT.Columns.Add("NF");
                //oDT.Columns.Add("CodigoCliente");
                oDT.Columns.Add("NomeCliente");
                oDT.Columns.Add("EmailCliente");
                oDT.Columns.Add("DtEmissao");
                oDT.Columns.Add("IdNFE");
                oDT.Columns.Add("IsProcessada");
                oDT.Columns.Add("DANFEvalida");

                foreach (FileInfo oFI in oDI.GetFiles("*.XML"))
                {
                    StreamReader oSR = new StreamReader(oFI.FullName);
                    String TextXML = oSR.ReadToEnd().Replace("xmlns=\"http://www.portalfiscal.inf.br/nfe\"", "");
                    // Utilizando o xml para preecher o objeto de dados
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(TextXML);

                    DataRow oDR = oDT.NewRow();

                    NFE oNFE = TratarXMLNFE(xmlDoc);

                    oDR["FileName"] = oFI.Name;
                    oDR["NF"] = oNFE.codigo_nota_fiscal;
                    oDR["NomeCliente"] = oNFE.nome_cliente;
                    oDR["DtEmissao"] = oNFE.data_emissao;
                    oDR["IdNFE"] = oNFE.codigo_NFE;
                    oDR["EmailCliente"] = oNFE.email_cliente;
                    oDR["IsProcessada"] = oNFE.indicador_NFE_processada;
                    oDR["DANFEvalida"] = FindValidDANFEPDF(oFI.Name, _configSM.ConfigXML.PathSource, "");

                    oDT.Rows.Add(oDR);

                    xmlDoc = null;
                    oSR.Close();
                    oSR.Dispose();
                    oSR = null;
                }

                dgvMain.DataSource = oDT;
                oDI = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[listarNFE()]");
            }
            return;
        }

        private NFE TratarXMLNFE(XmlDocument xmlNFE)
        {
            try
            {
                NFE oNFE = new NFE();

                
                // Tratar XML sem processamento
                if (xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML) != null)
                {

                    oNFE.codigo_nota_fiscal = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagNrNFE).InnerText;
                    oNFE.nome_cliente = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagNmCliente).InnerText;
                    oNFE.data_emissao = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagDtHrEmissao).InnerText;
                    oNFE.codigo_NFE = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagIdNFE).InnerText.Replace("NFe", "");

                    if (xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente) == null)
                    {
                        oNFE.email_cliente = "Indisponível";
                    }
                    else
                    {
                        if (!xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente).InnerText.Equals(""))
                            oNFE.email_cliente = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXML + _configSM.ConfigXML.TagPathEmailCliente).InnerText;
                        else
                            oNFE.email_cliente = "Indisponível";
                    }

                    oNFE.indicador_NFE_processada = false;
                }
                else
                {
                    if (xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado) != null)
                    {
                        oNFE.codigo_nota_fiscal = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagNrNFE).InnerText;
                        oNFE.nome_cliente = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagNmCliente).InnerText;
                        oNFE.data_emissao = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagDtHrEmissao).InnerText;
                        oNFE.codigo_NFE = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagIdNFE).InnerText.Replace("NFe", "");

                        if (xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagPathEmailCliente) == null)
                        {
                            oNFE.email_cliente = "Indisponível";
                        }
                        else
                        {
                            if (!xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagPathEmailCliente).InnerText.Equals(""))
                                oNFE.email_cliente = xmlNFE.SelectSingleNode(_configSM.ConfigXML.PathXMLProcessado + _configSM.ConfigXML.TagPathEmailCliente).InnerText;
                            else
                                oNFE.email_cliente = "Indisponível";
                        }

                        oNFE.indicador_NFE_processada = true;
                    }
                    else 
                    {
                        throw new Exception("Arquivo XML inválido!");
                    }
                }

                return oNFE;
                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[TratarXMLNFE()]");
            }
        }

        private bool atualizarDados()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //listarNFE();
                listarNFEnew();
                dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMain.AutoResizeColumns();
                dgvMain.AllowUserToDeleteRows = false;
                dgvMain.AllowUserToAddRows = false;
                //dgvMain. .EditMode = DataGridViewEditMode.EditProgrammatically;
                dgvMain.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                formatarGrid();
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[atualizarDados()]");
            }
            return true;
        }

        private void formatarGrid()
        {
            try
            {
                //dgvMain.Columns[7].Visible = false;
                foreach (DataGridViewRow dr in dgvMain.Rows)
                {
                    if (dr.Cells["EmailCliente"].Value.ToString().Equals("Indisponível"))
                    {
                        //dr.ReadOnly = true;
                        dr.DefaultCellStyle.ForeColor = Color.Red;
                    }

                    dr.ReadOnly = !Common.ValidRowToCheck(dr);
                    /*if (dr.Cells["IsProcessada"].Value.ToString().Equals("False") || dr.Cells["DANFEvalida"].Value.ToString().Equals(String.Empty))
                    {
                        dr.ReadOnly = true;
                    }*/
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[formatarGrid()]");
            }
            return;
        }

        private int sendMail(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            int send = 0;
            try
            {
                MailMessage mmMail;

                foreach (DataGridViewRow dr in dgvMain.Rows)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (dr.Cells[0].Value != DBNull.Value)
                        {
                            if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                            {
                                String fileName = _configSM.ConfigXML.PathSource + dr.Cells["FileName"].Value.ToString();
                                String fileDANFE = _configSM.ConfigXML.PathSource + dr.Cells["DANFEvalida"].Value.ToString();
                                String folderDest = dr.Cells["DtEmissao"].Value.ToString().Substring(0, 4) + "\\" + dr.Cells["DtEmissao"].Value.ToString().Substring(5, 2) + "\\";

                                String[] nomeCliente = dr.Cells["NomeCliente"].Value.ToString().Split(' ');

                                String fileNameDest = _configSM.ConfigXML.PathSource + "Enviados\\" + folderDest + "nfe_" + dr.Cells["NF"].Value.ToString() + "_" + nomeCliente[0] + ".xml";
                                String fileDANFENameDest = _configSM.ConfigXML.PathSource + "Enviados\\" + folderDest + "nfe_" + dr.Cells["NF"].Value.ToString() + "_" + nomeCliente[0] + ".pdf";
                                String toName = dr.Cells["EmailCliente"].Value.ToString();
                                //String toName = "mcellobb@gmail.com";


                                if (toName.Contains("@"))
                                {
                                    //**** Email de envio - Cliente
                                    String complementoBody = "";
                                    complementoBody = "\n\n Nota Fiscal: " + dr.Cells["NF"].Value.ToString();
                                    complementoBody += "\n Cliente: " + dr.Cells["NomeCliente"].Value.ToString();
                                    complementoBody += "\n Chave de Acesso: " + dr.Cells["IdNFE"].Value.ToString();
                                    mmMail = new MailMessage(_MAIL_FROM, toName, _MAIL_SUBJECT + " [" + Common.PrepareFileNameNFE(dr.Cells["NF"].Value.ToString(), nomeCliente[0]) + "]", _MAIL_BODY + complementoBody);
                                    //**** Email de envio - Cliente

                                    if (!_MAIL_CC1.Equals(String.Empty))
                                        mmMail.CC.Add(_MAIL_CC1);

                                    if (!_MAIL_CC2.Equals(String.Empty))
                                        mmMail.CC.Add(_MAIL_CC2);

                                    Attachment attMail = new Attachment(fileName, MediaTypeNames.Application.Octet);
                                    Attachment attDANFEMail = new Attachment(fileDANFE, MediaTypeNames.Application.Pdf);

                                    ContentDisposition ctdMail = attMail.ContentDisposition;
                                    ctdMail.CreationDate = File.GetCreationTime(fileName);
                                    ctdMail.ModificationDate = File.GetLastWriteTime(fileName);
                                    ctdMail.ReadDate = File.GetLastAccessTime(fileName);
                                    mmMail.Attachments.Add(attMail);
                                    mmMail.Attachments.Add(attDANFEMail);

                                    SmtpClient smtpMail = new SmtpClient(_MAIL_SERVER);
                                    smtpMail.Port = Convert.ToInt32(_configSM.ConfigXML.MailPort);
                                    smtpMail.EnableSsl = true;
                                    smtpMail.Credentials = new NetworkCredential(_MAIL_USER, _MAIL_PASSWORD);
                                    smtpMail.Send(mmMail);

                                    smtpMail = null;
                                    ctdMail = null;
                                    attMail.Dispose();
                                    attMail = null;
                                    mmMail.Dispose();
                                    mmMail = null;

                                    if (!Directory.Exists(_configSM.ConfigXML.PathSource + "Enviados\\" + folderDest))
                                        Directory.CreateDirectory(_configSM.ConfigXML.PathSource + "Enviados\\" + folderDest);

                                    FileInfo fileInfo = new FileInfo(fileNameDest);
                                    if (fileInfo.Exists)
                                        fileInfo.Delete();
                                    else
                                        File.Move(fileName, fileNameDest);

                                    FileInfo fileInfoDanfe = new FileInfo(fileDANFENameDest);
                                    if (fileInfoDanfe.Exists)
                                        fileInfoDanfe.Delete();
                                    else
                                        File.Move(fileDANFE, fileDANFENameDest);

                                    send++;
                                }
                                int percentComplete = (int)((float)send / (float)TotalItens * 100);
                                worker.ReportProgress(percentComplete);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[sendMail()]");
            }
            return send;
        }



        private int totalLinhasSelecionadas()
        {
            try
            {
                int totalLinhas = 0;
                foreach (DataGridViewRow dr in dgvMain.Rows)
                {
                    if (dr.Cells[0].Value != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                        {
                            totalLinhas++;
                        }
                    }
                }
                return totalLinhas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[totalLinhasSelecionadas()]");
            }
        }

        /// <summary>
        /// Prepare controls to use.
        /// </summary>
        /// <param name="val">If was "true" value, keep controls free to use. If was "false", freeze controls that can cause some trouble during the execution.</param>
        private void prepareControls(bool val)
        {
            btnAtualizar.Enabled = val;
            btnDesmarcarTodos.Enabled = val;
            btnEnviarEmail.Enabled = val;
            btnSelecionarTodos.Enabled = val;
            dgvMain.Enabled = val;
            mnuSendMailNFE.Enabled = val;

            pgbMail.Visible = !val;
            lblpgbMain.Visible = !val;
        }

        private bool GenerateReportPDF()
        {
            try
            {
                CreatePDF x = new CreatePDF();

                foreach (DataGridViewRow dr in dgvMain.Rows)
                {
                    if (dr.Cells[0].Value != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                        {
                            String fileName = _configSM.ConfigXML.PathSource + dr.Cells["FileName"].Value.ToString();
                            String[] nomeCliente = dr.Cells["NomeCliente"].Value.ToString().Split(' ');

                            x.GenerateReport(fileName, @"C:\NFE\Losinox\DOC\" + Common.PrepareFileNameNFE(dr.Cells["NF"].Value.ToString(), nomeCliente[0]), _configSM.ConfigXML.PathTemplateNFE);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[]");
            }
        }

        /// <summary>
        /// This method find to a DANFE PDF document that to be valid
        /// Mail can't to be send if this document doesn't exist. It's business rule.
        /// First here, we use the XML base to validate this infos. 
        /// Since the name of XML file could be the same of the PDF file, this is one (but not only) way to make this check.
        /// </summary>
        /// <param name="xmlFileName">Name of XML file to looking for the PDF file.</param>
        /// <param name="pathToFind">Where the PDF are stored.</param>
        /// <param name="keyToValidate">Some string that make the PDF unique</param>
        /// <param name="recursiveSearch">True or false recursive search</param>
        /// <returns></returns>
        public string FindValidDANFEPDF(string xmlFileName, string pathToFind, string keyToValidate, bool recursiveSearch = false)
        {
            string PDFFileName = xmlFileName.ToUpperInvariant().Replace("XML", "PDF");

            FileInfo fileInfo = new FileInfo(pathToFind + PDFFileName);

            if (fileInfo.Exists)
                return fileInfo.Name;
            else
                return string.Empty;
        }

        #endregion

        #region BackgroundEvents
        private void thSend_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                e.Result = sendMail((int)e.Argument, worker, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[thSend_DoWork()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message, "Erros encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    MessageBox.Show("Envio cancelado.", "SendMail NF-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (e.Result != null)
                        MessageBox.Show(e.Result.ToString() + " email(s) enviado(s) com sucesso!", "SendMail NF-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (e.Result != null)
                        MessageBox.Show(e.Result.ToString() + " email(s) enviado(s) com sucesso!", "SendMail NF-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[thSend_RunWorkerCompleted()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            prepareControls(true);
            atualizarDados();
            this.Cursor = Cursors.Arrow;
        }

        private void thSend_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                pgbMail.Value = e.ProgressPercentage;
                int current = (int)(((float)e.ProgressPercentage / 100) * (float)TotalItens);
                lblpgbMain.Text = current.ToString() + " de " + TotalItens.ToString() + " email(s) enviado(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[thSend_ProgressChanged()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}