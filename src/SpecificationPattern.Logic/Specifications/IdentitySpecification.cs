using System.Linq.Expressions;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications;

internal sealed class IdentitySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}