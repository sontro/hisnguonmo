using AutoMapper;
using HID.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisVaccinationExam;
using MOS.MANAGER.HisVaccination;
using MOS.MANAGER.HisTreatment.UpdatePatientInfo;
using MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MOS.MANAGER.HisServiceReq.Pacs;

namespace MOS.MANAGER.HisPatient.UpdateInfo
{
    partial class HisPatientUpdateInfo : BusinessBase
    {
        private HIS_PATIENT beforeUpdate;
        private HIS_VACCINATION_EXAM beforeUpdateVaccinExam;
        private List<HIS_VACCINATION> beforeUpdateVaccins;

        private HisTreatmentUpdatePatientInfo hisTreatmentUpdate;
        private List<HisSereServUpdateHein> updateHeinprocessors = new List<HisSereServUpdateHein>();

        internal HisPatientUpdateInfo()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdatePatientInfo(param);
        }

        internal HisPatientUpdateInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdatePatientInfo(param);
        }


        internal bool Run(HisPatientUpdateSDO data, ref HIS_PATIENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientCheck checker = new HisPatientCheck(param);
                HIS_PATIENT raw = null;
                HIS_VACCINATION_EXAM vaccinExam = null;
                HIS_VACCINATION vaccin = null;
                valid = valid && checker.VerifyId(data.HisPatient.ID, ref raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                    this.beforeUpdate = Mapper.Map<HIS_PATIENT>(raw);

                    this.ProcessPatientImage(data);

                    if (data.HisPatient.IS_HIV == Constant.IS_TRUE)
                    {
                        data.HisPatient.IS_HIV = Constant.IS_TRUE;
                    }
                    else
                    {
                        data.HisPatient.IS_HIV = null;
                    }
                    if (!DAOWorker.HisPatientDAO.Update(data.HisPatient))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", data));
                    }

                    this.ProcessTreatment(data);
                    this.ProcessUpdateExternal(data.HisPatient.ID);
                    if (data.IsUpdateVaccinationExam)
                    {
                        this.ProcessVaccinationExam(data.HisPatient, ref vaccinExam);
                        this.ProcessVaccination(vaccinExam, ref vaccin);
                    }

                    resultData = data.HisPatient;
                    result = true;
                    HisPatientLog.Run(data.HisPatient, raw, LibraryEventLog.EventLog.Enum.HisPatient_SuaThongTinBenhNhan);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessVaccination(HIS_VACCINATION_EXAM vaccinExam, ref HIS_VACCINATION vaccin)
        {
            if (vaccinExam != null)
            {
                List<HIS_VACCINATION> vaccins = new HisVaccinationGet().GetByVaccinationExamId(vaccinExam.ID);
                if (IsNotNullOrEmpty(vaccins))
                {
                    Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
                    this.beforeUpdateVaccins = Mapper.Map<List<HIS_VACCINATION>>(vaccins);

                    vaccins.ForEach(o => HisVaccinationUtil.SetTdl(o, vaccinExam));

                    if (!DAOWorker.HisVaccinationDAO.UpdateList(vaccins))
                    {
                        throw new Exception("Cap nhat thong tin thua patient cho HIS_VACCINATION theo vaccinExamId that bai");
                    }
                }
            }
        }

        private void ProcessVaccinationExam(HIS_PATIENT patient, ref HIS_VACCINATION_EXAM vaccinExam)
        {
            if (patient != null)
            {
                List<HIS_VACCINATION_EXAM> vaccinExams = new HisVaccinationExamGet().GetByPatientId(patient.ID);
                vaccinExam = IsNotNullOrEmpty(vaccinExams) ? vaccinExams.OrderByDescending(o => o.REQUEST_TIME).FirstOrDefault() : null;
                if (vaccinExam != null)
                {
                    Mapper.CreateMap<HIS_VACCINATION_EXAM, HIS_VACCINATION_EXAM>();
                    this.beforeUpdateVaccinExam = Mapper.Map<HIS_VACCINATION_EXAM>(vaccinExam);

                    HisVaccinationExamUtil.SetTdl(vaccinExam, patient);

                    if (!DAOWorker.HisVaccinationExamDAO.Update(vaccinExam))
                    {
                        throw new Exception("Cap nhat thong tin thua patient cho HIS_VACCINATION_EXAM co request time moi nhat that bai");
                    }
                }
            }
        }

        private bool ProcessUpdateExternal(long patientId)
        {
            bool result = true;

            //Truy van lai tu CSDL de tranh truong hop UpdatePatientInfo su dung cac truong virtual (cac truong VIR_)
            //Cac truong nay dang duoc set gia tri tu dong trong DB
            HIS_PATIENT patient = new HisPatientGet().GetById(patientId);

            if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
            {
                ILisProcessor sender = LisFactory.GetProcessor(param);
                List<string> messages = null;
                if (sender != null)
                {
                    result = result && sender.UpdatePatientInfo(patient, ref messages);
                }
            }

            IPacsProcessor pacsSender = PacsFactory.GetProcessor(param);
            List<string> pacsMessages = null;
            if (pacsSender != null)
            {
                pacsSender.UpdatePatientInfo(patient, ref pacsMessages);
            }

            return result;
        }

        private void ProcessPatientImage(HisPatientUpdateSDO data)
        {
            try
            {
                if (data.IsNotUpdateImage)
                {
                    return;
                }

                data.HisPatient.BHYT_URL = null;
                data.HisPatient.AVATAR_URL = null;

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
                        LogSystem.Warn("Luu anh len FSS that bai PATIENT_CODE: " + data.HisPatient.PATIENT_CODE);
                    }
                }
            }
            catch (Exception ex)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Neu mo tu chuc nang "Ho so dieu tri" hoac mo tu "Danh sach BN" va co check "Sua ho so dieu tri moi nhat" thi thuc hien cap nhat thong tin ho so dieu tri
        /// </summary>
        /// <param name="data"></param>
        private void ProcessTreatment(HisPatientUpdateSDO data)
        {
            HIS_TREATMENT treatmentToUpdate = null;

            //Neu mo tu chuc nang "Ho so dieu tri"
            if (data.TreatmentId.HasValue)
            {
                treatmentToUpdate = new HisTreatmentGet().GetById(data.TreatmentId.Value);
            }
            //Neu mo tu "Danh sach BN" va co check "Sua ho so dieu tri moi nhat" thi thuc hien cap nhat thong tin ho so dieu tri
            else if (data.UpdateTreatment)
            {
                List<HIS_TREATMENT> listTreatment = new HisTreatmentGet().GetByPatientId(data.HisPatient.ID);
                if (IsNotNullOrEmpty(listTreatment))
                {
                    listTreatment = listTreatment.OrderByDescending(o => o.IN_TIME).ToList();
                    foreach (var item in listTreatment)
                    {
                        if (item.IS_TEMPORARY_LOCK != Constant.IS_TRUE && item.IS_ACTIVE == Constant.IS_TRUE)
                        {
                            treatmentToUpdate = item;
                            break;
                        }
                    }
                }
            }

            if (treatmentToUpdate != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforTreatment = Mapper.Map<HIS_TREATMENT>(treatmentToUpdate);

                treatmentToUpdate.TDL_PATIENT_UNSIGNED_NAME = !string.IsNullOrWhiteSpace(treatmentToUpdate.TDL_PATIENT_NAME) ? Inventec.Common.String.Convert.UnSignVNese2(treatmentToUpdate.TDL_PATIENT_NAME) : null;

                if (!this.hisTreatmentUpdate.Run(treatmentToUpdate, data.HisPatient, data.IsUpdateEmr))
                {
                    throw new Exception("Cap nhat thong tin BN cho ho so dieu tri that bai");
                }

                if (HisServicePatyCFG.HAS_PATIENT_CLASSIFY && beforTreatment.TDL_PATIENT_CLASSIFY_ID != data.HisPatient.PATIENT_CLASSIFY_ID)
                {
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatmentToUpdate.ID);
                    HisSereServUpdateHein updateHeinprocessor = new HisSereServUpdateHein(param, treatmentToUpdate, ptas, false);
                    this.updateHeinprocessors.Add(updateHeinprocessor);
                    //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang
                    if (!updateHeinprocessor.UpdateDb())
                    {
                        throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                    }

                }
            }
        }

        public void RollbackData()
        {
            if (this.beforeUpdateVaccins != null && this.beforeUpdateVaccins.Count > 0)
            {
                if (!DAOWorker.HisVaccinationDAO.UpdateList(this.beforeUpdateVaccins))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinations that bai, can kiem tra lai.");
                }
            }

            if (this.beforeUpdateVaccinExam != null)
            {
                if (!DAOWorker.HisVaccinationExamDAO.Update(this.beforeUpdateVaccinExam))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationExam that bai, can kiem tra lai.");
                }
            }
            if (IsNotNullOrEmpty(this.updateHeinprocessors))
            {
                foreach (HisSereServUpdateHein p in this.updateHeinprocessors)
                {
                    p.RollbackData();
                }
            }
            this.hisTreatmentUpdate.Rollback();

            if (this.beforeUpdate != null)
            {
                if (!DAOWorker.HisPatientDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback du lieu HisPatient that bai, can kiem tra lai.");
                }
            }
        }
    }
}
