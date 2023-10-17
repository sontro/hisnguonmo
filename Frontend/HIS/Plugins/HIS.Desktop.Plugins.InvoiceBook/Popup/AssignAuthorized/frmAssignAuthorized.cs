using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.InvoiceBook.Popup.AssignAuthorized
{
    public partial class frmAssignAuthorized : Form
    {
        public V_HIS_INVOICE_BOOK HisInvoiceBookInUc = new V_HIS_INVOICE_BOOK();
        List<V_HIS_USER_INVOICE_BOOK> _listUsersInvoiceBook = new List<V_HIS_USER_INVOICE_BOOK>();
        List<ACS_USER> _listUsers = new List<ACS_USER>();
        List<ACS_USER> _listUsersTemporary = new List<ACS_USER>();
        
        public frmAssignAuthorized()
        {
            InitializeComponent();
        }
    }
}
