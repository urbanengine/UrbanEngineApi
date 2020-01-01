namespace UrbanEngine.SharedKernel.Validation
{
    public interface IValidate
    {
        /// <summary>
        /// indicates if validation passes with no errors indicated
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// returns the validation error message
        /// </summary>
        /// <returns></returns>
        string GetErrorMessage();
    }
}
