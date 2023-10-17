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

namespace MOS.MANAGER.HisExpMest.Manu.Update
{
    class HisExpMestProcessor: BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestManuSDO data, HIS_EXP_MEST hisExpMest, HIS_IMP_MEST manuImpMest, ref HIS_EXP_MEST resultData)
        {
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(hisExpMest);

                //Tao exp_mest
                hisExpMest.MEDI_STOCK_ID = data.MediStockId;
                hisExpMest.REQ_ROOM_ID = data.ReqRoomId;
                hisExpMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__TNCC; //xuat tra NCC
                hisExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                hisExpMest.MANU_IMP_MEST_ID = data.ManuImpMestId;
                hisExpMest.TDL_MANU_IMP_MEST_CODE = manuImpMest.IMP_MEST_CODE; //luu du thua du lieu
                hisExpMest.SUPPLIER_ID = manuImpMest.SUPPLIER_ID;
                hisExpMest.DESCRIPTION = data.Description;
                hisExpMest.RECIPIENT = data.Recipient;
                hisExpMest.RECEIVING_PLACE = data.ReceivingPlace;
                hisExpMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;

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
