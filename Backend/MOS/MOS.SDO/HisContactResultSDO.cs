using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisContactResultSDO
    {
        //Nguoi tiep xuc
        public HIS_CONTACT_POINT ContactPoint { get; set; }
        //Thong tin tiep xuc
        public HIS_CONTACT ContactInfo { get; set; }
    }
}
