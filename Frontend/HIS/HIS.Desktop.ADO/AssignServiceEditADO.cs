using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class AssignServiceEditADO
    {
        public Common.RefeshReference DelegateRefeshReference { get; set; }
        public long serviceReqId { get; set; }
        public long instructionTime { get; set; }

        public AssignServiceEditADO(long _serviceReqId, long _instructionTime, Common.RefeshReference _DelegateRefeshReference)
        {
            this.serviceReqId = _serviceReqId;
            this.DelegateRefeshReference = _DelegateRefeshReference;
            this.instructionTime = _instructionTime;
        }
    }
}
