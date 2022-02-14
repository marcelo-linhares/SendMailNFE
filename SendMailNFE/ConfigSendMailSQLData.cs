using System;
using System.Collections.Generic;
using System.Text;

namespace SendMailNFE
{
    public class ConfigSendMailSQLData
    {
        private String _server;
        private String _dataBaseNF;
        private String _tableNF;
        private String _columnIdNF;
        private String _columnIdClienteNF;
        private String _dataBaseCliente;
        private String _tableCliente;
        private String _columnIdCliente;
        private String _columnTextCliente;
        private String _columnEmailCliente;

        public String Server 
        {
            get { return _server; }
            set { _server = value; }
        }

        public String DataBaseNF
        {
            get { return _dataBaseNF; }
            set { _dataBaseNF = value; }
        }

        public String TableNF
        {
            get { return _tableNF; }
            set { _tableNF = value; }
        }

        public String ColumnIdNF
        {
            get { return _columnIdNF; }
            set { _columnIdNF = value; }
        }

        public String ColumnIdClienteNF
        {
            get { return _columnIdClienteNF; }
            set { _columnIdClienteNF = value; }
        }

        public String DataBaseCliente
        {
            get { return _dataBaseCliente; }
            set { _dataBaseCliente = value; }
        }

        public String TableCliente
        {
            get { return _tableCliente; }
            set { _tableCliente = value; }
        }

        public String ColumnIdCliente
        {
            get { return _columnIdCliente; }
            set { _columnIdCliente = value; }
        }

        public String ColumnTextCliente
        {
            get { return _columnTextCliente; }
            set { _columnTextCliente = value; }
        }

        public String ColumnEmailCliente
        {
            get { return _columnEmailCliente; }
            set { _columnEmailCliente = value; }
        }
    }
}
