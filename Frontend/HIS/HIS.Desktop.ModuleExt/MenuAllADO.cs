using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ModuleExt
{
    public class MenuAllADO
    {
        public MenuAllADO()
        {

        }
        public MenuAllADO(List<Inventec.Desktop.Common.Modules.Module> moduleAllInPages, ImageCollection imageCollection)
        {
            this.ModuleAllInPages = moduleAllInPages;
            this.ImageCollection = imageCollection;
        }
        public ImageCollection ImageCollection { get; set; }
        public List<Inventec.Desktop.Common.Modules.Module> ModuleAllInPages { get; set; }
    }
}
