using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class BloodProcessor : BusinessBase
    {
        private List<HisExpMestBltyReqMaker> bltyReqMakers;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.bltyReqMakers = new List<HisExpMestBltyReqMaker>();
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> dicExpMest, ref List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs)
        {
            bool result = false;
            try
            {
                expMestBltyReqs = new List<HIS_EXP_MEST_BLTY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    ExpMestChmsDetailSDO sdo = dic.Value;
                    HIS_EXP_MEST expMest = dic.Key;

                    if (IsNotNullOrEmpty(sdo.BloodTypes))
                    {
                        List<HIS_EXP_MEST_BLTY_REQ> request = null;
                        HisExpMestBltyReqMaker maker = new HisExpMestBltyReqMaker(param);
                        this.bltyReqMakers.Add(maker);
                        if (!maker.Run(sdo.BloodTypes, expMest, ref request))
                        {
                            throw new Exception("hisExpMestBltyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                        }

                        expMestBltyReqs.AddRange(request);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }


        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.bltyReqMakers))
                {
                    foreach (var maker in this.bltyReqMakers.AsEnumerable().Reverse())
                    {
                        try
                        {
                            maker.Rollback();
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
