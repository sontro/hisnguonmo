using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2.DataStore
{
    public class DataStore
    {
        internal static List<MOS.EFMODEL.DataModels.HIS_TRAN_PATI_FORM> TranPatiForms { get; set; }
        internal static List<MOS.EFMODEL.DataModels.HIS_ICD> Icds { get; set; }

        public static void LoadDataStore()
        {
            try
            {
                TranPatiForms = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_TRAN_PATI_FORM>();
                Icds = BackendDataWorker.Get<HIS_ICD>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
