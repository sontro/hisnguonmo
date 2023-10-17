using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisHeinApproval;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentCheck : BusinessBase
    {
        /// <summary>
        /// Kiem tra du lieu co o trang thai unpause (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnpause(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data.IS_PAUSE.HasValue && data.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_BenhNhanDaKetThucDieuTri);
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

        internal bool IsUnpause(List<HIS_TREATMENT> data)
        {
            bool valid = true;
            try
            {
                List<string> treatmentCodes = data
                    .Where(o => o.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.TREATMENT_CODE).ToList();
                if (IsNotNullOrEmpty(treatmentCodes))
                {
                    string lockCodeStr = string.Join(",", treatmentCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaKetThucDieuTri, lockCodeStr);
                    valid = false;
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai pause (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsPause(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (!data.IS_PAUSE.HasValue || data.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaKetThucDieuTri);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai khoa BHYT chua (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLockHein(HIS_TREATMENT data)
        {
            return IsUnLockHein(new List<HIS_TREATMENT>() { data });
        }

        internal bool IsUnLockHein(List<HIS_TREATMENT> data)
        {
            bool valid = true;
            try
            {
                List<string> treatmentCodes = data
                    .Where(o => o.IS_LOCK_HEIN == MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.TREATMENT_CODE).ToList();
                if (IsNotNullOrEmpty(treatmentCodes))
                {
                    string lockCodeStr = string.Join(",", treatmentCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaDuyetKhoaBaoHiem, lockCodeStr);
                    valid = false;
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

        internal bool IsUnLockHein(long id)
        {
            HIS_TREATMENT data = new HisTreatmentGet().GetById(id);
            return this.IsUnLockHein(data);
        }

        /// <summary>
        /// Kiem tra du lieu co o trang thai khoa BHYT chua (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLockHein(HIS_TREATMENT data)
        {
            return IsLockHein(new List<HIS_TREATMENT>() { data });
        }

        internal bool IsLockHein(List<HIS_TREATMENT> data)
        {
            bool valid = true;
            try
            {
                List<string> treatmentCodes = data
                    .Where(o => o.IS_LOCK_HEIN != MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.TREATMENT_CODE).ToList();
                if (IsNotNullOrEmpty(treatmentCodes))
                {
                    string lockCodeStr = string.Join(",", treatmentCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoChuaDuyetKhoaBaoHiem, lockCodeStr);
                    valid = false;
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

        internal bool IsLockHein(long id)
        {
            HIS_TREATMENT data = new HisTreatmentGet().GetById(id);
            return this.IsLockHein(data);
        }

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(long id, ref HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                data = new HisTreatmentGet().GetById(id);
                return this.IsUnLock(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidOpenTreatmentPolicy(HIS_TREATMENT data, long patientTypeId)
        {
            bool valid = true;
            try
            {
                //Lay ra tat ca ho so dieu tri cua benh nhan tren he thong
                List<HIS_TREATMENT> exists = new HisTreatmentGet().GetByPatientId(data.PATIENT_ID);
                if (IsNotNullOrEmpty(exists))
                {
                    //Neu ko bat cau hinh cho phep mo nhieu ho so thi thuc hien check
                    if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION != HisTreatmentCFG.AlowManyTreatmentOpeningOption.YES)
                    {
                        //Kiem tra xem co ho so dieu tri nao chua duoc ket thuc hay khong
                        //Luu y: 
                        //- Voi ho so ko phai BHYT, ma chi den lam CLS (ko co cong kham) thi ko "chặn"
                        //- Co cau hinh "chi check neu ho so dang mo cung chi nhanh voi ho so dang dang ky tiep don" hoac "check tat ca"
                        List<HIS_TREATMENT> opening = exists
                            .Where(o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE))
                            .Where(o => HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.NO
                                || (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.YES_IF_OTHER_OPENING
                                    && o.BRANCH_ID == data.BRANCH_ID))
                            .Where(o => o.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                                || o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                                || this.IsNonBhytOutPatientAndHasExam(o))
                            .ToList();

                        if (IsNotNullOrEmpty(opening))
                        {
                            List<string> AllMess = new List<string>();

                            //lay ra danh sach ho so co dien dieu tri khac kham từ danh sach ho so dang mo(opening)
                            List<HIS_TREATMENT> treatments = opening.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatments))
                            {
                                List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }
                           
                            foreach (var item in opening)
                            {
                                string bedRoomMess = item.TREATMENT_CODE;
                                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == item.LAST_DEPARTMENT_ID);
                                if (department != null)
                                {
                                    bedRoomMess = item.TREATMENT_CODE + " - " + department.DEPARTMENT_NAME;
                                }

                                if (IsNotNullOrEmpty(bedRooms))
                                {
                                    List<V_HIS_TREATMENT_BED_ROOM_1> treatBedRooms = bedRooms.Where(p => p.TREATMENT_ID == item.ID).ToList();
                                    if (IsNotNullOrEmpty(treatBedRooms))
                                    {
                                        V_HIS_TREATMENT_BED_ROOM_1 treatBedRoom = treatBedRooms.OrderByDescending(o => o.ADD_TIME).FirstOrDefault();
                                        if (treatBedRoom != null)
                                        {
                                            bedRoomMess = item.TREATMENT_CODE + " - " + treatBedRoom.BED_ROOM_NAME + " - " + treatBedRoom.DEPARTMENT_NAME;
                                        }
                                    }
                                }
                               
                                if (bedRoomMess != null)
                                {
                                    AllMess.Add(bedRoomMess);
                                }
                            }
                            if (IsNotNullOrEmpty(AllMess))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhNhanDangDieuTriKhongChoPhepTaoMoi, string.Join("; ", AllMess));
                                return false;
                            }
                        }
                    }

                    // Lay danh sach cac HIS_BRANCH thoa man khong co thong tin tuyen hoac co thong tin tuyen trung voi chi nhanh nguoi dung dang lam viec
                    List<long> branchIds = null;
                    this.GetValidBranch(ref branchIds);

                    //Kiem tra xem ho so nao tao trong cung ngay dieu tri hay khong
                    long? startDay = Inventec.Common.DateTime.Get.StartDay(data.IN_TIME);
                    long? endDay = Inventec.Common.DateTime.Get.EndDay(data.IN_TIME);

                    List<HIS_TREATMENT> createdInDay = null;
                    // Loc danh sach ho so tao trong ngay theo chi nhanh
                    if (IsNotNullOrEmpty(branchIds))
                    {
                        createdInDay = exists.Where(o => o.IN_TIME <= endDay && o.IN_TIME >= startDay && branchIds.Contains(o.BRANCH_ID)).ToList();
                    }
                    else
                    {
                        createdInDay = exists.Where(o => o.IN_TIME <= endDay && o.IN_TIME >= startDay).ToList();
                    }

                    if (IsNotNullOrEmpty(createdInDay))
                    {
                        if (HisTreatmentCFG.MANY_TREATMENT_PER_DAY_OPTION == (int)HisTreatmentCFG.ManyTreatmentPerDayOption.ALLOW)
                        {
                            return true;
                        }
                        else if (HisTreatmentCFG.MANY_TREATMENT_PER_DAY_OPTION == (int)HisTreatmentCFG.ManyTreatmentPerDayOption.NO_2_BHYT)
                        {
                            if (patientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                            {
                                foreach (HIS_TREATMENT t in createdInDay)
                                {
                                    if (t.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                    {
                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongChoPhepTaoHonMotHoSoDieuTriBhytTrongNgay, t.TREATMENT_CODE);
                                        return false;
                                    }
                                }
                            }
                            return true;
                        }
                        else if (HisTreatmentCFG.MANY_TREATMENT_PER_DAY_OPTION == (int)HisTreatmentCFG.ManyTreatmentPerDayOption.EMERGENCY)
                        {
                            if (data.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE)
                            {
                                List<string> createdInDayTreatmentCodes = createdInDay.Select(o => o.TREATMENT_CODE).ToList();
                                string extraMess = string.Join("; ", createdInDayTreatmentCodes);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongChoPhepTaoHonMotHoSoDieuTriTrongCungMotNgay, extraMess);
                                return false;
                            }
                        }
                        else if (HisTreatmentCFG.MANY_TREATMENT_PER_DAY_OPTION == (int)HisTreatmentCFG.ManyTreatmentPerDayOption.NO_2_NON_EMERGENCY_BHYT)
                        {
                            int t = data.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE && patientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? 1 : 0;
                            List<string> existTreatmentCodes = createdInDay
                                .Where(o => o.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE && o.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                .Select(o => o.TREATMENT_CODE).ToList();

                            if (t + (existTreatmentCodes == null ? 0 : existTreatmentCodes.Count) > 1)
                            {
                                string extraMess = string.Join("; ", existTreatmentCodes);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongChoPhepTaoHonMotHoSoBhytKhongPhaiCapCuuTrongCungMotNgay, extraMess);
                                return false;
                            }
                        }
                        else
                        {
                            List<string> createdInDayTreatmentCodes = createdInDay.Select(o => o.TREATMENT_CODE).ToList();
                            string extraMess = string.Join("; ", createdInDayTreatmentCodes);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongChoPhepTaoHonMotHoSoDieuTriTrongCungMotNgay, extraMess);
                            return false;
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

        private void GetValidBranch(ref List<long> branchIds)
        {
            List<HIS_BRANCH> branchs = null;
            branchIds = new List<long>();
            HIS_BRANCH currentBranch = new TokenManager(param).GetBranch();
            if (IsNotNull(currentBranch))
            {
                branchIds.Add(currentBranch.ID);

                branchs = HisBranchCFG.DATA.Where(o => string.IsNullOrWhiteSpace(o.HEIN_LEVEL_CODE) || o.HEIN_LEVEL_CODE == currentBranch.HEIN_LEVEL_CODE).ToList();
                if (IsNotNullOrEmpty(branchs))
                {
                    branchIds.AddRange(branchs.Select(o => o.ID).ToList());
                }
            }
            else
            {
                branchs = HisBranchCFG.DATA.Where(o => string.IsNullOrWhiteSpace(o.HEIN_LEVEL_CODE)).ToList();
                if (IsNotNullOrEmpty(branchs))
                {
                    branchIds.AddRange(branchs.Select(o => o.ID).ToList());
                }
            }

            if (IsNotNullOrEmpty(branchIds))
            {
                branchIds = branchIds.Distinct().ToList();
            }
        }

        internal bool IsValidDepartment(long treatmentId)
        {
            HIS_DEPARTMENT_TRAN lastDepartmentTran = null;
            List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
            return this.IsValidDepartment(treatmentId, workPlaces, ref lastDepartmentTran);
        }

        internal bool IsValidDepartment(long treatmentId, WorkPlaceSDO workPlace, ref HIS_DEPARTMENT_TRAN lastDepartmentTran)
        {
            return this.IsValidDepartment(treatmentId, new List<WorkPlaceSDO>() { workPlace }, ref lastDepartmentTran);
        }

        internal bool IsValidDepartment(long treatmentId, List<WorkPlaceSDO> workPlaces, ref HIS_DEPARTMENT_TRAN lastDepartmentTran)
        {
            bool valid = true;
            try
            {
                HIS_DEPARTMENT_TRAN departmentTran = new HisDepartmentTranGet().GetLastByTreatmentId(treatmentId);

                if (!departmentTran.DEPARTMENT_IN_TIME.HasValue)
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentTran.DEPARTMENT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, department.DEPARTMENT_NAME);
                    return false;
                }

                if (!workPlaces.Where(o => o.DepartmentId == departmentTran.DEPARTMENT_ID).Any())
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentTran.DEPARTMENT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriThuocKhoaKhongChoPhepThucHien, department.DEPARTMENT_NAME);
                    return false;
                }
                lastDepartmentTran = departmentTran;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
        internal bool HasNoHeinApproval(long treatmentId)
        {
            bool valid = true;
            try
            {
                List<HIS_HEIN_APPROVAL> exists = new HisHeinApprovalGet().GetByTreatmentId(treatmentId);
                if (IsNotNullOrEmpty(exists))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_DuLieuDaDuyetBhyt);
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

        /// <summary>
        /// La benh nhan dien dieu tri la "kham", ko phai doi tuong BHYT va co cong kham
        /// </summary>
        /// <param name="treatment"></param>
        /// <returns></returns>
        private bool IsNonBhytOutPatientAndHasExam(HIS_TREATMENT treatment)
        {
            try
            {
                if (treatment.TDL_PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                    && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                    ssFilter.TREATMENT_ID = treatment.ID;
                    ssFilter.TDL_SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH;
                    ssFilter.HAS_EXECUTE = true;

                    List<HIS_SERE_SERV> examSereServs = new HisSereServGet().Get(ssFilter);
                    return IsNotNullOrEmpty(examSereServs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
