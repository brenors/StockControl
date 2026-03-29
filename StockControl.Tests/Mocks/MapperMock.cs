using AutoMapper;
using Moq;

namespace StockControl.Tests.Mocks
{

    public static class MapperMock
    {
        public static IMapper Create<TSource, TDestination>(TDestination destination)
        {
            var mock = new Mock<IMapper>();

            mock.Setup(m => m.Map<TDestination>(It.IsAny<TSource>()))
                .Returns(destination);

            mock.Setup(m => m.Map<IEnumerable<TDestination>>(It.IsAny<IEnumerable<TSource>>()))
                .Returns(new List<TDestination> { destination });

            return mock.Object;
        }
    }
}
