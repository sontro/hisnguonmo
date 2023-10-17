using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.BedMapView.Base
{
    class Bed : Label
    {
        public int GridX { get; set; }
        public int GridY { get; set; }

        public virtual void Remove()
        {
            this.Dispose();
        }

    }
}
