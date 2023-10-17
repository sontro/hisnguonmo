using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HisMestPeriodApproveSDO data, List<ExpMaterialSDO> ExpMaterials, List<ExpMedicineSDO> ExpMedicines, HIS_MEDI_STOCK_PERIOD period, ref HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(ExpMaterials) || IsNotNullOrEmpty(ExpMedicines))
                {
                    HIS_EXP_MEST exp = new HIS_EXP_MEST();

                    //Tao exp_mest
                    exp.MEDI_STOCK_ID = period.MEDI_STOCK_ID;
                    exp.REQ_ROOM_ID = data.WorkingRoomId;
                    exp.EXP_MEST_REASON_ID = HisExpMestCFG.EXP_MEST_REASON_INVE_ID;
                    exp.DESCRIPTION = data.Description;
                    exp.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__KHAC; //xuat khac
                    exp.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    exp.SOURCE_MEST_PERIOD_ID = period.ID;

                    if (!this.hisExpMestCreate.Create(exp))
                    {
                        throw new Exception("hisExpMestCreate. Ket thuc nghiep vu");
                    }
                    expMest = exp;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
