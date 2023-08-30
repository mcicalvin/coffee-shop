﻿using Microsoft.Extensions.Logging;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Interfaces.Services;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services.CommandServices
{
    public abstract class BaseCommandService<Ent> : BaseService, IBaseCommandService<Ent> where Ent : BaseEntity
    {
        protected ICommandRepository<Ent> commandRepository;
        public BaseCommandService(ICommandRepository<Ent> commandRepository,
            ILogger<BaseCommandService<Ent>> logger)
            : base(logger)
        {
            this.commandRepository = commandRepository;
        }

        public CreationResponse<Ent> Create(Ent entity)
        {
            var response = new CreationResponse<Ent>();

            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;

                commandRepository.Create(entity);

                AfterCreation(entity);

                response.CreatedEntity = entity;
                response.StatusCode = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                LogError(response, ex, $"Unknown error occured during creation of [{nameof(entity)}]");
            }

            return response;
        }

        public abstract void AfterCreation(Ent entity);

        public BaseResponse Update(Ent entity)
        {
            var response = new BaseResponse();

            try
            {
                if (entity != null)
                {
                    entity.UpdatedAt = DateTime.Now;
                    commandRepository.Update(entity);
                    response.StatusCode = ResponseStatus.Success;
                }
                else
                {
                    response.StatusCode = ResponseStatus.Fail;
                    response.Message = "Item not deleted.";
                }
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occured when trying to delete.");
            }
            return response;
        }

        public BaseResponse Delete(long id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse Delete(Ent entity)
        {
            var response = new BaseResponse();

            try
            {
                if (entity != null)
                {
                    entity.Status = EntityStatus.Deleted;
                    entity.UpdatedAt = DateTime.Now;
                    commandRepository.Update(entity);
                    response.StatusCode = ResponseStatus.Success;
                }
                else
                {
                    response.StatusCode = ResponseStatus.Fail;
                    response.Message = "Item not deleted.";
                }
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occured when trying to delete.");
            }
            return response;
        }
    }
}
