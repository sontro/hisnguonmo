using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.Filter;

namespace HIS.Desktop.Plugins.HisImportMestMedicine.Base
{
    class GlobalStore
    {
        internal const string showButton = "HIS.Desktop.Plugins.ShowButtonExportList";

        private static List<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK> MediStock;
        public static List<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK> ListMediStock
        {
            get
            {

                MediStock = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK>().Where(o => o.IS_PAUSE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                return MediStock;
            }
            set
            {
                MediStock = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_PATY> MedicinePaty;
        public static List<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_PATY> ListMedicinePaty
        {
            get
            {
                if (MedicinePaty == null || MedicinePaty.Count == 0)
                {
                    MedicinePaty = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_PATY>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return MedicinePaty;
            }
            set
            {
                MedicinePaty = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT> ImpMestStt;
        public static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT> HisImpMestStts
        {
            get
            {
                if (ImpMestStt == null || ImpMestStt.Count == 0)
                {
                    ImpMestStt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return ImpMestStt;
            }
            set
            {
                ImpMestStt = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE> ImpMestType;
        public static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE> HisImpMestTypes
        {
            get
            {
                if (ImpMestType == null || ImpMestType.Count == 0)
                {
                    ImpMestType = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return ImpMestType;
            }
            set
            {
                ImpMestType = value;
            }
        }
    }
}
