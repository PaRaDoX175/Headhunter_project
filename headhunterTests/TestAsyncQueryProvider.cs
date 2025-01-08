//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Query;
//using System.Linq.Expressions;

//namespace headhunterTests
//{
//    internal class TestAsyncQueryProvider<T> : IAsyncQueryProvider
//    {
//        private readonly IQueryProvider _inner;

//        internal TestAsyncQueryProvider(IQueryProvider inner)
//        {
//            _inner = inner;
//        }

//        public IQueryable CreateQuery(Expression expression)
//        {
//            return new TestAsyncEnumerable<T>(expression);
//        }

//        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
//        {
//            return new TestAsyncEnumerable<TElement>(expression);
//        }

//        public object Execute(Expression expression)
//        {
//            return _inner.Execute(expression);
//        }

//        public TResult Execute<TResult>(Expression expression)
//        {
//            return _inner.Execute<TResult>(expression);
//        }

//        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
//        {
//            return _inner.Execute<TResult>(expression);
//        }
//    }

//    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
//    {
//        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
//        {
//        }

//        public TestAsyncEnumerable(Expression expression) : base(expression)
//        {
//        }

//        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
//        {
//            return new TestAsyncEnumerator<T>(this.AsAsyncEnumerable().GetAsyncEnumerator());
//        }

//        IQueryProvider IQueryable.Provider
//        {
//            get { return new TestAsyncQueryProvider<T>(this); }
//        }
//    }

//    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
//    {
//        private readonly IAsyncEnumerator<T> _inner;

//        public TestAsyncEnumerator(IAsyncEnumerator<T> inner)
//        {
//            _inner = inner;
//        }

//        public T Current { get { return _inner.Current; } }

//        public ValueTask DisposeAsync()
//        {
//            return _inner.DisposeAsync();
//        }

//        public ValueTask<bool> MoveNextAsync()
//        {
//            return _inner.MoveNextAsync();
//        }
//    }

//}
