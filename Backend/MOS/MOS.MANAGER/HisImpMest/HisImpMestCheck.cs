using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMestStt;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System.Text;

namespace MOS.MANAGER.HisImpMest
{
    class HisImpMestCheck : BusinessBase
    {
        #region Danh sach trang thai cho phep cap nhat
        private static List<long> allowedUpdateStatus = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT
        };
        #endregion

        #region Danh sach trang thai cho phep thuc nhap (import)
        private static List<long> allowedImportStatus = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL,
        };
        #endregion

        #region Danh sach trang thai cho phep khi insert
        private static List<long> allowedInsertStatus = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST
        };
        #endregion

        #region Danh sach trang thai cho phep khi Delete
        private static List<long> allowedDeleteStatus = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT
        };
        #endregion

        #region Dictionary de luu danh sach trang thai cho phep khi chuyen tu trang thay nay sang trang thai khac
        //Voi: key: la trang thai hien tai, value: list trang thai cho phep chuyen sang
        private static Dictionary<long, List<long>> changeStatusDic = new Dictionary<long, List<long>>{
            {IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT, new List<long>{
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT
            }},
            {IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST, new List<long>{
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST
            }},
            {IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL, new List<long>{
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
            }},
            {IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT, new List<long>{
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST
            }},
        };
        #endregion

        #region Danh sach loai nhap cho phep huy thuc nhap
        private static List<long> allowedCancelImportType = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DK,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KHAC,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KK,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCS,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HPTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HM,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__TSD
        };
        #endregion

        internal HisImpMestCheck()
            : base()
        {

        }

        internal HisImpMestCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.IMP_MEST_STT_ID)) throw new ArgumentNullException("data.IMP_MEST_STT_ID");
                if (!IsGreaterThanZero(data.IMP_MEST_TYPE_ID)) throw new ArgumentNullException("data.IMP_MEST_TYPE_ID");
                if (!IsGreaterThanZero(data.MEDI_STOCK_ID)) throw new ArgumentNullException("data.MEDI_STOCK_ID");
                if (data.REQ_ROOM_ID == null || !IsGreaterThanZero(data.REQ_ROOM_ID.Value)) throw new ArgumentNullException("data.REQ_ROOM_ID");
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
                if (DAOWorker.HisImpMestDAO.ExistsCode(code, id))
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

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                data = new HisImpMestGet().GetById(id);
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
        internal bool VerifyIds(List<long> listId, List<HIS_IMP_MEST> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisImpMestFilterQuery filter = new HisImpMestFilterQuery();
                    filter.IDs = listId;
                    List<HIS_IMP_MEST> listData = new HisImpMestGet().Get(filter);
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

        internal bool IsUnLock(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                if (!valid)
                {
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
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_IMP_MEST> listData)
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

        internal bool IsAllowChangeStatus(long statusBeforeId, long statusAfterId)
        {
            bool valid = true;
            try
            {
                HIS_IMP_MEST_STT statusBefore = HisImpMestSttCFG.DATA.Where(o => o.ID == statusBeforeId).FirstOrDefault();
                HIS_IMP_MEST_STT statusAfter = HisImpMestSttCFG.DATA.Where(o => o.ID == statusAfterId).FirstOrDefault();

                if (statusBefore == null || statusAfter == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception(string.Format("ImpMestSttId khong hop le: statusBeforeId={0}, statusAfterId={1}, ", statusBeforeId, statusAfterId));
                }

                valid = valid && (changeStatusDic[statusBeforeId] != null && changeStatusDic[statusBeforeId].Contains(statusAfterId));
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepChuyenDoiGiuaHaiTrangThaiNay, statusBefore.IMP_MEST_STT_NAME, statusAfter.IMP_MEST_STT_NAME);
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

        internal bool VerifyStatusForInsert(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.IMP_MEST_STT_ID);
                if (data.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT
                    && !data.CHMS_EXP_MEST_ID.HasValue)
                {
                    valid = valid && allowedInsertStatus.Contains(data.IMP_MEST_STT_ID);
                }
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepTaoMoiTrangThaiNay);
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

        internal bool VerifyStatusForUpdate(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.IMP_MEST_STT_ID);
                valid = valid && allowedUpdateStatus.Contains(data.IMP_MEST_STT_ID);
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepCapNhatKhiDangOTrangThaiNay);
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

        internal bool VerifyStatusForDelete(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.IMP_MEST_STT_ID);
                valid = valid && allowedDeleteStatus.Contains(data.IMP_MEST_STT_ID);
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepHuyKhiDangOTrangThaiNay);
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

        internal bool VerifyStatusForImport(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.IMP_MEST_STT_ID);
                valid = valid && allowedImportStatus.Contains(data.IMP_MEST_STT_ID);
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepNhapKhiDangOTrangThaiNay);
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

        internal bool VerifyStatusForCancelImport(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null || data.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepHuyNhapKhiDangOTrangThaiNay);
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

        internal bool VerifyStatusForCancelImport(List<HIS_IMP_MEST> listData)
        {
            bool valid = true;
            try
            {
                foreach (HIS_IMP_MEST data in listData)
                {
                    valid = valid && this.VerifyStatusForCancelImport(data);
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

        internal bool CheckMediStockPermission(HIS_IMP_MEST data, bool isAuto)
        {
            bool valid = true;
            try
            {
                //Khong check quyen voi truong hop tu dong duyet/thuc xuat
                if (!isAuto)
                {
                    List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                    WorkPlaceSDO allowWorkPlace = workPlaces != null ? workPlaces.Where(o => o.MediStockId.HasValue && o.MediStockId.Value == data.MEDI_STOCK_ID).FirstOrDefault() : null;

                    if (allowWorkPlace == null)
                    {
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MEDI_STOCK_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_BanDangKhongTruyCapVaoKhoNhapKhongChoPhepThucHien);
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

        internal bool IsWorkingAtMediStock(HIS_IMP_MEST data, WorkPlaceSDO workPlace)
        {
            bool valid = true;
            try
            {
                if (workPlace == null || !workPlace.MediStockId.HasValue || workPlace.MediStockId.Value != data.MEDI_STOCK_ID)
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MEDI_STOCK_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKho, mediStock.MEDI_STOCK_NAME);
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

        internal bool IsUnLockMediStock(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (!new HisMediStockCheck(param).IsUnLock(data.MEDI_STOCK_ID))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa);
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

        internal bool HasNotMediStockPeriod(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = !IsNotNull(data.MEDI_STOCK_PERIOD_ID);
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
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

        internal bool HasNotMediStockPeriod(List<HIS_IMP_MEST> listData)
        {
            bool valid = true;
            try
            {
                foreach (HIS_IMP_MEST data in listData)
                {
                    valid = valid && this.HasNotMediStockPeriod(data);
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

        internal bool VerifyStatusForMediStockPeriodUpdate(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.IMP_MEST_STT_ID);
                //Chi cho phep cap nhat chot ky doi voi cac phieu nhap da "thuc nhap"
                valid = valid && (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
                if (!valid)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepChotKyKhiDangOTrangThaiNay);
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

        internal bool HasNotInAggrImpMest(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data.AGGR_IMP_MEST_ID.HasValue)
                {
                    HIS_IMP_MEST hisAggrImpMest = new HisImpMestGet().GetById(data.AGGR_IMP_MEST_ID.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_DaThuocPhieuNhapTongHop, hisAggrImpMest.IMP_MEST_CODE);
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

        internal bool IsValidImpMedicines(List<HisMedicineWithPatySDO> impMedicineSDOs)
        {
            bool valid = true;
            try
            {
                valid = IsNotNullOrEmpty(impMedicineSDOs) && (impMedicineSDOs.Where(o => o.Medicine == null || o.Medicine.AMOUNT <= 0).Count() == 0);
                if (!valid)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Chua co thong tin chi tiet phieu nhap, hoac thong tin chi tiet co amount <= 0. " + LogUtil.TraceData("impMedicineSDOs: ", impMedicineSDOs));
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

        internal bool IsValidImpMaterials(List<HisMaterialWithPatySDO> impMaterialSDOs)
        {
            bool valid = true;
            try
            {
                valid = IsNotNullOrEmpty(impMaterialSDOs) && (impMaterialSDOs.Where(o => o.Material == null || o.Material.AMOUNT <= 0).Count() == 0);
                if (!valid)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Chua co thong tin chi tiet phieu nhap, hoac thong tin chi tiet co amount <= 0. " + LogUtil.TraceData("impMedicineSDOs: ", impMaterialSDOs));
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

        internal bool IsValidMedicineExpiredDates(List<HisMedicineWithPatySDO> impMedicineSDOs)
        {
            bool valid = true;
            try
            {
                List<HisMedicineWithPatySDO> list = impMedicineSDOs
                    .Where(o => o.Medicine.EXPIRED_DATE.HasValue && (!Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(o.Medicine.EXPIRED_DATE.Value).HasValue || Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(o.Medicine.EXPIRED_DATE.Value).Value < DateTime.Today)).ToList();
                if (list != null && list.Count > 0)
                {
                    valid = false;
                    string now = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
                    string invalidList = "";
                    foreach (HisMedicineWithPatySDO sdo in list)
                    {
                        HIS_MEDICINE_TYPE type = new HisMedicineTypeGet().GetById(sdo.Medicine.MEDICINE_TYPE_ID);
                        invalidList = invalidList + string.Format("{0} ({1})", type.MEDICINE_TYPE_CODE, type.MEDICINE_TYPE_NAME) + ", ";
                    }
                    invalidList = invalidList.Substring(0, invalidList.Length - 2);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisManuMedicine_TonTaiThuocCoHanSuDungKhongHopLe, now, invalidList);
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

        internal bool IsValidBloodExpiredDates(List<HIS_BLOOD> impBloods)
        {
            bool valid = true;
            try
            {
                List<HIS_BLOOD> list = impBloods
                    .Where(o => !o.EXPIRED_DATE.HasValue || (!Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(o.EXPIRED_DATE.Value).HasValue || Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(o.EXPIRED_DATE.Value).Value < DateTime.Today)).ToList();
                if (list != null && list.Count > 0)
                {
                    valid = false;
                    string now = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
                    string invalidList = "";
                    foreach (HIS_BLOOD blood in list)
                    {
                        invalidList += blood.BLOOD_CODE + ", ";
                    }
                    invalidList = invalidList.Substring(0, invalidList.Length - 2);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisManuImpMest_TonTaiMauCoHanSuDungKhongHopLe, invalidList, now);
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

        /// <summary>
        /// Lay danh sach thuoc dang bi tam dung nhap moi
        /// </summary>
        /// <returns></returns>
        internal bool IsNotExistStopImpMedicineType(List<HisMedicineWithPatySDO> impMedicineSDOs)
        {
            bool valid = true;
            try
            {
                HisMedicineTypeFilterQuery filter = new HisMedicineTypeFilterQuery();
                filter.IDs = impMedicineSDOs.Select(o => o.Medicine.MEDICINE_TYPE_ID).ToList();
                filter.IS_STOP_IMP = true;
                List<HIS_MEDICINE_TYPE> lst = new HisMedicineTypeGet().Get(filter);
                if (lst != null && lst.Count > 0)
                {
                    string nameList = "";
                    foreach (HIS_MEDICINE_TYPE dto in lst)
                    {
                        nameList += dto.MEDICINE_TYPE_NAME + ", ";
                    }
                    nameList = nameList.Substring(0, nameList.Length - 2);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisManuMedicine_TonTaiLoaiThuocDangTamDungNhap, nameList);
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

        /// <summary>
        /// Lay danh sach thuoc dang bi tam dung nhap moi
        /// </summary>
        /// <returns></returns>
        internal bool IsNotExistStopImpMaterialType(List<HisMaterialWithPatySDO> impMaterialSDOs)
        {
            bool valid = true;
            try
            {
                HisMaterialTypeFilterQuery filter = new HisMaterialTypeFilterQuery();
                filter.IDs = impMaterialSDOs.Select(o => o.Material.MATERIAL_TYPE_ID).ToList();
                filter.IS_STOP_IMP = true;
                List<HIS_MATERIAL_TYPE> lst = new HisMaterialTypeGet().Get(filter);
                if (lst != null && lst.Count > 0)
                {
                    string nameList = "";
                    foreach (HIS_MATERIAL_TYPE dto in lst)
                    {
                        nameList += dto.MATERIAL_TYPE_NAME + ", ";
                    }
                    nameList = nameList.Substring(0, nameList.Length - 2);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisManuMaterial_TonTaiLoaiVatTuDangTamDungNhap, nameList);
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

        internal bool CheckRequestRoomPermission(HIS_IMP_MEST data, ref WorkPlaceSDO roomWorking)
        {
            bool valid = true;
            try
            {
                List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                roomWorking = workPlaces != null ? workPlaces.Where(o => o.RoomId == data.REQ_ROOM_ID.Value).FirstOrDefault() : null;
                if (roomWorking == null)
                {
                    V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.REQ_ROOM_ID.Value).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_BanDangKhongTruyCapVaoPhongKhongChoPhepThucHien, room.ROOM_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidTypeChangeStatus(HIS_IMP_MEST data, long newImpMestSttId)
        {
            bool valid = true;
            try
            {
                if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__TH)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat la phieu thu hoi cu. khong cho phep thay doi trang thai");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyTypeForCancelImport(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (!allowedCancelImportType.Contains(data.IMP_MEST_TYPE_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepHuyNhapVoiLoaiPhieuNhapNay);
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

        internal bool IsAggrImpMest(HIS_IMP_MEST data, ref List<HIS_IMP_MEST> childs, ref List<HIS_IMP_MEST_MEDICINE> impMestMedicnes, ref List<HIS_IMP_MEST_MATERIAL> impMestMaterials)
        {
            bool valid = true;
            try
            {
                if (data.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Loai nhap khong phai la tong hop tra ImpMesTypeId: " + data.IMP_MEST_TYPE_ID);
                }

                childs = new HisImpMestGet().GetByAggrImpMestId(data.ID);

                if (!IsNotNullOrEmpty(childs))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc HIS_IMP_MEST con cua phieu tong hop");
                }

                List<HIS_IMP_MEST_MATERIAL> materials = new HisImpMestMaterialGet().GetByImpMestIds(childs.Select(s => s.ID).ToList());

                List<HIS_IMP_MEST_MEDICINE> medicines = new HisImpMestMedicineGet().GetByImpMestIds(childs.Select(s => s.ID).ToList());

                if (!IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(medicines))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong co HIS_IMP_MEST_MATERIAL va HIS_IMP_MEST_MEDICINE");
                }
                impMestMaterials = materials;
                impMestMedicnes = medicines;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckImpLoginnamePermission(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                var coltrolRole = new AcsAuthorize.AcsAuthorizeGet().GetControlInRoles();

                if (!HisEmployeeUtil.IsAdmin() && (String.IsNullOrEmpty(data.IMP_LOGINNAME) || String.IsNullOrEmpty(loginname) || data.IMP_LOGINNAME != loginname)
                    && (coltrolRole == null || !coltrolRole.Exists(o => o.CONTROL_CODE == HisImpMestCFG.ControlCode_CancelImport)))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ChiChoHuyNhapVoiCacPhieuDoMinhNhap);
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

        internal bool IsValidChangeType(long newTypeId, long rawTypeId)
        {
            bool valid = true;
            try
            {
                if (newTypeId != rawTypeId)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepThayDoiLoaiCuaPhieuNhap);
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

        internal bool IsValidChangeMediStock(long newStockId, long rawStockId)
        {
            bool valid = true;
            try
            {
                if (newStockId != rawStockId)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong cho phep thay kho nhap: newStockId: " + newStockId + "; oldStockId: " + rawStockId);
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

        internal bool IsValidMediStock(HIS_IMP_MEST data, ref V_HIS_MEDI_STOCK stock)
        {
            bool valid = true;
            try
            {
                //kiem tra neu loai nhap kho la nhap tu nha cung cap (NCC) thi phai kiem tra kho nhap co cho phep nhap tu NCC hay ko
                stock = HisMediStockCFG.DATA.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.ID == data.MEDI_STOCK_ID).FirstOrDefault();
                if (stock == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    LogSystem.Error("Khong ton tai kho tuong ung voi id " + data.MEDI_STOCK_ID);
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

        internal bool IsImported(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuNhapChuaThucNhap);
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

        internal bool HasNoNationalCode(HIS_IMP_MEST impMest)
        {
            return HasNoNationalCode(new List<HIS_IMP_MEST>() { impMest });
        }

        internal bool HasNoNationalCode(List<HIS_IMP_MEST> impMest)
        {
            try
            {
                List<string> hasCodes = impMest != null ? impMest
                    .Where(o => !string.IsNullOrWhiteSpace(o.NATIONAL_IMP_MEST_CODE))
                    .Select(o => o.IMP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_DaCoMaQuocGia, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNationalCode(HIS_IMP_MEST impMest)
        {
            return HasNationalCode(new List<HIS_IMP_MEST>() { impMest });
        }

        internal bool HasNationalCode(List<HIS_IMP_MEST> impMest)
        {
            try
            {
                List<string> hasCodes = impMest != null ? impMest
                    .Where(o => string.IsNullOrWhiteSpace(o.NATIONAL_IMP_MEST_CODE))
                    .Select(o => o.IMP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ChuaCoMaQuocGia, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsImported(List<HIS_IMP_MEST> impMest)
        {
            try
            {
                List<string> hasCodes = impMest != null ? impMest
                    .Where(o => o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    .Select(o => o.IMP_MEST_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ChuaThucNhap, str);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool CheckMaterialTypeReusable(List<HisMaterialWithPatySDO> impMaterialSDOs, ref List<HisMaterialWithPatySDO> materialReusSDOs, ref List<HIS_MATERIAL_TYPE> materialTypes, ref List<string> serialNumbers)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(impMaterialSDOs))
                {
                    List<long> materialTypeIds = impMaterialSDOs.Select(o => o.Material.MATERIAL_TYPE_ID).ToList();
                    List<HIS_MATERIAL_TYPE> matys = new HisMaterialTypeGet().GetByIds(materialTypeIds);

                    var mateReusables = impMaterialSDOs.Where(o => matys != null && matys.Any(a => a.ID == o.Material.MATERIAL_TYPE_ID && a.IS_REUSABLE == Constant.IS_TRUE)).ToList();
                    if (IsNotNullOrEmpty(mateReusables))
                    {
                        List<string> messages = new List<string>();
                        foreach (var item in mateReusables)
                        {
                            if (item.SerialNumbers == null || ((decimal)item.SerialNumbers.Count) != item.Material.AMOUNT)
                            {
                                var materialType = matys.FirstOrDefault(o => o.ID == item.Material.MATERIAL_TYPE_ID);
                                string name = materialType != null ? materialType.MATERIAL_TYPE_NAME : "";
                                messages.Add(name);
                                continue;
                            }
                            serialNumbers.AddRange(item.SerialNumbers.Select(s => s.SerialNumber).ToList());
                        }
                        if (IsNotNullOrEmpty(messages))
                        {
                            string ms = String.Join(";", messages);
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterialType__VTTSDSoLuongSerialKhacSoLuongNhap, ms);
                            return false;
                        }
                    }
                    materialTypes = matys;
                    materialReusSDOs = mateReusables;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckDuplicateSerialNumber(List<string> listSerialNumber, long? impMestId)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(listSerialNumber))
                {
                    var Groups = listSerialNumber.GroupBy(g => g).ToList();
                    var duplicate = Groups.Where(o => o.Count() >= 2).Select(s => s.FirstOrDefault()).ToList();
                    if (IsNotNullOrEmpty(duplicate))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TonTaiSoSerialNumberTrungNhau, String.Join(";", duplicate));
                        return false;
                    }
                    List<string> errors = new List<string>();
                    foreach (var seri in listSerialNumber)
                    {
                        HisImpMestMaterialFilterQuery imMaterFilter = new HisImpMestMaterialFilterQuery();
                        imMaterFilter.SERIAL_NUMBER__EXACT = seri;
                        imMaterFilter.IMP_MEST_ID__NOT__EQUAL = impMestId;
                        List<HIS_IMP_MEST_MATERIAL> exists = new HisImpMestMaterialGet().Get(imMaterFilter);
                        if (IsNotNullOrEmpty(exists))
                        {
                            errors.Add(seri);
                        }
                    }

                    if (IsNotNullOrEmpty(errors))
                    {
                        string nums = String.Join(";", errors);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacSoSeriSauDaTonTai, nums);
                        return false;
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

        internal bool IsNotHasImpMestProposeId(HIS_IMP_MEST impMests, long? impMestProposeId)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotHasImpMestProposeId(new List<HIS_IMP_MEST>() { impMests }, impMestProposeId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotHasImpMestProposeId(List<HIS_IMP_MEST> impMests, long? impMestProposeId)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> hasProposes = null;
                if (impMestProposeId.HasValue)
                {
                    hasProposes = impMests != null ? impMests.Where(o => o.IMP_MEST_PROPOSE_ID.HasValue && o.IMP_MEST_PROPOSE_ID.Value != impMestProposeId.Value).ToList() : null;
                }
                else
                {
                    hasProposes = impMests != null ? impMests.Where(o => o.IMP_MEST_PROPOSE_ID.HasValue).ToList() : null;
                }
                if (IsNotNullOrEmpty(hasProposes))
                {
                    string codes = String.Join(",", hasProposes.Select(s => s.IMP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacPhieuNhapSauDaDuocDeNghiTT, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra co cau hinh tu dong duyet, nhap, voi chuyen kho khong
        /// Neu co cau hinh thi check xem nguoi dung co quyen o ca kho xuat va kho nhap khong
        /// </summary>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool IsAutoStockTransfer(HIS_IMP_MEST impMest, long? expMediStockId)
        {
            bool valid = false;
            try
            {
                if (!HisMediStockCFG.IS_AUTO_STOCK_TRANSFER) return false;
                if (impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK
                    && expMediStockId.HasValue)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisUserRoomCFG.DATA.Any(a => a.LOGINNAME == loginname && HisMediStockCFG.DATA.Exists(e => e.ID == impMest.MEDI_STOCK_ID && e.ROOM_ID == a.ROOM_ID)))
                    {
                        return false;
                    }

                    if (!HisUserRoomCFG.DATA.Any(a => a.LOGINNAME == loginname && HisMediStockCFG.DATA.Exists(e => e.ID == expMediStockId.Value && e.ROOM_ID == a.ROOM_ID)))
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra tinh chinh xac cua thuoc khi huy thuc nhap
        /// </summary>
        /// <param name="medicineId">lo thuoc can check</param>
        /// <param name="mediStockId">Kho nhap</param>
        /// <param name="impMestId">Phieu nhap can huy thuc nhap</param>
        /// <returns></returns>
        internal bool CheckCorrectImpExpMedicine(long medicineId, long mediStockId, long impMestId)
        {
            List<ImpExpMestMedicineData> checks = new List<ImpExpMestMedicineData>();

            string queryExp = new StringBuilder().Append("SELECT -EMME.AMOUNT as AMOUNT, EMME.EXP_TIME as IMP_EXP_TIME FROM HIS_EXP_MEST_MEDICINE EMME ").Append("JOIN HIS_EXP_MEST EXP ON EMME.EXP_MEST_ID = EXP.ID ").Append(" WHERE EXP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND EMME.MEDICINE_ID = ").Append(medicineId).Append(" AND EMME.IS_EXPORT = 1 AND EMME.IS_DELETE = 0").ToString();
            List<ImpExpMestMedicineData> expMedicines = DAOWorker.SqlDAO.GetSql<ImpExpMestMedicineData>(queryExp);
            if (!IsNotNullOrEmpty(expMedicines))
            {
                return true;
            }
            checks.AddRange(expMedicines);
            string queryImp = new StringBuilder()
                .Append("SELECT IMME.AMOUNT as AMOUNT, IMP.IMP_TIME as IMP_EXP_TIME FROM HIS_IMP_MEST_MEDICINE IMME ")
                .Append("JOIN HIS_IMP_MEST IMP ON IMME.IMP_MEST_ID = IMP.ID ")
                .Append("WHERE IMP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND IMP.ID <> ").Append(impMestId).Append(" AND IMP.IMP_MEST_STT_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT).Append(" AND IMME.MEDICINE_ID = ").Append(medicineId).Append(" AND IMME.IS_DELETE = 0").ToString();
            List<ImpExpMestMedicineData> impMedicines = DAOWorker.SqlDAO.GetSql<ImpExpMestMedicineData>(queryImp);

            if (!IsNotNullOrEmpty(impMedicines))
            {
                return false;
            }
            checks.AddRange(impMedicines);
            checks = checks.OrderBy(o => o.IMP_EXP_TIME).ToList();
            decimal availAmount = 0;
            foreach (ImpExpMestMedicineData item in checks)
            {
                availAmount += item.AMOUNT;
                if (availAmount < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiem tra tinh chinh xac cua thuoc khi huy thuc nhap
        /// </summary>
        /// <param name="materialId">Lo vat tu can check</param>
        /// <param name="mediStockId">Kho nhap</param>
        /// <param name="impMestId">Phieu nhap can huy thuc nhap</param>
        /// <returns></returns>
        internal bool CheckCorrectImpExpMaterial(long materialId, long mediStockId, long impMestId)
        {
            List<ImpExpMestMaterialData> checks = new List<ImpExpMestMaterialData>();

            string queryExp = new StringBuilder().Append("SELECT -EMMA.AMOUNT as AMOUNT, EMMA.EXP_TIME as IMP_EXP_TIME FROM HIS_EXP_MEST_MATERIAL EMMA ").Append("JOIN HIS_EXP_MEST EXP ON EMMA.EXP_MEST_ID = EXP.ID ").Append(" WHERE EXP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND EMMA.MATERIAL_ID = ").Append(materialId).Append(" AND EMMA.IS_EXPORT = 1 AND EMMA.IS_DELETE = 0 ").ToString();
            List<ImpExpMestMaterialData> expMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestMaterialData>(queryExp);
            if (!IsNotNullOrEmpty(expMaterials))
            {
                return true;
            }
            checks.AddRange(expMaterials);
            string queryImp = new StringBuilder().Append("SELECT IMMA.AMOUNT as AMOUNT, IMP.IMP_TIME as IMP_EXP_TIME FROM HIS_IMP_MEST_MATERIAL IMMA ").Append("JOIN HIS_IMP_MEST IMP ON IMMA.IMP_MEST_ID = IMP.ID ").Append("WHERE IMP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND IMP.ID <> ").Append(impMestId).Append(" AND IMP.IMP_MEST_STT_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT).Append(" AND IMMA.MATERIAL_ID = ").Append(materialId).Append(" AND IMMA.IS_DELETE = 0 ").ToString();
            List<ImpExpMestMaterialData> impMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestMaterialData>(queryImp);

            if (!IsNotNullOrEmpty(impMaterials))
            {
                return false;
            }
            checks.AddRange(impMaterials);
            checks = checks.OrderBy(o => o.IMP_EXP_TIME).ToList();
            decimal availAmount = 0;
            foreach (ImpExpMestMaterialData item in checks)
            {
                availAmount += item.AMOUNT;
                if (availAmount < 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal bool IsRequesting(HIS_IMP_MEST raw)
        {
            bool valid = false;
            try
            {
                if (raw != null && (raw.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST))
                {
                    HIS_IMP_MEST_STT stt = HisImpMestSttCFG.DATA.Where(o => o.ID == raw.IMP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_PhieuNhapDangOTrangThai, stt.IMP_MEST_STT_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApproved(HIS_IMP_MEST raw)
        {
            bool valid = false;
            try
            {
                if (raw != null && (raw.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL))
                {
                    HIS_IMP_MEST_STT stt = HisImpMestSttCFG.DATA.Where(o => o.ID == raw.IMP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_PhieuNhapDangOTrangThai, stt.IMP_MEST_STT_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidImpTime(long? impTime)
        {
            bool valid = true;
            try
            {
                //Thoi gian nhap lon hon thoi gian hien tai
                if (impTime.HasValue && impTime.Value > Inventec.Common.DateTime.Get.Now().Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ThoiGianNhapLonHonThoiGianHienTai);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidExpMedicineAndMaterial(HIS_IMP_MEST raw, long? impTime)
        {
            bool valid = true;
            try
            {
                if (impTime.HasValue && raw != null)
                {
                    // Lay ra thong tin lo thuoc/vat tu nhap
                    List<HIS_IMP_MEST_MATERIAL> impMaterials = new HisImpMestMaterialGet().GetByImpMestId(raw.ID);
                    List<HIS_IMP_MEST_MEDICINE> impMedicines = new HisImpMestMedicineGet().GetByImpMestId(raw.ID);
                    List<long> materialIds = IsNotNullOrEmpty(impMaterials) ? impMaterials.Select(o => o.MATERIAL_ID).Distinct().ToList() : null;
                    List<long> medicineIds = IsNotNullOrEmpty(impMedicines) ? impMedicines.Select(o => o.MEDICINE_ID).Distinct().ToList() : null;

                    // Lay ra thong tin lo thuoc/vat tu xuat
                    List<HIS_EXP_MEST_MATERIAL> expMaterials = IsNotNullOrEmpty(materialIds) ? new HisExpMestMaterialGet().GetByMaterialIds(materialIds) : null;
                    List<HIS_EXP_MEST_MEDICINE> expMedicines = IsNotNullOrEmpty(medicineIds) ? new HisExpMestMedicineGet().GetByMedicineIds(medicineIds) : null;

                    List<long> expIds = new List<long>(); 
                    if (IsNotNullOrEmpty(expMaterials))
                    {
                        List<HIS_EXP_MEST_MATERIAL> unValidExpMaterial = expMaterials.Where(o => o.EXP_TIME < impTime).ToList();
                        if (IsNotNullOrEmpty(unValidExpMaterial))
                        {
                            unValidExpMaterial = unValidExpMaterial.Where(o => o.EXP_MEST_ID.HasValue).ToList();
                            expIds.AddRange(unValidExpMaterial.Select(o => o.EXP_MEST_ID.Value));
                        }
                    }

                    if (IsNotNullOrEmpty(expMedicines))
                    {
                        List<HIS_EXP_MEST_MEDICINE> unValidExpMedicine = expMedicines.Where(o => o.EXP_TIME < impTime).ToList();
                        if (IsNotNullOrEmpty(unValidExpMedicine))
                        {
                            unValidExpMedicine = unValidExpMedicine.Where(o => o.EXP_MEST_ID.HasValue).ToList();
                            expIds.AddRange(unValidExpMedicine.Select(o => o.EXP_MEST_ID.Value));
                        }
                    }

                    if (IsNotNullOrEmpty(expIds))
                    {
                        expIds = expIds.Distinct().ToList();
                        var expmest = new HisExpMestGet().GetByIds(expIds);
                        if (IsNotNullOrEmpty(expmest))
                        {
                            var listcodes = expmest.Select(o => o.EXP_MEST_CODE).Distinct().ToList();
                            string code = string.Join(", ", listcodes);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_TonTaiPhieuXuatCoThoiGianXuatNhoHonThoiGianNhap, code);
                            return false;
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("Khong lay duoc cac thong tin phieu xuat theo cac ids: ", expIds));
                            return false;
                        }
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

        internal bool IsValidApprovalTime(long? approvalTime)
        {
            bool valid = true;
            try
            {
                //Thoi gian duyet lon hon thoi gian hien tai
                if (approvalTime.HasValue && approvalTime.Value > Inventec.Common.DateTime.Get.Now().Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ThoiGianDuyetLonHonThoiGianHienTai);
                    return false;
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

    class ImpExpMestMedicineData
    {
        public long IMP_EXP_TIME { get; set; }
        public decimal AMOUNT { get; set; }
    }
    class ImpExpMestMaterialData
    {
        public long IMP_EXP_TIME { get; set; }
        public decimal AMOUNT { get; set; }
    }

}
