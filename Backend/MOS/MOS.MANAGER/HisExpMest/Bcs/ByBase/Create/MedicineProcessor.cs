using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMetyReqDt;
using MOS.MANAGER.HisBcsMetyReqReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqCreate hisExpMestMetyReqCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMetyReqCreate = new HisExpMestMetyReqCreate(param);
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest)
        {
            bool result = false;
            try
            {

                List<HIS_EXP_MEST_METY_REQ> inserteds = new List<HIS_EXP_MEST_METY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    if (IsNotNullOrEmpty(dic.Value.Medicines))
                    {
                        List<HIS_EXP_MEST_METY_REQ> datas = this.MakeExpMestMetyReq(dic.Key, dic.Value.Medicines);
                        inserteds.AddRange(datas);
                    }
                }

                if (IsNotNullOrEmpty(inserteds))
                {
                    if (!this.hisExpMestMetyReqCreate.CreateList(inserteds))
                    {
                        throw new Exception("hisExpMestMetyReqCreate. Ket thuc nghiep vu");
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

        private List<HIS_EXP_MEST_METY_REQ> MakeExpMestMetyReq(HIS_EXP_MEST expMest, List<BaseMedicineTypeSDO> data)
        {
            List<HIS_EXP_MEST_METY_REQ> reqDatas = new List<HIS_EXP_MEST_METY_REQ>();

            foreach (BaseMedicineTypeSDO reqSdo in data)
            {
                Dictionary<long, HIS_EXP_MEST_METY_REQ> dicReq = new Dictionary<long, HIS_EXP_MEST_METY_REQ>();

                HIS_EXP_MEST_METY_REQ r = new HIS_EXP_MEST_METY_REQ();
                r.MEDICINE_TYPE_ID = reqSdo.MedicineTypeId;
                r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                r.EXP_MEST_ID = expMest.ID;
                r.AMOUNT = reqSdo.Amount;
                reqDatas.Add(r);
            }

            return reqDatas;
        }

        internal void Rollback()
        {
            this.hisExpMestMetyReqCreate.RollbackData();
        }
    }
}
