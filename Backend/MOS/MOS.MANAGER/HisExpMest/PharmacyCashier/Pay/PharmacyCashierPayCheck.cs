using Inventec.Core;
using Inventec.Common.Logging;

using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisExpMest.Common.Get;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.Pay
{
    public class PharmacyCashierPayCheck : BusinessBase
    {
        internal PharmacyCashierPayCheck()
            : base()
        {
        }

        internal PharmacyCashierPayCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsAllowing(WorkPlaceSDO workPlaceSdo, PharmacyCashierSDO sdo, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            bool valid = true;
            try
            {
                if (workPlaceSdo == null || !workPlaceSdo.MediStockId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongLamViecTaiKho);
                    return false;
                }

                var cr = HisCashierRoomCFG.DATA.Where(o => o.ID == sdo.CashierRoomId).FirstOrDefault();
                if (cr == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.CashierRoomId ko hop le");
                    return false;
                }

                if (!HisUserRoomCFG.DATA.Exists(t => t.ROOM_ID == cr.ROOM_ID && t.IS_ACTIVE == Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                    return false;
                }
                cashierRoom = cr;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidAccountBook(PharmacyCashierSDO sdo, ref V_HIS_ACCOUNT_BOOK recieptBook, ref V_HIS_ACCOUNT_BOOK invoiceBook)
        {
            try
            {

                //Neu thanh toan dich vu thi bat buoc phai chon so hoa don
                if (IsNotNullOrEmpty(sdo.InvoiceSereServs) || IsNotNullOrEmpty(sdo.InvoiceAssignServices))
                {
                    V_HIS_ACCOUNT_BOOK ib = null;
                    ib = new HisAccountBookGet().GetViewById(sdo.InvoiceAccountBookId);
                    if (ib == null || ib.BILL_TYPE_ID != HisAccountBookCFG.BILL_TYPE_ID__INVOICE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaChonSoHoaDon);
                        return false;
                    }
                    if (!this.IsUnlockAndValidNumOrder(sdo.InvoiceNumOrder, this.GetNumTransByMaxItemPerTrans(sdo.InvoiceSereServs, sdo.InvoiceAssignServices, ib), ib))
                    {
                        return false;
                    }
                    invoiceBook = ib;
                }

                //Neu thanh toan dich vu dttt bhyt/vien phi va khong phai vac xin thi bat buoc phai chon so bien lai
                if (IsNotNullOrEmpty(sdo.RecieptSereServs) || IsNotNullOrEmpty(sdo.RecieptAssignServices))
                {
                    V_HIS_ACCOUNT_BOOK rb = null;
                    rb = new HisAccountBookGet().GetViewById(sdo.RecieptAccountBookId);
                    if (rb == null || rb.BILL_TYPE_ID == HisAccountBookCFG.BILL_TYPE_ID__INVOICE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaChonSoBienLai);
                        return false;
                    }
                    if (!this.IsUnlockAndValidNumOrder(sdo.RecieptNumOrder, this.GetNumTransByMaxItemPerTrans(sdo.RecieptSereServs, sdo.RecieptAssignServices, rb), rb))
                    {
                        return false;
                    }
                    recieptBook = rb;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private long GetNumTransByMaxItemPerTrans(List<SereServTranSDO> SereServs, List<AssignServiceExtSDO> AssignServices, V_HIS_ACCOUNT_BOOK book)
        {
            long num = 0;
            if (book.MAX_ITEM_NUM_PER_TRANS.HasValue && book.MAX_ITEM_NUM_PER_TRANS.Value > 0)
            {
                long count = 0;
                count += (SereServs != null ? SereServs.Count : 0);
                count += (AssignServices != null ? AssignServices.Count : 0);
                long mod = count % book.MAX_ITEM_NUM_PER_TRANS.Value;
                if (mod > 0)
                {
                    num = (count / book.MAX_ITEM_NUM_PER_TRANS.Value);
                }
                else
                {
                    num = ((count / book.MAX_ITEM_NUM_PER_TRANS.Value) - 1);
                }
            }
            return num;
        }

        private bool IsUnlockAndValidNumOrder(long? numOrder, long numTrans, V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, accountBook.ACCOUNT_BOOK_NAME);
                    return false;
                }

                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER == MOS.UTILITY.Constant.IS_TRUE)
                {
                    if (!numOrder.HasValue || numOrder.Value <= 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaNhapSoChungTu, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }

                    long max = (accountBook.FROM_NUM_ORDER + accountBook.TOTAL) - 1;
                    if (numOrder < accountBook.FROM_NUM_ORDER || (numOrder + numTrans) > max)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiNamNgoaiKhoangChoPhep, accountBook.ACCOUNT_BOOK_NAME, accountBook.FROM_NUM_ORDER.ToString(), max.ToString());
                        return false;
                    }

                    HisTransactionFilterQuery tranFilterQuery = new HisTransactionFilterQuery();
                    tranFilterQuery.ACCOUNT_BOOK_ID = accountBook.ID;
                    tranFilterQuery.NUM_ORDER__EQUAL = numOrder;
                    var listTran = new HisTransactionGet().Get(tranFilterQuery);
                    if (IsNotNullOrEmpty(listTran))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoChungTuCuaSoThuChiDaTonTai, numOrder.Value.ToString(), accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }
                }
                else
                {
                    if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - (numTrans + 1)))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_HetSo, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }
                    if (numOrder > 0)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                }

                if (accountBook.IS_FOR_BILL != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongChoPhepThucHienLoaiGiaoDich, accountBook.ACCOUNT_BOOK_NAME);
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

        internal bool IsValidTreatment(PharmacyCashierSDO sdo, ref HIS_TREATMENT treatment, ref HIS_SERVICE_REQ prescription, ref List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                if (sdo.PrescriptionId.HasValue)
                {
                    prescription = new HisServiceReqGet().GetById(sdo.PrescriptionId.Value);
                    if (prescription == null || !HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(prescription.SERVICE_REQ_TYPE_ID))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sdo.PrescriptionId ko hop le (ko ton tai hoac ko phai don thuoc)");
                        return false;
                    }
                }
                List<long> sereServIds = new List<long>();
                if (IsNotNullOrEmpty(sdo.InvoiceSereServs))
                {
                    sereServIds.AddRange(sdo.InvoiceSereServs.Select(o => o.SereServId).ToList());
                }
                if (IsNotNullOrEmpty(sdo.RecieptSereServs))
                {
                    sereServIds.AddRange(sdo.RecieptSereServs.Select(o => o.SereServId).ToList());
                }

                if (IsNotNullOrEmpty(sereServIds))
                {
                    sereServs = new HisSereServGet().GetByIds(sereServIds);
                    if (!IsNotNullOrEmpty(sereServs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sdo.SereServIds ko ton tai");
                        return false;
                    }
                }

                if (prescription != null || IsNotNullOrEmpty(sereServs))
                {
                    long treatmentId = prescription != null ? prescription.TREATMENT_ID : sereServs[0].TDL_TREATMENT_ID.Value;
                    if (IsNotNullOrEmpty(sereServs) && sereServs.Exists(o => o.TDL_TREATMENT_ID != treatmentId))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai du lieu thuoc 2 ho so khac nhau");
                        return false;
                    }

                    if (!new HisTreatmentCheck(param).IsUnLock(treatmentId, ref treatment))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotHeinSereServ(List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                if (sereServs != null && sereServs.Exists(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TonTaiDichVuBhyt);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidInvoiceSereServPrice(List<HIS_SERE_SERV> sereServs, List<SereServTranSDO> invoiceSSTranSDOs, List<AssignServiceExtSDO> invoiceAssignServices, WorkPlaceSDO wp, HIS_TREATMENT treatment, ref List<HIS_SERE_SERV_BILL> invoiceSereServBills, ref List<HIS_BILL_GOODS> invoiceBillGoods)
        {
            try
            {
                List<HIS_SERE_SERV_BILL> newBills = new List<HIS_SERE_SERV_BILL>();
                if (IsNotNullOrEmpty(invoiceSSTranSDOs) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = invoiceSSTranSDOs.Select(o => o.SereServId).Distinct().ToList();

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_BILL> existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);

                    //Thuc hien kiem tra xem trong d/s cac sere_serv thanh toan, co dich vu nao co tong so tien
                    //thanh toan vuot qua so tien BN can thanh toan ko
                    List<string> serviceNames = new List<string>();
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        if (invoiceSSTranSDOs.Exists(e => e.SereServId == s.ID))
                        {
                            HIS_MEDICINE_TYPE medicineType = null;
                            if (s.MEDICINE_ID.HasValue)
                            {
                                medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.SERVICE_ID == s.SERVICE_ID);
                            }
                            if ((medicineType == null || medicineType.IS_VACCINE != Constant.IS_TRUE)
                               && (s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("Ton tai dich vu hoa don khong phai la vacxin va co doi tuong thanh toan la BHYT/VienPhi" + LogUtil.TraceData("SereServ", s));
                            }
                            decimal totalBill = IsNotNullOrEmpty(existsBills) ? existsBills.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.PRICE) : 0;
                            decimal payAmount = invoiceSSTranSDOs.Where(o => o.SereServId == s.ID).Sum(o => o.Price);
                            totalBill += payAmount;

                            //Luu y: check lech tien voi "Constant.PRICE_DIFFERENCE", de tranh truong hop lam tron 
                            //(4 so sau phan thap phan) 
                            if (totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value > Constant.PRICE_DIFFERENCE)
                            {
                                serviceNames.Add(s.TDL_SERVICE_NAME);
                            }
                            else
                            {
                                HIS_SERE_SERV_BILL ssBill = new HIS_SERE_SERV_BILL();
                                ssBill.SERE_SERV_ID = s.ID;
                                ssBill.PRICE = payAmount;
                                HisSereServBillUtil.SetTdl(ssBill, s);
                                newBills.Add(ssBill);
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(serviceNames))
                    {
                        string nameStr = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_VuotQuaSoTienCanThanhToan, nameStr);
                        return false;
                    }
                }

                List<HIS_BILL_GOODS> hisBillGoods = new List<HIS_BILL_GOODS>();

                if (IsNotNullOrEmpty(invoiceAssignServices))
                {
                    List<string> notPatys = new List<string>();
                    List<string> priceErrors = new List<string>();
                    foreach (AssignServiceExtSDO assign in invoiceAssignServices)
                    {
                        if (assign.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            || assign.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai dich vu assigne them cho hoa don co doi tuong thanh toan la BHYT/VienPhi");
                        }
                        HIS_BILL_GOODS goods = new HIS_BILL_GOODS();
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == assign.ServiceId);
                        if (service == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc Service theo id: " + assign.ServiceId);
                        }
                        V_HIS_SERVICE_PATY paty = MOS.ServicePaty.ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, wp.BranchId, null, null, null, assign.IntructionTime, treatment.IN_TIME, assign.ServiceId, assign.PatientTypeId, null);
                        if (paty == null)
                        {
                            notPatys.Add(service.SERVICE_NAME);
                            continue;
                        }

                        if (paty.PRICE != assign.Price || paty.VAT_RATIO != assign.VatRatio)
                        {
                            priceErrors.Add(service.SERVICE_NAME);
                            continue;
                        }

                        goods.AMOUNT = assign.Amount;
                        goods.GOODS_NAME = service.SERVICE_NAME;
                        goods.GOODS_UNIT_NAME = service.SERVICE_UNIT_NAME;
                        goods.PRICE = paty.PRICE;
                        goods.VAT_RATIO = paty.VAT_RATIO;
                        hisBillGoods.Add(goods);
                    }

                    if (IsNotNullOrEmpty(notPatys))
                    {
                        string name = String.Join(",", notPatys);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPharmacyPay_CacDichVuSauKhongCoChinhSachGia, name);
                        return false;
                    }
                    if (IsNotNullOrEmpty(priceErrors))
                    {
                        string name = String.Join(",", priceErrors);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPharmacyPay_CacDichVuSauKhongCoGiaKhongHopLe, name);
                        return false;
                    }
                }

                invoiceBillGoods = hisBillGoods;
                invoiceSereServBills = newBills;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }


        internal bool IsValidRecieptSereServPrice(List<HIS_SERE_SERV> sereServs, List<SereServTranSDO> recieptSSTranSDOs, List<AssignServiceExtSDO> recieptAssignServices, WorkPlaceSDO wp, HIS_TREATMENT treatment, ref List<HIS_SERE_SERV_BILL> recieptSereServBills, ref List<HIS_BILL_GOODS> recieptBillGoods)
        {
            try
            {
                List<HIS_SERE_SERV_BILL> newBills = new List<HIS_SERE_SERV_BILL>();
                if (IsNotNullOrEmpty(recieptSSTranSDOs) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = recieptSSTranSDOs.Select(o => o.SereServId).Distinct().ToList();

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_BILL> existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);

                    //Thuc hien kiem tra xem trong d/s cac sere_serv thanh toan, co dich vu nao co tong so tien
                    //thanh toan vuot qua so tien BN can thanh toan ko
                    List<string> serviceNames = new List<string>();
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        if (recieptSSTranSDOs.Exists(e => e.SereServId == s.ID))
                        {
                            HIS_MEDICINE_TYPE medicineType = null;
                            if (s.MEDICINE_ID.HasValue)
                            {
                                medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.SERVICE_ID == s.SERVICE_ID);
                            }
                            if ((medicineType != null && medicineType.IS_VACCINE == Constant.IS_TRUE)
                               || !(s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("Ton tai dich vu bien lai la vacxin hoac co doi tuong thanh toan khac BHYT/VienPhi" + LogUtil.TraceData("SereServ", s));
                            }
                            decimal totalBill = IsNotNullOrEmpty(existsBills) ? existsBills.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.PRICE) : 0;
                            decimal payAmount = recieptSSTranSDOs.Where(o => o.SereServId == s.ID).Sum(o => o.Price);
                            totalBill += payAmount;

                            //Luu y: check lech tien voi "Constant.PRICE_DIFFERENCE", de tranh truong hop lam tron 
                            //(4 so sau phan thap phan) 
                            if (totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value > Constant.PRICE_DIFFERENCE)
                            {
                                serviceNames.Add(s.TDL_SERVICE_NAME);
                            }
                            else
                            {
                                HIS_SERE_SERV_BILL ssBill = new HIS_SERE_SERV_BILL();
                                ssBill.SERE_SERV_ID = s.ID;
                                ssBill.PRICE = payAmount;
                                HisSereServBillUtil.SetTdl(ssBill, s);
                                newBills.Add(ssBill);
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(serviceNames))
                    {
                        string nameStr = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_VuotQuaSoTienCanThanhToan, nameStr);
                        return false;
                    }
                }

                List<HIS_BILL_GOODS> hisBillGoods = new List<HIS_BILL_GOODS>();

                if (IsNotNullOrEmpty(recieptAssignServices))
                {
                    List<string> notPatys = new List<string>();
                    List<string> priceErrors = new List<string>();
                    foreach (AssignServiceExtSDO assign in recieptAssignServices)
                    {
                        if (!(assign.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || assign.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai dich vu assigne them cho bien lai co doi tuong thanh toan khong la BHYT/VienPhi");
                        }

                        HIS_BILL_GOODS goods = new HIS_BILL_GOODS();
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == assign.ServiceId);
                        if (service == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc Service theo id: " + assign.ServiceId);
                        }
                        V_HIS_SERVICE_PATY paty = MOS.ServicePaty.ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, wp.BranchId, null, null, null, assign.IntructionTime, treatment.IN_TIME, assign.ServiceId, assign.PatientTypeId, null);
                        if (paty == null)
                        {
                            notPatys.Add(service.SERVICE_NAME);
                            continue;
                        }

                        if (paty.PRICE != assign.Price || paty.VAT_RATIO != assign.VatRatio)
                        {
                            priceErrors.Add(service.SERVICE_NAME);
                            continue;
                        }

                        goods.AMOUNT = assign.Amount;
                        goods.GOODS_NAME = service.SERVICE_NAME;
                        goods.GOODS_UNIT_NAME = service.SERVICE_UNIT_NAME;
                        goods.PRICE = paty.PRICE;
                        goods.VAT_RATIO = paty.VAT_RATIO;
                        hisBillGoods.Add(goods);
                    }

                    if (IsNotNullOrEmpty(notPatys))
                    {
                        string name = String.Join(",", notPatys);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPharmacyPay_CacDichVuSauKhongCoChinhSachGia, name);
                        return false;
                    }
                    if (IsNotNullOrEmpty(priceErrors))
                    {
                        string name = String.Join(",", priceErrors);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPharmacyPay_CacDichVuSauKhongCoGiaKhongHopLe, name);
                        return false;
                    }
                }

                recieptBillGoods = hisBillGoods;
                recieptSereServBills = newBills;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool CheckDuplicateExpMest(PharmacyCashierSDO sdo, HIS_SERVICE_REQ prescription)
        {
            bool valid = true;
            try
            {
                if (prescription != null && (IsNotNullOrEmpty(sdo.Medicines) || IsNotNullOrEmpty(sdo.Materials)))
                {
                    HisExpMestFilterQuery expFilter = new HisExpMestFilterQuery();
                    expFilter.PRESCRIPTION_ID = prescription.ID;
                    List<HIS_EXP_MEST> exists = new HisExpMestGet().Get(expFilter);
                    if (IsNotNullOrEmpty(exists))
                    {
                        string codes = String.Join(",", exists.Select(s => s.EXP_MEST_CODE).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPharmacyCashier_DonThuocDaCoPhieuXuatBan, codes);
                        return false;
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
}
