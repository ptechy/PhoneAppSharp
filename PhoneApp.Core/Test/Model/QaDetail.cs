namespace PhoneApp.Core.Test.Model
{
	public class QaDetail
	{
		public string Description { get; }
		public string Result { get; }

		public QaDetail( string description, string result)
		{
			Description = description;
			Result = result;
		}
	}
}
