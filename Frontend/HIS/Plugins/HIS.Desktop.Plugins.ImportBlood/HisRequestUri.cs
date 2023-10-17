using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood
{
   internal class HisRequestUri
    {
       public const string HIS_MANU_IMP_MEST_CREATE = "api/HisImpMest/ManuCreate";
       public const string HIS_MANU_IMP_MEST_UPDATE = "api/HisImpMest/ManuUpdate";
       public const string HIS_INIT_IMP_MEST_CREATE = "api/HisImpMest/InitCreate";
       public const string HIS_INIT_IMP_MEST_UPDATE = "api/HisImpMest/InitUpdate";
       public const string HIS_INVE_IMP_MEST_CREATE = "api/HisImpMest/InveCreate";
       public const string HIS_INVE_IMP_MEST_UPDATE = "api/HisImpMest/InveUpdate";
       public const string HIS_OTHER_IMP_MEST_CREATE = "api/HisImpMest/OtherCreate";
       public const string HIS_OTHER_IMP_MEST_UPDATE = "api/HisImpMest/OtherUpdate";
       public const string HIS_DONATION_IMP_MEST_CREATE = "api/HisImpMest/DonationCreate";
       public const string HIS_DONATION_IMP_MEST_UPDATE = "api/HisImpMest/DonationUpdate";

       public const string HIS_HIS_BLTY_VOLUME_GET = "api/HisBltyVolume/Get";
    }
}
