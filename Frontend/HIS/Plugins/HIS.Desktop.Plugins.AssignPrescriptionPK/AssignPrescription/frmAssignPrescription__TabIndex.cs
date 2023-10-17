using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription
{
    public partial class frmAssignPrescription : HIS.Desktop.Utility.FormBase
    {

        /// <summary>
        /// Init set tab index
        /// </summary>
        private void InitTabIndex()
        {
            try
            {
                //dicOrderTabIndexControl.Add("lkAccidentHurtTypeId", 0);
                //dicOrderTabIndexControl.Add("txtContent", 0);
                //dicOrderTabIndexControl.Add("lkTreatmentId", 0);
                //dicOrderTabIndexControl.Add("chkUseAlcohol", 0);
                //dicOrderTabIndexControl.Add("dtAccidentTime", 0);
                //dicOrderTabIndexControl.Add("lkAccidentCareId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentResultId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentHelmetId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentVehicleId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentPoisonId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentLocationId", 0);
                //dicOrderTabIndexControl.Add("lkAccidentBodyPartId", 0);

                //dicOrderTabIndexControl.Add("txtPatientCode", 0);
                //dicOrderTabIndexControl.Add("txtPatientName", 1);

                if (dicOrderTabIndexControl != null && dicOrderTabIndexControl.Count > 0)
                {
                    foreach (KeyValuePair<string, int> itemOrderTab in dicOrderTabIndexControl)
                    {
                        //SetTabIndexToControl(itemOrderTab, lcEditorInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private bool SetTabIndexToControl(KeyValuePair<string, int> itemOrderTab, DevExpress.XtraLayout.LayoutControl layoutControlEditor)
        {
            bool success = false;
            try
            {
                if (!layoutControlEditor.IsInitialized) return success;
                layoutControlEditor.BeginUpdate();
                try
                {
                    foreach (DevExpress.XtraLayout.BaseLayoutItem item in layoutControlEditor.Items)
                    {
                        DevExpress.XtraLayout.LayoutControlItem lci = item as DevExpress.XtraLayout.LayoutControlItem;
                        if (lci != null && lci.Control != null)
                        {
                            BaseEdit be = lci.Control as BaseEdit;
                            if (be != null)
                            {
                                //Cac control dac biet can fix khong co thay doi thuoc tinh enable
                                if (itemOrderTab.Key.Contains(be.Name))
                                {
                                    be.TabIndex = itemOrderTab.Value;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    layoutControlEditor.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return success;
        }

    }
}
