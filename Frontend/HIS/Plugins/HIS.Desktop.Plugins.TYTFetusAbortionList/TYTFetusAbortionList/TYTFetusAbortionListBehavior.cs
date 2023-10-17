using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.FetusAbortionList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.FetusAbortionList.TYTFetusAbortionList
{
    public sealed class TYTFetusAbortionListBehavior : Tool<IDesktopToolContext>, ITYTFetusAbortionList
    {
        Inventec.Desktop.Common.Modules.Module moduleData;

        public TYTFetusAbortionListBehavior()
            : base()
        {
        }

        public TYTFetusAbortionListBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object ITYTFetusAbortionList.Run()
        {
            try
            {
                return new UCTYTFetusAbortionList(this.moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
