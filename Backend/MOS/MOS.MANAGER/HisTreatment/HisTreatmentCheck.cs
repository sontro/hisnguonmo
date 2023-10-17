using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccidentHurt;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisInfusion;
using MOS.MANAGER.HisMediReact;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.HisDebate;
using MOS.SDO;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientProgram;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.UTILITY;
using MOS.Filter;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentCheck : BusinessBase
    {
        internal HisTreatmentCheck()
            : base()
        {

        }

        internal HisTreatmentCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        /// <summary>
        /// Ket thuc dieu tri kiem tra xem co ton tai to dieu tri nao co
        /// thoi gian dieu tri lon hon thoi gian ket thuc dieu tri
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
        internal bool VerifyTreatmentFinishCheckTracking(HisTreatmentFinishSDO data)
        {
            bool result = true;
            try
            {
                if (data != null && !data.IsTemporary && data.TreatmentFinishTime > 0)
                {
                    HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                    filter.TREATMENT_ID = data.TreatmentId;
                    filter.TRACKING_TIME_FROM = data.TreatmentFinishTime + 1;// TRACKING_TIME > outTime : default  TRACKING_TIME >= outTime
                    List<HIS_TRACKING> trackings = new HisTrackingGet().Get(filter);
                    if (trackings != null && trackings.Count > 0)
                    {
                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TreatmentFinishCheckTracking);
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        internal bool VerifyRequireField(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.PATIENT_ID)) throw new ArgumentNullException("data.PATIENT_ID");
                if (!IsGreaterThanZero(data.IN_TIME)) throw new ArgumentNullException("data.IN_TIME");
                if (string.IsNullOrWhiteSpace(data.TDL_PATIENT_CODE)) throw new ArgumentNullException("data.TDL_PATIENT_CODE");
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

        internal bool VerifyRequireField(HisTreatmentRejectStoreSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (string.IsNullOrWhiteSpace(data.RejectReason)) throw new ArgumentNullException("data.RejectReason");
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

        internal bool VerifyRequireField(HisTreatmentApproveFinishSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (string.IsNullOrWhiteSpace(data.ApproveFinishNote)) throw new ArgumentNullException("data.ApproveFinishNote");
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

        internal bool VerifyRequireField(StoreBordereauCodeSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (string.IsNullOrWhiteSpace(data.StoreBordereauCode)) throw new ArgumentNullException("data.StoreBordereauCode");
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
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                data = new HisTreatmentGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
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
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_TREATMENT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.IDs = listId;
                    List<HIS_TREATMENT> listData = new HisTreatmentGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuyetKhoaTaiChinh);
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

        internal bool IsUnLock(List<HIS_TREATMENT> data)
        {
            bool valid = true;
            try
            {
                List<string> treatmentCodes = data
                    .Where(o => IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != o.IS_ACTIVE)
                    .Select(o => o.TREATMENT_CODE).ToList();
                if (IsNotNullOrEmpty(treatmentCodes))
                {
                    string lockCodeStr = string.Join(",", treatmentCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaDuyetKhoaVienPhi, lockCodeStr);
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
        /// Kiem tra du lieu co o trang thai lock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLock(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaDuyetKhoaTaiChinh);
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
        /// Kiem tra du lieu co o trang thai unlock (su dung id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisTreatmentDAO.IsUnLock(id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuyetKhoaTaiChinh);
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
        /// Kiem tra du lieu co o trang thai unlock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnTemporaryLock(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data.IS_TEMPORARY_LOCK == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaTamKhoaTaiChinh);
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

        internal bool IsUnTemporaryLock(List<HIS_TREATMENT> data)
        {
            bool valid = true;
            try
            {
                List<string> treatmentCodes = data
                    .Where(o => o.IS_TEMPORARY_LOCK == Constant.IS_TRUE)
                    .Select(o => o.TREATMENT_CODE).ToList();
                if (IsNotNullOrEmpty(treatmentCodes))
                {
                    string lockCodeStr = string.Join(",", treatmentCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacHoSoDaTamKhoaTaiChinh, lockCodeStr);
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
        /// Kiem tra ma da ton tai hay chua, id duoc su dung trong truong hop muon bo qua chinh ma cua minh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisTreatmentDAO.ExistsCode(code, id))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_TonTaiDuLieu);
                    return false;
                }

                List<HIS_TREATMENT_BED_ROOM> hisTreatmentBedRooms = new HisTreatmentBedRoomGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisTreatmentBedRooms))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_TonTaiDuLieu);
                    return false;
                }

                List<HIS_TRANSACTION> hisTransactions = new HisTransactionGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisTransactions))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TonTaiDuLieu);
                    return false;
                }

                List<HIS_ACCIDENT_HURT> hisAccidentHurts = new HisAccidentHurtGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisAccidentHurts))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccidentHurt_TonTaiDuLieu);
                    return false;
                }

                List<HIS_SERVICE_REQ> hisServiceReqs = new HisServiceReqGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisServiceReqs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiDuLieu);
                    return false;
                }

                List<HIS_CARE> hisBedServiceTypes = new HisCareGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisBedServiceTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCare_TonTaiDuLieu);
                    return false;
                }

                List<HIS_DEPARTMENT_TRAN> hisDepartmentTrans = new HisDepartmentTranGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisDepartmentTrans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_TonTaiDuLieu);
                    return false;
                }

                List<HIS_PATIENT_TYPE_ALTER> hisPatientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisPatientTypeAlters))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TonTaiDuLieu);
                    return false;
                }

                List<HIS_DHST> hisDhsts = new HisDhstGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisDhsts))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDhst_TonTaiDuLieu);
                    return false;
                }

                //List<HIS_IMP_MEST> hisImpMests = new HisImpMestGet().GetByTreatmentId(id);
                //if (IsNotNullOrEmpty(hisImpMests))
                //{
                //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMobaImpMest_TonTaiDuLieu);
                //    throw new Exception("Ton tai du lieu HIS_IMP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                //}

                List<HIS_TRACKING> hisTrackings = new HisTrackingGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisTrackings))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTracking_TonTaiDuLieu);
                    return false;
                }

                List<HIS_DEBATE> hisDebates = new HisDebateGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisDebates))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDebate_TonTaiDuLieu);
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

        internal bool IsValidInOutTime(long inTime, long? outTime, long? clinicalInTime)
        {
            bool valid = true;
            try
            {
                if (outTime.HasValue && inTime > outTime.Value)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(inTime);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianRaVienKhongDuocNhoHonThoiGianVaoVien, time);
                    valid = false;
                }

                if (clinicalInTime.HasValue && inTime > clinicalInTime.Value)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(inTime);
                    string clinicalTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(clinicalInTime.Value);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianNhapVienBeHonThoiGianVao, clinicalTime, time);
                    valid = false;
                }

                if (clinicalInTime.HasValue && outTime.HasValue && clinicalInTime > outTime)
                {
                    string ot = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(outTime.Value);
                    string clinicalTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(clinicalInTime.Value);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianNhapVienLonHonThoiGianRaVien, clinicalTime, ot);
                    valid = false;
                }


                long currentTime = Inventec.Common.DateTime.Get.Now().Value;
                if (outTime.HasValue && Config.HisTreatmentCFG.FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME && outTime.Value > currentTime)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(currentTime);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianRaVienKhongDuocLonHonThoiGianHienTai, time);
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

        internal bool IsNotUseAppointmentCode(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                //Neu dang ky su dung ma hen kham thi kiem tra xem da co ho so dieu tri nao su dung ma hen kham nay hay chua
                if (!string.IsNullOrWhiteSpace(data.APPOINTMENT_CODE))
                {
                    List<HIS_TREATMENT> treatments = new HisTreatmentGet().GetByAppointmentCode(data.APPOINTMENT_CODE);
                    if (IsNotNullOrEmpty(treatments))
                    {
                        List<string> treatmentCodes = treatments.Select(o => o.TREATMENT_CODE).ToList();
                        string treatmentCodeStr = string.Join(",", treatmentCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DaTonTaiHoSoDieuTriSuDungMaHenKhamNay, treatmentCodeStr);
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

        internal bool IsValidProgramId(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidTransferInInfo(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (this.IsHasTransferInInfo(data))
                {
                    if (!data.IS_TRANSFER_IN.HasValue || data.IS_TRANSFER_IN.Value != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("HSDT co thong tin chuyen den nhung truong  IS_TRANSFER_IN != 1");
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

        /// <summary>
        /// Kiem tra xem co thong tin chuyen den hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsHasTransferInInfo(HIS_TREATMENT data)
        {
            if (data != null)
            {
                return data.TRANSFER_IN_FORM_ID.HasValue
                    || data.TRANSFER_IN_REASON_ID.HasValue
                    || !string.IsNullOrWhiteSpace(data.TRANSFER_IN_ICD_CODE)
                    || !string.IsNullOrWhiteSpace(data.TRANSFER_IN_CODE)
                    || !string.IsNullOrWhiteSpace(data.TRANSFER_IN_ICD_NAME)
                    || !string.IsNullOrWhiteSpace(data.TRANSFER_IN_MEDI_ORG_CODE)
                    || !string.IsNullOrWhiteSpace(data.TRANSFER_IN_MEDI_ORG_NAME);
            }
            return false;
        }

        internal bool IsValidEmergencyInfo(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data.EMERGENCY_WTIME_ID.HasValue && data.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu ko hop le. Co thong tin EMERGENCY_WTIME_ID thi IS_EMERGENCY phai bang 1");
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

        internal bool CheckDeathInfo(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                if (data.IS_PAUSE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaKetThucDieuTri);
                    return false;
                }

                if (data.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_LoaiKetThucDieuTriCuaHoSoKhongPhaiLaTuVong);
                    return false;
                }

                if (!data.DEATH_TIME.HasValue || !data.DEATH_WITHIN_ID.HasValue || !data.DEATH_CAUSE_ID.HasValue)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsTreatmentEndTypeAppointment(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_LoaiKetThucDieuTriCuaHoSoKhongPhaiLaHenKham);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }

        internal bool HasDataStoreId(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data != null && !data.DATA_STORE_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocLuuVaoKhoBenhAn);
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
        /// Kiem tra thoi gian y lenh cua yeu cau va thoi gian ket thuc dieu tri
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIntructionTime(long? outTime, long treatmentId)
        {
            try
            {
                if (outTime.HasValue)
                {
                    //Kiem tra xem co y lenh nao co thoi gian lon hon thoi gian duyet khoa vien phi hay khong
                    List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatmentId);
                    List<string> serviceReqCodes = existsSereServs != null ? existsSereServs
                        .Where(o => o.AMOUNT > 0 && !o.IS_NO_EXECUTE.HasValue && o.TDL_INTRUCTION_TIME > outTime.Value)
                        .Select(o => o.TDL_SERVICE_REQ_CODE).Distinct().ToList() : null;

                    if (IsNotNullOrEmpty(serviceReqCodes))
                    {
                        string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacPhieuChiDinhCoThoiGianYLenhLonHonThoiGianRaVien, serviceReqCodeStr);
                        return false;
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

        internal bool IsNotExistsTreatmentOrder(HisTreatmentOrderSDO data)
        {
            bool valid = true;
            try
            {
                long treatmentid = data.Id ?? 0;
                string sql = "SELECT TREA.TREATMENT_CODE FROM HIS_TREATMENT TREA WHERE TREA.TREATMENT_ORDER =: param1 AND TREA.IN_DATE =: param2 AND ID <> : param3";

                List<string> exists = DAOWorker.SqlDAO.GetSql<string>(sql, data.TreatmentOrder, data.InDate, treatmentid);
                if (IsNotNullOrEmpty(exists))
                {
                    param.Messages.AddRange(exists);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsInCode(string inCode, long treatmentId)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT TREA.TREATMENT_CODE FROM HIS_TREATMENT TREA WHERE TREA.IN_CODE =: param1 AND ID <> : param2";
                List<string> exists = DAOWorker.SqlDAO.GetSql<string>(sql, inCode, treatmentId);
                
                if (IsNotNullOrEmpty(exists))
                {
                    string existTreatmentCodes = string.Join(",", exists);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DaTonTaiSoVaoVien, inCode, existTreatmentCodes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsInCode(string inCode)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT TREA.TREATMENT_CODE FROM HIS_TREATMENT TREA WHERE TREA.IN_CODE = :param1";
                List<string> exists = DAOWorker.SqlDAO.GetSql<string>(sql, inCode);

                if (IsNotNullOrEmpty(exists))
                {
                    string existTreatmentCodes = string.Join(",", exists);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DaTonTaiSoVaoVien, inCode, existTreatmentCodes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool HasNoDataStoreId(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.HasNoDataStoreId(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNoDataStoreId(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> hasDataStoreIds = datas != null ? datas.Where(o => o.DATA_STORE_ID.HasValue).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(hasDataStoreIds))
                {
                    string codes = String.Join(",", hasDataStoreIds);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocLuuVaoKhoBenhAn, codes);
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

        internal bool IsNotRejectStore(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotRejectStore(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotRejectStore(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> notRejectStores = datas != null ? datas.Where(o => o.APPROVAL_STORE_STT_ID.HasValue && o.APPROVAL_STORE_STT_ID.Value == IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__TU_CHOI).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notRejectStores))
                {
                    string codes = String.Join(",", notRejectStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocTuChoiDuyetBenhAn, codes);
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

        internal bool IsRejectStore(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsRejectStore(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsRejectStore(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> rejectStores = datas != null ? datas.Where(o => !o.APPROVAL_STORE_STT_ID.HasValue || o.APPROVAL_STORE_STT_ID.Value != IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__TU_CHOI).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(rejectStores))
                {
                    string codes = String.Join(",", rejectStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocTuChoiDuyetBenhAn, codes);
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

        internal bool IsUnapproveFinish(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsUnapproveFinish(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsUnapproveFinish(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> notRejectStores = datas != null ? datas.Where(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == Constant.IS_TRUE).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notRejectStores))
                {
                    string codes = String.Join(",", notRejectStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocDanhDauDuDieuKienRaVien, codes);
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

        internal bool IsApproveFinish(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsApproveFinish(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApproveFinish(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> notRejectStores = datas != null ? datas.Where(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != Constant.IS_TRUE).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notRejectStores))
                {
                    string codes = String.Join(",", notRejectStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocDanhDauDuDieuKienRaVien, codes);
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

        internal bool IsNotApprovalStore(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotApprovalStore(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotApprovalStore(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> approvalStores = datas != null ? datas.Where(o => o.APPROVAL_STORE_STT_ID.HasValue && o.APPROVAL_STORE_STT_ID.Value == IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(approvalStores))
                {
                    string codes = String.Join(",", approvalStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocChotDuyetHoSoBenhAn, codes);
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

        internal bool IsApprovalStore(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsApprovalStore(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApprovalStore(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> notApprovalStores = datas != null ? datas.Where(o => !o.APPROVAL_STORE_STT_ID.HasValue || o.APPROVAL_STORE_STT_ID.Value != IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notApprovalStores))
                {
                    string codes = String.Join(",", notApprovalStores);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocChotDuyetHoSoBenhAn, codes);
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

        internal bool IsVerifyTreatmentCode(string treatmentCode, ref HIS_TREATMENT raw)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(treatmentCode))
                {
                    raw = new HisTreatmentGet().GetByCode(treatmentCode);
                    if (raw == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        Logging("treatmentCode invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => treatmentCode), treatmentCode), LogType.Error);
                        valid = false;
                    }
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

        internal bool IsValidDepartmentInTime(long treatmentId, long inTime)
        {
            bool valid = true;
            try
            {
                List<V_HIS_DEPARTMENT_TRAN> departTrans = new HisDepartmentTranGet().GetViewByTreatmentId(treatmentId);
                V_HIS_DEPARTMENT_TRAN minIdDepartTrans = IsNotNullOrEmpty(departTrans) ? departTrans.OrderBy(o => o.ID).FirstOrDefault() : null;
                if (minIdDepartTrans != null && minIdDepartTrans.DEPARTMENT_IN_TIME.HasValue && inTime > minIdDepartTrans.DEPARTMENT_IN_TIME.Value)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianVaoKhongDuocLonHonThoiGianVao,
                        minIdDepartTrans.DEPARTMENT_NAME,
                        Inventec.Common.DateTime.Convert.TimeNumberToTimeString(minIdDepartTrans.DEPARTMENT_IN_TIME.Value));
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

        internal bool IsExamType(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (!data.TDL_TREATMENT_TYPE_ID.HasValue || data.TDL_TREATMENT_TYPE_ID.Value != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoCoDienDieuTriKhongPhaiLaKham);
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

        internal bool HasNotFinished(HIS_TREATMENT data)
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

        internal bool IsStoreBordereauCodeUsed(HIS_TREATMENT data, string storeBordereauCode)
        {
            bool valid = true;
            try
            {
                if (data.HEIN_LOCK_TIME.HasValue)
                {
                    var heinLockDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.HEIN_LOCK_TIME.Value);
                    long heinLockDateNumberFrom = Convert.ToInt64(heinLockDate.Value.ToString("yyyyMMdd") + "000000");
                    long heinLockDateNumberTo = Convert.ToInt64(heinLockDate.Value.ToString("yyyyMMdd") + "235959");
                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.HEIN_LOCK_TIME_FROM = heinLockDateNumberFrom;
                    filter.HEIN_LOCK_TIME_TO = heinLockDateNumberTo;
                    if (data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        filter.IS_NOI_TRU_TREATMENT_TYPE = true;
                    }
                    else if (data.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        filter.IS_NOI_TRU_TREATMENT_TYPE = false;
                    }
                    List<HIS_TREATMENT> listData = new HisTreatmentGet().Get(filter);
                    listData = listData != null ? listData.Where(o => o.ID != data.ID && o.STORE_BORDEREAU_CODE == storeBordereauCode).ToList() : null;
                    if (listData != null && listData.Count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_MaLuuTruDaDuocSuDung, storeBordereauCode);
                        valid = false;
                    }
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

        internal bool HasTreatmentFinished(long treatmentId, ref HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                data = new HisTreatmentGet().GetById(treatmentId);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    valid = false;
                }
                if (data.IS_PAUSE.HasValue && data.IS_PAUSE.Value == MOS.UTILITY.Constant.IS_TRUE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaKetThucDieuTri);
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

        internal bool CheckRecordInspection(long? serviceReqId)
        {
            bool valid = true;
            try
            {
                if (serviceReqId.HasValue)
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(serviceReqId.Value);
                    if (serviceReq == null)
                    {
                        LogSystem.Error("Ko ton tai service_req tuong ung voi id:" + serviceReqId);
                    }
                    valid = valid && CheckRecordInspection(serviceReq);
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

        internal bool CheckRecordInspection(HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            try
            {
                if (serviceReq != null && !HisServiceReqCFG.IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                    if (treatment != null && treatment.IS_LOCK_HEIN == Constant.IS_TRUE && treatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_HoSoBenhAnDaDuocDuyetKhongChoPhepSuaThongTinXuTri);
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
    }
}
