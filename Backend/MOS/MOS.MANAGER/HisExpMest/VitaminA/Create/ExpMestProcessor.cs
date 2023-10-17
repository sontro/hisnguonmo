using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.VitaminA.Create
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HisExpMestVitaminASDO data, ref HIS_EXP_MEST hisExpMest)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = new HIS_EXP_MEST();

                //Tao exp_mest
                expMest.MEDI_STOCK_ID = data.MediStockId;
                expMest.REQ_ROOM_ID = data.ReqRoomId;
                expMest.DESCRIPTION = data.Description;
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VITAMINA; //xuat khac
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;

                if (!this.hisExpMestCreate.Create(expMest))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                hisExpMest = expMest;
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
                this.hisExpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
