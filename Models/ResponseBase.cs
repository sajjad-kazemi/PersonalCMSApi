namespace Models
{
	public class ResponseBase
	{
		public ResponseBase(bool isSuccess , string message = null)
		{
			if (message is null)
			{
				if (isSuccess)
				{
					message = "عملیات با موفقیت انجام شد.";
				}
				else
				{
					message = "عملیات با خطا مواجه شد.";
				}
			}
			Message = message;
			IsSuccess = isSuccess;
		}
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
	}
}
