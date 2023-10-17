using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisBedMapping.ADO
{
    public class Bed : Label
    {
        public long Id { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }

        public virtual void Remove()
        {
            this.Dispose();
        }
    }
}
