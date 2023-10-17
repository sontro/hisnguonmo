using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockPeriod
{
    class HisMediStockPeriodCheck : BusinessBase
    {
        internal HisMediStockPeriodCheck()
            : base()
        {

        }

        internal HisMediStockPeriodCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_MEDI_STOCK_PERIOD data)
        {
            bool valid = true;
            try
            {
                valid = data != null;
                valid = valid && IsGreaterThanZero(data.MEDI_STOCK_ID);
                valid = valid && IsNotNullOrEmpty(data.MEDI_STOCK_PERIOD_NAME);
                if (!valid)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Data null hoac MEDI_STOCK_ID <= 0 hoac MEDI_STOCK_PERIOD_NAME rong. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                }
            }
            catch (ArgumentNullException ex)
            {
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

        internal bool VerifyRequireField(HisMestPeriodApproveSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MediStockPeriodId)) throw new ArgumentNullException("data.MediStockPeriodId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyId(long id, ref HIS_MEDI_STOCK_PERIOD data)
        {
            bool valid = true;
            try
            {
                data = new HisMediStockPeriodGet().GetById(id);
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

        internal bool IsUnLock(HIS_MEDI_STOCK_PERIOD data)
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
                if (!DAOWorker.HisMediStockPeriodDAO.IsUnLock(id))
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_MEDI_STOCK_PERIOD> hisMediStockPeriods = new HisMediStockPeriodGet().GetByPreviousId(id);
                if (IsNotNullOrEmpty(hisMediStockPeriods))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStockPeriod_TonTaiDuLieuKiSau);
                    throw new Exception("Ton tai du lieu ki sau do (HIS_MEDI_STOCK_PERIOD), khong cho phep xoa" + LogUtil.TraceData("id", id));
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

        internal bool CheckToTime(HIS_MEDI_STOCK_PERIOD data, ref HIS_MEDI_STOCK_PERIOD previousHisMediStockPeriod)
        {
            bool valid = true;
            try
            {
                //neu khong truyen len thoi gian thi lay thoi gian hien tai
                if (!data.TO_TIME.HasValue)
                {
                    data.TO_TIME = Inventec.Common.DateTime.Get.Now().Value;
                }

                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.TO_TIME.Value > now)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(now);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_ThoiGianChotKyKhongDuocLonHonThoiGianHienTai, time);
                    return false;
                }
                HisMediStockPeriodFilterQuery filter = new HisMediStockPeriodFilterQuery();
                filter.MEDI_STOCK_ID = data.MEDI_STOCK_ID;

                List<HIS_MEDI_STOCK_PERIOD> exists = new HisMediStockPeriodGet().Get(filter);
                if (IsNotNullOrEmpty(exists))
                {
                    previousHisMediStockPeriod = exists.OrderByDescending(o => o.TO_TIME).FirstOrDefault();
                    if (exists.Exists(o => o.TO_TIME > data.TO_TIME))
                    {
                        long recentTime = exists.FirstOrDefault(o => o.TO_TIME > data.TO_TIME).TO_TIME.Value;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_ThoiGianChotKyKhongDuocNhoHonThoiGianChotKyGanNhat, Inventec.Common.DateTime.Convert.TimeNumberToTimeString(recentTime));
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

        internal bool IsNotApprove(HIS_MEDI_STOCK_PERIOD data)
        {
            bool valid = true;
            try
            {
                if (data.IS_APPROVE == Constant.IS_TRUE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_KyKhoDaDuocDuyet);
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

        internal bool IsApprove(HIS_MEDI_STOCK_PERIOD data)
        {
            bool valid = true;
            try
            {
                if (data.IS_APPROVE != Constant.IS_TRUE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStockPeriod_KyKhoChuaDuocDuyet);
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

        internal bool VerifyWokingInStock(HIS_MEDI_STOCK_PERIOD raw, WorkPlaceSDO workplace)
        {
            bool valid = true;
            try
            {
                if (!workplace.MediStockId.HasValue || workplace.MediStockId.Value != raw.MEDI_STOCK_ID)
                {
                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == raw.MEDI_STOCK_ID);
                    string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKho, name);
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
