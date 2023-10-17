using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisInfusion;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediReact;
using MOS.MANAGER.HisMestPeriodMedi;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineCheck : BusinessBase
    {
        internal HisMedicineCheck()
            : base()
        {

        }

        internal HisMedicineCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MEDICINE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MEDICINE_TYPE_ID)) throw new ArgumentNullException("data.MEDICINE_TYPE_ID");
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

        internal bool IsUnLock(HIS_MEDICINE data)
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
                if (!DAOWorker.HisMedicineDAO.IsUnLock(id))
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
        internal bool VerifyId(long id, ref HIS_MEDICINE data)
        {
            bool valid = true;
            try
            {
                data = new HisMedicineGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_MEDICINE> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisMedicineFilterQuery filter = new HisMedicineFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MEDICINE> listData = new HisMedicineGet().Get(filter);
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
                    HisMedicineFilterQuery filter = new HisMedicineFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MEDICINE> listData = new HisMedicineGet().Get(filter);
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
        internal bool IsUnLock(List<HIS_MEDICINE> listData)
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

        internal bool VerifyValidExpiredDate(HIS_MEDICINE data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && (!data.EXPIRED_DATE.HasValue || (data.EXPIRED_DATE.HasValue && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.EXPIRED_DATE.Value).HasValue));
                if (!valid)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Du lieu EXPIRED_DATE ko hop le. EXPIRED_DATE neu co nhap thi bat buoc phai la ngay hop le." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
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
                List<HIS_MEDICINE_BEAN> hisMedicineBeans = new HisMedicineBeanGet().GetByMedicineId(id);
                if (IsNotNullOrEmpty(hisMedicineBeans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEDICINE_BEAN, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MEST_PERIOD_MEDI> hisMestPeriodMedis = new HisMestPeriodMediGet().GetByMedicineId(id);
                if (IsNotNullOrEmpty(hisMestPeriodMedis))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMestPeriodMedi_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEST_PERIOD_MEDI, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                //review lai
                //List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = new HisExpMestMedicineGet().GetByMedicineId(id);
                //if (IsNotNullOrEmpty(hisExpMestMedicines))
                //{
                //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMestMedicine_TonTaiDuLieu);
                //    throw new Exception("Ton tai du lieu HIS_EXP_MEST_MEDICINE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                //}

                //List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines = new HisImpMestMedicineGet().GetByMedicineId(id);
                //if (IsNotNullOrEmpty(hisImpMestMedicines))
                //{
                //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMestMedicine_TonTaiDuLieu);
                //    throw new Exception("Ton tai du lieu HIS_IMP_MEST_MEDICINE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                //}

                List<HIS_MEDI_REACT> hisMediReacts = new HisMediReactGet().GetByMedicineId(id);
                if (IsNotNullOrEmpty(hisMediReacts))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediReact_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEDI_REACT, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool CheckChangePrice(HIS_MEDICINE newData, HIS_MEDICINE raw)
        {
            bool valid = true;
            try
            {
                if (newData != null && raw != null &&
                    (newData.IMP_PRICE != raw.IMP_PRICE
                    || newData.IMP_VAT_RATIO != raw.IMP_VAT_RATIO
                    || newData.IS_SALE_EQUAL_IMP_PRICE != raw.IS_SALE_EQUAL_IMP_PRICE))
                {
                    HisMedicineBeanFilterQuery mbFilter = new HisMedicineBeanFilterQuery();
                    mbFilter.MEDICINE_ID = raw.ID;
                    mbFilter.HAS_EXP_MEST_MEDICINE_ID = true;
                    List<HIS_MEDICINE_BEAN> beans = new HisMedicineBeanGet().Get(mbFilter);
                    if (IsNotNullOrEmpty(beans))
                    {
                        HisExpMestMedicineViewFilterQuery eMMFilter = new HisExpMestMedicineViewFilterQuery();
                        eMMFilter.IDs = beans.Select(s => s.EXP_MEST_MEDICINE_ID.Value).Distinct().ToList();
                        List<V_HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetView(eMMFilter);
                        List<string> expMestCodes = expMestMedicines.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
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

        internal bool IsLock(HIS_MEDICINE data)
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
                if (DAOWorker.HisMedicineDAO.IsUnLock(id))
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
        /// Kiem tra du lieu co o trang thai lock (su dung danh sach id)
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
                    HisMedicineFilterQuery filter = new HisMedicineFilterQuery();
                    filter.IDs = listId;
                    List<HIS_MEDICINE> listData = new HisMedicineGet().Get(filter);
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
        /// Kiem tra du lieu co o trang thai lock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsLock(List<HIS_MEDICINE> listData)
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
