using System.Linq.Expressions;

namespace TaskManagement.Application.Common.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    int? Skip { get; }
    int? Take { get; }
}
