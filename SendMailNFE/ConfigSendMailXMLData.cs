using System;
using System.Collections.Generic;
using System.Text;

namespace SendMailNFE
{
    public class ConfigSendMailXMLData
    {
        private String _pathSource;
        private String _pathXML;
        private String _tagNrNFE;
        private String _tagDtEmissao;
        private String _tagDtHrEmissao;
        private String _tagNmCliente;
        private String _tagIdNFE;
        private String _tagValidaEmail;
        private String _tagEmailCliente;
        private String _tagPathEmailCliente;
        private String _mailSubject;
        private String _mailBody;
        private String _mailServer;
        private String _mailUser;
        private String _mailPassword;
        private String _mailFrom;
        private String _mailCC1;
        private String _mailCC2;
        private String _mailPort;
        private Dictionary<string, string> _configXMLNFE;
        private String _pathTemplateNFE;

        public String PathSource
        {
            get { return _pathSource; }
            set { _pathSource = value; }
        }

        public String PathXML
        {
            get { return _pathXML; }
            set { _pathXML = value; }
        }

        public String TagNrNFE
        {
            get { return _tagNrNFE; }
            set { _tagNrNFE = value; }
        }

        public String TagDtEmissao
        {
            get { return _tagDtEmissao; }
            set { _tagDtEmissao = value; }
        }

        public String TagDtHrEmissao
        {
            get { return _tagDtHrEmissao; }
            set { _tagDtHrEmissao = value; }
        }

        public String TagNmCliente
        {
            get { return _tagNmCliente; }
            set { _tagNmCliente = value; }
        }

        public String TagIdNFE
        {
            get { return _tagIdNFE; }
            set { _tagIdNFE = value; }
        }

        public String TagValidaEmail
        {
            get { return _tagValidaEmail; }
            set { _tagValidaEmail = value; }
        }

        public String TagEmailCliente
        {
            get { return _tagEmailCliente; }
            set { _tagEmailCliente = value; }
        }

        public String TagPathEmailCliente
        {
            get { return _tagPathEmailCliente; }
            set { _tagPathEmailCliente = value; }
        }

        public String MailSubject
        {
            get { return _mailSubject; }
            set { _mailSubject = value; }
        }

        public String MailBody
        {
            get { return _mailBody; }
            set { _mailBody = value; }
        }

        public String MailServer
        {
            get { return _mailServer; }
            set { _mailServer = value; }
        }

        public String MailUser
        {
            get { return _mailUser; }
            set { _mailUser = value; }
        }

        public String MailPassword
        {
            get { return _mailPassword; }
            set { _mailPassword = value; }
        }

        public String MailFrom
        {
            get { return _mailFrom; }
            set { _mailFrom = value; }
        }

        public String MailCC1
        {
            get { return _mailCC1; }
            set { _mailCC1 = value; }
        }

        public String MailCC2
        {
            get { return _mailCC2; }
            set { _mailCC2 = value; }
        }

        public String MailPort
        {
            get { return _mailPort; }
            set { _mailPort = value; }
        }

        public Dictionary<string, string> ConfigXMLNFE
        {
            get { return _configXMLNFE; }
            set { _configXMLNFE = value; }
        }

        public String PathTemplateNFE
        {
            get { return _pathTemplateNFE; }
            set { _pathTemplateNFE = value; }
        }
    }
}
