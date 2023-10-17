using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisServiceReq
{
    class BarcodeGenerator
    {
        private static List<string> USED_CODES = new List<string>();
        private static Object thisLock = new Object();

        /// <summary>
        /// DXXXX: trong do D la ngay trong tuan (2,3,4,5,6,7,8). 
        /// XXXX là stt tăng từ 1 - n trong ngày
        /// Do dai của XXXX se = gia tri cua key cau hinh MOS.HIS_SERVICE_REQ.LIS_SID_LENGTH tru cho 1
        /// </summary>
        /// <returns></returns>
        public static string GetNext(long virCreateDate)
        {
            string barcode = null;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                barcode = CodeGeneratorMasterService.BarcodeGetNext(virCreateDate, new CommonParam());
                return barcode;
            }

            lock (thisLock)
            {
                DateTime dateCreate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(virCreateDate) ?? DateTime.Now;

                int dayOfWeek = (int)dateCreate.DayOfWeek + 1;
                if (dayOfWeek == 1)
                {
                    dayOfWeek = 8;
                }
                string seedCode = dayOfWeek.ToString();
                long dateNow = long.Parse(dateCreate.ToString("yyyyMMdd") + "000000");

                //khong dung max duoc do sap xep string khac voi sap xep long
                string sql = "SELECT BARCODE FROM HIS_SERVICE_REQ WHERE VIR_CREATE_DATE = :param1 AND BARCODE LIKE :param2 AND SERVICE_REQ_TYPE_ID = :param3";
                List<string> listBarcodes = DAOWorker.SqlDAO.GetSql<string>(sql, dateNow, String.Format("{0}%",seedCode), IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN);

                long useOrder = 0;

                List<string> releaseBarcodes = new List<string>();
                if (listBarcodes != null && listBarcodes.Count > 0)
                {
                    releaseBarcodes.AddRange(listBarcodes);
                }

                if (USED_CODES != null && USED_CODES.Count > 0)
                {
                    //chi them so cung ngay
                    foreach (string s in USED_CODES)
                    {
                        if (!string.IsNullOrWhiteSpace(s) && s.StartsWith(seedCode))
                        {
                            releaseBarcodes.Add(s);
                        }
                    }
                }

                List<long> releaseBarcodeOrder = new List<long>();
                if (releaseBarcodes != null && releaseBarcodes.Count > 0)
                {
                    for (int i = 0; i < releaseBarcodes.Count; i++)
                    {
                        try
                        {
                            string order = releaseBarcodes[i].Substring(1, releaseBarcodes[i].Length - seedCode.Length);
                            long t = long.Parse(order);
                            releaseBarcodeOrder.Add(t);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }
                    }
                }

                //lay so lon nhat cong 1
                if (releaseBarcodeOrder != null && releaseBarcodeOrder.Count > 0)
                {
                    releaseBarcodeOrder = releaseBarcodeOrder.OrderByDescending(t => t).ToList();

                    useOrder = releaseBarcodeOrder[0] + 1;
                }
                else
                {
                    useOrder = 1;
                }

                string formatNumber = "D" + (HisServiceReqCFG.LIS_SID_LENGTH - 1);

                barcode = string.Format("{0}{1}", seedCode, useOrder.ToString(formatNumber));

                //luu lai cac "barcode" vua sinh ra
                //de check tranh viec sinh ra "barcode" giong nhau trong truong hop tien trinh thu 2 goi vao
                //ham nay truoc khi tien trinh truoc kip insert vao DB
                USED_CODES.Add(barcode);
                return barcode;
            }
        }

        /// <summary>
        /// Xac nhan da update vao DB --> remove khoi danh sach USED_IN_CODES
        /// </summary>
        public static bool FinishUpdateDB(List<string> barcodes)
        {
            bool rs = false;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                rs = CodeGeneratorMasterService.BarcodeFinishUpdateDB(barcodes, new CommonParam());
                return rs;
            }

            lock (thisLock)
            {
                if (USED_CODES != null && USED_CODES.Count > 0)
                {
                    foreach (string s in barcodes)
                    {
                        if (USED_CODES != null && !string.IsNullOrWhiteSpace(s) && USED_CODES.Contains(s))
                        {
                            USED_CODES.Remove(s);
                        }
                    }
                }
            }
            return true;
        }
    }
}
