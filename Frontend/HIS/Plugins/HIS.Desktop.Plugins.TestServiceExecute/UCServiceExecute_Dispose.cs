using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceExecute
{
    public partial class UCServiceExecute : HIS.Desktop.Utility.UserControlBase
    {
        public override void ProcessDisposeModuleDataAfterClose()
        {
            try
            {
                ServiceReqConstruct = null;
                sereServ = null;
                sereServExt = null;
                currentSarPrint = null;
                currentServiceReq = null;
                moduleData = null;
                listTemplate = null;
                dicParam = null;
                dicImage = null;
                listImage = null;
                RefreshData = null;
                imageLoad = null;
                listServiceADO = null;
                mainSereServ = null;
                listServiceADOForAllInOne = null;
                ekipUserAdos = null;
                dicSereServExt = null;
                dicSarPrint = null;
                patient = null;
                TreatmentWithPatientTypeAlter = null;
                UserName = null;
                thoiGianKetThuc = null;
                HideTimePrint = null;
                ConnectPacsByFss = null;
                ConnectImageOption = null;
                fss = null;
                ListMachine = null;
                ListServiceMachine = null;
                SERVICE_TYPE_IDs = null;

                this.BtnChangeImage.Click -= new System.EventHandler(this.BtnChangeImage_Click);
                this.BtnChooseImage.Click -= new System.EventHandler(this.BtnChooseImage_Click);
                this.BtnDeleteImage.Click -= new System.EventHandler(this.BtnDeleteImage_Click);
                this.BtnEmr.Click -= new System.EventHandler(this.BtnEmr_Click);
                this.txtNumberOfFilm.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtNumberOfFilm_KeyPress);
                this.txtNumberOfFilm.Validating -= new System.ComponentModel.CancelEventHandler(this.txtNumberOfFilm_Validating);
                this.btnSaveNClose.Click -= new System.EventHandler(this.btnSaveNClose_Click);
                this.btnTuTruc.Click -= new System.EventHandler(this.btnTuTruc_Click);
                this.cboSereServTemp.Closed -= new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cboSereServTemp_Closed);
                this.cboSereServTemp.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboSereServTemp_ButtonClick);
                this.cboSereServTemp.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.cboSereServTemp_PreviewKeyDown);
                this.txtSereServTempCode.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtSereServTempCode_PreviewKeyDown);
                this.gridViewSereServ.RowClick -= new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
                this.gridViewSereServ.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView_CustomRowCellEdit);
                this.gridViewSereServ.ShownEditor -= new System.EventHandler(this.gridView_ShownEditor);
                this.gridViewSereServ.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
                this.gridViewSereServ.DoubleClick -= new System.EventHandler(this.gridView_DoubleClick);
                this.repositoryItemButtonServiceReqMaty.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonServiceReqMaty_ButtonClick);
                this.repositoryItemButtonSendSancy.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonSendSancy_ButtonClick);
                this.repositoryItembtnTraKqSA.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItembtnTraKqSA_ButtonClick);
                this.repositoryItemMachineId.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemMachineId_ButtonClick);
                this.btnAssignPrescription.Click -= new System.EventHandler(this.btnAssignPrescription_Click);
                this.btnAssignService.Click -= new System.EventHandler(this.btnAssignService_Click);
                this.btnSave.Click -= new System.EventHandler(this.btnSave_Click);
                this.btnFinish.Click -= new System.EventHandler(this.btnFinish_Click);
                this.btnPrint.Click -= new System.EventHandler(this.btnPrint_Click);
                this.btnSereServTempList.Click -= new System.EventHandler(this.btnSereServTempList_Click);
                this.txtNote.Validating -= new System.ComponentModel.CancelEventHandler(this.txtNote_Validating);
                this.txtConclude.Validating -= new System.ComponentModel.CancelEventHandler(this.txtConclude_Validating);
                this.tileView1.ItemClick -= new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tileView1_ItemClick);
                this.tileView1.ItemDoubleClick -= new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tileView1_ItemDoubleClick);
                this.cardView.Click -= new System.EventHandler(this.cardView_Click);
                this.dxValidationProvider1.ValidationFailed -= new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(this.dxValidationProvider1_ValidationFailed);
                this.btnAssignPaan.Click -= new System.EventHandler(this.btnAssignPaan_Click);
                this.Load -= new System.EventHandler(this.UCServiceExecute_Load);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
