using Inventec.Core;
using SAR.MANAGER.Core.SarReport.GetFile;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportGetFile : BeanObjectBase, IDelegacyFile
    {
        object entity;

        internal SarReportGetFile(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        FileHolder IDelegacyFile.Execute()
        {
            FileHolder result = null;
            try
            {
                ISarReportGetFile behavior = SarReportGetFileBehaviorFactory.MakeISarReportGetFile(param, entity);
                result = behavior != null ? behavior.Run() : null;
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
