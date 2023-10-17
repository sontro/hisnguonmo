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

namespace MOS.MANAGER.HisExpMest.Depa
{
    partial class HisExpMestDepaCheck : BusinessBase
    {
        internal HisExpMestDepaCheck()
            : base()
        {

        }

        internal HisExpMestDepaCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool VerifyRequireField(HisExpMestDepaSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
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

        internal bool ValidData(HisExpMestDepaSDO data)
        {
            bool valid = true;
            try
            {
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Bloods)) throw new ArgumentNullException("data.Materials, data.Medicines, data.Bloods null");
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

        internal bool IsAllowed(HisExpMestDepaSDO data)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
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

        internal bool IsAllowUpdate(HisExpMestDepaSDO data, HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__HPKP)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la xuat hao phi khoa phong " + LogUtil.TraceData("expMest", expMest));
                }
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);

                if (workPlace == null)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("RequestRoomId invalid: " + data.ReqRoomId);
                }

                if (expMest.REQ_DEPARTMENT_ID != workPlace.DepartmentId && !(workPlace.MediStockId == expMest.MEDI_STOCK_ID || workPlace.MediStockId == expMest.IMP_MEDI_STOCK_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoHoacKhoaYeuCau);
                    throw new Exception("Nguoi dung khong lam viec o kho xuat hoac khoa yeu cau. Khong cho phep sua phieu xuat hao phi khoa phong");
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
