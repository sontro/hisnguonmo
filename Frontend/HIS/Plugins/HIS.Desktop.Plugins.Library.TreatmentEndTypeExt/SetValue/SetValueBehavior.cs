using HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Run;
using HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.SickLeave;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.SetValue
{
    public sealed class SetValueBehavior : ISetValue
    {
        Form frm;
        object entity;
        public SetValueBehavior()
            : base()
        {
        }

        public SetValueBehavior(CommonParam param, Form frm, object data)
            : base()
        {
            this.frm = frm;
            this.entity = data;
        }

        void ISetValue.Run(FormEnum.TYPE type)
        {
            try
            {
                if (type == FormEnum.TYPE.SICK_LEAVE)
                {
                    ((frmSickLeave)frm).SetValue(entity);
                }
                else if (type == FormEnum.TYPE.MATERNITY_LEAVE)
                {

                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
