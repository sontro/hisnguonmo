using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServExt
{
    class HisSereServExtUtil
    {
        public static void SetTdl(HIS_SERE_SERV_EXT ssExt, HIS_SERVICE_REQ serviceReq)
        {
            if (ssExt != null && serviceReq != null)
            {
                ssExt.TDL_SERVICE_REQ_ID = serviceReq.ID;
                ssExt.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
            }
        }

        public static void SetTdl(HIS_SERE_SERV_EXT ssExt, HIS_SERE_SERV sereServ)
        {
            if (ssExt != null && sereServ != null)
            {
                ssExt.SERE_SERV_ID = sereServ.ID;
                ssExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                ssExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;
            }
        }

        public static void Log(HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, HIS_SERE_SERV_EXT sereServExt)
        {
            try
            {
                if (sereServ != null && sereServExt != null)
                {
                    HIS_MACHINE machine = MOS.MANAGER.Config.HisMachineCFG.DATA.FirstOrDefault(o => o.ID == sereServExt.MACHINE_ID);
                    string machineName = sereServExt.MACHINE_CODE;
                    if (machine != null)
                    {
                        machineName = machine.MACHINE_CODE + " - " + machine.MACHINE_NAME;
                    }

                    new EventLogGenerator(EventLog.Enum.HisSereServExt_TraKetQua, sereServExt.SUBCLINICAL_RESULT_LOGINNAME, sereServExt.SUBCLINICAL_RESULT_USERNAME, sereServExt.SUBCLINICAL_NURSE_LOGINNAME, sereServExt.SUBCLINICAL_NURSE_USERNAME, sereServ.TDL_SERVICE_NAME, machineName, sereServExt.DESCRIPTION, sereServExt.CONCLUDE, sereServExt.NOTE).TreatmentCode(sereServ.TDL_TREATMENT_CODE).ServiceReqCode(sereServ.TDL_SERVICE_REQ_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

    }
}
