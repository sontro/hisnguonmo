using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood.Update
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST || expMest.DESCRIPTION != serviceReq.DESCRIPTION)
                    {
                        Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                        HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM;
                        expMest.DESCRIPTION = serviceReq.DESCRIPTION;
                        result = this.hisExpMestUpdate.Update(expMest, before);
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
