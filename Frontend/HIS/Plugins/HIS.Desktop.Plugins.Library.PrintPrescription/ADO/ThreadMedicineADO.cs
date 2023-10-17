using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintPrescription.ADO
{
    class ThreadMedicineADO
    {
        #region du lieu vao
        public List<MOS.EFMODEL.DataModels.HIS_EXP_MEST> ExpMests { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_EXP_MEST_MATERIAL> Materials { get; set; }
        //public List<MOS.EFMODEL.DataModels.HIS_EXP_MEST_MATY_REQ> MatyReqs { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_EXP_MEST_MEDICINE> Medicines { get; set; }
        //public List<MOS.EFMODEL.DataModels.HIS_EXP_MEST_METY_REQ> MetyReqs { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ_MATY> ServiceReqMaties { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ_METY> ServiceReqMeties { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ> ServiceReqs { get; set; }
        
        #endregion
        public bool? HasOutHospital { get; set; }
        public bool HasMediMate { get; set; }

        /// <summary>
        /// key exp_Mest_id thuốc, vật tư trong kho
        /// key service_req_id thuốc, vật tư ngoài kho
        /// </summary>
        public Dictionary<long, List<ExpMestMedicineSDO>> DicLstMediMateExpMestTypeADO { get; set; }//ra
        //public List<ExpMestMedicineSDO> lstMedicineExpmestTypeADO { get; set; }
        
        public ThreadMedicineADO(MOS.SDO.OutPatientPresResultSDO data, bool hasMediMate, bool? hasOutHospital = null)
        {
            try
            {
                if (data != null)
                {
                    this.ExpMests = data.ExpMests;
                    this.Materials = data.Materials;
                    this.Medicines = data.Medicines;
                    this.ServiceReqMaties = data.ServiceReqMaties;
                    this.ServiceReqMeties = data.ServiceReqMeties;
                    this.ServiceReqs = data.ServiceReqs;
                    this.HasMediMate = hasMediMate;
                    this.HasOutHospital = hasOutHospital;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public ThreadMedicineADO(MOS.SDO.InPatientPresResultSDO data, bool hasMediMate, bool? hasOutHospital = null)
        {
            try
            {
                if (data != null)
                {
                    this.ExpMests = data.ExpMests;
                    this.Materials = data.Materials;
                    this.Medicines = data.Medicines;
                    this.ServiceReqMaties = data.ServiceReqMaties;
                    this.ServiceReqMeties = data.ServiceReqMeties;
                    this.ServiceReqs = data.ServiceReqs;
                    this.HasMediMate = hasMediMate;
                    this.HasOutHospital = hasOutHospital;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
