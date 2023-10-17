using HIS.Desktop.Plugins.HisCaroAccountBook.HisCaroAccountBook;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCaroAccountBook.HisCaroAccountBook
{
	[ExtensionOf(typeof(DesktopRootExtensionPoint),
			 "HIS.Desktop.Plugins.HisCaroAccountBook",
			 "Danh mục",
			 "Bussiness",
			 4,
			 "showproduct_32x32.png",
			 "A",
			 Module.MODULE_TYPE_ID__FORM,
			 true,
			 true)
	]
	class HisCaroAccountBookProcessor : ModuleBase, IDesktopRoot
	{
		CommonParam param;
		public HisCaroAccountBookProcessor()
		{
			param = new CommonParam();
		}
		public HisCaroAccountBookProcessor(CommonParam paramBusiness)
		{
			param = (paramBusiness != null ? paramBusiness : new CommonParam());
		}

		public object Run(object[] args)
		{
			object result = null;
			try
			{
				IHisCaroAccountBook behavior = HisCaroAccountBookFactory.makeIControl(param, args);
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
