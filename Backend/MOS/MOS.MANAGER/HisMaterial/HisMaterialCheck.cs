using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisInfusion;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMediReact;
using MOS.MANAGER.HisMestPeriodMate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialCheck : BusinessBase
    {
        internal HisMaterialCheck()
            : base()
        {

        }

        internal HisMaterialCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MATERIAL data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MATERIAL_TYPE_ID)) throw new ArgumentNullException("data.MATERIAL_TYPE_ID");
                if (data.IMP_VAT_RATIO < 0) throw new ArgumentNullException("data.IMP_VAT_RATIO");
                if (data.AMOUNT < 0) throw new ArgumentNullException("data.AMOUNT");
                if (data.IMP_PRICE < 0) throw new ArgumentNullException("data.IMP_PRICE");
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

        internal bool IsUnLock(HIS_MATERIAL data)
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
                if (!DAOWorker.HisMaterialDAO.IsUnLock(id))
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
        internal bool VerifyId(long id, ref HIS_MATERIAL data)
        {
            bool valid = true;
            try
            {
                data = new HisMaterialGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_MATERIAL> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisMaterialFilterQuery filter = new HisMaterialFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MATERIAL> listData = new HisMaterialGet().Get(filter);
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisMaterialFilterQuery filter = new HisMaterialFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MATERIAL> listData = new HisMaterialGet().Get(filter);
                    if (listData != null && listData.Count > 0)
                    {
                        foreach (var data in listData)
                        {
                            if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                        }
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_MATERIAL> listData)
        {
            bool valid = true;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    foreach (var data in listData)
                    {
                        if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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
                List<HIS_MATERIAL_BEAN> hisMaterialBeans = new HisMaterialBeanGet().GetByMaterialId(id);
                if (IsNotNullOrEmpty(hisMaterialBeans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MATERIAL_BEAN, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MEST_PERIOD_MATE> hisMestPeriodMates = new HisMestPeriodMateGet().GetByMaterialId(id);
                if (IsNotNullOrEmpty(hisMestPeriodMates))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMestPeriodMate_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEST_PERIOD_MATE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                //review lai
                //List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = new HisExpMestMaterialGet().GetByMaterialId(id);
                //if (IsNotNullOrEmpty(hisExpMestMaterials))
                //{
                //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMestMaterial_TonTaiDuLieu);
                //    throw new Exception("Ton tai du lieu HIS_EXP_MEST_MATERIAL, khong cho phep xoa" + LogUtil.TraceData("id", id));
                //}

                //List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = new HisImpMestMaterialGet().GetByMaterialId(id);
                //if (IsNotNullOrEmpty(hisImpMestMaterials))
                //{
                //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMestMaterial_TonTaiDuLieu);
                //    throw new Exception("Ton tai du lieu HIS_IMP_MEST_MATERIAL, khong cho phep xoa" + LogUtil.TraceData("id", id));
                //}
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckChangePrice(HIS_MATERIAL newData, HIS_MATERIAL raw)
        {
            bool valid = true;
            try
            {
                if (newData != null && raw != null && 
                    (newData.IMP_PRICE != raw.IMP_PRICE 
                    || newData.IMP_VAT_RATIO != raw.IMP_VAT_RATIO
                    || newData.IS_SALE_EQUAL_IMP_PRICE != raw.IS_SALE_EQUAL_IMP_PRICE))
                {
                    HisMaterialBeanFilterQuery mbFilter = new HisMaterialBeanFilterQuery();
                    mbFilter.MATERIAL_ID = raw.ID;
                    mbFilter.HAS_EXP_MEST_MATERIAL_ID = true;
                    List<HIS_MATERIAL_BEAN> beans = new HisMaterialBeanGet().Get(mbFilter);
                    if (IsNotNullOrEmpty(beans))
                    {
                        HisExpMestMaterialViewFilterQuery eMMFilter = new HisExpMestMaterialViewFilterQuery();
                        eMMFilter.IDs = beans.Select(s => s.EXP_MEST_MATERIAL_ID.Value).Distinct().ToList();
                        List<V_HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetView(eMMFilter);
                        List<string> expMestCodes = expMestMaterials.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common_TonTaiPhieuXuatChuaThucXuatKhongChoPhepSuaGia, String.Join(";", expMestCodes));
                        valid = false;
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

        internal bool IsLock(HIS_MATERIAL data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
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

        internal bool IsLock(long id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisMaterialDAO.IsUnLock(id))
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach id)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLock(List<long> listId)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisMaterialFilterQuery filter = new HisMaterialFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MATERIAL> listData = new HisMaterialGet().Get(filter);
                    if (listData != null && listData.Count > 0)
                    {
                        foreach (var data in listData)
                        {
                            if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
                        }
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

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLock(List<HIS_MATERIAL> listData)
        {
            bool valid = true;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    foreach (var data in listData)
                    {
                        if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE == data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
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
