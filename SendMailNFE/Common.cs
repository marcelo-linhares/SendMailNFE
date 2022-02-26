using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailNFE
{
    class Common
    {

        public static String PrepareFileNameNFE(String nrNFE, String nomeCliente)
        {
            return "NFE_" + nrNFE + "_" + nomeCliente.Replace(".", "").Replace("/", "");
        }

    }
}
