using System;
using System.Text;

namespace UrbanEngine.Core.Exceptions
{
	public class RoomUnavailableException : Exception
	{
		private readonly DateTimeOffset? _startDateTime;
		private readonly DateTimeOffset? _endDateTime;

		public override string Message => GetErrorMessage();

		public RoomUnavailableException(DateTimeOffset? startDateTime, DateTimeOffset? endDateTime)
			: base()
		{
			_startDateTime = startDateTime;
			_endDateTime = endDateTime;
		}

		public RoomUnavailableException(string message, DateTimeOffset? startDateTime, DateTimeOffset? endDateTime)
			: base(message)
		{
			_startDateTime = startDateTime;
			_endDateTime = endDateTime;
		}

		private string GetErrorMessage()
		{
			var sb = new StringBuilder();

			sb.Append("The requested room is unavailable during the requested times ");

			if(_startDateTime.HasValue)
				sb.Append($"start: {_startDateTime} ");

			if(_startDateTime.HasValue && _endDateTime.HasValue)
				sb.Append("to ");

			if(_endDateTime.HasValue)
				sb.Append($"end: {_endDateTime} ");

			sb.Append(base.Message);

			return sb.ToString();
		}
	}
}
