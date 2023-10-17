using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDebate
{
    class UpdateTelemedicineInfo : BusinessBase
    {
        private HisDebateUpdate updateProcessor;

        internal UpdateTelemedicineInfo()
            : base()
        {
            this.Init();
        }

        internal UpdateTelemedicineInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.updateProcessor = new HisDebateUpdate(param);
        }

        internal bool Run(DebateTelemedicineSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DEBATE debate = null;
                UpdateTelemedicineInfoCheck checker = new UpdateTelemedicineInfoCheck(param);
                HisDebateCheck commonChecker = new HisDebateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.DebateId, ref debate);
                if (valid)
                {
                    debate.TMP_ID = data.TmpId;
                    if (!updateProcessor.Update(debate))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebate_CapNhatThatBai);
                        return false;
                    }
                    result = true;
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

        internal void RollbackData()
        {
            this.updateProcessor.RollbackData();
        }
    }
}