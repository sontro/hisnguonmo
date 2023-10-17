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

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Delete
{
    class MetyReqProcessor : BusinessBase
    {
        private HisExpMestMetyReqDecreaseBcsReqAmount metyReqDecrease;
        private HisExpMestMedicineDecreaseBcsReqAmount medicineDecrease;

        internal MetyReqProcessor(CommonParam param)
            : base(param)
        {
            this.metyReqDecrease = new HisExpMestMetyReqDecreaseBcsReqAmount(param);
            this.medicineDecrease = new HisExpMestMedicineDecreaseBcsReqAmount(param);
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
                        this.ProcessDecreaseMetyReq(metyReqs, ref sqls);
                        this.ProcessDecreaseMedicine(metyReqs, ref sqls);
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

        private void ProcessDecreaseMetyReq(List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<string> sqls)
        {
            List<HIS_BCS_METY_REQ_REQ> bcsReqs = new HisBcsMetyReqReqGet().GetByExpMestMetyReqIds(metyReqs.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(bcsReqs))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = bcsReqs.GroupBy(g => g.PRE_EXP_MEST_METY_REQ_ID).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.metyReqDecrease.Run(dicDecrease))
                {
                    throw new Exception("metyReqDecrease. Ket thuc nghiep vu");
                }

                string sql = DAOWorker.SqlDAO.AddInClause(bcsReqs.Select(s => s.ID).ToList(), "DELETE HIS_BCS_METY_REQ_REQ WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessDecreaseMedicine(List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<string> sqls)
        {
            List<HIS_BCS_METY_REQ_DT> bcsReqs = new HisBcsMetyReqDtGet().GetByExpMestMetyReqIds(metyReqs.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(bcsReqs))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = bcsReqs.GroupBy(g => g.EXP_MEST_MEDICINE_ID).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.medicineDecrease.Run(dicDecrease))
                {
                    throw new Exception("medicineDecrease. Ket thuc nghiep vu");
                }

                string sql = DAOWorker.SqlDAO.AddInClause(bcsReqs.Select(s => s.ID).ToList(), "DELETE HIS_BCS_METY_REQ_DT WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
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

        internal void Rollback()
        {
            this.medicineDecrease.RollbackData();
            this.metyReqDecrease.Rollback();
        }
    }
}
