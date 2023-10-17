using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Truncate
{
    class HisServiceReqTruncateCheck : BusinessBase
    {
        internal HisServiceReqTruncateCheck()
            : base()
        {
        }

        internal HisServiceReqTruncateCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidSereServ(HIS_SERVICE_REQ raw, ref List<HIS_SERE_SERV> allSereServsOfTreatment, ref List<HIS_SERE_SERV> sereServsToDelete)
        {
            try
            {
                allSereServsOfTreatment = new HisSereServGet(param).GetByTreatmentId(raw.TREATMENT_ID);
                var deleteSereServs = allSereServsOfTreatment != null ? allSereServsOfTreatment.Where(o => o.SERVICE_REQ_ID == raw.ID).ToList() : null;
                var deleteSereServIds = deleteSereServs != null ? deleteSereServs.Select(o => o.ID).ToList() : null;
                if (IsNotNullOrEmpty(deleteSereServs))
                {
                    HisSereServCheck sereServCheck = new HisSereServCheck(param);
                    bool valid = true;
                    valid = valid && sereServCheck.IsUnLock(deleteSereServs);
                    valid = valid && sereServCheck.HasNoInvoice(deleteSereServs);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                    valid = valid && sereServCheck.HasNoBill(deleteSereServs);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                    valid = valid && sereServCheck.HasNoHeinApproval(deleteSereServs); //da duyet ho so Bao hiem thi ko cho phep sua
                    valid = valid && sereServCheck.HasNoDeposit(deleteSereServIds, false);
                    valid = valid && sereServCheck.HasNoDebt(deleteSereServIds);

                    if (valid)
                    {
                        sereServsToDelete = deleteSereServs;
                        return true;
                    }

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsValidPrescription(HIS_SERVICE_REQ data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                if (HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(data.SERVICE_REQ_TYPE_ID))
                {
                    expMest = new HisExpMestGet().GetByServiceReqId(data.ID);

                    if (expMest == null && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
                    {
                        var tmp = new HisExpMestGet().GetByPrescriptionId(data.ID);
                        expMest = IsNotNullOrEmpty(tmp) ? tmp[0] : null;
                    }
                    if (expMest != null)
                    {
                        if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatDonDaThucXuatKhongChoPhepXoa);
                            return false;
                        }

                        if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatDonDaDuyetKhongChoPhepXoa);
                            return false;
                        }

                        if (expMest.BILL_ID.HasValue)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatBanTuongUngDaDuocThanhToan, expMest.EXP_MEST_CODE);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsAllow(HisServiceReqSDO data, HIS_SERVICE_REQ raw)
        {
            bool valid = true;
            try
            {
                if (raw.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    string loginName = ResourceTokenManager.GetLoginName();
                    if (string.IsNullOrWhiteSpace(loginName) || (!loginName.Equals(raw.CREATOR) && !loginName.Equals(raw.REQUEST_LOGINNAME) && !HisEmployeeUtil.IsAdmin()))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDoNguoiKhacTaoKhongChoPhepXoa);
                        return false;
                    }
                }
                else
                {
                    WorkPlaceSDO workPlace = null;
                    if (!this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                        return false;
                    }
                    if (raw.REQUEST_DEPARTMENT_ID != workPlace.DepartmentId)
                    {
                        HIS_DEPARTMENT depart = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == raw.REQUEST_DEPARTMENT_ID);
                        string name = depart != null ? depart.DEPARTMENT_NAME : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_KhoaDangLamViecKhongPhaiKhoaChiDinh, name);
                        return false;
                    }
                }
                if (raw.RATION_SUM_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhSuatAnDaDuocDuyet);
                    return false;
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

        internal bool IsAllowStatusForDelete(HIS_SERVICE_REQ raw)
        {
            bool valid = true;
            try
            {
                //Neu voi cac chi dinh ko phai la don thuoc thi ko cho phep xoa neu don thuoc da bat dau
                if (!HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(raw.SERVICE_REQ_TYPE_ID) && raw.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiChuaBatDau);
                    return false;
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

        internal bool IsValidServiceFollow(HIS_SERVICE_REQ raw, List<HIS_SERE_SERV> allSereServsOfTreatment, ref List<HIS_SERVICE_REQ> serviceReqFollow, ref List<HIS_SERE_SERV> sereServsToDelete, ref List<HIS_EXP_MEST> listExpMest)
        {
            try
            {
                if (raw.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM)
                {
                    serviceReqFollow = new HisServiceReqGet().GetByParentId(raw.ID);
                    if (IsNotNullOrEmpty(serviceReqFollow))
                    {
                        foreach (var req in serviceReqFollow)
                        {
                            //Y lệnh DVKT đính kèm có trạng thái “Hoàn thành”, “Đang xử lý”
                            if (!IsAllowStatusForDelete(req))
                            {
                                return false;
                            }

                            // lấy ra đơn để xóa
                            HIS_EXP_MEST expMest = null;
                            if (!IsValidPrescription(req, ref expMest))
                            {
                                return false;
                            }

                            if (IsNotNull(expMest))
                            {
                                if (listExpMest == null)
                                {
                                    listExpMest = new List<HIS_EXP_MEST>();
                                }

                                listExpMest.Add(expMest);
                            }
                        }

                        //Y lệnh DVKT đính kèm có các dịch vụ đã được thanh toán hoặc tạm thu dịch vụ hoặc xuất hóa đơn hoặc chốt nợ
                        if (IsNotNullOrEmpty(allSereServsOfTreatment))
                        {
                            List<long> serviceReqIds = serviceReqFollow.Select(s => s.ID).ToList();
                            List<HIS_SERE_SERV> affectList = allSereServsOfTreatment.Where(o => serviceReqIds.Contains(o.SERVICE_REQ_ID ?? 0)).ToList();
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

                                if (!IsNotNullOrEmpty(sereServsToDelete))
                                {
                                    sereServsToDelete = new List<HIS_SERE_SERV>();
                                }

                                sereServsToDelete.AddRange(affectList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }

            return true;
        }
    }
}
