using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiscApi.Controllers;

[ApiController]
public class PizzaOrderController : ControllerBase
{

    [HttpPost("/my/pizza-order/cheese/large")]
 
    public async Task<ActionResult> AddAnOrder([FromBody] OrderRequest request)
    {
        var response = new OrderResponse
        {
            PizzaStyle = "cheese",
            Size = "large",
            Qty = request.Qty.Value,
            SpecialInstructions = request.SpecialInstructions,
        };
        return Ok(response);
    }

    [HttpPost("/my/pizza-order/veggie/medium")]
    public async Task<ActionResult> AddVeggiePizzaOrder([FromBody] OrderRequest request)
    {
        var response = new OrderResponse
        {
            PizzaStyle = "veggie",
            Size = "medium",
            Qty = request.Qty.Value,
            SpecialInstructions = request.SpecialInstructions,
        };
        return Ok(response);
    }

}

public record OrderRequest 
{
   
    [Required]
    public int? Qty { get; set; }

    
    public string? SpecialInstructions { get; set; }

    
}

public record OrderResponse
{
    [Required]
    public string PizzaStyle { get; init; } = string.Empty;
    [Required]
    public string Size { get; init; } = string.Empty;
    [Required]
    public int Qty { get; init; }
    public string? SpecialInstructions { get; init; }

}