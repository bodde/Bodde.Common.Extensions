public static class ArrayExtensions
{
    extension<T>(T[] me)
    {
        public bool IsEmpty() => me.Length == 0;
    }
    
    extension<T>(T[]? me)
    {
        public T[] OrEmpty() => me ?? [];
    }
}