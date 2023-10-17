using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAntibioticNewReg;
using MOS.MANAGER.HisAntibioticRequest;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisPrepare.Check;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription
{
    class SuspendingExpMest
    {
        public string TDL_SERVICE_REQ_CODE { get; set; }
        public string TDL_AGGR_EXP_MEST_CODE { get; set; }
    }

    class HisServiceReqPresCheck : BusinessBase
    {
        internal HisServiceReqPresCheck()
            : base()
        {
        }

        internal HisServiceReqPresCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(PrescriptionSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentException("data");
                if (data.TreatmentId <= 0) throw new ArgumentException("data.TreatmentId");
                if (data.RequestRoomId <= 0) throw new ArgumentException("data.RequestRoomId");

                //Kiem tra du lieu trong truong hop la don thuoc chay than
                if (data.IsKidney)
                {
                    if (!data.KidneyTimes.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonChayThanBatBuocNhapSoLanChayThan);
                        return false;
                    }

                    //Kiem tra xem co thuoc chay than nao duoc ke trong kho ko
                    List<string> medicineTypeNames = HisMedicineTypeCFG.DATA != null ? HisMedicineTypeCFG.DATA.Where(o => o.IS_KIDNEY == Constant.IS_TRUE && data.Medicines != null && data.Medicines.Exists(t => t.MedicineTypeId == o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList() : null;

                    if (IsNotNullOrEmpty(medicineTypeNames))
                    {
                        string s = string.Join(",", medicineTypeNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThuocChayThanKhongChoPhepKeChoBenhNhanMangVe, s);
                        return false;
                    }
                }

                //Neu don chay than duoc tao o man hinh "chay than" thi bat buoc phai co thuoc chay than
                if (data.IsExecuteKidneyPres)
                {
                    //Kiem tra xem co thuoc chay than nao duoc ke trong kho ko
                    bool isValid = data.Medicines != null && data.Medicines.Exists(t => HisMedicineTypeCFG.DATA != null && HisMedicineTypeCFG.DATA.Exists(o => o.ID == t.MedicineTypeId && o.IS_KIDNEY == Constant.IS_TRUE));

                    if (!isValid)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaChonThuocChayThan);
                        return false;
                    }
                }

                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines)
                    && !IsNotNullOrEmpty(data.ServiceReqMaties) && !IsNotNullOrEmpty(data.ServiceReqMeties)
                    && !IsNotNullOrEmpty(data.SerialNumbers))
                {
                    LogSystem.Warn("data.Materials, data.Medicines, data.ServiceReqMaties, data.Medicines, data.SerialNumbers, data.SubPresMeties, data.SubPresMaties null");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }

                if (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => t.MaterialTypeId <= 0 || t.Amount <= 0 || t.MediStockId <= 0 || t.PatientTypeId <= 0))
                {
                    LogSystem.Warn("data.Materials thieu thong tin MaterialTypeId, Amount, MediStockId hoac PatientTypeId");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => t.MedicineTypeId <= 0 || t.Amount <= 0 || t.MediStockId <= 0 || t.PatientTypeId <= 0))
                {
                    LogSystem.Warn("data.Medicines thieu thong tin MedicineTypeId, Amount, MediStockId hoac PatientTypeId");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => t.IsBedExpend && !t.IsExpend))
                {
                    LogSystem.Warn("data.Medicines ton tai ban ghi ko phai 'hao phi' nhung duoc danh dau la 'hao phi tien giuong'");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => t.IsBedExpend && !t.IsExpend))
                {
                    LogSystem.Warn("data.Materials ton tai ban ghi ko phai 'hao phi' nhung duoc danh dau la 'hao phi tien giuong'");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }

                if (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => t.IsBedExpend && t.SereServParentId.HasValue))
                {
                    LogSystem.Warn("data.Medicines ton tai ban ghi co 'sere_serv_parent_id' nhung duoc danh dau la 'hao phi tien giuong'");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => t.IsBedExpend && t.SereServParentId.HasValue))
                {
                    LogSystem.Warn("data.Materials ton tai ban ghi co 'sere_serv_parent_id' nhung duoc danh dau la 'hao phi tien giuong'");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => !t.MaterialId.HasValue) && data.Materials.Exists(t => t.MaterialId.HasValue))
                {
                    LogSystem.Warn("data.Materials ton tai ban ghi co MaterialId va ban ghi ko co MaterialId");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => !t.MedicineId.HasValue) && data.Medicines.Exists(t => t.MedicineId.HasValue))
                {
                    LogSystem.Warn("data.Medicines ton tai ban ghi co MedicineId va ban ghi ko co MedicineId");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }

                if (data.TrackingId.HasValue)
                {
                    HIS_TRACKING tracking = new HisTrackingGet().GetById(data.TrackingId.Value);
                    if (tracking == null || tracking.TREATMENT_ID != data.TreatmentId)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ToDieuTriCuaHoSoKhac);
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

        internal bool IsAllowMediStock(PrescriptionSDO data)
        {
            List<long> mediStockIds = null;
            return this.IsAllowMediStock(data, ref mediStockIds);
        }

        internal bool IsAllowMediStock(PrescriptionSDO data, ref List<long> mediStockIds)
        {
            //Kiem tra cac kho su dung co hop le khong
            List<long> tmp = new List<long>();

            if (IsNotNullOrEmpty(data.Medicines))
            {
                List<long> ids = data.Medicines.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }
            if (IsNotNullOrEmpty(data.Materials))
            {
                List<long> ids = data.Materials.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }
            if (IsNotNullOrEmpty(data.SerialNumbers))
            {
                List<long> ids = data.SerialNumbers.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }

            tmp = tmp.Distinct().ToList(); //tranh truong hop thuoc va vat tu cung kho

            if (IsNotNullOrEmpty(tmp))
            {
                //Kiem tra phong y/c co hop le khong
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }
                else if (workPlace != null && workPlace.ExecuteRoomId.HasValue)
                {
                    var exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ID == workPlace.ExecuteRoomId.Value);
                    if (exeRoom == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                        return false;
                    }

                    //Kiem tra xem co kho khong nam trong d/s cac kho duoc cau hinh cho phep xuat tu phong dang lam viec hay khong
                    if (!HisServiceReqCFG.IS_PRESCRIPTION_MEST_ROOM_OPTION || (exeRoom.IS_EXAM != Constant.IS_TRUE))
                    {
                        List<long> forbiddenStocks = tmp.Where(o => HisMestRoomCFG.DATA == null || !HisMestRoomCFG.DATA.Exists(t => t.MEDI_STOCK_ID == o && t.ROOM_ID == workPlace.RoomId && t.IS_ACTIVE == Constant.IS_TRUE)).ToList();
                        if (IsNotNullOrEmpty(forbiddenStocks))
                        {
                            List<string> mediStockNames = HisMediStockCFG.DATA.Where(o => forbiddenStocks.Contains(o.ID)).Select(o => o.MEDI_STOCK_NAME).ToList();
                            string mediStockNameStr = string.Join(",", mediStockNames);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoSauKhongChoPhepXuatDenPhongDangLamViec, mediStockNameStr);
                            return false;
                        }
                    }
                }

                //Kiem tra trong cac kho ke thuoc co kho nao bi khoa hay khong
                List<string> lockMediStockNames = HisMediStockCFG.DATA
                    .Where(o => tmp.Contains(o.ID) && o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    .Select(o => o.MEDI_STOCK_NAME)
                    .ToList();

                if (IsNotNullOrEmpty(lockMediStockNames))
                {
                    string mediStockNameStr = string.Join(",", lockMediStockNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStockNameStr);
                    return false;
                }

                mediStockIds = tmp;
            }
            return true;
        }

        internal bool IsValidPatientType(PrescriptionSDO data, long instructionTime, ref List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters)
        {
            return this.IsValidPatientType(data, new List<long>() { instructionTime }, ref patientTypeAlters);
        }

        internal bool IsValidPatientType(PrescriptionSDO data, List<long> instructionTimes, ref List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters)
        {
            //Kiem tra doi tuong thanh toan cua cac thuoc/vat tu co hop le khong
            //Luu y: chi check voi cac doi tuong thanh toan ko phai la vien phi
            List<long> patientTypeIds = new List<long>();
            if (IsNotNullOrEmpty(data.Medicines))
            {
                List<long> ids = data.Medicines.Select(o => o.PatientTypeId).Distinct().ToList();
                patientTypeIds.AddRange(ids);
            }
            if (IsNotNullOrEmpty(data.Materials))
            {
                List<long> ids = data.Materials.Select(o => o.PatientTypeId).Distinct().ToList();
                patientTypeIds.AddRange(ids);
            }
            return this.IsValidPatientType(data.TreatmentId, patientTypeIds, instructionTimes, ref patientTypeAlters);
        }

        internal bool IsValidPatientType(long treatmentId, List<long> patientTypeIds, List<long> instructionTimes, ref List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters)
        {
            bool valid = true;
            try
            {
                //Kiem tra thong tin dien doi tuong hien tai ho so dieu tri
                patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(treatmentId);

                if (!IsNotNullOrEmpty(patientTypeAlters))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiThongTinDienDoiTuong);
                    return false;
                }

                //Chi check voi doi tuong thanh toan co "don vi dong chi tra"
                patientTypeIds = patientTypeIds.Where(o => !HisPatientTypeCFG.NO_CO_PAYMENT.Exists(t => t.ID == o)).Distinct().ToList();

                if (IsNotNullOrEmpty(patientTypeIds))
                {
                    //Duyet theo thoi gian y lenh de check dien doi tuong
                    foreach (long instructionTime in instructionTimes)
                    {
                        HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetApplied(treatmentId, instructionTime, patientTypeAlters);

                        //Kiem tra xem trong cac doi tuong thanh toan, co doi tuong thanh toan nao khac voi dien doi tuong cua BN ko
                        List<long> notExistIds = patientTypeIds.Where(o => pta == null || o != pta.PATIENT_TYPE_ID).ToList();
                        if (IsNotNullOrEmpty(notExistIds))
                        {
                            List<string> patientTypeNames = HisPatientTypeCFG.DATA.Where(o => notExistIds.Contains(o.ID)).Select(o => o.PATIENT_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", patientTypeNames);
                            string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(instructionTime);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiThongTinDienDoiTuongTruocThoiDiemYLenh, nameStr, time);
                            return false;
                        }

                        //Neu dien doi tuong la BHYT thi kiem tra han su dung
                        if (pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && patientTypeIds != null && patientTypeIds.Contains(HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT) && pta.IS_NO_CHECK_EXPIRE != Constant.IS_TRUE && pta.HEIN_CARD_TO_TIME.HasValue)
                        {
                            //Cong them "so ngay cho phep vuot qua" truoc khi check han the
                            int exceedDayAllow = pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY ? HisHeinBhytCFG.EXCEED_DAY_ALLOW_FOR_IN_PATIENT : 0;

                            DateTime toTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(pta.HEIN_CARD_TO_TIME.Value).Value.AddDays(exceedDayAllow);
                            long toTimeNum = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(toTime).Value;

                            long? heinCardFromDate = Inventec.Common.DateTime.Get.StartDay(pta.HEIN_CARD_FROM_TIME.Value);
                            long? heinCardToDate = Inventec.Common.DateTime.Get.StartDay(toTimeNum);
                            long? instructionDate = Inventec.Common.DateTime.Get.StartDay(instructionTime);

                            if (heinCardFromDate > instructionDate || heinCardToDate < instructionDate)
                            {
                                string instructionTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(instructionTime);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhKhongNamTrongKhoangThoiGianHieuLucCuaTheBhyt, instructionTimeStr);
                                return false;
                            }
                        }
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

        internal bool IsAllowUpdate(PrescriptionSDO data, ref HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                var tmp = data.Id.HasValue ? new HisServiceReqGet().GetById(data.Id.Value) : null;
                if (tmp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.Id ko ton tai");
                    return false;
                }

                if (tmp.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    HIS_SERVICE_REQ_STT stt = HisServiceReqSttCFG.DATA.Where(o => o.ID == tmp.SERVICE_REQ_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonDangOTrangThaiKhongChoPhepCapNhat, stt.SERVICE_REQ_STT_NAME);
                    return false;
                }

                var tmpExpMest = new HisExpMestGet().GetByServiceReqId(tmp.ID);

                if (tmpExpMest != null)
                {
                    if ((IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => t.MediStockId != tmpExpMest.MEDI_STOCK_ID))
                    || (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => t.MediStockId != tmpExpMest.MEDI_STOCK_ID)))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai thuoc/vat tu co kho khac voi kho cua phieu xuat");
                        return false;
                    }

                    if (tmpExpMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                        || tmpExpMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpExpMest.EXP_MEST_STT_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepChinhSua, expMestStt.EXP_MEST_STT_NAME);
                        return false;
                    }

                    //Doi voi tong hop phong kham thi ko van cho phep sua neu da tong hop (mien la phieu chua duoc duyet/thuc xuat)
                    if (tmpExpMest.AGGR_EXP_MEST_ID.HasValue && tmpExpMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonDaDuocTongHopPhieuLinh);
                        return false;
                    }

                    if (tmpExpMest.ANTIBIOTIC_REQUEST_ID.HasValue)
                    {
                        HIS_ANTIBIOTIC_REQUEST antibioticRequest = new HisAntibioticRequestGet().GetById(tmpExpMest.ANTIBIOTIC_REQUEST_ID.Value);

                        //Neu co phieu yeu cau su dung khang sinh da duoc phe duyet 
                        //va ko bat cau hinh "cho phep cap nhat phieu da duoc phe duyet"
                        //thi kiem tra du lieu don thuoc xem co thay doi danh sach khang sinh khong
                        if (antibioticRequest != null && antibioticRequest.ANTIBIOTIC_REQUEST_STT == IMSys.DbConfig.HIS_RS.HIS_ANTIBIOTIC_REQUEST_STT.APPROVED && !HisAntibioticRequestCFG.IS_ALLOW_TO_UPDATE_APPROVED_REQUEST)
                        {
                            HisAntibioticNewRegFilterQuery filter = new HisAntibioticNewRegFilterQuery();
                            filter.ANTIBIOTIC_REQUEST_ID = tmpExpMest.ANTIBIOTIC_REQUEST_ID;
                            List<HIS_ANTIBIOTIC_NEW_REG> antibioticNewRegs = new HisAntibioticNewRegGet().Get(filter);

                            //Lay ra danh sach hoat chat da duoc luu trong phieu yeu cau su dung
                            List<long> oldApprovalRequiredActiveIngredientIds = antibioticNewRegs != null ? antibioticNewRegs.Select(o => o.ACTIVE_INGREDIENT_ID).Distinct().ToList() : null;

                            //Lay ra danh sach hoat chat tuong ung voi cac thuoc cua don moi
                            List<long> newMedicineTypeIds = data.Medicines != null ? data.Medicines.Select(o => o.MedicineTypeId).ToList() : null;

                            List<long> newApprovalRequiredActiveIngredientIds = newMedicineTypeIds != null && HisMedicineTypeAcinCFG.APPROVAL_REQUIRED != null ? HisMedicineTypeAcinCFG.APPROVAL_REQUIRED.Where(o => newMedicineTypeIds.Contains(o.MEDICINE_TYPE_ID)).Select(o => o.ACTIVE_INGREDIENT_ID).Distinct().ToList() : null;

                            if (CommonUtil.IsDiff<long>(oldApprovalRequiredActiveIngredientIds, newApprovalRequiredActiveIngredientIds))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuYeuCauSuDungKhangSinhDaDuocDuyet, antibioticRequest.ANTIBIOTIC_REQUEST_CODE);
                                return false;
                            }
                        }

                    }
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (loginName != tmp.CREATOR && loginName != tmp.REQUEST_LOGINNAME && !HisEmployeeUtil.IsAdmin())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepSuaDonDoMinhTaoHoacChiDinh);
                    return false;
                }

                serviceReq = tmp;
                expMest = tmpExpMest;

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidMediStockInCaseOfUpdate(PrescriptionSDO data, HIS_SERVICE_REQ old, ref long? mediStockId)
        {
            bool valid = true;
            try
            {
                List<long> mediStockIds = new List<long>();
                if (IsNotNullOrEmpty(data.Medicines))
                {
                    List<long> ids = data.Medicines.Select(o => o.MediStockId).ToList();
                    mediStockIds.AddRange(ids);
                }
                if (IsNotNullOrEmpty(data.Materials))
                {
                    List<long> ids = data.Materials.Select(o => o.MediStockId).ToList();
                    mediStockIds.AddRange(ids);
                }
                mediStockIds = mediStockIds.Distinct().ToList();

                if (mediStockIds != null && mediStockIds.Count > 1)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Trong truong hop sua phieu xuat, ko cho phep chon thuoc o nhieu kho");
                    return false;
                }

                if (mediStockIds.Count == 1)
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockIds[0]).FirstOrDefault();

                    //Trong truong hop don thuoc cu co chua thuoc trong kho thi thuc hien check, ko cho phep sua thong tin kho xuat
                    //(neu don chi co thuoc ngoai kho thi phong y/c voi phong xu ly luon la mot)
                    if (old.EXECUTE_ROOM_ID != old.REQUEST_ROOM_ID
                        && (mediStock == null || mediStock.ROOM_ID != old.EXECUTE_ROOM_ID))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Trong truong hop sua don va don cu co chua thuoc trong kho thi ko cho phep thay doi thong tin kho (medi_stock_id)");
                        return false;
                    }

                    mediStockId = mediStockIds[0];
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

        internal bool IsAllowPrescription(PrescriptionSDO data)
        {
            bool valid = true;
            try
            {
                string loginname = (!String.IsNullOrWhiteSpace(data.RequestLoginName)) ? data.RequestLoginName : ResourceTokenManager.GetLoginName();

                //Neu co cau hinh, chi cho bs ke don thuoc
                if (HisServiceReqCFG.JUST_ALLOW_DOCTOR_PRESCRIPTION == 1
                    && !HisEmployeeUtil.IsDoctor(loginname)
                    && (IsNotNullOrEmpty(data.Medicines) || IsNotNullOrEmpty(data.ServiceReqMeties)))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_NguoiChiDinhKhongPhaiLaBacSyKhongDuocPhepKeDonThuoc, loginname, ConfigUtil.GetConfigCode(HisServiceReqCFG.JUST_ALLOW_DOCTOR_PRESCRIPTION_CFG) ?? "");
                    return false;
                }
                else if (HisServiceReqCFG.JUST_ALLOW_DOCTOR_PRESCRIPTION == 2
                    && !HisEmployeeUtil.IsDoctor(loginname))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_NguoiChiDinhKhongPhaiLaBacSyKhongDuocPhepKeDon, loginname, ConfigUtil.GetConfigCode(HisServiceReqCFG.JUST_ALLOW_DOCTOR_PRESCRIPTION_CFG) ?? "");
                    return false;
                }

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

        internal bool CheckRankPrescription(PrescriptionSDO data)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.Medicines))
                {
                    return true;
                }
                string loginname = data.RequestLoginName;
                if (String.IsNullOrWhiteSpace(loginname))
                {
                    loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                }

                HIS_EMPLOYEE emp = HisEmployeeCFG.DATA != null ? HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == loginname) : null;
                long? rank = null;
                if (emp != null)
                {
                    rank = emp.MEDICINE_TYPE_RANK;
                }

                if (rank.HasValue && rank.Value == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_TYPE.MEDICINE_TYPE_RANK__5)
                {
                    return true;
                }
                List<HIS_MEDICINE_TYPE> typeNotAllows = null;
                if (rank.HasValue)
                {
                    typeNotAllows = HisMedicineTypeCFG.DATA.Where(o => data.Medicines.Exists(e => e.MedicineTypeId == o.ID) && o.RANK.HasValue && o.RANK.Value > rank.Value).ToList();
                }
                else
                {
                    typeNotAllows = HisMedicineTypeCFG.DATA.Where(o => data.Medicines.Exists(e => e.MedicineTypeId == o.ID) && o.RANK.HasValue).ToList();
                }

                if (IsNotNullOrEmpty(typeNotAllows))
                {
                    List<string> names = typeNotAllows.Select(s => s.MEDICINE_TYPE_NAME).ToList();
                    string typeName = String.Join(";", names);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacThuocSauKhongNamTrongCapDoDuocKeDon, typeName);
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
        /// Kiem tra xem so luong ke co vuot qua so luong da du tru hay ko
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="Medicines"></param>
        /// <param name="Materials"></param>
        /// <param name="notInserviceReqId"></param>
        /// <returns></returns>
        internal bool CheckAmountPrepare(long treatmentId, List<PresMedicineSDO> Medicines, List<PresMaterialSDO> Materials, long? notInserviceReqId)
        {
            bool valid = true;
            try
            {
                HisPrepareCheckAmount prepareChecker = new HisPrepareCheckAmount(param);
                List<PrepareData> errorDatas = new List<PrepareData>();

                //Chi kiem tra doi voi cac loai thuoc bat buoc/vat tu phai du tru
                List<PresMedicineSDO> mustPrepareMetys = Medicines != null ? Medicines.Where(o => HisMedicineTypeCFG.DATA.Any(a => a.ID == o.MedicineTypeId && a.IS_MUST_PREPARE == Constant.IS_TRUE)).ToList() : null;
                List<PresMaterialSDO> mustPrepareMatys = Materials != null ? Materials.Where(o => HisMaterialTypeCFG.DATA.Any(a => a.ID == o.MaterialTypeId && a.IS_MUST_PREPARE == Constant.IS_TRUE)).ToList() : null;

                if (IsNotNullOrEmpty(mustPrepareMetys))
                {
                    var Groups = mustPrepareMetys.GroupBy(g => g.MedicineTypeId).ToList();
                    foreach (var group in Groups)
                    {
                        decimal appAmount = 0;
                        decimal presAmount = 0;
                        decimal newAmount = group.Sum(s => s.Amount);
                        if (!prepareChecker.CheckAmountMedicineNotInServiceReq(treatmentId, group.Key, notInserviceReqId ?? 0, newAmount, ref appAmount, ref presAmount))
                        {
                            PrepareData pd = new PrepareData();
                            pd.TypeId = group.Key;
                            pd.TypeName = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key).MEDICINE_TYPE_NAME;
                            pd.ApprovalAmount = appAmount;
                            pd.PresAmount = presAmount + newAmount;
                            errorDatas.Add(pd);
                        }
                    }
                }

                if (IsNotNullOrEmpty(mustPrepareMatys))
                {
                    var Groups = mustPrepareMatys.GroupBy(g => g.MaterialTypeId).ToList();
                    foreach (var group in Groups)
                    {
                        decimal appAmount = 0;
                        decimal presAmount = 0;
                        decimal newAmount = group.Sum(s => s.Amount);
                        if (!prepareChecker.CheckAmountMaterialNotInServiceReq(treatmentId, group.Key, notInserviceReqId ?? 0, newAmount, ref appAmount, ref presAmount))
                        {
                            PrepareData pd = new PrepareData();
                            pd.TypeId = group.Key;
                            pd.TypeName = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key).MATERIAL_TYPE_NAME;
                            pd.ApprovalAmount = appAmount;
                            pd.PresAmount = presAmount + newAmount;
                            errorDatas.Add(pd);
                        }
                    }
                }

                if (IsNotNullOrEmpty(errorDatas))
                {
                    string duyet = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_Duyet, param.LanguageCode);
                    string ke = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_Ke, param.LanguageCode);
                    string message = "";
                    foreach (var item in errorDatas)
                    {
                        if (String.IsNullOrWhiteSpace(message))
                        {
                            message = String.Format("{0}({1}={2}.{3}={4})", item.TypeName, duyet, item.ApprovalAmount, ke, item.PresAmount);
                        }
                        else
                        {
                            message = String.Format(";{0}({1}={2}.{3}={4})", item.TypeName, duyet, item.ApprovalAmount, ke, item.PresAmount);
                        }
                    }
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacThuocVatTuSauCoSoLuongKeLonHonDuTru, message);
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
        /// Kiem tra xem da vuot qua so ngay lon nhat cho phep "treo" don chua
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        internal bool IsNotExceededMaxSuspendingDay(long departmentId)
        {
            bool valid = true;
            try
            {
                if (HisServiceReqCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION > 0)
                {
                    DateTime t = DateTime.Now.AddDays(-1 * HisServiceReqCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION);
                    long tmp = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(t).Value;

                    string sql = "SELECT TDL_SERVICE_REQ_CODE, TDL_AGGR_EXP_MEST_CODE "
                    + " FROM HIS_EXP_MEST S "
                    + " JOIN HIS_SERVICE_REQ R ON R.ID = S.SERVICE_REQ_ID "
                    + " WHERE R.IS_NO_EXECUTE IS NULL AND S.IS_NOT_TAKEN IS NULL "
                    + " AND S.REQ_DEPARTMENT_ID = {0} AND S.CREATE_TIME < {1} "
                    + " AND S.EXP_MEST_STT_ID IN ({2}, {3}, {4}) AND S.EXP_MEST_TYPE_ID IN ({5})";
                    sql = string.Format(sql, departmentId, tmp, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT);

                    List<SuspendingExpMest> rs = DAOWorker.SqlDAO.GetSql<SuspendingExpMest>(sql);
                    if (IsNotNullOrEmpty(rs))
                    {
                        string serviceReqCodeStr = string.Join(",", rs.Select(o => o.TDL_SERVICE_REQ_CODE).ToList());
                        List<string> aggrCodes = rs.Where(o => o.TDL_AGGR_EXP_MEST_CODE != null).Select(o => o.TDL_AGGR_EXP_MEST_CODE).Distinct().ToList();
                        if (IsNotNullOrEmpty(aggrCodes))
                        {
                            string aggrExpMestCodeStr = string.Join(",", aggrCodes);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuDieuTriQuaNgayChuaXuat, HisServiceReqCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION.ToString(), serviceReqCodeStr, aggrExpMestCodeStr);
                        }
                        else
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuQuaNgayChuaXuat, HisServiceReqCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION.ToString(), serviceReqCodeStr);
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidUseWithInstructionTimes(List<long> useTimes, List<long> instructionTimes)
        {
            bool valid = true;
            try
            {
                if (useTimes != null && useTimes.Count > 1 && instructionTimes != null && instructionTimes.Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepThucHienKeDonThuocVaDuTruThuocNhieuNgayCungLuc);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidExpMestReason(List<PresMedicineSDO> medicines, List<PresMaterialSDO> materials, bool isUpdate)
        {
            bool valid = true;
            try
            {
                if (HisExpMestCFG.IS_REASON_REQUIRED)
                {
                    List<string> inValidNames = new List<string>();

                    var inValidMedicines = IsNotNullOrEmpty(medicines) ? medicines.Where(o => !o.ExpMestReasonId.HasValue).ToList() : null;
                    var inValidMaterials = IsNotNullOrEmpty(materials) ? materials.Where(o => !o.ExpMestReasonId.HasValue).ToList() : null;
                    if (IsNotNullOrEmpty(inValidMedicines))
                        inValidNames.AddRange(HisMedicineTypeCFG.DATA.Where(w => inValidMedicines.Exists(e => e.MedicineTypeId == w.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList());
                    if (IsNotNullOrEmpty(inValidMaterials))
                        inValidNames.AddRange(HisMaterialTypeCFG.DATA.Where(w => inValidMaterials.Exists(e => e.MaterialTypeId == w.ID)).Select(o => o.MATERIAL_TYPE_NAME).ToList());

                    if (IsNotNullOrEmpty(inValidNames))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThuocVatTuThieuThongTinLydoXuat, string.Join(", ", inValidNames));
                        return false;
                    }

                    if (isUpdate)
                    {
                        List<long> duplicateReasons = new List<long>();
                        if (IsNotNullOrEmpty(medicines))
                            duplicateReasons.AddRange(medicines.Select(o => o.ExpMestReasonId.Value).ToList());
                        if (IsNotNullOrEmpty(materials))
                            duplicateReasons.AddRange(materials.Select(o => o.ExpMestReasonId.Value).ToList());

                        if (duplicateReasons.Distinct().ToList().Count > 1)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiHonMotLyDoXuat, string.Join(", ", duplicateReasons.Distinct().ToList()));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidIcdPatientTypeOtherPaySource(string IcdCode, string IcdSubCode, List<PresMedicineSDO> medicines, List<PresMaterialSDO> materials)
        {
            bool valid = true;
            try
            {
                if (string.IsNullOrWhiteSpace(IcdSubCode) && (";" + HisServiceReqCFG.ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID + ";").Contains(";" + IcdCode + ";"))
                {
                    List<string> inValidNames = new List<string>();

                    var inValidMedicines = IsNotNullOrEmpty(medicines) ? medicines.Where(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && !o.OtherPaySourceId.HasValue).ToList() : null;
                    var inValidMaterials = IsNotNullOrEmpty(materials) ? materials.Where(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && !o.OtherPaySourceId.HasValue).ToList() : null;
                    if (IsNotNullOrEmpty(inValidMedicines))
                        inValidNames.AddRange(HisMedicineTypeCFG.DATA.Where(w => inValidMedicines.Exists(e => e.MedicineTypeId == w.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList());
                    if (IsNotNullOrEmpty(inValidMaterials))
                        inValidNames.AddRange(HisMaterialTypeCFG.DATA.Where(w => inValidMaterials.Exists(e => e.MaterialTypeId == w.ID)).Select(o => o.MATERIAL_TYPE_NAME).ToList());

                    if (IsNotNullOrEmpty(inValidNames))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_BanCanBoSungThongTinChuanDoanPhuHoacDoiDoiTuongThanhToanCuarThuocVatTuSangVienPhi, string.Join("/", inValidNames));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidFinishTimeCls(HIS_TREATMENT treatment, HIS_SERVICE_REQ parentServiceReq, long instructionTime)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(treatment) && treatment.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == treatment.TDL_PATIENT_TYPE_ID.Value);
                    if (IsNotNull(patientType) && patientType.IS_CHECK_FINISH_CLS_WHEN_PRES == Constant.IS_TRUE && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM && IsNotNull(parentServiceReq) && parentServiceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                    {
                        HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                        filter.PARENT_ID = parentServiceReq.ID;
                        filter.SERVICE_REQ_TYPE_IDs = HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs;
                        filter.IS_NO_EXECUTE = false;
                        var sReqs = new HisServiceReqGet().Get(filter);
                        if (IsNotNullOrEmpty(sReqs))
                        {
                            var reqsNotFinished = sReqs.Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT).ToList();
                            if (IsNotNullOrEmpty(reqsNotFinished))
                            {
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiYLenhCLSChuaKetThuc, string.Join(",", reqsNotFinished.Select(o => o.SERVICE_REQ_CODE)));
                                return false;
                            }
                            else
                            {
                                var reqMaxFinishTime = sReqs.OrderByDescending(o => o.FINISH_TIME).FirstOrDefault();
                                if (reqMaxFinishTime != null && reqMaxFinishTime.FINISH_TIME.HasValue)
                                {
                                    if (instructionTime < reqMaxFinishTime.FINISH_TIME)
                                    {
                                        DateTime dtFinish = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(reqMaxFinishTime.FINISH_TIME.Value).Value;
                                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TGYLenhCuaDonThuocNhoHonTGKTCls, dtFinish.ToString("HH:mm dd/mm/yyyy"));
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidMedicineWithBidDate(List<V_HIS_MEDICINE_2> choosenMedicines, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                if (HisServiceReqCFG.DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE)
                {
                    List<string> error = new List<string>();
                    List<V_HIS_MEDICINE_2> listMedicineValid = choosenMedicines.Where(o => o.VALID_FROM_TIME.HasValue || o.VALID_TO_TIME.HasValue).ToList();
                    if (IsNotNullOrEmpty(listMedicineValid))
                    {
                        Dictionary<string, List<string>> dicFromTo = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicFrom = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicTo = new Dictionary<string, List<string>>();

                        List<HIS_EXP_MEST_MEDICINE> checkValid = expMestMedicines.Where(o => listMedicineValid.Exists(e => e.ID == o.MEDICINE_ID)).ToList();
                        foreach (HIS_EXP_MEST_MEDICINE expMedicine in checkValid)
                        {
                            V_HIS_MEDICINE_2 medicine = listMedicineValid.FirstOrDefault(o => o.ID == expMedicine.MEDICINE_ID);
                            HIS_EXP_MEST expMest = expMests.FirstOrDefault(o => expMedicine.EXP_MEST_ID == o.ID);

                            if (IsNotNull(medicine) && IsNotNull(expMest))
                            {
                                //tao moi va sua bi sai thong tin TDL_INTRUCTION_DATE
                                long intructionDate = (expMest.TDL_INTRUCTION_TIME ?? 0) - (expMest.TDL_INTRUCTION_TIME ?? 0) % 1000000;
                                string medicineName = string.Format("{0}({1})", medicine.MEDICINE_TYPE_NAME, medicine.MEDICINE_TYPE_CODE);

                                if (medicine.VALID_FROM_TIME.HasValue && medicine.VALID_TO_TIME.HasValue &&
                                    (intructionDate < medicine.VALID_FROM_TIME.Value || intructionDate > medicine.VALID_TO_TIME.Value))
                                {
                                    string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_FROM_TIME.Value);
                                    string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_TO_TIME.Value);
                                    string key = string.Format("{0} - {1}", validFromTime, validToTime);

                                    if (!dicFromTo.ContainsKey(key))
                                    {
                                        dicFromTo[key] = new List<string>();
                                    }

                                    dicFromTo[key].Add(medicineName);
                                }
                                else if (medicine.VALID_FROM_TIME.HasValue && intructionDate < medicine.VALID_FROM_TIME.Value)
                                {
                                    string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_FROM_TIME.Value);

                                    if (!dicFrom.ContainsKey(validFromTime))
                                    {
                                        dicFrom[validFromTime] = new List<string>();
                                    }

                                    dicFrom[validFromTime].Add(medicineName);
                                }
                                else if (medicine.VALID_TO_TIME.HasValue && intructionDate > medicine.VALID_TO_TIME.Value)
                                {
                                    string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_TO_TIME.Value);

                                    if (!dicTo.ContainsKey(validToTime))
                                    {
                                        dicTo[validToTime] = new List<string>();
                                    }

                                    dicTo[validToTime].Add(medicineName);
                                }
                            }
                        }


                        if (dicFrom.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNhoHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFrom)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhLonHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicFromTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNgoaiKhoangNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFromTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(error))
                    {
                        param.Messages.AddRange(error);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidMaterialWithBidDate(List<V_HIS_MATERIAL_2> choosenMaterials, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                if (HisServiceReqCFG.DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE)
                {
                    List<string> error = new List<string>();
                    List<V_HIS_MATERIAL_2> listMaterialValid = choosenMaterials.Where(o => o.VALID_FROM_TIME.HasValue || o.VALID_TO_TIME.HasValue).ToList();
                    if (IsNotNullOrEmpty(listMaterialValid))
                    {
                        Dictionary<string, List<string>> dicFromTo = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicFrom = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicTo = new Dictionary<string, List<string>>();

                        List<HIS_EXP_MEST_MATERIAL> checkValid = expMestMaterials.Where(o => listMaterialValid.Exists(e => e.ID == o.MATERIAL_ID)).ToList();
                        foreach (HIS_EXP_MEST_MATERIAL expMaterial in checkValid)
                        {
                            V_HIS_MATERIAL_2 material = listMaterialValid.FirstOrDefault(o => o.ID == expMaterial.MATERIAL_ID);
                            HIS_EXP_MEST expMest = expMests.FirstOrDefault(o => expMaterial.EXP_MEST_ID == o.ID);

                            if (IsNotNull(material) && IsNotNull(expMest))
                            {
                                //tao moi va sua bi sai thong tin TDL_INTRUCTION_DATE
                                long intructionDate = (expMest.TDL_INTRUCTION_TIME ?? 0) - (expMest.TDL_INTRUCTION_TIME ?? 0) % 1000000;
                                string materialName = string.Format("{0}({1})", material.MATERIAL_TYPE_NAME, material.MATERIAL_TYPE_CODE);

                                if (material.VALID_FROM_TIME.HasValue && material.VALID_TO_TIME.HasValue &&
                                    (intructionDate < material.VALID_FROM_TIME.Value || intructionDate > material.VALID_TO_TIME.Value))
                                {
                                    string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_FROM_TIME.Value);
                                    string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_TO_TIME.Value);
                                    string key = string.Format("{0} - {1}", validFromTime, validToTime);

                                    if (!dicFromTo.ContainsKey(key))
                                    {
                                        dicFromTo[key] = new List<string>();
                                    }

                                    dicFromTo[key].Add(materialName);
                                }
                                else if (material.VALID_FROM_TIME.HasValue && intructionDate < material.VALID_FROM_TIME.Value)
                                {
                                    string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_FROM_TIME.Value);

                                    if (!dicFrom.ContainsKey(validFromTime))
                                    {
                                        dicFrom[validFromTime] = new List<string>();
                                    }

                                    dicFrom[validFromTime].Add(materialName);
                                }
                                else if (material.VALID_TO_TIME.HasValue && intructionDate > material.VALID_TO_TIME.Value)
                                {
                                    string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_TO_TIME.Value);

                                    if (!dicTo.ContainsKey(validToTime))
                                    {
                                        dicTo[validToTime] = new List<string>();
                                    }

                                    dicTo[validToTime].Add(materialName);
                                }
                            }
                        }


                        if (dicFrom.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNhoHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFrom)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhLonHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicFromTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNgoaiKhoangNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFromTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(error))
                    {
                        param.Messages.AddRange(error);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckServiceFinishTime(long instructionTime, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, List<HIS_SERE_SERV> existedSereServs, bool IsCabinet)
        {
            bool result = true;
            try
            {
                if (treatment != null)
                {
                    if (!IsCabinet && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                        && HisTreatmentCFG.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME > 0)
                    {
                        List<long> autoFinishServiceIds = HisTreatmentCFG.AutoFinishServiceIds(workPlace.BranchId);
                        HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                        filter.HAS_EXECUTE = true;
                        filter.TREATMENT_ID = treatment.ID;
                        var serviceReqs = new HisServiceReqGet().Get(filter);

                        List<HIS_SERE_SERV> checkSereServ = existedSereServs != null ? existedSereServs.Where(o =>
                            HisServiceTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID)
                            && (autoFinishServiceIds == null || !autoFinishServiceIds.Contains(o.SERVICE_ID))).ToList() : null;

                        if (IsNotNullOrEmpty(checkSereServ))
                        {
                            List<HIS_SERE_SERV_EXT> sereServExtCheck = new HisSereServExt.HisSereServExtGet().GetBySereServIds(checkSereServ.Select(s => s.ID).ToList());
                            long lastSereServEndTime = 0;
                            HIS_SERE_SERV lastSereServ = null;
                            foreach (var item in checkSereServ)
                            {
                                HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExtCheck) ? sereServExtCheck.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                                HIS_SERVICE_REQ req = IsNotNullOrEmpty(serviceReqs) ? serviceReqs.FirstOrDefault(o => o.ID == item.SERVICE_REQ_ID) : null;
                                if (ext != null && ext.END_TIME.HasValue && ext.END_TIME.Value > lastSereServEndTime)
                                {
                                    lastSereServEndTime = ext.END_TIME.Value;
                                    lastSereServ = item;
                                }
                                else if (req != null && req.FINISH_TIME.HasValue && req.FINISH_TIME.Value > lastSereServEndTime)
                                {
                                    lastSereServEndTime = req.FINISH_TIME.Value;
                                    lastSereServ = item;
                                }
                            }

                            if (lastSereServEndTime > 0 && Inventec.Common.DateTime.Check.IsValidTime(lastSereServEndTime) && lastSereServ != null)
                            {
                                int serviceFinishTimeMustBeLessThanPresTime = HisTreatmentCFG.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME;
                                var dtLastSereServEndTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(lastSereServEndTime).Value;
                                dtLastSereServEndTime = dtLastSereServEndTime.AddSeconds(-dtLastSereServEndTime.Second);
                                var dtInstructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(instructionTime).Value;
                                dtInstructionTime = dtInstructionTime.AddSeconds(-dtInstructionTime.Second);
                                if ((dtInstructionTime - dtLastSereServEndTime).TotalMinutes < serviceFinishTimeMustBeLessThanPresTime)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKeDonPhaiLonHonThoiGianKetThucDichVu___Phut, lastSereServ.TDL_SERVICE_NAME, Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(lastSereServEndTime), serviceFinishTimeMustBeLessThanPresTime.ToString());
                                    return false;
                                }
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
    }

    public class PrepareData
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal ApprovalAmount { get; set; }
        public decimal PresAmount { get; set; }

    }
}
