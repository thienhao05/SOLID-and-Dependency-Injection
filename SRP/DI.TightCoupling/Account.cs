using System;
using System.Collections.Generic;
using System.Text;

namespace DI.TightCoupling
{
    //Entity
    public class Account
    {
        //chứa info id, name, address, email, phone, whatsapp id, status, role, reg-date....
        //rút lại những info cần thiết thôi để dùng luân chuyển giữa các tầng thì ta chế thêm 1 class Dto - data transfer object
        //Account ---- đổ qua lại data 2 object -- mapper-- AccountDto (java)
    }
}
