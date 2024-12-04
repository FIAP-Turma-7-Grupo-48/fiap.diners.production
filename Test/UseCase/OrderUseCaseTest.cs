using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Moq;
using UseCase.Dtos.OrderRequest;
using UseCase.Services;
using Xunit;

namespace UseCase.OrderTest
{
    public class OrderUseCaseTest
    {
        OrderUseCase _orderUseCase;

        Mock<IOrderRepository> _orderRepository;
        Mock<NotificationContext> _notificationContext;

        Order orderResponseMock;

        public OrderUseCaseTest()
        {

            _orderRepository = new Mock<IOrderRepository>();
            _notificationContext = new Mock<NotificationContext>();

            _orderUseCase = new OrderUseCase(_orderRepository.Object,
                                            _notificationContext.Object
                                            );
            orderResponseMock = new()
            {
                ExternalOrderId = 2,
                OrderProducts = new List<OrderProduct> { new() { Name = "Novo hamburguer", ProductType = ProductType.SideDish, Quantity = 1 } }
            };
        }

        [Fact]
        public async void DevePermitirCriarPedido()
        {
            CreateOrderRequest createOrderRequest = new CreateOrderRequest()
            {
                Id = 1,
                OrderProducts = new List<CreateOrderProductRequest>
                {
                    new CreateOrderProductRequest
                    {
                        Name = "Novo hamburguer",
                        OrderId = 2,
                        productType = ProductType.SideDish,
                        Quantity = 1
                    }
                }
            };
           
            _orderRepository.Setup(x => x.CreateAsync(It.IsAny<Order>(), default)).ReturnsAsync(orderResponseMock);

            var result = await _orderUseCase.CreateAsync(createOrderRequest, default);

            Assert.NotNull(result);
            Assert.NotEqual("0", result.Id);
            Assert.True(result.OrderProducts.Any());
        }


        [Fact]
        public async void DevePermitirAtualizarStatusPedidoPreparando()
        {
            _orderRepository.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync(orderResponseMock);

            _orderRepository.Setup(x => x.UpdateAsync(It.IsAny<Order>(), default));

            await _orderUseCase.UpdateStatusToPreparing(2, default);
        }

        [Fact]
        public async void DevePermitirAtualizarStatusPedidoPronto()
        {
            _orderRepository.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync(orderResponseMock);

            _orderRepository.Setup(x => x.UpdateAsync(It.IsAny<Order>(), default));

            await _orderUseCase.UpdateStatusToDone(2, default);
        }

        [Fact]
        public async void DevePermitirAtualizarStatusPedidoFinalizado()
        {
            _orderRepository.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync(orderResponseMock);

            _orderRepository.Setup(x => x.UpdateAsync(It.IsAny<Order>(), default));

            await _orderUseCase.UpdateStatusToFinished(2, default);
        }

        [Fact]
        public async void DevePermitirListarPedido()
        {
            List<Order> lstOrder = new List<Order>();
            lstOrder.Add(orderResponseMock);

            _orderRepository.Setup(x => x.ListAsync(It.IsAny<OrderStatus>(),
                                                    It.IsAny<int>(),
                                                    It.IsAny<int>(),
                                                    default)).ReturnsAsync(lstOrder);

            var result = await _orderUseCase.ListAsync(OrderStatus.Finished, 1, 10, default);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async void DevePermitirListarPedidoAtivos()
        {
            List<Order> lstOrder = new List<Order>();
            lstOrder.Add(orderResponseMock);

            _orderRepository.Setup(x => x.ListAsync(It.IsAny<OrderStatus>(),
                                                    It.IsAny<int>(),
                                                    It.IsAny<int>(),
                                                    default)).ReturnsAsync(lstOrder);

            var result = await _orderUseCase.ListActiveAsync(1, 10, default);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}