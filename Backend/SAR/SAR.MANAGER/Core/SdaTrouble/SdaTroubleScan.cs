using SAR.MANAGER.Core.SdaTrouble.Scan;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleScan : BeanObjectBase, IDelegacy
    {

        internal SdaTroubleScan(CommonParam param)
            : base(param)
        {
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                ISdaTroubleScan behavior = SdaTroubleScanBehaviorFactory.MakeISdaTroubleScan(param);
                result = behavior != null ? behavior.Run() : false;
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
