using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpay.Enums
{
    /// <summary>
    /// 付款方式。
    /// </summary>
    public enum EPaymentMethod
    {
        Credit,
        Union,
        WebATM,
        ATM,
        CVS,
        BARCODE,
        ALL,
    }
}
