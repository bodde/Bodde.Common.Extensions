using System.Diagnostics.CodeAnalysis;

public static class ArrayExtensions
{
    extension<T>([NotNullWhen(false)]T[]? me)
    {
        /// <summary>
        /// Determines whether the array is <see langword="null"/> or contains no elements.
        /// </summary>
        /// <returns><see langword="true"/> if the array is <see langword="null"/> or empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNullOrEmpty() => me == null || me.IsEmpty();
    }

    extension<T>([NotNullWhen(true)]T[]? me)
    {
        /// <summary>
        /// Determines whether the array is not <see langword="null"/> and contains at least one element.
        /// </summary>
        /// <returns><see langword="true"/> if the array is not <see langword="null"/> and is not empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNotNullOrEmpty() => me.IsNullOrEmpty() == false;
    }

    extension<T>(T[]? me)
    {
        /// <summary>
        /// Returns the array, or an empty array when the value is <see langword="null"/>.
        /// </summary>
        /// <returns>The original array or an empty array.</returns>
        public T[] OrEmpty() => me ?? [];
    }
        
    extension<T>(T[] me)
    {
        /// <summary>
        /// Determines whether the array contains no elements.
        /// </summary>
        /// <returns><see langword="true"/> if the array is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsEmpty() => me.Length == 0;
    }

}