using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Create
{
    class HisDispensePackingCreateCheck : BusinessBase
    {
        internal HisDispensePackingCreateCheck()
            : base()
        {

        }

        internal HisDispensePackingCreateCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool CheckValidData(HisPackingCreateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.DispenseTime <= 0) throw new ArgumentNullException("data.DispenseTime");
                if (data.Amount <= 0) throw new ArgumentNullException("data.Amount");
                if (data.MaterialTypeId <= 0) throw new ArgumentNullException("data.MaterialTypeId");
                if (!IsNotNullOrEmpty(data.MaterialPaties)) throw new ArgumentNullException("data.MaterialPaties");
                if (!IsNotNullOrEmpty(data.MaterialTypes)) throw new ArgumentNullException("data.MaterialTypes");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }

        internal bool ValidMaterialType(List<HisPackingMatySDO> materialTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(materialTypes))
                {
                    foreach (HisPackingMatySDO matySdo in materialTypes)
                    {
                        if (matySdo == null) throw new ArgumentNullException("matySdo");
                        if (matySdo.MaterialTypeId <= 0) throw new ArgumentNullException("matySdo.MaterialTypeId");
                        if (matySdo.Amount <= 0) throw new ArgumentNullException("matySdo.Amount");
                    }
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
