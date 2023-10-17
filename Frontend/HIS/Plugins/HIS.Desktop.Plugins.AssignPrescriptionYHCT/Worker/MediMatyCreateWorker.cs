using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT
{
    class MediMatyCreateWorker
    {
        public delegate object GetDataAmountOutOfStock(MediMatyTypeADO model, long serviceId, long meidStockId);
        public delegate void SetDefaultMediStockForData(MediMatyTypeADO model);
        public delegate HIS_PATIENT_TYPE ChoosePatientTypeDefaultlService(long patientTypeId, long serviceId, long serviceTypeId);
        public delegate long GetPatientTypeId();
        public delegate int GetNumRow();
        public delegate void SetNumRow();
        public delegate List<MediMatyTypeADO> GetMediMatyTypeADOs();
        public delegate bool GetIsAutoCheckExpend();

        public GetDataAmountOutOfStock getDataAmountOutOfStock;
        public SetDefaultMediStockForData setDefaultMediStockForData;
        public ChoosePatientTypeDefaultlService choosePatientTypeDefaultlService;
        public GetPatientTypeId getPatientTypeId;
        public GetNumRow getNumRow;
        public SetNumRow setNumRow;
        public GetMediMatyTypeADOs getMediMatyTypeADOs;
        public GetIsAutoCheckExpend getIsAutoCheckExpend;

        internal MediMatyCreateWorker(GetDataAmountOutOfStock _getDataAmountOutOfStock, SetDefaultMediStockForData _setDefaultMediStockForData, ChoosePatientTypeDefaultlService _choosePatientTypeDefaultlService, GetPatientTypeId _getPatientTypeId, GetNumRow _getNumRow, SetNumRow _setNumRow, GetMediMatyTypeADOs _getMediMatyTypeADOs, GetIsAutoCheckExpend _getIsAutoCheckExpend)
        {
            this.getDataAmountOutOfStock = _getDataAmountOutOfStock;
            this.setDefaultMediStockForData = _setDefaultMediStockForData;
            this.choosePatientTypeDefaultlService = _choosePatientTypeDefaultlService;
            this.getPatientTypeId = _getPatientTypeId;
            this.getNumRow = _getNumRow;
            this.setNumRow = _setNumRow;
            this.getMediMatyTypeADOs = _getMediMatyTypeADOs;
            this.getIsAutoCheckExpend = _getIsAutoCheckExpend;
        }
    }
}
