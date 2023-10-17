using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO
{
    class HisAllergyCardSDOCheck : BusinessBase
    {
        internal HisAllergyCardSDOCheck()
            : base()
        {

        }

        internal HisAllergyCardSDOCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisAllergyCardSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.AllergyCard)) throw new ArgumentNullException("data.AllergyCard");
                if (!IsNotNullOrEmpty(data.Allergenics)) throw new ArgumentNullException("data.Allergenics");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
