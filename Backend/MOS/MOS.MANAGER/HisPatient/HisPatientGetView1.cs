using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatient
{
	partial class HisPatientGet : GetBase
	{
		internal List<V_HIS_PATIENT_1> GetView1(HisPatientView1FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisPatientDAO.GetView1(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}
	
		internal V_HIS_PATIENT_1 GetView1ById(long id)
		{
			try
			{
				return GetView1ById(id, new HisPatientView1FilterQuery());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_PATIENT_1 GetView1ById(long id, HisPatientView1FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisPatientDAO.GetView1ById(id, filter.Query());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_PATIENT_1 GetView1ByCode(string code)
		{
			try
			{
				return GetView1ByCode(code, new HisPatientView1FilterQuery());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_PATIENT_1 GetView1ByCode(string code, HisPatientView1FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisPatientDAO.GetView1ByCode(code, filter.Query());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}
    }
}
