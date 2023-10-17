using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Security.Cryptography.Xml;
using MOS.SDO;
using MOS.KskSignData;

namespace MOS.MANAGER.HisKskDriver.Sync
{
    internal class HisKskDriverSync : BusinessBase
    {
        internal HisKskDriverSync()
            : base()
        {

        }

        internal HisKskDriverSync(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<KskDriverSyncSDO> data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    foreach (var item in data)
                    {
                        result = Run(item.KskDriveId, item.SyncData) || result;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        internal bool Run(long id, KskSyncSDO sysData)
        {
            bool result = false;
            try
            {
                V_HIS_KSK_DRIVER raw = new HisKskDriverGet().GetViewById(id);
                if (raw == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("KskDriverId is invalid");
                }

                long time = Inventec.Common.DateTime.Get.Now().Value;
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() ?? raw.MODIFIER ?? "";
                KskDriverSyncInfo syncInfo = null;

                bool valid = true;
                HisKskDriverSyncCheck checker = new HisKskDriverSyncCheck(param);
                valid = valid && checker.IsHasCmnd(raw);
                valid = valid && checker.IsHasTHX(raw);
                valid = valid && checker.IsHasConfig(raw.BRANCH_CODE, ref syncInfo);

                if (valid)
                {
                    ResponseSyncSDO res = this.CallGateway(raw, sysData, syncInfo);
                    long resultType = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__SYNC_SUCCESSFUL;
                    string mess = "";
                    if (res == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.CoLoiXayRa);
                        mess = param.GetBugCode();
                        resultType = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__SYNC_FAILED;
                    }
                    else if (res.MSG_STATE != "1")
                    {
                        param.Messages.Add(String.Format("{0} - {1}", raw.KSK_DRIVER_CODE, res.MSG_TEXT));
                        mess = res.MSG_TEXT;
                        resultType = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__SYNC_FAILED;
                    }
                    else
                    {
                        result = true;

                    }

                    mess = CommonUtil.SubString(mess, 4000);
                    if (result)
                    {
                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_KSK_DRIVER SET SYNC_RESULT_TYPE = :param1, SYNC_FAILD_REASON = :param2, SYNC_TIME = :param3, MODIFIER = :param4 WHERE ID = :param5", resultType, mess, time, loginname, raw.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_KSK_DRIVER faild. KskDriverId: " + raw.ID);
                        }
                    }
                    else if (raw.SYNC_RESULT_TYPE == IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__NOT_SYNC)
                    {
                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_KSK_DRIVER SET SYNC_RESULT_TYPE = :param1, SYNC_FAILD_REASON = :param2, SYNC_TIME = :param3, MODIFIER = :param4 WHERE ID = :param5", resultType, mess, time, loginname, raw.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_KSK_DRIVER faild. KskDriverId: " + raw.ID);
                        }
                    }
                    else
                    {
                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_KSK_DRIVER SET SYNC_FAILD_REASON = :param1, SYNC_TIME = :param2, MODIFIER = :param3 WHERE ID = :param4", mess, time, loginname, raw.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_KSK_DRIVER faild. KskDriverId: " + raw.ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private ResponseSyncSDO CallGateway(V_HIS_KSK_DRIVER raw, KskSyncSDO sysData, KskDriverSyncInfo syncInfo)
        {
            ResponseSyncSDO result = null;
            try
            {
                KskSyncSDO ado = new KskSyncSDO();
                #region syncData
                if (!IsNotNull(sysData))
                {
                    X509Certificate2 certificate = null;
                    string fileName = Path.Combine(HttpRuntime.AppDomainAppPath, syncInfo.CertificateLink);
                    if (File.Exists(fileName))
                    {
                        certificate = new X509Certificate2(fileName, syncInfo.CertificatePass);
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Error("Khong ton tai file chung thu theo duong dan: " + fileName);
                    }

                    ado = new KskDataProcess().MakeData(raw, certificate);
                }
                else
                {
                    ado = sysData;
                }
                #endregion

                LogSystem.Info(LogUtil.TraceData("SyncKskDriver", ado));
                string address = syncInfo.Url.Substring(0, syncInfo.Url.IndexOf("/", 8) + 1);
                string requestUri = syncInfo.Url.Replace(address, "");

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Add("Username", syncInfo.User);
                    client.DefaultRequestHeaders.Add("Password", syncInfo.Password);
                    HttpResponseMessage resp = client.PostAsJsonAsync(requestUri, ado).Result;
                    if (!resp.IsSuccessStatusCode)
                    {
                        LogSystem.Error(string.Format("Loi khi goi API: {0}. StatusCode: {1}. Input: \n{2}.", syncInfo.Url, resp.StatusCode.GetHashCode(), Newtonsoft.Json.JsonConvert.SerializeObject(ado, Newtonsoft.Json.Formatting.Indented)));
                    }
                    string responseData = "";
                    try
                    {
                        responseData = resp.Content.ReadAsStringAsync().Result;
                        LogSystem.Debug("responseData: " + responseData);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseSyncSDO>(responseData);
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error("responseData: " + responseData);
                        LogSystem.Error("Input: \n" + Newtonsoft.Json.JsonConvert.SerializeObject(ado, Newtonsoft.Json.Formatting.Indented));
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
