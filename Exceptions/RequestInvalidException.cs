namespace BlogPost.Exceptions{
    public class RequestFieldInvalidException(Dictionary<string, string> fieldErrors, string message) : Exception(message){
      public readonly Dictionary<string, string>  FieldErrors = fieldErrors;
  }
}