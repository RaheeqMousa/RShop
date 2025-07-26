using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.Models;
using RShop.DAL.Data;
using RShop.DAL.Repositories.Interfaces;
using RShop.DAL.Repositories.Classes;
using Mapster;

namespace RShop.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> genericRepository;

        public GenericService(IGenericRepository<TEntity> grepo)
        {
            genericRepository = grepo;
        }

        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return genericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = genericRepository.getById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            return genericRepository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll()
        {
            var entity = genericRepository.getAll();
            return entity.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            return genericRepository.getById(id) is null ? default : genericRepository.getById(id).Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = genericRepository.getById(id);
            if (entity is null)
            {
                return false; // Entity not found
            }
            entity.Status = (entity.Status == Status.Active) ? Status.Inactive : Status.Active;
            genericRepository.Update(entity);
            return true;

        }

        public int Update(int id, TRequest request)
        {
            var entity = genericRepository.getById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            

            return genericRepository.Update(request.Adapt<TEntity>());
        }
    }
}
