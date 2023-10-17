using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Process
{
	class HisVaccinationProcessCheck: BusinessBase
	{
		internal HisVaccinationProcessCheck()
			: base()
		{
		}

		internal HisVaccinationProcessCheck(CommonParam param)
			: base(param)
		{
		}

		internal bool IsValidVaccination(HIS_VACCINATION vaccination, HisVaccinationProcessSDO data, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
		{
			try
			{
				List<long> expMestMedicineIds = data.VaccinationInjections != null ? data.VaccinationInjections.Select(o => o.ExpMestMedicineId).ToList() : null;
				List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByIds(expMestMedicineIds);

				if (!IsNotNullOrEmpty(medicines))
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai expMestMedicineIds ko hop le");
                    return false;
				}

				if (medicines.Exists(o => o.TDL_VACCINATION_ID != vaccination.ID))
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai HIS_EXP_MEST_MEDICINE ko thuoc vaccination tuong ung");
                    return false;
				}

				expMestMedicines = medicines;
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
