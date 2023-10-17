using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisMediStockExty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    class ExportAutoProcessor : BusinessBase
    {
        private HisExpMestExport hisExpMestExport;

        internal ExportAutoProcessor()
            : base()
        {
            this.hisExpMestExport = new HisExpMestExport(param);
        }

        internal ExportAutoProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestExport = new HisExpMestExport(param);
        }


        internal bool Run(HIS_EXP_MEST expMest, HisExpMestApproveSDO data)
        {
            return this.Run(expMest, data, null, null);
        }

        internal bool Run(HIS_EXP_MEST expMest, HisExpMestApproveSDO data, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            bool result = false;
            try
            {
                if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                    && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL)
                {
                    HIS_MEDI_STOCK_EXTY mediStockExty = HisMediStockExtyCFG.DATA != null ?
                        HisMediStockExtyCFG.DATA
                        .Where(o => o.MEDI_STOCK_ID == expMest.MEDI_STOCK_ID
                            && o.EXP_MEST_TYPE_ID == expMest.EXP_MEST_TYPE_ID)
                        .FirstOrDefault() : null;

                    //Neu kho cho phep tu dong thuc xuat thi thuc hien xuat
                    if (new HisExpMestCheck().IsAutoStockTransfer(expMest)
                        || (mediStockExty != null && mediStockExty.IS_AUTO_EXECUTE == Constant.IS_TRUE))
                    {
                        HIS_EXP_MEST resultData = null;
                        HisExpMestExportSDO sdo = new HisExpMestExportSDO();
                        sdo.ExpMestId = data.ExpMestId;
                        sdo.ReqRoomId = data.ReqRoomId;
                        sdo.IsFinish = data.IsFinish ?? false;
                        if (!this.hisExpMestExport.Export(sdo, true, medicines, materials, ref resultData))
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
