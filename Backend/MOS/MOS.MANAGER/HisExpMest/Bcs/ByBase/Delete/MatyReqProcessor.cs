using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMatyReqDt;
using MOS.MANAGER.HisBcsMatyReqReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Delete
{
    class MatyReqProcessor : BusinessBase
    {
        internal MatyReqProcessor(CommonParam param)
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
                    List<HIS_EXP_MEST_MATY_REQ> matyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(matyReqs))
                    {
                        this.ProcessMetyReq(expMest, matyReqs, ref sqls);
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

        private void ProcessMetyReq(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> metyReqs, ref List<string> sqls)
        {
            foreach (var matyReq in metyReqs)
            {
                if (matyReq.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    throw new Exception("Ton tai HIS_EXP_MEST_MATY_REQ dang bi khoa" + LogUtil.TraceData("matyReq", matyReq));
                }
            }
            sqls.Add(String.Format("DELETE HIS_EXP_MEST_MATY_REQ WHERE EXP_MEST_ID = {0}", expMest.ID));
        }
    }
}
