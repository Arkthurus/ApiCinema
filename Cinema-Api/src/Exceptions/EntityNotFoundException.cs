using Cinema_Api.src.Exceptions;

public class EntityNotFoundException : BusinessException
{
	public EntityNotFoundException() { }

	public EntityNotFoundException(string? message)
		: base(message) { }
}
