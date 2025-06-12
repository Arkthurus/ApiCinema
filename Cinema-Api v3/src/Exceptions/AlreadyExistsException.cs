namespace Cinema_Api.src.Exceptions;

public class AlreadyExistsException : BusinessException
{
	public AlreadyExistsException() { }

	public AlreadyExistsException(string? message)
		: base(message) { }
}
