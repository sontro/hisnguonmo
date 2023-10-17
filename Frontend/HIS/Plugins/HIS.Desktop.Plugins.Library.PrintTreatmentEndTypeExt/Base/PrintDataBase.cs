using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.Base
{
    public class PrintDataBase
    {
        protected V_HIS_TREATMENT V_Treatment { get; set; }
        protected long TreatmentId { get; set; }
        protected HIS_TREATMENT Treatment { get; set; }
        protected HIS_PATIENT Patient { get; set; }
        protected V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        protected long? roomId { get; set; }

        public PrintDataBase(long treatmentId, long? roomId)
        {
            try
            {
                this.TreatmentId = treatmentId;
                this.roomId = roomId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void LoadData()
        {
            try
            {
                this.LoadTreatment();
                this.LoadPatient();
                this.LoadPatientTypeAlter();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void LoadDataV()
        {
            try
            {
                this.LoadVTreatment();
                this.LoadPatient();
                this.LoadPatientTypeAlter();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void LoadVTreatment()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentFilter treatmentFilter = new HisTreatmentFilter();
                treatmentFilter.ID = this.TreatmentId;
                this.V_Treatment = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.V_HIS_TREATMENT>>("api/HisTreatment/GetView", ApiConsumers.MosConsumer, treatmentFilter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void LoadTreatment()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentFilter treatmentFilter = new HisTreatmentFilter();
                treatmentFilter.ID = this.TreatmentId;
                this.Treatment = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, treatmentFilter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void LoadPatient()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisPatientFilter patientFilter = new HisPatientFilter();
                if (Treatment != null)
                {
                    patientFilter.ID = this.Treatment.PATIENT_ID;
                }
                else
                {
                    patientFilter.ID = this.V_Treatment.PATIENT_ID;
                }
                this.Patient = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_PATIENT>>("api/HisPatient/Get", ApiConsumers.MosConsumer, patientFilter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void LoadPatientTypeAlter()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisPatientTypeAlterViewAppliedFilter filter = new HisPatientTypeAlterViewAppliedFilter();
                filter.TreatmentId = this.TreatmentId;
                filter.InstructionTime = Inventec.Common.DateTime.Get.Now() ?? 0;
                this.PatientTypeAlter = new BackendAdapter(param).Get<MOS.EFMODEL.DataModels.V_HIS_PATIENT_TYPE_ALTER>(HisRequestUriStore.HIS_PATIENT_TYPE_ALTER_GET_APPLIED, ApiConsumers.MosConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
