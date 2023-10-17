using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
{
    class AutoProcessor : BusinessBase
    {
        internal AutoProcessor()
            : base()
        {

        }

        internal AutoProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    HisExpMestResultSDO resultSDO = null;
                    if (!new HisExpMestAutoProcess().Run(expMest, ref resultSDO))
                    {
                        LogSystem.Warn("Tu dong thuc xuat don thuoc that bai: Ma phieu xuat: " + expMest.EXP_MEST_CODE);
                    }
                }
                result = true;
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
