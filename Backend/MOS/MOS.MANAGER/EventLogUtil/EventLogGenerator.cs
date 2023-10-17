using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LogManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class EventLogGenerator
    {
        private EventLog.Enum logEnum;
        private object[] extraParams;

        public EventLogGenerator(EventLog.Enum en, params object[] extras)
        {
            this.logEnum = en;
            this.extraParams = extras;
        }

        [SimpleEventKey(SimpleEventKey.PATIENT_CODE)]
        private string patientCode;

        public EventLogGenerator PatientCode(string p)
        {
            this.patientCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.TREATMENT_CODE)]
        private string treatmentCode;

        public EventLogGenerator TreatmentCode(string p)
        {
            this.treatmentCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.ANTIBIOTIC_REQUEST_CODE)]
        private string antibioticRequestCode;

        public EventLogGenerator AntibioticRequestCode(string p)
        {
            this.antibioticRequestCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.SERVICE_REQ_CODE)]
        private string serviceReqCode;

        public EventLogGenerator ServiceReqCode(string p)
        {
            this.serviceReqCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.EXP_MEST_CODE)]
        private string expMestCode;

        public EventLogGenerator ExpMestCode(string p)
        {
            this.expMestCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.IMP_MEST_CODE)]
        private string impMestCode;

        public EventLogGenerator ImpMestCode(string p)
        {
            this.impMestCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.AGGR_EXP_MEST_CODE, SimpleEventKey.EXP_MEST_CODE)]
        private string aggrExpMestCode;

        public EventLogGenerator AggrExpMestCode(string p)
        {
            this.aggrExpMestCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.AGGR_IMP_MEST_CODE, SimpleEventKey.IMP_MEST_CODE)]
        private string aggrImpMestCode;

        public EventLogGenerator AggrImpMestCode(string p)
        {
            this.aggrImpMestCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.DISPENSE_CODE)]
        private string dispenseCode;

        public EventLogGenerator DispenseCode(string p)
        {
            this.dispenseCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.TRANSACTION_CODE)]
        private string transactionCode;

        public EventLogGenerator TransactionCode(string p)
        {
            this.transactionCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.INVOICE_NUM_ORDER)]
        private string invoiceNumOrder;

        public EventLogGenerator InvoiceNumOrder(string p)
        {
            this.invoiceNumOrder = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.VACCINATION_CODE)]
        private string vaccinationCode;

        public EventLogGenerator VaccinationCode(string p)
        {
            this.vaccinationCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.PREPARE_CODE)]
        private string prepareCode;

        public EventLogGenerator PrepareCode(string p)
        {
            this.prepareCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.MEDICINE_TYPE_CODE)]
        private string medicineTypeCode;

        public EventLogGenerator MedicineTypeCode(string p)
        {
            this.medicineTypeCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.MATERIAL_TYPE_CODE)]
        private string materialTypeCode;

        public EventLogGenerator MaterialTypeCode(string p)
        {
            this.materialTypeCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.BLOOD_TYPE_CODE)]
        private string bloodTypeCode;

        public EventLogGenerator BloodTypeCode(string p)
        {
            this.bloodTypeCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.STORE_CODE)]
        private string storeCode;

        public EventLogGenerator StoreCode(string p)
        {
            this.storeCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.BID_ID)]
        private string bidId;

        public EventLogGenerator BidId(string p)
        {
            this.bidId = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.BID_NUMBER)]
        private string bidNumber;

        public EventLogGenerator BidNumber(string p)
        {
            this.bidNumber = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.MEDICINE_TYPE_ID)]
        private string medicineTypeId;

        public EventLogGenerator MedicineTypeId(string p)
        {
            this.medicineTypeId = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.MATERIAL_TYPE_ID)]
        private string materialTypeId;

        public EventLogGenerator MaterialTypeId(string p)
        {
            this.materialTypeId = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.BLOOD_TYPE_ID)]
        private string bloodTypeId;

        public EventLogGenerator BloodTypeId(string p)
        {
            this.bloodTypeId = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.DETAIL)]
        private string detail;

        public EventLogGenerator Detail(string p)
        {
            this.detail = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.SERVICE_CODE)]
        private string serviceCode;

        public EventLogGenerator ServiceCode(string p)
        {
            this.serviceCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.KEY)]
        private string key;

        public EventLogGenerator Key(string p)
        {
            this.key = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.VACCINATION_DATA)]
        private VaccinationData vaccinationData;

        public EventLogGenerator VaccinationData(VaccinationData p)
        {
            this.vaccinationData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.SERVICE_REQ_DATA)]
        private ServiceReqData serviceReqData;

        public EventLogGenerator ServiceReqData(ServiceReqData p)
        {
            this.serviceReqData = p;
            return this;
        }


        [ComplexEventKey(ComplexEventKey.SERVICE_REQ_DATA_1)]
        private ServiceReqData serviceReqData1;

        public EventLogGenerator ServiceReqData1(ServiceReqData p)
        {
            this.serviceReqData1 = p;
            return this;
        }


        [ComplexEventKey(ComplexEventKey.SERVICE_REQ_DATA_2)]
        private ServiceReqData serviceReqData2;

        public EventLogGenerator ServiceReqData2(ServiceReqData p)
        {
            this.serviceReqData2 = p;
            return this;
        }


        [ComplexEventKey(ComplexEventKey.SERVICE_REQ_LIST)]
        private List<ServiceReqData> serviceReqList;

        public EventLogGenerator ServiceReqList(List<ServiceReqData> p)
        {
            this.serviceReqList = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.VACCINATION_LIST)]
        private List<VaccinationData> vaccinationList;

        public EventLogGenerator VaccinationList(List<VaccinationData> p)
        {
            this.vaccinationList = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.PATIENT_DATA)]
        private PatientData patientData;

        public EventLogGenerator PatientData(PatientData p)
        {
            this.patientData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.ASSIGN_RATION_DATA)]
        private AssignRationData assignRationData;

        public EventLogGenerator AssignRationData(AssignRationData r)
        {
            this.assignRationData = r;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.UPDATE_RATION_DATA)]
        private UpdateRationData updateRationData;

        public EventLogGenerator UpdateRationData(UpdateRationData r)
        {
            this.updateRationData = r;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.AGGR_EXP_MEST_DATA)]
        private AggrExpMestData aggrExpMestData;

        public EventLogGenerator AggrExpMestData(AggrExpMestData p)
        {
            this.aggrExpMestData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.AGGR_IMP_MEST_DATA)]
        private AggrImpMestData aggrImpMestData;

        public EventLogGenerator AggrExpMestData(AggrImpMestData p)
        {
            this.aggrImpMestData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.AGGR_EXP_MEST_LIST)]
        private List<AggrExpMestData> aggrExpMestList;

        public EventLogGenerator AggrExpMestList(List<AggrExpMestData> p)
        {
            this.aggrExpMestList = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.AGGR_IMP_MEST_LIST)]
        private List<AggrImpMestData> aggrImpMestList;

        public EventLogGenerator AggrImpMestList(List<AggrImpMestData> p)
        {
            this.aggrImpMestList = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.PREPARE_DETAIL_LIST)]
        private List<string> prepareDetailList;

        public EventLogGenerator PrepareDetailList(List<string> p)
        {
            this.prepareDetailList = p;
            return this;
        }
        [SimpleEventKey(SimpleEventKey.MEDICINE_ID)]
        private string medicineId;

        public EventLogGenerator MedicineId(string p)
        {
            this.medicineId = p;
            return this;
        }
        [SimpleEventKey(SimpleEventKey.MATERIAL_ID)]
        private string materialId;

        public EventLogGenerator MaterialId(string p)
        {
            this.materialId = p;
            return this;
        }

        public void Run()
        {
            try
            {
                string logContent = LogCommonUtil.GetEventLogContent(this.logEnum);

                if (!string.IsNullOrWhiteSpace(logContent))
                {
                    if (this.extraParams != null && this.extraParams.Length > 0)
                    {
                        logContent = String.Format(logContent, extraParams);
                    }

                    FieldInfo[] fields = typeof(EventLogGenerator).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (FieldInfo field in fields)
                    {
                        if (field.CustomAttributes != null && field.CustomAttributes.Count() > 0)
                        {
                            SimpleEventKey simpleEventKey = (SimpleEventKey)field.GetCustomAttribute(typeof(SimpleEventKey), false);
                            ComplexEventKey complexEventKey = (ComplexEventKey)field.GetCustomAttribute(typeof(ComplexEventKey), false);

                            string val = "";

                            if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                IEnumerable list = (IEnumerable)field.GetValue(this);
                                if (list != null)
                                {
                                    foreach (var t in list)
                                    {
                                        if (t != null)
                                        {
                                            val += string.IsNullOrWhiteSpace(val) ? t.ToString() : "; " + t.ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                val = field.GetValue(this) != null ? field.GetValue(this).ToString() : "";
                            }

                            if (simpleEventKey != null)
                            {
                                val = !string.IsNullOrWhiteSpace(val) ? string.Format("{0}: {1}", simpleEventKey.Value, val) : val;
                                logContent = logContent.Replace("%" + simpleEventKey.Value + "%", val);
                            }
                            if (complexEventKey != null)
                            {
                                logContent = logContent.Replace("%" + complexEventKey.Value + "%", val);
                            }
                        }
                    }

                    EventLogCache.Push(logContent);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
