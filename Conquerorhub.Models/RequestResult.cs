using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Request.Models
{
   
        public class RequestResult<T> where T : class
        {
            [JsonProperty(PropertyName = "result")]
            public T Entity { get; private set; }

            [JsonProperty(PropertyName = "error_messages")]
            public List<string> ErrorMessages { get; set; }

            public RequestResult()
            {
            }

            public RequestResult(T value)
            {
                Entity = value;
                ErrorMessages = null;
            }

            public RequestResult(T value, List<string> errorMessages)
            {
                Entity = value;
                ErrorMessages = errorMessages;
            }

            public RequestResult(T value, string errorMessage)
            {
                Entity = value;
                ErrorMessages = new List<string>() { errorMessage };
            }

            [JsonIgnore]
            public bool HasError => (ErrorMessages != null && ErrorMessages.Count > 0);

            public static RequestResult<T> NewUnexpectedError(T value)
            {
                return new RequestResult<T>(value, "Unexpected error has occurred");
            }
        }

        public class BooleanResult
        {
            public bool Result { get; set; }

            public BooleanResult(bool value)
            {
                Result = value;
            }

            public static BooleanResult Empty => new BooleanResult(false);
        }

        public class GuidResult
        {
            public Guid Result { get; set; }

            public GuidResult(Guid value)
            {
                Result = value;
            }

            public static GuidResult Empty => new GuidResult(Guid.Empty);
        }

        public class VoidResult
        {
            public VoidResult()
            {
            }
        }
    public class IntResult
    {
        public int[] Result { get; set; }

        public IntResult(int[] value)
        {
            Result = value;
        }

        public static int[] Empty => new int[2] {0,0 };
    }
}


