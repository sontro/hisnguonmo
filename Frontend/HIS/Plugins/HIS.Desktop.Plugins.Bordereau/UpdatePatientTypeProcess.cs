using DevExpress.Utils;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Common.Adapter;
using Inventec.Common.RichEditor.Base;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Bordereau
{
    public class UpdatePatientTypeProcess
    {

        public static bool UpdatePatientType(MOS.EFMODEL.DataModels.HIS_SERE_SERV hisSereServ,ref CommonParam param)
        {
            bool success = false;
            try
            {
                WaitingManager.Show();
                if (hisSereServ != null)
                {
                    HisSereServFilter filter = new HisSereServFilter();
                    hisSereServ = new BackendAdapter(param).Post<MOS.EFMODEL.DataModels.HIS_SERE_SERV>(HisRequestUriStore.HIS_SERE_SERV_UPDATE, ApiConsumers.MosConsumer, hisSereServ, param);
                    if (hisSereServ != null)
                    {
                        success = true;
                    }
                }

                WaitingManager.Hide();

                //#region Show message
                //MessageManager.Show(this, param, success);
                //#endregion
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
