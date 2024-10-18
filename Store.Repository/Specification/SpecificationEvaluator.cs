using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spcs)
        {
            var query = inputQuery;

            if(spcs.Criteria is not null)
                query = query.Where(spcs.Criteria);

            if(spcs.OrderBy is not null)
                query = query.OrderBy(spcs.OrderBy);

            if (spcs.OrderByDesc is not null)
                query = query.OrderByDescending(spcs.OrderByDesc);

            if(spcs.IsPaginated)
                query = query.Skip(spcs.Skip).Take(spcs.Take);

            query = spcs.Includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query;
        }
    }
}
