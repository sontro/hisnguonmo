using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.SereServ
{
    class HisServiceReqUpdateSereServCheck : BusinessBase
    {
        internal HisServiceReqUpdateSereServCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateSereServCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsAllowed(HIS_SERVICE_REQ raw, List<HIS_SERE_SERV> olds, HisServiceReqUpdateSDO data)
        {
            bool valid = true;
            try
            {
                if (raw.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepCapNhatKhiChuaHoanThanh);
                    return false;
                }
                else if (raw.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL
                    && (HisServiceReqCFG.ALLOW_MODIFYING_OF_STARTED != HisServiceReqCFG.AllowModifyingStartedOption.ALL)
                    && (HisServiceReqCFG.ALLOW_MODIFYING_OF_STARTED != HisServiceReqCFG.AllowModifyingStartedOption.JUST_EXAM || raw.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepSuaPhieuChiDinhKhiDaBatDau);
                    return false;
                }

                //Khong cho phep sua cac phieu do nguoi khac tao ra
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (raw.REQUEST_LOGINNAME != loginName && raw.CREATOR != loginName && !HisEmployeeUtil.IsAdmin())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepSuaPhieuChiDinhCuaNguoiKhacTao);
                    return false;
                }

                List<HIS_SERE_SERV> affectList = new List<HIS_SERE_SERV>();
                //Neu co cau hinh khong cho sua y lenh neu da thanh toan
                if (HisServiceReqCFG.DO_NOT_ALLOW_TO_EDIT_IF_PAID && IsNotNullOrEmpty(olds))
                {
                    affectList.AddRange(olds);
                }
                //Neu co cap nhat hoac xoa dich vu thi kiem tra xem dv da tam ung hay thanh toan chua
                else if (IsNotNullOrEmpty(olds) && (IsNotNullOrEmpty(data.UpdateServices) || IsNotNullOrEmpty(data.DeleteSereServIds)))
                {
                    if (IsNotNullOrEmpty(data.DeleteSereServIds))
                    {
                        List<HIS_SERE_SERV> ss = olds.Where(o => data.DeleteSereServIds.Contains(o.ID)).ToList();
                        if (IsNotNullOrEmpty(ss))
                        {
                            affectList.AddRange(ss);
                        }
                    }
                    if (IsNotNullOrEmpty(data.UpdateServices))
                    {
                        List<HIS_SERE_SERV> ss = olds.Where(o => data.UpdateServices.Exists(t => t.SereServId == o.ID && (o.IS_OUT_PARENT_FEE != t.IsOutParentFee || o.IS_EXPEND != t.IsExpend || o.PATIENT_TYPE_ID != t.PatientTypeId || o.AMOUNT != t.Amount || o.PRIMARY_PATIENT_TYPE_ID != t.PrimaryPatientTypeId))).ToList();

                        if (IsNotNullOrEmpty(ss))
                        {
                            affectList.AddRange(ss);
                        }
                    }
                }

                if (IsNotNullOrEmpty(affectList))
                {
                    HisSereServCheck checker = new HisSereServCheck(param);
                    List<long> ssIds = affectList.Select(o => o.ID).ToList();
                    if (!checker.HasNoBill(affectList)
                        || !checker.HasNoInvoice(affectList)
                        || !checker.HasNoDebt(affectList)
                        || !checker.HasNoDeposit(ssIds, true))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidData(List<ServiceReqDetailSDO> updateServices)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(updateServices))
                {
                    if (updateServices.Exists(o => !o.SereServId.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("updateServices.SereServId invalid");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsValidAmount(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> exists, List<long> deleteIds, List<ServiceReqDetailSDO> inserts)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
                {
                    int remainCount = exists != null ? exists.Where(o => deleteIds == null || !deleteIds.Contains(o.ID)).Count() : 0;
                    int insertCount = inserts != null ? inserts.Count : 0;

                    if (remainCount + insertCount > 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhPhauThuatKhongDuocCoQua1DichVu);
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsValidDoNotUseBHYT(List<ServiceReqDetailSDO> serviceReqDetails)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(serviceReqDetails))
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisEmployeeUtil.IsAdmin(loginName))
                    {
                        var invalidDetails = serviceReqDetails.Where(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                        if (IsNotNullOrEmpty(invalidDetails))
                        {
                            List<V_HIS_SERVICE> services = HisServiceCFG.DO_NOT_USE_BHYT_DATA_VIEW.Where(o => invalidDetails.Exists(t => t.ServiceId == o.ID)).ToList();
                            if (IsNotNullOrEmpty(services))
                            {
                                List<string> lstValid = services.Select(o => string.Format("{0} - {1}", o.SERVICE_CODE, o.SERVICE_NAME)).ToList();
                                string strValid = string.Join(", ", lstValid);

                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDuocCauHinhKhongHuongBHYT, strValid);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }

        internal bool IsValidSampleTime(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                    && (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL || serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDaThucHienLayMau);
                    result = false;
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
