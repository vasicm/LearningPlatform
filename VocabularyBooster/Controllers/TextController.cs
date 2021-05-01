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
    public class TextController : Controller
    {
        private readonly IWordService wordService;
        private readonly IMapper mapper;

        public TextController(IWordService wordService, IMapper mapper)
        {
            this.wordService = wordService;
            this.mapper = mapper;
        }

        [HttpPut("add", Name = nameof(AddText))]
        [SwaggerResponse(StatusCodes.Status200OK, "The text.", typeof(bool))]
        [SwaggerOperation(OperationId = nameof(AddText))]
        public async Task<IActionResult> AddText(Text text)
        {
            await this.wordService.AddText(this.mapper.Map<VocabularyBooster.Core.GraphModel.Text>(text));
            return this.Ok(true);
        }

        [HttpGet("search", Name = nameof(SearchText))]
        [SwaggerResponse(StatusCodes.Status200OK, "The text.", typeof(List<Text>))]
        [SwaggerOperation(OperationId = nameof(SearchText))]
        public async Task<IActionResult> SearchText(string phrase)
        {
            var textList = await this.wordService.SearchText(phrase);
            return this.Ok(textList.Select(text => this.mapper.Map<Text>(text)).ToList());
        }
    }
}
