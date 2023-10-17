using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Approve
{
    class MedicineProcessor : BusinessBase
    {
        internal MedicineProcessor()
            : base()
        {
        }

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(HIS_EXP_MEST aggrExpMest, string loginname, string username, long? approvalTime, ref List<string> sqls)
        {
            try
            {
                string sql = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET APPROVAL_TIME = {0}, APPROVAL_LOGINNAME = '{1}', APPROVAL_USERNAME ='{2}' WHERE TDL_AGGR_EXP_MEST_ID = {3} ", approvalTime, loginname, username, aggrExpMest.ID);
                sqls.Add(sql);
                return true;
            }
            catch (Exception ex)
            {
                param.HasException = true;
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
