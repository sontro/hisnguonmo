using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HIS.Desktop.Plugins.XMLViewer130
{
    public partial class UCXml130 : UserControl
    {
        public List<object> dataSourceXMLGrid = new List<object>();
        public UCXml130()
        {
            InitializeComponent();
        }

        public void LoadList(List<object> listObj)
        {
            try
            {
                dataSourceXMLGrid.AddRange(listObj);
                gridControllXml.DataSource = dataSourceXMLGrid;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewXml_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.Column.ColumnType.Name == typeof(XmlCDataSection).Name)
                {
                    e.DisplayText = (e.Value as XmlCDataSection).Value;
                    e.Column.OptionsColumn.AllowEdit = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
