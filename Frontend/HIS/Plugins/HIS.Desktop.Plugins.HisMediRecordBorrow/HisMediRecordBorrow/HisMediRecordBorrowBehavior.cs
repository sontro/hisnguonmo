using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisMediRecordBorrow.HisMediRecordBorrow
{
    class HisMediRecordBorrowBehavior : Tool<IDesktopToolContext>, IHisMediRecordBorrow
    {
        object[] entity;
        internal HisMediRecordBorrowBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMediRecordBorrow.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long treatmentBorrowTypeId = 0;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is long)
                        {
                            treatmentBorrowTypeId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null && treatmentBorrowTypeId == 0)
                {
                    return new UCHisMediRecordBorrow(moduleData);
                }
                else if (moduleData != null && treatmentBorrowTypeId > 0)
                {
                    return new frmHisMediRecordBorrow(moduleData, treatmentBorrowTypeId);
                }
                else
                {
                    return null;
                }
                //return new UCHisMediRecordBorrow();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
