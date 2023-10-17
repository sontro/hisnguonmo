using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine
{
	partial class HisExpMestMedicineUpdateCommonInfo : BusinessBase
	{
		internal HisExpMestMedicineUpdateCommonInfo()
			: base()
		{

		}

		internal HisExpMestMedicineUpdateCommonInfo(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Run(HIS_EXP_MEST_MEDICINE data, ref HIS_EXP_MEST_MEDICINE resultData)
		{
			bool result = false;
			try
			{
				HIS_EXP_MEST_MEDICINE old = null;
				HIS_SERVICE_REQ serviceReq = null;
				V_HIS_EXP_MEST_MEDICINE viewExpMestMedicine = null;
				
				if (this.IsAllow(data.ID, ref old, ref serviceReq, ref viewExpMestMedicine))
				{
					Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
					HIS_EXP_MEST_MEDICINE toUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
					toUpdate.TUTORIAL = data.TUTORIAL;
					toUpdate.SPEED = data.SPEED;
                    toUpdate.USE_TIME_TO = data.USE_TIME_TO;
                    toUpdate.PREVIOUS_USING_COUNT = data.PREVIOUS_USING_COUNT;

					if (new HisExpMestMedicineUpdate(param).Update(toUpdate, old))
					{
						result = true;
						resultData = toUpdate;

                        DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_DATE).Value;
                        DateTime? oldUseTimeTo = old.USE_TIME_TO.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(old.USE_TIME_TO.Value) : null;
                        DateTime? newUseTimeTo = data.USE_TIME_TO.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.USE_TIME_TO.Value) : null;
                        string oldNumOfDay = oldUseTimeTo.HasValue ? (oldUseTimeTo.Value - time).Days.ToString() : "";
                        string newNumOfDay = newUseTimeTo.HasValue ? (oldUseTimeTo.Value - time).Days.ToString() : "";
                        
						new EventLogGenerator(EventLog.Enum.HisExpMestMedicine_SuaThongTinChung, viewExpMestMedicine.MEDICINE_TYPE_NAME, viewExpMestMedicine.MEDICINE_TYPE_CODE, old.TUTORIAL, toUpdate.TUTORIAL, old.SPEED, toUpdate.SPEED, oldNumOfDay, newNumOfDay).TreatmentCode(serviceReq.TDL_TREATMENT_CODE).ServiceReqCode(serviceReq.SERVICE_REQ_CODE).ExpMestCode(viewExpMestMedicine.EXP_MEST_CODE).Run();
					}
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

		private bool IsAllow(long id, ref HIS_EXP_MEST_MEDICINE old, ref HIS_SERVICE_REQ serviceReq, ref V_HIS_EXP_MEST_MEDICINE viewExpMestMedicine)
		{
			try
			{
				old = new HisExpMestMedicineGet().GetById(id);
				if (old == null)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
					LogSystem.Warn("Id ko ton tai");
					return false;
				}

				if (!old.TDL_SERVICE_REQ_ID.HasValue)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("Ko phai don thuoc. Ko co TDL_SERVICE_REQ_ID");
					return false;
				}

				serviceReq = new HisServiceReqGet().GetById(old.TDL_SERVICE_REQ_ID.Value);

				if (serviceReq == null)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("Ko ton tai serviceReq");
					return false;
				}

				viewExpMestMedicine = new HisExpMestMedicineGet().GetViewById(id);
				if (viewExpMestMedicine == null)
				{
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
					LogSystem.Warn("view V_HIS_EXP_MEST_MEDICINE null");
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
	}
}
