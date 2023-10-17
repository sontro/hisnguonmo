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
using Inventec.Common.Logging;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.LocalStorage.BackendData.Get
{
    class MedicineMaterialTypeComboGetBehavior : BusinessBase, IGetDataT
    {
        internal MedicineMaterialTypeComboGetBehavior(CommonParam param)
            : base(param)
        {

        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                List<MedicineMaterialTypeComboADO> result = new List<MedicineMaterialTypeComboADO>();

                var rs1 = (from m in BackendDataWorker.Get<V_HIS_MEDICINE_TYPE>() where m.IS_ACTIVE == GlobalVariables.CommonNumberTrue select new MedicineMaterialTypeComboADO(m)).ToList();
                var rs2 = (from m in BackendDataWorker.Get<V_HIS_MATERIAL_TYPE>() where m.IS_ACTIVE == GlobalVariables.CommonNumberTrue select new MedicineMaterialTypeComboADO(m)).ToList();
                result.AddRange(rs1);
                result.AddRange(rs2);

                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
