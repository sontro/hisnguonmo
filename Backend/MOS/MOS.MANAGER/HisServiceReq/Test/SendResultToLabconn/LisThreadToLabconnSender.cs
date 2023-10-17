using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using MOS.ApiConsumerManager;
using MOS.UTILITY;
using MOS.TDO;
using MOS.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.Config.CFG;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn
{
    class LisThreadToLabconnSender : BusinessBase
    {
        private static bool IS_SENDING = false;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_SENDING = true;

                List<long> executeRoomIds = DataPrepare.GetExecuteRoomId();
                LogSystem.Info(LogUtil.TraceData("Data executeRoomIds query by exe_room_code send to Labconn:", executeRoomIds));
                if (executeRoomIds != null && executeRoomIds.Count > 0)
                {
                    // Get serviceReq data
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.EXECUTE_ROOM_IDs = executeRoomIds;
                    filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    filter.INTRUCTION_DATE_FROM = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                    filter.IS_NO_EXECUTE = false;
                    filter.IS_NOT_SENT__OR__UPDATED = true;
                    filter.IS_INCLUDE_DELETED = true; // Include deleted data
                    if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
                    {
                        long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                        filter.INTRUCTION_TIME_TO = thoiGianHtai;
                    }

                    var serviceReqs = new HisServiceReqGet().Get(filter);
                    if (serviceReqs != null && serviceReqs.Count > 0)
                    {
                        if (!new LisLabconnProcessor().ProcessDataToLabconn(serviceReqs))
                        {
                            LogSystem.Info("ProcessDataToLabconn that bai");
                        }
                    }
                }
                LogSystem.Info("Ketthuc SenttoLabconn");
                IS_SENDING = false;
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                LogSystem.Error(ex);
            }
        }
    }
}
