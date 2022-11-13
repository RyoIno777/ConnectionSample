using System;
using System.ComponentModel;
using System.Linq;

namespace ConnectionSample.Core.Enums
{
    /// <summary>
    /// Enumの拡張機能です。
    /// </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// 指定されたEnum値のDescription属性の文字列を取得します。
        /// </summary>
        /// <param name="value">Enum値</param>
        /// <returns>Description属性の文字列</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return
                Attribute.GetCustomAttribute(
                    field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute ?
                attribute.Description : value.ToString();
        }

        /// <summary>
        /// Description属性からEnum値に変換します。
        /// </summary>
        /// <typeparam name="T">対象のEnum</typeparam>
        /// <param name="description">Description属性名</param>
        /// <returns>Enum値</returns>
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException();
            }
            var field =
                type.GetFields()
                .SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false), (f, a) => new { Field = f, Att = a })
                .Where(a => ((DescriptionAttribute)a.Att).Description == description).SingleOrDefault();

            return field == null ? default : (T)field.Field.GetRawConstantValue();
        }

    }
}