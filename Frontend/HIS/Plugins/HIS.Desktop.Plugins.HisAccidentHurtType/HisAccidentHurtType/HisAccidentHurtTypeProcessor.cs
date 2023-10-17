using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccidentHurtType.HisAccidentHurtType
{
	[ExtensionOf(typeof(DesktopRootExtensionPoint),
			 "HIS.Desktop.Plugins.HisAccidentHurtType",
			 "Danh mục",
			 "Bussiness",
			 4,
             "tai-nan.png",
			 "A",
			 Module.MODULE_TYPE_ID__FORM,
			 true,
			 true)
	]
	class HisAccidentHurtTypeProcessor : ModuleBase, IDesktopRoot
	{
		CommonParam param;
		public HisAccidentHurtTypeProcessor()
		{
			param = new CommonParam();
		}
		public HisAccidentHurtTypeProcessor(CommonParam paramBusiness)
		{
			param = (paramBusiness != null ? paramBusiness : new CommonParam());
		}

		public object Run(object[] args)
		{
			object result = null;
			try
			{
				IHisAccidentHurtType behavior = HisAccidentHurtTypeFactory.makeIControl(param, args);
				result = behavior != null ? (object)(behavior.Run()) : null;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		/// <summary>
		/// Ham tra ve trang thai cua module la enable hay disable
		/// Ghi de gia tri khac theo nghiep vu tung module
		/// </summary>
		/// <returns>true/false</returns>
		public override bool IsEnable()
		{
			bool result = false;
			try
			{
				result = true;
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
