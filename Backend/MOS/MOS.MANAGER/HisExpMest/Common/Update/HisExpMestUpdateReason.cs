using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Confirm;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestReason;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Update
{
    class HisExpMestUpdateReason : BusinessBase
    {
        private HisExpMestUpdate expMestUpdate;
        private HisExpMestUpdate expMestUpdateForChildren;

        internal HisExpMestUpdateReason()
            : base()
        {
            this.Init();
        }

        internal HisExpMestUpdateReason(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestUpdate = new HisExpMestUpdate(param);
            this.expMestUpdateForChildren = new HisExpMestUpdate(param);
        }

        internal bool Run(ExpMestUpdateReasonSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST raw = null;
                List<HIS_EXP_MEST> children = null;
                WorkPlaceSDO workPlace = null;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                HisExpMestConfirmCheck confirmChecker = new HisExpMestConfirmCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.WorkingInMediStockOrIsCreator(raw, workPlace);
                valid = valid && checker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    if (data.ExpMestReasonId != raw.EXP_MEST_REASON_ID)
                    {
                        HIS_EXP_MEST_REASON oldReason = null;
                        HIS_EXP_MEST_REASON newReason = null;
                        if (data.ExpMestReasonId.HasValue)
                            newReason = new HisExpMestReasonGet().GetById(data.ExpMestReasonId.Value);
                        if (raw.EXP_MEST_REASON_ID.HasValue)
                            oldReason = new HisExpMestReasonGet().GetById(raw.EXP_MEST_REASON_ID.Value);

                        Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                        HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(raw);
                        
                        raw.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                        if (!expMestUpdate.Update(raw, before))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_CapNhatThatBai);
                            throw new Exception("Sua thong tin HisExpMest that bai." + LogUtil.TraceData("listData", raw));
                        }

                        this.ProcessForChildren(raw, ref children);

                        new EventLogGenerator(EventLog.Enum.HisExpMest_CapNhatLyDoXuat, raw.EXP_MEST_CODE, oldReason != null ? oldReason.EXP_MEST_REASON_NAME : "", newReason != null ? newReason.EXP_MEST_REASON_NAME : "").Run();
                    }
                    result = true;
                    resultData = raw;
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

        private void ProcessForChildren(HIS_EXP_MEST raw, ref List<HIS_EXP_MEST> children)
        {
            if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
            {
                children = new HisExpMestGet().GetByAggrExpMestId(raw.ID);
                if (IsNotNullOrEmpty(children))
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    List<HIS_EXP_MEST> childrenBefore = Mapper.Map<List<HIS_EXP_MEST>>(children);

                    children.ForEach(o => o.EXP_MEST_REASON_ID = raw.EXP_MEST_REASON_ID);

                    if (!this.expMestUpdateForChildren.UpdateList(children, childrenBefore))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin ly do xuat cho phieu con cua tong hop phong kham hoac phieu linh that bai." + LogUtil.TraceData("listData", children));
                    }
                }
            }
        }

        internal void RollbackData()
        {
            this.expMestUpdateForChildren.RollbackData();
            this.expMestUpdate.RollbackData();
        }
    }
}