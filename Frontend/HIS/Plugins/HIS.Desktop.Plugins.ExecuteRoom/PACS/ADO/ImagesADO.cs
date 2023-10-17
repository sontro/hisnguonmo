using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.PACS.ADO
{

    class ImagesADO
    {

        public ImagesADO() { }

        public int ImageNumber { get; set; }

        public string ImageDirectory { get; set; }

        public string ImageDcmFileName { get; set; }

        public string ImageThumbFileName { get; set; }

        public string BodyPart { get; set; }

        public string ViewPosition { get; set; }

        public string ReceivedDate { get; set; }

        public string ImageDateTime { get; set; }       
        public string SeriesDateTime { get; set; }

        public int SeriesInstances { get; set; }

        public int SeriesNumber { get; set; }

    }
}
