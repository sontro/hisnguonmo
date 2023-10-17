using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Popup
{
    public partial class frmPayment : FormBase
    {
        Action<bool> delegateReturn;

        public frmPayment(decimal totalPatientPrice, decimal amountPaid, decimal unpaidAmout, decimal balance, Action<bool> delegateReturn)
        {
            InitializeComponent();
            this.delegateReturn = delegateReturn;
            try
            {
                FillDataToControl(totalPatientPrice, amountPaid, unpaidAmout, balance);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void FillDataToControl(decimal totalPatientPrice, decimal amountPaid, decimal unpaidAmout, decimal balance)
        {
            try
            {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                lblAmountPaid.Text = amountPaid.ToString("#,###", cul.NumberFormat);
                lblBalance.Text = balance.ToString("#,###", cul.NumberFormat);
                lblTotalPatientPrice.Text = totalPatientPrice.ToString("#,###", cul.NumberFormat);
                lblUnpaidAmout.Text = unpaidAmout.ToString("#,###", cul.NumberFormat);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                this.delegateReturn(true);
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.delegateReturn(false);
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.btnNo.Click -= new System.EventHandler(this.btnNo_Click);
                this.btnYes.Click -= new System.EventHandler(this.btnYes_Click);
                emptySpaceItem1 = null;
                layoutControlItem7 = null;
                layoutControlItem6 = null;
                layoutControlItem5 = null;
                layoutControlItem4 = null;
                layoutControlItem3 = null;
                layoutControlItem2 = null;
                layoutControlItem1 = null;
                lblTotalPatientPrice = null;
                lblAmountPaid = null;
                lblUnpaidAmout = null;
                lblBalance = null;
                labelControl5 = null;
                btnYes = null;
                btnNo = null;
                s = null;
                layoutControl1 = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        //string FormatCurrency(decimal parameter)
        //{
        //    string val = "0";
        //    try
        //    {
        //        DecimalFormat formatter = new DecimalFormat("###,###,###.##");


        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Warn(ex);
        //    }
        //}
    }
}
