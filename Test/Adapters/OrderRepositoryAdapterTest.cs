using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Infrastructure.Adapters;
using Infrastructure.MongoModels;
using Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Adapters.OrderRepositoryTest
{
    public class OrderRepositoryAdapterTest
    {
        OrderRepositoryAdapter _orderRepositoryAdapter;
        Mock<IOrderMongoDbRepository> _orderMongoDbRepository;
        public OrderRepositoryAdapterTest()
        {
            _orderMongoDbRepository = new Mock<IOrderMongoDbRepository>();
            _orderRepositoryAdapter = new OrderRepositoryAdapter(_orderMongoDbRepository.Object);
        }

        [Fact]
        public async void DevePermitirCriarPedido()
        {
            OrderMongoModel orderMongoModel = new OrderMongoModel
            {
                Id = "1",
                ExternalOrderId = 1,
                Status = Domain.Entities.Enums.OrderStatus.Received,
                OrderProducts = new List<OrderProductMongoModel>
                {
                    new OrderProductMongoModel
                    {
                        Name = "X-Bacon",
                        ProductType = Domain.Entities.Enums.ProductType.Snack,
                        Quantity = 1
                    }
                },
                Created = DateTime.Now,
            };

            Order order = new()
            {
                ExternalOrderId = 1,
                OrderProducts = new List<OrderProduct> { new() { Name = "X-Bacon", ProductType = ProductType.Snack, Quantity = 1 } }
            };

            _orderMongoDbRepository.Setup(x => x.CreateAsync(It.IsAny<OrderMongoModel>(), default)).ReturnsAsync(orderMongoModel);

            var result = await _orderRepositoryAdapter.CreateAsync(order, default);

            Assert.NotNull(result);
            Assert.True(result.OrderProducts.Any());
            Assert.True(result.ExternalOrderId == order.ExternalOrderId);
        }

        [Fact]
        public async void DevePermitirObterPedido()
        {
            OrderMongoModel orderMongoModel = new OrderMongoModel
            {
                Id = "1",
                ExternalOrderId = 1,
                Status = Domain.Entities.Enums.OrderStatus.Received,
                OrderProducts = new List<OrderProductMongoModel>
                {
                    new OrderProductMongoModel
                    {
                        Name = "X-Bacon",
                        ProductType = Domain.Entities.Enums.ProductType.Snack,
                        Quantity = 1
                    }
                },
                Created = DateTime.Now,
            };

            _orderMongoDbRepository.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync(orderMongoModel);

            var result = await _orderRepositoryAdapter.GetAsync("1", default);

            Assert.NotNull(result);
            Assert.Contains(result.Id, result.Id);
            Assert.True(result.OrderProducts.Any());
        }

        [Fact]
        public async void DevePermitirListarPedidoPorStatus()
        {
            OrderMongoModel orderMongoModel = new OrderMongoModel
            {
                Id = "1",
                ExternalOrderId = 1,
                Status = Domain.Entities.Enums.OrderStatus.Received,
                OrderProducts = new List<OrderProductMongoModel>
                {
                    new OrderProductMongoModel
                    {
                        Name = "X-Bacon",
                        ProductType = Domain.Entities.Enums.ProductType.Snack,
                        Quantity = 1
                    }
                },
                Created = DateTime.Now,
            };
            List<OrderMongoModel> lstOrderMongoModel = new List<OrderMongoModel>();
            lstOrderMongoModel.Add(orderMongoModel);

            _orderMongoDbRepository.Setup(x => x.ListAsync(It.IsAny<IEnumerable<OrderStatus>>(),
                                                           It.IsAny<int>(),
                                                           It.IsAny<int>(),
                                                           default)).ReturnsAsync(lstOrderMongoModel.ToList());

            var result = await _orderRepositoryAdapter.ListAsync(OrderStatus.Received, 1, 10, default);

            Assert.NotNull(result);
            Assert.True(result.Any());

        }

        [Fact]
        public async void DevePermitirListarPedidoPorListaStatus()
        {
            OrderMongoModel orderMongoModel = new OrderMongoModel
            {
                Id = "1",
                ExternalOrderId = 1,
                Status = Domain.Entities.Enums.OrderStatus.Received,
                OrderProducts = new List<OrderProductMongoModel>
                {
                    new OrderProductMongoModel
                    {
                        Name = "X-Bacon",
                        ProductType = Domain.Entities.Enums.ProductType.Snack,
                        Quantity = 1
                    }
                },
                Created = DateTime.Now,
            };
            List<OrderMongoModel> lstOrderMongoModel = new List<OrderMongoModel>();
            lstOrderMongoModel.Add(orderMongoModel);

            _orderMongoDbRepository.Setup(x => x.ListAsync(It.IsAny<IEnumerable<OrderStatus>>(), 
                                                           It.IsAny<int>(),
                                                           It.IsAny<int>(),
                                                           default)).ReturnsAsync(lstOrderMongoModel.ToList());

            List<OrderStatus> lstOrderStatus = new List<OrderStatus> { OrderStatus.Received, OrderStatus.Done };

            var result = await _orderRepositoryAdapter.ListAsync(lstOrderStatus, 1, 10, default);

            Assert.NotNull(result);
            Assert.True(result.Any());

        }

        [Fact]
        public void DevePermitirAtualizarPedido()
        {
            OrderMongoModel orderMongoModel = new OrderMongoModel
            {
                Id = "1",
                ExternalOrderId = 1,
                Status = Domain.Entities.Enums.OrderStatus.Finished,
                OrderProducts = new List<OrderProductMongoModel>
                {
                    new OrderProductMongoModel
                    {
                        Name = "X-Bacon",
                        ProductType = Domain.Entities.Enums.ProductType.Snack,
                        Quantity = 1
                    }
                },
                Created = DateTime.Now,
            };

            Order order = new()
            {
                ExternalOrderId = 1,
                OrderProducts = new List<OrderProduct> { new() { Name = "X-Bacon", ProductType = ProductType.Snack, Quantity = 1 } }
            };

            _orderMongoDbRepository.Setup(x => x.ReplaceOneAsync(It.IsAny<OrderMongoModel>(), default)).ReturnsAsync(orderMongoModel);

            _orderRepositoryAdapter.UpdateAsync(order, default);
        }
    }
}