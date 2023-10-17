using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ImpMestViewDetailADO
    {
        public long ImpMestId { get; set; }
        public long ImpMestSttId { get; set; }
        public long IMP_MEST_TYPE_ID { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="_ImpMestId">Id phieu nhap</param>
        /// <param name="_IMP_MEST_TYPE_ID">id loai nhap</param>
        /// /// <param name="_ImpMestSttId">id trang thai nhap</param>
        public ImpMestViewDetailADO(long _ImpMestId, long _IMP_MEST_TYPE_ID, long _ImpMestSttId)
        {
            try
            {
                this.ImpMestId = _ImpMestId;
                this.ImpMestSttId = _ImpMestSttId;
                this.IMP_MEST_TYPE_ID = _IMP_MEST_TYPE_ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
