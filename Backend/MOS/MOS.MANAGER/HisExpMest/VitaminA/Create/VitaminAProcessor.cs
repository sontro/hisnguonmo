using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVitaminA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.VitaminA.Create
{
    class VitaminAProcessor : BusinessBase
    {
        private HisVitaminAUpdate hisVitaminAUpdate;

        internal VitaminAProcessor(CommonParam param)
            : base(param)
        {
            this.hisVitaminAUpdate = new HisVitaminAUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_VITAMIN_A> vitaminAs)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_VITAMIN_A, HIS_VITAMIN_A>();
                List<HIS_VITAMIN_A> befores = Mapper.Map<List<HIS_VITAMIN_A>>(vitaminAs);
                vitaminAs.ForEach(o => o.EXP_MEST_ID = expMest.ID);
                if (!this.hisVitaminAUpdate.UpdateList(vitaminAs, befores))
                {
                    throw new Exception("Update vitaminA that bai");
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
                this.hisVitaminAUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
