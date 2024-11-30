namespace TaskPulse.Domain.Helpers;

public class Constants
{

   public class ErrorMessages
   {
      public const string UserIdExist = "User already exists with same email or username.";
      public const string UserIdNonExist = "Username is not present. Please Register First.";
      public const string InvalidPassword = "Invalid password. Please enter the correct password.";
      public const string GeneralException = "Please try again after some time.";
      public const string UserRegistration = "UserName, password and Email is required for login.";
      public const string UserLogin = "UserName and password is required for login.";
      public const string GetTaskLogin = "UserName and password is required for login.";
      public const string GetTaskNull = "No tasks found for the provided user ID";
      public const string CaptchaVerfiy = "Captcha verification failed.";
   }

   public class ValidationErrorMessages
   {
      public const string UserIdRequired = "UserId is required";
      public const string TaskIdRequired = "TaskId is required";
      public const string TitleRequired = "Title is required and it should be greater than 25 characters";
      public const string StatusRequired = "Status is required";
      
      public const string UserNameRequired = "UserName is required";
      public const string PasswordRequired = "Password is required";
      public const string EmailIdRequired = "Valid Email Id is required";
      public const string CaptchaRequired = "Captcha is required";
   }
    
    
}