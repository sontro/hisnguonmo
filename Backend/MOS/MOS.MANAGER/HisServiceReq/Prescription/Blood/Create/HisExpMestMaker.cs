using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisExpMestMaker : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, V_HIS_MEDI_STOCK mediStock, ref HIS_EXP_MEST resultData)
        {
            try
            {
                if (IsNotNull(serviceReq))
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM;
                    expMest.SERVICE_REQ_ID = serviceReq.ID;
                    expMest.MEDI_STOCK_ID = mediStock.ID;
                    expMest.DESCRIPTION = serviceReq.DESCRIPTION;
                    if (!this.hisExpMestCreate.Create(expMest, serviceReq))
                    {
                        Inventec.Common.Logging.LogSystem.Info("HisExpMestCreate => ExpMestCreate => Create fail____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => expMest), expMest));
                        return false;
                    }
                    resultData = expMest;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
