using HIS.Desktop.Plugins.HisUserAccountBook.HisUserAccountBook;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisUserAccountBook.HisUserAccountBook
{
	class HisUserAccountBookFactory
	{
		internal static IHisUserAccountBook makeIControl(CommonParam param, object[] data)
		{
			IHisUserAccountBook result = null;
			try
			{
				result = new HisUserAccountBookBehavior(param, data);

				if (result == null) throw new NullReferenceException();
			}
			catch (NullReferenceException ex)
			{
				Inventec.Common.Logging.LogSystem.Error
					("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString()
					+ Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);

				result = null;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				result = null;
			}
			return result;
		}
	}
}
