using SDA.EFMODEL.DataModels;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog.Get.ListV
{
    interface ISdaEventLogGetListV
    {
        List<V_SDA_EVENT_LOG> Run();
    }
}
