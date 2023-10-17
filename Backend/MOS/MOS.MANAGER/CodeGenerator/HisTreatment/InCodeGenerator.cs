using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using Oracle.ManagedDataAccess.Client;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisTreatment
{
    class InCodeGenerator
    {
        private static List<string> USED_IN_CODES = new List<string>();
        private static Object thisLock = new Object();

        /// <summary>
        /// Mã lưu trữ có dạng: TTTT/XXX....XX. Trong đó:
        /// TTTTT: So thu tu tang dan
        /// XXX....XX : la "seed code", tuy theo ham su dung truyen vao
        /// </summary>
        /// <returns></returns>
        public static string GetNext(string seedCode)
        {
            string inCode = null;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                inCode = CodeGeneratorMasterService.InCodeGetNext(seedCode, new CommonParam());
                return inCode;
            }

            lock (thisLock)
            {
                string postfix = string.Format("/{0}", seedCode);

                string sql = "SELECT IN_CODE FROM HIS_TREATMENT WHERE IN_CODE_SEED_CODE = :param1 AND IN_CODE IS NOT NULL";
                List<string> incodes = DAOWorker.SqlDAO.GetSql<string>(sql, seedCode);

                long useOrder = 0;

                List<string> releaseInCodes = new List<string>();
                if (incodes != null && incodes.Count > 0)
                {
                    releaseInCodes.AddRange(incodes);
                }
                List<string> usedInCodes = USED_IN_CODES != null ? USED_IN_CODES.Where(o => o.EndsWith(postfix)).ToList() : null;
                if (usedInCodes != null && usedInCodes.Count > 0)
                {
                    releaseInCodes.AddRange(usedInCodes);
                }

                List<long> releaseInCodeOrder = new List<long>();
                if (releaseInCodes != null && releaseInCodes.Count > 0)
                {
                    for (int i = 0; i < releaseInCodes.Count; i++)
                    {
                        try
                        {
                            string order = releaseInCodes[i].Substring(0, releaseInCodes[i].Length - postfix.Length);
                            long t = long.Parse(order);
                            releaseInCodeOrder.Add(t);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }
                    }
                }

                if (releaseInCodeOrder != null && releaseInCodeOrder.Count > 0)
                {
                    if (releaseInCodeOrder.Count == 1)
                    {
                        if (releaseInCodeOrder[0] > 1)
                        {
                            useOrder = 1;
                        }
                        else
                        {
                            useOrder = releaseInCodeOrder[0] + 1;
                        }
                    }
                    else
                    {
                        //Duyet de kiem tra tinh lien tuc cua day so
                        releaseInCodeOrder = releaseInCodeOrder.OrderBy(t => t).ToList();

                        for (int i = 1; i < releaseInCodeOrder.Count; i++)
                        {
                            //Neu dãy số bị khuyết (ko liên tục) thì sinh số mới để bù khuyết
                            if (releaseInCodeOrder[i] > releaseInCodeOrder[i - 1] + 1)
                            {
                                string tmp = string.Format("{0}{1}", (releaseInCodeOrder[i - 1] + 1).ToString("D5"), postfix);
                                if (releaseInCodes == null || !releaseInCodes.Contains(tmp))
                                {
                                    useOrder = releaseInCodeOrder[i - 1] + 1;
                                    break;
                                }
                            }
                            else if (i == releaseInCodes.Count - 1)
                            {
                                useOrder = releaseInCodeOrder[i] + 1;
                            }
                        }
                    }
                }
                else
                {
                    useOrder = 1;
                }

                inCode = string.Format("{0}{1}", useOrder.ToString("D5"), postfix);

                //luu lai cac "in code" vua sinh ra
                //de check tranh viec sinh ra "in code" giong nhau trong truong hop tien trinh thu 2 goi vao
                //ham nay truoc khi tien trinh truoc kip insert vao DB
                USED_IN_CODES.Add(inCode);
                return inCode;
            }
        }

        /// <summary>
        /// Xac nhan da update vao DB --> remove khoi danh sach USED_IN_CODES
        /// </summary>
        public static bool FinishUpdateDB(string inCode)
        {
            bool rs = false;
            //Neu la MOS "slave" thi goi den MOS "master" de sinh code
            if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
            {
                rs = CodeGeneratorMasterService.InCodeFinishUpdateDB(inCode, new CommonParam());
                return rs;
            }

            lock (thisLock)
            {
                if (USED_IN_CODES != null && !string.IsNullOrWhiteSpace(inCode))
                {
                    USED_IN_CODES.Remove(inCode);
                }
            }
            return true;
        }

        internal static string GetInCode(string seedCode)
        {
            string result = null;
            try
            {
                string storedSql = "PKG_TREATMENT_IN_CODE.PRO_GET";
                OracleParameter seedCodePar = new OracleParameter("P_IN_CODE_SEED_CODE", OracleDbType.Varchar2, 300);
                seedCodePar.Direction = ParameterDirection.Input;
                seedCodePar.Value = seedCode;

                OracleParameter resultPar = new OracleParameter("P_IN_CODE", OracleDbType.Varchar2, 1000);
                resultPar.Direction = ParameterDirection.Output;

                object resultHolder = null;

                if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, resultPar, seedCodePar))
                {
                    result = (string)resultHolder;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private static void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                if (parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    resultHolder = !Constant.DB_NULL_STR.Equals(parameters[0].Value.ToString()) ? parameters[0].Value.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
