using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche;
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
    /// Phuc vu gui doc du lieu do LIS tra ve (duoi dang file)
    /// </summary>
    public class LisThreadReader : BusinessBase
    {
        private static bool IS_READING = false;

        public static void Run()
        {
            try
            {
                if (IS_READING)
                {
                    LogSystem.Warn("Tien trinh doc dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_READING = true;

                List<LisRocheFileAddress> address = new List<LisRocheFileAddress>();

                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7 || LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM)
                {
                    foreach (var item in LisRocheCFG.FILE_ADDRESSES)
                    {
                        Process(item, false);
                    }
                }

                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7 || LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.HL7)
                {
                    foreach (var item in LisRocheCFG.FILE_HL7_ADDRESSES)
                    {
                        Process(item, true);
                    }
                }

                IS_READING = false;
            }
            catch (Exception ex)
            {
                IS_READING = false;
                LogSystem.Error(ex);
            }
        }

        private static void Process(LisRocheFileAddress address, bool isHl7)
        {
            List<FileInfo> files = FileHandler.Read(address.Ip, address.User, address.Password, address.ReadFolder, address.FileHandlerType);
            //List<FileInfo> files = FileHandler.Read("192.168.1.98", "VietSens", "abc123", "Visinh");//de test
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (!String.IsNullOrEmpty(file.Data) && new LisRocheProcessor().UpdateResult(file.Data, isHl7))
                        {
                            FileHandler.Move(address.Ip, address.User, address.Password, file, "success", address.FileHandlerType);
                        }
                        else
                        {
                            FileHandler.Move(address.Ip, address.User, address.Password, file, "fail", address.FileHandlerType);
                        }
                    }
                }
            }
            else
            {
                LogSystem.Debug("Khong doc duoc file ket qua nao tu folder: " + address.ReadFolder);
            }
        }
    }
}