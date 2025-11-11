using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record OrderSummary(
        int Id,
        int ProductId,  
        string ProductName,
        decimal ProductPrice,
        int Quantity,
        decimal TotalAmount,
        DateTime Date
        );

}
