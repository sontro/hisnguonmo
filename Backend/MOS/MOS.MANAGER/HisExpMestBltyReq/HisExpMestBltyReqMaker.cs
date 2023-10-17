using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    class HisExpMestBltyReqMaker : BusinessBase
    {
        private HisExpMestBltyReqCreate hisExpMestBltyReqCreate;

        internal HisExpMestBltyReqMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestBltyReqCreate = new HisExpMestBltyReqCreate(param);
        }

        internal bool Run(List<ExpBloodTypeSDO> bloods, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLTY_REQ> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(bloods) && expMest != null)
                {
                    //Luu theo tung id kho
                    List<HIS_EXP_MEST_BLTY_REQ> data = new List<HIS_EXP_MEST_BLTY_REQ>();

                    //Duyet theo y/c cua client de tao ra exp_mest_blood tuong ung
                    foreach (ExpBloodTypeSDO sdo in bloods)
                    {
                        HIS_EXP_MEST_BLTY_REQ exp = new HIS_EXP_MEST_BLTY_REQ();
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.BLOOD_ABO_ID = sdo.BloodAboId;
                        exp.BLOOD_RH_ID = sdo.BloodRhId;
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = sdo.Amount;
                        exp.BLOOD_TYPE_ID = sdo.BloodTypeId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.DESCRIPTION = sdo.Description;
                        data.Add(exp);
                    }

                    if (IsNotNullOrEmpty(data))
                    {
                        if (!this.hisExpMestBltyReqCreate.CreateList(data))
                        {
                            return false;
                        }
                        resultData = data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestBltyReqCreate.RollbackData();
        }
    }
}
