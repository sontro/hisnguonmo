using SAR.MANAGER.Core.SarPrintTypeCfg.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeCfgDelete(CommonParam param, object data)
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
                    ISarPrintTypeCfgDelete behavior = SarPrintTypeCfgDeleteBehaviorFactory.MakeISarPrintTypeCfgDelete(param, entity);
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
