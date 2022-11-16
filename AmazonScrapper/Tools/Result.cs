namespace AmazonScrapper.Tools
{
    public class Result<T>
    {
        private T _value;
        public T Value
        {
            get
            {
                // insert desired logic here
                return _value;
            }
            set
            {
                // insert desired logic here
                _value = value;
            }
        }

        public static implicit operator T(Result<T> value)
        {
            return value.Value;
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T> { Value = value };
        }
    }
}