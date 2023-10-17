using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.UnexportAndUnapprove
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodUpdate hisBloodUpdate;
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodUpdate = new HisBloodUpdate(param);
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLOOD> hisExpMestBloods)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisExpMestBloods))
                {

                    List<long> bloodIds = hisExpMestBloods.Select(s => s.BLOOD_ID).ToList();
                    List<HIS_BLOOD> hisBloods = new HisBloodGet().GetByIds(bloodIds);

                    Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> expMestBloodBefores = Mapper.Map<List<HIS_EXP_MEST_BLOOD>>(hisExpMestBloods);
                    hisExpMestBloods.ForEach(o =>
                        {
                            o.EXP_TIME = null;
                            o.EXP_USERNAME = null;
                            o.EXP_LOGINNAME = null;
                            o.IS_EXPORT = null;
                        });
                    if (!this.hisExpMestBloodUpdate.UpdateList(hisExpMestBloods, expMestBloodBefores))
                    {
                        throw new Exception("Update HIS_EXP_MEST_BLOOD that bai");
                    }

                    Mapper.CreateMap<HIS_BLOOD, HIS_BLOOD>();
                    List<HIS_BLOOD> bloodBefores = Mapper.Map<List<HIS_BLOOD>>(hisBloods);
                    hisBloods.ForEach(o =>
                    {
                        o.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        o.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    });
                    if (!this.hisBloodUpdate.UpdateList(hisBloods, bloodBefores))
                    {
                        throw new Exception("Update HIS_BLOOD that bai");
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
            try
            {
                this.hisBloodUpdate.RollbackData();
                this.hisExpMestBloodUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
