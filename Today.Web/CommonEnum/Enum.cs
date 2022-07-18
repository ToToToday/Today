using System.ComponentModel;

namespace Today.Web.Services.CommonEnum
{
    public class Enum
    {
        public enum PartnerType
        {
            [Description("家庭旅遊")]
            Family = 1,
            [Description("情侶旅遊")]
            Couple = 2,
            [Description("獨自出遊")]
            Personal = 3,
            [Description("朋友旅遊")]
            friend = 4,
        }
    }
}
