using System;

namespace Wwfd.Core.Exceptions
{
	internal class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
		{
		}

		public EntityNotFoundException(string message) : base(message)
		{
		}

		public EntityNotFoundException(Type type, int id) : base(string.Format("The entity type {0} with the id of {1} was not found.", type, id))
		{
		}
	}
}