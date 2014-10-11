using Eclus.NET.MKO.Enums;

namespace Eclus.NET.MKO.Exceptions
{
    public class MKODeviceException : System.Exception
    {
        #region Public properties



        public ErrorType ErrorType { get; private set; }



        #endregion
        #region Public constructors



        public MKODeviceException(ErrorType type)
        {
            ErrorType = type;
        }

        public MKODeviceException(ErrorType type, string message)
            : base(message)
        {
            ErrorType = type;
        }



        #endregion
    }
}