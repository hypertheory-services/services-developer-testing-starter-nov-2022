
using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using MiscApi.Controllers;

namespace MiscApi.UnitTests;

public class PizzaOrderValidations
{
    [Fact]
    public void DataAttributeChecks()
    {
        

        typeof(OrderRequest)
            .GetProperty(nameof(OrderRequest.SpecialInstructions))
            .Should()
            .NotBeDecoratedWith<RequiredAttribute>();

    }
}
