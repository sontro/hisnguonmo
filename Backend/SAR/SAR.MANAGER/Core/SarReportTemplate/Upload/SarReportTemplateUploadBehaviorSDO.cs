using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SAR.SDO;
using System.Collections.Generic;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using AutoMapper;

namespace SAR.MANAGER.Core.SarReportTemplate.Upload
{
    class SarReportTemplateUploadBehaviorSDO : BeanObjectBase, ISarReportTemplateUpload
    {
        SarReportTemplateSDO entity;
        List<FileUploadInfo> fileUploadInfos;

        internal SarReportTemplateUploadBehaviorSDO(CommonParam param, SarReportTemplateSDO data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateUpload.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    if (UploadFile(entity))
                    {
                        //entity.REPORT_TEMPLATE_URL = string.Format("{\"FILE_NAME\":\"{0}\",\"URL\":\"{1}\",\"EXTENSION\":\"{2}\"}", fileUploadInfos[0].OriginalName, fileUploadInfos[0].Url, entity.EXTENSION_RECEIVE);
                        entity.REPORT_TEMPLATE_URL = "{\"FILE_NAME\":\"" + fileUploadInfos[0].OriginalName + "\",\"URL\":\"" + fileUploadInfos[0].Url.Replace("\\", "\\\\") + "\",\"EXTENSION\":\"" + entity.EXTENSION_RECEIVE + "\"}";
                        entity.REPORT_TEMPLATE_URL = entity.REPORT_TEMPLATE_URL.Replace("\\", "\\\\");
                        Mapper.CreateMap<SarReportTemplateSDO, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>();
                        var raw = Mapper.Map<SarReportTemplateSDO, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>(entity);
                        result = DAOWorker.SarReportTemplateDAO.Update(raw);
                        Mapper.CreateMap<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SarReportTemplateSDO>();
                        entity = Mapper.Map<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SarReportTemplateSDO>(raw);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportTemplateCheckVerifyValidData.Verify(param, entity);
                result = result && SarReportTemplateCheckVerifyIsUnlock.Verify(param, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool UploadFile(SarReportTemplateSDO entity)
        {
            bool success = false;
            try
            {
                List<FileHolder> fileHolders = new List<FileHolder>();
                if (entity.FileUpload != null && entity.FileUpload.Count > 0)
                {
                    for (int i = 0; i < entity.FileUpload.Count; i++)
                    {
                        FileHolder file = new FileHolder();
                        file.FileName = entity.FileUpload[i].FileName;
                        file.Content = new System.IO.MemoryStream();
                        entity.FileUpload[i].InputStream.CopyTo(file.Content);
                        file.Content.Position = 0;
                        fileHolders.Add(file);
                    }

                    fileUploadInfos = FileUpload.UploadFile(ManagerConstant.DSS_CLIENT_CODE, FileStoreLocation.REPORT_TEMPLATE, fileHolders, true);
                    if (fileUploadInfos != null && fileUploadInfos.Count > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return success;
        }
    }
}
