using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisTreatment
{
    class StoreCodeGenerator
    {
        private static Object thisLock = new Object();

        //Luu tru bien dem de sinh ma luu tru theo thu tu tang dan
        private static Dictionary<string, long> STORE_CODE_COUNT = new Dictionary<string, long>();

        private static List<string> USED_STORE_CODES = new List<string>();

        /// <summary>
        /// Sinh chung toan vien, theo cau truc XXXXXX/YY trong do:
        /// X: la so tu tang
        /// Y: la 2 chu so cuoi cua nam
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption1(long seedTime)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                return CodeGeneratorMasterService.StoreCodeGetNextOption1(seedTime, new CommonParam());
            }

            lock (thisLock)//lock de dam bao ko co 2 thread cung chay doan code nay cung 1 thoi diem
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;

                //base chinh la "/" + 2 so cuoi cua nam
                string baseStr = string.Format("/{0}", dt.Year.ToString().Substring(dt.Year.ToString().Length - 2, 2));
                long next = 0;
                
                if (STORE_CODE_COUNT.ContainsKey(baseStr))
                {
                    next = STORE_CODE_COUNT[baseStr] + 1;
                    STORE_CODE_COUNT[baseStr] = next; //set lai gia tri vao bien count
                }
                else
                {
                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.STORE_CODE__END_WITH = baseStr;
                    List<HIS_TREATMENT> treatments = new HisTreatmentGet().Get(filter);

                    long max = 0;
                    if (treatments != null && treatments.Count > 0)
                    {
                        foreach (HIS_TREATMENT t in treatments)
                        {
                            int index = 0;
                            try
                            {
                                index = int.Parse(t.STORE_CODE.Substring(0, t.STORE_CODE.Length - baseStr.Length));
                                max = index > max ? index : max;
                            }
                            catch (Exception ex)
                            {
                                LogSystem.Error(ex);
                            }
                        }
                    }

                    next = max + 1;

                    STORE_CODE_COUNT.Add(baseStr, next);
                }
                return string.Format("{0}{1}", next.ToString("D6"), baseStr);
            }
        }

        /// <summary>
        /// Sinh chung toan vien, theo cau truc PYYXXXXXX trong do:
        /// P: la ma cua doi tuong benh nhan, 
        /// Y: la 2 chu so cuoi cua nam
        /// X: la so tu tang
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption2(string patientTypeCode, long seedTime)
        {   
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                StoreCodeGenerateSDO data = new StoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.SeedCode = patientTypeCode;

                return CodeGeneratorMasterService.StoreCodeGetNextOption2(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;
                //base chinh la ma doi tuong + 2 so cuoi cua nam
                string baseStr = string.Format("{0}{1}", patientTypeCode, dt.Year.ToString().Substring(dt.Year.ToString().Length - 2, 2)); ;

                long next = 0;
                if (STORE_CODE_COUNT.ContainsKey(baseStr))
                {
                    next = STORE_CODE_COUNT[baseStr] + 1;
                    STORE_CODE_COUNT[baseStr] = next; //set lai gia tri vao bien count
                }
                else
                {
                    long max = 0;

                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.STORE_CODE__START_WITH = baseStr;
                    List<HIS_TREATMENT> treatments = new HisTreatmentGet().Get(filter);

                    if (treatments != null && treatments.Count > 0)
                    {
                        foreach (HIS_TREATMENT t in treatments)
                        {
                            int index = 0;
                            try
                            {
                                index = int.Parse(t.STORE_CODE.Substring(baseStr.Length));
                                max = index > max ? index : max;
                            }
                            catch (Exception ex)
                            {
                                LogSystem.Error(ex);
                            }
                        }
                    }
                    next = max + 1; 

                    STORE_CODE_COUNT.Add(baseStr, next);
                }

                return string.Format("{0}{1}", baseStr, next.ToString("D6"));
            }
        }

        /// <summary>
        /// Mã lưu trữ có dạng: KKKKKTTTTT/YY. Trong đó:
        /// KKKKK: Mã khoa kết thúc điều trị (hoặc mã kho lưu trữ) ( Tối đa 5 kí tự)
        /// TTTTT :Thứ tự lưu trữ của hồ sơ đó của khoa (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
        /// YY : Năm lưu trữ 
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption3_4(string placeCode, long seedTime)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                StoreCodeGenerateSDO data = new StoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.SeedCode = placeCode;

                return CodeGeneratorMasterService.StoreCodeGetNextOption34(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;
                string postfix = string.Format("/{0}", dt.Year.ToString().Substring(dt.Year.ToString().Length - 2));

                HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                filter.STORE_CODE__START_WITH = placeCode;
                filter.STORE_CODE__END_WITH = postfix;
                List<HIS_TREATMENT> treatments = new HisTreatmentGet().Get(filter);

                long useOrder = 0;

                List<string> releaseStoreCodes = new List<string>();
                if (treatments != null && treatments.Count > 0)
                {
                    releaseStoreCodes.AddRange(treatments.Select(o => o.STORE_CODE).ToList());
                }
                
                List<string> usedStoreCodes = USED_STORE_CODES != null ? USED_STORE_CODES.Where(o => o.StartsWith(placeCode) && o.EndsWith(postfix)).ToList() : null;
                if (usedStoreCodes != null && usedStoreCodes.Count > 0)
                {
                    releaseStoreCodes.AddRange(usedStoreCodes);
                }

                if (releaseStoreCodes != null && releaseStoreCodes.Count > 0)
                {
                    releaseStoreCodes = releaseStoreCodes.OrderBy(t => t).ToList();
                    long prev = 0;
                    for (int i = 0; i < releaseStoreCodes.Count; i++)
                    {
                        long current = 0;
                        try
                        {
                            string order = releaseStoreCodes[i].Substring(placeCode.Length, releaseStoreCodes[i].Length - placeCode.Length - postfix.Length);
                            current = long.Parse(order);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }

                        if (current > prev + 1)
                        {
                            string tmp = string.Format("{0}{1}{2}", placeCode, (prev + 1).ToString("D5"), postfix);
                            if (usedStoreCodes == null || !usedStoreCodes.Contains(tmp))
                            {
                                useOrder = prev + 1;
                                break;
                            }
                        }
                        else if (i == releaseStoreCodes.Count - 1)
                        {
                            useOrder = current + 1;
                        }
                        prev = current;
                    }
                }
                else
                {
                    useOrder = 1;
                }
                string storeCode = string.Format("{0}{1}{2}", placeCode, useOrder.ToString("D5"), postfix);
                USED_STORE_CODES.Add(storeCode);

                return storeCode;
            }
        }

        /// <summary>
        /// Xac nhan da update vao DB --> remove khoi danh sach USED_STORE_CODES
        /// </summary>
        public static bool FinishUpdateDB(List<string> storeCodes)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                return CodeGeneratorMasterService.StoreCodeFinishUpdateDB(storeCodes, new CommonParam());
            }

            lock (thisLock)
            {
                if (USED_STORE_CODES != null && storeCodes != null && storeCodes.Count > 0)
                {
                    foreach (string s in storeCodes)
                    {
                        if (USED_STORE_CODES != null && !string.IsNullOrWhiteSpace(s) && USED_STORE_CODES.Contains(s))
                        {
                            USED_STORE_CODES.Remove(s);
                        }
                    }
                }
            }
            return true;
        }
    }
}
