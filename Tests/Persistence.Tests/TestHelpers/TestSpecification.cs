﻿using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers
{
    internal class TestSpecification : BaseSpecification<FakeEntity>
    {
        public TestSpecification()
        {
        }

        public TestSpecification(Expression<Func<FakeEntity, bool>> criteria, bool orderById = true)
            : base(criteria)
        { 
            if(orderById)
            {
                ApplyOrderBy(o => o.Id);
            }
        }

        public static TestSpecification AllItems => new TestSpecification(p => true); 
        public static TestSpecification NoItems => new TestSpecification(p => false);
    }
}