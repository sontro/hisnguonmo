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
    class HeinLiveListAreaBehavior : BusinessBase, IGetDataT
    {
        internal HeinLiveListAreaBehavior(CommonParam param)
            : base(param)
        {
        }

        object IGetDataT.Execute<T>()
        {
            try
            {                
                return MOS.LibraryHein.Bhyt.HeinLiveArea.HeinLiveAreaStore.Get();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
