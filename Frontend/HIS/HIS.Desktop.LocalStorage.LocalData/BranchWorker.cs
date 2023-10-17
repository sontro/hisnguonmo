using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.LocalData
{
    public class BranchWorker
    {
        public static void ChangeBranch(long branchId)
        {
            try
            {
                HIS.Desktop.LocalStorage.Branch.BranchWorker.SetBranchId(branchId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static long GetCurrentBranchId()
        {
            return HIS.Desktop.LocalStorage.Branch.BranchWorker.GetBranchId();
        }
    }
}
