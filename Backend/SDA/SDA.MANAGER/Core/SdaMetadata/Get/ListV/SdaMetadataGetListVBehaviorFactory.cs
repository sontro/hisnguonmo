using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Get.ListV
{
    class SdaMetadataGetListVBehaviorFactory
    {
        internal static ISdaMetadataGetListV MakeISdaMetadataGetListV(CommonParam param, object data)
        {
            ISdaMetadataGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SdaMetadataViewFilterQuery))
                {
                    result = new SdaMetadataGetListVBehaviorByViewFilterQuery(param, (SdaMetadataViewFilterQuery)data);
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
