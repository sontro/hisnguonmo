using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.PACS.ADO
{
    class ImageResponseADO
    {
        public ImageResponseADO() { }
        public List<SeriesADO> Series { get; set; }
        public bool TrangThai { get; set; }
        public string ThongBao { get; set; }
    }
}
