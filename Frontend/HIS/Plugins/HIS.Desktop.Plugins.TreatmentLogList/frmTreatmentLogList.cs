using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentLogList
{
 
 public partial class frmTreatmentLogList : HIS.Desktop.Utility.FormBase
 {
long TreatmentId = 0;
CommonParam param = new CommonParam();
Inventec.Desktop.Common.Modules.Module module;
long currentRoomId = 0;
public frmTreatmentLogList(Inventec.Desktop.Common.Modules.Module Module, long treatmentId, long currentRoomId)
:base(Module)
  {
   TreatmentId = treatmentId;
   module = Module;
   this.currentRoomId = currentRoomId;
   InitializeComponent();

   UCTreatmentProcessPartial UCtreatmentProcessPartial = new UCTreatmentProcessPartial(module, TreatmentId, currentRoomId);
   this.xtraUserControl1.Controls.Add(UCtreatmentProcessPartial);
UCtreatmentProcessPartial.Dock= DockStyle.Fill;

  }

 
 }
}
