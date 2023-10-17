using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    class HisExpMestMatyReqMaker : BusinessBase
    {
        private HisExpMestMatyReqCreate hisExpMestMatyReqCreate;

        internal HisExpMestMatyReqMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMatyReqCreate = new HisExpMestMatyReqCreate(param);
        }

        internal bool Run(List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> resultData, long? treatmentId = null)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    //Luu theo tung id kho
                    List<HIS_EXP_MEST_MATY_REQ> data = new List<HIS_EXP_MEST_MATY_REQ>();
                    //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                    foreach (ExpMaterialTypeSDO sdo in materials)
                    {
                        HIS_EXP_MEST_MATY_REQ exp = new HIS_EXP_MEST_MATY_REQ();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = sdo.Amount;
                        exp.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.DESCRIPTION = sdo.Description;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TREATMENT_ID = treatmentId;
                        data.Add(exp);
                    }

                    if (IsNotNullOrEmpty(data))
                    {
                        if (!this.hisExpMestMatyReqCreate.CreateList(data))
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
            this.hisExpMestMatyReqCreate.RollbackData();
        }
    }
}
