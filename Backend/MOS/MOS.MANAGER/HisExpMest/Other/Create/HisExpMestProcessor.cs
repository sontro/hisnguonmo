using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisExpMest.Other.Create
{
    class HisExpMestProcessor: BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HisExpMestOtherSDO data, ref HIS_EXP_MEST hisExpMest)
        {
            try
            {
                HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                
                //Tao exp_mest
                expMest.MEDI_STOCK_ID = data.MediStockId;
                expMest.REQ_ROOM_ID = data.ReqRoomId;
                expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                expMest.DESCRIPTION = data.Description;
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__KHAC; //xuat khac
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                expMest.RECIPIENT = data.Recipient;
                expMest.RECEIVING_PLACE = data.ReceivingPlace;
                expMest.DISCOUNT = data.Discount;

                if (!this.hisExpMestCreate.Create(expMest))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                hisExpMest = expMest;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
