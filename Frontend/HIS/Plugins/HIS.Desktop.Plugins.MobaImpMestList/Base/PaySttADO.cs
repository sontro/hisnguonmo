using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaImpMestList.Base
{
    class PaySttADO
    {
        public string STT_NAME { get; set; }
        /// <summary>
        /// 1:thanh toán, 2: đang thanh toán, 3: chưa thanh toán
        /// </summary>
        public long ID { get; set; }

        public PaySttADO(long id, string name)
        {
            this.ID = id;
            this.STT_NAME = name;
        }
    }
}
