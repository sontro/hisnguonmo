using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unimport
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
                impMests.ForEach(o =>
                    {
                        o.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL;
                        o.IMP_TIME = null;
                        o.IMP_USERNAME = null;
                        o.IMP_LOGINNAME = null;
                    });
                if (!this.hisImpMestUpdate.UpdateList(impMests, beforeUpdates))
                {
                    throw new Exception("Ket thuc nghiep vu.");
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
