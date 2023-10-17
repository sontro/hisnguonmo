using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.Chms
{
    partial class HisExpMestChmsCheck : BusinessBase
    {
        internal HisExpMestChmsCheck()
            : base()
        {

        }

        internal HisExpMestChmsCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool VerifyRequireField(HisExpMestChmsSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
                if (data.ImpMediStockId <= 0) throw new ArgumentNullException("data.ImpMediStockId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool VerifyRequireField(HisExpMestChmsListSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool ValidData(HisExpMestChmsSDO data)
        {
            bool valid = true;
            try
            {
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Bloods) && !IsNotNullOrEmpty(data.ExpMaterialSdos) && !IsNotNullOrEmpty(data.ExpMedicineSdos)) throw new ArgumentNullException("data.Materials, data.Medicines, data.Bloods, data.ExpMaterialSdos, data.ExpMedicineSdos null");
                if ((IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.Medicines)) && (IsNotNullOrEmpty(data.ExpMaterialSdos) || IsNotNullOrEmpty(data.ExpMedicineSdos))) throw new ArgumentNullException("Chi duoc phep xuat chuyen kho theo lo hoac theo loai");
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

        internal bool IsAllowed(HisExpMestChmsSDO data)
        {
            try
            {
                V_HIS_MEDI_STOCK expMediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MediStockId).FirstOrDefault();
                V_HIS_MEDI_STOCK impMediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.ImpMediStockId).FirstOrDefault();

                if (impMediStock == null || expMediStock == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Id kho xuat hoac kho nhan ko hop le");
                    return false;
                }
                List<HIS_MEST_ROOM> lst = IsNotNullOrEmpty(HisMestRoomCFG.DATA) ? HisMestRoomCFG.DATA.Where(o => o.MEDI_STOCK_ID == data.MediStockId && o.ROOM_ID == impMediStock.ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;

                if (!IsNotNullOrEmpty(lst))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisChmsExpMest_KhongKhaXuatGiua2Kho, expMediStock.MEDI_STOCK_NAME, impMediStock.MEDI_STOCK_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);

                //Neu la linh~ thuoc thi kho yeu cau phai la kho nhan
                if (data.Type == ChmsTypeEnum.GET && (workPlace == null || workPlace.MediStockId != data.ImpMediStockId))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoNhap);
                    return false;
                }

                //Neu la tra thuoc thi kho yeu cau phai la kho xuat
                if (data.Type == ChmsTypeEnum.GIVE_BACK && (workPlace == null || workPlace.MediStockId != data.MediStockId))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }

                if ((impMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE) || (impMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepXuatChuyenKhoGiuaKhoKinhDoanhVaKhoThuong);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool IsAllowStatusUpdate(HIS_EXP_MEST data)
        {
            try
            {
                if (!HisExpMestConstant.ALLOW_UPDATE_DETAIL_STT_IDs.Contains(data.EXP_MEST_STT_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepCapNhatKhiDangOTrangThaiNay);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool IsAllowUpdate(HisExpMestChmsSDO data, HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la xua chuyen kho " + LogUtil.TraceData("expMest", expMest));
                }
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);

                if (workPlace == null)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("RequestRoomId invalid: " + data.ReqRoomId);
                }

                if (expMest.REQ_ROOM_ID != data.ReqRoomId && !(workPlace.MediStockId == expMest.MEDI_STOCK_ID || workPlace.MediStockId == expMest.IMP_MEDI_STOCK_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoNhapHoacKhoXuat);
                    throw new Exception("Nguoi dung khong lam viec o kho xuat hoac kho nhap. Khong cho phep sua phieu xuat Chuyen Kho");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

    }
}
