using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientType;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatient;
using MOS.LibraryHein.Common;
using MOS.MANAGER.HisTreatment;
using AutoMapper;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.Token;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.MANAGER.HisMediOrg;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using Inventec.Common.ObjectChecker;
using System.IO;
using MOS.UTILITY;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using MOS.MANAGER.HisTreatment.Update;


namespace MOS.MANAGER.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterCreate : BusinessBase
    {
        private HIS_PATIENT_TYPE_ALTER recentHisPatientTypeAlter;

        private HisPatientTypeAlterUtil hisPatientTypeAlterUtil;
        private bool generateHeinCardNumber = false;

        internal HisPatientTypeAlterCreate()
            : base()
        {
            this.hisPatientTypeAlterUtil = new HisPatientTypeAlterUtil(param);
        }

        internal HisPatientTypeAlterCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisPatientTypeAlterUtil = new HisPatientTypeAlterUtil(param);
        }

        internal bool Create(HIS_PATIENT_TYPE_ALTER data, ref HIS_PATIENT_TYPE_ALTER resultData)
        {
            bool result = false;
            if (data != null)
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TREATMENT_ID);
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                HisPatientTypeAlterAndTranPatiSDO toCreate = new HisPatientTypeAlterAndTranPatiSDO();
                toCreate.PatientTypeAlter = data;
                toCreate.TransferInFormId = treatment.TRANSFER_IN_FORM_ID;
                toCreate.TransferInIcdCode = treatment.TRANSFER_IN_ICD_CODE;
                toCreate.TransferInIcdName = treatment.TRANSFER_IN_ICD_NAME;
                toCreate.TransferInMediOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                toCreate.TransferInMediOrgName = treatment.TRANSFER_IN_MEDI_ORG_NAME;
                toCreate.TransferInReasonId = treatment.TRANSFER_IN_REASON_ID;
                toCreate.TransferInCode = treatment.TRANSFER_IN_CODE;
                toCreate.TransferInCmkt = treatment.TRANSFER_IN_CMKT;
                toCreate.TransferInTimeFrom = treatment.TRANSFER_IN_TIME_FROM;
                toCreate.TransferInTimeTo = treatment.TRANSFER_IN_TIME_TO;
                result = this.Create(toCreate, treatment, patient, ref resultData, false);
            }
            return result;
        }

        internal bool Create(HisPatientTypeAlterAndTranPatiSDO data, ref HisPatientTypeAlterAndTranPatiSDO resultData)
        {
            bool result = false;
            if (data != null && data.PatientTypeAlter != null)
            {
                HIS_PATIENT_TYPE_ALTER tmp = null;
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.PatientTypeAlter.TREATMENT_ID);
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                HIS_DEPARTMENT_TRAN departmentTran = new HisDepartmentTranGet().GetLastByTreatmentId(treatment.ID, data.PatientTypeAlter.LOG_TIME);

                if (departmentTran == null)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.PatientTypeAlter.LOG_TIME);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ChuaCoThongTinVaoKhoaTruocThoiDiem, time);
                    return false;
                }
                data.PatientTypeAlter.DEPARTMENT_TRAN_ID = departmentTran.ID;

                if (this.Create(data, treatment, patient, ref tmp, false))
                {
                    resultData = new HisPatientTypeAlterAndTranPatiSDO();
                    resultData.PatientTypeAlter = tmp;
                    resultData.TransferInFormId = treatment.TRANSFER_IN_FORM_ID;
                    resultData.TransferInIcdCode = treatment.TRANSFER_IN_ICD_CODE;
                    resultData.TransferInIcdName = treatment.TRANSFER_IN_ICD_NAME;
                    resultData.TransferInMediOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                    resultData.TransferInMediOrgName = treatment.TRANSFER_IN_MEDI_ORG_NAME;
                    resultData.TransferInReasonId = treatment.TRANSFER_IN_REASON_ID;
                    resultData.TransferInCode = treatment.TRANSFER_IN_CODE;
                    resultData.TransferInCmkt = treatment.TRANSFER_IN_CMKT;
                    resultData.TransferInTimeFrom = treatment.TRANSFER_IN_TIME_FROM;
                    resultData.TransferInTimeTo = treatment.TRANSFER_IN_TIME_TO;

                    HisTreatmentLog.Run(treatment.TREATMENT_CODE, tmp, EventLog.Enum.HisPatientTypeAlter_TaoThongTinDienDoiTuong);
                    result = true;
                }
            }
            return result;
        }

        internal bool Create(HIS_PATIENT_TYPE_ALTER data, HIS_TREATMENT treatment, HIS_PATIENT patient, ref HIS_PATIENT_TYPE_ALTER resultData)
        {
            HisPatientTypeAlterAndTranPatiSDO toCreate = new HisPatientTypeAlterAndTranPatiSDO();
            toCreate.PatientTypeAlter = data;
            toCreate.TransferInFormId = treatment.TRANSFER_IN_FORM_ID;
            toCreate.TransferInIcdCode = treatment.TRANSFER_IN_ICD_CODE;
            toCreate.TransferInIcdName = treatment.TRANSFER_IN_ICD_NAME;
            toCreate.TransferInMediOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
            toCreate.TransferInMediOrgName = treatment.TRANSFER_IN_MEDI_ORG_NAME;
            toCreate.TransferInReasonId = treatment.TRANSFER_IN_REASON_ID;
            toCreate.TransferInCode = treatment.TRANSFER_IN_CODE;
            toCreate.TransferInCmkt = treatment.TRANSFER_IN_CMKT;
            toCreate.TransferInTimeFrom = treatment.TRANSFER_IN_TIME_FROM;
            toCreate.TransferInTimeTo = treatment.TRANSFER_IN_TIME_TO;
            return this.Create(toCreate, treatment, patient, ref resultData, true);
        }

        private bool Create(HisPatientTypeAlterAndTranPatiSDO data, HIS_TREATMENT treatment, HIS_PATIENT patient, ref HIS_PATIENT_TYPE_ALTER resultData, bool firstCreating)
        {
            bool result = false;
            try
            {
                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);
                bool valid = true;
                if (!firstCreating)
                {
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    valid = valid && treatmentChecker.IsUnLock(treatment);//chi cho cap nhat khi chua bi khoa
                    valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentChecker.IsUnpause(treatment);//chi cho cap nhat khi chua bi tam khoa
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);//chi cho cap nhat khi chua duyet khoa BH
                    valid = valid && checker.IsValidTime(treatment, data.PatientTypeAlter.LOG_TIME);
                    valid = valid && checker.CheckDepartmentInTime(data.PatientTypeAlter);
                    valid = valid && checker.IsValidOpenNotBhytTreatmentPolicy(patient.ID, treatment.ID, data.PatientTypeAlter);
                }

                if (valid)
                {
                    this.ProcessPatientTypeAlterBHYTImage(data, patient);
                    this.ProcessHisPatientTypeAlter(data.PatientTypeAlter, patient, treatment, firstCreating);

                    //Neu ko phai la lan dau tien thi co xu ly nghiep vu update treatment + tran_pati + update his_sere_serv
                    if (!firstCreating || this.generateHeinCardNumber)
                    {
                        this.hisPatientTypeAlterUtil.ProcessPatientAndTreatment(patient, treatment, data);
                    }

                    //Ko phai lan dau tien tao thi co update thong tin sere_serv
                    if (!firstCreating && !new HisSereServUpdateHein(param, treatment, false).UpdateDb())
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_CapNhatGiaVaThongTinBaoHiemChoDichVuThatBai);
                        return false;
                    }

                    resultData = this.recentHisPatientTypeAlter;
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
                pta.BHYT_URL = newData.BHYT_URL;
            }
        }

        private void ProcessChildrenBhyt(HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER data, HIS_PATIENT patient)
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
                    this.generateHeinCardNumber = true;
                    this.UpdateBhytInfo(data, newData);
                }
                else if (BhytPatientTypeData.IsChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patient.DOB).Value)  && HeinHasBirthCertificateCode.TRUE.Equals(data.HAS_BIRTH_CERTIFICATE))
                {
                    if (data.HEIN_CARD_FROM_TIME.HasValue && !string.IsNullOrWhiteSpace(data.ADDRESS) && !string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_CODE) && !string.IsNullOrWhiteSpace(data.HEIN_MEDI_ORG_NAME))
                    {
                        data.IS_NO_CHECK_EXPIRE = Constant.IS_TRUE;
                    }
                }
            }
        }

        private void ProcessHisPatientTypeAlter(HIS_PATIENT_TYPE_ALTER data, HIS_PATIENT patient, HIS_TREATMENT treatment, bool firstCreating)
        {
            this.ProcessChildrenBhyt(treatment, data, patient);
            this.ProcessQnBhyt(data, patient);
            if (!this.Create(data, treatment, firstCreating))
            {
                throw new Exception("Tao thong tin hisPatientTypeAlter that bai. Nghiep vu tiep theo se khong thuc hien duoc");
            }
        }

        /// <summary>
        /// return url bhyt patientTypeAlter
        /// </summary>
        /// <param name="data"></param>
        /// <param name="patient"></param>
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
                this.generateHeinCardNumber = true;
                this.UpdateBhytInfo(data, newData);
            }
        }

        internal void RollbackData()
        {
            if (this.recentHisPatientTypeAlter != null && !DAOWorker.HisPatientTypeAlterDAO.Truncate(this.recentHisPatientTypeAlter))
            {
                LogSystem.Warn("Truncate du lieu HIS_PATIENT_TYPE_ALTER that bai. ID:" + LogUtil.TraceData("recentHisPatientTypeAlter", this.recentHisPatientTypeAlter));
            }
            this.recentHisPatientTypeAlter = null;
        }

        internal bool Create(HIS_PATIENT_TYPE_ALTER data, HIS_TREATMENT treatment, bool firstCreating)
        {
            bool result = false;
            try
            {
                bool valid = true;
                data.TDL_PATIENT_ID = treatment.PATIENT_ID;//luu du thua du lieu

                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                //Neu la lan dau tien tao patient_type_alter thi ko check treatment_type
                valid = valid && checker.IsValidTreatmentType(data, data.TREATMENT_ID, data.LOG_TIME, firstCreating);
                valid = valid && checker.HasNoOutPrescription(data, null);
                valid = valid && checker.IsUnusedHeinCardNumberByAnother(data.HEIN_CARD_NUMBER, data.TDL_PATIENT_ID);

                if (valid)
                {
                    //Bo sung cac thong tin doi tuong nhung lay theo thong tin chi nhanh
                    this.SetBranchInfo(data);

                    if (!DAOWorker.HisPatientTypeAlterDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeAlter_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientTypeAlter that bai." + LogUtil.TraceData("HisPatientTypeAlter", data));
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

        private void SetBranchInfo(HIS_PATIENT_TYPE_ALTER pta)
        {
            HIS_BRANCH b = new TokenManager(param).GetBranch();

            if (pta != null && pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                && b != null)
            {
                //lay lai du lieu de tranh truong hop nguoi dung co thay doi du lieu chi nhanh sau khi dang nhap
                HIS_BRANCH branch = HisBranchCFG.DATA.Where(o => o.ID == b.ID).FirstOrDefault();

                pta.LEVEL_CODE = branch.HEIN_LEVEL_CODE;
            }
        }
    }
}
