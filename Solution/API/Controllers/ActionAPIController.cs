using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/actions")]
public class ActionApiController : ControllerBase
{
    // private readonly IActionService actionService;
    // private readonly ISchemaService schemaService;

    [HttpGet]
    [Route("{entityName}/{entityId:guid}")]
    public IActionResult GetEntityActions(string entityName, Guid entityId)
    {
        return ValidateAndExecute(entityName, () =>
        {
            // var actions = actionService.GetEntityActions(entityName, entityId, false);
        
            // if (actions.Any())
            if (true)
            {
                return Ok();
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
        // if (!assemblyChecker.AssembliesLoaded())
        // {
        //     throw new AssembliesNotFoundException();
        // }
    }
}