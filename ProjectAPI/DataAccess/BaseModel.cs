using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class BaseModel<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Contexto
        /// </summary>
        JujuTestContext _context;
        /// <summary>
        /// Entidad
        /// </summary>
        protected DbSet<TEntity> _dbSet;
        protected DbSet<Customer> _dbSetCustomer;
        protected DbSet<Post> _dbSetPost;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BaseModel(JujuTestContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _dbSetCustomer = _context.Set<Customer>();
            _dbSetPost = _context.Set<Post>();
        }


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll
        {
            get { return _dbSet; }
        }



        /// <summary>
        /// Consulta las entidades en la columna Name para validar que no sea iguales
        /// </summary>
        public virtual IQueryable<TEntity> FindByColumnName(string columnName, string value)
        {
            return _dbSet.Where(e => EF.Property<string>(e, columnName) == value);
        }


        /// <summary>
        /// Consulta las entidades en la columna CustomerId para validar que coincidan 
        /// </summary>
        public virtual IQueryable<Customer> UserExisting(int CustomerId, int value)
        {
            return _dbSetCustomer.Where(e => EF.Property<int>(e, "CustomerId") == value);
        }



        /// <summary>
        /// Consulta una entidad por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }



        /// <summary>
        /// Crea un entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }


        public List<Post> CreateMultiplePosts(List<Post> posts)
        {
            _dbSet.AddRange((IEnumerable<TEntity>)posts);
            _context.SaveChanges();

            return posts;
        }



        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity editedEntity, TEntity originalEntity, out bool changed)
        {

            _context.Entry(originalEntity).CurrentValues.SetValues(editedEntity);
            changed = _context.Entry(originalEntity).State == EntityState.Modified;
            _context.SaveChanges();

            return originalEntity;
        }

       

        /// <summary>
        /// Elimina una entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
        
        public virtual IQueryable<Post> DeletePostAssociated(int CustomerId, int value)
        {
            var postsToDelete = _dbSetPost.Where(p => p.CustomerId == value);
            _dbSetPost.RemoveRange(postsToDelete);
            _context.SaveChanges();

            return postsToDelete;
        }


        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
