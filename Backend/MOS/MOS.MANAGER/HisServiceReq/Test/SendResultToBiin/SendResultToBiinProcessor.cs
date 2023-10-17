using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToBiin
{
    /// <summary>
    /// Gửi kết quả xét nghiệm sang hệ thống biin
    /// </summary>
    public class SendResultToBiinProcessor : BusinessBase
    {
        private static bool IS_SENDING = false;
        private const long SuccessCode = 0;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh xu ly gui ket qua xet nghiem sang he thong Biin dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_SENDING = true;

                if (BiinCFG.IS_AUTO_SEND_SEND_BIIN)//có cấu hình địa chỉ sẽ thực hiện gửi.
                {
                    List<HIS_SERVICE_REQ> successList = PrepareDataProcessor.Run();

                    if (successList != null && successList.Count > 0 && !new UpdateDataProcessor().Run(successList))
                    {
                        LogSystem.Error("Cap nhat trang thai da nhan SMS thong bao ket qua CLS that bai");
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
