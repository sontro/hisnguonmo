using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceChangeReqList.ADO
{
    internal class FilterTypeADO
    {
        public enum stt
        {
            moi = 1,
            duyetChiDinh = 2,
            duyetPhieuThu = 3
        };

        public stt id { get; set; }
        public string Name { get; set; }

        public FilterTypeADO(stt _id, string _Name)
        {
            this.id = _id;
            this.Name = _Name;
        }
    }
}
