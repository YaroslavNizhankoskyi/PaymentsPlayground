using System.ComponentModel.DataAnnotations;

namespace PaymentsPlayground.Helpers
{
    public static class ModelValidator<T>
    {
        public static List<ValidationResult> Validate(T model)
        {
            var ctx = new ValidationContext(model);

            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            return results;
        }
    }
}
