using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportBO : BusinessObjectBase
    {
        internal bool Public(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportPublic(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Send(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportSend(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateNameDescription(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportUpdateNameDescription(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateStt(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportUpdateStt(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal FileHolder GetFile(object data)
        {
            FileHolder result = null;
            try
            {
                IDelegacyFile delegacy = new SarReportGetFile(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
