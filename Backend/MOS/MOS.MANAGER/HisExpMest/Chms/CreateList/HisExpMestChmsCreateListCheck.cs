using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class HisExpMestChmsCreateListCheck : BusinessBase
    {
        internal HisExpMestChmsCreateListCheck()
            : base()
        {

        }

        internal HisExpMestChmsCreateListCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisExpMestChmsListSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId null");
                if (!IsNotNullOrEmpty(data.ExpMests)) throw new ArgumentNullException("data.ExpMests null");

                foreach (var item in data.ExpMests)
                {
                    if (!IsNotNullOrEmpty(item.MedicineTypes)
                    && !IsNotNullOrEmpty(item.MaterialTypes)
                    && !IsNotNullOrEmpty(item.BloodTypes)
                    && !IsNotNullOrEmpty(item.Medicines)
                    && !IsNotNullOrEmpty(item.Materials))
                        throw new ArgumentNullException("item.MedicineTypes, item.MaterialTypes, item.BloodTypes, item.Medicines, item.Materials null");

                    if ((IsNotNullOrEmpty(item.MaterialTypes) || IsNotNullOrEmpty(item.MedicineTypes))
                        && (IsNotNullOrEmpty(item.Medicines) || IsNotNullOrEmpty(item.Materials)))
                        throw new ArgumentNullException("Chi duoc phep xuat chuyen kho theo lo hoac theo loai");
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool VerifyAllow(HisExpMestChmsListSDO data)
        {
            try
            {

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.WorkingRoomId);

                //Neu la linh~ thuoc thi 
                if (data.Type == ChmsTypeEnum.GET)
                {
                    //kho yeu cau phai la kho nhan
                    if (workPlace == null || data.ExpMests.Any(a => workPlace.MediStockId != a.ImpMediStockId))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoNhap);
                        return false;
                    }

                    //Khong duoc phep trung kho nhap
                    if (data.ExpMests.GroupBy(g => g.ExpMediStockId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Error("Ton tai cac phieu xuat trung kho nhap va kho xuat");
                        return false;
                    }
                }

                //Neu la tra thuoc thi
                if (data.Type == ChmsTypeEnum.GIVE_BACK)
                {
                    // kho yeu cau phai la kho xuat
                    if (workPlace == null || data.ExpMests.Any(a => workPlace.MediStockId != a.ExpMediStockId))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                        return false;
                    }

                    //Khong duoc phep trung kho nhap
                    if (data.ExpMests.GroupBy(g => g.ImpMediStockId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Error("Ton tai cac phieu xuat trung kho nhap va kho xuat");
                        return false;
                    }
                }

                foreach (var item in data.ExpMests)
                {
                    V_HIS_MEDI_STOCK expMediStock = HisMediStockCFG.DATA.Where(o => o.ID == item.ExpMediStockId).FirstOrDefault();
                    V_HIS_MEDI_STOCK impMediStock = HisMediStockCFG.DATA.Where(o => o.ID == item.ImpMediStockId).FirstOrDefault();

                    if (impMediStock == null || expMediStock == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Error("Id kho xuat hoac kho nhan ko hop le");
                        return false;
                    }

                    List<HIS_MEST_ROOM> lst = IsNotNullOrEmpty(HisMestRoomCFG.DATA) ? HisMestRoomCFG.DATA.Where(o => o.MEDI_STOCK_ID == item.ExpMediStockId && o.ROOM_ID == impMediStock.ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;

                    if (!IsNotNullOrEmpty(lst))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisChmsExpMest_KhongKhaXuatGiua2Kho, expMediStock.MEDI_STOCK_NAME, impMediStock.MEDI_STOCK_NAME);
                        return false;
                    }

                    if ((impMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE) || (impMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepXuatChuyenKhoGiuaKhoKinhDoanhVaKhoThuong);
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
