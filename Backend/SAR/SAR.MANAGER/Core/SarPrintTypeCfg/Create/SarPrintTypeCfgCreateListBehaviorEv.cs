﻿using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Create
{
    class SarPrintTypeCfgCreateListBehaviorEv : BeanObjectBase, ISarPrintTypeCfgCreate
    {
        List<SAR_PRINT_TYPE_CFG> entity;

        internal SarPrintTypeCfgCreateListBehaviorEv(CommonParam param, List<SAR_PRINT_TYPE_CFG> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.CreateList(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarPrintTypeCfgCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}