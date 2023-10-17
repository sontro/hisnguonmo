using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Delete
{
    class SdaMetadataDeleteBehaviorFactory
    {
        internal static ISdaMetadataDelete MakeISdaMetadataDelete(CommonParam param, object data)
        {
            ISdaMetadataDelete result = null;
            try
            {
                if (data.GetType() == typeof(SDA_METADATA))
                {
                    result = new SdaMetadataDeleteBehaviorEv(param, (SDA_METADATA)data);
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
