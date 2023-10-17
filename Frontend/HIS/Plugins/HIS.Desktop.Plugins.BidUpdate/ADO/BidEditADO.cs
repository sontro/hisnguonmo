using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidUpdate.ADO
{
    public class BidEditADO
    {
        public Delete_ButtonClick delete_ButtonClick { get; set; }
        public Grid_Click grid_Click { get; set; }
        public List<ADO.MedicineTypeADO> listADOs { get; set; }
        public int TYPE { get; set; }
    }
}
