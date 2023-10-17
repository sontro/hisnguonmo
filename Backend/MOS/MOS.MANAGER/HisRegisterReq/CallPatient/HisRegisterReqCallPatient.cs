using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterReq
{
    class HisRegisterReqCallPatient : BusinessBase
    {
        private List<HIS_REGISTER_REQ> beforeUpdates = null;

        internal HisRegisterReqCallPatient()
            : base()
        {
            this.Init();
        }

        internal HisRegisterReqCallPatient(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beforeUpdates = new List<HIS_REGISTER_REQ>();
        }

        internal bool Run(CallPatientSDO data)
        {
            bool result = false;
            try
            {
                List<HIS_REGISTER_REQ> listRaw = new List<HIS_REGISTER_REQ>();
                HisRegisterReqCheck commonChecker = new HisRegisterReqCheck(param);
                HisRegisterReqCallPatientCheck checker = new HisRegisterReqCallPatientCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyIds(data.RegisterReqIds, listRaw);
                valid = valid && checker.HasCallPlaceInfo(data.CallPlace);
                if (valid)
                {
                    this.beforeUpdates.AddRange(listRaw);

                    listRaw.ForEach(o => { o.CALL_TIME = Inventec.Common.DateTime.Get.Now(); o.CALL_PLACE = data.CallPlace; });

                    if (!DAOWorker.HisRegisterReqDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegisterReq that bai." + LogUtil.TraceData("data", listRaw));
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

        public void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdates))
            {
                if (!DAOWorker.HisRegisterReqDAO.UpdateList(this.beforeUpdates))
                {
                    LogSystem.Warn("Rollback du lieu HisRegisterReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRegisterReq", this.beforeUpdates));
                }
            }
        }
    }
}
