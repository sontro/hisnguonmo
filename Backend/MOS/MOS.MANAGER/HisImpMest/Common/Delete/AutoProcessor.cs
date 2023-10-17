using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Unexport;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    class AutoProcessor : BusinessBase
    {
        internal AutoProcessor()
            : base()
        {

        }

        internal AutoProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = true;
            try
            {
                if (expMest != null && HisExpMestConstant.CHMS_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL)
                    {
                        return result;
                    }
                    V_HIS_MEDI_STOCK mediStock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.IMP_MEDI_STOCK_ID);
                    if (mediStock == null)
                    {
                        throw new Exception("Khong lay duoc V_HIS_MEDI_STOCK theo id: " + expMest.IMP_MEDI_STOCK_ID);
                    }
                    if (mediStock.IS_AUTO_CREATE_CHMS_IMP == MOS.UTILITY.Constant.IS_TRUE && this.CheckHasNotImpMest(expMest))
                    {
                        HIS_EXP_MEST resultData = null;
                        HisExpMestSDO sdo = new HisExpMestSDO();
                        sdo.ExpMestId = expMest.ID;
                        sdo.ReqRoomId = expMest.REQ_ROOM_ID;
                        if (!new HisExpMestUnexport().Run(sdo, true, ref resultData))
                        {
                            throw new Exception("Tu dong huy thuc xuat phieu xuat chuyen kho that bai ExpMestCode: " + expMest.EXP_MEST_CODE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool CheckHasNotImpMest(HIS_EXP_MEST expMest)
        {
            bool result = true;
            try
            {
                List<HIS_IMP_MEST> chmsImpMests = new HisImpMestGet().GetByChmsExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(chmsImpMests))
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
