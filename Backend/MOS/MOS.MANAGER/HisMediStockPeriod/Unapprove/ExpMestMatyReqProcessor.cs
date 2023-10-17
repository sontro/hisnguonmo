using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMatyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class ExpMestMatyReqProcessor : BusinessBase
    {
        internal ExpMestMatyReqProcessor(CommonParam param)
            : base(param)
        {
     
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MATY_REQ> hisExpMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestMatyReqs))
                    {
                        foreach (var matyReq in hisExpMestMatyReqs)
                        {
                            if (matyReq.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_MATY_REQ dang bi khoa" + LogUtil.TraceData("matyReq", matyReq));
                                return false;
                            }
                        }                        
                        string deleteMatyReq = new StringBuilder().Append("DELETE HIS_EXP_MEST_MATY_REQ WHERE EXP_MEST_ID = ").Append(expMest.ID).ToString();
                        sqls.Add(deleteMatyReq);
                    }
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
    }
}
