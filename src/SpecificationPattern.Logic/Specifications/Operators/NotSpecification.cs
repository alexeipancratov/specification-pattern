using System.Linq.Expressions;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications.Operators;

public sealed class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;

    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }
    
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = _specification.ToExpression();

        UnaryExpression notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}