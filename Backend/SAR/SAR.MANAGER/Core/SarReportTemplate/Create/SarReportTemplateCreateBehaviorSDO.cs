using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SAR.SDO;
using System.Collections.Generic;
using System.Linq;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;

namespace SAR.MANAGER.Core.SarReportTemplate.Create
{
    class SarReportTemplateCreateBehaviorSDO : BeanObjectBase, ISarReportTemplateCreate
    {
        SarReportTemplateSDO entity;

        internal SarReportTemplateCreateBehaviorSDO(CommonParam param, SarReportTemplateSDO data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTemplateDAO.Create(entity);
                if (result)
                {
                    UploadFile(entity);
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
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        void UploadFile(SarReportTemplateSDO entity)
        {
            try
            {
                //if (entity.FileUploads != null && entity.FileUploads.Count > 0)
                //{
                //    List<FileHolder> fileHolders = new List<FileHolder>();
                //    foreach (var item in entity.FileUploads)
                //    {
                //        FileHolder file = new FileHolder();
                //        file.FileName = entity.REPORT_TEMPLATE_NAME;
                //        file.Content = item;
                //        file.Content.Position = 0;
                //        fileHolders.Add(file);
                //    }

                //    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(ManagerConstant.DSS_CLIENT_CODE, FileStoreLocation.REPORT_TEMPLATE, fileHolders);
                //}

                if (entity.FileUpload != null)
                {
                    List<FileHolder> fileHolders = new List<FileHolder>();

                    FileHolder file = new FileHolder();
                    file.FileName = entity.REPORT_TEMPLATE_NAME;
                    file.Content = entity.FileUpload;
                    file.Content.Position = 0;
                    fileHolders.Add(file);

                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(ManagerConstant.DSS_CLIENT_CODE, FileStoreLocation.REPORT_TEMPLATE, fileHolders);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
