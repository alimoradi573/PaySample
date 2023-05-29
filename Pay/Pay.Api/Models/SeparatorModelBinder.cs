using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pay.OvetimePolicies.Application.DTOs;

namespace Pay.OvetimePolicies.Api.Models
{
    public class SeparatorModelBinder : IModelBinder
    {
        private readonly char separator;

        public SeparatorModelBinder(char separator)
        {
            this.separator = separator;
        }
        /*
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var rawValue = valueProviderResult.FirstValue;
            var values = rawValue?.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            if (values != null && values.Length > 0)
            {
                bindingContext.Result = ModelBindingResult.Success(values);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
        */
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var values = bindingContext.ValueProvider.GetValue("data");
            if (values.Length == 0)
            {
                return Task.CompletedTask;
            }

            var splitData = values.FirstValue.Split(new char[] { separator});
            if (splitData.Length >= 3)
            {
                PayDTO dto = new PayDTO()
                {
                    Id = int.Parse(splitData[0]),
                    FirstName = splitData[1],
                    LastName = splitData[2],
                    BasicSalary = long.Parse(splitData[3]),
                    Allowance = long.Parse(splitData[4]),
                    Transportation = long.Parse(splitData[5]),
                    Date = DateTime.Parse(splitData[6]),
                };
                bindingContext.Result = ModelBindingResult.Success(dto);
            }

            return Task.CompletedTask;
        }
    }

    public class SeparatorModelBinderProvider : IModelBinderProvider
    {
        private readonly char separator;

        public SeparatorModelBinderProvider(char separator)
        {
            this.separator = separator;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(PayDTO))
            {
                return new SeparatorModelBinder(separator);
            }

            return null;
        }
    }
}