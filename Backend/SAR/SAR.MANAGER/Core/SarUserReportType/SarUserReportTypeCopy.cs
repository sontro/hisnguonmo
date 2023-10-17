using Inventec.Core;
using SAR.MANAGER.Core.SarUserReportType.Copy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeCopy : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SarUserReportTypeCopy(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                ISarUserReportTypeCopy behavior = SarUserReportTypeCopyFactory.MakeISarUserReportTypeCopy(param, entity);
                result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
