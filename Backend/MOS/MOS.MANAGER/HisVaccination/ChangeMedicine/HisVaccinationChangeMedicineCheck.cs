using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccination.ChangeMedicine
{
	/// <summary>
	/// Check doi thong tin lo vaccin
	/// </summary>
	partial class HisVaccinationChangeMedicineCheck : BusinessBase
	{
		internal HisVaccinationChangeMedicineCheck()
			: base()
		{
		}

		internal HisVaccinationChangeMedicineCheck(CommonParam param)
			: base(param)
		{
		}

		internal bool IsValidData(HisVaccinationChangeMedicineSDO data)
		{
			try
			{
				if (data == null)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("data null");
					return false;
				}
				if (data.WorkingRoomId <= 0)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("thieu data.WorkingRoomId");
					return false;
				}
				if (data.ExpMestMedicineId <= 0)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("thieu data.ExpMestMedicineId");
					return false;
				}
				if (data.NewMedicineId <= 0)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("thieu data.MedicineId");
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}
			return false;
		}

		internal bool IsNotApproval(HIS_EXP_MEST_MEDICINE expMestMedicine)
		{
            bool result = true;
			try
			{
				if (expMestMedicine.IS_EXPORT == Constant.IS_TRUE)
				{
					MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccination_VaccinDaThucXuat);
					result = false;
				}

				if (expMestMedicine.APPROVAL_TIME.HasValue)
				{
					MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccination_VaccinDaDuocDuyet);
                    result = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
                result = false;
			}
			return result;
		}

		/// <summary>
		/// Kiem tra xem lo vaccin moi co cung gia voi lo vaccin cu ko.
		/// </summary>
		/// <param name="newMedicineId"></param>
		/// <param name="expMestMedicine"></param>
		/// <returns></returns>
		internal bool IsTheSamePrice(long newMedicineId, HIS_EXP_MEST_MEDICINE expMestMedicine, ref HIS_MEDICINE newMedicine, ref HIS_MEDICINE oldMedicine)
		{
			try
			{
				newMedicine = new HisMedicineGet().GetById(newMedicineId);
                oldMedicine = new HisMedicineGet().GetById(expMestMedicine.MEDICINE_ID.Value);

				if (expMestMedicine != null && newMedicine != null)
				{
					decimal newPrice = 0;
					decimal newVatRatio = 0;

					if (newMedicine.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
					{
						newPrice = newMedicine.IMP_PRICE;
						newVatRatio = newMedicine.IMP_VAT_RATIO;
					}
					else
					{
						HisMedicinePatyFilterQuery filter = new HisMedicinePatyFilterQuery();
						filter.MEDICINE_ID = newMedicineId;
						filter.PATIENT_TYPE_ID = expMestMedicine.PATIENT_TYPE_ID;
						filter.IS_ACTIVE = Constant.IS_TRUE;
						List<HIS_MEDICINE_PATY> medicinePaties = new HisMedicinePatyGet().Get(filter);
						if (!IsNotNullOrEmpty(medicinePaties))
						{
							HIS_PATIENT_TYPE pt = HisPatientTypeCFG.DATA.Where(o => o.ID == expMestMedicine.PATIENT_TYPE_ID).FirstOrDefault();
							string packageNumber = newMedicine.PACKAGE_NUMBER != null ? newMedicine.PACKAGE_NUMBER : "";
							MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccination_LoVaccinKhongCoChinhSachGiaBanTuongUngVoiDoiTuong, packageNumber, pt.PATIENT_TYPE_NAME);
							return false;
						}
						HIS_MEDICINE_PATY medicinePaty = medicinePaties.OrderByDescending(o => o.ID).FirstOrDefault();
						newPrice = medicinePaty.EXP_PRICE;
						newVatRatio = medicinePaty.EXP_VAT_RATIO;
					}

					if (newPrice != expMestMedicine.PRICE || newVatRatio != expMestMedicine.VAT_RATIO)
					{
						MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccination_LoVaccinMoiCoGiaHoacVatKhacVoiLoVaccinCu);
						return false;
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}
			return false;
		}

        internal bool IsWorkingAtRoom(long exeRoomId,long reqRoomId, long workingRoomId)
        {
            if (exeRoomId != workingRoomId && reqRoomId != workingRoomId)
            {
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == exeRoomId).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong, room.ROOM_NAME);
                return false;
            }
            return true;
        }
	}
}
