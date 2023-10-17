using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public class MediOrgDataWorker
    {
        private static List<HIS.Desktop.LocalStorage.BackendData.ADO.MediOrgADO> mediOrgADOs;
        public static List<HIS.Desktop.LocalStorage.BackendData.ADO.MediOrgADO> MediOrgADOs
        {
            get
            {
                try
                {
                    if (mediOrgADOs == null)
                    {
                        var mediorgs = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_MEDI_ORG>();
                        mediOrgADOs = (from m in mediorgs select new HIS.Desktop.LocalStorage.BackendData.ADO.MediOrgADO(m)).ToList();
                    }
                    if (mediOrgADOs == null) mediOrgADOs = new List<HIS.Desktop.LocalStorage.BackendData.ADO.MediOrgADO>();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                }

                return mediOrgADOs;
            }
            set
            {
                mediOrgADOs = value;
            }
        }
    }
}
