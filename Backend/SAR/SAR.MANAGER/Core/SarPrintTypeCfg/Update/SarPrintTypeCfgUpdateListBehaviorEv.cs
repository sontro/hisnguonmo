using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Update
{
    class SarPrintTypeCfgUpdateListBehaviorEv : BeanObjectBase, ISarPrintTypeCfgUpdate
    {
        List<SAR_PRINT_TYPE_CFG> current;
        List<SAR_PRINT_TYPE_CFG> entity;

        internal SarPrintTypeCfgUpdateListBehaviorEv(CommonParam param, List<SAR_PRINT_TYPE_CFG> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.UpdateList(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                RollBack();
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarPrintTypeCfgCheckVerifyValidData.Verify(param, entity);
                result = result && SarPrintTypeCfgCheckVerifyIsUnlock.Verify(param, entity, ref current);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void RollBack()
        {
            try
            {
                if (current != null && current.Count > 0)
                    DAOWorker.SarPrintTypeCfgDAO.UpdateList(current);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
