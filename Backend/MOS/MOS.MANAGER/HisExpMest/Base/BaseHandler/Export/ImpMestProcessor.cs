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

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestCreate hisImpMestCreate;

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, WorkPlaceSDO workPlace, long time, string loginname, string usernname, ref HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                HIS_IMP_MEST imp = new HIS_IMP_MEST();
                imp.APPROVAL_LOGINNAME = loginname;
                imp.APPROVAL_TIME = time;
                imp.APPROVAL_USERNAME = usernname;
                imp.CHMS_EXP_MEST_ID = expMest.ID;
                imp.CHMS_TYPE_ID = expMest.CHMS_TYPE_ID;
                imp.DESCRIPTION = expMest.DESCRIPTION;
                imp.IMP_LOGINNAME = loginname;
                imp.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
                imp.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK;
                imp.IMP_TIME = time;
                imp.IMP_USERNAME = usernname;
                imp.MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                imp.CHMS_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                imp.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                imp.REQ_ROOM_ID = workPlace.RoomId;
                imp.REQ_LOGINNAME = loginname;
                imp.REQ_USERNAME = usernname;
                imp.TDL_CHMS_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                if (!this.hisImpMestCreate.Create(imp))
                {
                    throw new Exception("Khong tao duoc HIS_IMP_MEST. Ket thuc nghiep vu");
                }
                impMest = imp;
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
            try
            {
                this.hisImpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
