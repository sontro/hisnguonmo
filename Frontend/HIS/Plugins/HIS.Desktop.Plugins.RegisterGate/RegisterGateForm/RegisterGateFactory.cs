using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterGate.RegisterGateForm
{
    class RegisterGateFactory
    {
        internal static IRegisterGate MakeControl(CommonParam commonParam, object[] data)
        {
            IRegisterGate result = null;
            try
            {
                result = new RegisterGateBehavior(commonParam, data);
                if (result == null)
                {
                    throw new NullReferenceException();
                }

            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Không khởi tạo được đối tượng" + data.GetType().ToString() +
                Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;

            } return result;
        }
    }
}
