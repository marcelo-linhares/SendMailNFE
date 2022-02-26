using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SendMailNFE
{
    public class ConfigSendMail
    {

        private ConfigSendMailSQLData _configSQL = new ConfigSendMailSQLData();
        private ConfigSendMailXMLData _configXML = new ConfigSendMailXMLData();

        public ConfigSendMailSQLData ConfigSQL
        {
            get { return _configSQL; }
            set { ConfigSQL = value; }
        }

        public ConfigSendMailXMLData ConfigXML
        {
            get { return _configXML; }
            set { ConfigXML = value; }
        }

        public ConfigSendMail()
        {
            ReadXML();
        }

        private void ReadXML()
        { 
            // Busca e lê xml de configuração da aplicação
            using (XmlTextReader xmlConfig = new XmlTextReader(System.AppDomain.CurrentDomain.BaseDirectory + "\\configSendMailNFE.xml"))
            {
                xmlConfig.Read();
                // Utilizando o xml para preecher o objeto de dados
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlConfig);


                _configSQL.Server = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/Server").InnerText;
                _configSQL.DataBaseNF = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/DataBaseNF").InnerText;
                _configSQL.TableNF = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/TableNF").InnerText;
                _configSQL.ColumnIdNF = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/ColumnIdNF").InnerText;
                _configSQL.ColumnIdClienteNF = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/ColumnIdClienteNF").InnerText;
                _configSQL.DataBaseCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/DataBaseCliente").InnerText;
                _configSQL.TableCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/TableCliente").InnerText;
                _configSQL.ColumnIdCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/ColumnIdCliente").InnerText;
                _configSQL.ColumnTextCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/ColumnTextCliente").InnerText;
                _configSQL.ColumnEmailCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigSQL/ColumnEmailCliente").InnerText;

                _configXML.PathSource = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/PathSource").InnerText;
                _configXML.PathXML = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/PathXML").InnerText;
                _configXML.PathXMLProcessado = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/PathXMLProcessado").InnerText;
                _configXML.PathTemplateNFE = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/PathTemplateNFE").InnerText;
                _configXML.TagNrNFE = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagNrNFE").InnerText;
                _configXML.TagDtEmissao = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagDtEmissao").InnerText;
                _configXML.TagDtHrEmissao = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagDtHrEmissao").InnerText;
                _configXML.TagNmCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagNmCliente").InnerText;
                _configXML.TagIdNFE = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagIdNFE").InnerText;
                _configXML.TagValidaEmail = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagValidaEmail").InnerText;
                _configXML.TagEmailCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagEmailCliente").InnerText;
                _configXML.TagPathEmailCliente = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/TagPathEmailCliente").InnerText;


                _configXML.MailBody = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailBody").InnerText;
                _configXML.MailCC1 = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailCC1").InnerText;
                _configXML.MailCC2 = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailCC2").InnerText;
                _configXML.MailFrom = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailFrom").InnerText;
                _configXML.MailPassword = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailPassword").InnerText;
                _configXML.MailServer = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailServer").InnerText;
                _configXML.MailSubject = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailSubject").InnerText;
                _configXML.MailUser = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailUser").InnerText;
                _configXML.MailPort = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/MailPort").InnerText;
                
                _configXML.ConfigXMLNFE = MontarConfigXML(xmlDoc);

                xmlDoc = null;
                xmlConfig.Close();
            }

            return;
        }

        private Dictionary<string, string> MontarConfigXML(XmlDocument xmlDoc)
        {
            Dictionary<string, string> configXMLNFE = new Dictionary<string, string>();

            //XmlNodeList listaXML = xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/NFE3/");

            foreach (XmlNode singleNode in xmlDoc.SelectSingleNode("/SendMailNFE/ConfigXML/NFE3").ChildNodes)
            {
                if (!singleNode.Name.Equals("PathXML"))
                {
                    configXMLNFE.Add(singleNode.InnerText, singleNode.SelectSingleNode("@Path").InnerText);
                }
                else
                {
                    //this.ConfigXML.PathXML = singleNode.InnerText;
                    //configXMLNFE.Add(singleNode.InnerText, singleNode.SelectSingleNode("@Path").InnerText);
                }
                    
            }

            return configXMLNFE;
        }


    }
}
