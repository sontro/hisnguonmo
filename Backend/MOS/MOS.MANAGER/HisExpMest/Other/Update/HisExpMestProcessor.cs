using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Other.Update
{
    class HisExpMestProcessor: BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestOtherSDO data, HIS_EXP_MEST hisExpMest, ref HIS_EXP_MEST resultData)
        {
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(hisExpMest);

                //Tao exp_mest
                hisExpMest.MEDI_STOCK_ID = data.MediStockId;
                hisExpMest.REQ_ROOM_ID = data.ReqRoomId;
                hisExpMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                hisExpMest.DESCRIPTION = data.Description;
                hisExpMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__KHAC; //xuat khac
                hisExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                hisExpMest.RECIPIENT = data.Recipient;
                hisExpMest.RECEIVING_PLACE = data.ReceivingPlace;
                hisExpMest.DISCOUNT = data.Discount;

                if (ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST>(before, hisExpMest))
                {
                    if (!this.hisExpMestUpdate.Update(hisExpMest, before))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                resultData = hisExpMest;
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
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
