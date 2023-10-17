using Inventec.Common.Logging;
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
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread
{
    /// <summary>
    /// Phuc vu gui du lieu sang he thong LIS theo tien trinh
    /// </summary>
    public class LisThreadSender : BusinessBase
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

                List<OrderData> data = DataPrepare.Run();

                List<OrderData> sucessList = new List<OrderData>();
                if (data != null && data.Count > 0)
                {
                    ILisProcessor processor = LisFactory.GetProcessor(null);
                    if (processor != null)
                    {
                        foreach (OrderData sd in data)
                        {
                            List<string> messages = null;
                            if (processor.RequestOrder(sd, ref messages))
                            {
                                sucessList.Add(sd);
                            }
                            else
                            {
                                LogSystem.Warn("Gui yc XN sang he thong LIS that bai" + LogUtil.TraceData("OrderData:", sd));
                            }
                        }

                        if (sucessList != null && sucessList.Count > 0)
                        {
                            if (!new UpdateOrderStatus().UpdateSentOrder(sucessList))
                            {
                                LogSystem.Error("Cap nhat trang thai sau khi gui XN that bai");
                            }
                        }
                    }
                }

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
