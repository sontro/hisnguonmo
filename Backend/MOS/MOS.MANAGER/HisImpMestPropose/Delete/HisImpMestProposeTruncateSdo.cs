using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMestPropose.Delete
{
    class HisImpMestProposeTruncateSdo : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;

        internal HisImpMestProposeTruncateSdo()
            : base()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal HisImpMestProposeTruncateSdo(CommonParam param)
            : base(param)
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal bool Run(HisImpMestProposeDeleteSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                WorkPlaceSDO workPlace = null;
                HIS_IMP_MEST_PROPOSE raw = null;
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.Id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.CheckWorkingRoom(data.WorkingRoomId, raw);
                valid = valid && checker.IsNotExistsImpMestPay(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessHisImpMest(raw);
                    this.ProcessHisImpMestPropose(raw, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls. Xoa HIS_IMP_MEST_PROPOSE that bai. Rollback du lieu. Sql: " + sqls.ToString());
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMestPropose(HIS_IMP_MEST_PROPOSE raw, ref List<string> sqls)
        {
            string sql = String.Format("DELETE HIS_IMP_MEST_PROPOSE WHERE ID = {0}", raw.ID);
            sqls.Add(sql);
        }

        private void ProcessHisImpMest(HIS_IMP_MEST_PROPOSE raw)
        {
            List<HIS_IMP_MEST> impMests = new HisImpMestGet(param).GetByImpMestProposeId(raw.ID);
            if (param.HasException)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.CoLoiXayRa);
                throw new Exception("Co loi xay ra khi get HIS_IMP_MEST the IMP_MEST_PROPOSE_ID");
            }

            if (IsNotNullOrEmpty(impMests))
            {
                Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                List<HIS_IMP_MEST> befores = Mapper.Map<List<HIS_IMP_MEST>>(impMests);
                impMests.ForEach(o => o.IMP_MEST_PROPOSE_ID = null);
                if (!this.hisImpMestUpdate.UpdateList(impMests, befores))
                {
                    throw new Exception("hisImpMestUpdate. Cap nhat HIS_IMP_MEST that bai");
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
