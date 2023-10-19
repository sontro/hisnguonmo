using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsUserFilter : FilterBase
    {
        public string EMAIL { get; set; }
        public string LOGINNAME { get; set; }
        public string LOGINNAME__OR__SUB_LOGINNAME { get; set; }
        public string CN_WORD { get; set; }
        public string KEY_WORD { get; set; }
        public List<string> LOGINNAMEs { get; set; }
        public List<string> LOGINNAME__OR__SUB_LOGINNAMEs { get; set; }
        public bool? IS_NOT_PEOPLE { get; set; }//For THE
        public AcsUserFilter()
            : base()
        {
        }
    }
}
