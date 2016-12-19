namespace AppStrap.Validation
{
    /// <summary>
    /// IHandleValidation supplies the contract for data validation services.
    /// </summary>
    public interface IHandleValidation
    {
        /// <summary>
        /// Validates an object.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns>ValidationResult</returns>
        ValidationResult Handle(object obj);
    }

    public interface IHandleValidation<in T> : IHandleValidation
    {
        /// <summary>
        /// Validates an object.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns></returns>
        ValidationResult Handle(T obj);
    }
}
