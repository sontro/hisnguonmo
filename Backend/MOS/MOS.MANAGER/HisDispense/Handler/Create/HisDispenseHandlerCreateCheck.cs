using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Create
{
    class HisDispenseHandlerCreateCheck : BusinessBase
    {
        internal HisDispenseHandlerCreateCheck()
            : base()
        {

        }

        internal HisDispenseHandlerCreateCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool CheckValidData(HisDispenseSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.DispenseTime <= 0) throw new ArgumentNullException("data.DispenseTime");
                if (data.Amount <= 0) throw new ArgumentNullException("data.Amount");
                if (!IsNotNullOrEmpty(data.MedicinePaties)) throw new ArgumentNullException("data.MedicinePaties");
                if (!IsNotNullOrEmpty(data.MedicineTypes) && !IsNotNullOrEmpty(data.MaterialTypes)) throw new ArgumentNullException("data.MedicineTypes && data.MaterialTypes");
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

        internal bool ValidMedicineType(List<HisDispenseMetySDO> medicineTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(medicineTypes))
                {
                    foreach (HisDispenseMetySDO metySdo in medicineTypes)
                    {
                        if (metySdo == null) throw new ArgumentNullException("matySdo");
                        if (metySdo.MedicineTypeId <= 0) throw new ArgumentNullException("matySdo.MedicineTypeId");
                        if (metySdo.Amount <= 0) throw new ArgumentNullException("matySdo.Amount");
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

        internal bool ValidMaterialType(List<HisDispenseMatySDO> materialTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(materialTypes))
                {
                    foreach (HisDispenseMatySDO matySdo in materialTypes)
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

        internal bool CheckWorkPlace(HisDispenseSDO data)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (sdo == null || !sdo.MediStockId.HasValue || sdo.MediStockId.Value != data.MediStockId)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin phong lam viec. Hoac phong lam viec khong phai la Kho bao che" + LogUtil.TraceData("WorkPlace", sdo));
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
