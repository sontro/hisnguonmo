using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCS.Desktop.Plugins.QcsQuery.SqlSave
{
    class SqlSaveBehavior : BusinessBase, ISqlSave
    {
        object entity;
        internal SqlSaveBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object ISqlSave.Run()
        {
            try
            {
                return new frmSqlSave((long)this.entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
