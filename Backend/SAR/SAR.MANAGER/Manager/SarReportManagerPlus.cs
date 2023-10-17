using SAR.MANAGER.Core.SarReport;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Manager
{
    public partial class SarReportManager : ManagerBase
    {
        public object Public(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportBO bo = new SarReportBO();
                if (bo.Public(data))
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

        public object Send(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportBO bo = new SarReportBO();
                if (bo.Send(data))
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

        public object UpdateNameDescription(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportBO bo = new SarReportBO();
                if (bo.UpdateNameDescription(data))
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

        public object UpdateStt(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportBO bo = new SarReportBO();
                if (bo.UpdateStt(data))
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

        public object GetFile(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportBO bo = new SarReportBO();
                result = bo.GetFile(data);
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
    }
}
