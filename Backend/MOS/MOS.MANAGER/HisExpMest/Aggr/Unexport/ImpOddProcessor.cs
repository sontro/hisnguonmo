using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Common.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Unexport
{
    class ImpOddProcessor : BusinessBase
    {
        internal ImpOddProcessor()
            : base()
        {

        }

        internal ImpOddProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_IMP_MEST> oddImpMests)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(oddImpMests))
                {
                    foreach (HIS_IMP_MEST data in oddImpMests)
                    {
                        result = result && new HisImpMestTruncate(param).Truncate(data.ID, true);
                        if (!result)
                        {
                            LogSystem.Warn("Khong xoa duoc phieu nhap bu le ImpMestCode: " + data.IMP_MEST_CODE);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
