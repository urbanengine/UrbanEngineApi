using System;
using System.Linq.Expressions;
using LinqKit;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.Users
{
	public sealed class UserSpecification : BaseSpecification<UserEntity>
	{
		public UserSpecification(IUserFilter filter) : base(filter)
		{
			ApplyCriteria(GetExpression(filter));
		}

		private Expression<Func<UserEntity, bool>> GetExpression(IUserFilter filter)
		{
			var predicate = PredicateBuilder.New<UserEntity>();

			predicate = filter.IsDeleted.HasValue ?
				predicate.And(p => p.IsDeleted == filter.IsDeleted.Value) :
				predicate.And(p => p.IsDeleted != true);

			if(filter.CompanyId.HasValue)
				predicate = predicate.And(p => p.CompanyId == filter.CompanyId);

			if(!string.IsNullOrEmpty(filter.CountryOrRegion))
				predicate = predicate.And(p => p.CountryOrRegion == filter.CountryOrRegion);

			if(!string.IsNullOrEmpty(filter.PostalCode))
				predicate = predicate.And(p => p.PostalCode == filter.PostalCode);

			return predicate;
		}
	}
}
