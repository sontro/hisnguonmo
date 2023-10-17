using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ChmsImpMestList.Base
{
    class GlobalStore
    {
        private static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT> ImpMestStt;
        public static List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT> HisImpMestStts
        {
            get
            {
                if (ImpMestStt == null || ImpMestStt.Count == 0)
                {
                    ImpMestStt = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT>().OrderByDescending(o => o.CREATE_TIME).ToList();
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
                    ImpMestType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return ImpMestType;
            }
            set
            {
                ImpMestType = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK> MediStock;
        public static List<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK> ListMediStock
        {
            get
            {
                if (MediStock == null || MediStock.Count == 0)
                {
                    MediStock = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_MEDI_STOCK>().Where(o => o.IS_PAUSE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                }
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
                    MedicinePaty = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_PATY>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return MedicinePaty;
            }
            set
            {
                MedicinePaty = value;
            }
        }
    }
}
