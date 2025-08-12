using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using profile_api.domain.Common;
using profile_api.domain.Constants;
using profile_api.domain.DTOs.Category;
using profile_api.domain.Repositories.Interfaces;

namespace profile_api.domain.Handlers
{
    public class CategoryHandler
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryHandler> _logger;

        public CategoryHandler(ICategoryRepository categoryRepo, IMapper mapper, ILogger<CategoryHandler> logger)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<List<CategoryDTO>>> GetAll()
        {
            try
            {
                var categories = await _categoryRepo.FindAllAsync();
                var result = _mapper.Map<List<CategoryDTO>>(categories);
                return new Response<List<CategoryDTO>>(ErrorCode.SUCCESS_CODE, ErrorCode.SUCCESS_MESSAGE, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new Response<List<CategoryDTO>>(ErrorCode.ERROR_CODE, ErrorCode.ERROR_MESSAGE, null);
            }
        }
    }
}
