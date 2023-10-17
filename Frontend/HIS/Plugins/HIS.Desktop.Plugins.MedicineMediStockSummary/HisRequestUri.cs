using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineMediStockSummary
{
    public static class HisRequestUri
    {
        public const string HIS_MEDICINE_TYPE_GETVIEW = "api/HisMedicineType/GetView";
        public static string HIS_MEDICINE_TYPE_CHANGE_LOCK = "api/HisMedicineType/ChangeLock";
        public static string HIS_MEDICINE_TYPE_DELETE = "api/HisMedicineType/Delete";
        public const string HIS_MEDISTOCK_GETVIEW = "api/HisMediStock/GetView";
    }
}
