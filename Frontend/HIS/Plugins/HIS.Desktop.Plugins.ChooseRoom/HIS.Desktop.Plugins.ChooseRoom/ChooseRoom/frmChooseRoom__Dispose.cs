using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ChooseRoom.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Controls.PopupLoader;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ChooseRoom.ChooseRoom
{
    public partial class frmChooseRoom : HIS.Desktop.Utility.FormBase
    {
        public override void ProcessDisposeModuleDataAfterClose()
        {
            try
            {
                refeshReference = null;
                currentUserRooms = null;
                currentUserRoomsByBranch = null;
                currentBranchs = null;
                currentDesks = null;
                currentWorkingShifts = null;
                currentNurses = null;
                statecheckColumn = false;
                MustChooseWorkingShift = false;
                groupRoomOption = null;
                positionHandleControl = 0;
                this.txtLoginName.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtLoginName_PreviewKeyDown);
                this.cboNurse.Closed -= new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cboNurse_Closed);
                this.cboNurse.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboNurse_ButtonClick);
                this.cboNurse.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.cboNurse_KeyUp);
                this.cboWorkingShift.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.cboWorkingShift_KeyUp);
                this.cboDepartment.Closed -= new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cboDepartment_Closed);
                this.cboDepartment.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboDepartment_ButtonClick);
                this.cboDepartment.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.cboDepartment_KeyUp);
                this.btnChoice.Click -= new System.EventHandler(this.btnChoice_Click);
                this.cboBranch.Closed -= new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cboBranch_Closed);
                this.cboBranch.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.cboBranch_KeyUp);
                this.txtKeyword.TextChanged -= new System.EventHandler(this.txtKeyword_TextChanged);
                this.txtKeyword.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtKeyword_PreviewKeyDown);
                this.gridControlRooms.ProcessGridKey -= new System.Windows.Forms.KeyEventHandler(this.gridControlRooms_ProcessGridKey);
                this.gridViewRooms.CustomDrawGroupRow -= new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridViewRooms_CustomDrawGroupRow);
                this.gridViewRooms.RowCellStyle -= new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewRooms_RowCellStyle);
                this.gridViewRooms.ShownEditor -= new System.EventHandler(this.gridViewRooms_ShownEditor);
                this.gridViewRooms.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewRooms_CellValueChanged);
                this.gridViewRooms.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.gridViewRooms_KeyDown);
                this.gridViewRooms.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.gridViewRooms_MouseDown);
                this.gridViewRooms.DoubleClick -= new System.EventHandler(this.gridViewRooms_DoubleClick);
                this.dxValidationProviderControl.ValidationFailed -= new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(this.dxValidationProviderControl_ValidationFailed);
                this.barButtonItem__Save.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnCtrlS_ItemClick);
                this.barButtonItem__UncheckAll.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnCtrlF1_ItemClick);
                this.Load -= new System.EventHandler(this.frmChooseRoom_Load);
                this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.frmChooseRoom_KeyDown);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
