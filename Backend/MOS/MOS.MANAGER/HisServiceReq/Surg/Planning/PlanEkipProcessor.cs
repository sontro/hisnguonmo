using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEkipPlan;
using MOS.MANAGER.HisEkipPlanUser;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    class PlanEkipProcessor : BusinessBase
    {
        private HisEkipPlanCreate hisEkipPlanCreate;
        private HisEkipPlanUserCreate hisEkipPlanUserCreate;
        private HisEkipPlanUserTruncate hisEkipPlanUserTruncate;

        internal PlanEkipProcessor()
            : base()
        {
            this.Init();
        }

        internal PlanEkipProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisEkipPlanCreate = new HisEkipPlanCreate(param);
            this.hisEkipPlanUserCreate = new HisEkipPlanUserCreate(param);
            this.hisEkipPlanUserTruncate = new HisEkipPlanUserTruncate(param);
        }

        internal bool Run(long? ekipPlanId, List<EkipSDO> ekips, ref long resultData)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(ekips))
                {
                    return true;
                }

                List<HIS_EKIP_PLAN_USER> toInserts = new List<HIS_EKIP_PLAN_USER>();
                List<HIS_EKIP_PLAN_USER> toDeletes = new List<HIS_EKIP_PLAN_USER>();
                List<HIS_EKIP_PLAN_USER> exists = null;
                if (ekipPlanId.HasValue)
                {
                    exists = new HisEkipPlanUserGet().GetByEkipPlanId(ekipPlanId.Value);
                }
                else
                {
                    HIS_EKIP_PLAN ekipPlan = new HIS_EKIP_PLAN();
                    if (!this.hisEkipPlanCreate.Create(ekipPlan))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    ekipPlanId = ekipPlan.ID;
                }

                toInserts = ekips.Where(o => exists == null || !exists.Exists(t => t.EXECUTE_ROLE_ID == o.ExecuteRoleId && t.LOGINNAME == o.LoginName))
                    .Select(o => new HIS_EKIP_PLAN_USER
                    {
                        LOGINNAME = o.LoginName,
                        USERNAME = o.UserName,
                        EXECUTE_ROLE_ID = o.ExecuteRoleId,
                        EKIP_PLAN_ID = ekipPlanId.Value
                    }).ToList();

                toDeletes = exists != null ? exists.Where(t => ekips == null || !ekips.Exists(o => t.EXECUTE_ROLE_ID == o.ExecuteRoleId && t.LOGINNAME == o.LoginName)).ToList() : null;

                if (IsNotNullOrEmpty(toInserts) && !this.hisEkipPlanUserCreate.CreateList(toInserts))
                {
                    throw new Exception("Rollback du lieu");
                }
                if (IsNotNullOrEmpty(toDeletes) && !this.hisEkipPlanUserTruncate.TruncateList(toDeletes))
                {
                    throw new Exception("Rollback du lieu");
                }
                resultData = ekipPlanId.Value;
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisEkipPlanCreate.RollbackData();
            this.hisEkipPlanUserCreate.RollbackData();
        }
    }
}
