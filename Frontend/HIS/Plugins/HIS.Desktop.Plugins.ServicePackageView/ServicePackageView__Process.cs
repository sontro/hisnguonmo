using DevExpress.Utils;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServicePackageView
{
    public class ServicePackageView__Process
    {
        private static WaitDialogForm waitLoad = null;

        public static bool UpdateSereServ(MOS.EFMODEL.DataModels.HIS_SERE_SERV hisSereServ)
        {
            CommonParam param = new CommonParam();
            bool success = false;
            try
            {
                
                if (hisSereServ != null)
                {
                    //EXE.LOGIC.HisSereServ.HisSereServLogic hisSereServLogic = new LOGIC.HisSereServ.HisSereServLogic(param);
                    MOS.Filter.HisSereServFilter filter = new MOS.Filter.HisSereServFilter();
                    HIS_SERE_SERV hisSereServResult = new BackendAdapter(param)
                    .Post<MOS.EFMODEL.DataModels.HIS_SERE_SERV>(HisRequestUriStore.HIS_SERE_SERV_UPDATE, ApiConsumers.MosConsumer, hisSereServ, param);

                    if (hisSereServResult != null)
                    {
                        success = true;
                    }
                }
                #region Show message
                //.Show(SessionManager.GetFormMain(), param, success);
                //MessageManager.Show(this, param, success);
                #endregion

                #region Process has exception
                SessionManager.ProcessTokenLost(param);
                #endregion
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Fatal(ex);
            }
            return success;
        }
    }
}
