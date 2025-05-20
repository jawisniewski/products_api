using Products.Domain.Common;
using Products.Domain.Extensions;

namespace Products.Domain.Entities
{
    public class Product
    {
        private Product() { }
        private Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public static Result<Product> Create(string name, string code, decimal price)
        {
            var product = new Product(code, name, price);

            var validateResult = product.Validate();

            if (validateResult.IsFailure)
            {
                return (Result<Product>) validateResult;
            }

            return Result<Product>.Success(product);
        }

        private Result Validate()
        {
            var validateNameResult = ValidateName();

            if (validateNameResult.IsFailure)
                return validateNameResult;

            var validateCodeResult = ValidateCode();

            if (validateCodeResult.IsFailure)
                return validateCodeResult;

            var validatePriceResult = ValidatePrice();

            if(validatePriceResult.IsFailure)
                return validatePriceResult;

            return Result.Success();
        }

        private Result ValidatePrice()
        {
            if (Price < 0)
            {
                return Result.Failure("Price cannot be negative");
            }

            if (!Price.HasMaximumTwoDecimalPlaces())
            {
                return Result.Failure("Price cannot have more then 2 places");
            }

            return Result.Success();
        }

        private Result ValidateCode()
        {
            if (string.IsNullOrEmpty(Code))
            {
                return Result.Failure("Code cannot be empty");
            }

            if (Code.Length < 3)
            {
                return Result.Failure("Code must be at least 3 characters long");
            }

            if (Code.Length > 200)
            {
                return Result.Failure("Code must be at most 200 characters long");
            }
            return Result.Success();
        }

        private Result ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Result.Failure("Name cannot be empty");
            }

            if (Name.Length < 3)
            {
                return Result.Failure("Name must be at least 3 characters long");
            }

            if (Name.Length > 200)
            {
                return Result.Failure("Name must be at most 200 characters long");
            }

            return Result.Success();
        }
    }
}
