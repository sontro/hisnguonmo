using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMetyReqDt;
using MOS.MANAGER.HisBcsMetyReqReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Delete
{
    class MetyReqProcessor : BusinessBase
    {

        internal MetyReqProcessor(CommonParam param)
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
                    List<HIS_EXP_MEST_METY_REQ> metyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(metyReqs))
                    {
                        this.ProcessMetyReq(expMest, metyReqs, ref sqls);
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

        private void ProcessMetyReq(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<string> sqls)
        {
            foreach (var metyReq in metyReqs)
            {
                if (metyReq.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    throw new Exception("Ton tai HIS_EXP_MEST_METY_REQ dang bi khoa" + LogUtil.TraceData("metyReq", metyReq));
                }
            }
            sqls.Add(String.Format("DELETE HIS_EXP_MEST_METY_REQ WHERE EXP_MEST_ID = {0}", expMest.ID));
        }
    }
}
