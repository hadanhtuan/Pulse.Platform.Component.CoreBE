using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/actions")]
    [ApiExplorerSettings(GroupName = "actions")]
    public class ActionApiController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ActionApiController> _logger;

        public ActionApiController(ILogger<ActionApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "{entityName}/{entityId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEntityActions(string entityName, Guid entityId)
        {
            return ValidateAndExecute(entityName, () =>
            {
                var actions = actionService.GetEntityActions(entityName, entityId, false);
        
                if (actions.Any())
                {
                    return Ok(actions);
                }
                else
                {
                    return NoContent();
                }
            });
        }

        private IActionResult ValidateAndExecute(string entityTypeName, Func<IActionResult> action,
            Guid? entityId = null)
        {
            CheckPreconditions();

            // if (!schemaService.CheckIfTypeExists(entityTypeName))
            // {
            //     throw new EntityTypeNotFoundException(entityTypeName);
            // }

            // try
            // {
            //     return action.Invoke();
            // }
            // catch (FormattedMessageException e)
            // {
            //     throw new ActionApiException(e);
            // }
            
            return action.Invoke();
        }

        private void CheckPreconditions()
        {
        }
    }
}