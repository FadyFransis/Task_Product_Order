using AutoMapper;
using App.Core.DTOs;
using App.Core.Entities.Base;
using App.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using App.Core.Interfaces.Repository;
using LinqKit;
using App.Common.Services.Logger;

namespace App.Core.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        //private const int MaxPageSize = 100;
        protected readonly Ilogger mLogger;
        protected readonly IMapper mapper;

        protected readonly IGenericRepository<T> mRepository;

        public GenericService(IGenericRepository<T> oRepository, Ilogger logger, IMapper _mapper)
        {
            mRepository = oRepository;
            mLogger = logger;
            mapper = _mapper;

        }
        protected Task<List<Error>> ValidateUniqueAttribute(T entity)
        {
            //TODO: get properties annotated by Unique custom attribute
            return Task.FromResult(new List<Error>());
        }
        public virtual Task<List<Error>> CustomValidate(T entity)
        {
            return Task.FromResult(new List<Error>());
        }

        public virtual Task<(IQueryable<T> Result, int TotalItems)> GetAll<ReturnType>(
            string sort = "id",
            Expression<Func<T, bool>> predicate = null,
            int? page = null, int? limit = null,
            params Expression<Func<T, object>>[] includeProperties
            )
        {
            var searchpredicate = PredicateBuilder.New<T>();
            searchpredicate.And(predicate);
            searchpredicate.And(x => x.RecordStatus == RecordStatus.Enabled);
            return mRepository.GetAll(searchpredicate, sort, page, limit, includeProperties);
        }


        public virtual Task<(IQueryable<T> Result, int TotalItems)> GetAllIncludeString<ReturnType>(
            string sort = "id",
            Expression<Func<T, bool>> predicate = null,
            int? page = null, int? limit = null,
            params string[] includeProperties
            )
        {
            var searchpredicate = PredicateBuilder.New<T>();
            searchpredicate.And(predicate);
            searchpredicate.And(x => x.RecordStatus == RecordStatus.Enabled);
            return mRepository.GetAllIncludeString(searchpredicate, sort, page, limit, includeProperties);
        }

        public virtual Task<T> GetById<ReturnType>(long id, params Expression<Func<T, object>>[] includeProperties) where ReturnType : new()
        {
            return mRepository.FindSingle(x => x.Id == id && x.RecordStatus == RecordStatus.Enabled, includeProperties);
        }
        public virtual Task<T> GetByIdIncludeString<ReturnType>(long id, params string[] includeProperties) where ReturnType : new()
        {
            return mRepository.FindSingleIncludeString(x => x.Id == id && x.RecordStatus == RecordStatus.Enabled, includeProperties);
        }

        public virtual async Task<T> Add(T entity)
        {
            

                entity.RecordStatus = RecordStatus.Enabled;
                entity.CreationDate = entity.LastUpdatedDate = DateTime.Now;

                await mRepository.Add(entity);
                int affectedRecoreds = await mRepository.Save();
                if (affectedRecoreds == 0)
                    throw new Exception("no records saved");
                return entity;

        }

        public virtual async Task<List<ReturnType>> AddMultipleEntities<ReturnType, InputType>(List<InputType> entity) where ReturnType : new()
        {
            var entityToSave = mapper.Map<List<T>>(entity);

            foreach (var item in entityToSave)
            {
                item.RecordStatus = RecordStatus.Enabled;
                item.CreationDate = item.LastUpdatedDate = DateTime.UtcNow;
            }

            await mRepository.AddMultipleEntities(entityToSave);
            int affectedRecoreds = await mRepository.Save();
            if (affectedRecoreds == 0)
                throw new Exception("no records saved");
            var mappedEntity = mapper.Map<List<ReturnType>>(entityToSave);
            return mappedEntity;

        }

        public virtual async Task<T> Update(long id, T entity)
        {
            var repoEntity = await mRepository.FindSingle(x => x.Id == id && x.RecordStatus == RecordStatus.Enabled);
            if (repoEntity == null || id != repoEntity.Id)
            {
                throw new Exception("Entity not exist or Id not valid");
            }

            var ceationdate = repoEntity.CreationDate;
            var status = repoEntity.RecordStatus;

            entity.LastUpdatedDate = DateTime.Now;
            entity.CreationDate = ceationdate;

            if (repoEntity.RecordStatus == 0)
                entity.RecordStatus = status;


            mRepository.Update(entity);
            int affectedRecoreds = await mRepository.Save();
            return entity;

        }
        public virtual async Task<List<T>> UpdateRange(List<T> entities)
        {

            entities.All(c => { c.LastUpdatedDate = DateTime.Now; return true; });
            try
            {
                mRepository.UpdateRange(entities);

                int affectedRecoreds = await mRepository.Save();
            }
            catch (Exception ex)
            {

            }
            return entities;

        }
        public virtual async Task<ReturnType> Delete<ReturnType>(long id) where ReturnType : new()
        {
            var repoEntity = (await mRepository.FindSingle(x => x.Id == id && x.RecordStatus == RecordStatus.Enabled));
            if (repoEntity == null || id != repoEntity.Id)
            {
                throw new Exception("Entity not exist or Id not valid");
            }
            //TODO: check if result equals 1 with its else can be moved to GetById<T> method

            mRepository.Delete(repoEntity);
            int affectedRecoreds = await mRepository.Save();
            if (affectedRecoreds == 0)
            {
                throw new Exception("no records saved");
            }
            var mapped = mapper.Map<ReturnType>(repoEntity);
            return mapped;

        }

        public async Task<int> ChangeStatus(long id, bool active)
        {
            var errors = new List<Error>();

            var resultItem = await mRepository.FindSingle(x => x.Id == id);

            if (resultItem == null)
            {
                throw new Exception("Entity not exist or Id not valid");
            }

            resultItem.RecordStatus = active ? RecordStatus.Enabled : RecordStatus.Disabled;

            mRepository.Update(resultItem);
            var affected = await mRepository.Save();
            return affected;
        }

        //TODO: suggestion: we can move this logic to seprate class with interface i.e IValidationService, as per Single Rseponsibility Principle
        public List<Error> ValidateObject(T entity)
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            validationResults = entity.Validate(context).ToList();

            var result = Validator.TryValidateObject(entity, context, validationResults, true);

            return validationResults.Select(r => new Error { Code = "Validation Error", Body = r.ErrorMessage }).ToList();
        }
    }
}
