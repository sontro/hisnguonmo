using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO
{
    class HisAdrSDOCheck : BusinessBase
    {
        internal HisAdrSDOCheck()
            : base()
        {

        }

        internal HisAdrSDOCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisAdrSDO data)
        {
            bool valid = true;
            try
            {
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Adr);
                valid = valid && IsNotNullOrEmpty(data.AdrMedicineTypes);
                if (!valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

    }
}
