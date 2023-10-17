using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Transaction.Transaction
{
    interface ITransaction
    {
        object Run();
    }
}
