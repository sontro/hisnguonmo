using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ExpMestViewDetailADO
    {
        public long ExpMestId { get; set; }
        public long ExpMestTypeId { get; set; }
        public long ExpMestStt { get; set; }

        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="_ExpMestId">id phieu xuat</param>
        /// <param name="_ExpMestTypeId">id loai xuat</param>
        /// /// <param name="_ExpMestTypeId">id trang thai</param>
        public ExpMestViewDetailADO(long _ExpMestId, long _ExpMestTypeId, long _ExpMestStt)
        {
            try
            {
                this.ExpMestId = _ExpMestId;
                this.ExpMestTypeId = _ExpMestTypeId;
                this.ExpMestStt = _ExpMestStt;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
