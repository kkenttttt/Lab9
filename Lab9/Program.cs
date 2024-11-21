using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class Order
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string Language { get; set; }
        public int Volume { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
    }

    public interface IOrderProcessor
    {
        void ProcessOrder(Order order);
    }

    public class TextOrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"Processing text order for {order.ClientName}");
        }
    }

    public class AudioOrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"Processing audio order for {order.ClientName}");
        }
    }

    public class OrderService
    {
        private readonly IOrderProcessor _orderProcessor;

        public OrderService(IOrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        public void ProcessOrder(Order order)
        {
            _orderProcessor.ProcessOrder(order);
        }
    }

    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        Order GetOrder(int orderId);
    }

    public interface IOrderNotification
    {
        void SendNotification(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            Console.WriteLine($"Order {order.OrderId} saved for {order.ClientName}");
        }

        public Order GetOrder(int orderId)
        {
            return new Order { OrderId = orderId, ClientName = "John Doe" };
        }
    }

    public class OrderNotification : IOrderNotification
    {
        public void SendNotification(Order order)
        {
            Console.WriteLine($"Notification sent for order {order.OrderId}");
        }
    }

    public class OrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderNotification _orderNotification;
        private readonly IOrderProcessor _orderProcessor;

        public OrderManager(IOrderRepository orderRepository, IOrderNotification orderNotification, IOrderProcessor orderProcessor)
        {
            _orderRepository = orderRepository;
            _orderNotification = orderNotification;
            _orderProcessor = orderProcessor;
        }

        public void CreateOrder(Order order)
        {
            _orderProcessor.ProcessOrder(order);
            _orderRepository.SaveOrder(order);
            _orderNotification.SendNotification(order);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository orderRepository = new OrderRepository();
            IOrderNotification orderNotification = new OrderNotification();
            IOrderProcessor orderProcessor = new TextOrderProcessor();

            OrderManager orderManager = new OrderManager(orderRepository, orderNotification, orderProcessor);

            Order order = new Order
            {
                OrderId = 1,
                ClientName = "John Doe",
                Language = "English",
                Volume = 100,
                OrderType = "Text",
                Status = "New"
            };

            orderManager.CreateOrder(order);
        }
        
    }
}
