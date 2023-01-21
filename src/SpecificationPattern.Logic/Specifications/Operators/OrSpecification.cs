using System.Linq.Expressions;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications.Operators;

public sealed class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }
    
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        ParameterExpression paramExpr = Expression.Parameter(typeof(T));
        BinaryExpression exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return finalExpr;
    }
}