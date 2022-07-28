using System;
using System.ComponentModel;
using System.Reflection;
using Today.Web.CommonEnum;

namespace Today.Web.Helper
{
    public static class EnumHelper
    {
        public static string ToDescription(this Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
        
        
        public static string ToDescription<T>(this int enumValue) where T : Enum
        {
            var enumType = Enum.Parse(typeof(T), enumValue.ToString());
            string str = enumType.ToString();
            System.Reflection.FieldInfo field = enumType.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

       
    }
}
