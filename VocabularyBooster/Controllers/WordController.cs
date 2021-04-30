using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VocabularyBooster.Service;
using VocabularyBooster.ViewModel;

namespace VocabularyBooster.Controllers
{
    [Route(CommonRoute.BaseApiRoute + "/[controller]")]
    [ApiController]
    public class WordController : Controller
    {
        private readonly IWordService wordService;
        private readonly IMapper mapper;

        public WordController(IWordService wordService, IMapper mapper)
        {
            this.wordService = wordService;
            this.mapper = mapper;
        }

        [HttpPut("add", Name = nameof(AddWord))]
        [SwaggerResponse(StatusCodes.Status200OK, "The text.", typeof(bool))]
        [SwaggerOperation(OperationId = nameof(AddWord))]
        public async Task<IActionResult> AddWord(Word word)
        {
            await this.wordService.AddOrUpdateWord(this.mapper.Map<VocabularyBooster.Core.GraphModel.Word>(word));
            return this.Ok(true);
        }

        [HttpGet("search", Name = nameof(SearchWord))]
        [SwaggerResponse(StatusCodes.Status200OK, "The word.", typeof(List<Word>))]
        [SwaggerOperation(OperationId = nameof(SearchWord))]
        public async Task<IActionResult> SearchWord(string phrase)
        {
            var textList = await this.wordService.SearchWord(phrase);
            return this.Ok(textList.Select(text => this.mapper.Map<Word>(text)).ToList());
        }
    }
}
