using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Delete
{
    class SarPrintTypeCfgDeleteListBehaviorEv : BeanObjectBase, ISarPrintTypeCfgDelete
    {
        List<SAR_PRINT_TYPE_CFG> entity;

        internal SarPrintTypeCfgDeleteListBehaviorEv(CommonParam param, List<SAR_PRINT_TYPE_CFG> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.TruncateList(entity);
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
                foreach (var item in entity)
                {
                    result = result && SarPrintTypeCfgCheckVerifyIsUnlock.Verify(param, item.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
