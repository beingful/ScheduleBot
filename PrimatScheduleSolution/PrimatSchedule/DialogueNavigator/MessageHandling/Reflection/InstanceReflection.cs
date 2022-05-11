namespace PrimatScheduleBot
{
    public class InstanceReflection<T> where T : class, new()
    {
        private readonly T _instance;

        public InstanceReflection(T instance) => _instance = instance;
        
        private PropertyReflection<T> GetPropertyReflection(string propertyName) => new PropertyReflection<T>(_instance, propertyName);

        public void SetValue(string propertyName, object value)
        {
            PropertyReflection<T> propertyReflection = GetPropertyReflection(propertyName);

            propertyReflection.SetValue(value);
        }

        public void ConvertAndSetValue(string propertyName, string value)
        {
            PropertyReflection<T> propertyReflection = GetPropertyReflection(propertyName);

            propertyReflection.ConvertAndSetValue(value);
        }

        public object GetValue(string propertyName)
        {
            PropertyReflection<T> propertyReflection = GetPropertyReflection(propertyName);

            return propertyReflection.GetValue();
        } 
    }
}
