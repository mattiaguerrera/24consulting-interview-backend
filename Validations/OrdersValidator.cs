using FluentValidation;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using System.Net;

namespace Validations
{
    public class OrdersValidator : AbstractValidator<Order>
    {
        private readonly IOrderStepRepository _orderStepRepository;
        public OrdersValidator(IOrderStepRepository orderStepRepository)
        {
            this._orderStepRepository = orderStepRepository;

            RuleFor(order => order.CustomerId).NotNull();
            RuleFor(order => order.PaymentMethodId).NotNull();
            RuleFor(order => order.OrderStepId).NotNull();


            this.RuleFor(t => t.OrderStepId)
                .Must((t, p, c) =>
                {
                    bool isValid = this._orderStepRepository.CheckOrderStep(t.OrderStepId, t, out IEnumerable<string> failures);                    
                    if (isValid) return true;

                    foreach (var errorMessage in failures)
                    {
                        var formattedErrorMessage = errorMessage.Replace("{PropertyName}", c.DisplayName);
                        c.AddFailure(c.DisplayName, formattedErrorMessage);
                    }
                    return false;
                });
        }
    }

}