using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTemplate
{
    public partial class SarReportTemplateDAO : EntityBase
    {
        private SarReportTemplateCreate CreateWorker
        {
            get
            {
                return (SarReportTemplateCreate)Worker.Get<SarReportTemplateCreate>();
            }
        }
        private SarReportTemplateUpdate UpdateWorker
        {
            get
            {
                return (SarReportTemplateUpdate)Worker.Get<SarReportTemplateUpdate>();
            }
        }
        private SarReportTemplateDelete DeleteWorker
        {
            get
            {
                return (SarReportTemplateDelete)Worker.Get<SarReportTemplateDelete>();
            }
        }
        private SarReportTemplateTruncate TruncateWorker
        {
            get
            {
                return (SarReportTemplateTruncate)Worker.Get<SarReportTemplateTruncate>();
            }
        }
        private SarReportTemplateGet GetWorker
        {
            get
            {
                return (SarReportTemplateGet)Worker.Get<SarReportTemplateGet>();
            }
        }
        private SarReportTemplateCheck CheckWorker
        {
            get
            {
                return (SarReportTemplateCheck)Worker.Get<SarReportTemplateCheck>();
            }
        }

        public bool Create(SAR_REPORT_TEMPLATE data)
        {
            bool result = false;
            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool CreateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            bool result = false;
            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(SAR_REPORT_TEMPLATE data)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool UpdateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Delete(SAR_REPORT_TEMPLATE data)
        {
            bool result = false;
            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool DeleteList(List<SAR_REPORT_TEMPLATE> listData)
        {
            bool result = false;

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Truncate(SAR_REPORT_TEMPLATE data)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool TruncateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<SAR_REPORT_TEMPLATE> Get(SarReportTemplateSO search, CommonParam param)
        {
            List<SAR_REPORT_TEMPLATE> result = new List<SAR_REPORT_TEMPLATE>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public SAR_REPORT_TEMPLATE GetById(long id, SarReportTemplateSO search)
        {
            SAR_REPORT_TEMPLATE result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
