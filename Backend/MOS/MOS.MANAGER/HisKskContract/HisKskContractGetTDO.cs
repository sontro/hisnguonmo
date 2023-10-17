using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract
{
    partial class HisKskContractGetTDO : BusinessBase
    {
        internal HisKskContractGetTDO()
            : base()
        {

        }

        internal HisKskContractGetTDO(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HisKskContractTDO> GetTdo(long? fromTime, long? toTime)
        {
            try
            {
                List<V_HIS_KSK_CONTRACT> contracts = this.GetData(fromTime, toTime);
                return this.MakeData(contracts);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private List<HisKskContractTDO> MakeData(List<V_HIS_KSK_CONTRACT> contracts)
        {
            if (IsNotNullOrEmpty(contracts))
            {
                List<HisKskContractTDO> result = new List<HisKskContractTDO>();
                foreach (var contract in contracts)
                {
                    var tdo = new HisKskContractTDO();
                    tdo.KskContractCode = contract.KSK_CONTRACT_CODE;
                    tdo.WorkPlaceName = contract.WORK_PLACE_NAME;
                    tdo.DepartmentName = contract.DEPARTMENT_NAME;
                    result.Add(tdo);
                }
                return result;
            }
            return null;
        }

        private List<V_HIS_KSK_CONTRACT> GetData(long? fromTime, long? toTime)
        {
            HisKskContractViewFilterQuery filter = new HisKskContractViewFilterQuery();
            if (fromTime.HasValue)
            {
                filter.CONTRACT_DATE_FROM = fromTime.Value;
            }
            if (toTime.HasValue)
            {
                filter.CONTRACT_DATE_TO = toTime.Value;
            }
            return new HisKskContractGet().GetView(filter);
        }
    }
}
