using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    class ImportAutoProcessor : BusinessBase
    {
        private HisImpMestChmsCreate hisImpMestChmsCreate;

        internal ImportAutoProcessor()
            : base()
        {
            this.hisImpMestChmsCreate = new HisImpMestChmsCreate();
        }

        internal ImportAutoProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestChmsCreate = new HisImpMestChmsCreate();
        }

        internal bool Run(HIS_EXP_MEST expMest, long reqRoomId)
        {
            bool result = true;
            try
            {
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("expMest null");
                }
                if (!HisExpMestConstant.CHMS_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    return true;
                }
                if (expMest.IMP_MEDI_STOCK_ID.HasValue)
                {
                    if (!new HisExpMestCheck().IsAutoStockTransfer(expMest))
                    {
                        if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL)
                        {
                            V_HIS_MEDI_STOCK mediStock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.IMP_MEDI_STOCK_ID.Value);
                            if (mediStock == null)
                            {
                                LogSystem.Info("Khong lay duoc Kho " + LogUtil.TraceData("mediStock", mediStock));
                                return false;
                            }

                            if (mediStock.IS_AUTO_CREATE_CHMS_IMP != Constant.IS_TRUE)
                            {
                                return true;
                            }
                        }
                    }

                    HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                    impMest.MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK)
                    {
                        impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK;
                    }
                    else if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                    {
                        impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCS;
                    }
                    else
                    {
                        impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL;
                    }

                    impMest.REQ_ROOM_ID = reqRoomId;
                    impMest.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    impMest.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                    impMest.CHMS_EXP_MEST_ID = expMest.ID;
                    impMest.TDL_CHMS_EXP_MEST_CODE = expMest.EXP_MEST_CODE;

                    HisImpMestResultSDO resultData = null;

                    if (!this.hisImpMestChmsCreate.Create(impMest, ref resultData))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongTaoPhieuNhapThatBai);
                    }
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestChmsCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
