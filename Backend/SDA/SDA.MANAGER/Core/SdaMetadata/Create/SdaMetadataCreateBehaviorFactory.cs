using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaMetadata.Create
{
    class SdaMetadataCreateBehaviorFactory
    {
        internal static ISdaMetadataCreate MakeISdaMetadataCreate(CommonParam param, object data)
        {
            ISdaMetadataCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_METADATA))
                {
                    result = new SdaMetadataCreateBehaviorEv(param, (SDA_METADATA)data);
                }
                else if (data.GetType() == typeof(List<SDA_METADATA>))
                {
                    result = new SdaMetadataCreateListBehaviorEv(param, (List<SDA_METADATA>)data);
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
