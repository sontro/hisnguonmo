using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.Base
{
    class GlobalStore
    {
        public static List<MOS.EFMODEL.DataModels.HIS_ICD> HisIcds
        {
            get
            {
                return HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_ICD>().OrderByDescending(o => o.ICD_CODE).ToList();
            }
        }

        public static List<ACS.EFMODEL.DataModels.ACS_USER> HisAcsUser
        {
            get
            {
                return HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <ACS.EFMODEL.DataModels.ACS_USER>().Where(p => !string.IsNullOrEmpty(p.USERNAME) && p.IS_ACTIVE ==1).OrderBy(o => o.USERNAME).ToList();
            }
        }
    }
}
