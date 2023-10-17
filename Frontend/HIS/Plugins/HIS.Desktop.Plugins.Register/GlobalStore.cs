using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Register
{
    class GlobalStore
    {
        internal static List<long> PatientTypeIdAllows { get; set; }

        //private static Dictionary<long, List<V_HIS_SERVICE_PATY>> dicServicePaty;
        //internal static Dictionary<long, List<V_HIS_SERVICE_PATY>> DicServicePaty
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (dicServicePaty == null || dicServicePaty.Count == 0)
        //            {
        //                dicServicePaty = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY>()
        //                    .Where(t => PatientTypeIdAllows != null && PatientTypeIdAllows.Contains(t.PATIENT_TYPE_ID))
        //                    .GroupBy(o => o.SERVICE_ID)
        //                    .ToDictionary(o => o.Key, o => o.ToList());
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogSystem.Warn(ex);
        //        }

        //        return dicServicePaty;
        //    }
        //    set
        //    {
        //        dicServicePaty = value;
        //    }
        //}
    }
}
