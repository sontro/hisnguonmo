using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPacsOne.HisPacsOne
{
    class HisPacsOneBehavior : BusinessBase, IHisPacsOne
    {
        object[] entity;
        internal HisPacsOneBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisPacsOne.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData _delegateSelect = null;
                List<string> inputFilePaths = null;
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            else if (entity[i] is DelegateSelectData)
                            {
                                _delegateSelect = (DelegateSelectData)entity[i];
                            }
                            else if (entity[i] is string)
                            {
                                if (inputFilePaths == null) inputFilePaths = new List<string>();
                                inputFilePaths.Add((string)entity[i]);
                            }
                        }
                    }
                }

                return new frmHisPacsOne(moduleData, _delegateSelect, (inputFilePaths != null ? inputFilePaths.ToArray() : null));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
