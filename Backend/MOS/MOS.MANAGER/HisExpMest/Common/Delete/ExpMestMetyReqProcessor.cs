using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMetyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class ExpMestMetyReqProcessor : BusinessBase
    {
        internal ExpMestMetyReqProcessor(CommonParam param)
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
                    List<HIS_EXP_MEST_METY_REQ> hisExpMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestMetyReqs))
                    {
                        foreach (var metyReq in hisExpMestMetyReqs)
                        {
                            if (metyReq.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_METY_REQ dang bi khoa" + LogUtil.TraceData("metyReq", metyReq));
                                return false;
                            }
                        }
                        string deleteMetyReq = new StringBuilder().Append("DELETE HIS_EXP_MEST_METY_REQ WHERE EXP_MEST_ID = ").Append(expMest.ID).ToString();
                        listSql.Add(deleteMetyReq);
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
