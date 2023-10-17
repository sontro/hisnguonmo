using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisServiceReqBloodPresCheck : BusinessBase
    {
        internal HisServiceReqBloodPresCheck()
            : base()
        {

        }

        internal HisServiceReqBloodPresCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra du lieu dau vao
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifydData(PatientBloodPresSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TreatmentId <= 0) throw new ArgumentNullException("data.TreatmentId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId");
                if (data.InstructionTime <= 0) throw new ArgumentNullException("data.InstructionTime");
                if (!IsNotNullOrEmpty(data.ExpMestBltyReqs)) throw new ArgumentNullException("data.ExpMestBltyReqs null");
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

        /// <summary>
        /// Kiem tra kho xuat, phong yeu cau
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStock"></param>
        /// <returns></returns>
        internal bool ValidMediStockId(PatientBloodPresSDO data, ref V_HIS_MEDI_STOCK mediStock)
        {
            try
            {
                //Kiem tra phong y/c co hop le khong
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }

                mediStock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.MediStockId);
                if (mediStock == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("MediStockId Invalid");
                    return false;
                }
                //Kiem tra xem kho co nam trong d/s cac kho duoc cau hinh cho phep xuat tu phong dang lam viec hay khong

                if (HisMestRoomCFG.DATA == null || !HisMestRoomCFG.DATA.Exists(t => t.MEDI_STOCK_ID == data.MediStockId && t.ROOM_ID == workPlace.RoomId && t.IS_ACTIVE == Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoSauKhongChoPhepXuatDenPhongDangLamViec, mediStock.MEDI_STOCK_NAME);
                    return false;
                }

                //Kiem tra trong cac kho ke thuoc co kho nao bi khoa hay khong

                if (mediStock.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStock.MEDI_STOCK_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiem tra tinh chinh xac cua du lieu 
        /// HIS_EXP_MEST_BLTY_REQ dau vao
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ValidData(PatientBloodPresSDO data)
        {
            bool valid = true;
            try
            {
                if (data.ExpMestBltyReqs.Exists(o => !o.PATIENT_TYPE_ID.HasValue))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Ton tai HIS_EXP_MEST_BLTY_REQ khong co PatientTypeId");
                    return false;
                }
                if (data.ExpMestBltyReqs.Exists(o => o.BLOOD_TYPE_ID <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai HIS_EXP_MEST_BLTY_REQ co BLOOD_TYPE_ID khong hop le");
                    return false;
                }

                if (data.ExpMestBltyReqs.Exists(o => o.AMOUNT <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai HIS_EXP_MEST_BLTY_REQ co AMOUNT <= 0.");
                    return false;
                }

                var reqs = data.ExpMestBltyReqs.Select(s => new { s.BLOOD_TYPE_ID, s.PATIENT_TYPE_ID, s.IS_OUT_PARENT_FEE, s.SERE_SERV_PARENT_ID }).Distinct().ToList();
                if (reqs.Count != data.ExpMestBltyReqs.Count)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai 2 dong trung nhau");
                    return false;
                }

                string loginname = (!String.IsNullOrWhiteSpace(data.RequestLoginName)) ? data.RequestLoginName : ResourceTokenManager.GetLoginName();

                //Neu co cau hinh, bat buoc nguoi chi dinh phai co thong tin chung chi hanh nghe
                if (HisServiceReqCFG.REQ_USER_MUST_HAVE_DIPLOMA
                    && !HisEmployeeUtil.HasDiploma(loginname))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_NguoiChiDinhKhongCoChungChiHanhNghe, loginname);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra chinh sach gia cua loai mau
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStock"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        internal bool CheckServicePaty(PatientBloodPresSDO data, V_HIS_MEDI_STOCK mediStock, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                long branchId = Config.HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == mediStock.DEPARTMENT_ID).BRANCH_ID;
                V_HIS_ROOM reqRoom = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.RequestRoomId);

                foreach (var req in data.ExpMestBltyReqs)
                {
                    HIS_BLOOD_TYPE bloodType = Config.HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == req.BLOOD_TYPE_ID);
                    if (bloodType == null)
                    {
                        LogSystem.Warn("BLOOD_TYPE_ID invalid " + LogUtil.TraceData("BltyReq", req));
                        return false;
                    }

                    V_HIS_SERVICE_PATY servicePaty = MOS.ServicePaty.ServicePatyUtil.GetApplied(Config.HisServicePatyCFG.DATA, branchId, mediStock.ROOM_ID, data.RequestRoomId, reqRoom.DEPARTMENT_ID, data.InstructionTime, treatment.IN_TIME, bloodType.SERVICE_ID, req.PATIENT_TYPE_ID.Value, null);

                    if (servicePaty == null)
                    {
                        HIS_PATIENT_TYPE patientType = Config.HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == req.PATIENT_TYPE_ID.Value);
                        string patientTypeName = patientType != null ? patientType.PATIENT_TYPE_NAME : null;
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, bloodType.BLOOD_TYPE_NAME, bloodType.BLOOD_TYPE_CODE, patientTypeName);
                        return false;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra nguoi sua co phai la nguoi tao hoac nguoi chi dinh hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool AllowUpdate(HIS_SERVICE_REQ data)
        {
            bool result = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (loginname != data.CREATOR && loginname != data.REQUEST_LOGINNAME && !HisEmployeeUtil.IsAdmin())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepSuaDonDoMinhTaoHoacChiDinh);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Kiem tra phieu xuat,
        /// trang thai cu phieu xuat co cho phep su hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceReq"></param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool VerifyExpMest(PatientBloodPresSDO data, HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST expMest)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Loai yeu cau khong phai la don mau." + LogUtil.TraceData("ServiceReq", serviceReq));
                    return false;
                }
                expMest = new HisExpMestGet().GetByServiceReqId(serviceReq.ID);
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    LogSystem.Warn("Khong lay duoc HIS_EXP_MEST theo Service_Req_Id: " + serviceReq.ID);
                    return false;
                }
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    LogSystem.Warn("Yeu cau la don mau nhung phieu xuat tuong ung khong phai la don mau. Kiem tra lai du lieu " + LogUtil.TraceData("expMest", expMest));
                    return false;
                }

                if (!HisExpMestConstant.ALLOW_UPDATE_DETAIL_STT_IDs.Contains(expMest.EXP_MEST_STT_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepCapNhatKhiDangOTrangThaiNay);
                    return false;
                }
                if (data.MediStockId != expMest.MEDI_STOCK_ID)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Khong cho phep sua kho xuat khi sua don mau");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsValidPatientType(PatientBloodPresSDO data, ref List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters)
        {
            //Kiem tra doi tuong thanh toan cua cac thuoc/vat tu co hop le khong
            //Luu y: chi check voi cac doi tuong thanh toan ko phai la vien phi
            List<long> patientTypeIds = new List<long>();
            if (IsNotNullOrEmpty(data.ExpMestBltyReqs))
            {
                List<long> ids = data.ExpMestBltyReqs.Select(o => o.PATIENT_TYPE_ID ?? 0).Distinct().ToList();
                patientTypeIds.AddRange(ids);
            }
            List<long> instructionTimes = new List<long>() { data.InstructionTime };
            return new HisServiceReqPresCheck(param).IsValidPatientType(data.TreatmentId, patientTypeIds, instructionTimes, ref patientTypeAlters);
        }


        internal bool IsValidForParentServiceReq(PatientBloodPresSDO data, HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest, ref List<HIS_SERVICE_REQ> parentSRs)
        {
            bool result = true;
            try
            {
                parentSRs = new HisServiceReqGet().GetByParentId(serviceReq.ID);
                List<HIS_EXP_MEST_BLTY_REQ> bltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(parentSRs) && IsNotNullOrEmpty(data.ExpMestBltyReqs) && IsNotNullOrEmpty(bltyReqs))
                {
                    bool isChangedCount = data.ExpMestBltyReqs.Count != bltyReqs.Count;
                    bool isChangedAmount = data.ExpMestBltyReqs.Sum(o => o.AMOUNT) != bltyReqs.Sum(o => o.AMOUNT);

                    var newPatientTypeIds = data.ExpMestBltyReqs.Select(o => o.PATIENT_TYPE_ID).ToList();
                    var oldPatientTypeIds = bltyReqs.Select(o => o.PATIENT_TYPE_ID).ToList();
                    bool isChangedPatientType = false;
                    if (IsNotNullOrEmpty(newPatientTypeIds) && IsNotNullOrEmpty(oldPatientTypeIds))
                    {
                        isChangedPatientType = (newPatientTypeIds.Exists(o => !oldPatientTypeIds.Contains((long)o.Value)) || oldPatientTypeIds.Exists(o => !newPatientTypeIds.Contains((long)o.Value)));
                    }

                    var newBlooTypeIds = data.ExpMestBltyReqs.Select(o => o.BLOOD_TYPE_ID).ToList();
                    var oldBlooTypeIds = bltyReqs.Select(o => o.BLOOD_TYPE_ID).ToList();
                    bool isChangedBloodType = false;
                    if (IsNotNullOrEmpty(newBlooTypeIds) && IsNotNullOrEmpty(oldBlooTypeIds))
                    {
                        isChangedBloodType = (newBlooTypeIds.Exists(o => !oldBlooTypeIds.Contains(o)) || oldBlooTypeIds.Exists(o => !newBlooTypeIds.Contains(o)));
                    }

                    // Neu nhu da thay doi
                    if (isChangedCount || isChangedAmount || isChangedPatientType || isChangedBloodType)
                    {
                        if (parentSRs.Exists(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT ||o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepSuaPhieuChiDinhKhiDaBatDau);
                            return false;
                        }

                        var ss = new HisSereServGet().GetByServiceReqIds(parentSRs.Select(o => o.ID).ToList());
                        if (IsNotNullOrEmpty(ss))
                        {
                            HisSereServCheck checker = new HisSereServCheck(param);
                            List<long> ssIds = ss.Select(o => o.ID).ToList();
                            if (!checker.HasNoBill(ss)
                                || !checker.HasNoInvoice(ss)
                                || !checker.HasNoDebt(ss)
                                || !checker.HasNoDeposit(ssIds, true))
                            {
                                LogSystem.Error("Y lenh DVKT dinh kem co cac dich vu da duoc thanh toan hoac tam thu dich vu hoac xuat hoa don hoac chot no");
                                return false;
                            }
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsValidForUpdateDifferentBloodType(PatientBloodPresSDO data)
        {
            bool valid = true;
            try
            {
                // Neu co key cau hinh tach mau theo loai mau khi create thi khong cho update nhieu loai mau khac nhau
                if (HisServiceReqCFG.IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE && data.ExpMestBltyReqs.GroupBy(o => o.BLOOD_TYPE_ID).Count() > 1)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepCapNhatChiDinhMauVoiNhieuLoaiMauKhacNhau);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
