using System;

namespace Scryptdnx.Utils
{
    public class Enums
    {
        public enum ParamType
        {
            None,
            Command,
            Trigger,
            Crypto
        }

        public static T GetEnumValue<T>(object value) => 
			Enum.IsDefined(typeof(T), value) ? (T)Enum.Parse(typeof(T), value.ToString()) : default(T);
    }
}