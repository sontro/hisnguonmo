using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.InPres.Export;
using MOS.MANAGER.HisMediStockExty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Approve
{
    class AutoProcessor : BusinessBase
    {
        private HisExpMestInPresExport hisExpMestInPresExport;

        internal AutoProcessor()
            : base()
        {
            this.hisExpMestInPresExport = new HisExpMestInPresExport(param);
        }

        internal AutoProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestInPresExport = new HisExpMestInPresExport(param);
        }


        internal bool Run(HisExpMestSDO data, HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest == null || data == null)
                {
                    return false;
                }
                HIS_MEDI_STOCK_EXTY mediStockExty = HisMediStockExtyCFG.DATA != null ?
                    HisMediStockExtyCFG.DATA
                    .Where(o => o.MEDI_STOCK_ID == expMest.MEDI_STOCK_ID
                        && o.EXP_MEST_TYPE_ID == expMest.EXP_MEST_TYPE_ID)
                    .FirstOrDefault() : null;

                //Neu kho cho phep tu dong thuc xuat thi thuc hien xuat
                if (mediStockExty != null && mediStockExty.IS_AUTO_EXECUTE == Constant.IS_TRUE)
                {
                    HIS_EXP_MEST resultData = null;
                    HisExpMestSDO sdo = new HisExpMestSDO();
                    sdo.ExpMestId = data.ExpMestId;
                    sdo.ReqRoomId = data.ReqRoomId;

                    if (!this.hisExpMestInPresExport.Run(sdo, ref resultData))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongXuatThatBai);
                        LogSystem.Warn("Tu dong thuc xuat that bai." + LogUtil.TraceData("expMest", expMest));
                    }
                    else
                    {
                        //cap nhat lai trang thai cua du lieu truyen vao
                        expMest.EXP_MEST_STT_ID = resultData.EXP_MEST_STT_ID;
                        expMest.FINISH_TIME = resultData.FINISH_TIME;
                        expMest.FINISH_DATE = resultData.FINISH_DATE;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
