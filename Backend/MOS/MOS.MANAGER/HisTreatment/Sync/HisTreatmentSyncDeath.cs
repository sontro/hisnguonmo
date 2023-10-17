using HIS.Bhyt.Hssk;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisBranch;
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

namespace MOS.MANAGER.HisTreatment.Sync
{
    class HisTreatmentSyncDeath : BusinessBase
    {
        internal HisTreatmentSyncDeath()
            : base()
        {

        }

        internal HisTreatmentSyncDeath(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<DeathSyncSDO> data)
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
                        result = Run(item) || result;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        internal bool Run(DeathSyncSDO data)
        {
            bool result = false;
            try
            {
                MOS.MANAGER.Config.HisTreatmentCFG.DeathSyncInfo syncInfo = null;

                HisBranchCheck branchChecker = new HisBranchCheck(param);
                HIS_BRANCH branch = null;
                bool valid = true;
                valid = valid && this.CheckData(data);
                valid = valid && branchChecker.VerifyId(data.TreatmentData.BRANCH_ID, ref branch);
                valid = valid && this.CheckConfig(branch.BRANCH_CODE, ref syncInfo);
                valid = valid && this.CheckCertificate(data, syncInfo);
                if (valid)
                {
                    long time = Inventec.Common.DateTime.Get.Now().Value;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() ?? data.TreatmentData.MODIFIER ?? "";

                    SyncDataProcess process = new SyncDataProcess(BhytCFG.CHECK_HEIN_CARD_BHXH__ADDRESS, syncInfo.User, syncInfo.Password);
                    string mess = "";
                    if (!String.IsNullOrWhiteSpace(data.FileBase64Str))
                    {
                        result = process.SendDeathInfo(syncInfo.Url, branch, data.FileBase64Str, ref mess);
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

                        result = process.SendDeathInfo(syncInfo.Url, branch, data.PatientData, data.TreatmentData, data.DeathData, certificate, ref mess);
                    }

                    mess = CommonUtil.SubString(mess, 4000);

                    long resultType = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.DEATH_SYNC_TYPE__NOT_SYNC;
                    if (result)
                    {
                        resultType = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.DEATH_SYNC_TYPE__SYNC_SUCCESSFUL;
                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET DEATH_SYNC_RESULT_TYPE = :param1, DEATH_SYNC_FAILD_REASON = :param2, DEATH_SYNC_TIME = :param3, MODIFIER = :param4 WHERE ID = :param5", resultType, mess, time, loginname, data.TreatmentData.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_TREATMENT faild. KskDriverId: " + data.TreatmentData.ID);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(mess))
                        {
                            resultType = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.DEATH_SYNC_TYPE__SYNC_FAILED;
                            param.Messages.Add(string.Format("{0}: {1}", data.TreatmentData.TREATMENT_CODE, mess));
                        }

                        if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET DEATH_SYNC_RESULT_TYPE = :param1, DEATH_SYNC_FAILD_REASON = :param1, DEATH_SYNC_TIME = :param2, MODIFIER = :param3 WHERE ID = :param4", resultType, mess, time, loginname, data.TreatmentData.ID))
                        {
                            throw new Exception("Update SYNC_RESULT_TYPE for HIS_TREATMENT faild. KskDriverId: " + data.TreatmentData.ID);
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

        private bool CheckCertificate(DeathSyncSDO data, HisTreatmentCFG.DeathSyncInfo syncInfo)
        {
            bool valid = true;
            try
            {
                if ((String.IsNullOrWhiteSpace(data.FileBase64Str) && (
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

        private bool CheckConfig(string heinMediOrgCode, ref Config.HisTreatmentCFG.DeathSyncInfo syncInfo)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(HisTreatmentCFG.DEATH_SYNC_INFO))
                {
                    syncInfo = HisTreatmentCFG.DEATH_SYNC_INFO.FirstOrDefault(o => o.HeinMediOrgCode == heinMediOrgCode);
                }

                if ((!IsNotNull(syncInfo)
                    || String.IsNullOrWhiteSpace(syncInfo.Url)
                    || String.IsNullOrWhiteSpace(syncInfo.User)
                    || String.IsNullOrWhiteSpace(syncInfo.Password))
                    )
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskDriver_KhongCoCauHinhKetNoiCongDuLieuYTe);
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

        private bool CheckData(DeathSyncSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.PatientData == null) throw new ArgumentNullException("data");
                if (data.TreatmentData == null) throw new ArgumentNullException("data");
                if (data.TreatmentData.PATIENT_ID != data.PatientData.ID)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    Inventec.Common.Logging.LogSystem.Error("TreatmentData.PATIENT_ID != PatientData.ID");
                }
                if (data.TreatmentData.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_LoaiKetThucDieuTriCuaHoSoKhongPhaiLaTuVong);
                    Inventec.Common.Logging.LogSystem.Error("TREATMENT_END_TYPE_ID khong phai tu vong");
                    valid = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
