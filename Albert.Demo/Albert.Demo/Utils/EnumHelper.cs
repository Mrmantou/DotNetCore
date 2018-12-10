using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Albert.Demo.Utils
{
    /// <summary>
    /// Enum帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举类型名称及值的SelectListItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectList<T>()
        {
            var enumType = typeof(T);
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var value in Enum.GetValues(enumType))
            {
                selectList.Add(new SelectListItem
                {
                    Text = Enum.GetName(enumType, value),
                    Value = value.ToString()
                });
            }

            return selectList;
        }

        /// <summary>
        /// 获取枚举类型描述及值的SelectListItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectListDesc<T>()
        {
            var list = new List<SelectListItem>();

            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType);
            foreach (var value in enumValues)
            {
                MemberInfo memberInfo = enumType.GetMember(value.ToString()).First();

                var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                list.Add(new SelectListItem()
                {
                    Text = descriptionAttribute.Description,
                    Value = value.ToString()
                });
            }

            return list;
        }
    }
}
