using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPtttGroupBest;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroup
{
    partial class HisPtttGroupCreate : BusinessBase
    {
        private HIS_PTTT_GROUP recentHisPtttGroupDTO;

        private HisPtttGroupBestCreate hisPtttGroupBestCreate;

        internal HisPtttGroupCreate()
            : base()
        {
            this.hisPtttGroupBestCreate = new HisPtttGroupBestCreate(param);
        }

        internal HisPtttGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisPtttGroupBestCreate = new HisPtttGroupBestCreate(param);
        }

        internal bool Create(HisPtttGroupSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupCheck checker = new HisPtttGroupCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisPtttGroup);
                valid = valid && checker.ExistsCode(data.HisPtttGroup.PTTT_GROUP_CODE, null);
                valid = valid && checker.HasNoExistedNumOrder(data.HisPtttGroup.NUM_ORDER, false, null);
                if (valid)
                {

                    if (!DAOWorker.HisPtttGroupDAO.Create(data.HisPtttGroup))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttGroupDTO = data.HisPtttGroup;

                    if (IsNotNullOrEmpty(data.HisPtttGroupBests))
                    {
                        data.HisPtttGroupBests.ForEach(o => o.PTTT_GROUP_ID = data.HisPtttGroup.ID);
                        if (!this.hisPtttGroupBestCreate.CreateList(data.HisPtttGroupBests))
                        {
                            throw new Exception("hisPtttGroupBestCreate. Ket thuc nghiep vu");
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.hisPtttGroupBestCreate.RollbackData();
            if (this.recentHisPtttGroupDTO != null)
            {
                if (!DAOWorker.HisPtttGroupDAO.Truncate(this.recentHisPtttGroupDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttGroupDTO", this.recentHisPtttGroupDTO));
                }
            }
        }
    }
}
