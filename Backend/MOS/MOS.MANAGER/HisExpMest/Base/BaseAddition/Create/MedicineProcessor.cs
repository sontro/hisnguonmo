using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseAddition.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
        }

        internal bool Run(List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    List<HIS_EXP_MEST_METY_REQ> data = null;

                    if (!this.hisExpMestMetyReqMaker.Run(medicines, expMest, ref resultData))
                    {
                        throw new Exception("hisExpMestMetyReqMaker. Ket thuc nghiep vu");
                    }
                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestMetyReqMaker.Rollback();
        }
    }
}
