using ACS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintServiceReq
{
    internal class ProcessDictionaryData
    {
        private static Dictionary<string, ACS_USER> dicAcsUserMobile { get; set; }
        private static Dictionary<long, V_HIS_SERVICE> dicService { get; set; }

        internal static ACS_USER GetUserMobile(string loginname)
        {
            ACS_USER result = null;

            if (dicAcsUserMobile == null)
            {
                var acsUserMobiles = BackendDataWorker.Get<ACS.EFMODEL.DataModels.ACS_USER>().Where(o => !String.IsNullOrWhiteSpace(o.MOBILE)).ToList();
                if (acsUserMobiles != null && acsUserMobiles.Count > 0)
                {
                    dicAcsUserMobile = acsUserMobiles.ToDictionary(o => o.LOGINNAME, o => o);
                }
            }

            if (dicAcsUserMobile != null && dicAcsUserMobile.ContainsKey(loginname))
            {
                result = dicAcsUserMobile[loginname];
            }

            return result;
        }

        internal static void Reload()
        {
            try
            {
                var acsUserMobiles = BackendDataWorker.Get<ACS.EFMODEL.DataModels.ACS_USER>().Where(o => !String.IsNullOrWhiteSpace(o.MOBILE)).ToList();
                if (acsUserMobiles != null && acsUserMobiles.Count > 0)
                {
                    dicAcsUserMobile = acsUserMobiles.ToDictionary(o => o.LOGINNAME, o => o);
                }

                dicService = BackendDataWorker.Get<V_HIS_SERVICE>().ToDictionary(o => o.ID, o => o);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static V_HIS_SERVICE GetService(long id)
        {
            if (dicService == null)
            {
                dicService = BackendDataWorker.Get<V_HIS_SERVICE>().ToDictionary(o => o.ID, o => o);
            }

            if (dicService.ContainsKey(id))
            {
                return dicService[id];
            }

            return null;
        }
    }
}
