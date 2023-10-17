using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData.ADO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Get
{
    class HisAgeGetListBehavior : BusinessBase, IGetDataT
    {
        internal HisAgeGetListBehavior(CommonParam param)
            : base(param)
        {
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                List<AgeADO> ages = new List<AgeADO>();
                AgeADO kh1 = new AgeADO(1, "Tuổi");
                ages.Add(kh1);

                AgeADO kh2 = new AgeADO(2, "Tháng");
                ages.Add(kh2);

                AgeADO kh3 = new AgeADO(3, "Ngày");
                ages.Add(kh3);

                AgeADO kh4 = new AgeADO(4, "Giờ");
                ages.Add(kh4);
                return ages;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
