using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisVaccinationExam;
using MOS.MANAGER.HisVitaminA;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientRegisterVitaminA : BusinessBase
    {
        private HIS_PATIENT recentHisPatient;

        HisPatientCreate hisPatientCreate;
        HisPatientUpdate hisPatientUpdate;
        HisVitaminACreate hisVitaminACreate;
        HisVaccinationExamCreate hisVaccinationExamCreate;
        HisDhstCreate hisDhstCreate;
        HisCardUpdate hisCardUpdate;

        internal HisPatientRegisterVitaminA()
            : base()
        {
            this.Init();
        }

        internal HisPatientRegisterVitaminA(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientCreate = new HisPatientCreate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisVitaminACreate = new HisVitaminACreate(param);
            this.hisVaccinationExamCreate = new HisVaccinationExamCreate(param);
            this.hisCardUpdate = new HisCardUpdate(param);
            this.hisDhstCreate = new HisDhstCreate(param);
        }

        internal bool Run(HisPatientVitaminASDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_CARD hisCard = null;
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsValidCardInfo(data, ref hisCard);
                if (valid)
                {
                    this.ProcessHisPatient(data);
                    this.ProcessHisCard(hisCard);
                    this.ProcessHisVitaminA(data, workPlace);
                    this.ProcessHisVaccination(data, workPlace);
                    this.ProcessHisDhst(data, workPlace);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessHisDhst(HisPatientVitaminASDO data, WorkPlaceSDO workPlace)
        {
            if (data.HisVaccinationExam != null && data.HisDhst != null)
            {
                data.HisDhst.EXECUTE_ROOM_ID = workPlace.RoomId;
                if (!data.HisDhst.EXECUTE_TIME.HasValue)
                {
                    data.HisDhst.EXECUTE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                }
                
                data.HisDhst.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                data.HisDhst.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.HisDhst.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.HisDhst.VACCINATION_EXAM_ID = data.HisVaccinationExam.ID;

                if (!this.hisDhstCreate.Create(data.HisDhst))
                {
                    throw new Exception("hisVaccinationExamCreate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessHisPatient(HisPatientVitaminASDO data)
        {
            if (data.HisPatient.ID > 0)
            {
                this.ProcessPatientImage(data);
                if (!this.hisPatientUpdate.Update(data.HisPatient))
                {
                    throw new Exception("hisPatientUpdate. Ket thuc nghiep vu");
                }
            }
            else
            {
                if (!this.hisPatientCreate.Create(data.HisPatient))
                {
                    throw new Exception("hisPatientCreate. Ket thuc nghiep vu");
                }

                if (data.ImgAvatarData != null || data.ImgBhytData != null)
                {
                    this.ProcessPatientImage(data);
                    if (!this.hisPatientUpdate.Update(data.HisPatient))
                    {
                        throw new Exception("hisPatientUpdate. Ket thuc nghiep vu");
                    }
                }
            }
            this.recentHisPatient = data.HisPatient;
        }

        private void ProcessHisCard(HIS_CARD card)
        {
            if (card != null)
            {
                card.PATIENT_ID = this.recentHisPatient.ID;
                if (!this.hisCardUpdate.Update(card))
                {
                    throw new Exception("hisCardUpdate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessHisVitaminA(HisPatientVitaminASDO data, WorkPlaceSDO workPlace)
        {
            if (data.HisVitaminA != null)
            {
                V_HIS_EXECUTE_ROOM exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == data.HisVitaminA.EXECUTE_ROOM_ID);
                if (exeRoom == null || exeRoom.IS_VITAMIN_A != Constant.IS_TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("EXECUTE_ROOM_ID Invalid. Ket thuc nghiep vu");
                }
                data.HisVitaminA.REQUEST_ROOM_ID = data.RequestRoomId;
                data.HisVitaminA.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                data.HisVitaminA.BRANCH_ID = workPlace.BranchId;
                data.HisVitaminA.EXECUTE_DEPARTMENT_ID = exeRoom.DEPARTMENT_ID;
                data.HisVitaminA.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.HisVitaminA.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.HisVitaminA.PATIENT_ID = this.recentHisPatient.ID;
                if (data.HisVitaminA.REQUEST_TIME <= 0)
                {
                    data.HisVitaminA.REQUEST_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                }

                if (!this.hisVitaminACreate.Create(data.HisVitaminA, this.recentHisPatient))
                {
                    throw new Exception("hisVitaminACreate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessHisVaccination(HisPatientVitaminASDO data, WorkPlaceSDO workPlace)
        {
            if (data.HisVaccinationExam != null)
            {
                V_HIS_EXECUTE_ROOM exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == data.HisVaccinationExam.EXECUTE_ROOM_ID);
                if (exeRoom == null || exeRoom.IS_VACCINE != Constant.IS_TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("EXECUTE_ROOM_ID Invalid. Ket thuc nghiep vu");
                }
                data.HisVaccinationExam.REQUEST_ROOM_ID = data.RequestRoomId;
                data.HisVaccinationExam.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                data.HisVaccinationExam.BRANCH_ID = workPlace.BranchId;
                data.HisVaccinationExam.EXECUTE_DEPARTMENT_ID = exeRoom.DEPARTMENT_ID;
                data.HisVaccinationExam.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.HisVaccinationExam.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.HisVaccinationExam.PATIENT_ID = this.recentHisPatient.ID;
                if (data.HisVaccinationExam.REQUEST_TIME <= 0)
                {
                    data.HisVaccinationExam.REQUEST_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                }

                if (!this.hisVaccinationExamCreate.Create(data.HisVaccinationExam, this.recentHisPatient))
                {
                    throw new Exception("hisVaccinationExamCreate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessPatientImage(HisPatientVitaminASDO data)
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
                    bhytFile.FileName = data.HisPatient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_BHYT + ".jpeg";
                    fileHolders.Add(bhytFile);
                }
                if (data.ImgAvatarData != null && data.ImgAvatarData.Length > 0)
                {
                    FileHolder avaFile = new FileHolder();
                    MemoryStream avaStream = new MemoryStream();
                    avaStream.Write(data.ImgAvatarData, 0, data.ImgAvatarData.Length);
                    avaStream.Position = 0;
                    avaFile.Content = avaStream;
                    avaFile.FileName = data.HisPatient.PATIENT_CODE + "_" + Constant.PATIENT_IMG_AVATAR + ".jpeg";
                    fileHolders.Add(avaFile);
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
                                    data.HisPatient.BHYT_URL = info.Url;
                                }
                                else
                                {
                                    data.HisPatient.AVATAR_URL = info.Url;
                                }
                            }
                        }
                    }
                    else
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                        LogSystem.Warn("Luu anh len FSS that bai PatientId: " + data.HisPatient.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                LogSystem.Error(ex);
            }
        }

        private bool IsValidCardInfo(HisPatientVitaminASDO data, ref HIS_CARD hisCard)
        {
            if (IsNotNullOrEmpty(data.CardCode))
            {
                hisCard = new HisCardGet().GetByCardCode(data.CardCode);
                if (hisCard == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_SoTheKhongHopLe);
                    return false;
                }
                if (hisCard.PATIENT_ID.HasValue && hisCard.PATIENT_ID.Value != data.HisPatient.ID)
                {
                    HIS_PATIENT patient = new HisPatientGet().GetById(hisCard.PATIENT_ID.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_TheDaDuocSuDungBoiBenhNhanKhac, patient.VIR_PATIENT_NAME, patient.PATIENT_CODE);
                    return false;
                }
            }
            return true;
        }

        private void Rollback()
        {
            try
            {
                this.hisDhstCreate.RollbackData();
                this.hisVaccinationExamCreate.RollbackData();
                this.hisVitaminACreate.RollbackData();
                this.hisCardUpdate.RollbackData();
                this.hisPatientUpdate.RollbackData();
                this.hisPatientCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
