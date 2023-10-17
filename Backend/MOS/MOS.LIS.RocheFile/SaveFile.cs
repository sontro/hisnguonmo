using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheFile
{
    public class SaveFile
    {
        public bool Save(string data, string serviceReqCode, string saveFolder)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(data) && !String.IsNullOrEmpty(serviceReqCode) && !String.IsNullOrEmpty(saveFolder))
                {
                    string pathName = Path.Combine(saveFolder, "Order_" + serviceReqCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".dat");

                    if (File.Exists(pathName)) File.Delete(pathName);//đã tồn tại file thì xóa

                    using (StreamWriter sw = new StreamWriter(pathName))
                    {
                        sw.WriteLine(data);
                    }

                    result = File.Exists(pathName);
                    if (!result) LogSystem.Warn("Khong luu duoc file: data: " + data + "; serviceReqCode: " + serviceReqCode + "; saveFolder: " + saveFolder);
                }
                else
                {
                    LogSystem.Warn("Du lieu truyen vao khong hop le, Luu file that bai: data: " + data + "; serviceReqCode: " + serviceReqCode + "; saveFolder: " + saveFolder);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
