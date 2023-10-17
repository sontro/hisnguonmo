using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ApiConsumer
{
    public partial class RequestUriStore
    {        
        public const string HIS_MEST_PERIOD_METY_GETVIEW = "/api/HisMestPeriodMety/GetView";
        public const string HIS_MEST_PERIOD_MATY_GETVIEW = "/api/HisMestPeriodMaty/GetView";
        public const string HIS_MEST_PERIOD_MEDI_GETVIEW = "/api/HisMestPeriodMedi/GetView";
        public const string HIS_MEST_PERIOD_MATE_GETVIEW = "/api/HisMestPeriodMate/GetView"; 
        public const string HIS_MEST_PERIOD_BLOOD_GETVIEW = "/api/HisMestPeriodBlood/Get";
        public const string HIS_MEST_STOCK_PERIOD_CREATE = "/api/HisMediStockPeriod/Create";
        public const string HIS_MEST_STOCK_PERIOD_UPDATE = "/api/HisMediStockPeriod/Update";
        public const string HIS_MEST_STOCK_PERIOD_INVENTORY = "/api/HisMediStockPeriod/UpdateInventory";
    }
}
