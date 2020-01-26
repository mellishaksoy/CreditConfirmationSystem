using DataAccessHandler.Domain;
using DataAccessHandler.Infrastructure.Contexts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessHandler.Infrastructure.Repositories
{
    public abstract class BaseMongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        private IMongoCollection<T> _document;
        public IMongoDbContext Context { get; }

        protected BaseMongoRepository(string connectionString)
        {
            Context = new MongoDbContext(connectionString);
        }

        #region Properties
        protected IMongoCollection<T> Document
        {
            get
            {
                if (_document == null)
                    _document = Context.Collection<T>();

                return _document;
            }
        }

        

        public IMongoQueryable<T> Table
        {
            get
            {
                return Document.AsQueryable();
            }
        }
        
        public int Count
        {
            get
            {
                return Table.Count();
            }
        }
        #endregion

        #region Add
        
        public void Add(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                Document.InsertMany(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Add(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                Document.InsertOne(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                await Document.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                await Document.InsertManyAsync(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update

        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                foreach (var entity in entities)
                    Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
                Document.ReplaceOne(filter, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
                await Document.ReplaceOneAsync(filter, entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                foreach (var entity in entities)
                    await UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete

        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                foreach (var entity in entities)
                    Delete(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
                Document.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException(nameof(entity));

                var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
                await Document.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                foreach (var entity in entities)
                    await DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Query

        public T Find(Expression<Func<T, bool>> filter)
        {
            return Document.Find(filter).FirstOrDefault();
        }
        
        public T Find(object Id)
        {
            return Document.Find(x => x.Id == (Id as string)).FirstOrDefault();
        }
        
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return Document.Find(filter).ToList();
        }
        
        

        public async Task<T> FindAsync(object Id)
        {
            var res = await Document.FindAsync(x => x.Id == (Id as string));
            return res.FirstOrDefault();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            var res = await Document.FindAsync(filter);
            return res.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            var res = await Document.FindAsync(filter);
            return res.ToList();
        }
        #endregion
    }
}
