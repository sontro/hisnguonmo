using Inventec.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.Base
{
    internal class DAOWorker
    {
        internal static MOS.DAO.Sql.SqlDAO SqlDAO { get { return (MOS.DAO.Sql.SqlDAO)Worker.Get<MOS.DAO.Sql.SqlDAO>(); } }
    }
}
