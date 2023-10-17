using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisAntibioticRequest;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAntibioticRequest
{
    class HisAntibioticRequestApprove : BusinessBase
    {
        private HisAntibioticRequestUpdate antibioticRequestUpdate;

        internal HisAntibioticRequestApprove()
            : base()
        {
            this.Init();
        }

        internal HisAntibioticRequestApprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.antibioticRequestUpdate = new HisAntibioticRequestUpdate();
        }

        internal bool Run(HisAntibioticRequestApproveSDO data, ref V_HIS_ANTIBIOTIC_REQUEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                HIS_ANTIBIOTIC_REQUEST raw = null;
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                bool valid = true;
                valid = valid && checker.VerifyId(data.AntibioticRequestId, ref raw);
                valid = valid && checker.IsValidExpMest(raw.ID, ref expMest);
                if (valid)
                {
                    raw.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (data.IsApproved)
                    {
                        raw.ANTIBIOTIC_REQUEST_STT = 2;
                    }
                    else
                    {
                        raw.ANTIBIOTIC_REQUEST_STT = 3;
                    }
                    raw.OTHER_OPINION = data.OtherOpinion;
                    raw.APPROVE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    if (!this.antibioticRequestUpdate.Update(raw))
                    {
                        throw new Exception("Cap nhat HIS_ANTIBIOTIC_REQUEST that bai .Rollback du lieu");
                    }
                    result = true;
                    HisAntibioticRequestViewFilterQuery filter = new HisAntibioticRequestViewFilterQuery();
                    filter.ID = raw.ID;
                    resultData = new HisAntibioticRequestGet().GetView(filter).FirstOrDefault();
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisAntibioticRequest_PheDuyetYeuCauSuDungKhangSinh, data).AntibioticRequestCode(raw.ANTIBIOTIC_REQUEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.antibioticRequestUpdate.RollbackData();
        }
    }
}
