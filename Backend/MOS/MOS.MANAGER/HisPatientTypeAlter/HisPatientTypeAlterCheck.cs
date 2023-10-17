using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.MANAGER.HisMediOrg;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisTreatment;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    class HisPatientTypeAlterCheck : BusinessBase
    {
        internal HisPatientTypeAlterCheck()
            : base()
        {

        }

        internal HisPatientTypeAlterCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.PATIENT_TYPE_ID)) throw new ArgumentNullException("data.PATIENT_TYPE_ID");
                if (!IsNotNull(data.TDL_PATIENT_ID)) throw new ArgumentNullException("data.TDL_PATIENT_ID");//bat buoc phai xu ly de set truong nay vao truoc khi luu vao DB
                if (!IsGreaterThanZero(data.TREATMENT_ID)) throw new ArgumentNullException("data.TREATMENT_ID");
                if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK && !data.KSK_CONTRACT_ID.HasValue) throw new ArgumentNullException("Thieu thong tin Hop dong (KSK_CONTRACT_ID)");

                if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && !this.IsValidBhytInfo(data))
                {
                    return false;//return chu ko tra ve exception do ham tren da tra ve message
                }
                if (HeinHasBirthCertificateCode.TRUE.Equals(data.HAS_BIRTH_CERTIFICATE) && !string.IsNullOrWhiteSpace(data.HEIN_CARD_NUMBER))
                {
                    if (!IsNotNull(data.HEIN_CARD_FROM_TIME)) throw new ArgumentNullException("data.HEIN_CARD_FROM_TIME");
                    if (string.IsNullOrWhiteSpace(data.ADDRESS)) throw new ArgumentNullException("data.ADDRESS");//bat buoc phai xu ly de set truong nay vao truoc khi luu vao DB
                    if (string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_CODE)) throw new ArgumentNullException("data.HEIN_MEDI_ORG_CODE");
                    if (string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_NAME)) throw new ArgumentNullException("data.HEIN_MEDI_ORG_NAME");
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

        /// <summary>
        /// Kiem tra thong tin BHYT co hop le khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsValidBhytInfo(HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = false;
            if (data != null)
            {
                valid = true;
                if (string.IsNullOrWhiteSpace(data.HEIN_CARD_NUMBER))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongCoThongTinSoThe);
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(data.ADDRESS))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongCoThongTinDiaChiThe);
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_CODE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongCoThongTinMaDkKcbBd);
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_NAME))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongCoThongTinTenDkKcbBd);
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(data.LEVEL_CODE))
                {
                    LogSystem.Warn("data.LEVEL_CODE null");
                    valid = false;
                }
                if (string.IsNullOrWhiteSpace(data.RIGHT_ROUTE_CODE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongCoThongTinDungTuyenTraiTuyen);
                    valid = false;
                }
                if (data.HAS_BIRTH_CERTIFICATE != HeinHasBirthCertificateCode.TRUE && !data.IS_NO_CHECK_EXPIRE.HasValue && (!data.HEIN_CARD_FROM_TIME.HasValue || !data.HEIN_CARD_TO_TIME.HasValue))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ThieuThongTinHanThe);
                    valid = false;
                }
                if (data.HAS_BIRTH_CERTIFICATE != HeinHasBirthCertificateCode.TRUE && !HisMediOrgGet.IsValidCode(data.HEIN_MEDI_ORG_CODE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_MaDkKcbBdKhongHopLe);
                    valid = false;
                }
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra su ton tai cua id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id)
        {
            bool valid = true;
            try
            {
                if (new HisPatientTypeAlterGet().GetById(id) == null)
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
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = true;
            try
            {
                data = new HisPatientTypeAlterGet().GetById(id);
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
        /// Kiem tra su ton tai cua danh sach cac id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PATIENT_TYPE_ALTER> listData = new HisPatientTypeAlterGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
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
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_PATIENT_TYPE_ALTER> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PATIENT_TYPE_ALTER> listData = new HisPatientTypeAlterGet().Get(filter);
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
        internal bool IsUnLock(HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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
                if (!DAOWorker.HisPatientTypeAlterDAO.IsUnLock(id))
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PATIENT_TYPE_ALTER> listData = new HisPatientTypeAlterGet().Get(filter);
                    if (listData != null && listData.Count > 0)
                    {
                        foreach (var data in listData)
                        {
                            if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_PATIENT_TYPE_ALTER> listData)
        {
            bool valid = true;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    foreach (var data in listData)
                    {
                        if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        internal bool IsValidTreatmentType(HIS_PATIENT_TYPE_ALTER data, long treatmentId, long logTime, bool firstCreating)
        {
            bool valid = true;
            try
            {
                //neu patient_type_alter dau tien cua treatment thi ko can check
                if (!firstCreating)
                {
                    HIS_PATIENT_TYPE_ALTER patientTypeAlter = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatmentId, logTime);
                    if (patientTypeAlter != null)
                    {
                        if (patientTypeAlter.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                            && data.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_DienDoiTuongTruocDoLaDieuTriKhongChoPhepTaoMoiDienDoiTuongLaKham);
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

        internal bool HasNoOutPrescription(HIS_PATIENT_TYPE_ALTER newData, HIS_PATIENT_TYPE_ALTER oldData)
        {
            bool valid = true;
            try
            {
                if ((newData.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                    && newData.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                    && (oldData == null || newData.TREATMENT_TYPE_ID != oldData.TREATMENT_TYPE_ID))
                {
                    valid = this.HasNoOutPrescription(newData.TREATMENT_ID, newData.TREATMENT_TYPE_ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNoOutPrescription(long treatmentId, long treatmentTypeId)
        {
            bool valid = true;
            try
            {
                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                ssFilter.TREATMENT_ID = treatmentId;
                ssFilter.TDL_SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;
                ssFilter.IS_EXPEND = false;
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().Get(ssFilter);
                hisSereServs = hisSereServs != null ? hisSereServs.Where(o => o.AMOUNT > 0).ToList() : null;

                if (IsNotNullOrEmpty(hisSereServs))
                {
                    List<long> serviceReqIds = hisSereServs.Select(s => s.SERVICE_REQ_ID ?? 0).Distinct().ToList();
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByServiceReqIds(serviceReqIds);
                    List<string> expMestCodes = expMests != null ? expMests.Select(s => s.EXP_MEST_CODE).ToList() : null;
                    string expMestCodeStr = string.Join(",", expMestCodes);
                    var treatmentType = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == treatmentTypeId);
                    if (IsNotNull(treatmentType) && treatmentType.ALLOW_HOSPITALIZE_WHEN_PRES == Constant.IS_TRUE)
                    {
                        valid = true;
                    }
                    else
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_DaTonTaiDonPhongKham, expMestCodeStr);
                        valid = false;
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

        internal bool HasNoOutPrescription(long treatmentId)
        {
            bool valid = true;
            try
            {
                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                ssFilter.TREATMENT_ID = treatmentId;
                ssFilter.TDL_SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;
                ssFilter.IS_EXPEND = false;
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().Get(ssFilter);
                hisSereServs = hisSereServs != null ? hisSereServs.Where(o => o.AMOUNT > 0).ToList() : null;

                if (IsNotNullOrEmpty(hisSereServs))
                {
                    List<long> serviceReqIds = hisSereServs.Select(s => s.SERVICE_REQ_ID ?? 0).Distinct().ToList();
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByServiceReqIds(serviceReqIds);
                    List<string> expMestCodes = expMests != null ? expMests.Select(s => s.EXP_MEST_CODE).ToList() : null;
                    string expMestCodeStr = string.Join(",", expMestCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_DaTonTaiDonPhongKham, expMestCodeStr);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifySereServPatientType(HIS_PATIENT_TYPE_ALTER oldData, HIS_PATIENT_TYPE_ALTER newData)
        {
            bool valid = true;
            try
            {
                //Lay ra danh sach HIS_PATIENT_TYPE_ALTER cua ho so do
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(oldData.TREATMENT_ID);
                List<long> kskContractIds = ptas != null ? ptas.Where(o => o.KSK_CONTRACT_ID.HasValue && o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK).Select(o => o.KSK_CONTRACT_ID.Value).ToList() : null;
                if (newData != null && newData.KSK_CONTRACT_ID.HasValue && newData.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK)
                {
                    kskContractIds.Add(newData.KSK_CONTRACT_ID.Value);
                }

                List<HIS_KSK_CONTRACT> kskContracts = new HisKskContractGet().GetByIds(kskContractIds);

                //Thuc hien cap nhat lai danh sach ptas duoc khi check tiep nghiep vu tiep theo

                //Neu newData != null --> cap nhat du lieu
                //Neu newData == null --> xoa du lieu
                HIS_PATIENT_TYPE_ALTER old = ptas.Where(o => o.ID == oldData.ID).FirstOrDefault();
                ptas.Remove(old);
                if (newData != null)
                {
                    ptas.Remove(old);
                    ptas.Add(newData);
                }

                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByTreatmentId(oldData.TREATMENT_ID);

                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    foreach (HIS_SERE_SERV s in hisSereServs)
                    {
                        HIS_PATIENT_TYPE_ALTER usingPta = null;
                        bool allow = sereServChecker.HasAppliedPatientTypeAlter(s.TDL_INTRUCTION_TIME, ptas, ref usingPta)
                            && sereServChecker.IsAllowUsingPatientType(kskContracts, usingPta, s.SERVICE_ID, s.PATIENT_TYPE_ID, s.TDL_INTRUCTION_TIME);
                        if (!allow)
                        {
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

        internal bool IsValidTime(HIS_TREATMENT treatment, long time)
        {
            bool valid = true;
            try
            {
                if (time < treatment.IN_TIME)
                {
                    string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.IN_TIME);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ThoiGianKhongDuocNhoHonThoiGianVaoVien, inTime);
                    return false;
                }
                if (treatment.OUT_TIME.HasValue && time > treatment.OUT_TIME.Value)
                {
                    string outTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ThoiGianKhongDuocLonHonThoiGianRaVien, outTime);
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

        internal bool CheckDepartmentInTime(HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = true;
            try
            {
                HIS_DEPARTMENT_TRAN dt = new HisDepartmentTranGet().GetById(data.DEPARTMENT_TRAN_ID);
                if (dt != null)
                {
                    if (dt.DEPARTMENT_IN_TIME.HasValue && dt.DEPARTMENT_IN_TIME.Value > data.LOG_TIME)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatientTypeAlter_ThoiGianXacLapDoiTuongNhoHonThoiGianVaoKhoa);
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

        internal bool IsUnusedHeinCardNumberByAnother(string heinCardNumber, long patientId)
        {
            bool valid = true;
            try
            {
                //neu co su dung the thi kiem tra xem the nay da duoc su dung boi benh nhan khac chua
                if (IsNotNullOrEmpty(heinCardNumber))
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.HEIN_CARD_NUMBER__EXACT = heinCardNumber;
                    //luu y: kem theo filter nay de tranh phai truy van nhieu du lieu
                    filter.TDL_PATIENT_ID__NOT_EQUAL = patientId;

                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().Get(filter);

                    if (IsNotNullOrEmpty(ptas))
                    {
                        List<long> patientIds = ptas.Select(o => o.TDL_PATIENT_ID).ToList();
                        List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                        List<string> anotherUses = patients != null ? patients.Select(o => o.PATIENT_CODE).ToList() : null;
                        if (IsNotNullOrEmpty(anotherUses))
                        {
                            string patientCodes = string.Join(", ", anotherUses);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TheNayDaDuocSuDungBoiBenhNhanKhac, heinCardNumber, patientCodes);
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

        internal bool CheckSyncInfoBhyt(HIS_PATIENT_TYPE_ALTER data)
        {
            bool valid = true;
            try
            {
                bool isSync = true;
                if (data != null && data.PATIENT_TYPE_ID == Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.TREATMENT_ID = data.TREATMENT_ID;
                    filter.PATIENT_TYPE_ID = data.PATIENT_TYPE_ID;
                    filter.ID__NOT_EQUAL = data.ID;
                    filter.HEIN_CARD_NUMBER__EXACT = data.HEIN_CARD_NUMBER;
                    List<HIS_PATIENT_TYPE_ALTER> hisPatientTypeAlters = new HisPatientTypeAlterGet().Get(filter);
                    if (IsNotNullOrEmpty(hisPatientTypeAlters))
                    {
                        foreach (HIS_PATIENT_TYPE_ALTER item in hisPatientTypeAlters)
                        {
                            isSync = isSync && ((data.RIGHT_ROUTE_CODE ?? "") == (item.RIGHT_ROUTE_CODE ?? "") && (data.RIGHT_ROUTE_TYPE_CODE ?? "") == (item.RIGHT_ROUTE_TYPE_CODE ?? "") && (data.LIVE_AREA_CODE ?? "") == (item.LIVE_AREA_CODE ?? "") && (data.HEIN_MEDI_ORG_CODE ?? "") == (item.HEIN_MEDI_ORG_CODE ?? "") && (data.JOIN_5_YEAR ?? "") == (item.JOIN_5_YEAR ?? "") && (data.PAID_6_MONTH ?? "") == (item.PAID_6_MONTH ?? ""));
                        }
                    }
                    if (!isSync)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatientTypeAlter_BenhNhanCoThongTinDienDoiTuongKhongDongNhat);
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

        internal bool IsValidOpenNotBhytTreatmentPolicy(long patientId, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER pta)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.UNLIMIT_WITH_NOT_BHYT_TREATMENT && IsNotNull(treatment))
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var NoitruHoacBanNgay = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (!o.IS_EMERGENCY.HasValue || o.IS_EMERGENCY.Value != Constant.IS_TRUE)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                                ).ToList();

                        var NgoaiTru = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (!o.IS_EMERGENCY.HasValue || o.IS_EMERGENCY.Value != Constant.IS_TRUE)
                                && o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                                ).ToList();

                        List<HIS_TREATMENT> exists = new List<HIS_TREATMENT>();
                        if (IsNotNullOrEmpty(NoitruHoacBanNgay))
                        {
                            exists.AddRange(NoitruHoacBanNgay);
                        }
                        if(IsNotNullOrEmpty(NgoaiTru))
                        {
                            exists.AddRange(NgoaiTru);
                        }
                        if (IsNotNullOrEmpty(exists) 
                            &&(((IsNotNullOrEmpty(NgoaiTru) && treatment.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM) || IsNotNullOrEmpty(NoitruHoacBanNgay))
                            && treatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            && (!treatment.IS_EMERGENCY.HasValue || treatment.IS_EMERGENCY != Constant.IS_TRUE)))
                        {
                            List<string> AllMess = new List<string>();

                            //lay ra danh sach ho so co dien dieu tri khac kham từ danh sach ho so dang mo(exists)
                            List<HIS_TREATMENT> treatments = exists.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatments))
                            {
                                List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }

                            exists = exists.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in exists)
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
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongChoPhepMoHonMotHoSoBHYTMaKhongPhaiCapCuu, string.Join("; ", AllMess));
                                return false;
                            }
                        }
                    }
                }

                else if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.HOSPITALIZED_OR_TRANSFERRED && IsNotNull(pta) && pta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM && pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var NgoaiTruHoacNoitruHoacBanNgay = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (o.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                                && (o.ID != pta.TREATMENT_ID)
                                ).ToList();

                        if (IsNotNullOrEmpty(NgoaiTruHoacNoitruHoacBanNgay))
                        {
                            List<string> AllMessNgoaiTruHoacNoitruHoacBanNgay = new List<string>();
                            List<HIS_TREATMENT> treatmentNgoaiTruHoacNoitruHoacBanNgays = NgoaiTruHoacNoitruHoacBanNgay.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatmentNgoaiTruHoacNoitruHoacBanNgays))
                            {
                                List<long> treatmentIds = treatmentNgoaiTruHoacNoitruHoacBanNgays.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }

                            NgoaiTruHoacNoitruHoacBanNgay = NgoaiTruHoacNoitruHoacBanNgay.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in NgoaiTruHoacNoitruHoacBanNgay)
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
                                    AllMessNgoaiTruHoacNoitruHoacBanNgay.Add(bedRoomMess);
                                }
                            }

                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatientTypeAlter_BenhNhanCoHoSoDieuTriBhytChuaKetThuc, string.Join("; ", AllMessNgoaiTruHoacNoitruHoacBanNgay));
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

        internal bool IsValidOpenNotBhytTreatmentPolicy(long patientId, long currentTreatmentId, HIS_PATIENT_TYPE_ALTER pta)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.UNLIMIT_WITH_NOT_BHYT_TREATMENT && IsNotNull(pta))
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var NoitruHoacBanNgay = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (!o.IS_EMERGENCY.HasValue || o.IS_EMERGENCY.Value != Constant.IS_TRUE)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                                && o.ID != currentTreatmentId).ToList();

                        var NgoaiTru = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (!o.IS_EMERGENCY.HasValue || o.IS_EMERGENCY.Value != Constant.IS_TRUE)
                                && o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                                && o.ID != currentTreatmentId).ToList();

                        List<HIS_TREATMENT> exists = new List<HIS_TREATMENT>();
                        if (IsNotNullOrEmpty(NoitruHoacBanNgay))
                        {
                            exists.AddRange(NoitruHoacBanNgay);
                        }
                        if (IsNotNullOrEmpty(NgoaiTru))
                        {
                            exists.AddRange(NgoaiTru);
                        }

                        if (IsNotNullOrEmpty(exists)
                            && (((IsNotNullOrEmpty(NgoaiTru) && pta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM) || IsNotNullOrEmpty(NoitruHoacBanNgay))
                            && pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            && pta.RIGHT_ROUTE_TYPE_CODE != HeinRightRouteTypeCode.EMERGENCY))
                        {
                            List<string> AllMess = new List<string>();

                            //lay ra danh sach ho so co dien dieu tri khac kham từ danh sach ho so dang mo(exists)
                            List<HIS_TREATMENT> treatments = exists.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatments))
                            {
                                List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }

                            exists = exists.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in exists)
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
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongChoPhepMoHonMotHoSoBHYTMaKhongPhaiCapCuu, string.Join("; ", AllMess));
                                return false;
                            }
                        }
                    }
                }

                else if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.HOSPITALIZED_OR_TRANSFERRED && IsNotNull(pta) && pta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM && pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var NgoaiTruHoacNoitruHoacBanNgay = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (o.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                                && (o.ID != pta.TREATMENT_ID)
                                ).ToList();

                        if (IsNotNullOrEmpty(NgoaiTruHoacNoitruHoacBanNgay))
                        {
                            List<string> AllMessNgoaiTruHoacNoitruHoacBanNgay = new List<string>();
                            List<HIS_TREATMENT> treatmentNgoaiTruHoacNoitruHoacBanNgays = NgoaiTruHoacNoitruHoacBanNgay.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatmentNgoaiTruHoacNoitruHoacBanNgays))
                            {
                                List<long> treatmentIds = treatmentNgoaiTruHoacNoitruHoacBanNgays.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }
                            NgoaiTruHoacNoitruHoacBanNgay = NgoaiTruHoacNoitruHoacBanNgay.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in NgoaiTruHoacNoitruHoacBanNgay)
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
                                    AllMessNgoaiTruHoacNoitruHoacBanNgay.Add(bedRoomMess);
                                }
                            }

                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatientTypeAlter_BenhNhanCoHoSoDieuTriBhytChuaKetThuc, string.Join("; ", AllMessNgoaiTruHoacNoitruHoacBanNgay));
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

        internal bool IsValidOpenNotBhytTreatmentPolicy2(long patientId, HIS_TREATMENT currentTreatment, HIS_PATIENT_TYPE_ALTER lastPta, HisDepartmentTranHospitalizeSDO data)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.UNLIMIT_WITH_NOT_BHYT_TREATMENT && IsNotNull(currentTreatment))
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var exists = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (!o.IS_EMERGENCY.HasValue || o.IS_EMERGENCY.Value != Constant.IS_TRUE)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                                && o.ID != currentTreatment.ID).ToList();

                        if (IsNotNullOrEmpty(exists)
                            && currentTreatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            && (!currentTreatment.IS_EMERGENCY.HasValue || currentTreatment.IS_EMERGENCY != Constant.IS_TRUE))
                        {
                            List<string> AllMess = new List<string>();

                            //lay ra danh sach ho so co dien dieu tri khac kham từ danh sach ho so dang mo(exists)
                            List<HIS_TREATMENT> treatments = exists.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatments))
                            {
                                List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }

                            exists = exists.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in exists)
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
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongChoPhepTaoHonMotHoSoBHYTMaKhongPhaiCapCuu, string.Join("; ", AllMess));
                                return false;
                            }
                        }
                    }
                }

                else if (HisTreatmentCFG.ALLOW_MANY_TREATMENT_OPENING_OPTION == HisTreatmentCFG.AlowManyTreatmentOpeningOption.HOSPITALIZED_OR_TRANSFERRED && IsNotNull(currentTreatment) && (IsNotNull(data) || (currentTreatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)) && currentTreatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    List<HIS_TREATMENT> olds = new HisTreatmentGet().GetByPatientId(patientId);
                    if (IsNotNullOrEmpty(olds))
                    {
                        var exists = olds.Where(
                            o => (!o.IS_PAUSE.HasValue || o.IS_PAUSE.Value != Constant.IS_TRUE)
                                && (lastPta.TDL_PATIENT_ID == patientId)
                                && (lastPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                && (o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                                && (o.ID != lastPta.TREATMENT_ID)
                                ).ToList();

                        if (IsNotNullOrEmpty(exists))
                        {
                            List<string> AllMess = new List<string>();

                            //lay ra danh sach ho so co dien dieu tri khac kham từ danh sach ho so dang mo(exists)
                            List<HIS_TREATMENT> treatments = exists.Where(o => o.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM).ToList();
                            List<V_HIS_TREATMENT_BED_ROOM_1> bedRooms = null;
                            if (IsNotNullOrEmpty(treatments))
                            {
                                List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                                HisTreatmentBedRoomView1FilterQuery filter = new HisTreatmentBedRoomView1FilterQuery();
                                filter.TREATMENT_IDs = treatmentIds;
                                bedRooms = new HisTreatmentBedRoomGet().GetView1(filter);
                            }

                            exists = exists.OrderByDescending(o => o.CREATE_TIME).ToList();
                            foreach (var item in exists)
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
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_BenhNhanCoHoSoDieuTriBhytChuaKetThuc, string.Join("; ", AllMess));
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

        internal bool IsValidForChangePatient(long treatmentId, HIS_PATIENT oldPatient, HIS_PATIENT newPatient)
        {
            bool valid = true;
            try
            {
                List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(treatmentId);
                if (IsNotNullOrEmpty(patientTypeAlters))
                {
                    List<string> heinCardNumbers = patientTypeAlters.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).Select(o => o.HEIN_CARD_NUMBER).ToList();

                    if (IsNotNullOrEmpty(heinCardNumbers) && HisTreatmentCFG.IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT)
                    {
                        HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                        filter.TDL_PATIENT_ID = oldPatient.ID;
                        filter.HEIN_CARD_NUMBER__EXACTs = heinCardNumbers;
                        filter.TREATMENT_ID__NOT_EQUAL = treatmentId;
                        List<HIS_PATIENT_TYPE_ALTER> otherPtas = new HisPatientTypeAlterGet().Get(filter);
                        if (IsNotNullOrEmpty(otherPtas))
                        {
                            string heinCardNumberStr = string.Join(",", heinCardNumbers);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_NeuDoiThongTinSeDanDen2BenhNhanCoCungTheBhyt, oldPatient.PATIENT_CODE, newPatient.PATIENT_CODE, heinCardNumberStr);
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
    }
}
