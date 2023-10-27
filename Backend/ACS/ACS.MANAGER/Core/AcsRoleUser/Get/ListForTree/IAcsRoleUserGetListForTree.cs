using ACS.EFMODEL.DataModels;
using ACS.SDO;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListForTree
{
    interface IAcsRoleUserGetListForTree
    {
        List<AcsRoleUserSDO> Run();
    }
}
