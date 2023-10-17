using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class PatientProcessor : BusinessBase
    {
        private HisPatientCreate hisPatientCreate;
        private HisPatientUpdate hisPatientUpdate;
        private HisCardUpdate hisCardUpdate;

        internal PatientProcessor()
            : base()
        {
            this.Init();
        }

        internal PatientProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientCreate = new HisPatientCreate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisCardUpdate = new HisCardUpdate(param);
        }

        internal bool Run(HisPatientVaccinationSDO data, HIS_CARD card, ref HIS_PATIENT patient)
        {
            bool result = false;
            try
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
                this.ProcessHisCard(card, data);

                patient = data.HisPatient;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                patient = null;
                result = false;
            }
            return result;
        }


        private void ProcessPatientImage(HisPatientVaccinationSDO data)
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

        private void ProcessHisCard(HIS_CARD card, HisPatientVaccinationSDO data)
        {
            if (card != null)
            {
                card.PATIENT_ID = data.HisPatient.ID;
                if (!this.hisCardUpdate.Update(card))
                {
                    throw new Exception("hisCardUpdate. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            try
            {
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
