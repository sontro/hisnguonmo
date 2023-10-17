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

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Delete
{
    class MatyReqProcessor : BusinessBase
    {
        private HisExpMestMatyReqDecreaseBcsReqAmount matyReqDecrease;
        private HisExpMestMaterialDecreaseBcsReqAmount materialDecrease;

        internal MatyReqProcessor(CommonParam param)
            : base(param)
        {
            this.matyReqDecrease = new HisExpMestMatyReqDecreaseBcsReqAmount(param);
            this.materialDecrease = new HisExpMestMaterialDecreaseBcsReqAmount(param);
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
                        this.ProcessDecreaseMatyReq(matyReqs, ref sqls);
                        this.ProcessDecreaseMaterial(matyReqs, ref sqls);
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

        private void ProcessDecreaseMatyReq(List<HIS_EXP_MEST_MATY_REQ> matyReqs, ref List<string> sqls)
        {
            List<HIS_BCS_MATY_REQ_REQ> bcsReqs = new HisBcsMatyReqReqGet().GetByExpMestMatyReqIds(matyReqs.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(bcsReqs))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = bcsReqs.GroupBy(g => g.PRE_EXP_MEST_MATY_REQ_ID).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.matyReqDecrease.Run(dicDecrease))
                {
                    throw new Exception("matyReqDecrease. Ket thuc nghiep vu");
                }

                string sql = DAOWorker.SqlDAO.AddInClause(bcsReqs.Select(s => s.ID).ToList(), "DELETE HIS_BCS_MATY_REQ_REQ WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessDecreaseMaterial(List<HIS_EXP_MEST_MATY_REQ> matyReqs, ref List<string> sqls)
        {
            List<HIS_BCS_MATY_REQ_DT> bcsReqs = new HisBcsMatyReqDtGet().GetByExpMestMatyReqIds(matyReqs.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(bcsReqs))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = bcsReqs.GroupBy(g => g.EXP_MEST_MATERIAL_ID).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.materialDecrease.Run(dicDecrease))
                {
                    throw new Exception("materialDecrease. Ket thuc nghiep vu");
                }

                string sql = DAOWorker.SqlDAO.AddInClause(bcsReqs.Select(s => s.ID).ToList(), "DELETE HIS_BCS_MATY_REQ_DT WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
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

        internal void Rollback()
        {
            this.materialDecrease.RollbackData();
            this.matyReqDecrease.Rollback();
        }
    }
}
