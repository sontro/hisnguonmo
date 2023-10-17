using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidCreate.Base
{
    class GlobalConfig
    {
        public static int THUOC = 1;
        public static int VATTU = 2;
        public static int MAU = 3;
        public static string IsMedicine = "x";

        private static List<MOS.EFMODEL.DataModels.HIS_PACKING_TYPE> PackingType;
        public static List<MOS.EFMODEL.DataModels.HIS_PACKING_TYPE> HisPackingTypes
        {
            get
            {
                if (PackingType == null || PackingType.Count == 0)
                {
                    PackingType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_PACKING_TYPE>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return PackingType;
            }
            set
            {
                PackingType = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_SUPPLIER> Supplier;
        public static List<MOS.EFMODEL.DataModels.HIS_SUPPLIER> ListSupplier 
        {
            get
            {
                if (Supplier == null || Supplier.Count == 0)
                {
                    Supplier = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_SUPPLIER>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return Supplier;
            }
            set
            {
                Supplier = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_SERVICE_UNIT> ServiceUnit;
        public static List<MOS.EFMODEL.DataModels.HIS_SERVICE_UNIT> ListServiceUnit
        {
            get
            {
                if (ServiceUnit == null || ServiceUnit.Count == 0)
                {
                    ServiceUnit = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_SERVICE_UNIT>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return ServiceUnit;
            }
            set
            {
                ServiceUnit = value;
            }
        }
    }
}
