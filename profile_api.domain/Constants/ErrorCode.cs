using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profile_api.domain.Constants
{
    public static class ErrorCode
    {
        public const int SUCCESS_CODE = 200;
        public const int ERROR_CODE = 500;

        public const string SUCCESS_MESSAGE = "Thành công.";
        public const string ERROR_MESSAGE = "Đã xảy ra lỗi. Vui lòng thử lại sau.";
    }
}
