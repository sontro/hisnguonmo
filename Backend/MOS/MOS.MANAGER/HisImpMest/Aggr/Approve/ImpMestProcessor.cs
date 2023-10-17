using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Approve
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;

        internal ImpMestProcessor()
            : base()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST aggrImpMest, List<HIS_IMP_MEST> childs)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST> impMests = new List<HIS_IMP_MEST>();
                impMests.Add(aggrImpMest);
                impMests.AddRange(childs);
                Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                List<HIS_IMP_MEST> beforeUpdates = Mapper.Map<List<HIS_IMP_MEST>>(impMests);

                long now = Inventec.Common.DateTime.Get.Now().Value;

                impMests.ForEach(o =>
                    {
                        o.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL;
                        o.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        o.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        o.APPROVAL_TIME = now;
                    });
                if (!this.hisImpMestUpdate.UpdateList(impMests, beforeUpdates))
                {
                    throw new Exception("Ket thuc nghiep vu.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
