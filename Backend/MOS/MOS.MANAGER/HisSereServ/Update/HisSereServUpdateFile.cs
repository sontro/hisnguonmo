using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServFile;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    class HisSereServUpdateFile : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServFileCreate hisSereServFileCreate;

        internal HisSereServUpdateFile()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdateFile(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisSereServFileCreate = new HisSereServFileCreate(param);
        }

        internal bool Update(HIS_SERE_SERV data, List<FileHolder> files, ref HisSereServWithFileSDO hisSereServWithFileSDO)
        {
            bool result = false;
            try
            {
                hisSereServWithFileSDO = new HisSereServWithFileSDO();
                if (!this.hisSereServUpdate.UpdateResult(data))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
                }
                hisSereServWithFileSDO.HisSereServ = data;
                if (IsNotNullOrEmpty(files))
                {
                    List<HIS_SERE_SERV_FILE> hisSereServFiles = new List<HIS_SERE_SERV_FILE>();
                    if (!this.Update(data.ID, files, ref hisSereServFiles))
                    {
                        throw new Exception("Tao file that bai. Rollback du lieu. Ket thuc nghiep vu.");
                    }
                    hisSereServWithFileSDO.HisSereServFiles = hisSereServFiles;
                }
                result = true;
            }
            catch (Exception ex)
            {
                hisSereServWithFileSDO = null;
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        internal bool Update(long sereServId, List<FileHolder> files, ref List<HIS_SERE_SERV_FILE> hisSereServFileDTOs)
        {
            bool result = false;
            try
            {
                //upload file sang he thong thong FSS
                List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.SERE_SERV, files);
                List<HIS_SERE_SERV_FILE> dtos = new List<HIS_SERE_SERV_FILE>();
                if (fileUploadInfos != null && fileUploadInfos.Count > 0)
                {
                    foreach (FileUploadInfo fui in fileUploadInfos)
                    {
                        HIS_SERE_SERV_FILE f = new HIS_SERE_SERV_FILE();
                        f.SERE_SERV_FILE_NAME = fui.OriginalName;
                        f.URL = fui.Url;
                        f.SERE_SERV_ID = sereServId;
                        dtos.Add(f);
                    }
                    if (!this.hisSereServFileCreate.CreateList(dtos))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
                    }
                    hisSereServFileDTOs = dtos;
                    result = true;
                }
            }
            catch (FileUploadException ex)
            {
                LogSystem.Error("Upload file co loi xay ra. StatusCode: " + ex.StatusCode.ToString() + ". " + ex.Message);
                param.HasException = true;
                this.RollbackData();
                result = false;
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

        internal void RollbackData()
        {
            //rollback du lieu his_sere_serv_file truoc khi rollback his_sere_serv
            this.hisSereServFileCreate.RollbackData();
            this.hisSereServUpdate.RollbackData();
        }
    }
}
