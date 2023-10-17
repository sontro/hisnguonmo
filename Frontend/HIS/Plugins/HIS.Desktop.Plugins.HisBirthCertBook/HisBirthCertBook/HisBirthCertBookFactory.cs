using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisBirthCertBook.HisBirthCertBook
{
    class HisBirthCertBookFactory
    {
        internal static IHisBirthCertBook MakeIControl(CommonParam param, object[] data)
        {
            IHisBirthCertBook result = null;

            try{

                result = new HisBirthCertBookBehavior(param,data);
                if (result == null) throw new NullReferenceException(); 

            }catch(NullReferenceException ex){

                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + 
                    data.GetType().ToString() + 
                    Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;

            }catch(Exception ex){

                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;

            }

            return result;
        }

        
    }
}
