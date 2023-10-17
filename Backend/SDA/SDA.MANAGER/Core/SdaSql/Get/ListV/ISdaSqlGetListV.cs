using SDA.EFMODEL.DataModels;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSql.Get.ListV
{
    interface ISdaSqlGetListV
    {
        List<V_SDA_SQL> Run();
    }
}
