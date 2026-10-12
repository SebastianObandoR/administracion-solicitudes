using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdministracionSolicitudes.Web.Models
{
    public sealed class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);
            var value = valueResult.FirstValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "El valor es requerido.");
                return Task.CompletedTask;
            }

            var culture = value.Contains(',') && !value.Contains('.')
                ? CultureInfo.GetCultureInfo("es-CR")
                : CultureInfo.InvariantCulture;

            if (decimal.TryParse(value, NumberStyles.Number, culture, out var parsedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "El valor ingresado no es valido.");
            return Task.CompletedTask;
        }
    }
}
