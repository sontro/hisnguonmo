using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisRationTime;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class HisServiceReqRationUtil : BusinessBase
    {
        internal static void RationLog(object threadData)
        {
            try
            {
                if (threadData != null)
                {

                    RationThreadData data = (RationThreadData)threadData;
                    bool isHalfInFirstDay = data.IsHalfInFirstDay;
                    List<HIS_SERVICE_REQ> reqs = data.ServiceReqs;
                    List<HIS_SERE_SERV_RATION> rations = data.SereServRations;
                    List<HIS_RATION_TIME> rationTimes = new HisRationTimeGet().GetById(reqs.Where(o => o.RATION_TIME_ID.HasValue).Select(o => o.RATION_TIME_ID.Value).ToList());
                    foreach (var r in reqs)
                    {
                        HIS_RATION_TIME rTime = rationTimes.FirstOrDefault(o => o.ID == r.RATION_TIME_ID);
                        List<HIS_SERE_SERV_RATION> ssRations = rations.Where(o => o.SERVICE_REQ_ID == r.ID).ToList();
                        AssignRationData logData = new AssignRationData(
                            r.TDL_TREATMENT_CODE,
                            r.SERVICE_REQ_CODE,
                            rTime,
                            ssRations,
                            r.IS_FOR_HOMIE,
                            isHalfInFirstDay,
                            r.IS_FOR_AUTO_CREATE_RATION
                            );
                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_ChiDinhSuatAn).AssignRationData(logData).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void UpdateRationLog(object threadData)
        {
            try
            {
                if (threadData != null)
                {

                    UpdateRationThreadData data = (UpdateRationThreadData)threadData;
                    HIS_SERVICE_REQ req = data.ServiceReq;
                    List<HIS_SERE_SERV_RATION> currenRations = data.CurrentSereServRations;
                    List<HIS_SERE_SERV_RATION> oldRations = data.OldSereServRations;
                    HIS_RATION_TIME rTime = req.RATION_TIME_ID.HasValue ? new HisRationTimeGet().GetById(req.RATION_TIME_ID.Value) : new HIS_RATION_TIME();
                    UpdateRationData logData = new UpdateRationData(req.TDL_TREATMENT_CODE, req.SERVICE_REQ_CODE, rTime, currenRations, oldRations);
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_SuaChiDinhSuatAn).UpdateRationData(logData).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
