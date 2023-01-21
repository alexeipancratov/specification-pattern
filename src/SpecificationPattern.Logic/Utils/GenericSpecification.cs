using System.Linq.Expressions;

namespace SpecificationPattern.Logic.Utils;

// A thin layer on top of Expressions which provokes duplicating logic.
public class GenericSpecification<T>
{
    public Expression<Func<T, bool>> Expression { get; }

    public GenericSpecification(Expression<Func<T, bool>> expression)
    {
        Expression = expression;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return Expression.Compile().Invoke(entity);
    }
}
