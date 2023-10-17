using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class HisMediStockPeriodApproveCheck : BusinessBase
    {
        internal HisMediStockPeriodApproveCheck()
            : base()
        {

        }

        internal HisMediStockPeriodApproveCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HIS_MEDI_STOCK_PERIOD raw, ref List<HIS_MEST_PERIOD_MATE> materials, ref List<HIS_MEST_PERIOD_MEDI> medicines)
        {
            bool valid = true;
            try
            {
                List<HIS_MEDI_STOCK_PERIOD> afters = new HisMediStockPeriodGet().GetByPreviousId(raw.ID);
                if (IsNotNullOrEmpty(afters))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_TonTaiDuLieuKiSau);
                    return false;
                }

                materials = new HisMestPeriodMateGet().GetByMediStockPeriodId(raw.ID);
                medicines = new HisMestPeriodMediGet().GetByMediStockPeriodId(raw.ID);

                if (!IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(medicines))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_KyKhoKhongCoThongTinThuocVatTu);
                    return false;
                }

                if ((IsNotNullOrEmpty(materials) && !materials.Any(a => ((a.INVENTORY_AMOUNT ?? 0) - (a.VIR_END_AMOUNT ?? 0)) != 0))
                    && (IsNotNullOrEmpty(medicines) && !medicines.Any(a => ((a.INVENTORY_AMOUNT ?? 0) - (a.VIR_END_AMOUNT ?? 0)) != 0)))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_KhongPhatHienLechDuLieuThuocVatTuGiuaKiemKeVaSoLuongCuoiKi);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = true;
            }
            return valid;
        }

        internal bool IsValidForExpKeyConfig()
        {
            bool valid = true;
            try
            {
                if (!IsGreaterThanZero(HisExpMestCFG.EXP_MEST_REASON_INVE_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_ChuaCauHinhLoaiXuatKiemKe);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = true;
            }
            return valid;
        }
    }
}
