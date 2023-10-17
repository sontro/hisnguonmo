using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AnticipateUpdate.Base
{
    class GlobalConfig
    {
        public static int THUOC = 1;
        public static int VATTU = 2;
        public static int MAU = 3;
        public static string IsMedicine = "x";

        private static List<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE> MedicineType;
        public static List<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE> HisMedicineTypes
        {
            get
            {
                if (MedicineType == null || MedicineType.Count == 0)
                {
                    MedicineType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE>().OrderBy(o => o.MEDICINE_TYPE_CODE).ToList();
                }
                return MedicineType;
            }
            set
            {
                MedicineType = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE> MaterialType;
        public static List<MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE> HisMaterialTypes
        {
            get
            {
                if (MaterialType == null || MaterialType.Count == 0)
                {
                    MaterialType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE>().OrderBy(o => o.MATERIAL_TYPE_CODE).ToList();
                }
                return MaterialType;
            }
            set
            {
                MaterialType = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.V_HIS_BLOOD_TYPE> BloodType;
        public static List<MOS.EFMODEL.DataModels.V_HIS_BLOOD_TYPE> HisBloodTypes
        {
            get
            {
                if (BloodType == null || BloodType.Count == 0)
                {
                    BloodType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.V_HIS_BLOOD_TYPE>().OrderBy(o => o.BLOOD_TYPE_CODE).ToList();
                }
                return BloodType;
            }
            set
            {
                BloodType = value;
            }
        }

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
