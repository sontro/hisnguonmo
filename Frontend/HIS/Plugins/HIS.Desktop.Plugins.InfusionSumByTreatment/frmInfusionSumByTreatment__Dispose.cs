using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfusionSumByTreatment
{
    public partial class frmInfusionSumByTreatment : FormBase
    {
        public override void ProcessDisposeModuleDataAfterClose()
        {
            try
            {
                IsTreatmentList = false;
                positionHandleControl = 0;
                treatment = null;
                treatmentId = 0;
                currentModule = null;
                currentInfusionSum = null;
                ucIcd = null;
                icdProcessor = null;
                this.bbtnRCSave.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRCSave_ItemClick);
                this.bbtnRCNew.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRCNew_ItemClick);
                this.bbtnRCChoiceIcd.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRCChoiceIcd_ItemClick);
                this.bbtnEdit.ItemClick -= new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnEdit_ItemClick);
                this.btnNew.Click -= new System.EventHandler(this.btnNew_Click);
                this.btnSave.Click -= new System.EventHandler(this.btnSave_Click);
                this.txtIcdSubCode.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtIcdSubCode_PreviewKeyDown);
                this.txtIcdSubCode.Validating -= new System.ComponentModel.CancelEventHandler(this.txtIcdSubCode_Validating);
                this.txtIcdText.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtIcdText_PreviewKeyDown);
                this.gridViewInfusionSum.RowClick -= new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewInfusionSum_RowClick);
                this.gridViewInfusionSum.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewInfusionSum_CustomRowCellEdit);
                this.gridViewInfusionSum.CustomUnboundColumnData -= new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewInfusionSum_CustomUnboundColumnData);
                this.gridViewInfusionSum.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.gridViewInfusionSum_KeyDown);
                this.ButtonEdit_Edit.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonEdit_Edit_ButtonClick);
                this.repositoryItemBtnPrint.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemBtnPrint_ButtonClick);
                this.repositoryItemBtnViewDetail.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemBtnViewDetail_ButtonClick);
                this.repositoryItemBtnDelete.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemBtnDelete_ButtonClick);
                this.dxValidationProvider1.ValidationFailed -= new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(this.dxValidationProvider1_ValidationFailed);
                this.Load -= new System.EventHandler(this.frmInfusionSumByTreatment_Load);
                gridViewInfusionSum.GridControl.DataSource = null;
                gridControlInfusionSum.DataSource = null;
                repositoryItemBtnViewDetailDisable = null;
                dxValidationProvider1 = null;
                gridColumn_InfusionSum_Note = null;
                lciNote = null;
                txtNote = null;
                bbtnEdit = null;
                ButtonEdit_Edit = null;
                gridColumn_InfusionSum_Edit = null;
                bbtnRCChoiceIcd = null;
                emptySpaceItem2 = null;
                layoutControlItem2 = null;
                panelControlUcIcd = null;
                barDockControlRight = null;
                barDockControlLeft = null;
                barDockControlBottom = null;
                barDockControlTop = null;
                bbtnRCNew = null;
                bbtnRCSave = null;
                bar1 = null;
                barManager1 = null;
                repositoryItemBtnDeleteDisable = null;
                repositoryItemBtnDelete = null;
                repositoryItemBtnViewDetail = null;
                repositoryItemBtnPrint = null;
                gridColumn_InfusionSum_Modifier = null;
                gridColumn_InfusionSum_ModifyTime = null;
                gridColumn_InfusionSum_Creator = null;
                gridColumn_InfusionSum_CreateTime = null;
                gridColumn_InfusionSum_Room = null;
                gridColumn_InfusionSum_Department = null;
                gridColumn_InfusionSum_IcdText = null;
                gridColumn_InfusionSum_IcdMain = null;
                gridColumn_InfusionSum_ViewDetail = null;
                gridColumn_InfusionSum_Print = null;
                gridColumn_InfusionSum_Delete = null;
                gridColumn_InfusionSum_Stt = null;
                emptySpaceItem1 = null;
                layoutControlItem6 = null;
                layoutControlItem4 = null;
                layoutIcdSubCode = null;
                txtIcdSubCode = null;
                btnSave = null;
                btnNew = null;
                layoutIcdText = null;
                layoutControlItem1 = null;
                gridViewInfusionSum = null;
                gridControlInfusionSum = null;
                txtIcdText = null;
                layoutControlGroup1 = null;
                layoutControl1 = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
