using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AnticipateDetail
{
    public partial class frmAnticipateDetail : Form
    {
        private MOS.EFMODEL.DataModels.HIS_ANTICIPATE anticipate;
        //private List<MSS.SDO.HisAnticipateMetySdo> anticipateMetiePrints;
        //private List<MOS.EFMODEL.DataModels.HIS_ANTICIPATE_METY> anticipateMetieprints;
        private List<MSS.SDO.HisAnticipateMetySdo> anticipateMetiePrints;
        Dictionary<string, object> dicParam;
        public frmAnticipateDetail()
        {
            InitializeComponent();
        }
        public frmAnticipateDetail(MOS.EFMODEL.DataModels.HIS_ANTICIPATE Anticipate)		
        {
            try
            {
                InitializeComponent();
                this.anticipate = Anticipate;
                anticipateMetiePrints = new List<MSS.SDO.HisAnticipateMetySdo>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }


        }

        private void frmAnticipateDetail_Load(object sender, EventArgs e)
        {
            loadDataToGridControlMety();
            //loadDataToGridControlMaty();
            //loadDataToGridControlBlty();
            //initButtonPrint();
        }
        private void loadDataToGridControlMety()
        {
            try
            {
                if (this.anticipate == null)
                {
                    return;
                }
                //MSS.MANAGER.AnticipateMety.HisAnticipateMetyLogic anticipateLogic = new MANAGER.AnticipateMety.HisAnticipateMetyLogic();
                //MOS.Filter.HisAnticipateMetyViewFilter filter = new MOS.Filter.HisAnticipateMetyViewFilter();
                MOS.EFMODEL.DataModels.V_HIS_ANTICIPATE_METY anticipateLogic = new MOS.EFMODEL.DataModels.V_HIS_ANTICIPATE_METY();
                MOS.Filter.HisAnticipateMetyViewFilter filter = new MOS.Filter.HisAnticipateMetyViewFilter();
                filter.ANTICIPATE_ID = this.anticipate.ID;
                //var anticipateMeties = anticipateLogic.GetView(filter);
                var anticipateMeties = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_ANTICIPATE_METY>(filter);
                if (anticipateMeties != null && anticipateMeties.Count > 0)
                {
                    foreach (var item in anticipateMeties)
                    {
                        MSS.SDO.HisAnticipateMetySdo aAnticipateMety = new MSS.SDO.HisAnticipateMetySdo();
                       // MOS.EFMODEL.DataModels.V_HIS_ANTICIPATE_METY aAnticipateMety = new MOS.EFMODEL.DataModels.V_HIS_ANTICIPATE_METY();
                        Inventec.Common.Mapper.DataObjectMapper.Map<MOS.EFMODEL.DataModels.V_HIS_ANTICIPATE_METY>(aAnticipateMety, item);
                        aAnticipateMety.TotalMoney = item.AMOUNT * (item.IMP_PRICE ?? 0);
                        anticipateMetiePrints.Add(aAnticipateMety);
                    }
                }

                //gridControlAnticipateMety.DataSource = anticipateMeties;
                gridControl1.DataSource = anticipateMeties;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
