using System.Linq.Expressions;
using SpecificationPattern.Logic.Specifications.Operators;

namespace SpecificationPattern.Logic.Specifications.Abstract;

// Strongly typed version of Specification.
public abstract class Specification<T>
{
    public static readonly Specification<T> All = new IdentitySpecification<T>();

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> specification)
    {
        if (this == All)
        {
            return specification;
        }
        if (specification == All)
        {
            return this;
        }
        
        return new AndSpecification<T>(this, specification);
    }
    
    public Specification<T> Or(Specification<T> specification)
    {
        if (this == All || specification == All)
        {
            return All;
        }
        
        return new OrSpecification<T>(this, specification);
    }
    
    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}