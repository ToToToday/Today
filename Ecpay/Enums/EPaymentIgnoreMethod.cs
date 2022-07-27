using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpay.Enums
{
    /// <summary>
    /// 隱藏付款方式。
    /// </summary>
    [Flags]
    public enum EPaymentIgnoreMethod
    {
        Credit = 0,
        WebATM = 1,
        ATM = 2,
        CVS = 4,
        BARCODE = 8
    }
}
