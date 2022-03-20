using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMailNFE
{
    class Common
    {

        public static String PrepareFileNameNFE(String nrNFE, String nomeCliente)
        {
            return "NFE_" + nrNFE + "_" + nomeCliente.Replace(".", "").Replace("/", "");
        }

        public static bool ValidRowToCheck(DataGridViewRow dr)
        {
            if (dr.Cells["IsProcessada"].Value.ToString().Equals("False") || dr.Cells["DANFEvalida"].Value.ToString().Equals(String.Empty))
                return false;
            else
                return true;
        }

    }
}
