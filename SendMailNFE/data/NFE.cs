using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailNFE.Data
{
    public class NFE
    {

        public string codigo_nota_fiscal { get; set; }
        public string codigo_cliente { get; set; }
        public string nome_cliente { get; set; }
        public string email_cliente { get; set; }
        public string data_emissao { get; set; }
        public string codigo_NFE { get; set; }
        public bool indicador_NFE_processada { get; set; }
        public bool indicador_DANFE_PDF_valido { get; set; }
    }
}
