using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HisExpMestChmsListSDO data, ref Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> resultData)
        {
            bool result = false;
            try
            {
                resultData = new Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO>();
                List<HIS_EXP_MEST> lstExpMest = new List<HIS_EXP_MEST>();
                foreach (ExpMestChmsDetailSDO sdo in data.ExpMests)
                {
                    bool isByPackage = (IsNotNullOrEmpty(sdo.Medicines) || IsNotNullOrEmpty(sdo.Materials));
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    //Tao exp_mest
                    expMest.MEDI_STOCK_ID = sdo.ExpMediStockId;
                    expMest.REQ_ROOM_ID = data.WorkingRoomId;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK; //xuat chuyen kho
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.IMP_MEDI_STOCK_ID = sdo.ImpMediStockId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.RECIPIENT = data.Recipient;
                    expMest.RECEIVING_PLACE = data.ReceivingPlace;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;

                    if (isByPackage)
                    {
                        expMest.IS_REQUEST_BY_PACKAGE = Constant.IS_TRUE;
                    }
                    else
                    {
                        expMest.IS_REQUEST_BY_PACKAGE = null;
                    }
                    lstExpMest.Add(expMest);
                    resultData[expMest] = sdo;
                }

                if (!this.hisExpMestCreate.CreateList(lstExpMest))
                {
                    throw new Exception("hisExpMestCreate. Ket thuc nghiep vu");
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

        internal void Rollback()
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
