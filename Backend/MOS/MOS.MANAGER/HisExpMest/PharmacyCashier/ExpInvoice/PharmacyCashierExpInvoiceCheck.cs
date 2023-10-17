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
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMaterial;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.ExpInvoice
{
	public class PharmacyCashierExpInvoiceCheck: BusinessBase
	{
		internal PharmacyCashierExpInvoiceCheck()
			: base()
		{
		}

		internal PharmacyCashierExpInvoiceCheck(CommonParam param)
			: base(param)
		{

		}

		internal bool IsAllowing(WorkPlaceSDO workPlaceSdo, PharmacyCashierExpInvoiceSDO sdo, ref V_HIS_CASHIER_ROOM cashierRoom)
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

		internal bool IsValidExpMest(PharmacyCashierExpInvoiceSDO sdo, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
		{
			bool valid = true;
			try
			{
                expMest = new HisExpMestGet().GetById(sdo.ExpMestId);
                if (expMest == null)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.ExpMestId ko hop le");
					return false;
				}

                if (expMest.BILL_ID.HasValue)
				{
					MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaXuatHoaDon);
					return false;
				}

                expMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                expMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);

                if (!IsNotNullOrEmpty(expMestMaterials) && !IsNotNullOrEmpty(expMestMedicines))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongCoThongTinThuocVatTu, expMest.EXP_MEST_CODE);
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

		internal bool IsValidAccountBook(PharmacyCashierExpInvoiceSDO sdo, ref V_HIS_ACCOUNT_BOOK invoiceBook)
		{
			bool valid = true;
			try
			{
				V_HIS_ACCOUNT_BOOK ib = new HisAccountBookGet().GetViewById(sdo.InvoiceAccountBookId);
				if (ib == null || ib.BILL_TYPE_ID != HisAccountBookCFG.BILL_TYPE_ID__INVOICE)
				{
					MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaChonSoHoaDon);
					return false;
				}
				this.IsUnlockAndValidNumOrder(sdo.InvoiceNumOrder, ib);

				invoiceBook = ib;

				return true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				valid = false;
			}
			return valid;
		}

		private bool IsUnlockAndValidNumOrder(long? numOrder, V_HIS_ACCOUNT_BOOK accountBook)
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
					if (numOrder < accountBook.FROM_NUM_ORDER || numOrder > max)
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
					if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - 1))
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
	}
}
