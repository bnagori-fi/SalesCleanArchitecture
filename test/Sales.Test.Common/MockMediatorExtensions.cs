using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Test.Common
{
    public static class MockMediatorExtensions
    {
        public static void MockMediator<TRequest, TResult>(this Mock<IMediator> @this, TResult result) where TRequest : IRequest<TResult>
        {
            @this.Setup(x => x.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()))
                .Returns((TRequest message, CancellationToken token) => Task.FromResult(result));
        }

        public static void MockMediator<TRequest, TResult>(this Mock<IMediator> @this, Func<TRequest, TResult> handleRequest) where TRequest : IRequest<TResult>
        {
            @this.Setup(x => x.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()))
                .Returns((TRequest message, CancellationToken token) => Task.FromResult(handleRequest(message)));
        }

        public static void MockMediator<TRequest>(this Mock<IMediator> @this, Func<TRequest, Unit> handleRequest) where TRequest : IRequest
        {
            @this.Setup(x => x.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()))
                .Returns((TRequest message, CancellationToken token) => Task.FromResult(handleRequest(message)));
        }

        public static void VerifyMediator<TRequest>(this Mock<IMediator> @this, Times times, Func<TRequest, bool> where) where TRequest : IRequest
        {
            @this.Verify(x => x.Send(It.Is<TRequest>(r => where(r)), It.IsAny<CancellationToken>()), times);
        }

        public static void VerifyMediator<TRequest, TResult>(this Mock<IMediator> @this, Times times, Func<TRequest, bool> where) where TRequest : IRequest<TResult>
        {
            @this.Verify(x => x.Send(It.Is<TRequest>(r => where(r)), It.IsAny<CancellationToken>()), times);
        }
    }
}
