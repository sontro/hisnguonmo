using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateBO : BusinessObjectBase
    {
        internal SarReportTemplateBO()
            : base()
        {

        }

        internal T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new SarReportTemplateGet(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool Create(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportTemplateCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Update(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportTemplateUpdate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ChangeLock(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportTemplateChangeLock(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Delete(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportTemplateDelete(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
