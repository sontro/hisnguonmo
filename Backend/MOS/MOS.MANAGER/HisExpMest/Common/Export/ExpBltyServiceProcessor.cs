using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBltyService;
using MOS.MANAGER.HisExpBltyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    class ExpBltyServiceProcessor : BusinessBase
    {
        private HisExpBltyServiceCreate hisExpBltyServiceCreate;

        internal ExpBltyServiceProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpBltyServiceProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpBltyServiceCreate = new HisExpBltyServiceCreate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_BLOOD> hisExpMestBloods, HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM 
                    && IsNotNullOrEmpty(hisExpMestBloods))
                {
                    List<HIS_EXP_BLTY_SERVICE> creates = new List<HIS_EXP_BLTY_SERVICE>();

                    HisBltyServiceFilterQuery filter = new HisBltyServiceFilterQuery();
                    filter.BLOOD_TYPE_IDs = hisExpMestBloods.Select(o => o.TDL_BLOOD_TYPE_ID).ToList();
                    List<HIS_BLTY_SERVICE> bltyServiceCfgs = new HisBltyServiceGet().Get(filter);

                    if (IsNotNullOrEmpty(bltyServiceCfgs))
                    {
                        var Groups = hisExpMestBloods.GroupBy(g => g.TDL_BLOOD_TYPE_ID).ToList();
                        foreach (var group in Groups)
                        {
                            long count = group.ToList().Count;
                            List<HIS_BLTY_SERVICE> bltyServices = bltyServiceCfgs.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.BLOOD_TYPE_ID == group.Key && o.BLTY_AMOUNT == count).ToList();
                            if (!IsNotNullOrEmpty(bltyServices))
                            {
                                continue;
                            }

                            foreach (HIS_BLTY_SERVICE serv in bltyServices)
                            {
                                HIS_EXP_BLTY_SERVICE exp = new HIS_EXP_BLTY_SERVICE();
                                exp.BLOOD_TYPE_ID = group.Key;
                                exp.EXP_MEST_ID = expMest.ID;
                                exp.SERVICE_ID = serv.SERVICE_ID;
                                exp.SERVICE_AMOUNT = serv.SERVICE_AMOUNT;
                                exp.NUM_ORDER = serv.NUM_ORDER;
                                creates.Add(exp);
                            }
                        }

                        if (IsNotNullOrEmpty(creates) && !this.hisExpBltyServiceCreate.CreateList(creates))
                        {
                            throw new Exception("hisExpBltyServiceCreate. Ket thuc nghiep vu");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpBltyServiceCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
