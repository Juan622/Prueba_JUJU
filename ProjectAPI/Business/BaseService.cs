using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class BaseService<TEntity> where TEntity : class, new()
    {
        protected BaseModel<TEntity> _BaseModel;
        private object PostController;

        public BaseService(BaseModel<TEntity> baseModel)
        {
            _BaseModel = baseModel;
        }

        #region Repository


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _BaseModel.GetAll;
        }

        /// <summary>
        /// Crea un entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            return _BaseModel.Create(entity);
        }



        public virtual async Task<TEntity> FindByColumnName(string columnName, string entity)
        {
            var nameRepaet =  await _BaseModel.FindByColumnName(columnName, entity).FirstOrDefaultAsync();

            if (nameRepaet == null)
            {
                return nameRepaet;
            }
            throw new Exception("Ya existe un usuario con este mismo nombre");
        }



        public virtual async Task<List<Customer>> UserExisting(int customerId, int entity)
        {
            var userRepaet = await _BaseModel.UserExisting(customerId, entity).ToListAsync();

            if (userRepaet.Any())
            {
                return userRepaet;
            }
            throw new Exception("No existe un usuario asociado de tu Post");
        }


        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(object id, TEntity editedEntity, out bool changed)
        {
            TEntity originalEntity = _BaseModel.FindById(id);
            return _BaseModel.Update(editedEntity, originalEntity, out changed);
        }


        /// <summary>
        /// Elimina una entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            return _BaseModel.Delete(entity);
        }

        public virtual async Task<Post> DeletePostAssociated(int CustomerId, int value)
        {
            return await _BaseModel.DeletePostAssociated(CustomerId, value).FirstOrDefaultAsync(); ;
        }

        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _BaseModel.SaveChanges();
        }


        /// <summary>
        /// Guarda multiples Post con NewApi
        /// </summary>
        public List<Post> CreateMultiplePosts(List<Post> posts)
        {
            var multiplesPost = _BaseModel.CreateMultiplePosts(posts);

            if (multiplesPost.Any())
            {
                return multiplesPost;
            }
            throw new Exception("Debes ingresar al menos un post");
        }

        #endregion
    }
}
