using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i3nRiqJSON;
using System.Data;

namespace Comercial_Solutions.Clases
{

   
    class Cls_Inventario
    {
        i3nRiqJson db = new i3nRiqJson();

        public System.Collections.ArrayList Alterno(string query)
        {
            System.Collections.ArrayList xx = (db.consultar(query));

            return xx;
        } 




    }
}
