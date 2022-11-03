namespace Source.Common.Utils
{
    public class Singleton<T> where T : new()
    {
        private static T instance;
        private static volatile object lockObject = new();

        public static T Instance
        {
            get
            {
                lock (lockObject)
                {
                    var result = instance ??= new T();
                    return result;
                }
            }
        }

        protected Singleton()
        {
        }
    }
}