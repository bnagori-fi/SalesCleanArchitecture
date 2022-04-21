using AutoMapper;

namespace Sales.Application.Mappings
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreBaseEntityProperties<TSource, TDestination, TKey>(this IMappingExpression<TSource, TDestination> expression) 
                where TDestination : Domain.Common.EntityBase<TKey>
        {
            expression.ForMember(o => o.Id, s => s.Ignore());
            expression.ForMember(o => o.CreatedBy, s => s.Ignore());
            expression.ForMember(o => o.CreatedDate, s => s.Ignore());
            expression.ForMember(o => o.LastModifiedBy, s => s.Ignore());
            expression.ForMember(o => o.LastModifiedDate, s => s.Ignore());
            
            return expression;
        }
    }
}
