using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using Shared.DTOs;
using Shared.Models;
using System.Text;

namespace OrderApi.Repository
{
    public class OrderRepo(OrderDbContext context, IPublishEndpoint publishEndpoint) : IOrder
    {
      public async  Task<ServiceResponse> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var orderSummary = await GetOrderSummaryAsync();
            string content = BuilderOrderEmailBody(orderSummary.Id, orderSummary.ProductName, orderSummary.ProductPrice,
               orderSummary.Quantity, orderSummary.TotalAmount, orderSummary.Date);

            await publishEndpoint.Publish(new EmailDTO("Order Information ", content));
            await ClearOrderTable();

            return new ServiceResponse(true, "Order added successfully");
        }

       public async Task<List<Order>> GetAllOrdersAsync()
        {
          var orders = await context.Orders.ToListAsync();
            return orders;  
        }

       public async Task<OrderSummary> GetOrderSummaryAsync()
        {
          var order = await context.Orders.FirstOrDefaultAsync();
          var produts = await context.Products.ToListAsync();
          var productInfo = produts.Find(x => x.Id == order!.ProductId);
           return new OrderSummary(
                order!.Id,
                order.ProductId,
                productInfo!.Name,
                productInfo.Price,
                order.Quantity,
                productInfo.Price * order.Quantity,
                order.Date
                );
        }

        private string BuilderOrderEmailBody(int orderId, string productName,decimal price, 
            int orderQuantity, decimal totalAmout, DateTime date)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1><strong>Order Information </strong></h1>");
            sb.AppendLine($"<p><strong>Order ID: </strong>{orderId}</h1>");
            sb.AppendLine("<h2>Order Item:  </h2>");
            sb.AppendLine("<ul>");
            sb.AppendLine($"<li>Name: {productName}<li>");
            sb.AppendLine($"<li>Price: {price}<li>");
            sb.AppendLine($"<li>Quantity: {orderQuantity}<li>");
            sb.AppendLine($"<li>Amount: {totalAmout}<li>");
            sb.AppendLine($"<li>Date Ordered: {date}<li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("<p> Thank you for order </p>");

            return sb.ToString();   
        }

        private async Task ClearOrderTable()
        {
            context.Orders.RemoveRange(await context.Orders.FirstOrDefaultAsync());
            await context.SaveChangesAsync();
        }
    }
}
