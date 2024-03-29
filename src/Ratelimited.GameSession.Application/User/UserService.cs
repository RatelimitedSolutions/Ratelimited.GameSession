﻿using DotNetCore.Objects;
using DotNetCore.Results;
using Microsoft.EntityFrameworkCore;
using Ratelimited.GameSession.Database;
using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public sealed class UserService : IUserService
    {
        private IAuthService _authService;
        private IUnitOfWork _unitOfWork;
        private IUserRepository _userRepository;

        public UserService
        (
            IAuthService authService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<IResult<long>> AddAsync(UserModel model)
        {
            var validation = await new AddUserModelValidator().ValidateAsync(model);

            if (validation.Failed)
            {
                return Result<long>.Fail(validation.Message);
            }

            var authResult = await _authService.AddAsync(model.Auth);

            if (authResult.Failed)
            {
                return Result<long>.Fail(authResult.Message);
            }

            var user = UserFactory.Create(model, authResult.Data);

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return Result<long>.Success(user.Id);
        }

        public async Task<IResult> DeleteAsync(long id)
        {
            var authId = await _userRepository.GetAuthIdByUserIdAsync(id);

            await _userRepository.DeleteAsync(id);

            await _authService.DeleteAsync(authId);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public Task<UserModel> GetAsync(long id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task InactivateAsync(long id)
        {
            var user = new User(id);

            user.Inactivate();

            await _userRepository.UpdateStatusAsync(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters)
        {
            return _userRepository.Queryable.Select(UserExpression.Model).ListAsync(parameters);
        }

        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            return await _userRepository.Queryable.Select(UserExpression.Model).ToListAsync();
        }

        public async Task<IResult> UpdateAsync(UserModel model)
        {
            var validation = await new UpdateUserModelValidator().ValidateAsync(model);

            if (validation.Failed)
            {
                return Result.Fail(validation.Message);
            }

            var user = await _userRepository.GetAsync(model.Id);

            if(user == default)
            {
                return Result.Success();
            }

            user.ChangeFullName(model.Name, model.Surname);

            user.ChangeEmail(model.Email);

            await _userRepository.UpdateAsync(user.Id, user);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
