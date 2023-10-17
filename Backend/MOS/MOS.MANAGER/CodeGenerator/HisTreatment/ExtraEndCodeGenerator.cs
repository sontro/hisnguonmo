using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisTreatment
{
    class ExtraEndCodeGenerator
    {
        private static List<string> USED_EXTRA_END_CODES = new List<string>();
        private static Object thisLock = new Object();

        /// <summary>
        /// Mã lưu trữ có dạng: TTTT/XXX....XX. Trong đó:
        /// TTTTT: So thu tu tang dan
        /// XXX....XX : la "seed code", tuy theo ham su dung truyen vao
        /// </summary>
        /// <returns></returns>
        public static string GetNext(string seedCode)
        {
            string extraEndCode = null;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                extraEndCode = CodeGeneratorMasterService.ExtraEndCodeGetNext(seedCode, new CommonParam());
                return extraEndCode;
            }
            
            lock (thisLock)
            {
                string postfix = string.Format("/{0}", seedCode);

                string sql = "SELECT EXTRA_END_CODE FROM HIS_TREATMENT WHERE EXTRA_END_CODE_SEED_CODE = :param1 AND EXTRA_END_CODE IS NOT NULL AND IS_DELETE = 0 ";
                List<string> extraEndCodes = DAOWorker.SqlDAO.GetSql<string>(sql, seedCode);

                long useOrder = 0;

                List<string> releaseExtraEndCodes = new List<string>();
                if (extraEndCodes != null && extraEndCodes.Count > 0)
                {
                    releaseExtraEndCodes.AddRange(extraEndCodes);
                }
                List<string> usedExtraEndCodes = USED_EXTRA_END_CODES != null ? USED_EXTRA_END_CODES.Where(o => o.EndsWith(postfix)).ToList() : null;
                if (usedExtraEndCodes != null && usedExtraEndCodes.Count > 0)
                {
                    releaseExtraEndCodes.AddRange(usedExtraEndCodes);
                }

                List<long> releaseExtraEndCodeOrder = new List<long>();
                if (releaseExtraEndCodes != null && releaseExtraEndCodes.Count > 0)
                {
                    for (int i = 0; i < releaseExtraEndCodes.Count; i++)
                    {
                        try
                        {
                            string order = releaseExtraEndCodes[i].Substring(0, releaseExtraEndCodes[i].Length - postfix.Length);
                            long t = long.Parse(order);
                            releaseExtraEndCodeOrder.Add(t);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }
                    }
                }

                if (releaseExtraEndCodeOrder != null && releaseExtraEndCodeOrder.Count > 0)
                {
                    if (releaseExtraEndCodeOrder.Count == 1)
                    {
                        if (releaseExtraEndCodeOrder[0] > 1)
                        {
                            useOrder = 1;
                        }
                        else
                        {
                            useOrder = releaseExtraEndCodeOrder[0] + 1;
                        }
                    }
                    else
                    {
                        //Duyet de kiem tra tinh lien tuc cua day so
                        releaseExtraEndCodeOrder = releaseExtraEndCodeOrder.OrderBy(t => t).ToList();

                        for (int i = 1; i < releaseExtraEndCodeOrder.Count; i++)
                        {
                            //Neu dãy số bị khuyết (ko liên tục) thì sinh số mới để bù khuyết
                            if (releaseExtraEndCodeOrder[i] > releaseExtraEndCodeOrder[i - 1] + 1)
                            {
                                string tmp = string.Format("{0}{1}", (releaseExtraEndCodeOrder[i - 1] + 1).ToString("D5"), postfix);
                                if (releaseExtraEndCodes == null || !releaseExtraEndCodes.Contains(tmp))
                                {
                                    useOrder = releaseExtraEndCodeOrder[i - 1] + 1;
                                    break;
                                }
                            }
                            else if (i == releaseExtraEndCodes.Count - 1)
                            {
                                useOrder = releaseExtraEndCodeOrder[i] + 1;
                            }
                        }
                    }
                }
                else
                {
                    useOrder = 1;
                }

                extraEndCode = string.Format("{0}{1}", useOrder.ToString("D5"), postfix);

                //luu lai cac "in code" vua sinh ra
                //de check tranh viec sinh ra "in code" giong nhau trong truong hop tien trinh thu 2 goi vao
                //ham nay truoc khi tien trinh truoc kip insert vao DB
                USED_EXTRA_END_CODES.Add(extraEndCode);
                return extraEndCode;
            }
        }

        /// <summary>
        /// Xac nhan da update vao DB --> remove khoi danh sach USED_EXTRA_END_CODES
        /// </summary>
        public static bool FinishUpdateDB(string extraEndCode)
        {
            bool rs = false;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                rs = CodeGeneratorMasterService.ExtraEndCodeFinishUpdateDB(extraEndCode, new CommonParam());
                return rs;
            }

            lock (thisLock)
            {
                if (USED_EXTRA_END_CODES != null && !string.IsNullOrWhiteSpace(extraEndCode) && USED_EXTRA_END_CODES.Contains(extraEndCode))
                {
                    USED_EXTRA_END_CODES.Remove(extraEndCode);
                }
            }
            return true;
        }
    }
}
