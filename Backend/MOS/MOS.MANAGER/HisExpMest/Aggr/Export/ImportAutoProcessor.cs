using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Export
{
    class ImportAutoProcessor : BusinessBase
    {
        private List<HisImpMestChmsCreate> hisImpMestChmsCreates;

        internal ImportAutoProcessor()
            : base()
        {
            this.hisImpMestChmsCreates = new List<HisImpMestChmsCreate>();
        }

        internal ImportAutoProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestChmsCreates = new List<HisImpMestChmsCreate>();
        }

        internal bool Run(List<HIS_EXP_MEST> expMests, long reqRoomId)
        {
            bool result = true;
            try
            {
                if (!IsNotNullOrEmpty(expMests))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongTaoPhieuNhapThatBai);
                    throw new Exception("Tu dong tao phieu nhap bu le that bai expMests null");
                }
                bool valid = true;
                foreach (HIS_EXP_MEST item in expMests)
                {
                    if (item.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL)
                    {
                        continue;
                    }
                    if (item.IMP_MEDI_STOCK_ID.HasValue)
                    {
                        HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                        impMest.MEDI_STOCK_ID = item.IMP_MEDI_STOCK_ID.Value;
                        impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL;

                        impMest.REQ_ROOM_ID = reqRoomId;
                        impMest.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        impMest.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                        impMest.CHMS_EXP_MEST_ID = item.ID;
                        impMest.TDL_CHMS_EXP_MEST_CODE = item.EXP_MEST_CODE;

                        HisImpMestResultSDO resultData = null;

                        HisImpMestChmsCreate impMestChmsCreate = new HisImpMestChmsCreate();

                        if (!impMestChmsCreate.Create(impMest, ref resultData))
                        {
                            LogSystem.Warn("Tu dong tao phieu nhap bu le that bai ExpMestCode: " + item.EXP_MEST_CODE);
                            valid = false;
                        }
                        else
                        {
                            this.hisImpMestChmsCreates.Add(impMestChmsCreate);
                        }
                    }
                    else
                    {
                        LogSystem.Warn("Phieu xuat bu le khong co IMP_MEDI_STOCK_ID ExpMestCode: " + item.EXP_MEST_CODE);
                        valid = false;
                    }
                }
                if (!valid)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongTaoPhieuNhapThatBai);
                }
                result = valid;
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
                if (IsNotNullOrEmpty(this.hisImpMestChmsCreates))
                {
                    foreach (var impMestChmsCreate in this.hisImpMestChmsCreates)
                    {
                        impMestChmsCreate.RollbackData();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
