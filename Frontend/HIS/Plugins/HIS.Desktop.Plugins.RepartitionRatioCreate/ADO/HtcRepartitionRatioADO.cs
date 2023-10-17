using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RepartitionRatioCreate.ADO
{
    public class HtcRepartitionRatioADO
    {
        public long? Id { get; set; }
        public string DepartmentCode { get; set; }

        public string TypeName { get; set; }
        public decimal? Ratio { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string ModifyTime { get; set; }
        public string Modifier { get; set; }

        public long RepartitionTypeId { get; set; }
        public long? ParentId { get; set; }

        public string AdoId { get; set; }
        public string AdoParentId { get; set; }

        public bool? IsUpdate { get; set; }
    }
}
