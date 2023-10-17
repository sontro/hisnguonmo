using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.Base
{
    class RequestUriStore
    {
        public const string HIS_MEDICINE_BEAN__RELEASEBEANALL = "api/HisMedicineBean/ReleaseAll";
        public const string HIS_MEDICINE_BEAN__TAKEBEANLIST = "api/HisMedicineBean/TakeList";
        public const string HIS_MEDICINE_BEAN__RELEASE = "api/HisMedicineBean/Release";
        
        public const string HIS_MATERIAL_BEAN__RELEASE = "api/HisMaterialBean/Release";
        public const string HIS_MATERIAL_BEAN__TAKEBEANLIST = "api/HisMaterialBean/TakeList";
        public const string HIS_MATERIAL_BEAN__RELEASEBEANALL = "api/HisMaterialBean/ReleaseAll";

        public const string HIS_EXP_MEST__SALE_CREATE = "api/HisExpMest/SaleCreate";
        public const string HIS_EXP_MEST__SALE_UPDATE = "api/HisExpMest/SaleUpdate";

        public const string HIS_MEDICINE_BEAN__GET = "api/HisMedicineBean/Get";
        public const string HIS_MATERIAL_BEAN__GET = "api/HisMaterialBean/Get";

        public const string HIS_EXP_MEST__SALE_CREATE_LIST = "api/HisExpMest/SaleCreateList";
        public const string HIS_EXP_MEST__SALE_UPDATE_LIST = "api/HisExpMest/SaleUpdateList";
    }
}
