using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation.Create
{
    partial class HisQcNormationCreateSdoCheck : BusinessBase
    {
		
        internal HisQcNormationCreateSdoCheck()
            : base()
        {

        }

        internal HisQcNormationCreateSdoCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsValidData(HisQcNormationSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MachineId <= 0) throw new ArgumentException("data.MachineId");
                if (data.QcTypeId <= 0) throw new ArgumentException("data.QcTypeId");
                if (data.MaterialNormations != null && data.MaterialNormations.Select(o => o.MaterialTypeId).Distinct().Count() != data.MaterialNormations.Count)
                {
                    throw new ArgumentException("data.MaterialNormations trung MaterialTypeId");
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (ArgumentException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
