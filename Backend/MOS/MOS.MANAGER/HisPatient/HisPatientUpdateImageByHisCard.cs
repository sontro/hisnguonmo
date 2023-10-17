using COS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    public class HisPatientUpdateImageByHisCard : BusinessBase
    {
        public HisPatientUpdateImageByHisCard()
            : base()
        {

        }

        public HisPatientUpdateImageByHisCard(CommonParam param)
            : base(param)
        {

        }

        public bool Run()
        {
            bool result = false;
            try
            {
                string sqlQuery = "SELECT P.* FROM HIS_PATIENT P WHERE P.AVATAR_URL IS NULL AND P.BHYT_URL IS NULL AND EXISTS (SELECT 1 FROM HIS_CARD WHERE PATIENT_ID = P.ID)";
                List<HIS_PATIENT> patients = DAOWorker.SqlDAO.GetSql<HIS_PATIENT>(sqlQuery);
                if (IsNotNullOrEmpty(patients))
                {
                    foreach (HIS_PATIENT pt in patients)
                    {
                        if (!String.IsNullOrEmpty(pt.AVATAR_URL) || !String.IsNullOrEmpty(pt.BHYT_URL))
                        {
                            continue;
                        }
                        HIS_CARD card = new HisCardGet().GetLastByPatientId(pt.ID);
                        if (card == null)
                        {
                            LogSystem.Info("Benh nhan khong co the KCB thong minh patientId: " + pt.ID);
                            continue;
                        }

                        MemoryStream avatarImage = null;
                        MemoryStream bhytImage = null;
                        List<V_COS_PEOPLE_IMG_TYPE> imgTypes = ApiConsumerStore.CosConsumer.Get<List<V_COS_PEOPLE_IMG_TYPE>>(true, "api/CosPeopleImgType/GetViewByCardCode", null, card.CARD_CODE);
                        if (IsNotNullOrEmpty(imgTypes))
                        {
                            try
                            {
                                foreach (V_COS_PEOPLE_IMG_TYPE img in imgTypes)
                                {
                                    if (img.IMG_TYPE_ID == IMSys.DbConfig.COS_RS.COS_IMG_TYPE.ID_AVATAR && !String.IsNullOrWhiteSpace(img.URL))
                                    {
                                        avatarImage = FileDownload.GetFile(img.URL, CosCFG.COS_FSS_BASE_URI);
                                    }
                                    else if (img.IMG_TYPE_ID == IMSys.DbConfig.COS_RS.COS_IMG_TYPE.ID_BHYT_BEFORE && !String.IsNullOrWhiteSpace(img.URL))
                                    {
                                        bhytImage = FileDownload.GetFile(img.URL, CosCFG.COS_FSS_BASE_URI);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                            }

                            this.UpdateImageToLocal(avatarImage, bhytImage, pt);
                        }
                        if (!String.IsNullOrEmpty(pt.BHYT_URL) || !String.IsNullOrEmpty(pt.AVATAR_URL))
                        {
                            string sql = "UPDATE HIS_PATIENT SET AVATAR_URL = :param1, BHYT_URL = :param2 WHERE ID = :param3";
                            if (!DAOWorker.SqlDAO.Execute(sql, pt.AVATAR_URL, pt.BHYT_URL, pt.ID))
                            {
                                LogSystem.Info("Update Url cho Patient that bai SQL: " + sql);
                            }
                        }
                    }
                }
                else
                {
                    LogSystem.Info("Khong co benh nhan nao can Update anh");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void UpdateImageToLocal(MemoryStream avatarStream, MemoryStream bhytStream, HIS_PATIENT pt)
        {
            List<FileHolder> fileHolders = new List<FileHolder>();
            if (bhytStream != null && bhytStream.Length > 0)
            {
                bhytStream.Position = 0;
                FileHolder bhytFile = new FileHolder();
                bhytFile.Content = bhytStream;
                bhytFile.FileName = pt.PATIENT_CODE + "_" + Constant.PATIENT_IMG_BHYT + ".jpeg";
                fileHolders.Add(bhytFile);
            }
            if (avatarStream != null && avatarStream.Length > 0)
            {
                avatarStream.Position = 0;
                FileHolder avaFile = new FileHolder();
                avaFile.Content = avatarStream;
                avaFile.FileName = pt.PATIENT_CODE + "_" + Constant.PATIENT_IMG_AVATAR + ".jpeg";
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
                                pt.BHYT_URL = info.Url;
                            }
                            else
                            {
                                pt.AVATAR_URL = info.Url;
                            }
                        }
                    }
                }
                else
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_LuuAnhThatBai);
                    LogSystem.Warn("Luu anh len FSS Local that bai PatientId: " + pt.ID);
                }
            }
        }
    }
}
