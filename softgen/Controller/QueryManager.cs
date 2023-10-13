using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softgen
{
    public static class QueryManager
    {
        public static string getgrp_data = "SELECT * FROM m_group where group_id = 'CH';";
        public static string Addgrp_data = "INSERT INTO m_group (group_id, group_desc, active_yn,status,ent_on,ent_by,trans_status)";
        // Add more query strings here

        //////////////FOR ITEM MASTER-COMBO BOX DATA QUERIES//////////////////////////////////
        public static string getgrpcomb_data = "SELECT CONCAT(group_desc, ' id -( ', group_id,' )') AS namewithid, group_id, group_desc FROM m_group";
    }
}
