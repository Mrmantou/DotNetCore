﻿using Albert.Demo.Application.UrlNavs.Dtos;
using Albert.Demo.Domain.UrlNavs;
using Albert.Domain.Repositories;
using Albert.Domain.Uow;
using Albert.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Demo.Application.UrlNavs
{
    public class UrlNavAppService : IUrlNavAppService
    {
        private readonly IRepository<UrlNav, Guid> repository;
        private readonly IUnitOfWork unitOfWork;

        public UrlNavAppService(IRepository<UrlNav, Guid> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Create(UrlNav urlNav)
        {
            await repository.InsertAsync(urlNav);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<UrlNav>> GetUrlNavs(GetUrlNavArg input)
        {
            return await repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Title), u => u.Title.Contains(input.Title))
                .WhereIf(input.Id.HasValue, u => u.Id == input.Id.Value)
                .ToListAsync();
        }

        public async Task Update(UrlNav urlNav)
        {
            await repository.UpdateAsync(urlNav);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await repository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
