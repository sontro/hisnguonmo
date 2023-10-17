using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsBackKhoa;
using MOS.MANAGER.HisServiceReq.Pacs.PacsThread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    public class PacsSwitch
    {
        public static void Reader()
        {
            try
            {
                if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_SANCY)
                {
                    PacsThreadReader.Run();
                }
                else if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_BACH_KHOA)
                {
                    PacsThreadReader.Run();
                    //Đọc kết quả xong sẽ gửi kết quả được tạo trên his
                    new PacsBackKhoaSendResultProcessor().SendResult();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
