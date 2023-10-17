using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupBO : BusinessObjectBase
    {
        internal bool CreateWithUpdatePath(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaGroupCreateWithUpdatePath(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateWithUpdatePath(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaGroupUpdateWithUpdatePath(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateAllPath(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaGroupUpdateAllPath(param, data);
                result = delegacy.Execute();
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
