using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Common.Filters;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleFilterSpecification : ISpecification<Event>
    {
        private readonly IFilter<Event> _filter;
        private readonly IPagingParameters _paging;

        public ScheduleFilterSpecification(IFilter<Event> filter, IPagingParameters paging)
        {
            _filter = filter ?? throw new ArgumentNullException("filter");
            _paging = paging ?? PagingParameters.Empty;
        }

        #region ISpecification Members

        public Expression<Func<Event, bool>> Criteria => _filter.GetExpression();

        public List<Expression<Func<Event, object>>> Includes => null;

        public List<string> IncludeStrings => null;

        public Expression<Func<Event, object>> OrderBy => null;

        public Expression<Func<Event, object>> OrderByDescending => o => o.StartDate;

        public int? Take => _paging.GetTakeValue();

        public int? Skip => _paging.GetSkipValue();

        #endregion
    }
}
