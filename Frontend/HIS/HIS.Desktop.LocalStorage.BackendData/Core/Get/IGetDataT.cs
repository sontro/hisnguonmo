using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    interface IGetDataT
    {
        object Execute<T>();
    }

    //interface IDelegacyT
    //{
    //    T Execute<T>();
    //}
}
