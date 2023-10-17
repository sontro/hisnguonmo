using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientCheck : BusinessBase
    {
        internal HisPatientCheck()
            : base()
        {

        }

        internal HisPatientCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HisPatientProfileSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.HisPatient == null) throw new ArgumentNullException("data.HisPatient");
                if (data.HisPatientTypeAlter == null) throw new ArgumentNullException("data.HisPatientTypeAlter");
                if (data.HisTreatment == null) throw new ArgumentNullException("data.HisTreatment");
                if (data.HisPatient == null) throw new ArgumentNullException("data.HisPatient");
                if (data.HisPatient == null) throw new ArgumentNullException("data.HisPatient");
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

        internal bool VerifyRequireField(HisPatientUpdateCardSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.PatientId <= 0) throw new ArgumentNullException("data.PatientId");
                if (String.IsNullOrWhiteSpace(data.CardCode)) throw new ArgumentNullException("data.CardCode");
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

        internal bool VerifyRequireField(HisPatientUpdateClassifySDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.PatientId <= 0) throw new ArgumentNullException("data.PatientId");
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

        internal bool VerifyRequireField(HIS_PATIENT data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && (data.DOB > 0);
                valid = valid && IsNotNullOrEmpty(data.FIRST_NAME);
                valid = valid && IsGreaterThanZero(data.GENDER_ID);
                if (!valid)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool VerifyRequireField(HisPatientVitaminASDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.HisPatient == null) throw new ArgumentNullException("data.HisPatient");
                if (data.HisVitaminA == null && data.HisVaccinationExam == null) throw new ArgumentNullException("data.HisVitaminA && data.HisVaccinationExam");
                if (data.HisVitaminA != null && data.HisVaccinationExam != null) throw new ArgumentNullException("data.HisVitaminA && data.HisVaccinationExam");
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
        internal bool VerifyId(long id, ref HIS_PATIENT data)
        {
            bool valid = true;
            try
            {
                data = new HisPatientGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_PATIENT> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisPatientFilterQuery filter = new HisPatientFilterQuery();
                    filter.IDs = listId;
                    List<HIS_PATIENT> listData = new HisPatientGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + LogUtil.TraceData("listData", listData) + LogUtil.TraceData("listId", listId), LogType.Error);
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisPatientDAO.ExistsCode(code, id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
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

        internal bool ExistsStoreCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(code))
                {
                    HisPatientFilterQuery filter = new HisPatientFilterQuery();
                    filter.PATIENT_STORE_CODE__EXACT = code;
                    filter.ID__NOT_EQUAL = id;
                    List<HIS_PATIENT> patients = new HisPatientGet().Get(filter);
                    if (IsNotNullOrEmpty(patients))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_SoLuuTruDaTonTai, patients[0].PATIENT_CODE);
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

        internal bool IsUnLock(HIS_PATIENT data)
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_PATIENT> listData)
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisPatientDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
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
                List<HIS_TREATMENT> hisTreatments = new HisTreatmentGet().GetByPatientId(id);
                if (IsNotNullOrEmpty(hisTreatments))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_TREATMENT, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_SERVICE_REQ> hisServiceReqs = new HisServiceReqGet().GetByPatientId(id);
                if (IsNotNullOrEmpty(hisServiceReqs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERVICE_REQ, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_CARD> hisCards = new HisCardGet().GetByPatientId(id);
                if (IsNotNullOrEmpty(hisCards))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCard_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_CARD, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_EXP_MEST> hisExpMests = new HisExpMestGet().GetByPatientId(id);
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_EXP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_PATIENT_TYPE_ALTER> hisPatientTypeAlters = new HisPatientTypeAlterGet().GetByPatientId(id);
                if (IsNotNullOrEmpty(hisPatientTypeAlters))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_PATIENT_TYPE_ALTER, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool HasNotPersonCode(HIS_PATIENT data)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrEmpty(data.PERSON_CODE))
                {
                    valid = false;
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_DaCoMaYTe);
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

        internal bool CheckDuplicateStoreCode(List<HIS_PATIENT> listData)
        {
            bool result = true;
            try
            {
                var listCheck = listData.Where(o => !String.IsNullOrWhiteSpace(o.PATIENT_STORE_CODE)).ToList();
                if (IsNotNullOrEmpty(listCheck))
                {
                    var Groups = listCheck.GroupBy(g => g.PATIENT_STORE_CODE).Where(o => o.Count() >= 2).ToList();
                    if (Groups.Count > 0)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_TonTaiHaiBenhNhanCoCungSoLuuTru);
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

        internal bool IsUnusedHeinCardNumberByAnother(List<HIS_PATIENT> patients)
        {
            bool valid = true;
            try
            {
                List<HIS_PATIENT> hasHeinCards = patients != null ? patients.Where(o => !string.IsNullOrWhiteSpace(o.TDL_HEIN_CARD_NUMBER)).ToList() : null;

                //neu co su dung the thi kiem tra xem the nay da duoc su dung boi benh nhan khac chua
                //Can check ca bang patient va patient_type_alter (vi co truong hop tao ho so benh nhan nhung ko co ho so dieu tri)
                if (IsNotNullOrEmpty(hasHeinCards))
                {
                    List<string> heinCardNumbers = hasHeinCards.Select(o => o.TDL_HEIN_CARD_NUMBER).ToList();
                    List<long> patientIds = hasHeinCards.Select(o => o.ID).ToList();

                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.HEIN_CARD_NUMBER__EXACTs = heinCardNumbers;
                    filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;

                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().Get(filter);

                    MOS.Filter.HisPatientFilter.HeinCardNumberOrId f = new Filter.HisPatientFilter.HeinCardNumberOrId();
                    f.HeinCardNumbers = heinCardNumbers;
                    f.Ids = ptas != null ? ptas.Select(o => o.TDL_PATIENT_ID).ToList() : null;

                    HisPatientFilterQuery patientFilter = new HisPatientFilterQuery();
                    patientFilter.HEIN_CARD_NUMBER__OR__IDs = f;
                    List<HIS_PATIENT> existPatients = new HisPatientGet().Get(patientFilter);

                    if (IsNotNullOrEmpty(existPatients))
                    {
                        foreach (HIS_PATIENT p in hasHeinCards)
                        {
                            List<HIS_PATIENT> anotherPatients = existPatients != null ? existPatients.Where(o => o.TDL_HEIN_CARD_NUMBER == p.TDL_HEIN_CARD_NUMBER
                                && o.ID != p.ID).ToList() : null;

                            List<HIS_PATIENT_TYPE_ALTER> anotherPatientTypeAlters = ptas
                                .Where(o => o.TDL_PATIENT_ID != p.ID && o.HEIN_CARD_NUMBER == p.TDL_HEIN_CARD_NUMBER)
                                .ToList();

                            List<string> anotherCodes = new List<string>();

                            if (IsNotNullOrEmpty(anotherPatients))
                            {
                                anotherCodes.AddRange(anotherPatients.Select(o => o.PATIENT_CODE).ToList());
                            }

                            if (IsNotNullOrEmpty(anotherPatientTypeAlters))
                            {
                                List<string> tmp = existPatients.Where(t => anotherPatientTypeAlters.Exists(o => o.TDL_PATIENT_ID == t.ID)).Select(o => o.PATIENT_CODE).ToList();
                                anotherCodes.AddRange(tmp);
                            }

                            if (IsNotNullOrEmpty(anotherCodes))
                            {
                                anotherCodes = anotherCodes.Distinct().ToList();
                                string patientCodes = string.Join(", ", anotherCodes);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TheNayDaDuocSuDungBoiBenhNhanKhac, p.TDL_HEIN_CARD_NUMBER, patientCodes);
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
    }
}
