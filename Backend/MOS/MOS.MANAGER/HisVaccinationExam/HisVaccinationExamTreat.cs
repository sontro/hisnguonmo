using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisVaexVaer;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationExam
{
    /// <summary>
    /// Xu ly kham
    /// </summary>
	partial class HisVaccinationExamTreat : BusinessBase
	{
		private HisVaccinationExamUpdate hisVaccinationExamUpdate;
        private HisVaexVaerCreate hisVaexVaerCreate;
        private HisVaexVaerTruncate hisVaexVaerTruncate;
        private HisVaexVaerUpdate hisVaexVaerUpdate;
        private HisDhstCreate hisDhstCreate;
        private HisDhstUpdate hisDhstUpdate;

		internal HisVaccinationExamTreat()
			: base()
		{
            this.Init();
		}

        internal HisVaccinationExamTreat(CommonParam param)
            : base(param)
		{
            this.Init();
		}

        private void Init()
        {
            this.hisVaccinationExamUpdate = new HisVaccinationExamUpdate(param);
            this.hisVaexVaerCreate = new HisVaexVaerCreate(param);
            this.hisVaexVaerTruncate = new HisVaexVaerTruncate(param);
            this.hisVaexVaerUpdate = new HisVaexVaerUpdate(param);
            this.hisDhstCreate = new HisDhstCreate(param);
            this.hisDhstUpdate = new HisDhstUpdate(param);
        }

		internal bool Run(HisVaccinationExamTreatSDO data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
				HIS_VACCINATION_EXAM raw = null;
                WorkPlaceSDO workPlace = null;
				valid = valid && checker.VerifyId(data.VaccinationExamId, ref raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtRoom(raw.EXECUTE_ROOM_ID, data.WorkingRoomId);
				if (valid)
				{
                    this.ProcessVaccinationExam(data, raw);
                    this.ProcessVaexVaer(data);
                    this.ProcessDhst(data, workPlace);
					result = true;
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

        private void ProcessDhst(HisVaccinationExamTreatSDO data, WorkPlaceSDO workPlace)
        {
            if (data != null && data.Dhst != null)
            {
                if (data.Dhst.ID <= 0)
                {
                    data.Dhst.VACCINATION_EXAM_ID = data.VaccinationExamId;
                    data.Dhst.EXECUTE_ROOM_ID = data.WorkingRoomId;
                    if (!data.Dhst.EXECUTE_TIME.HasValue)
                    {
                        data.Dhst.EXECUTE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    }
                    
                    data.Dhst.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                    data.Dhst.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                    data.Dhst.EXECUTE_USERNAME = data.ExecuteUsername;

                    if (!this.hisDhstCreate.Create(data.Dhst))
                    {
                        throw new Exception("Tao thong tin DHST that bai");
                    }
                }
                else
                {
                    data.Dhst.VACCINATION_EXAM_ID = data.VaccinationExamId;
                    data.Dhst.EXECUTE_ROOM_ID = data.WorkingRoomId;
                    if (!data.Dhst.EXECUTE_TIME.HasValue)
                    {
                        data.Dhst.EXECUTE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    }
                    data.Dhst.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                    data.Dhst.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                    data.Dhst.EXECUTE_USERNAME = data.ExecuteUsername;

                    if (!this.hisDhstUpdate.Update(data.Dhst))
                    {
                        throw new Exception("Tao thong tin DHST that bai");
                    }
                }
            }
        }

		private void ProcessVaccinationExam(HisVaccinationExamTreatSDO data, HIS_VACCINATION_EXAM raw)
		{
            if (data != null && raw != null)
            {
                Mapper.CreateMap<HIS_VACCINATION_EXAM, HIS_VACCINATION_EXAM>();
                HIS_VACCINATION_EXAM before = Mapper.Map<HIS_VACCINATION_EXAM>(raw);
                raw.EXECUTE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                raw.CONCLUDE = data.Conclude.HasValue ? (long?)data.Conclude : null;
                raw.NOTE = data.Note;
                raw.EXECUTE_USERNAME = data.ExecuteUsername;
                raw.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                raw.PT_ALLERGIC_HISTORY = data.PtAllergicHistory;
                raw.PT_PATHOLOGICAL_HISTORY = data.PtPathologicalHistory;
                raw.VACCINATION_EXAM_STT_ID = data.VaccinationExamSttId;
                raw.IS_TEST_HBSAG = data.IsTestHBSAG;
                raw.IS_POSITIVE_RESULT = data.IsPositiveResult;
                raw.IS_SPECIALIST_EXAM = data.IsSpecialistExam;
                raw.SPECIALIST_DEPARTMENT_ID = data.SpecialistDepartmentId;
                raw.SPECIALIST_REASON = data.SpecialistReason;
                raw.SPECIALIST_RESULT = data.SpecialistResult;
                raw.SPECIALIST_CONCLUDE = data.SpecialistConclude;
                raw.RABIES_NUMBER_OF_DAYS = data.RabiesNumberOfDays;
                raw.RABIES_ANIMAL_DOG = data.IsRabiesAnimalDog.HasValue && data.IsRabiesAnimalDog.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_ANIMAL_CAT = data.IsRabiesAnimalCat.HasValue && data.IsRabiesAnimalCat.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_ANIMAL_BAT = data.IsRabiesAnimalBat.HasValue && data.IsRabiesAnimalBat.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_ANIMAL_OTHER = data.IsRabiesAnimalOther.HasValue && data.IsRabiesAnimalOther.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_LOCATION_HEAD = data.IsRabiesWoundLocationHead.HasValue && data.IsRabiesWoundLocationHead.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_LOCATION_FACE = data.IsRabiesWoundLocationFace.HasValue && data.IsRabiesWoundLocationFace.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_LOCATION_NECK = data.IsRabiesWoundLocationNeck.HasValue && data.IsRabiesWoundLocationNeck.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_LOCATION_HAND = data.IsRabiesWoundLocationHand.HasValue && data.IsRabiesWoundLocationHand.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_LOCATION_FOOT = data.IsRabiesWoundLocationFoot.HasValue && data.IsRabiesWoundLocationFoot.Value ? (short?)Constant.IS_TRUE : null;
                raw.RABIES_WOUND_RANK = data.RabiesWoundRank;
                raw.RABIES_ANIMAL_STATUS = data.RabiesWoundStatus;
                if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_VACCINATION_EXAM>(before, raw)
                    && !this.hisVaccinationExamUpdate.Update(raw, before))
                {
                    throw new Exception("Cap nhat thong tin HIS_VACCINATION_EXAM that bai");
                }
            }
		}

        private void ProcessVaexVaer(HisVaccinationExamTreatSDO data)
        {
            if (data != null)
            {
                List<HIS_VAEX_VAER> vaexVaers = new HisVaexVaerGet().GetByVaccinationExamId(data.VaccinationExamId);
                List<HIS_VAEX_VAER> toDeletes = IsNotNullOrEmpty(vaexVaers) ? vaexVaers.Where(o => data.VaexVaerInfos == null || !data.VaexVaerInfos.Select(s => s.VaccExamResultId).Contains(o.VACC_EXAM_RESULT_ID)).ToList() : null;
                List<HIS_VAEX_VAER> toInserts = IsNotNullOrEmpty(data.VaexVaerInfos) ?
                    data.VaexVaerInfos
                    .Where(o => vaexVaers == null || !vaexVaers.Exists(t => t.VACC_EXAM_RESULT_ID == o.VaccExamResultId))
                    .Select(t => new HIS_VAEX_VAER
                    {
                        VACC_EXAM_RESULT_ID = t.VaccExamResultId,
                        VACCINATION_EXAM_ID = data.VaccinationExamId,
                        NOTE = t.Note
                    })
                    .ToList() : null;

                List<HIS_VAEX_VAER> toUpdates = new List<HIS_VAEX_VAER>();
                if (IsNotNullOrEmpty(data.VaexVaerInfos) && IsNotNullOrEmpty(vaexVaers))
                {
                    foreach (var vv in data.VaexVaerInfos)
                    {
                        HIS_VAEX_VAER t = vaexVaers.Where(o => o.VACC_EXAM_RESULT_ID == vv.VaccExamResultId).FirstOrDefault();
                        if (t != null)
                        {
                            t.NOTE = vv.Note;
                            toUpdates.Add(t);
                        }
                    }
                }

                if (IsNotNullOrEmpty(toDeletes) && !this.hisVaexVaerTruncate.TruncateList(toDeletes))
                {
                    throw new Exception("Xoa du lieu HIS_VAEX_VAER that bai");
                }
                if (IsNotNullOrEmpty(toUpdates) && !this.hisVaexVaerUpdate.UpdateList(toUpdates))
                {
                    throw new Exception("Cap nhat du lieu HIS_VAEX_VAER that bai");
                }
                if (IsNotNullOrEmpty(toInserts) && !this.hisVaexVaerCreate.CreateList(toInserts))
                {
                    throw new Exception("Them du lieu HIS_VAEX_VAER that bai");
                }
            }
        }

        private void Rollback()
        {
            this.hisDhstCreate.RollbackData();
            this.hisDhstUpdate.RollbackData();
            this.hisVaexVaerCreate.RollbackData();
            this.hisVaexVaerUpdate.RollbackData();
            this.hisVaccinationExamUpdate.RollbackData();
        }
	}
}
