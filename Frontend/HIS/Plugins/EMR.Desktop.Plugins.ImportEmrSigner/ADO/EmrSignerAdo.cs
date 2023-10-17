using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.ImportEmrSigner.ADO
{
    class EmrSignerAdo : EMR.EFMODEL.DataModels.EMR_SIGNER
    {
        public long IdRow { get; set; }
        public string NUM_ORDER_STR { get; set; }
        public string SIGN_IMAGE_STR { get; set; }
        public Image IMAGE_SIGN { get; set; }

        public string ERROR { get; set; }
    }
}
