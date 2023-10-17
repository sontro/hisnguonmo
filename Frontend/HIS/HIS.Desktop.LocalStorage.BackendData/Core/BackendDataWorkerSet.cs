using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.LocalStorage.BackendData.ADO;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Common.Logging;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public partial class BackendDataWorkerSet
    {
        public BackendDataWorkerSet() { }

        public List<T> Set<T>() where T : class
        {
            return Set<T>(false, false);
        }

        public List<T> Set<T>(bool isTranslate, bool isRamOnly) where T : class
        {
            return Set<T>(isTranslate, isRamOnly, false);
        }

        public List<T> Set<T>(bool isTranslate, bool isRamOnly, bool islock) where T : class
        {
            return BackendDataWorker.Get<T>(isTranslate, isRamOnly, islock, true);
        }
    }
}
