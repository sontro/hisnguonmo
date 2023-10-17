﻿using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Unapprove
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

        internal bool Run(long aggrExpMestId, ref List<string> sqls)
        {
            try
            {
                string sql = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET APPROVAL_TIME = NULL, APPROVAL_LOGINNAME = NULL, APPROVAL_USERNAME = NULL WHERE TDL_AGGR_EXP_MEST_ID = {0} ", aggrExpMestId);
                sqls.Add(sql);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
