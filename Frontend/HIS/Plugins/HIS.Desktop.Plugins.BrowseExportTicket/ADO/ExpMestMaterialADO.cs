using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BrowseExportTicket.ADO
{
    public class ExpMestMaterialADO : V_HIS_EXP_MEST_MATY_REQ
    {
        public int Action { get; set; }
        public long? ID_GRID { get; set; }
        public decimal? SUM_IN_STOCK { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public bool isCheckMaterial { get; set; }
        public decimal YCD_AMOUNT { get; set; }
        public bool IS_ALLOW_EXPORT_ODD { get; set; }
        public bool IsReplace { get; set; }
        public long? REPLACE_MATERIAL_TYPE_ID { get; set; }
        public string REPLACE_MATERIAL_TYPE_NAME { get; set; }
        public short? IS_REUSABLE { get; set; }
        public decimal CURRENT_DD_AMOUNT { get; set; }
        public decimal CURRENT_YC_AMOUNT { get; set; }
        public bool IsApproved { get; set; }
        public decimal? TT_AMOUNT { get; set; }
        public decimal TON_KHO { get; set; }
        public long? MATERIAL_ID { get; set; }
    }
}
