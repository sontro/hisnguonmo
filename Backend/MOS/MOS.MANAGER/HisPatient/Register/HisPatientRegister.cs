using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisHoldReturn;
using MOS.MANAGER.HisDocHoldType;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MOS.MANAGER.HisDepartmentTran.Create;

namespace MOS.MANAGER.HisPatient.Register
{
    /// <summary>
    /// Xu ly nghiep vu dang ky dich vu -> tao cac thong tin ho so benh nhan
    /// </summary>
    class HisPatientRegister : BusinessBase
    {
        #region Cac du lieu vua thuc hien tao moi hoac cap nhat
        private HIS_PATIENT recentHisPatient;
        private HIS_PATIENT_TYPE_ALTER recentHisPatientTypeAlter;
        private HIS_DEPARTMENT_TRAN recentHisDepartmentTran;
        private HIS_TREATMENT recentHisTreatment;
        #endregion

        private HisPatientUpdate hisPatientUpdate;
        private HisPatientCreate hisPatientCreate;
        private HisTreatmentCreate hisTreatmentCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisDepartmentTranCreate hisDepartmentTranCreate;
        private HisPatientUpdate hisPatientUpdateImage;
        private HisCardUpdate hisCardUpdate;
        private HisCardCreate hisCardCreate;

        private bool isNewPatient;

        internal HisPatientRegister()
            : base()
        {
            this.Init();
        }

        internal HisPatientRegister(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisPatientCreate = new HisPatientCreate(param);
            this.hisTreatmentCreate = new HisTreatmentCreate(param);
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
            this.hisPatientUpdateImage = new HisPatientUpdate(param);
            this.hisCardCreate = new HisCardCreate(param);
            this.hisCardUpdate = new HisCardUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        /// <summary>
        /// Dang ky thong tin ho so. Viec dang ky se bao gom insert hoac update. Nghiep vu bao gom cac buoc sau:
        /// - Insert hoac update du lieu Benh nhan (his_patient)
        /// - Insert du lieu The BHYT (patient_type_alter)
        /// - Insert du lieu Ho so dieu tri (treatment)
        /// - Insert du lieu Chuyen khoa (department_tran)
        /// Luu y:
        /// * Insert neu chua ton tai du lieu (client ko gui len thong tin ID)
        /// * Update neu da co du lieu (client gui len co thong tin ID)
        /// </summary>
        internal bool RegisterProfile(HisPatientProfileSDO data, HIS_CARD hisCard, bool hasExam)
        {
            bool result = true;
            try
            {
                WorkPlaceSDO workPlace = null;
                bool valid = true;
                HisPatientCheck checker = new HisPatientCheck(param);
                HisPatientRegisterCheck registerChecker = new HisPatientRegisterCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.VerifyRequireField(data);

                //Neu dang ky kham, thi viec check nay da check o ham xu ly kham
                valid = valid && (hasExam || registerChecker.IsValidCardInfo(data, ref hisCard));
                valid = valid && (hasExam || registerChecker.IsValidInCode(data));
                
                if (valid)
                {
                    data.HisPatientTypeAlter.LOG_TIME = data.TreatmentTime;
                    data.HisTreatment.IN_TIME = data.TreatmentTime;
                    data.HisTreatment.BRANCH_ID = workPlace.BranchId;
                    this.isNewPatient = data.HisPatient.ID <= 0;

                    this.ProcessHisPatient(data);
                    this.ProcessHisCard(data, hisCard);
                    this.ProcessHisTreatment(data, workPlace);
                    this.ProcessHisDepartmentTran(data);
                    this.ProcessHisPatientTypeAlter(data);
                    this.PassResult(data);

                    this.ProccessBHYTHolded(data.HisTreatment.IS_BHYT_HOLDED, data.HisTreatment.ID, data.HisTreatment.PATIENT_ID, data.HisTreatment.TDL_HEIN_CARD_NUMBER, workPlace.RoomId);


                    if (data != null && data.HisTreatment != null)
                    {
                        new HisTreatment.Util.HisTreatmentUploadEmr().Run(data.HisTreatment.ID);
                    }

                    //Khong co thong tin kham thi ghi nhat ky tac dong                    
                    if (!hasExam)
                    {
                        HisTreatmentLog.Run(data.HisPatient, data.HisTreatment, data.HisPatientTypeAlter, EventLog.Enum.HisTreatment_DangKyTiepDonKhongKham);
                    }

                    this.InitThreadSCreateMS(data.HisPatient);
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

        private void ProccessBHYTHolded(short? isBHYTHolded, long treatmentId, long patientId, string heinCardNumber, long roomId)
        {
            if (isBHYTHolded.HasValue && isBHYTHolded.Value == Constant.IS_TRUE)
            {
                HisDocHoldTypeFilterQuery filter = new HisDocHoldTypeFilterQuery();
                filter.IS_HEIN_CARD = Constant.IS_TRUE;
                var docHoldType = new HisDocHoldTypeGet().Get(filter);

                if (IsNotNullOrEmpty(docHoldType))
                {
                    HIS_HOLD_RETURN holdReturn = new HIS_HOLD_RETURN();
                    holdReturn.TREATMENT_ID = treatmentId;
                    holdReturn.PATIENT_ID = patientId;
                    holdReturn.HOLD_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    holdReturn.HOLD_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    holdReturn.HOLD_ROOM_ID = roomId;
                    holdReturn.RESPONSIBLE_ROOM_ID = roomId;
                    holdReturn.HEIN_CARD_NUMBER = heinCardNumber;
                    if (!DAOWorker.HisHoldReturnDAO.Create(holdReturn))
                    {
                        LogSystem.Error("Tao du lieu HIS_HOLD_RETURN khi tiep don giu the bao hiem y te that bai");
                    }

                    HIS_HORE_DHTY horeDhty = new HIS_HORE_DHTY();
                    horeDhty.DOC_HOLD_TYPE_ID = docHoldType.FirstOrDefault().ID;
                    horeDhty.HOLD_RETURN_ID = holdReturn.ID;
                    if (!DAOWorker.HisHoreDhtyDAO.Create(horeDhty))
                    {
                        LogSystem.Error("Tao du lieu HIS_HORE_DHTY khi tiep don giu the bao hiem y te that bai");
                    }
                }
                else
                {
                    LogSystem.Error("Khong lay duoc loai giay to giu khi tao HIS_HORE_DHTY khi tiep don giu the bao hiem y te that bai");
                }
            }
        }

        private void InitThreadSCreateMS(HIS_PATIENT patient)
        {
            try
            {
                if (CosCFG.IS_CREATE_REGISTER_CODE && IsNotNull(patient) && string.IsNullOrEmpty(patient.REGISTER_CODE))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.CreateMS));
                    thread.Priority = ThreadPriority.BelowNormal;
                    thread.Start(patient.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void CreateMS(object data)
        {
            try
            {
                long patientId = (long)data;
                new HisPatientCreateRegisterCode().Run(patientId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessHisCard(HisPatientProfileSDO data, HIS_CARD hisCard)
        {
            if (hisCard != null)
            {
                hisCard.PATIENT_ID = this.recentHisPatient.ID;
                if (!this.hisCardUpdate.Update(hisCard))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
            else if (!string.IsNullOrWhiteSpace(data.CardCode))
            {
                hisCard = new HIS_CARD();
                hisCard.CARD_CODE = data.CardCode;
                hisCard.SERVICE_CODE = data.CardServiceCode;
                hisCard.PATIENT_ID = this.recentHisPatient.ID;
                hisCard.BANK_CARD_CODE = data.BankCardCode;

                if (!this.hisCardCreate.Create(hisCard))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Insert hoac update du lieu Benh nhan (his_patient)
        /// </summary>
        /// <param name="patientDTO"></param>
        private void ProcessHisPatient(HisPatientProfileSDO data)
        {
            
            //bo sung district_code vao HIS_PATIENT
            data.HisPatient.DISTRICT_CODE = data.DistrictCode;
            data.HisPatient.PROVINCE_CODE = data.ProvinceCode;
            if(!string.IsNullOrWhiteSpace(data.HisPatient.FIRST_NAME))
            {
                data.HisPatient.FIRST_NAME = data.HisPatient.FIRST_NAME.ToUpper();
            }
            if (!string.IsNullOrWhiteSpace(data.HisPatient.LAST_NAME))
            {
                data.HisPatient.LAST_NAME = data.HisPatient.LAST_NAME.ToUpper();
            }
            if (!String.IsNullOrWhiteSpace(data.HisPatientTypeAlter.HEIN_CARD_NUMBER) && data.HisPatient.TDL_HEIN_CARD_NUMBER != data.HisPatientTypeAlter.HEIN_CARD_NUMBER)
            {
                data.HisPatient.TDL_HEIN_CARD_NUMBER = data.HisPatientTypeAlter.HEIN_CARD_NUMBER;
            }
            if (IsNotNullOrEmpty(data.CardCode))
            {
                data.HisPatient.HAS_CARD = Constant.IS_TRUE;
            }
            HisPatientUtil.SetOwnBranhIds(data.HisPatient, data.HisTreatment.BRANCH_ID);
            //Neu da ton tai benh nhan (client su dung ID cua benh nhan da co trong CSDL) thi thuc hien cap nhat thong tin benh nhan
            if (data.HisPatient.ID > 0)
            {
                //Neu la BN cu thi thuc hien luu anh truoc khi update, 
                //do da co thong tin patient_code (nghiep vu luu anh can su dung patient_code)
                this.ProcessPatientImage(data, data.HisPatient);
                this.hisPatientUpdate.Update(data.HisPatient);
            }
            //Neu chua ton tai benh nhan (client su dung ID cua benh nhan da co trong CSDL) 
            //thi thuc hien them moi benh nhan. Trong truong hop them moi that bai thi ket thuc nghiep vu
            else
            {
                this.isNewPatient = true;//check benh nhan lan dau duoc nhap len he thong ==> truyen vao khi tao treatment
                if (!this.hisPatientCreate.Create(data.HisPatient))
                {
                    throw new Exception("Rollback du lieu. Nghiep vu tiep theo se khong duoc thuc hien");
                }

                //can xu ly luu anh sau khi create patient, do o nghiep vu luu anh can lay thong tin patient_code
                this.ProcessPatientImage(data, data.HisPatient);

                if (!this.hisPatientUpdate.Update(data.HisPatient))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                }
            }

            //luu thong tin vua cap nhat vao CSDL
            this.recentHisPatient = data.HisPatient;
        }

        /// <summary>
        /// Xu ly du lieu Treatment
        /// </summary>
        /// <param name="hisMediRecordPeriodDTO"></param>
        private void ProcessHisTreatment(HisPatientProfileSDO data, WorkPlaceSDO workPlace)
        {
            data.HisTreatment.PATIENT_ID = this.recentHisPatient.ID;
            data.HisTreatment.HAS_CARD = this.recentHisPatient.HAS_CARD;
            if (!String.IsNullOrWhiteSpace(data.HisTreatment.TRANSFER_IN_ICD_CODE) || !String.IsNullOrWhiteSpace(data.HisTreatment.TRANSFER_IN_ICD_NAME))
            {
                data.HisTreatment.ICD_CODE = data.HisTreatment.TRANSFER_IN_ICD_CODE;
                data.HisTreatment.ICD_NAME = data.HisTreatment.TRANSFER_IN_ICD_NAME;
            }

            if (data.HisTreatment.OTHER_PAY_SOURCE_ID.HasValue)
            {
                //Neu nguon khac co cau hinh "ko cho phep gan cho Ho so dieu tri" thi cap nhat lai thanh null
                HIS_OTHER_PAY_SOURCE otherPaySource = HisOtherPaySourceCFG.DATA != null ? HisOtherPaySourceCFG.DATA.Where(o => o.ID == data.HisTreatment.OTHER_PAY_SOURCE_ID.Value).FirstOrDefault() : null;
                if (otherPaySource == null || otherPaySource.IS_NOT_FOR_TREATMENT == Constant.IS_TRUE)
                {
                    data.HisTreatment.OTHER_PAY_SOURCE_ID = null;
                }
            }

            //Set khoa da tung thuoc ve cho ho so dieu tri
            data.HisTreatment.DEPARTMENT_IDS = data.DepartmentId.ToString();
            data.HisTreatment.LAST_DEPARTMENT_ID = data.DepartmentId;

            //Neu dien doi tuong la dieu tri thi thuc hien nhap cac thong tin lien quan den vao vien
            if (HisTreatmentTypeCFG.TREATMENTs.Contains(data.HisPatientTypeAlter.TREATMENT_TYPE_ID))
            {
                data.HisTreatment.HOSPITALIZE_DEPARTMENT_ID = data.DepartmentId;
                data.HisTreatment.IN_DEPARTMENT_ID = data.DepartmentId;
                data.HisTreatment.IN_TREATMENT_TYPE_ID = data.HisPatientTypeAlter.TREATMENT_TYPE_ID;
                data.HisTreatment.IN_ROOM_ID = data.RequestRoomId;
                data.HisTreatment.IN_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.HisTreatment.IN_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.HisTreatment.IN_ICD_NAME = data.HisTreatment.ICD_NAME;
                data.HisTreatment.IN_ICD_CODE = data.HisTreatment.ICD_CODE;
                data.HisTreatment.IN_ICD_SUB_CODE = data.HisTreatment.ICD_SUB_CODE;
                data.HisTreatment.IN_ICD_TEXT = data.HisTreatment.ICD_TEXT;

                HisTreatmentInCode.SetInCode(data.HisTreatment, data.TreatmentTime, data.DepartmentId, data.HisPatientTypeAlter.TREATMENT_TYPE_ID);
            }

            data.HisTreatment.IS_CHRONIC = data.IsChronic ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
            data.HisTreatment.HOSPITALIZE_REASON_CODE = data.HisTreatment.HOSPITALIZE_REASON_CODE;
            data.HisTreatment.HOSPITALIZE_REASON_NAME = data.HisTreatment.HOSPITALIZE_REASON_NAME;

            //Thuc hien them moi treatment, neu them moi that bai thi ket thuc nghiep vu
            if (!this.hisTreatmentCreate.Create(data.HisTreatment, this.recentHisPatient, this.isNewPatient, data.HisPatientTypeAlter))
            {
                throw new Exception("Rollback du lieu, nghiep vu tiep theo se ko duoc thuc hien");
            }
            HisTreatmentInCode.FinishDB(data.HisTreatment);//xac nhan da xu ly DB (phuc vu nghiep vu sinh so vao vien)

            if (this.ProcessTreatmentImage(data, data.HisTreatment))
            {
                if (!this.hisTreatmentUpdate.Update(data.HisTreatment))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                }
            }

            this.recentHisTreatment = data.HisTreatment;
        }

        /// <summary>
        /// Xu ly thong tin thay doi dien doi tuong benh nhan
        /// - Them moi thong tin HisTreatmentLog
        /// - Them moi thong tin HisPatientTypeAlter (co bo sung thong tin BHYT neu co su dung the BHYT)
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHisPatientTypeAlter(HisPatientProfileSDO data)
        {
            HIS_PATIENT_TYPE_ALTER sdo = data.HisPatientTypeAlter;
            sdo.TREATMENT_ID = this.recentHisTreatment.ID;
            sdo.LOG_TIME = data.TreatmentTime;
            sdo.EXECUTE_ROOM_ID = data.RequestRoomId;
            sdo.DEPARTMENT_TRAN_ID = this.recentHisDepartmentTran.ID;
            sdo.BHYT_URL = this.recentHisPatient.BHYT_URL;//tiennv

            HIS_PATIENT_TYPE_ALTER resultData = new HIS_PATIENT_TYPE_ALTER();
            if (!this.hisPatientTypeAlterCreate.Create(sdo, this.recentHisTreatment, this.recentHisPatient, ref resultData))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisPatientTypeAlter = resultData;
        }

        /// <summary>
        /// Xu ly thong tin thay doi khoa phong
        /// - Them moi thong tin HisTreatmentLog
        /// - Them moi thong tin HisDepartmentTran
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHisDepartmentTran(HisPatientProfileSDO data)
        {

            HisDepartmentTranSDO sdo = new HisDepartmentTranSDO();
            sdo.TreatmentId = this.recentHisTreatment.ID;

            //Neu doi tuong dieu tri la "dieu tri noi tru", "dieu tri ngoai tru" thi danh dau ban ghi chuyen khoa cung la ban ghi nhap vien
            if (data.HisPatientTypeAlter != null
                && (data.HisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                || data.HisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU))
            {
                sdo.IsHospitalized = true;
            }
            sdo.IsReceive = true;
            sdo.Time = data.TreatmentTime;
            sdo.DepartmentId = data.DepartmentId;
            sdo.RequestRoomId = data.RequestRoomId;

            HIS_DEPARTMENT_TRAN resultData = new HIS_DEPARTMENT_TRAN();
            if (!this.hisDepartmentTranCreate.Create(sdo, true, ref resultData))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisDepartmentTran = resultData;
        }

        private void ProcessPatientImage(HisPatientProfileSDO data, HIS_PATIENT patient)
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
                    bhytFile.FileName = patient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_BHYT + ".jpeg";
                    fileHolders.Add(bhytFile);
                }
                if (data.ImgAvatarData != null && data.ImgAvatarData.Length > 0)
                {
                    FileHolder avaFile = new FileHolder();
                    MemoryStream avaStream = new MemoryStream();
                    avaStream.Write(data.ImgAvatarData, 0, data.ImgAvatarData.Length);
                    avaStream.Position = 0;
                    avaFile.Content = avaStream;
                    avaFile.FileName = patient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_AVATAR + ".jpeg";
                    fileHolders.Add(avaFile);
                }

                if (data.ImgCmndBeforeData != null && data.ImgCmndBeforeData.Length > 0)
                {
                    FileHolder cmndFile = new FileHolder();
                    MemoryStream cmndStream = new MemoryStream();
                    cmndStream.Write(data.ImgCmndBeforeData, 0, data.ImgCmndBeforeData.Length);
                    cmndStream.Position = 0;
                    cmndFile.Content = cmndStream;
                    cmndFile.FileName = patient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_CMND_BEFORE + ".jpeg";
                    fileHolders.Add(cmndFile);
                }

                if (data.ImgCmndAfterData != null && data.ImgCmndAfterData.Length > 0)
                {
                    FileHolder cmndFile = new FileHolder();
                    MemoryStream cmndStream = new MemoryStream();
                    cmndStream.Write(data.ImgCmndAfterData, 0, data.ImgCmndAfterData.Length);
                    cmndStream.Position = 0;
                    cmndFile.Content = cmndStream;
                    cmndFile.FileName = patient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_CMND_AFTER + ".jpeg";
                    fileHolders.Add(cmndFile);
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
                                    patient.BHYT_URL = info.Url;
                                }
                                else if (info.OriginalName.Contains(Constant.PATIENT_IMG_AVATAR))
                                {
                                    patient.AVATAR_URL = info.Url;
                                }
                                else if (info.OriginalName.Contains(Constant.PATIENT_IMG_CMND_BEFORE))
                                {
                                    patient.CMND_BEFORE_URL = info.Url;
                                }
                                else
                                {
                                    patient.CMND_AFTER_URL = info.Url;
                                }
                            }
                        }
                    }
                    else
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                        LogSystem.Warn("Luu anh len FSS that bai PatientId: " + patient.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Xu ly ket qua de tra lai
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(HisPatientProfileSDO data)
        {
            data.HisPatient = this.recentHisPatient;
            data.HisPatientTypeAlter = this.recentHisPatientTypeAlter;
            data.HisTreatment = this.recentHisTreatment;
        }

        private bool ProcessTreatmentImage(HisPatientProfileSDO data, HIS_TREATMENT treatment)
        {
            bool result = false;
            try
            {
                List<FileHolder> fileHolders = new List<FileHolder>();
                if (data.ImgTransferInData != null && data.ImgTransferInData.Length > 0)
                {
                    FileHolder transferInFile = new FileHolder();
                    MemoryStream bhytStream = new MemoryStream();
                    bhytStream.Write(data.ImgTransferInData, 0, data.ImgTransferInData.Length);
                    bhytStream.Position = 0;
                    transferInFile.Content = bhytStream;
                    transferInFile.FileName = treatment.TDL_PATIENT_CODE + "_" + treatment.IN_TIME + ".jpeg";
                    fileHolders.Add(transferInFile);
                }

                if (IsNotNullOrEmpty(fileHolders))
                {
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.TRANSFER_IN, fileHolders, true);
                    if (fileUploadInfos != null && fileUploadInfos.Count == fileHolders.Count)
                    {
                        foreach (FileUploadInfo info in fileUploadInfos)
                        {
                            if (!String.IsNullOrWhiteSpace(info.OriginalName))
                            {
                                if (info.OriginalName.Contains(treatment.IN_TIME.ToString()))
                                {
                                    treatment.TRANSFER_IN_URL = info.Url;
                                }
                            }
                        }

                        result = true;
                    }
                    else
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                        LogSystem.Warn("Luu anh len FSS that bai TreatmentId: " + treatment.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                LogSystem.Error(ex);
            }

            return result;
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.hisPatientTypeAlterCreate.RollbackData();
            this.hisDepartmentTranCreate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
            this.hisTreatmentCreate.Rollback();
            this.hisCardCreate.RollbackData();
            this.hisCardUpdate.RollbackData();
            this.hisPatientCreate.RollbackData();
            this.hisPatientUpdate.RollbackData();
        }
    }
}
