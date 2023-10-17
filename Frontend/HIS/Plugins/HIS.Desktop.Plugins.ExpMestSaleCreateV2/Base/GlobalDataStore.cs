using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2.Base
{
    public class GlobalDataStore
    {
        public const int ActionAdd = 1;
        public const int ActionEdit = 2;

        public enum ModuleAction
        { 
            ADD,
            EDIT
        }
    }
}
