using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK.ADO
{
    public class ErrorADO
    {
        public string ErrorCode { get; set; }
        public string ErrorReason { get; set; }

        public ErrorADO() { }
        public ErrorADO(string _ErrorCode, string _ErrorReason) 
        {
            try
            {
                this.ErrorCode = _ErrorCode;
                this.ErrorReason = _ErrorReason;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }
    }
}
