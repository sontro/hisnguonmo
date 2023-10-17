using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST resultData)
        {
            try
            {
                if (serviceReq != null)
                {
                    //lay thong tin kho xuat dua vao execute_room_id cua service_req
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID).FirstOrDefault();
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.IS_STAR_MARK = serviceReq.IS_STAR_MARK;
                    expMest.SERVICE_REQ_ID = serviceReq.ID;
                    expMest.MEDI_STOCK_ID = mediStock.ID;

                    if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT)
                    {
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT;
                    }
                    else if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK)
                    {
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK;
                    }

                    if (!this.hisExpMestCreate.Create(expMest, serviceReq))
                    {
                        return false;
                    }
                    resultData = expMest;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
