using HIS.Bhyt.Hssk;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MOS.MANAGER.HisBaby.Sync
{
    internal class HisBabySync : BusinessBase
    {
        internal HisBabySync()
            : base()
        {

        }

        internal HisBabySync(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<BabySyncSDO> data)
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
                        result = Run(item.BabyID, item.FileBase64Str) || result;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        internal bool Run(long id, string fileBase64Str)
        {
            bool result = false;
            try
            {
                V_HIS_BABY raw = new HisBabyGet().GetViewById(id);
                if (raw == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("BabyId is invalid");
                }
                HIS_BRANCH branch = new HisBranchGet().GetById(raw.BRANCH_ID.Value);
                if (branch == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("BRANCH_ID is invalid");
                }
                V_HIS_TREATMENT treatment = new HisTreatmentGet().GetViewById(raw.TREATMENT_ID);
                if (treatment == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("TREATMENT_ID is invalid");
                }

                long time = Inventec.Common.DateTime.Get.Now().Value;
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() ?? raw.MODIFIER ?? "";
                BabySyncInfo syncInfo = null;

                bool valid = true;
                HisBabySyncCheck checker = new HisBabySyncCheck(param);
                valid = valid && checker.IsHasConfig(branch.BRANCH_CODE, ref syncInfo);
                valid = valid && this.CheckCertificate(fileBase64Str, syncInfo);
                if (valid)
                {
                    SyncDataProcess process = new SyncDataProcess(BhytCFG.CHECK_HEIN_CARD_BHXH__ADDRESS, syncInfo.User, syncInfo.Password);
                    string mess = "";
                    if (!String.IsNullOrWhiteSpace(fileBase64Str))
                    {
                        result = process.SendBornInfo(syncInfo.Url, branch, fileBase64Str, ref mess);
                    }
                    else
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

                        result = process.SendBornInfo(syncInfo.Url, branch, raw, treatment, certificate, ref mess);
                    }

                    mess = CommonUtil.SubString(mess, 4000);

                    long resultType = IMSys.DbConfig.HIS_RS.HIS_BABY.SYNC_TYPE__NOT_SYNC;
                    if (result)
                    {
                        resultType = IMSys.DbConfig.HIS_RS.HIS_BABY.SYNC_TYPE__SYNC_SUCCESSFUL;
                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_BABY SET SYNC_RESULT_TYPE = :param1, SYNC_FAILD_REASON = :param2, SYNC_TIME = :param3, MODIFIER = :param4 WHERE ID = :param5", resultType, mess, time, loginname, raw.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_BABY faild. KskDriverId: " + raw.ID);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(mess))
                        {
                            resultType = IMSys.DbConfig.HIS_RS.HIS_BABY.SYNC_TYPE__SYNC_FAILED;
                            param.Messages.Add(string.Format("{0}: {1}", raw.TREATMENT_CODE, mess));
                        }

                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_BABY SET SYNC_RESULT_TYPE = :param1, SYNC_FAILD_REASON = :param1, SYNC_TIME = :param2, MODIFIER = :param3 WHERE ID = :param4", resultType, mess, time, loginname, raw.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_BABY faild. KskDriverId: " + raw.ID);
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

        private bool CheckCertificate(string fileBase64Str, BabySyncInfo syncInfo)
        {
            bool valid = true;
            try
            {
                if ((String.IsNullOrWhiteSpace(fileBase64Str) && (
                    String.IsNullOrWhiteSpace(syncInfo.CertificateLink)
                    || String.IsNullOrWhiteSpace(syncInfo.CertificatePass)
                    ))
                    )
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common_KhongCoThongTinKySo);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
