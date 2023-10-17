using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using QCS.Desktop.Plugins.QcsQuery.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;

namespace QCS.Desktop.Plugins.QcsQuery
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "QCS.Desktop.Plugins.QcsQuery",
       "Danh sách ...",
       "Common",
       31,
       "newitem_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]
    public class SqlProcessor : BusinessBase, IDesktopRoot
    {
        public SqlProcessor()
            : base()
        {

        }
        public SqlProcessor(CommonParam param)
            : base(param)
        {

        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ISql behavior = SqlFactory.MakeICrateType(param, args);
                result = behavior != null ? (UserControl)(behavior.Run()) : null;
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
