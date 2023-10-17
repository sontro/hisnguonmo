using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Process
{
	class HisVaccinationProcess: BusinessBase
	{
		private VaccinationProcessor vaccinationProcessor;
		private MedicineProcessor medicineProcessor;

		internal HisVaccinationProcess()
			: base()
		{
			this.Init();
		}

		internal HisVaccinationProcess(CommonParam param)
			: base(param)
		{
			this.Init();
		}

		private void Init()
		{
			this.vaccinationProcessor = new VaccinationProcessor(param);
			this.medicineProcessor = new MedicineProcessor(param);
		}

		internal bool Run(HisVaccinationProcessSDO data, ref HIS_VACCINATION resultData)
		{
			bool result = false;
			try
			{
				bool valid = true;

				HisVaccinationCheck vaccinationChecker = new HisVaccinationCheck(param);
				HisVaccinationProcessCheck checker = new HisVaccinationProcessCheck(param);

				HIS_VACCINATION vaccination = null;
				List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
				WorkPlaceSDO workPlace = null;
				valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
				valid = valid && vaccinationChecker.VerifyId(data.VaccinationId, ref vaccination);
				valid = valid && vaccinationChecker.IsNotFinish(vaccination); 
				valid = valid && this.IsWorkingAtRoom(vaccination.EXECUTE_ROOM_ID, data.WorkingRoomId);
				valid = valid && checker.IsValidVaccination(vaccination, data, ref expMestMedicines);

				if (valid)
				{
					if (!this.vaccinationProcessor.Run(data, vaccination, expMestMedicines))
					{
						throw new Exception("Rollback du lieu");
					}
					if (!this.medicineProcessor.Run(data.VaccinationInjections, expMestMedicines))
					{
						throw new Exception("Rollback du lieu");
					}
					result = true;
                    resultData = vaccination;
				}
			}
			catch (Exception ex)
			{
				this.Rollback();
				LogSystem.Error(ex);
				param.HasException = true;
				result = false;
			}
			return result;
		}

		private void Rollback()
		{
			this.vaccinationProcessor.Rollback();
            this.medicineProcessor.Rollback();
		}
	}
}
