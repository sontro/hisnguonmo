using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBltyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class ExpMestBltyReqProcessor : BusinessBase
    {

        internal ExpMestBltyReqProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_BLTY_REQ> hisExpMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestBltyReqs))
                    {
                        foreach (var matyReq in hisExpMestBltyReqs)
                        {
                            if (matyReq.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_BLTY_REQ dang bi khoa" + LogUtil.TraceData("matyReq", matyReq));
                                return false;
                            }

                        }
                        
                        string deleteBltyReq = new StringBuilder().Append("DELETE HIS_EXP_MEST_BLTY_REQ WHERE EXP_MEST_ID = ").Append(expMest.ID).ToString();
                        listSql.Add(deleteBltyReq);
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
