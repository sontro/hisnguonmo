using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseAddition.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
        }

        internal bool Run(List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    List<HIS_EXP_MEST_MATY_REQ> data = null;

                    if (!this.hisExpMestMatyReqMaker.Run(materials, expMest, ref resultData))
                    {
                        throw new Exception("hisExpMestMatyReqMaker. Ket thuc nghiep vu");
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
            this.hisExpMestMatyReqMaker.Rollback();
        }
    }
}
