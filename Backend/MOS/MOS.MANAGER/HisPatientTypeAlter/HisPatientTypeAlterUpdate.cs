using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using AutoMapper;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Token;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.MANAGER.HisPatient;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using Inventec.Common.ObjectChecker;
using System.IO;
using MOS.UTILITY;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisTreatment.Update;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterUpdate : BusinessBase
    {
        private List<HIS_PATIENT_TYPE_ALTER> beforeUpdateHisPatientTypeAlters = new List<HIS_PATIENT_TYPE_ALTER>();

        private HIS_PATIENT_TYPE_ALTER beforeUpdateHisPatientTypeAlter;
        private HIS_PATIENT_TYPE_ALTER recentHisPatientTypeAlter;
        private HisPatientTypeAlterUtil hisPatientTypeAlterUtil;

        internal HisPatientTypeAlterUpdate()
            : base()
        {
            this.hisPatientTypeAlterUtil = new HisPatientTypeAlterUtil(param);
        }

        internal HisPatientTypeAlterUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisPatientTypeAlterUtil = new HisPatientTypeAlterUtil(param);
        }

        internal bool Update(HisPatientTypeAlterAndTranPatiSDO data, ref HisPatientTypeAlterAndTranPatiSDO resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);
                bool valid = true;
                valid = valid && treatmentChecker.IsUnLock(data.PatientTypeAlter.TREATMENT_ID, ref treatment);//chi cho cap nhat khi chua bi khoa
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);//chi cho cap nhat khi chua bi tam khoa
                valid = valid && treatmentChecker.IsUnLockHein(treatment);//chi cho cap nhat khi chua duyet khoa BH
                valid = valid && checker.IsValidTime(treatment, data.PatientTypeAlter.LOG_TIME);
                valid = valid && checker.CheckDepartmentInTime(data.PatientTypeAlter);
                valid = valid && checker.IsValidOpenNotBhytTreatmentPolicy(treatment.PATIENT_ID, treatment.ID, data.PatientTypeAlter);
                if (valid)
                {
                    HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                    HIS_PATIENT_TYPE_ALTER patientTypeAlter = new HIS_PATIENT_TYPE_ALTER();

                    this.ProcessPatientTypeAlterBHYTImage(data, patient);//tiennv
                    this.Update(data.PatientTypeAlter, treatment, patient, ref patientTypeAlter);
                    this.hisPatientTypeAlterUtil.ProcessPatientAndTreatment(patient, treatment, data);

                    resultData = new HisPatientTypeAlterAndTranPatiSDO();
                    resultData.PatientTypeAlter = patientTypeAlter;
                    resultData.TransferInFormId = treatment.TRANSFER_IN_FORM_ID;
                    resultData.TransferInIcdCode = treatment.TRANSFER_IN_ICD_CODE;
                    resultData.TransferInIcdName = treatment.TRANSFER_IN_ICD_NAME;
                    resultData.TransferInMediOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                    resultData.TransferInMediOrgName = treatment.TRANSFER_IN_MEDI_ORG_NAME;
                    resultData.TransferInReasonId = treatment.TRANSFER_IN_REASON_ID;
                    resultData.TransferInCmkt = treatment.TRANSFER_IN_CMKT;
                    resultData.TransferInCode = treatment.TRANSFER_IN_CODE;
                    resultData.TransferInTimeFrom = treatment.TRANSFER_IN_TIME_FROM;
                    resultData.TransferInTimeTo = treatment.TRANSFER_IN_TIME_TO;

                    HisTreatmentLog.Run(treatment.TREATMENT_CODE, this.beforeUpdateHisPatientTypeAlter, patientTypeAlter, EventLog.Enum.HisPatientTypeAlter_CapNhatThongTinDienDoiTuong);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessPatientTypeAlterBHYTImage(HisPatientTypeAlterAndTranPatiSDO data, HIS_PATIENT patient)
        {
            try
            {
                List<FileHolder> fileHolders = new List<FileHolder>();
                if (data.ImgBhytData != null && data.ImgBhytData.Length > 0)
                {
                    FileHolder bhytFile = new FileHolder();
                    MemoryStream bhytStream = new MemoryStream();
                    bhytStream.Write(data.ImgBhytData, 0, data.ImgBhytData.Length);
                    bhytStream.Position = 0;
                    bhytFile.Content = bhytStream;
                    bhytFile.FileName = patient.PATIENT_CODE + "_" + Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now) + "_" + Constant.PATIENT_IMG_BHYT + ".jpeg";
                    fileHolders.Add(bhytFile);
                }

                if (IsNotNullOrEmpty(fileHolders))
                {
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.PATIENT, fileHolders, true);
                    if (fileUploadInfos != null && fileUploadInfos.Count == fileHolders.Count)
                    {
                        foreach (FileUploadInfo info in fileUploadInfos)
                        {
                            if (!String.IsNullOrWhiteSpace(info.OriginalName))
                            {
                                if (info.OriginalName.Contains(Constant.PATIENT_IMG_BHYT))
                                {
                                    data.PatientTypeAlter.BHYT_URL = info.Url;
                                }
                            }
                        }
                    }
                    else
                    {
                        LogSystem.Warn("Luu anh len FSS that bai patientTypeAlter: ");
                    }
                }
            }
            catch (Exception ex)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Update(HIS_PATIENT_TYPE_ALTER data, HIS_TREATMENT treatment, HIS_PATIENT patient, ref HIS_PATIENT_TYPE_ALTER resultData)
        {
            this.ProcessChildrenBhyt(data, patient, treatment);
            this.ProcessQnBhyt(data, patient);
            if (!this.Update(data))
            {
                throw new Exception("Cap nhat thong tin hisPatientTypeAlter that bai. Nghiep vu tiep theo se khong thuc hien duoc");
            }

            //tu dong cap nhat thong tin gia va bao hiem cua sere_serv
            //(ko can validate treatment vi da validate o phia tren)
            if (!new HisSereServUpdateHein(param, treatment, false).UpdateDb())
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_CapNhatGiaVaThongTinBaoHiemChoDichVuThatBai);
                throw new Exception();
            }

            resultData = this.recentHisPatientTypeAlter;
        }

        private void UpdateBhytInfo(HIS_PATIENT_TYPE_ALTER pta, HIS_PATIENT_TYPE_ALTER newData)
        {
            if (pta != null && newData != null)
            {
                pta.ADDRESS = newData.ADDRESS;
                pta.HAS_BIRTH_CERTIFICATE = newData.HAS_BIRTH_CERTIFICATE;
                pta.HEIN_CARD_FROM_TIME = newData.HEIN_CARD_FROM_TIME;
                pta.HEIN_CARD_NUMBER = newData.HEIN_CARD_NUMBER;
                pta.HEIN_CARD_TO_TIME = newData.HEIN_CARD_TO_TIME;
                pta.HEIN_MEDI_ORG_CODE = newData.HEIN_MEDI_ORG_CODE;
                pta.HEIN_MEDI_ORG_NAME = newData.HEIN_MEDI_ORG_NAME;
                pta.JOIN_5_YEAR = newData.JOIN_5_YEAR;
                pta.LIVE_AREA_CODE = newData.LIVE_AREA_CODE;
                pta.PAID_6_MONTH = newData.PAID_6_MONTH;
                pta.RIGHT_ROUTE_CODE = newData.RIGHT_ROUTE_CODE;
                pta.RIGHT_ROUTE_TYPE_CODE = newData.RIGHT_ROUTE_TYPE_CODE;
                pta.TDL_PATIENT_ID = newData.TDL_PATIENT_ID;
                pta.IS_TEMP_QN = newData.IS_TEMP_QN;
                pta.PATIENT_TYPE_ID = newData.PATIENT_TYPE_ID;
            }
        }

        private void ProcessChildrenBhyt(HIS_PATIENT_TYPE_ALTER data, HIS_PATIENT patient, HIS_TREATMENT treatment)
        {
            if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
            {
                HIS_BRANCH branch = new TokenManager(param).GetBranch();
                data.LEVEL_CODE = branch.HEIN_LEVEL_CODE;
                //neu ko co so the BHYT thi kiem tra xem he thong co the generate ra du lieu ko
                if (string.IsNullOrWhiteSpace(data.HEIN_CARD_NUMBER))
                {
                    List<HIS_PATIENT_TYPE_ALTER> generated = this.GetHasBirthCertificate();
                    List<HIS_PATIENT_TYPE_ALTER> generatedOfPatients = generated != null ? generated.Where(o => o.TREATMENT_ID == treatment.ID).ToList() : null;
                    HIS_PATIENT_TYPE_ALTER newData = null;
                    //Neu BN da duoc tao the BHYT thi lay lai thong tin the BHYT cu de su dung
                    if (IsNotNullOrEmpty(generatedOfPatients))
                    {
                        newData = generatedOfPatients.OrderByDescending(o => o.ID).FirstOrDefault();
                    }
                    else
                    {
                        List<string> heinCardNumbers = generated != null ? generated.Select(o => o.HEIN_CARD_NUMBER).ToList() : null;

                        DateTime? dateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patient.DOB);
                        Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, BhytPatientTypeData>();
                        BhytPatientTypeData pta = Mapper.Map<BhytPatientTypeData>(data);

                        if (BhytPatientTypeData.IsChild(dateOfBirth.Value)
                            && HeinHasBirthCertificateCode.TRUE.Equals(data.HAS_BIRTH_CERTIFICATE)
                            && String.IsNullOrWhiteSpace(patient.PROVINCE_CODE))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatientTypeAlter_TreEmCoGiayKSCanNhapDuThongTinTinhHuyenXa);
                            throw new Exception("Khong co thong tin ma tinh");
                        }
                        //tu dong sinh so the
                        bool canUpdate = pta.UpdateInCaseOfHavingBirthCertificate(heinCardNumbers, dateOfBirth.Value, patient.DISTRICT_CODE, patient.DISTRICT_NAME, patient.PROVINCE_CODE, patient.PROVINCE_NAME, patient.COMMUNE_NAME, patient.ADDRESS, branch.HEIN_LEVEL_CODE);
                        if (!canUpdate)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ThieuThongTinTheBhyt);
                            throw new Exception("Ko co thong tin the BHYT va ko the tu dong sinh the BHYT." + LogUtil.TraceData("data", data));
                        }
                        Mapper.CreateMap<BhytPatientTypeData, HIS_PATIENT_TYPE_ALTER>();
                        newData = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(pta);
                        //Theo CV 468/BHXH, thoi han the tu lay theo ngay vao vien
                        newData.HEIN_CARD_FROM_TIME = treatment.IN_TIME;
                    }
                    this.UpdateBhytInfo(data, newData);
                }
                else if (BhytPatientTypeData.IsChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patient.DOB).Value) && HeinHasBirthCertificateCode.TRUE.Equals(data.HAS_BIRTH_CERTIFICATE))
                {
                    if (data.HEIN_CARD_FROM_TIME.HasValue && !string.IsNullOrWhiteSpace(data.ADDRESS) && !string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_CODE) && !string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_NAME))
                    {
                        data.IS_NO_CHECK_EXPIRE = Constant.IS_TRUE;
                    }
                }
            }
        }

        private void ProcessQnBhyt(HIS_PATIENT_TYPE_ALTER data, HIS_PATIENT patient)
        {
            if (data.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                && data.IS_TEMP_QN == UTILITY.Constant.IS_TRUE
                && String.IsNullOrWhiteSpace(data.HEIN_CARD_NUMBER))
            {
                if (String.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_CODE) || String.IsNullOrWhiteSpace(data.ADDRESS))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Thieu truong thong tin bat buoc (HEIN_MEDI_ORG_CODE or ADDRESS)");
                }
                HIS_BRANCH branch = new TokenManager(param).GetBranch();
                data.LEVEL_CODE = branch.HEIN_LEVEL_CODE;
                List<HIS_PATIENT_TYPE_ALTER> generated = this.GetHasIsTempQn();
                List<HIS_PATIENT_TYPE_ALTER> generatedOfPatients = generated != null ? generated.Where(o => o.TDL_PATIENT_ID == patient.ID).ToList() : null;
                HIS_PATIENT_TYPE_ALTER newData = null;
                //Neu BN da duoc tao the BHYT thi lay lai thong tin the BHYT cu de su dung
                if (IsNotNullOrEmpty(generatedOfPatients))
                {
                    newData = generatedOfPatients.OrderByDescending(o => o.ID).FirstOrDefault();
                }
                else
                {
                    List<string> heinCardNumbers = generated != null ? generated.Select(o => o.HEIN_CARD_NUMBER).ToList() : null;

                    Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, BhytPatientTypeData>();
                    BhytPatientTypeData pta = Mapper.Map<BhytPatientTypeData>(data);
                    //tu dong sinh so the
                    bool canUpdate = pta.UpdateInCaseOfIsTempQnTrue(heinCardNumbers, branch.HEIN_LEVEL_CODE);
                    if (!canUpdate)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ThieuThongTinTheBhyt);
                        throw new Exception("Ko co thong tin the BHYT va ko the tu dong sinh the BHYT." + LogUtil.TraceData("data", data));
                    }
                    Mapper.CreateMap<BhytPatientTypeData, HIS_PATIENT_TYPE_ALTER>();
                    newData = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(pta);
                }
                newData.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                this.UpdateBhytInfo(data, newData);
            }
        }

        private bool Update(HIS_PATIENT_TYPE_ALTER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_TYPE_ALTER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifySereServPatientType(raw, data);
                valid = valid && checker.HasNoOutPrescription(data, raw);
                valid = valid && checker.IsUnusedHeinCardNumberByAnother(data.HEIN_CARD_NUMBER, data.TDL_PATIENT_ID);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    this.beforeUpdateHisPatientTypeAlter = raw;
                    
                    //che bien lai du lieu dau vao theo dung loai trong th doi tu bhyt sang vp
                    this.ResetBhytInfor(data);

                    if (!DAOWorker.HisPatientTypeAlterDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeAlter_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientTypeAlter that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientTypeAlter = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ResetBhytInfor(HIS_PATIENT_TYPE_ALTER data)
        {
            if (data != null && data.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
            {
                data.ADDRESS = null;
                data.HEIN_CARD_FROM_TIME = null;
                data.HEIN_CARD_NUMBER = null;
                data.HEIN_CARD_TO_TIME = null;
                data.HEIN_MEDI_ORG_CODE = null;
                data.HEIN_MEDI_ORG_NAME = null;
                data.HNCODE = null;
                data.JOIN_5_YEAR = null;
                data.LEVEL_CODE = null;
                data.LIVE_AREA_CODE = null;
                data.PAID_6_MONTH = null;
            }
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE_ALTER> listData, List<HIS_PATIENT_TYPE_ALTER> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }

                if (valid)
                {
                    this.beforeUpdateHisPatientTypeAlters.AddRange(befores);
                    if (!DAOWorker.HisPatientTypeAlterDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeAlter_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HIS_PATIENT_TYPE_ALTER that bai." + LogUtil.TraceData("listData", listData));
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<HIS_PATIENT_TYPE_ALTER> GetHasBirthCertificate()
        {
            HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
            filter.HAS_BIRTH_CERTIFICATE__EXACT = HeinHasBirthCertificateCode.TRUE;
            return new HisPatientTypeAlterGet().Get(filter);
        }

        private List<HIS_PATIENT_TYPE_ALTER> GetHasIsTempQn()
        {
            HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
            filter.IS_TEMP_QN = true;
            return new HisPatientTypeAlterGet().Get(filter);
        }

        internal void RollbackData()
        {
            if (this.beforeUpdateHisPatientTypeAlter != null)
            {
                if (!DAOWorker.HisPatientTypeAlterDAO.Update(this.beforeUpdateHisPatientTypeAlter))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientTypeAlter that bai" + LogUtil.TraceData("HisPatientTypeAlter", this.beforeUpdateHisPatientTypeAlter));
                }
            }
            this.hisPatientTypeAlterUtil.Rollback();
        }
    }
}
