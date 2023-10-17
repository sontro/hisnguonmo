using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseAddition
{
    class HisExpMestBaseAdditionCheck : BusinessBase
    {
        internal HisExpMestBaseAdditionCheck()
            : base()
        {

        }

        internal HisExpMestBaseAdditionCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(CabinetBaseAdditionSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.CabinetMediStockId)) throw new ArgumentNullException("data.CabinetMediStockId");
                if (!IsGreaterThanZero(data.ExpMestMediStockId)) throw new ArgumentNullException("data.ExpMestMediStockId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
                if (!IsNotNullOrEmpty(data.MaterialTypes) && !IsNotNullOrEmpty(data.MedicineTypes)) throw new ArgumentNullException("data.MaterialTypes && data.MedicineTypes");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
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

        internal bool IsWorkingInImpOrExpStock(WorkPlaceSDO workPlace, long impStockId, long expStockId)
        {
            bool valid = true;
            try
            {
                if (!workPlace.MediStockId.HasValue || (workPlace.MediStockId.Value != impStockId && workPlace.MediStockId.Value != expStockId))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKhoNhapHoacXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool ValidData(CabinetBaseAdditionSDO data)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    if (data.MedicineTypes.Any(a => a.Amount <= 0 || a.MedicineTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MedicineType co Amount hoac MedicineTypeId khong hop le");
                    }

                    if (data.MedicineTypes.GroupBy(g => g.MedicineTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai hai dong MedicineType co cung MedicineTypeId");
                    }
                }
                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    if (data.MaterialTypes.Any(a => a.Amount <= 0 || a.MaterialTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MaterialType co Amount hoac MaterialTypeId khong hop le");
                    }
                    if (data.MaterialTypes.GroupBy(g => g.MaterialTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai hai dong MaterialType co cung MaterialTypeId");
                    }
                }

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
