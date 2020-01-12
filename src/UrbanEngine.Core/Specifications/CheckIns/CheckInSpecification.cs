using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.CheckIns
{
    public class CheckInSpecification : BaseSpecification<CheckInEntity>
    {
        public CheckInSpecification( ICheckInFilter filter ) : base( filter )
        {
            ApplyCriteria( GetExpression( filter ) );
        }

        private Expression<Func<CheckInEntity, bool>> GetExpression( ICheckInFilter filter )
        {
            var predicate = PredicateBuilder.New<CheckInEntity>();

            if ( filter.UserId.HasValue ) predicate = predicate.And( o => o.UserId == filter.UserId );
            if ( filter.EventId.HasValue ) predicate = predicate.And( o => o.EventId == filter.EventId );
            if ( filter.CheckedInAt.HasValue ) predicate = predicate.And( o => o.CheckedInAt == filter.CheckedInAt );

            return predicate;
        }
    }
}
