using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisEmteMaterialType;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMestPeriodMaty;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    class HisMaterialTypeCheck : BusinessBase
    {
        internal HisMaterialTypeCheck()
            : base()
        {

        }

        internal HisMaterialTypeCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MATERIAL_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.SERVICE_ID)) throw new ArgumentNullException("data.SERVICE_ID");
                if (!IsNotNullOrEmpty(data.MATERIAL_TYPE_NAME)) throw new ArgumentNullException("data.MATERIAL_TYPE_NAME");
                if (!IsNotNullOrEmpty(data.MATERIAL_TYPE_CODE)) throw new ArgumentNullException("data.MATERIAL_TYPE_CODE");
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

        internal bool VerifyRequireField(HisMaterialTypeUpdateMapSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.MaterialTypeId)) throw new ArgumentNullException("data.MaterialTypeId");
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisMaterialTypeDAO.ExistsCode(code, id))
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

        internal bool IsUnLock(HIS_MATERIAL_TYPE data)
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
                if (!DAOWorker.HisMaterialTypeDAO.IsUnLock(id))
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
        internal bool VerifyId(long id, ref HIS_MATERIAL_TYPE data)
        {
            bool valid = true;
            try
            {
                data = new HisMaterialTypeGet().GetById(id);
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

        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_MATERIAL_TYPE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisMaterialTypeFilterQuery filter = new HisMaterialTypeFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MATERIAL_TYPE> listData = new HisMaterialTypeGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_MATERIAL_TYPE> hisMaterialTypes = new HisMaterialTypeGet().GetByParentId(id);
                if (IsNotNullOrEmpty(hisMaterialTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialType_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetByMaterialTypeId(id);
                if (IsNotNullOrEmpty(hisMaterials))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterial_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEDI_STOCK_MATY> hisMediStockMaties = new HisMediStockMatyGet().GetByMaterialTypeId(id);
                if (IsNotNullOrEmpty(hisMediStockMaties))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStockMaty_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEST_PERIOD_MATY> hisMestPeriodMaties = new HisMestPeriodMatyGet().GetByMaterialTypeId(id);
                if (IsNotNullOrEmpty(hisMestPeriodMaties))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterial_TonTaiDuLieu);
                    return false;
                }

                List<HIS_EMTE_MATERIAL_TYPE> hisEmteMaterialTypes = new HisEmteMaterialTypeGet().GetByMaterialTypeId(id);
                if (IsNotNullOrEmpty(hisEmteMaterialTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterial_TonTaiDuLieu);
                    return false;
                }

                List<HIS_BID_MATERIAL_TYPE> hisBidMaterialTypes = new HisBidMaterialTypeGet().GetByMaterialTypeId(id);
                if (IsNotNullOrEmpty(hisBidMaterialTypes))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBidMaterialType_TonTaiDuLieu);
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
