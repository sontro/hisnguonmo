using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal HisServiceReqGet()
            : base()
        {

        }

        internal HisServiceReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_REQ> Get(HisServiceReqFilterQuery filter)
        {
            try
            {
                List<HIS_SERVICE_REQ> result = DAOWorker.HisServiceReqDAO.Get(filter.Query(), param);
                if (IsNotNullOrEmpty(result))
                {
                    result.ForEach(o =>
                    {
                        o.HIS_SERVICE_REQ1 = null;
                        o.HIS_SERVICE_REQ2 = null;
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ GetById(long id, HisServiceReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ GetByCode(string code)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetByCode(code, new HisServiceReqFilterQuery().Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ GetLastByCodeEndWith(string code)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_CODE__ENDS_WITH = code;
                filter.ORDER_FIELD = "ID";
                filter.ORDER_DIRECTION = "DESC";
                List<HIS_SERVICE_REQ> data = this.Get(filter);
                return IsNotNullOrEmpty(data) ? data[0] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ GetByCode(string code, HisServiceReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.IDs = ids;
                    return this.Get(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByDepartmentId(long departmentId)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.REQUEST_DEPARTMENT_ID__OR__EXECUTE_DEPARTMENT_ID = departmentId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByRoomId(long roomId)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID = roomId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByPatientId(long id)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TDL_PATIENT_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByServiceReqSttId(long id)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_STT_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByServiceReqTypeId(long id)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_TYPE_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByTreatmentId(long id)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByTreatmentIds(List<long> ids)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_IDs = ids;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetBySampleRoomId(long sampleRoomId)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SAMPLE_ROOM_ID = sampleRoomId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisServiceReqViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ GetViewById(long id, HisServiceReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ> GetView(HisServiceReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ> GetViewByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqViewFilterQuery filter = new HisServiceReqViewFilterQuery();
                    filter.IDs = ids;
                    return this.GetView(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ GetViewFromTable(HIS_SERVICE_REQ serviceReq)
        {
            List<V_HIS_SERVICE_REQ> vServiceReqs = this.GetViewFromTable(new List<HIS_SERVICE_REQ>() { serviceReq });
            return IsNotNullOrEmpty(vServiceReqs) ? vServiceReqs[0] : null;
        }

        internal List<V_HIS_SERVICE_REQ> GetViewFromTable(List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                List<V_HIS_SERVICE_REQ> vServiceReqs =
                    (from sr in serviceReqs
                     join srt in HisServiceReqTypeCFG.DATA on sr.SERVICE_REQ_TYPE_ID equals srt.ID
                     join srs in HisServiceReqSttCFG.DATA on sr.SERVICE_REQ_STT_ID equals srs.ID
                     join eroom in HisRoomCFG.DATA on sr.EXECUTE_ROOM_ID equals eroom.ID
                     join rroom in HisRoomCFG.DATA on sr.REQUEST_ROOM_ID equals rroom.ID
                     join edep in HisDepartmentCFG.DATA on sr.EXECUTE_DEPARTMENT_ID equals edep.ID
                     join rdep in HisDepartmentCFG.DATA on sr.REQUEST_DEPARTMENT_ID equals rdep.ID
                     select new V_HIS_SERVICE_REQ
                     {
                         ID = sr.ID,
                         CREATE_TIME = sr.CREATE_TIME,
                         MODIFY_TIME = sr.MODIFY_TIME,
                         CREATOR = sr.CREATOR,
                         MODIFIER = sr.MODIFIER,
                         APP_CREATOR = sr.APP_CREATOR,
                         APP_MODIFIER = sr.APP_MODIFIER,
                         IS_ACTIVE = sr.IS_ACTIVE,
                         IS_DELETE = sr.IS_DELETE,
                         GROUP_CODE = sr.GROUP_CODE,
                         SERVICE_REQ_CODE = sr.SERVICE_REQ_CODE,
                         SERVICE_REQ_TYPE_ID = sr.SERVICE_REQ_TYPE_ID,
                         SERVICE_REQ_STT_ID = sr.SERVICE_REQ_STT_ID,
                         TREATMENT_ID = sr.TREATMENT_ID,
                         INTRUCTION_TIME = sr.INTRUCTION_TIME,
                         INTRUCTION_DATE = sr.INTRUCTION_DATE,
                         REQUEST_ROOM_ID = sr.REQUEST_ROOM_ID,
                         REQUEST_DEPARTMENT_ID = sr.REQUEST_DEPARTMENT_ID,
                         REQUEST_LOGINNAME = sr.REQUEST_LOGINNAME,
                         REQUEST_USERNAME = sr.REQUEST_USERNAME,
                         EXECUTE_ROOM_ID = sr.EXECUTE_ROOM_ID,
                         EXECUTE_DEPARTMENT_ID = sr.EXECUTE_DEPARTMENT_ID,
                         EXECUTE_LOGINNAME = sr.EXECUTE_LOGINNAME,
                         EXECUTE_USERNAME = sr.EXECUTE_USERNAME,
                         START_TIME = sr.START_TIME,
                         FINISH_TIME = sr.FINISH_TIME,
                         ICD_CODE = sr.ICD_CODE,
                         ICD_CAUSE_CODE = sr.ICD_CAUSE_CODE,
                         ICD_CAUSE_NAME = sr.ICD_CAUSE_NAME,
                         ICD_NAME = sr.ICD_NAME,
                         ICD_SUB_CODE = sr.ICD_SUB_CODE,
                         ICD_TEXT = sr.ICD_TEXT,
                         NUM_ORDER = sr.NUM_ORDER,
                         PRIORITY = sr.PRIORITY,
                         TRACKING_ID = sr.TRACKING_ID,
                         PARENT_ID = sr.PARENT_ID,
                         IS_WAIT_CHILD = sr.IS_WAIT_CHILD,
                         DHST_ID = sr.DHST_ID,
                         EXECUTE_GROUP_ID = sr.EXECUTE_GROUP_ID,
                         ASSIGN_REASON_ID = sr.ASSIGN_REASON_ID,
                         IS_NOT_REQUIRE_FEE = sr.IS_NOT_REQUIRE_FEE,
                         IS_NO_EXECUTE = sr.IS_NO_EXECUTE,
                         CALL_COUNT = sr.CALL_COUNT,
                         JSON_PRINT_ID = sr.JSON_PRINT_ID,
                         DESCRIPTION = sr.DESCRIPTION,
                         BARCODE = sr.BARCODE,
                         SAMPLE_ROOM_ID = sr.SAMPLE_ROOM_ID,
                         LIS_STT_ID = sr.LIS_STT_ID,
                         PAAN_POSITION_ID = sr.PAAN_POSITION_ID,
                         PAAN_LIQUID_ID = sr.PAAN_LIQUID_ID,
                         IS_EMERGENCY = sr.IS_EMERGENCY,
                         LIQUID_TIME = sr.LIQUID_TIME,
                         ECG_BEFORE = sr.ECG_BEFORE,
                         ECG_AFTER = sr.ECG_AFTER,
                         RESPIRATORY_BEFORE = sr.RESPIRATORY_BEFORE,
                         RESPIRATORY_AFTER = sr.RESPIRATORY_AFTER,
                         SYMPTOM_BEFORE = sr.SYMPTOM_BEFORE,
                         SYMPTOM_AFTER = sr.SYMPTOM_AFTER,
                         ADVISE = sr.ADVISE,
                         REHA_SUM_ID = sr.REHA_SUM_ID,
                         USE_TIME = sr.USE_TIME,
                         USE_TIME_TO = sr.USE_TIME_TO,
                         REMEDY_COUNT = sr.REMEDY_COUNT,
                         HOSPITALIZATION_REASON = sr.HOSPITALIZATION_REASON,
                         PATHOLOGICAL_PROCESS = sr.PATHOLOGICAL_PROCESS,
                         PATHOLOGICAL_HISTORY = sr.PATHOLOGICAL_HISTORY,
                         PATHOLOGICAL_HISTORY_FAMILY = sr.PATHOLOGICAL_HISTORY_FAMILY,
                         FULL_EXAM = sr.FULL_EXAM,
                         PART_EXAM = sr.PART_EXAM,
                         PART_EXAM_CIRCULATION = sr.PART_EXAM_CIRCULATION,
                         PART_EXAM_RESPIRATORY = sr.PART_EXAM_RESPIRATORY,
                         PART_EXAM_DIGESTION = sr.PART_EXAM_DIGESTION,
                         PART_EXAM_KIDNEY_UROLOGY = sr.PART_EXAM_KIDNEY_UROLOGY,
                         PART_EXAM_NEUROLOGICAL = sr.PART_EXAM_NEUROLOGICAL,
                         PART_EXAM_MUSCLE_BONE = sr.PART_EXAM_MUSCLE_BONE,
                         PART_EXAM_ENT = sr.PART_EXAM_ENT,
                         PART_EXAM_STOMATOLOGY = sr.PART_EXAM_STOMATOLOGY,
                         PART_EXAM_EYE = sr.PART_EXAM_EYE,
                         PART_EXAM_OEND = sr.PART_EXAM_OEND,
                         SICK_DAY = sr.SICK_DAY,
                         PART_EXAM_MENTAL = sr.PART_EXAM_MENTAL,
                         PART_EXAM_OBSTETRIC = sr.PART_EXAM_OBSTETRIC,
                         PART_EXAM_NUTRITION = sr.PART_EXAM_NUTRITION,
                         PART_EXAM_MOTION = sr.PART_EXAM_MOTION,
                         NEXT_TREATMENT_INSTRUCTION = sr.NEXT_TREATMENT_INSTRUCTION,
                         SUBCLINICAL = sr.SUBCLINICAL,
                         TREATMENT_INSTRUCTION = sr.TREATMENT_INSTRUCTION,
                         NOTE = sr.NOTE,
                         TDL_TREATMENT_CODE = sr.TDL_TREATMENT_CODE,
                         TDL_HEIN_CARD_NUMBER = sr.TDL_HEIN_CARD_NUMBER,
                         TDL_HEIN_MEDI_ORG_CODE = sr.TDL_HEIN_MEDI_ORG_CODE,
                         TDL_HEIN_MEDI_ORG_NAME = sr.TDL_HEIN_MEDI_ORG_NAME,
                         TDL_PATIENT_ID = sr.TDL_PATIENT_ID,
                         TDL_PATIENT_CODE = sr.TDL_PATIENT_CODE,
                         TDL_PATIENT_DOB = sr.TDL_PATIENT_DOB,
                         TDL_PATIENT_ADDRESS = sr.TDL_PATIENT_ADDRESS,
                         TDL_PATIENT_GENDER_ID = sr.TDL_PATIENT_GENDER_ID,
                         TDL_PATIENT_GENDER_NAME = sr.TDL_PATIENT_GENDER_NAME,
                         TDL_PATIENT_CAREER_NAME = sr.TDL_PATIENT_CAREER_NAME,
                         TDL_PATIENT_WORK_PLACE = sr.TDL_PATIENT_WORK_PLACE,
                         TDL_PATIENT_WORK_PLACE_NAME = sr.TDL_PATIENT_WORK_PLACE_NAME,
                         TDL_PATIENT_DISTRICT_CODE = sr.TDL_PATIENT_DISTRICT_CODE,
                         TDL_PATIENT_PROVINCE_CODE = sr.TDL_PATIENT_PROVINCE_CODE,
                         TDL_PATIENT_NAME = sr.TDL_PATIENT_NAME,

                         TREATMENT_CODE = sr.TDL_TREATMENT_CODE,
                         SERVICE_REQ_TYPE_CODE = srt.SERVICE_REQ_TYPE_CODE,
                         SERVICE_REQ_TYPE_NAME = srt.SERVICE_REQ_TYPE_NAME,
                         SERVICE_REQ_STT_CODE = srs.SERVICE_REQ_STT_CODE,
                         SERVICE_REQ_STT_NAME = srs.SERVICE_REQ_STT_NAME,

                         EXECUTE_ROOM_CODE = eroom.ROOM_CODE,
                         EXECUTE_ROOM_NAME = eroom.ROOM_NAME,
                         EXECUTE_ROOM_ADDRESS = eroom.ADDRESS,

                         REQUEST_ROOM_CODE = rroom.ROOM_CODE,
                         REQUEST_ROOM_NAME = rroom.ROOM_NAME,
                         REQUEST_ROOM_TYPE_CODE = rroom.ROOM_TYPE_CODE,
                         REQUEST_ROOM_TYPE_NAME = rroom.ROOM_TYPE_NAME,
                         REQUEST_ROOM_ADDRESS = rroom.ADDRESS,

                         EXECUTE_DEPARTMENT_CODE = edep.DEPARTMENT_CODE,
                         EXECUTE_DEPARTMENT_NAME = edep.DEPARTMENT_NAME,
                         EXECUTE_BHYT_CODE = edep.BHYT_CODE,
                         EXECUTE_PHONE = edep.PHONE,

                         REQUEST_DEPARTMENT_CODE = rdep.DEPARTMENT_CODE,
                         REQUEST_DEPARTMENT_NAME = rdep.DEPARTMENT_NAME,
                         REQUEST_BHYT_CODE = rdep.BHYT_CODE,
                         REQUEST_PHONE = rdep.PHONE,

                         HAS_CHILD = sr.HAS_CHILD,
                         IS_ACCEPTING_NO_EXECUTE = sr.IS_ACCEPTING_NO_EXECUTE,
                         IS_ANTIBIOTIC_RESISTANCE = sr.IS_ANTIBIOTIC_RESISTANCE,
                         IS_AUTO_FINISHED = sr.IS_AUTO_FINISHED,
                         IS_COLLECTED = sr.IS_COLLECTED,
                         IS_EXECUTE_KIDNEY_PRES = sr.IS_EXECUTE_KIDNEY_PRES,
                         IS_FIRST_OPTOMETRIST = sr.IS_FIRST_OPTOMETRIST,
                         IS_HOLD_ORDER = sr.IS_HOLD_ORDER,
                         IS_HOME_PRES = sr.IS_HOME_PRES,
                         IS_INFORM_RESULT_BY_SMS = sr.IS_INFORM_RESULT_BY_SMS,
                         IS_INTEGRATE_HIS_SENT = sr.IS_INTEGRATE_HIS_SENT,
                         IS_KIDNEY = sr.IS_KIDNEY,
                         IS_MAIN_EXAM = sr.IS_MAIN_EXAM,
                         IS_NOT_IN_DEBT = sr.IS_NOT_IN_DEBT,
                         IS_NOT_SHOW_MATERIAL_TRACKING = sr.IS_NOT_SHOW_MATERIAL_TRACKING,
                         IS_NOT_SHOW_MEDICINE_TRACKING = sr.IS_NOT_SHOW_MEDICINE_TRACKING,
                         IS_NOT_SHOW_OUT_MATE_TRACKING = sr.IS_NOT_SHOW_OUT_MATE_TRACKING,
                         IS_NOT_SHOW_OUT_MEDI_TRACKING = sr.IS_NOT_SHOW_OUT_MEDI_TRACKING,
                         IS_NOT_USE_BHYT = sr.IS_NOT_USE_BHYT,
                         IS_RESULTED = sr.IS_RESULTED,
                         IS_RESULT_IN_DIFF_DAY = sr.IS_RESULT_IN_DIFF_DAY,
                         IS_SAMPLED = sr.IS_SAMPLED,
                         IS_SEND_BARCODE_TO_LIS = sr.IS_SEND_BARCODE_TO_LIS,
                         IS_SENT_EXT = sr.IS_SENT_EXT,
                         IS_STAR_MARK = sr.IS_STAR_MARK,
                         IS_UPDATED_EXT = sr.IS_UPDATED_EXT,
                         JSON_FORM_ID = sr.JSON_FORM_ID,
                         TDL_APPOINTMENT_DATE = sr.TDL_APPOINTMENT_DATE,
                         TDL_INSTRUCTION_NOTE = sr.TDL_INSTRUCTION_NOTE,
                         TDL_IS_KSK_APPROVE = sr.TDL_IS_KSK_APPROVE,
                         TDL_KSK_IS_REQUIRED_APPROVAL = sr.TDL_KSK_IS_REQUIRED_APPROVAL,
                         TDL_PATIENT_AVATAR_URL = sr.TDL_PATIENT_AVATAR_URL,
                         TDL_PATIENT_CLASSIFY_ID = sr.TDL_PATIENT_CLASSIFY_ID,
                         TDL_PATIENT_COMMUNE_CODE = sr.TDL_PATIENT_COMMUNE_CODE,
                         TDL_PATIENT_FIRST_NAME = sr.TDL_PATIENT_FIRST_NAME,
                         TDL_PATIENT_IS_HAS_NOT_DAY_DOB = sr.TDL_PATIENT_IS_HAS_NOT_DAY_DOB,
                         TDL_PATIENT_LAST_NAME = sr.TDL_PATIENT_LAST_NAME,
                         TDL_PATIENT_MILITARY_RANK_NAME = sr.TDL_PATIENT_MILITARY_RANK_NAME,
                         TDL_PATIENT_MOBILE = sr.TDL_PATIENT_MOBILE,
                         TDL_PATIENT_NATIONAL_NAME = sr.TDL_PATIENT_NATIONAL_NAME,
                         TDL_PATIENT_PHONE = sr.TDL_PATIENT_PHONE,
                         TDL_PATIENT_TYPE_ID = sr.TDL_PATIENT_TYPE_ID,
                         TDL_TREATMENT_TYPE_ID = sr.TDL_TREATMENT_TYPE_ID
                     }).ToList();
                return vServiceReqs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ> GetViewByTreatmentId(long id)
        {
            try
            {
                HisServiceReqViewFilterQuery filter = new HisServiceReqViewFilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByExecuteGroupId(long id)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.EXECUTE_GROUP_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByInstructionTime(long instructionTimeFrom, long instructionTimeTo, List<long> serviceReqTypeIds, List<long> treatmentIds)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_TYPE_IDs = serviceReqTypeIds;
                filter.INTRUCTION_TIME_FROM = instructionTimeFrom;
                filter.INTRUCTION_TIME_TO = instructionTimeTo;
                filter.TREATMENT_IDs = treatmentIds;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByTrackingId(long id)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.TRACKING_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByUsedForTrackingId(long id)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.USED_FOR_TRACKING_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByParentId(long parentId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.PARENT_ID = parentId;
            return this.Get(filter);
        }

        internal HIS_SERVICE_REQ GetByBarcode(string barcode)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.BARCODE__EXACT = barcode;
                List<HIS_SERVICE_REQ> list = this.Get(filter);
                return IsNotNullOrEmpty(list) ? list[0] : null;
            }
            return null;
        }

        internal HIS_SERVICE_REQ GetByServiceReqCode(string serviceReqCode)
        {
            if (!string.IsNullOrWhiteSpace(serviceReqCode))
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.SERVICE_REQ_CODE__EXACT = serviceReqCode;
                List<HIS_SERVICE_REQ> list = this.Get(filter);
                return IsNotNullOrEmpty(list) ? list[0] : null;
            }
            return null;
        }

        internal List<HIS_SERVICE_REQ> GetByPaanPositionId(long paanPositionId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.PAAN_POSITION_ID = paanPositionId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByPaanLiquidId(long paanLiquidId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.PAAN_LIQUID_ID = paanLiquidId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByRehaSumId(long rehaSumId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.REHA_SUM_ID = rehaSumId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByDhstId(long id)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.DHST_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByPreviousId(long id)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.PREVIOUS_SERVICE_REQ_ID = id;
            return this.Get(filter);
        }

        internal List<V_HIS_SERVICE_REQ> GetViewByTreatmentIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisServiceReqViewFilterQuery filter = new HisServiceReqViewFilterQuery();
                    filter.TREATMENT_IDs = ids;
                    return this.GetView(filter);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

        internal List<HIS_SERVICE_REQ> GetByPtttCalendarId(long calendarId)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.PTTT_CALENDAR_ID = calendarId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByPatientClassifyId(long id)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.PATIENT_CLASSIFY_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByCarerCardBorrowId(long carerCardBorrowId)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.CARER_CARD_BORROW_ID = carerCardBorrowId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByCarerCardBorrowIds(List<long> carerCardBorrowIds)
        {
            try
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.CARER_CARD_BORROW_IDs = carerCardBorrowIds;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ> GetByTrackingIdOrUsedForTrackingId(long trackingId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.TRACKING_ID__OR__USED_FOR_TRACKING_ID = trackingId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ> GetByBedLogId(long bedLogId)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.BED_LOG_ID = bedLogId;
            return this.Get(filter);
        }
    }
}
