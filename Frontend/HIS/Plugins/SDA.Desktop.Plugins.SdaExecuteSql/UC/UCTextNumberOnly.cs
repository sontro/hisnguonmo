using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Inventec.Desktop.Common.Controls.ValidationRule;
using DevExpress.XtraEditors.DXErrorProvider;
using SDA.EFMODEL.DataModels;
using DevExpress.XtraEditors.ViewInfo;

namespace SDA.Desktop.Plugins.SdaExecuteSql.UC
{
    public partial class UCTextNumberOnly : UserControl
    {
        SDA_SQL_PARAM Data = new SDA_SQL_PARAM();
        int positionHandleControl = -1;

        public UCTextNumberOnly(SDA_SQL_PARAM data)
        {
            InitializeComponent();
            this.Data = data;
        }

        private void UCTextNumberOnly_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbNote.Text = this.Data.NOTE;
                lciTextNumberOnly.Text = this.Data.SQL_PARAM_NAME + ':';
                this.lciTextNumberOnly.OptionsToolTip.ToolTip = this.Data.SQL_PARAM_NAME;
                if (this.Data.IS_REQUIRED == 1)
                {
                    this.lciTextNumberOnly.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
                    ValidateControl();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateControl()
        {
            try
            {
                ControlEditValidationRule validRule = new ControlEditValidationRule();
                validRule.editor = txtTextNumberOnly;
                validRule.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProviderEditorInfo.SetValidationRule(txtTextNumberOnly, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal valueADO GetValue()
        {
            valueADO data = new valueADO();
            data.ID = this.Data.ID;
            data.value = txtTextNumberOnly.Text;
            return data;
        }

        internal bool validate()
        {
            positionHandleControl = -1;
            return dxValidationProviderEditorInfo.Validate();
        }

        private void dxValidationProviderEditorInfo_ValidationFailed(object sender, ValidationFailedEventArgs e)
        {
            try
            {
                BaseEdit edit = e.InvalidControl as BaseEdit;
                if (edit == null)
                    return;
                BaseEditViewInfo viewInfo = edit.GetViewInfo() as BaseEditViewInfo;
                if (viewInfo == null)
                    return;
                if (positionHandleControl == -1)
                {
                    positionHandleControl = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
                if (positionHandleControl > edit.TabIndex)
                {
                    positionHandleControl = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.Focus();
                        edit.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void txtTextNumberOnly_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
