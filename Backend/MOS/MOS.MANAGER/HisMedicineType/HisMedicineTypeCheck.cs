using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmteMedicineType;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMediStockMety;
using MOS.MANAGER.HisMestPeriodMety;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisBidMedicineType;

namespace MOS.MANAGER.HisMedicineType
{
    class HisMedicineTypeCheck : BusinessBase
    {
        internal HisMedicineTypeCheck()
            : base()
        {

        }

        internal HisMedicineTypeCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MEDICINE_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.SERVICE_ID)) throw new ArgumentNullException("data.SERVICE_ID");
                if (!IsNotNullOrEmpty(data.MEDICINE_TYPE_NAME)) throw new ArgumentNullException("data.MEDICINE_TYPE_NAME");
                if (!IsNotNullOrEmpty(data.MEDICINE_TYPE_CODE)) throw new ArgumentNullException("data.MEDICINE_TYPE_CODE");
                if (data.IMP_UNIT_ID.HasValue && (data.IMP_UNIT_CONVERT_RATIO ?? 0) <= 0) throw new ArgumentNullException("data.IMP_UNIT_ID && data.IMP_UNIT_CONVERT_RATIO");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsValidActiveIngredient(HIS_MEDICINE_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data.HIS_MEDICINE_TYPE_ACIN != null && data.HIS_MEDICINE_TYPE_ACIN.Count > 0)
                {
                    int count = data.HIS_MEDICINE_TYPE_ACIN.Select(o => o.ACTIVE_INGREDIENT_ID).Distinct().Count();
                    if (count != data.HIS_MEDICINE_TYPE_ACIN.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Du lieu gui len ton tai 2 ACTIVE_INGREDIENT_ID trung nhau");
                        return false;
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisMedicineTypeDAO.ExistsCode(code, id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                    valid = false;
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

        internal bool IsUnLock(HIS_MEDICINE_TYPE data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisMedicineTypeDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
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

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_MEDICINE_TYPE data)
        {
            bool valid = true;
            try
            {
                data = new HisMedicineTypeGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_MEDICINE_TYPE> hisMedicineTypes = new HisMedicineTypeGet().GetByParentId(id);
                if (IsNotNullOrEmpty(hisMedicineTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineType_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByMedicineTypeId(id);
                if (IsNotNullOrEmpty(hisMedicines))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicine_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEDI_STOCK_METY> hisMediStockMaties = new HisMediStockMetyGet().GetByMedicineTypeId(id);
                if (IsNotNullOrEmpty(hisMediStockMaties))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStockMety_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEST_PERIOD_METY> hisMestPeriodMaties = new HisMestPeriodMetyGet().GetByMedicineTypeId(id);
                if (IsNotNullOrEmpty(hisMestPeriodMaties))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicine_TonTaiDuLieu);
                    return false;
                }

                List<HIS_EMTE_MEDICINE_TYPE> hisEmteMedicineTypes = new HisEmteMedicineTypeGet().GetByMedicineTypeId(id);
                if (IsNotNullOrEmpty(hisEmteMedicineTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicine_TonTaiDuLieu);
                    return false;
                }

                List<HIS_BID_MEDICINE_TYPE> hisBidMedicineTypes = new HisBidMedicineTypeGet().GetByMedicineTypeId(id);
                if (IsNotNullOrEmpty(hisBidMedicineTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBidMedicineType_TonTaiDuLieu);
                    return false;
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
