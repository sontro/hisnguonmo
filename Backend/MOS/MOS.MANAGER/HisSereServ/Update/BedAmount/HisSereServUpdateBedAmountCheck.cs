using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.BedAmount
{
    class HisSereServUpdateBedAmountCheck : BusinessBase
    {
        public HisSereServUpdateBedAmountCheck()
            : base()
        {
        }

        public HisSereServUpdateBedAmountCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(UpdateBedAmountSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.SereServId)) throw new ArgumentNullException("data.SereServId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
                if (data.Amount < 0) throw new ArgumentNullException("data.Amount");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        public bool IsValidData(UpdateBedAmountSDO data, HIS_SERE_SERV sereServ, WorkPlaceSDO workPlace, ref HIS_SERVICE_REQ serviceReq, ref HIS_TREATMENT treatment, ref HIS_BED_LOG bedLog)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(sereServ))
                {
                    if (sereServ.SERVICE_REQ_ID.HasValue)
                    {
                        serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);
                    }
                    if (IsNotNull(serviceReq))
                    {
                        treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);

                        if (serviceReq.BED_LOG_ID.HasValue)
                        {
                            bedLog = new HisBedLogGet().GetById(serviceReq.BED_LOG_ID.Value);
                        }
                    }
                    if (serviceReq == null || treatment == null || bedLog == null)
                    {
                        LogSystem.Error("Khong lay duoc thong tin y lenh hay ho so dieu tri");
                        return false;
                    }

                    // Kiem tra khoa chi dinh cua dich vu
                    if (sereServ.TDL_REQUEST_DEPARTMENT_ID != workPlace.DepartmentId)
                    {
                        HIS_DEPARTMENT depart = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == sereServ.TDL_REQUEST_DEPARTMENT_ID);
                        string name = depart != null ? depart.DEPARTMENT_NAME : "";
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_KhoaDangLamViecKhongPhaiKhoaChiDinh, name);
                        return false;
                    }

                    // Kiem tra thong tin ket thuc cua lich su giuong
                    if (!bedLog.FINISH_TIME.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_LichSuGiuongKhongCoThongTinThoiGianKetThuc);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }

            return valid;
        }

        public bool IsValidDataForTempAmount(UpdateBedAmountSDO data, HIS_SERE_SERV sereServ, WorkPlaceSDO workPlace, ref HIS_SERVICE_REQ serviceReq, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(sereServ))
                {
                    if (sereServ.SERVICE_REQ_ID.HasValue)
                    {
                        serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);
                    }
                    if (IsNotNull(serviceReq))
                    {
                        treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                    }
                    if (serviceReq == null || treatment == null)
                    {
                        LogSystem.Error("Khong lay duoc thong tin y lenh hay ho so dieu tri");
                        return false;
                    }

                    // Kiem tra khoa chi dinh cua dich vu
                    if (sereServ.TDL_REQUEST_DEPARTMENT_ID != workPlace.DepartmentId)
                    {
                        HIS_DEPARTMENT depart = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == sereServ.TDL_REQUEST_DEPARTMENT_ID);
                        string name = depart != null ? depart.DEPARTMENT_NAME : "";
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_KhoaDangLamViecKhongPhaiKhoaChiDinh, name);
                        return false;
                    }

                    // Kiem tra thong tin ket thuc cua lich su giuong
                    if (!sereServ.AMOUNT_TEMP.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_KhongCoThongTinSoLuongTamTinhHoacSoLuongTamTinhDaDuocChot);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }

            return valid;
        }
    }
}
