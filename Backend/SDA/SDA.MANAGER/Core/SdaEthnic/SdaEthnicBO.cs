using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEthnic
{
    partial class SdaEthnicBO : BusinessObjectBase
    {
        internal SdaEthnicBO()
            : base()
        {

        }

        internal T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new SdaEthnicGet(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool Create(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEthnicCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Update(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEthnicUpdate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ChangeLock(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEthnicChangeLock(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Delete(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEthnicDelete(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal List<object> GetDynamic(object filter)
        {
            List<object> result = new List<object>();
            try
            {
                IDelegacyT delegacy = new SdaEthnicGetDynamic(param, filter);
                result = delegacy.Execute<List<object>>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<object>();
            }
            return result;
        }
    }
}
