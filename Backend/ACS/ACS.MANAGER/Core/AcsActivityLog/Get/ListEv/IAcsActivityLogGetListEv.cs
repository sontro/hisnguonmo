using ACS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.ListEv
{
    interface IAcsActivityLogGetListEv
    {
        List<ACS_ACTIVITY_LOG> Run();
    }
}
