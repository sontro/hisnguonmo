using SAR.MANAGER.Core.SarPrintTypeCfg.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeCfgCreate(CommonParam param, object data)
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
                    ISarPrintTypeCfgCreate behavior = SarPrintTypeCfgCreateBehaviorFactory.MakeISarPrintTypeCfgCreate(param, entity);
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
