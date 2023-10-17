using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;
using QCS.Desktop.Plugins.QcsQuery.SqlSave;

namespace QCS.Desktop.Plugins.QcsQuery
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.SqlSave",
       "Sửa ...",
       "Common",
       31,
       "newitem_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class SqlSaveProcessor : BusinessBase, IDesktopRoot
    {
        public SqlSaveProcessor()
            : base()
        {

        }
        public SqlSaveProcessor(CommonParam param)
            : base(param)
        {

        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ISqlSave behavior = SqlSaveFactory.MakeICrateType(param, (args != null && args.Count() > 0 ? args[0] : null));
                result = behavior != null ? (object)(behavior.Run()) : null;
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
