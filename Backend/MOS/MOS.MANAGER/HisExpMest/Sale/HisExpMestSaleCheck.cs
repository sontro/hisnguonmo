using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient;
using MOS.UTILITY;
using MOS.MANAGER.HisTransaction;

namespace MOS.MANAGER.HisExpMest.Sale
{
    partial class HisExpMestSaleCheck : BusinessBase
    {
        internal HisExpMestSaleCheck()
            : base()
        {

        }

        internal HisExpMestSaleCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool VerifyRequireField(HisExpMestSaleSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines)) throw new ArgumentNullException("data.Materials, data.Medicines null");
                if (!IsNotNullOrEmpty(data.MaterialBeanIds) && !IsNotNullOrEmpty(data.MedicineBeanIds)) throw new ArgumentNullException("data.MedicineBeanIds, data.MaterialBeanIds null");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (string.IsNullOrWhiteSpace(data.ClientSessionKey)) throw new ArgumentNullException("data.ClientSessionKey null");
                if (string.IsNullOrWhiteSpace(data.PatientName)) throw new ArgumentNullException("data.PatientName null");
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

        internal bool VerifyRequireField(List<HisExpMestSaleSDO> datas)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(datas)) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    valid = valid && this.VerifyRequireField(data);
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
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowed(HisExpMestSaleSDO data)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != data.MediStockId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }

                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MediStockId).FirstOrDefault();
                if (mediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongPhaiKhoKinhDoanh, mediStock.MEDI_STOCK_NAME);
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

        internal bool IsAllowed(List<HisExpMestSaleSDO> datas)
        {
            bool valid = true;
            try
            {
                foreach (var data in datas)
                {
                    valid = valid && IsAllowed(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra, neu trong truong hop cau hinh tach nhom thi ko cho phep:
        /// - Tao phieu xuat ban co nhieu hon 1 loai
        /// - Sua phieu xuat ban ma bo sung loai khac vao
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="olds"></param>
        /// <returns></returns>
        internal bool IsValidGroup(List<HisExpMestSaleSDO> listData, List<HIS_EXP_MEST> olds)
        {
            bool result = false;
            try
            {
                if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1
                    || HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                {
                    foreach (HisExpMestSaleSDO data in listData)
                    {
                        List<ExpMedicineTypeSDO> huongThan = null;
                        List<ExpMedicineTypeSDO> gayNghien = null;
                        List<ExpMedicineTypeSDO> thuocThuong = null;
                        List<ExpMedicineTypeSDO> thucPham = null;
                        
                        this.SplitByGroup(data, ref huongThan, ref gayNghien, ref thuocThuong, ref thucPham);

                        int ht = 0;
                        int gn = 0;
                        int hotro = 0;
                        int thuong = 0;

                        if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1)
                        {
                            ht = IsNotNullOrEmpty(huongThan) ? 1 : 0;
                            gn = IsNotNullOrEmpty(gayNghien) ? 1 : 0;
                            hotro = IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(data.Materials) ? 1 : 0;
                            thuong = IsNotNullOrEmpty(thuocThuong) ? 1 : 0;
                        }
                        else if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                        {
                            ht = IsNotNullOrEmpty(huongThan) ? 1 : 0;
                            gn = IsNotNullOrEmpty(gayNghien) ? 1 : 0;
                            hotro = IsNotNullOrEmpty(thucPham) ? 1 : 0;
                            thuong = IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(data.Materials) ? 1 : 0;
                        }
                        
                        if (ht + gn + hotro + thuong > 1)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepTaoPhieuXuatCoCacLoaiKhacNhau);
                            return false;
                        }

                        HIS_EXP_MEST old = IsNotNullOrEmpty(olds) && data.ExpMestId.HasValue ? olds.Where(o => o.ID == data.ExpMestId.Value).FirstOrDefault() : null;

                        if (old != null)
                        {
                            if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HUONG_THAN
                                && (IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(data.Materials)))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonHuongThanKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                return false;
                            }

                            if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.GAY_NGHIEN
                                && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(data.Materials)))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonGayNghienKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                return false;
                            }

                            if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1)
                            {
                                if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.THUONG
                                && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(data.Materials)))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonThuocThuongKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                    return false;
                                }

                                if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HO_TRO
                                    && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong)))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonHoTroKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                    return false;
                                }
                            }
                            else if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                            {
                                if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.THUONG
                                && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thucPham)))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonThuocThuongKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                    return false;
                                }

                                if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HO_TRO
                                    && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(data.Materials)))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DonHoTroKhongChoPhepBoSungLoaiKhac, old.EXP_MEST_CODE);
                                    return false;
                                }
                            }
                        }

                        if (ht == 1)
                        {
                            data.PresGroup = (long)HisServiceReqCFG.PresGroup.HUONG_THAN;
                        }
                        if (gn == 1)
                        {
                            data.PresGroup = (long)HisServiceReqCFG.PresGroup.GAY_NGHIEN;
                        }
                        if (hotro == 1)
                        {
                            data.PresGroup = (long)HisServiceReqCFG.PresGroup.HO_TRO;
                        }
                        if (thuong == 1)
                        {
                            data.PresGroup = (long)HisServiceReqCFG.PresGroup.THUONG;
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        private void SplitByGroup(HisExpMestSaleSDO data, ref List<ExpMedicineTypeSDO> huongThan, ref List<ExpMedicineTypeSDO> gayNghien, ref List<ExpMedicineTypeSDO> thuocThuong, ref List<ExpMedicineTypeSDO> thucPham)
        {
            try
            {
                huongThan = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId)).ToList() : null;

                gayNghien = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId)).ToList() : null;

                thuocThuong = IsNotNullOrEmpty(data.Medicines) ? data.Medicines
                    .Where(t => (HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs == null || !HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                        ).ToList() : null;

                thucPham = IsNotNullOrEmpty(data.Medicines) ? data.Medicines
                    .Where(t => HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs != null && HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId)
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                        ).ToList() : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal bool IsValidTransactionInfo(HisExpMestSaleListSDO data, ref V_HIS_ACCOUNT_BOOK hisAccountBook, ref long? transactionTime)
        {
            bool valid = true;
            try
            {
                if (data != null && data.CreateBill)
                {
                    if (!data.CashierRoomId.HasValue || !data.AccountBookId.HasValue || !data.PayFormId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TrongTruongHopXuatBienLaiCanNhapThongTinGiaoDich);
                        return false;
                    }
                    if (data.TransferAmount.HasValue && data.PayFormId != IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TMCK)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("TransferAmount Is Not Null && PayFormId <> Tien Mat/Chuyen Khoan");
                        return false;
                    }
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == data.CashierRoomId.Value).FirstOrDefault();

                    if (cashierRoom == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("data.CashierRoomId khong ton tai");
                        return false;
                    }

                    if (!HisUserRoomCFG.DATA.Exists(o => o.LOGINNAME == loginName && o.IS_ACTIVE == Constant.IS_TRUE && o.ROOM_ID == cashierRoom.ROOM_ID))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                        return false;
                    }

                    transactionTime = Inventec.Common.DateTime.Get.Now().Value;

                    HisTransactionCheck commonChecker = new HisTransactionCheck(param);

                    valid = valid && commonChecker.IsUnlockAccountBook(data.AccountBookId.Value, ref hisAccountBook);
                    valid = valid && commonChecker.HasNotFinancePeriod(data.CashierRoomId.Value, transactionTime.Value);
                    valid = valid && commonChecker.IsValidNumOrder(data.TransactionNumOrder, hisAccountBook);
                    valid = valid && commonChecker.IsValidAccountBookType(IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT, hisAccountBook);

                    return valid;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidTransactionInfo(HisExpMestSaleSDO data, ref V_HIS_ACCOUNT_BOOK hisAccountBook)
        {
            bool valid = true;
            try
            {
                if (data != null && data.CreateBill)
                {
                    if (!data.CashierRoomId.HasValue || !data.AccountBookId.HasValue || !data.PayFormId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TrongTruongHopXuatBienLaiCanNhapThongTinGiaoDich);
                        return false;
                    }
                    if (data.TransferAmount.HasValue && data.PayFormId != IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TMCK)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("TransferAmount Is Not Null && PayFormId <> Tien Mat/Chuyen Khoan");
                        return false;
                    }
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == data.CashierRoomId.Value).FirstOrDefault();

                    if (cashierRoom == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("data.CashierRoomId khong ton tai");
                        return false;
                    }

                    if (!HisUserRoomCFG.DATA.Exists(o => o.LOGINNAME == loginName && o.IS_ACTIVE == Constant.IS_TRUE && o.ROOM_ID == cashierRoom.ROOM_ID))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                        return false;
                    }

                    HisTransactionCheck commonChecker = new HisTransactionCheck(param);
                    long transactionTime = Inventec.Common.DateTime.Get.Now().Value;
                    valid = valid && commonChecker.IsUnlockAccountBook(data.AccountBookId.Value, ref hisAccountBook);
                    valid = valid && commonChecker.HasNotFinancePeriod(data.CashierRoomId.Value, transactionTime);
                    valid = valid && commonChecker.IsValidNumOrder(data.TransactionNumOrder, hisAccountBook);
                    valid = valid && commonChecker.IsValidAccountBookType(IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT, hisAccountBook);

                    return valid;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckAuto(long mediStockId, ref AutoEnum en)
        {
            bool valid = true;
            try
            {

                HIS_MEDI_STOCK_EXTY mediStockExty = HisMediStockExtyCFG.DATA.FirstOrDefault(o => o.MEDI_STOCK_ID == mediStockId && o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN);
                if (mediStockExty != null && mediStockExty.IS_AUTO_APPROVE == Constant.IS_TRUE)
                {
                    en = AutoEnum.APPROVE;
                    if (mediStockExty.IS_AUTO_EXECUTE == Constant.IS_TRUE && !HisExpMestCFG.EXPORT_SALE_MUST_BILL)
                    {
                        en = AutoEnum.APPROVE_EXPORT;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }

    enum AutoEnum
    {
        NONE = 1,
        APPROVE = 2,
        APPROVE_EXPORT = 3
    }
}
