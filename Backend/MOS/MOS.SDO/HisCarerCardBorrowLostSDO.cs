﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCarerCardBorrowLostSDO
    {
        public long CarerCardBorrowId { get; set; }
        public long GiveBackTime { get; set; }
        public long RequestRoomId { get; set; }  // phong dang lam viec
    }
}
