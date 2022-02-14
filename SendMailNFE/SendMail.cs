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

namespace SendMailNFE
{
    public partial class SendMail : Form
    {
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

        private void listarNFE(ConfigSendMail configSM)
        {
            try
            {
                Connect cnn = new Connect();

                cnn.DataBaseName = configSM.ConfigSQL.DataBaseNF;
                cnn.ServerSQL = configSM.ConfigSQL.Server;
                cnn.LoginSQL = _LOGINSQL;
                cnn.SenhaSQL = _SENHASQL;

                DirectoryInfo oDI = new DirectoryInfo(configSM.ConfigXML.PathSource);
                DataTable oDT = new DataTable();
                oDT.Columns.Add("EnviaEmail", System.Type.GetType("System.Boolean"));
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
                    oDR["NF"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagNrNFE).InnerText;
                    oDR["NomeCliente"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagNmCliente).InnerText;
                    if (xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + "../../../@versao").InnerText.Substring(0,1).Equals("3"))
                        oDR["DtEmissao"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagDtHrEmissao).InnerText;
                    else
                        oDR["DtEmissao"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagDtEmissao).InnerText;
                    oDR["IdNFE"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagIdNFE).InnerText.Replace("NFe", "");

                    //dEmi

                    //if (xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagValidaEmail).InnerText.Equals(configSM.ConfigXML.TagEmailCliente))
                    /*if (xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagPathEmailCliente) == null)
                        oDR["EmailCliente"] = string.Empty;
                    else
                        oDR["EmailCliente"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagPathEmailCliente).InnerText;
                    */


                    if (xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagPathEmailCliente) == null)
                    {
                        oDR["EmailCliente"] = "Indisponível";
                    }
                    else
                    {
                        if (!xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagPathEmailCliente).InnerText.Equals(""))
                            oDR["EmailCliente"] = xmlDoc.SelectSingleNode(configSM.ConfigXML.PathXML + configSM.ConfigXML.TagPathEmailCliente).InnerText;
                        else
                            oDR["EmailCliente"] = "Indisponível";
                    }
                    
/*
                    String sSQL;
                    sSQL = "select " + configSM.ConfigSQL.DataBaseCliente + ".." + configSM.ConfigSQL.TableCliente + "." + configSM.ConfigSQL.ColumnIdCliente + ", ";
                    sSQL += configSM.ConfigSQL.DataBaseCliente + ".." + configSM.ConfigSQL.TableCliente + "." + configSM.ConfigSQL.ColumnTextCliente + ", ";
                    sSQL += configSM.ConfigSQL.DataBaseCliente + ".." + configSM.ConfigSQL.TableCliente + "." + configSM.ConfigSQL.ColumnEmailCliente + " ";
                    sSQL += "from " + configSM.ConfigSQL.DataBaseCliente + ".." + configSM.ConfigSQL.TableCliente + " with (nolock) ";
                    sSQL += "inner join " + configSM.ConfigSQL.DataBaseNF + ".." + configSM.ConfigSQL.TableNF + " with (nolock) ";
                    sSQL += "on " + configSM.ConfigSQL.DataBaseCliente + ".." + configSM.ConfigSQL.TableCliente + "." + configSM.ConfigSQL.ColumnIdCliente + " = ";
                    sSQL += configSM.ConfigSQL.DataBaseNF + ".." + configSM.ConfigSQL.TableNF + "." + configSM.ConfigSQL.ColumnIdClienteNF + " ";
                    sSQL += "where " + configSM.ConfigSQL.DataBaseNF + ".." + configSM.ConfigSQL.TableNF + "." + configSM.ConfigSQL.ColumnIdNF + " = " + oDR["NF"].ToString();

                    SqlDataReader sqlDR = cnn.GetDataReader(sSQL);

                    if (sqlDR.HasRows)
                    {
                        oDR["CodigoCliente"] = sqlDR.GetValue(0).ToString();
                        oDR["NomeCliente"] = sqlDR.GetValue(1).ToString();
                        oDR["EmailCliente"] = sqlDR.GetValue(2).ToString();
                    }
                    else
                    {
                        oDR["CodigoCliente"] = "";
                        oDR["NomeCliente"] = "";
                        oDR["EmailCliente"] = "";
                    }
*/

                    oDT.Rows.Add(oDR);

                    //sqlDR.Close();
                    //sqlDR.Dispose();
                    //sqlDR = null;
                    xmlDoc = null;
                    oSR.Close();
                    oSR.Dispose();
                    oSR = null;
                }

                dgvMain.DataSource = oDT;
                cnn = null;
                oDI = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[listarNFE()]");
            }
            return;
        }

        private void SendMail_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigSendMail configSM = new ConfigSendMail();

                _MAIL_SUBJECT = configSM.ConfigXML.MailSubject;
                _MAIL_BODY = configSM.ConfigXML.MailBody;
                _MAIL_SERVER = configSM.ConfigXML.MailServer;
                _MAIL_USER = configSM.ConfigXML.MailUser;
                _MAIL_PASSWORD = configSM.ConfigXML.MailPassword;
                _MAIL_FROM = configSM.ConfigXML.MailFrom;
                _MAIL_CC1 = configSM.ConfigXML.MailCC1;
                _MAIL_CC2 = configSM.ConfigXML.MailCC2;

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

        private bool atualizarDados()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                ConfigSendMail configSM = new ConfigSendMail();
                listarNFE(configSM);
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
                dgvMain.Columns[7].Visible = false;
                foreach (DataGridViewRow dr in dgvMain.Rows)
                {
                    if (dr.Cells["EmailCliente"].Value.ToString().Equals("Indisponível"))
                    {
                        //dr.ReadOnly = true;
                        dr.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + _CLASSNAME + ".[formatarGrid()]");
            }
            return;
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
                dr.Cells[0].Value = true;
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

        private int sendMail(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            int send = 0;
            try
            {
                ConfigSendMail configSM = new ConfigSendMail();
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
                                String fileName = configSM.ConfigXML.PathSource + dr.Cells[1].Value.ToString();
                                String folderDest = dr.Cells[6].Value.ToString().Substring(0, 4) + "\\" + dr.Cells[6].Value.ToString().Substring(5, 2) + "\\";

                                String[] nomeCliente = dr.Cells[4].Value.ToString().Split(' ');

                                String fileNameDest = configSM.ConfigXML.PathSource + "Enviados\\" + folderDest + "nfe_" + dr.Cells[2].Value.ToString() + "_" + nomeCliente[0] + ".xml";
                                String toName = dr.Cells[5].Value.ToString();
                                //String toName = "mcellobb@gmail.com";


                                if (toName.Contains("@"))
                                {
                                    //**** Email de envio - Cliente
                                    String complementoBody = "";
                                    complementoBody = "\n\n Nota Fiscal: " + dr.Cells[2].Value.ToString();
                                    complementoBody += "\n Cliente: " + dr.Cells[4].Value.ToString();
                                    complementoBody += "\n Chave de Acesso: " + dr.Cells[7].Value.ToString();
                                    mmMail = new MailMessage(_MAIL_FROM, toName, _MAIL_SUBJECT, _MAIL_BODY + complementoBody);
                                    //**** Email de envio - Cliente

                                    if (!_MAIL_CC1.Equals(String.Empty))
                                        mmMail.CC.Add(_MAIL_CC1);

                                    if (!_MAIL_CC2.Equals(String.Empty))
                                        mmMail.CC.Add(_MAIL_CC2);

                                    Attachment attMail = new Attachment(fileName, MediaTypeNames.Application.Octet);

                                    ContentDisposition ctdMail = attMail.ContentDisposition;
                                    ctdMail.CreationDate = File.GetCreationTime(fileName);
                                    ctdMail.ModificationDate = File.GetLastWriteTime(fileName);
                                    ctdMail.ReadDate = File.GetLastAccessTime(fileName);
                                    mmMail.Attachments.Add(attMail);

                                    SmtpClient smtpMail = new SmtpClient(_MAIL_SERVER);
                                    smtpMail.Port = Convert.ToInt32(configSM.ConfigXML.MailPort);
                                    //smtpMail.EnableSsl = true;
                                    smtpMail.Credentials = new NetworkCredential(_MAIL_USER, _MAIL_PASSWORD);
                                    smtpMail.Send(mmMail);

                                    smtpMail = null;
                                    ctdMail = null;
                                    attMail.Dispose();
                                    attMail = null;
                                    mmMail.Dispose();
                                    mmMail = null;

                                    if (!Directory.Exists(configSM.ConfigXML.PathSource + "Enviados\\" + folderDest))
                                        Directory.CreateDirectory(configSM.ConfigXML.PathSource + "Enviados\\" + folderDest);

                                    File.Move(fileName, fileNameDest);
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

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                //thSend = new Thread(new ThreadStart(sendMail));
                //thSend.IsBackground = true;
                int sents = 0;
                TotalItens = totalLinhasSelecionadas();
                pgbMail.Maximum = 100;
                pgbMail.Minimum = 0;
                pgbMail.Value = 0;
                lblpgbMain.Text = "0 de " + TotalItens.ToString() + " email(s) enviado(s).";
                prepareControls(false);
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                thSend.RunWorkerAsync(sents);
                //MessageBox.Show(sents.ToString() + " email(s) enviado(s) com sucesso!", "SendMail NF-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //atualizarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + _CLASSNAME + ".[btnEnviarEmail_Click()]", "Erros Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnDANFEPDF_Click(object sender, EventArgs e)
        {
            ConfigSendMail configSM = new ConfigSendMail();
            CreatePDF x = new CreatePDF();

            foreach (DataGridViewRow dr in dgvMain.Rows)
            {
                if (dr.Cells[0].Value != DBNull.Value)
                {
                    if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                    {
                        String fileName = configSM.ConfigXML.PathSource + dr.Cells[1].Value.ToString();
                        String[] nomeCliente = dr.Cells[4].Value.ToString().Split(' ');

                        x.GenerateReport(fileName, configSM.ConfigXML.ConfigXMLNFE, configSM.ConfigXML.PathXML.Replace("/ide", ""), @"C:\NFE\DOC\" + Common.PrepareFileNameNFE(dr.Cells[2].Value.ToString(), nomeCliente[0]), configSM.ConfigXML.PathTemplateNFE);
                    }
                }
            }
        }
    }
}