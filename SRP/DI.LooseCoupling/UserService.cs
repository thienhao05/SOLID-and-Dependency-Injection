using System;
using System.Collections.Generic;
using System.Text;

namespace DI.LooseCoupling
{
    public class UserService
    {
        //SRP: TỚ GIỎI VỤ CRUD TABLE ACCOUNT, TRONG RAM
        //TỚ CẦN 2 DEPENDENCY, MỖI THẰNG LO 1 VIỆC 
        //USER-REPO VÀ EMAIL-SENDER
        private UserRepo userRepo; // có new hay ko, có là tight-coupling 
        //lỏng ra, thì đó sẽ là DI @Autowire bên java NGHĨA LÀ DI, NGHĨA LÀ KO FULL CONTROL 

        //private EmailSender emailSender = new EmailSender(); // cách này thì mình full control, ko DI
        //@Autowire - AI ĐÓ KHÁC NEW VÀ TIẾM CHÍNH OBJECT VÀO CHO MÌNH SERVICE
        //Spring/SpringBoot làm giúp việc new, chính/tiêm
        private EmailSender emailSender; //KO NEW THÌ PHẢI ĐC ĐƯA VÀO!!!

        //CÓ NHIỀU CÁCH ĐỂ ĐƯA OBJECT TỪ NGOÀI VÀO TRONG 1 CLASS
        //1. TRỰC TIẾP QUA FIELD, BIẾN emailSender THÀNH PUBLIC - NGUY HIỂM VI PHẠM ENCAPSULATION. VẪN MUỐN QUA FIELD MÀ PRIVATE 
        // - DÙNG KĨ THUẬT NÂNG CAO REFLECTION!!!!
        //  FIELD INJECTION(DÙNG REFLECTION OR LÀ FRAMEWORK, IOC FRAMEWORK)
        // ĐỨNG BÊN NGOÀI USER SERVICE MÀ GẮN ĐC VÀO BIẾN PRIVATE Ở ĐÂY MÌNH ĐANG LÀ emailSender
        // của mình đang là biến private thì ko được có thể chuyển thàn public nhưng mà như thế thì sẽ vi phạm Encapsulation

        //2. TRUYỀN VÀO THÔNG QUAN CONSTRUCTOR!!! MLEM NHẤT
        //TẠO OBJECT CHÍNH MÌNH QUA CONSTRUCTOR VÀ NHẬN THÊM ĐỒ QUA THAM SỐ CONSTRUCTOR 
        //OBJECT DEPENDENCY ĐI QUA, ĐƯA QUA CONSTRUCTOR

        //3. SETTER - TRUYỀN QUA HÀM SET() LƯỜI KO GỌI HÀM SET() THÌ DEPENDENCY BỊ NULL

        //4. DÙNG FRAMEWORK/THƯ VIỆN BÊN NGOÀI TỰ KIỂM SOÁT VIỆC TẠO OBJECT DEPENDENCY 
        //  VÀ TIÊM/CHÍCH VÀO: SPRING/SPRINGBOOT!!!

        // => FIELD INJECTION MÌNH SẼ KO HỌC VÌ NÓ VI PHẠM NGUYÊN LÝ ENCAPSULATION, NHƯNG MÀ DÙNG CÁC FRAMEWORK THÌ NÓ SẼ KO VI PHẠM NGUYÊN LÝ LUÔN
        // MÌNH SẼ CHƠI REFLECTION NGẦM

        //CHÍNH/TIÊM 2 THẰNG DEPENDENCY TỪ NGOÀI VÀO TRONG MÌNH SERVICE QUA CONTRUCTOR GẮN VÀO
        //Y CHANG TRUYỀN YOB, GPA --> 2 THẰNG NÀY LÀ PRIMITIVE, VALUE THUẦN
        //CÒN NÀY THÌ MÌNH TRUYỀN VÀO 2 CÁI OBJECT VÀO
        public UserService(UserRepo userRepo, EmailSender emailSender)
        {
            this.userRepo = userRepo;
            this.emailSender = emailSender;
        }


        public UserService(EmailSender emailSender) //LẤY MÌNH NEW CHO NÓ DỄ
        {
            //this.userRepo = userRepo;
            this.emailSender = emailSender;
        }

        public void RegisterAccount(Account acc)
        {
            //TODO: DÙNG REPO XUỐNG TABLE

            //GỬI EMAIL THOY
            //
            emailSender.SendEmail("demoLooseCoupling@gmail.com", "Please input to the OTP....");
        }

    }
}
