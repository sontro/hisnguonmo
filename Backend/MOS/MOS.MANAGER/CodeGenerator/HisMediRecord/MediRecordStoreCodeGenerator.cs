using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediRecord;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisMediRecord
{
    class MediRecordStoreCodeGenerator
    {
        private static Object thisLock = new Object();

        //Luu tru bien dem de sinh ma luu tru theo thu tu tang dan
        private static Dictionary<string, long> STORE_CODE_COUNT = new Dictionary<string, long>();

        private static List<string> USED_STORE_CODES = new List<string>();

        public static void AddKeepedStoreCode(List<string> keepedStoreCode)
        {
            if (keepedStoreCode != null && keepedStoreCode.Count > 0)
                USED_STORE_CODES.AddRange(keepedStoreCode);
        }

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
                    long year = dt.Year;

                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.STORE_CODE__END_WITH = baseStr;
                    filter.VIR_SEED_CODE_YEAR__EQUAL = year;
                    List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                    long max = 0;

                    if (mediRecords != null && mediRecords.Count > 0)
                    {
                        foreach (HIS_MEDI_RECORD t in mediRecords)
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
                MediRecordStoreCodeGenerateSDO data = new MediRecordStoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.SeedCode = patientTypeCode;

                return CodeGeneratorMasterService.MediRecordStoreCodeGetNextOption2(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;

                //base chinh la ma doi tuong + 2 so cuoi cua nam
                string baseStr = string.Format("{0}{1}", patientTypeCode, dt.Year.ToString().Substring(dt.Year.ToString().Length - 2, 2));

                long next = 0;
                if (STORE_CODE_COUNT.ContainsKey(baseStr))
                {
                    next = STORE_CODE_COUNT[baseStr] + 1;
                    STORE_CODE_COUNT[baseStr] = next; //set lai gia tri vao bien count
                }
                else
                {
                    long year = dt.Year;

                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.STORE_CODE__START_WITH = baseStr;
                    filter.VIR_SEED_CODE_YEAR__EQUAL = year;
                    List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                    long max = 0;

                    if (mediRecords != null && mediRecords.Count > 0)
                    {
                        foreach (HIS_MEDI_RECORD t in mediRecords)
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
        /// KKKKK: Mã khoa kết thúc điều trị
        /// TTTTT :Thứ tự lưu trữ của hồ sơ đó của khoa (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
        /// YY : Năm lưu trữ 
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption3(string placeCode, long seedTime)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                MediRecordStoreCodeGenerateSDO data = new MediRecordStoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.SeedCode = placeCode;

                return CodeGeneratorMasterService.MediRecordStoreCodeGetNextOption3(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;

                string postfix = string.Format("/{0}", dt.Year.ToString().Substring(dt.Year.ToString().Length - 2));
                long year = dt.Year;

                HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                filter.STORE_CODE__START_WITH = placeCode;
                filter.STORE_CODE__END_WITH = postfix;
                filter.VIR_SEED_CODE_YEAR__EQUAL = year;
                List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                long useOrder = 0;

                List<string> releaseStoreCodes = new List<string>();
                if (mediRecords != null && mediRecords.Count > 0)
                {
                    releaseStoreCodes.AddRange(mediRecords.Select(o => o.STORE_CODE).ToList());
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
        /// Mã lưu trữ có dạng: KKKKKTTTTT/YY. Trong đó:
        /// KKKKK: Mã mã kho lưu trữ (Tối đa 5 kí tự)
        /// TTTTT :Thứ tự lưu trữ của hồ sơ đó của khoa (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
        /// YY : Năm lưu trữ 
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption4(long dataStoreId, long seedTime)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                MediRecordStoreCodeGenerateSDO data = new MediRecordStoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.DataStoreId = dataStoreId;

                return CodeGeneratorMasterService.MediRecordStoreCodeGetNextOption4(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;

                string postfix = string.Format("/{0}", dt.Year.ToString().Substring(dt.Year.ToString().Length - 2));
                V_HIS_DATA_STORE dataStore = HisDataStoreCFG.DATA.Where(o => o.ID == dataStoreId).FirstOrDefault();

                if (dataStore == null)
                {
                    throw new Exception("Ko tim thay thong tin kho luu tru tuong ung voi ID (co the kho bi khoa) " + dataStoreId);
                }

                long year = dt.Year;

                HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                filter.STORE_CODE__START_WITH = dataStore.DATA_STORE_CODE;
                filter.STORE_CODE__END_WITH = postfix;
                filter.DATA_STORE_ID = dataStoreId;
                filter.VIR_SEED_CODE_YEAR__EQUAL = year;
                List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                long useOrder = 0;

                List<string> releaseStoreCodes = new List<string>();
                if (mediRecords != null && mediRecords.Count > 0)
                {
                    releaseStoreCodes.AddRange(mediRecords.Select(o => o.STORE_CODE).ToList());
                }

                List<string> usedStoreCodes = USED_STORE_CODES != null ? USED_STORE_CODES.Where(o => o.StartsWith(dataStore.DATA_STORE_CODE) && o.EndsWith(postfix)).ToList() : null;
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
                            string order = releaseStoreCodes[i].Substring(dataStore.DATA_STORE_CODE.Length, releaseStoreCodes[i].Length - dataStore.DATA_STORE_CODE.Length - postfix.Length);
                            current = long.Parse(order);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }

                        if (current > prev + 1)
                        {
                            string tmp = string.Format("{0}{1}{2}", dataStore.DATA_STORE_CODE, (prev + 1).ToString("D5"), postfix);
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
                string storeCode = string.Format("{0}{1}{2}", dataStore.DATA_STORE_CODE, useOrder.ToString("D5"), postfix);
                USED_STORE_CODES.Add(storeCode);

                return storeCode;
            }
        }

        /// <summary>
        /// Mã lưu trữ có dạng: KKKKK/TTTTT/YY. Trong đó:
        /// KKKKK: Mã mã kho lưu trữ (Tối đa 5 kí tự)
        /// TTTTT :Thứ tự lưu trữ của hồ sơ đó của khoa (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
        /// YY : Năm lưu trữ 
        /// </summary>
        /// <returns></returns>
        public static string GetNextOption5(long dataStoreId, long seedTime)
        {
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                MediRecordStoreCodeGenerateSDO data = new MediRecordStoreCodeGenerateSDO();
                data.SeedTime = seedTime;
                data.DataStoreId = dataStoreId;

                return CodeGeneratorMasterService.MediRecordStoreCodeGetNextOption5(data, new CommonParam());
            }

            lock (thisLock)
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(seedTime).Value;
                string postfix = string.Format("/{0}", dt.Year.ToString().Substring(dt.Year.ToString().Length - 2));
                V_HIS_DATA_STORE dataStore = HisDataStoreCFG.DATA.Where(o => o.ID == dataStoreId).FirstOrDefault();

                if (dataStore == null)
                {
                    throw new Exception("Ko tim thay thong tin kho luu tru tuong ung voi ID (co the kho bi khoa) " + dataStoreId);
                }

                string prefix = string.Format("{0}/", dataStore.DATA_STORE_CODE);

                long year = dt.Year;

                HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                filter.STORE_CODE__START_WITH = prefix;
                filter.STORE_CODE__END_WITH = postfix;
                filter.DATA_STORE_ID = dataStoreId;
                filter.VIR_SEED_CODE_YEAR__EQUAL = year;
                List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                long useOrder = 0;

                List<string> releaseStoreCodes = new List<string>();
                if (mediRecords != null && mediRecords.Count > 0)
                {
                    releaseStoreCodes.AddRange(mediRecords.Select(o => o.STORE_CODE).ToList());
                }

                List<string> usedStoreCodes = USED_STORE_CODES != null ? USED_STORE_CODES.Where(o => o.StartsWith(prefix) && o.EndsWith(postfix)).ToList() : null;
                if (usedStoreCodes != null && usedStoreCodes.Count > 0)
                {
                    releaseStoreCodes.AddRange(usedStoreCodes);
                }

                if (releaseStoreCodes != null && releaseStoreCodes.Count > 0)
                {
                    releaseStoreCodes = releaseStoreCodes.OrderBy(t => t).ToList();
                    long prev = 0;//129
                    for (int i = 0; i < releaseStoreCodes.Count; i++)
                    {
                        long current = 0;
                        try
                        {
                            string order = releaseStoreCodes[i].Substring(prefix.Length, releaseStoreCodes[i].Length - prefix.Length - postfix.Length);
                            current = long.Parse(order);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }

                        if (current > prev + 1)
                        {
                            string tmp = string.Format("{0}{1}{2}", prefix, (prev + 1).ToString("D5"), postfix);
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
                string storeCode = string.Format("{0}{1}{2}", prefix, useOrder.ToString("D5"), postfix);
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
                return CodeGeneratorMasterService.MediRecordStoreCodeFinishUpdateDB(storeCodes, new CommonParam());
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
