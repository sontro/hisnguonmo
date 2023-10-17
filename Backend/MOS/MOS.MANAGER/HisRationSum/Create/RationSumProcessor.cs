using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Create
{
    class RationSumProcessor : BusinessBase
    {
        private HisRationSumCreate hisRationSumCreate;

        internal RationSumProcessor(CommonParam param)
            : base(param)
        {
            this.hisRationSumCreate = new HisRationSumCreate(param);
        }

        internal bool Run(HisRationSumSDO data, List<HIS_SERVICE_REQ> serviceReqs, WorkPlaceSDO wp, ref List<HIS_RATION_SUM> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_RATION_SUM> creates = new List<HIS_RATION_SUM>();
                long time = Inventec.Common.DateTime.Get.Now().Value;

                bool isAutoSplitByIntructionDate = Config.HisRationSumCFG.AUTO_SPLIT_BY_INTRUCTION_DATE == 1;
                if (isAutoSplitByIntructionDate)
                {
                    var Groups = serviceReqs.GroupBy(g => new { g.EXECUTE_ROOM_ID, g.INTRUCTION_DATE }).ToList();
                    foreach (var g in Groups)
                    {
                        HIS_RATION_SUM rationSum = new HIS_RATION_SUM();
                        V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == g.Key.EXECUTE_ROOM_ID);
                        rationSum.ROOM_ID = room.ID;
                        rationSum.DEPARTMENT_ID = wp.DepartmentId;
                        rationSum.RATION_SUM_STT_ID = IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REQ;
                        rationSum.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        rationSum.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        rationSum.REQ_TIME = time;
                        rationSum.REQ_DATE = rationSum.REQ_TIME - (rationSum.REQ_TIME % 1000000);
                        rationSum.MAX_INTRUCTION_DATE = g.First().INTRUCTION_DATE;
                        creates.Add(rationSum);
                    }
                }
                else
                {
                    var Groups = serviceReqs.GroupBy(g => g.EXECUTE_ROOM_ID).ToList();
                    foreach (var g in Groups)
                    {
                        HIS_RATION_SUM rationSum = new HIS_RATION_SUM();
                        V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == g.Key);
                        rationSum.ROOM_ID = room.ID;
                        rationSum.DEPARTMENT_ID = wp.DepartmentId;
                        rationSum.RATION_SUM_STT_ID = IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REQ;
                        rationSum.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        rationSum.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        rationSum.REQ_TIME = time;
                        rationSum.REQ_DATE = rationSum.REQ_TIME - (rationSum.REQ_TIME % 1000000);
                        rationSum.MAX_INTRUCTION_DATE = g.Max(o=>o.INTRUCTION_DATE);
                        creates.Add(rationSum);
                    }
                }

                if (!this.hisRationSumCreate.CreateList(creates))
                {
                    throw new Exception("hisRationSumCreate. Ket thuc nghiep vu");
                }
                resultData = creates;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisRationSumCreate.RollbackData();
        }
    }
}
