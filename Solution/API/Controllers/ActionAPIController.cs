using API.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Schema.Service;

namespace API.Controllers
{
    [ApiController]
    [Route("api/actions")]
    [ApiExplorerSettings(GroupName = "actions")]
    public class ActionApiController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IActionService actionService;
        private readonly ISchemaService schemaService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory">Supplied by DI</param>
        /// <param name="auditLoggerFactory">Supplied by DI</param>
        /// <param name="actionService">Supplied by DI</param>
        /// <param name="schemaService"></param>
        /// <param name="assemblyChecker"></param>
        /// <param name="userContext">Supplied by DI</param>
        public ActionApiController(
            ILogger<ActionApiController> logger,
            IActionService actionService
        )
        {
            logger = logger;
            this.actionService = actionService;
        }

        [HttpGet(Name = "{entityName}/{entityId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEntityActions(string entityName, Guid entityId)
        {
            logger.LogDebug($"Received request to list all actions for entity type: '{entityName}' " +
                            $"and id: '{entityId}'.");
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
            // if (!assemblyChecker.AssembliesLoaded())
            // {
            //     throw new AssembliesNotFoundException();
            // }
        }
    }
}