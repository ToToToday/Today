using System.ComponentModel;

namespace Today.Web.CommonEnum
{
    //可以不用放在class裡面
    public class AllEnum
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
            [Description("商務旅遊")]
            business = 5,
        }

        public enum LoginWayType
        {
            [Description("Google")]
            Google= 3,
            [Description("Facebook")]
            Facebook = 2,
            [Description("Line")]
            Line = 4,
        }

    }
}
