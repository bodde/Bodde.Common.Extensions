public static class ArrayExtensions
{
    extension<T>(T[] me)
    {
        /// <summary>
        /// Determines whether the array contains no elements.
        /// </summary>
        /// <returns><see langword="true"/> if the array is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsEmpty() => me.Length == 0;
    }
    
    extension<T>(T[]? me)
    {
        /// <summary>
        /// Returns the array, or an empty array when the value is <see langword="null"/>.
        /// </summary>
        /// <returns>The original array or an empty array.</returns>
        public T[] OrEmpty() => me ?? [];
    }
}