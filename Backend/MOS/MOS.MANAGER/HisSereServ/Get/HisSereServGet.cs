using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServGet : GetBase
    {
        internal HisSereServGet()
            : base()
        {

        }

        internal HisSereServGet(Inventec.Core.CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV> Get(HisSereServFilterQuery filter)
        {
            try
            {
                List<HIS_SERE_SERV> result = DAOWorker.HisSereServDAO.Get(filter.Query(), param);
                if (IsNotNullOrEmpty(result))
                {
                    result.ForEach(o =>
                    {
                        o.HIS_SERE_SERV1 = null;
                        o.HIS_SERE_SERV2 = null;
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

        internal List<HIS_SERE_SERV> GetHasExecuteByTreatmentId(long treatmentId)
        {
            HisSereServFilterQuery sereServFilter = new HisSereServFilterQuery();
            sereServFilter.TREATMENT_ID = treatmentId;
            sereServFilter.HAS_EXECUTE = true;
            return new HisSereServGet().Get(sereServFilter);
        }

        internal List<V_HIS_SERE_SERV> GetView(HisSereServViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV> GetViewByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
                filter.IDs = ids;
                return this.GetView(filter);
            }
            return null;
        }

        /// <summary>
        /// Lay du lieu view tu bang bang cach join voi du lieu cac bang  danh muc co san trong RAM de tang hieu nang
        /// </summary>
        /// <param name="sereServs"></param>
        /// <returns></returns>
        internal List<V_HIS_SERE_SERV> GetViewFromTable(List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                List<V_HIS_SERE_SERV> vSereServs =
                    (from ss in sereServs
                     join pt in HisPatientTypeCFG.DATA on ss.PATIENT_TYPE_ID equals pt.ID
                     join su in HisServiceUnitCFG.DATA on ss.TDL_SERVICE_UNIT_ID equals su.ID
                     join st in HisServiceTypeCFG.DATA on ss.TDL_SERVICE_TYPE_ID equals st.ID
                     join hst in HisHeinServiceTypeCFG.DATA on ss.TDL_HEIN_SERVICE_TYPE_ID equals hst.ID into group1
                     from g1 in group1.DefaultIfEmpty() //de left join HIS_HEIN_SERVICE_TYPE
                     join rroom in HisRoomCFG.DATA on ss.TDL_REQUEST_ROOM_ID equals rroom.ID
                     join eroom in HisRoomCFG.DATA on ss.TDL_REQUEST_ROOM_ID equals eroom.ID
                     join edep in HisDepartmentCFG.DATA on ss.TDL_EXECUTE_DEPARTMENT_ID equals edep.ID
                     join rdep in HisDepartmentCFG.DATA on ss.TDL_REQUEST_DEPARTMENT_ID equals rdep.ID
                     select new V_HIS_SERE_SERV
                         {
                             ID = ss.ID,
                             CREATE_TIME = ss.CREATE_TIME,
                             MODIFY_TIME = ss.MODIFY_TIME,
                             CREATOR = ss.CREATOR,
                             MODIFIER = ss.MODIFIER,
                             APP_CREATOR = ss.APP_CREATOR,
                             APP_MODIFIER = ss.APP_MODIFIER,
                             IS_ACTIVE = ss.IS_ACTIVE,
                             IS_DELETE = ss.IS_DELETE,
                             GROUP_CODE = ss.GROUP_CODE,
                             SERVICE_ID = ss.SERVICE_ID,
                             SERVICE_REQ_ID = ss.SERVICE_REQ_ID,
                             PATIENT_TYPE_ID = ss.PATIENT_TYPE_ID,
                             PARENT_ID = ss.PARENT_ID,
                             HEIN_APPROVAL_ID = ss.HEIN_APPROVAL_ID,
                             JSON_PATIENT_TYPE_ALTER = ss.JSON_PATIENT_TYPE_ALTER,
                             AMOUNT = ss.AMOUNT,
                             PRICE = ss.PRICE,
                             ORIGINAL_PRICE = ss.ORIGINAL_PRICE,
                             CONFIG_HEIN_LIMIT_PRICE = ss.CONFIG_HEIN_LIMIT_PRICE,
                             HEIN_PRICE = ss.HEIN_PRICE,
                             HEIN_RATIO = ss.HEIN_RATIO,
                             HEIN_LIMIT_PRICE = ss.HEIN_LIMIT_PRICE,
                             HEIN_LIMIT_RATIO = ss.HEIN_LIMIT_RATIO,
                             HEIN_NORMAL_PRICE = ss.HEIN_NORMAL_PRICE,
                             ADD_PRICE = ss.ADD_PRICE,
                             OVERTIME_PRICE = ss.OVERTIME_PRICE,
                             DISCOUNT = ss.DISCOUNT,
                             VAT_RATIO = ss.VAT_RATIO,
                             SHARE_COUNT = ss.SHARE_COUNT,
                             IS_EXPEND = ss.IS_EXPEND,
                             IS_NO_PAY = ss.IS_NO_PAY,
                             IS_NO_EXECUTE = ss.IS_NO_EXECUTE,
                             IS_OUT_PARENT_FEE = ss.IS_OUT_PARENT_FEE,
                             IS_NO_HEIN_DIFFERENCE = ss.IS_NO_HEIN_DIFFERENCE,
                             IS_SPECIMEN = ss.IS_SPECIMEN,
                             IS_ADDITION = ss.IS_ADDITION,
                             EXECUTE_TIME = ss.EXECUTE_TIME,
                             HEIN_CARD_NUMBER = ss.HEIN_CARD_NUMBER,
                             MEDICINE_ID = ss.MEDICINE_ID,
                             MATERIAL_ID = ss.MATERIAL_ID,
                             BLOOD_ID = ss.BLOOD_ID,
                             EKIP_ID = ss.EKIP_ID,
                             PACKAGE_ID = ss.PACKAGE_ID,
                             VIR_PRICE = ss.VIR_PRICE,
                             VIR_PRICE_NO_ADD_PRICE = ss.VIR_PRICE_NO_ADD_PRICE,
                             VIR_PRICE_NO_EXPEND = ss.VIR_PRICE_NO_EXPEND,
                             VIR_HEIN_PRICE = ss.VIR_HEIN_PRICE,
                             VIR_PATIENT_PRICE = ss.VIR_PATIENT_PRICE,
                             VIR_PATIENT_PRICE_BHYT = ss.VIR_PATIENT_PRICE_BHYT,
                             VIR_TOTAL_PRICE = ss.VIR_TOTAL_PRICE,
                             VIR_TOTAL_PRICE_NO_ADD_PRICE = ss.VIR_TOTAL_PRICE_NO_ADD_PRICE,
                             VIR_TOTAL_PRICE_NO_EXPEND = ss.VIR_TOTAL_PRICE_NO_EXPEND,
                             VIR_TOTAL_HEIN_PRICE = ss.VIR_TOTAL_HEIN_PRICE,
                             VIR_TOTAL_PATIENT_PRICE = ss.VIR_TOTAL_PATIENT_PRICE,
                             VIR_TOTAL_PATIENT_PRICE_BHYT = ss.VIR_TOTAL_PATIENT_PRICE_BHYT,
                             TDL_INTRUCTION_TIME = ss.TDL_INTRUCTION_TIME,
                             TDL_INTRUCTION_DATE = ss.TDL_INTRUCTION_DATE,
                             TDL_PATIENT_ID = ss.TDL_PATIENT_ID,
                             TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID,
                             TDL_TREATMENT_CODE = ss.TDL_TREATMENT_CODE,
                             TDL_SERVICE_CODE = ss.TDL_SERVICE_CODE,
                             TDL_SERVICE_NAME = ss.TDL_SERVICE_NAME,
                             TDL_HEIN_SERVICE_BHYT_CODE = ss.TDL_HEIN_SERVICE_BHYT_CODE,
                             TDL_HEIN_SERVICE_BHYT_NAME = ss.TDL_HEIN_SERVICE_BHYT_NAME,
                             TDL_HEIN_ORDER = ss.TDL_HEIN_ORDER,
                             TDL_SERVICE_TYPE_ID = ss.TDL_SERVICE_TYPE_ID,
                             TDL_SERVICE_UNIT_ID = ss.TDL_SERVICE_UNIT_ID,
                             TDL_HEIN_SERVICE_TYPE_ID = ss.TDL_HEIN_SERVICE_TYPE_ID,
                             TDL_ACTIVE_INGR_BHYT_CODE = ss.TDL_ACTIVE_INGR_BHYT_CODE,
                             TDL_ACTIVE_INGR_BHYT_NAME = ss.TDL_ACTIVE_INGR_BHYT_NAME,
                             TDL_MEDICINE_CONCENTRA = ss.TDL_MEDICINE_CONCENTRA,
                             TDL_MEDICINE_BID_NUM_ORDER = ss.TDL_MEDICINE_BID_NUM_ORDER,
                             TDL_MEDICINE_REGISTER_NUMBER = ss.TDL_MEDICINE_REGISTER_NUMBER,
                             TDL_MEDICINE_PACKAGE_NUMBER = ss.TDL_MEDICINE_PACKAGE_NUMBER,
                             TDL_SERVICE_REQ_CODE = ss.TDL_SERVICE_REQ_CODE,
                             TDL_REQUEST_ROOM_ID = ss.TDL_REQUEST_ROOM_ID,
                             TDL_REQUEST_DEPARTMENT_ID = ss.TDL_REQUEST_DEPARTMENT_ID,
                             TDL_REQUEST_LOGINNAME = ss.TDL_REQUEST_LOGINNAME,
                             TDL_REQUEST_USERNAME = ss.TDL_REQUEST_USERNAME,
                             TDL_EXECUTE_ROOM_ID = ss.TDL_EXECUTE_ROOM_ID,
                             TDL_EXECUTE_DEPARTMENT_ID = ss.TDL_EXECUTE_DEPARTMENT_ID,
                             INVOICE_ID = ss.INVOICE_ID,
                             TDL_SERVICE_REQ_TYPE_ID = ss.TDL_SERVICE_REQ_TYPE_ID,
                             TDL_HST_BHYT_CODE = ss.TDL_HST_BHYT_CODE,
                             TDL_PACS_TYPE_CODE = ss.TDL_PACS_TYPE_CODE,
                             EQUIPMENT_SET_ID = ss.EQUIPMENT_SET_ID,
                             EQUIPMENT_SET_ORDER = ss.EQUIPMENT_SET_ORDER,
                             TDL_IS_MAIN_EXAM = ss.TDL_IS_MAIN_EXAM,

                             PATIENT_TYPE_CODE = pt.PATIENT_TYPE_CODE,
                             PATIENT_TYPE_NAME = pt.PATIENT_TYPE_NAME,
                             IS_COPAYMENT = pt.IS_COPAYMENT,

                             SERVICE_TYPE_CODE = st.SERVICE_TYPE_CODE,
                             SERVICE_TYPE_NAME = st.SERVICE_TYPE_NAME,

                             SERVICE_UNIT_CODE = su.SERVICE_UNIT_CODE,
                             SERVICE_UNIT_NAME = su.SERVICE_UNIT_NAME,

                             HEIN_SERVICE_TYPE_CODE = g1 != null ? g1.HEIN_SERVICE_TYPE_CODE : null, //left join ==> can check null
                             HEIN_SERVICE_TYPE_NAME = g1 != null ? g1.HEIN_SERVICE_TYPE_NAME : null, //left join ==> can check null
                             HEIN_SERVICE_TYPE_NUM_ORDER = g1 != null ? g1.NUM_ORDER : null, //left join ==> can check null

                             EXECUTE_ROOM_CODE = eroom.ROOM_CODE,
                             EXECUTE_ROOM_NAME = eroom.ROOM_NAME,

                             REQUEST_ROOM_CODE = rroom.ROOM_CODE,
                             REQUEST_ROOM_NAME = rroom.ROOM_NAME,

                             EXECUTE_DEPARTMENT_CODE = edep.DEPARTMENT_CODE,
                             EXECUTE_DEPARTMENT_NAME = edep.DEPARTMENT_NAME,

                             REQUEST_DEPARTMENT_CODE = rdep.DEPARTMENT_CODE,
                             REQUEST_DEPARTMENT_NAME = rdep.DEPARTMENT_NAME,
                             SERVICE_CONDITION_ID = ss.SERVICE_CONDITION_ID,
                             OTHER_PAY_SOURCE_ID = ss.OTHER_PAY_SOURCE_ID
                         }).ToList();
                return vSereServs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV> GetViewByServiceReqIds(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                return this.GetView(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV> GetByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV> GetByInvoiceId(long invoiceId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.INVOICE_ID = invoiceId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByServiceReqId(long serviceReqId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            return new HisSereServGet().Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByServiceReqIds(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV> GetByServiceReqIdsAndHasExecute(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.HAS_EXECUTE = true;
                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV> GetByServiceReqId(List<long> serviceReqIds)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.SERVICE_REQ_IDs = serviceReqIds;
            return new HisSereServGet().Get(filter);
        }

        internal List<V_HIS_SERE_SERV> GetViewByServiceReqId(long serviceReqId)
        {
            HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            return new HisSereServGet().GetView(filter);
        }

        internal List<V_HIS_SERE_SERV> GetViewByServiceReqIdAndIsSpecimen(long serviceReqId, bool? isSpecimen)
        {
            HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            filter.IS_SPECIMEN = isSpecimen;
            return new HisSereServGet().GetView(filter);
        }

        internal List<V_HIS_SERE_SERV> GetViewByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool? isSpecimen)
        {
            if (serviceReqIds != null)
            {
                HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_SPECIMEN = isSpecimen;
                return new HisSereServGet().GetView(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV> GetByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool? isSpecimen)
        {
            if (serviceReqIds != null)
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_SPECIMEN = isSpecimen;
                filter.HAS_EXECUTE = true;
                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV> GetViewByTreatmentId(long treatmentId)
        {
            HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
            filter.TREATMENT_ID = treatmentId;
            return new HisSereServGet().GetView(filter);
        }

        internal HIS_SERE_SERV GetById(long id, HisSereServFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV> GetByPatientTypeId(long patientTypeId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.PATIENT_TYPE_ID = patientTypeId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByTreatmentIdAndPatientTypeId(long treatmentId, long patientTypeId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.PATIENT_TYPE_ID = patientTypeId;
            filter.TREATMENT_ID = treatmentId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByServiceId(long serviceId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.SERVICE_ID = serviceId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByTreatmentId(long id)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.TREATMENT_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByTreatmentIds(List<long> ids)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.TREATMENT_IDs = ids;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByParentId(long parentId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.PARENT_ID = parentId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByParentIds(List<long> parentIds)
        {
            if (IsNotNullOrEmpty(parentIds))
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.PARENT_IDs = parentIds;
                return this.Get(filter);
            }
            return null;
        }

        internal V_HIS_SERE_SERV GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisSereServViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV GetViewById(long id, HisSereServViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV> GetByPackageId(long id)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.PACKAGE_ID = id;
            return this.Get(filter);
        }

        internal bool IsProperPatientTypeData(HIS_PATIENT_TYPE_ALTER patyAlterBhyt, BhytPatientTypeData patientData)
        {
            return IsNotNullOrEmpty(patyAlterBhyt.HEIN_CARD_NUMBER)
                && patyAlterBhyt.HEIN_CARD_NUMBER.Equals(patientData.HEIN_CARD_NUMBER)
                
                && ((IsNotNullOrEmpty(patientData.HEIN_MEDI_ORG_CODE) && IsNotNullOrEmpty(patientData.HEIN_MEDI_ORG_CODE) && patyAlterBhyt.RIGHT_ROUTE_CODE.Equals(patientData.HEIN_MEDI_ORG_CODE))
                    || (!IsNotNullOrEmpty(patientData.HEIN_MEDI_ORG_CODE) && !IsNotNullOrEmpty(patyAlterBhyt.HEIN_MEDI_ORG_CODE)))

                && ((IsNotNullOrEmpty(patientData.LEVEL_CODE) && IsNotNullOrEmpty(patientData.LEVEL_CODE) && patyAlterBhyt.RIGHT_ROUTE_CODE.Equals(patientData.LEVEL_CODE))
                    || (!IsNotNullOrEmpty(patientData.LEVEL_CODE) && !IsNotNullOrEmpty(patyAlterBhyt.LEVEL_CODE)))

                && ((IsNotNullOrEmpty(patientData.RIGHT_ROUTE_CODE) && IsNotNullOrEmpty(patientData.RIGHT_ROUTE_CODE) && patyAlterBhyt.RIGHT_ROUTE_CODE.Equals(patientData.RIGHT_ROUTE_CODE))
                    || (!IsNotNullOrEmpty(patientData.RIGHT_ROUTE_CODE) && !IsNotNullOrEmpty(patyAlterBhyt.RIGHT_ROUTE_CODE)))

                && ((IsNotNullOrEmpty(patientData.JOIN_5_YEAR) && IsNotNullOrEmpty(patientData.JOIN_5_YEAR) && patyAlterBhyt.JOIN_5_YEAR.Equals(patientData.JOIN_5_YEAR))
                    || (!IsNotNullOrEmpty(patientData.JOIN_5_YEAR) && !IsNotNullOrEmpty(patyAlterBhyt.JOIN_5_YEAR)))

                && ((IsNotNullOrEmpty(patientData.PAID_6_MONTH) && IsNotNullOrEmpty(patientData.PAID_6_MONTH) && patyAlterBhyt.JOIN_5_YEAR.Equals(patientData.PAID_6_MONTH))
                    || (!IsNotNullOrEmpty(patientData.PAID_6_MONTH) && !IsNotNullOrEmpty(patyAlterBhyt.PAID_6_MONTH)))

                && ((IsNotNullOrEmpty(patientData.LIVE_AREA_CODE) && IsNotNullOrEmpty(patientData.LIVE_AREA_CODE) && patyAlterBhyt.JOIN_5_YEAR.Equals(patientData.LIVE_AREA_CODE))
                    || (!IsNotNullOrEmpty(patientData.LIVE_AREA_CODE) && !IsNotNullOrEmpty(patyAlterBhyt.LIVE_AREA_CODE)));
        }

        internal List<HIS_SERE_SERV> GetByHeinApprovalId(long heinApprovalId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.HEIN_APPROVAL_ID = heinApprovalId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV> GetByEquipmentSetId(long equipmentSetId)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.EQUIPMENT_SET_ID = equipmentSetId;
            return this.Get(filter);
        }

        internal List<V_HIS_SERE_SERV> GetViewByTreatmentIds(List<long> treatmentIds)
        {
            if (treatmentIds!=null)
            {
                HisSereServViewFilterQuery filter = new HisSereServViewFilterQuery();
                filter.TREATMENT_IDs = treatmentIds;
                return new HisSereServGet().GetView(filter);
            }
            return null;
        }

    }
}
