using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Test
{
    /// <summary>
    /// Xu ly phieu xuat hoa chat chay xet nghiem
    /// </summary>
    class HisExpMestTestCreateCheck : BusinessBase
    {
        internal HisExpMestTestCreateCheck()
            : base()
        {
        }

        internal HisExpMestTestCreateCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(ExpMestTestSDO data, ref List<HIS_SERE_SERV_TEIN> sereServTeins)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (!IsNotNullOrEmpty(data.Materials)) throw new ArgumentNullException("data.Materials");

                if (data.ExpMestTestType == ExpMestTestTypeEnum.QC && !data.MachineId.HasValue)
                {
                    throw new ArgumentNullException("Loai la chay QC bat buoc truyen data.MachineId");
                }

                if (data.ExpMestTestType == ExpMestTestTypeEnum.TEST && IsNotNullOrEmpty(data.SereServIds))
                {
                    sereServTeins = new HisSereServTeinGet().GetBySereServIds(data.SereServIds);
                    List<HIS_SERE_SERV_TEIN> hasExpMests = IsNotNullOrEmpty(sereServTeins) ? sereServTeins.Where(o => o.EXP_MEST_ID.HasValue).ToList() : null;
                    if (IsNotNullOrEmpty(hasExpMests))
                    {
                        List<long> expMestIds = hasExpMests.Select(o => o.EXP_MEST_ID.Value).ToList();
                        List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByIds(expMestIds);
                        List<string> expMestCodes = IsNotNullOrEmpty(expMests) ? expMests.Select(o => o.EXP_MEST_CODE).ToList() : null;
                        string expMestCodeStr = string.Join(",", expMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaTonTaiPhieuXuatHoaChat, expMestCodeStr);
                        return false;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsAllowMediStock(WorkPlaceSDO workPlace, long mediStockId)
        {
            V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId).FirstOrDefault();

            if (mediStock == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                LogSystem.Warn("mediStockId ko hop le");
                return false;
            }

            if (mediStock.IS_ACTIVE != Constant.IS_TRUE)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStock.MEDI_STOCK_NAME);
                return false;
            }

            //Kiem tra xem co kho khong nam trong d/s cac kho duoc cau hinh cho phep xuat tu phong dang lam viec hay khong
            if (HisMestRoomCFG.DATA == null || !HisMestRoomCFG.DATA.Exists(t => t.MEDI_STOCK_ID == mediStockId && t.ROOM_ID == workPlace.RoomId && t.IS_ACTIVE == Constant.IS_TRUE))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoSauKhongChoPhepXuatDenPhongDangLamViec, mediStock.MEDI_STOCK_NAME);
                return false;
            }

            
            return true;
        }
    }
}
