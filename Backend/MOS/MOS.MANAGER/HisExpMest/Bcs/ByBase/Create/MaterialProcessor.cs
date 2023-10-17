using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMatyReqDt;
using MOS.MANAGER.HisBcsMatyReqReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqCreate hisExpMestMatyReqCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMatyReqCreate = new HisExpMestMatyReqCreate(param);
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest)
        {
            bool result = false;
            try
            {

                List<HIS_EXP_MEST_MATY_REQ> inserteds = new List<HIS_EXP_MEST_MATY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    if (IsNotNullOrEmpty(dic.Value.Materials))
                    {
                        List<HIS_EXP_MEST_MATY_REQ> datas = this.MakeExpMestMatyReq(dic.Key, dic.Value.Materials);
                        inserteds.AddRange(datas);
                    }
                }

                if (IsNotNullOrEmpty(inserteds))
                {
                    if (!this.hisExpMestMatyReqCreate.CreateList(inserteds))
                    {
                        throw new Exception("hisExpMestMatyReqCreate. Ket thuc nghiep vu");
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

        private List<HIS_EXP_MEST_MATY_REQ> MakeExpMestMatyReq(HIS_EXP_MEST expMest, List<BaseMaterialTypeSDO> data)
        {
            List<HIS_EXP_MEST_MATY_REQ> reqDatas = new List<HIS_EXP_MEST_MATY_REQ>();

            foreach (BaseMaterialTypeSDO reqSdo in data)
            {
                Dictionary<long, HIS_EXP_MEST_MATY_REQ> dicReq = new Dictionary<long, HIS_EXP_MEST_MATY_REQ>();

                HIS_EXP_MEST_MATY_REQ r = new HIS_EXP_MEST_MATY_REQ();
                r.MATERIAL_TYPE_ID = reqSdo.MaterialTypeId;
                r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                r.EXP_MEST_ID = expMest.ID;
                r.AMOUNT = reqSdo.Amount;
                reqDatas.Add(r);
            }

            return reqDatas;
        }

        internal void Rollback()
        {
            this.hisExpMestMatyReqCreate.RollbackData();
        }
    }
}
