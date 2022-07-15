using System;
using System.ComponentModel;
using System.Reflection;
using Today.Web.Services.CommonEnum;

namespace Today.Web.Helper
{
    public class EnumHelper
    {
        public static string GetTypeDescription(int type)
        {
            var enumType = Enum.Parse(typeof(CityEnum.PartnerType), type.ToString());
            var result = GetDescription((Enum)enumType);
            return result;
        }
        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();

            //// 利用反射找出相對應的欄位
            var field = type.GetField(value.ToString());
            //// 取得欄位設定DescriptionAttribute的值
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            //// 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return value.ToString();
            }

            //// 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }
    }
}
