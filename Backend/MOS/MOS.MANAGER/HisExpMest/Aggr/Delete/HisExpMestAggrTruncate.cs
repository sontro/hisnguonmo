using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Delete
{
    /// <summary>
    /// Xóa phiếu lĩnh:
    /// 1. Chỉ ở kho mới cho phép thực hiện
    /// 2. Phiếu chưa duyệt mới cho phép xóa
    /// 3. Cập nhật agg_exp_mest_id của các phiếu con ==> null
    /// </summary>
    class HisExpMestAggrTruncate : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private OddExpMestProcessor oddExpMestProcessor;

        internal HisExpMestAggrTruncate()
            : base()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.oddExpMestProcessor = new OddExpMestProcessor(param);
        }

        internal HisExpMestAggrTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            hisExpMestUpdate = new HisExpMestUpdate(param);
            this.oddExpMestProcessor = new OddExpMestProcessor(param);
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnlock(raw);
                valid = valid && checker.VerifyStatusForDelete(raw);
                if (valid)
                {
                    if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Chi co phieu linh moi duoc thuc hien chuc nang nay" + LogUtil.TraceData("expMest", raw));
                    }

                    List<string> sqls = new List<string>();
                    if (!this.oddExpMestProcessor.Run(raw, ref sqls))
                    {
                        throw new Exception("oddExpMestProcessor. Ket thuc nghiep vu");
                    }

                    //update exp_mest con
                    string sqlExpMest = string.Format("UPDATE HIS_EXP_MEST SET AGGR_EXP_MEST_ID = NULL, TDL_AGGR_EXP_MEST_CODE = NULL WHERE AGGR_EXP_MEST_ID = {0}", id);
                    //update exp_mest_medicine
                    string sqlMedicine = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = NULL WHERE TDL_AGGR_EXP_MEST_ID = {0} ", id);
                    //update exp_mest_material
                    string sqlMaterial = string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = NULL WHERE TDL_AGGR_EXP_MEST_ID = {0} ", id);

                    //Xoa phieu linh
                    string sqlDelete = string.Format("DELETE HIS_EXP_MEST WHERE ID = {0} ", id);

                    sqls.Add(sqlExpMest);
                    sqls.Add(sqlMedicine);
                    sqls.Add(sqlMaterial);
                    sqls.Add(sqlDelete);

                    result = DAOWorker.SqlDAO.Execute(sqls);

                    if (result)
                    {
                        new EventLogGenerator(EventLog.Enum.HisExpMest_HuyPhieuXuat).ExpMestCode(raw.EXP_MEST_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void RollbackData()
        {
            this.hisExpMestUpdate.RollbackData();
        }

    }
}
