using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMestPatientType;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisMediStockPeriod;
using MOS.UTILITY;

namespace MOS.MANAGER.HisMediStock
{
    class HisMediStockCheck : BusinessBase
    {
        internal HisMediStockCheck()
            : base()
        {

        }

        internal HisMediStockCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ROOM_ID)) throw new ArgumentNullException("data.ROOM_ID");
                if (!IsNotNullOrEmpty(data.MEDI_STOCK_NAME)) throw new ArgumentNullException("data.MEDI_STOCK_NAME");
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
                if (DAOWorker.HisMediStockDAO.ExistsCode(code, id))
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

        internal bool IsUnLock(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, data.MEDI_STOCK_NAME);
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
                if (!DAOWorker.HisMediStockDAO.IsUnLock(id))
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
        /// Check theo du lieu cache trong RAM (du lieu nay duoc reload dinh ky)
        /// Viec check nay nham tang hieu nang do khong can truy van vao DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool IsUnLockCache(long id)
        {
            bool valid = true;
            try
            {
                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == id).FirstOrDefault();
                if (mediStock == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("mediStock null");
                }
                if (mediStock.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStock.MEDI_STOCK_NAME);
                    throw new Exception("Kho dang bi khoa: " + mediStock.MEDI_STOCK_NAME);
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
                List<HIS_MEDI_STOCK> hisMediStocks = new HisMediStockGet().GetByParentId(id);
                if (IsNotNullOrEmpty(hisMediStocks))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_TonTaiDuLieuCon);
                    throw new Exception("Ton tai du lieu con HIS_MEDI_STOCK, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MEDICINE_BEAN> hisMedicineBeans = new HisMedicineBeanGet().GetByMediStockId(id);
                if (IsNotNullOrEmpty(hisMedicineBeans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEDICINE_BEAN, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_EXP_MEST> hisExpMests = new HisExpMestGet().GetByMediStockIdOrImpMediStockId(id);
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_EXP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_IMP_MEST> hisImpMests = new HisImpMestGet().GetByMediStockId(id);
                if (IsNotNullOrEmpty(hisImpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_IMP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MATERIAL_BEAN> hisMaterialBeans = new HisMaterialBeanGet().GetByMediStockId(id);
                if (IsNotNullOrEmpty(hisMaterialBeans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEST_PATIENT_TYPE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MEDI_STOCK_PERIOD> hisMediStockPeriods = new HisMediStockPeriodGet().GetByMediStockId(id);
                if (IsNotNullOrEmpty(hisMediStockPeriods))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStockPeriod_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEDI_STOCK_PERIOD, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_EXP_MEST> hisChmsExpMests = new HisExpMestGet().GetByImpMediStockId(id);
                if (IsNotNullOrEmpty(hisChmsExpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisChmsExpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu xuat chuyen kho HIS_EXP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool VerifyId(long id, ref HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                data = new HisMediStockGet().GetById(id);
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

        internal bool IsCabinetStock(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data.IS_CABINET != Constant.IS_TRUE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoKhongPhaiTuTruc, data.MEDI_STOCK_NAME);
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

        internal bool IsNotCabinetStock(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data.IS_CABINET == Constant.IS_TRUE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoKhongPhaiKhoThuong, data.MEDI_STOCK_NAME);
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

        internal bool IsCabinetManageOptionPresDetail(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data.CABINET_MANAGE_OPTION != IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES_DETAIL)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_TuTrucKhongQuanLyCoSoTheoChiTietDon, data.MEDI_STOCK_NAME);
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

        internal bool IsCabinetManageOptionBase(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data.CABINET_MANAGE_OPTION != IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__BASE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_TuTrucKhongQuanLyCoSoTheoCoSoThietLap, data.MEDI_STOCK_NAME);
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

        internal bool IsCabinetManageOptionPres(HIS_MEDI_STOCK data)
        {
            bool valid = true;
            try
            {
                if (data.CABINET_MANAGE_OPTION.HasValue && data.CABINET_MANAGE_OPTION != IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_TuTrucKhongQuanLyCoSoTheoDon, data.MEDI_STOCK_NAME);
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
