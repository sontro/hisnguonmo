using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineType
{
    public static class HisRequestUri
    {
        public const string HIS_MEDICINE_TYPE_GETVIEW = "api/HisMedicineType/GetView";
        public const string HIS_MEDICINE_TYPE_GetViewDynamic = "api/HisMedicineType/GetViewDynamic";
        public static string HIS_MEDICINE_TYPE_CHANGE_LOCK = "api/HisMedicineType/ChangeLock";
        public static string HIS_MEDICINE_TYPE_DELETE = "api/HisMedicineType/Delete";
    }
}
