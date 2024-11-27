namespace Api.Controllers.Exceptions;

internal class ControllerNotFoundException(string message) : Exception(message)
{
}
