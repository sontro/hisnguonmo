using System.Collections.Generic;

namespace MOS.Filter
{
    public class DHisTransExpFilter
    {
        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public string KEY_WORD { get; set; }
		public List<long> TYPE_IDs {get;set;}
		
		public long CASHIER_ROOM_ID {get;set;}
		public long MEDI_STOCK_ID {get;set;}
		public long? CREATE_TIME_FROM {get;set;}
		public long? CREATE_TIME_TO {get;set;}
		public long? TYPE_ID {get;set;}
        public long? NUM_ORDER__EQUAL { get; set; }
		
		public string TREATMENT_CODE__EXACT { get; set; }
        public string ACCOUNT_BOOK_CODE__EXACT { get; set; }
        public string EXP_MEST_CODE__EXACT { get; set; }
		public string ACCOUNT_BOOK_NAME {get;set;}
		public string TEMPLATE_CODE {get;set;}
		public string SYMBOL_CODE {get;set;}

        public DHisTransExpFilter()
            : base()
        {
        }
    }
}
