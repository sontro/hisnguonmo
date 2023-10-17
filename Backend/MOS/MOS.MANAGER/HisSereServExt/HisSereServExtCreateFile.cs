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
using System.IO;
using System.Linq;
using System.Threading;

namespace MOS.MANAGER.HisSereServExt
{
    public class HisSereServExtCreateFile : BusinessBase
    {
        private HisSereServFileCreate hisSereServFileCreate;
        private HisSereServExtCreate hisSereServExtCreate;

        internal HisSereServExtCreateFile()
            : base()
        {
        }

        internal HisSereServExtCreateFile(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServFileCreate = new HisSereServFileCreate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        internal bool Create(HisSereServExtSDO data, ref HisSereServExtWithFileSDO hisSereServExtWithFileSDO)
        {
            bool result = false;
            try
            {
                bool valid = true;
                this.hisSereServFileCreate = new HisSereServFileCreate(param);
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                valid = valid && checker.IsValidMinAndMaxProcessTime(data.HisSereServExt);
                if (valid)
                {
                    if (!this.hisSereServExtCreate.Create(data, ref hisSereServExtWithFileSDO))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
                    }

                    if (IsNotNullOrEmpty(data.Files))
                    {
                        List<HIS_SERE_SERV_FILE> hisSereServFiles = new List<HIS_SERE_SERV_FILE>();
                        if (!this.CreateSereServFile(data.HisSereServExt.SERE_SERV_ID, data.Files, ref hisSereServFiles))
                        {
                            throw new Exception("Tao file that bai. Rollback du lieu. Ket thuc nghiep vu.");
                        }
                        hisSereServExtWithFileSDO.SereServFiles = hisSereServFiles;
                    }
                    result = true;

                    this.InitThreadSendResultToPacs(data.HisSereServExt);
                }
            }
            catch (Exception ex)
            {
                hisSereServExtWithFileSDO = null;
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackWithFile();
                result = false;
            }
            return result;
        }

        private void InitThreadSendResultToPacs(HIS_SERE_SERV_EXT sereServExt)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.SendResultToPacs));
                thread.Priority = ThreadPriority.Lowest;
                thread.Start(sereServExt);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            } 
        }

        private void SendResultToPacs(object data)
        {
            try
            {
                Thread.Sleep(1000);
                HIS_SERE_SERV_EXT sereServExt = (HIS_SERE_SERV_EXT)data;
                new HisSereServExtSendResultToPacs().Run(sereServExt);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool CreateSereServFile(long sereServId, List<FileSDO> files, ref List<HIS_SERE_SERV_FILE> hisSereServFiles)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(files) && files.Exists(t => t.Content != null))
                {
                    List<FileHolder> fileHolders = new List<FileHolder>();
                    foreach (FileSDO file in files)
                    {
                        FileHolder f = new FileHolder();
                        f.Content = new MemoryStream(file.Content);
                        f.FileName = file.FileName;
                        fileHolders.Add(f);
                    }

                    //upload file sang he thong thong FSS
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.SERE_SERV, fileHolders);
                    List<HIS_SERE_SERV_FILE> ssFiles = new List<HIS_SERE_SERV_FILE>();
                    if (fileUploadInfos != null && fileUploadInfos.Count > 0)
                    {
                        foreach (FileUploadInfo fui in fileUploadInfos)
                        {
                            HIS_SERE_SERV_FILE f = new HIS_SERE_SERV_FILE();
                            f.SERE_SERV_FILE_NAME = fui.OriginalName;
                            f.URL = fui.Url;
                            f.SERE_SERV_ID = sereServId;

                            FileSDO file = files.FirstOrDefault(o => o.FileName == fui.OriginalName);
                            if (file != null)
                            {
                                f.BODY_PART_ID = file.BodyPartId;
                                f.CAPTION = file.Caption;
                            }
                            ssFiles.Add(f);
                        }
                        if (!this.hisSereServFileCreate.CreateList(ssFiles))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
                        }
                        hisSereServFiles = ssFiles;
                        result = true;
                    }
                }
            }
            catch (FileUploadException ex)
            {
                LogSystem.Error("Upload file co loi xay ra. StatusCode: " + ex.StatusCode.ToString() + ". " + ex.Message);
                param.HasException = true;
                this.RollbackWithFile();
                result = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackWithFile();
                result = false;
            }
            return result;
        }

        private void RollbackWithFile()
        {
            this.hisSereServFileCreate.RollbackData();
            this.hisSereServExtCreate.RollbackData();
        }
    }
}
