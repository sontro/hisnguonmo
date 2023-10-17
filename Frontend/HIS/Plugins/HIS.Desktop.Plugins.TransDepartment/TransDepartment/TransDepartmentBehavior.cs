using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TransDepartment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.TransDepartment.TransDepartment;


namespace HIS.Desktop.Plugins.TransDepartment.TransDepartment
{
    public sealed class TransDepartmentBehavior : Tool<IDesktopToolContext>, ITransDepartment
    {

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        RefeshReference RefeshReference;
        DelegateReturnSuccess DelegateReturnSuccess;
        TransDepartmentADO data = null;
        V_HIS_DEPARTMENT_TRAN departmentTran = null;
        bool isView = false;
        public TransDepartmentBehavior()
            : base()
        {
        }

        public TransDepartmentBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, TransDepartmentADO data, RefeshReference RefeshReference, DelegateReturnSuccess DelegateReturnSuccess, V_HIS_DEPARTMENT_TRAN departmentTran, bool _isView)
            : base()
        {
            this.moduleData = moduleData;
            this.data = data;
            this.departmentTran = departmentTran;
            this.RefeshReference = RefeshReference;
            this.DelegateReturnSuccess = DelegateReturnSuccess;
            this.isView = _isView;
        }

        public TransDepartmentBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, TransDepartmentADO data, RefeshReference RefeshReference, DelegateReturnSuccess DelegateReturnSuccess)
            : base()
        {
            this.moduleData = moduleData;
            this.data = data;
            this.RefeshReference = RefeshReference;
            this.DelegateReturnSuccess = DelegateReturnSuccess;
        }

        object ITransDepartment.Run()
        {
            try
            {
                if (this.departmentTran != null)
                {
                    return new frmDepartmentTran(moduleData, data
            , RefeshReference, DelegateReturnSuccess, this.departmentTran, isView);
                }
                else
                {
                    return new frmDepartmentTran(moduleData, data
            , RefeshReference, DelegateReturnSuccess);
                }
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
