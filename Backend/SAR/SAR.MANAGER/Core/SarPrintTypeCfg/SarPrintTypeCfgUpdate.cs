using SAR.MANAGER.Core.SarPrintTypeCfg.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeCfgUpdate(CommonParam param, object data)
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
                    ISarPrintTypeCfgUpdate behavior = SarPrintTypeCfgUpdateBehaviorFactory.MakeISarPrintTypeCfgUpdate(param, entity);
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
