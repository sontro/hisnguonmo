﻿using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create
{
    class AutoProcessor : BusinessBase
    {
        private HisExpMestAutoProcess auto;

        internal AutoProcessor(CommonParam param)
            : base(param)
        {
            this.auto = new HisExpMestAutoProcess();
        }

        internal bool Run(List<HIS_EXP_MEST> hisExpMests)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(hisExpMests))
                {
                    return false;
                }

                foreach (HIS_EXP_MEST expMest in hisExpMests)
                {
                    HisExpMestResultSDO resultSDO = null;
                    if (this.auto == null)
                        this.auto = new HisExpMestAutoProcess();
                    if (!this.auto.Run(expMest, ref resultSDO))
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

        internal void Rollback()
        {
            this.auto.RollBack();
        }
    }
}
