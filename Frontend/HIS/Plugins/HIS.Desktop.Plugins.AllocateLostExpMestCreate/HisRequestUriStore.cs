using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLostExpMestCreate
{
    class HisRequestUriStore
    {
        internal const string MOSHIS_MEDICINE_GET_IN_STOCK_MEDICINE = "/api/HisMedicine/GetInStockMedicine";
        internal const string MOSHIS_MATERIAL_GET_IN_STOCK_MATERIAL = "/api/HisMaterial/GetInStockMaterial";
        internal const string MOSHIS_LOST_EXP_MEST_CREATE = "/api/HisLostExpMest/Create";
        internal const string MOSHIS_LOST_EXP_MEST_GETVIEW = "/api/HisLostExpMest/GetView";

        public const string PRINT_TYPE_CODE__BIEUMAU__PHIEU_XUAT_MAT_MAT__MPS000168 = "Mps000168";
        public const string PRINT_TYPE_CODE__BIEUMAU__HOI_DONG_PHIEU_XUAT_MAT_MAT__MPS000205 = "Mps000205";
    }
}
