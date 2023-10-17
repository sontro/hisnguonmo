using SAR.MANAGER.Core.SarPrintTypeCfg.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeCfgChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrintTypeCfg.Contains(entity.GetType()))
                {
                    ISarPrintTypeCfgChangeLock behavior = SarPrintTypeCfgChangeLockBehaviorFactory.MakeISarPrintTypeCfgChangeLock(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
