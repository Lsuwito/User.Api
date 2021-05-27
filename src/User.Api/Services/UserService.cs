using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using User.Api.Exceptions;
using User.Api.Models;
using User.Api.Repositories;
using User.Api.Repositories.Entities;
using Flurl;

namespace User.Api.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationCursorConverter _paginationCursorConverter;
        private readonly IOptions<PaginationOptions> _paginationOptions;

        public UserService(IUserRepository userRepository, IMapper mapper, 
            IPaginationCursorConverter paginationCursorConverter,
            IOptions<PaginationOptions> paginationOptions)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _paginationCursorConverter = paginationCursorConverter;
            _paginationOptions = paginationOptions;
        }
    
        /// <inheritdoc />
        public async Task<Models.User> CreateUserAsync(UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            entity.UserId = await _userRepository.CreateUserAsync(entity);
            return _mapper.Map<Models.User>(entity);
        }

        /// <inheritdoc />
        public async Task<Models.User> GetUserAsync(Guid userId)
        {
            var entity = await _userRepository.GetUserAsync(userId);
            
            if (entity == null)
            {
                throw new ResourceNotFoundException("User was not found.");
            }
            
            return _mapper.Map<Models.User>(entity);
        }
        
        /// <inheritdoc />
        public async Task DeleteUserAsync(Guid userId)
        {
            if (!await _userRepository.DeleteUserAsync(userId))
            {
                throw new ResourceNotFoundException("Users was not found.");
            }
        }
        
        /// <inheritdoc />
        public async Task<Models.User> UpdateUserAsync(Guid userId, UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            entity.UserId = userId;
            
            if (!await _userRepository.UpdateUserAsync(entity))
            {
                throw new ResourceNotFoundException("Users was not found.");
            }
            
            return _mapper.Map<Models.User>(entity);
        }

        /// <inheritdoc />
        public async Task<Users> GetUsersAsync(SortByEnum sortBy, int limit, string cursorId)
        {
            PaginationCursor cursor = null;

            if (!string.IsNullOrWhiteSpace(cursorId))
            {
                try
                {
                    cursor = _paginationCursorConverter.FromString(cursorId);
                }
                catch (Exception ex) when (ex is FormatException || ex is JsonException)
                {
                    throw new InvalidRequestException("Invalid cursor ID.", ex);
                }
            }

            var entities = await _userRepository.GetUsersAsync(
                sortBy.ToString().ToLowerInvariant(), true, limit, 
                cursor?.LastSortValue, cursor?.LastSecondarySortValue);
            
            return new Users
            {
                Items = _mapper.Map<IEnumerable<Models.User>>(entities),
                NextUrl = GenerateNextUrl(sortBy, limit, entities.LastOrDefault())
            };
        }

        private string GenerateNextUrl(SortByEnum sortBy, int size, UserEntity lastRecord)
        {
            if (lastRecord == null)
            {
                return null;
            }

            var entityType = lastRecord.GetType();
            var property = entityType.GetProperty(sortBy.ToString());
            
            if (property == null)
            {
                throw new InvalidOperationException($"Property {sortBy} does not exist in {entityType.FullName}");
            }

            var cursor = new PaginationCursor
            {
                LastSortValue = property.GetValue(lastRecord)?.ToString(),
                LastSecondarySortValue = lastRecord.UserId.ToString()
            };

            var cursorId = _paginationCursorConverter.ToString(cursor);

            return _paginationOptions.Value.BaseUrl
                .AppendPathSegment("users")
                .SetQueryParams(new
                {
                    sortBy,
                    size,
                    cursorId
                }).ToString();
        }
    }
}