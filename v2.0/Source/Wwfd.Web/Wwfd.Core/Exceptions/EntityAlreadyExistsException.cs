using System;

namespace Wwfd.Core.Exceptions
{
	public class EntityAlreadyExistsException : Exception
	{
		public EntityAlreadyExistsException()
		{
		}

		public EntityAlreadyExistsException(string message) : base(message)
		{
		}

		public EntityAlreadyExistsException(Type type, int id) : base(string.Format("The entity type {0} with the id of {1} already exists.", type, id))
		{
		}
	}
}