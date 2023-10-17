using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    class HisExpMestMetyReqMaker : BusinessBase
    {
        private HisExpMestMetyReqCreate hisExpMestMetyReqCreate;

        internal HisExpMestMetyReqMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMetyReqCreate = new HisExpMestMetyReqCreate(param);
        }

        internal bool Run(List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> resultData, long? treatmentId = null)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    //Luu theo tung id kho
                    List<HIS_EXP_MEST_METY_REQ> data = new List<HIS_EXP_MEST_METY_REQ>();
                    
                    //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                    foreach (ExpMedicineTypeSDO sdo in medicines)
                    {
                        HIS_EXP_MEST_METY_REQ exp = new HIS_EXP_MEST_METY_REQ();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = sdo.Amount;
                        exp.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.DESCRIPTION = sdo.Description;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TREATMENT_ID = treatmentId;
                        data.Add(exp);                        
                    }

                    if (IsNotNullOrEmpty(data))
                    {
                        if (!this.hisExpMestMetyReqCreate.CreateList(data))
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
            this.hisExpMestMetyReqCreate.RollbackData();
        }
    }
}
