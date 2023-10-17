using SAR.MANAGER.Core.SarReportTemplate;
using Inventec.Core;
using System;
using AutoMapper;

namespace SAR.MANAGER.Manager
{
    public partial class SarReportTemplateManager : ManagerBase
    {
        public SarReportTemplateManager(CommonParam param)
            : base(param)
        {

        }

        public T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Get<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }

        public object Create(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                if (bo.Create(data))
                {
                    result = data;
                    //Mapper.CreateMap<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>();
                    //result = Mapper.Map<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>((SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE)data);
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public object Update(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                if (bo.Update(data))
                {
                    result = data;
                    //Mapper.CreateMap<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>();
                    //result = Mapper.Map<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>((SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE)data);
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public object ChangeLock(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                if (bo.ChangeLock(data))
                {
                    result = data;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public bool Delete(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                if (bo.Delete(data))
                {
                    result = true;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
