using Microsoft.AspNetCore.Mvc;
using Pulse.Library.Services.Schema;
using System.Net.Mime;

namespace API.Controllers;

[ApiController]
[Route("api/schema")]
public class SchemaController : ControllerBase
{
    /// private readonly IActionService actionService;
    /// private readonly ISchemaService schemaService;
    private readonly ISchemaService schemaService;

    public SchemaController(ISchemaService schemaService)
    {
        this.schemaService = schemaService;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchemaView))]
    public IActionResult GetEntityActions(string entityName, Guid entityId)
    {
        return ValidateAndExecute(entityName, () =>
        {
            return Ok(schemaService.ReadSchema().MapToViewModel());
        });
    }

    private IActionResult ValidateAndExecute(string entityTypeName, Func<IActionResult> action,
        Guid? entityId = null)
    {
        CheckPreconditions();

        /// if (!schemaService.CheckIfTypeExists(entityTypeName))
        /// {
        ///     throw new EntityTypeNotFoundException(entityTypeName);
        /// }

        /// try
        /// {
        ///     return action.Invoke();
        /// }
        /// catch (FormattedMessageException e)
        /// {
        ///     throw new ActionApiException(e);
        /// }

        return action.Invoke();
    }

    private void CheckPreconditions()
    {
        /// if (!assemblyChecker.AssembliesLoaded())
        /// {
        ///     throw new AssembliesNotFoundException();
        /// }
    }
}