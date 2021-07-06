using App.Core.DTOs;
using App.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {

        Task<(IQueryable<T> Result, int TotalItems)> GetAll<ReturnType>(
            string sort,
            Expression<Func<T, bool>> predicate,
            int? page = null, int? limit = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<(IQueryable<T> Result, int TotalItems)> GetAllIncludeString<ReturnType>(
            string sort,
            Expression<Func<T, bool>> predicate,
            int? page = null, int? limit = null,
            params string[] includeProperties);
        Task<T> GetById<ReturnType>(long id, params Expression<Func<T, object>>[] includeProperties) where ReturnType : new();
        Task<T> GetByIdIncludeString<ReturnType>(long id, params string[] includeProperties) where ReturnType : new();
        Task<T> Add(T entity);
        Task<List<ReturnType>> AddMultipleEntities<ReturnType, InputType>(List<InputType> entity) where ReturnType : new();
        Task<T> Update(long id, T entity);
        Task<ReturnType> Delete<ReturnType>(long id) where ReturnType : new();

        Task<int> ChangeStatus(long id, bool active);
    }
}
