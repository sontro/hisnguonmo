using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using SAR.SDO;

namespace SAR.MANAGER.Core.SarReportTemplate.Upload
{
    class SarReportTemplateUploadBehaviorFactory
    {
        internal static ISarReportTemplateUpload MakeISarReportTemplateUpload(CommonParam param, object data)
        {
            ISarReportTemplateUpload result = null;
            try
            {
                if (data.GetType() == typeof(SarReportTemplateSDO))
                {
                    result = new SarReportTemplateUploadBehaviorSDO(param, (SarReportTemplateSDO)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
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
