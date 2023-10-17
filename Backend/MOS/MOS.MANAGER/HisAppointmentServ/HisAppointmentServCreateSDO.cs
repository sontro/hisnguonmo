using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAppointmentServ
{
	class HisAppointmentServCreateSDO : BusinessBase
	{
		private HisAppointmentServCreate hisAppointmentServCreate;

		internal HisAppointmentServCreateSDO()
			: base()
		{
			this.hisAppointmentServCreate = new HisAppointmentServCreate(param);
		}

		internal HisAppointmentServCreateSDO(CommonParam paramCreate)
			: base(paramCreate)
		{
			this.hisAppointmentServCreate = new HisAppointmentServCreate(param);
		}

		internal bool Run(HisAppointmentServSDO data, ref List<HIS_APPOINTMENT_SERV> resultData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(data);
				valid = valid && IsNotNullOrEmpty(data.AppointmentServs);
				HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
				HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
				HIS_TREATMENT treatment = null;
				valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                //Ko can ho so phai ket thuc (thuc te, voi BN noi tru, vien co nhu cau chi dinh hen kham truoc khi ra vien)
				//valid = valid && treatChecker.IsPause(treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
				//valid = valid && treatChecker.IsTreatmentEndTypeAppointment(treatment);
				if (valid)
				{
					data.AppointmentServs.ForEach(o => 
						{
							o.TREATMENT_ID = treatment.ID;
							o.TDL_PATIENT_ID = treatment.PATIENT_ID;
							o.TDL_APPOINTMENT_TIME = treatment.APPOINTMENT_TIME;
						}
					);
					foreach (var item in data.AppointmentServs)
					{
						valid = valid && checker.VerifyRequireField(item);
					}
					if (valid)
					{
						if (!this.hisAppointmentServCreate.CreateList(data.AppointmentServs))
						{
							throw new Exception("hisAppointmentServCreate . ket thuc nghiep vu");
						}
						resultData = data.AppointmentServs;
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
				result = false;
			}
			return result;
		}
	}
}
