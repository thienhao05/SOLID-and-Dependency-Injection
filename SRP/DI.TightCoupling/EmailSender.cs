using System;
using System.Collections.Generic;
using System.Text;

namespace DI.TightCoupling
{
    public class EmailSender
    {
        //TUI, GÃ RẤT GIỎI CHUYỂN GỬI EMAIL, TUI KO DÍNH DÁNG GÌ ĐẾN SMS, KO DÍNH ĐẾN WHATSAPP, TUI THỎA NGUYÊN LÍ S/SRP TRONG SOLID
        //TUI CHỈ CHỨA NHIỀU HÀM CHUYÊN LIÊN QUAN TỚI EMAIL - 1 CHỦ THỂ 
        //SAU NÀY NÂNG CẤP CODE TỐT, CŨNG CHỈ LÀ XOAY QUANH EMAIL, 1 LÝ DO/CHỦ THỂ SỬA ĐỔI MÀ THOY

        //                          to:                 nội dung email
        //hàm này gửi mail tới người đăng kí account, thông tin email nhập từ màn hình đăng kí, đi qua Controller đến Service đến đây !!!
        //email của user đăng kí nằm trong Account Entity (đơn giản), nằm trong AccountDto (bản cắt bớt field từ Entity)
        public void SendEmail(string recipient, string message)
        {
            //TODO: LOGIC XỬ LÝ GỬI EMAIL: SETUP ACCOUNT ĐỂ ĐÓNG VAI NGƯỜI GỬI (FROM - MÌNH GỬI, APP GỬI)
            //      FORMAT EMAIL CHO PRO....

            //thông báo câu thành công 
            Console.WriteLine("Mail was sent to: " + recipient + " successfully!!!!!");

            //khai báo dependency bên trong cái Service thông qua cái việc gửi mail
        }
    }
}
