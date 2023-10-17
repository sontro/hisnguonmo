using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Common
{
    public delegate void RefeshReference();
    public delegate void DelegateRefeshTreatmentPartialData(long treatmentId);
    public delegate void DelegateRefeshDataIcd(MOS.EFMODEL.DataModels.HIS_ICD icd);
    public delegate void DelegateRefeshIcdChandoanphu(string icdCodes, string icdNames);
    public delegate void DelegateRefreshAcsUser(string loginNames);
    public delegate void DelegateDataTextLib(MOS.EFMODEL.DataModels.HIS_TEXT_LIB textLib);
    public delegate void DelegateSelectData(object data);
    public delegate void DelegateRefreshData();
    public delegate void DelegateRefresh(MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ serviceReq);
    public delegate void DelegateReturnSuccess(bool success);
    public delegate void DelegateReturnMutilObject(object[] args);
    public delegate void DelegateCloseForm_Uc(object data);
    public delegate void DelegateSelectDatas(object data1, object data2);
}
