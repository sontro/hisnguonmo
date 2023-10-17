using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    class HisTreatmentCheckDataStore : BusinessBase
    {
        internal HisTreatmentCheckDataStore()
            : base()
        {

        }

        internal HisTreatmentCheckDataStore(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> ids, ref HisTreatmentCheckDataStoreSDO resultData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    List<long> hasPtIds = null;
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.TREATMENT_IDs = ids;
                    filter.HAS_EXECUTE = true;
                    filter.TDL_SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT;

                    List<HIS_SERE_SERV> sereServSurgs = new HisSereServGet().Get(filter);
                    if (IsNotNullOrEmpty(sereServSurgs))
                    {
                        hasPtIds = ids.Where(o => sereServSurgs.Exists(e => e.TDL_TREATMENT_ID == o && e.AMOUNT > 0)).ToList();
                    }

                    resultData = new HisTreatmentCheckDataStoreSDO();
                    resultData.HasSurgIds = hasPtIds;
                    resultData.HasNotSurgIds = ids.Where(o => hasPtIds == null || !hasPtIds.Contains(o)).ToList();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }
    }
}
