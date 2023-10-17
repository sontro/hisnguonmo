using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.Plugins.GenerateRegisterOrder.ADO;

namespace HIS.Desktop.Plugins.GenerateRegisterOrder
{
    public partial class UCItem : UserControl
    {
        public long SizeTitle { get; set; }
        public long SizeStt { get; set; }
        public string TextStt { get; set; }
        public string TextTitle { get; set; }
        private List<HIS_REGISTER_GATE> lstGate { get; set; }
        private List<HisRegisterGateADO> lstSend { get; set; }
        public event EventHandler _Click;
        public UCItem(List<HIS_REGISTER_GATE> lst, List<HisRegisterGateADO> lstSend)
        {
            InitializeComponent();
            try
            {
                lstGate = lst;
                lstSend = lstSend;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UCItem_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", SizeTitle, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                this.lblTitleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", SizeStt, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", SizeStt, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                this.lblName.Text = TextTitle;
                this.lblNumber.Text = TextStt;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            if (_Click != null)
            {
                lblNumber_Click(lblNumber, e);
            }
        }

        private void lblTitleNumber_Click(object sender, EventArgs e)
        {
            if (_Click != null)
            {
                lblNumber_Click(lblNumber, e);
            }
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
            if (_Click != null)
            {
                Label lb = sender as Label;
                lb.Tag = this.Tag;
                _Click.Invoke(lb, e);
            }
        }

     

    }
}
