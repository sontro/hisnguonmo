using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseReduction.Create
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(CabinetBaseReductionSDO data, WorkPlaceSDO workPlace, ref HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST exp = new HIS_EXP_MEST();
                exp.DESCRIPTION = data.Description;
                exp.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK;
                exp.IMP_MEDI_STOCK_ID = data.ImpMestMediStockId;
                exp.MEDI_STOCK_ID = data.CabinetMediStockId;
                exp.REQ_ROOM_ID = workPlace.RoomId;
                exp.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                exp.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                exp.REQ_USER_TITLE = HisEmployeeUtil.GetTitle(exp.REQ_LOGINNAME);
                exp.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                exp.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                exp.CHMS_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION;

                if (!this.hisExpMestCreate.Create(exp))
                {
                    throw new Exception("hisExpMestCreate. Ket thuc nghiep vu");
                }
                result = true;
                expMest = exp;
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
            this.hisExpMestCreate.RollbackData();
        }
    }
}
