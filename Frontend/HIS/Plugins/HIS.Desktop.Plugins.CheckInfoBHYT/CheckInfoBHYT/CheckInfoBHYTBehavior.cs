using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CheckInfoBHYT;
using HIS.Desktop.ADO;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.CheckInfoBHYT.CheckInfoBHYT
{
    public sealed class CheckInfoBHYTBehavior : Tool<IDesktopToolContext>, ICheckInfoBHYT
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public CheckInfoBHYTBehavior()
            : base()
        {
        }

        public CheckInfoBHYTBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICheckInfoBHYT.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    long _patientId = 0;
                    CheckInfoBhytADO _ado = null;
                    HIS.Desktop.Common.DelegateRefreshData _dlg = null;
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            _patientId = (long)item;
                        }
                        else if (item is CheckInfoBhytADO)
                        {
                            _ado = (CheckInfoBhytADO)item;
                        }
                        else if (item is HIS.Desktop.Common.DelegateRefreshData)
                        {
                            _dlg = (HIS.Desktop.Common.DelegateRefreshData)item;
                        }
                    }
                    if (currentModule != null && _patientId > 0)
                    {
                        if (_dlg != null)
                            result = new frmCheckInfoBHYT(currentModule, _patientId);
                        else
                            result = new frmCheckInfoBHYT(currentModule, _patientId);
                    }
                    else if (currentModule != null && _ado != null)
                    {
                        if (_dlg != null)
                            result = new frmCheckInfoBHYT(currentModule, _ado);
                        else
                            result = new frmCheckInfoBHYT(currentModule, _ado);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
