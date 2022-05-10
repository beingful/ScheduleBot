using System;

namespace PrimatScheduleBot
{
    public class TypeDetermination
    {
        private readonly Type _type;

        public TypeDetermination(Type type) => _type = type;

        public Type GetExactType()
        {
            Type underlyingType = Nullable.GetUnderlyingType(_type);

            return underlyingType ?? _type;
        }

        public object GetTypeDefaultValue()
        {
            Type type = GetExactType();

            return type.GetDefaultValue();
        }
    }
}
