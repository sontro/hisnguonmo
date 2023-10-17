using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK
{
    class MediMatyCreateWorker
    {
        public delegate object GetDataAmountOutOfStock(MediMatyTypeADO model, long serviceId, long meidStockId);
        public delegate void SetDefaultMediStockForData(MediMatyTypeADO model);
        public delegate HIS_PATIENT_TYPE ChoosePatientTypeDefaultlService(long patientTypeId, MediMatyTypeADO medimaty);
        public delegate HIS_PATIENT_TYPE ChoosePatientTypeDefaultlServiceOther(long patientTypeId, long serviceId, long serviceTypeId);
        public delegate long GetPatientTypeId();
        public delegate int GetNumRow();
        public delegate void SetNumRow();
        public delegate List<MediMatyTypeADO> GetMediMatyTypeADOs();
        public delegate bool GetIsAutoCheckExpend();

        public GetDataAmountOutOfStock getDataAmountOutOfStock;
        public SetDefaultMediStockForData setDefaultMediStockForData;
        public ChoosePatientTypeDefaultlService choosePatientTypeDefaultlService;
        public ChoosePatientTypeDefaultlServiceOther choosePatientTypeDefaultlServiceOther;
        public GetPatientTypeId getPatientTypeId;
        public GetNumRow getNumRow;
        public SetNumRow setNumRow;
        public GetMediMatyTypeADOs getMediMatyTypeADOs;
        public GetIsAutoCheckExpend getIsAutoCheckExpend;

        internal MediMatyCreateWorker(GetDataAmountOutOfStock _getDataAmountOutOfStock, SetDefaultMediStockForData _setDefaultMediStockForData, ChoosePatientTypeDefaultlService _choosePatientTypeDefaultlService, ChoosePatientTypeDefaultlServiceOther _choosePatientTypeDefaultlServiceOther, GetPatientTypeId _getPatientTypeId, GetNumRow _getNumRow, SetNumRow _setNumRow, GetMediMatyTypeADOs _getMediMatyTypeADOs, GetIsAutoCheckExpend _getIsAutoCheckExpend)
        {
            this.getDataAmountOutOfStock = _getDataAmountOutOfStock;
            this.setDefaultMediStockForData = _setDefaultMediStockForData;
            this.choosePatientTypeDefaultlService = _choosePatientTypeDefaultlService;
            this.choosePatientTypeDefaultlServiceOther = _choosePatientTypeDefaultlServiceOther;
            this.getPatientTypeId = _getPatientTypeId;
            this.getNumRow = _getNumRow;
            this.setNumRow = _setNumRow;
            this.getMediMatyTypeADOs = _getMediMatyTypeADOs;
            this.getIsAutoCheckExpend = _getIsAutoCheckExpend;
        }
    }
}
