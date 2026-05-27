using DI.LooseCoupling;

namespace DI.LooseCouplingController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //demo này là của DI.LooseCoupling
            //MUỐN CÓ SERVICE, CẦN CÓ EMAIL-SENDER TRUYỀN VÀO
            EmailSender sender = new EmailSender(); //DEPENDENCY ĐC CHỦ ĐỘNG LỘ DIỆN, NEW
            UserService service = new UserService(sender); //CHÍNH TIÊM OBJ BÊN NGOÀI VÀO TRONG SERVICE
            //luôn luôn đảm bảo sender được new, vì mình dùng loose coupling, mình đã thả lỏng cho chính mình
            //ko truyền vào thì t đố new đc vì t bắt buộc m phải DI vào nhưng setter là t ko thèm set là bị null nhen
            //xài bình thường
            service.RegisterAccount(new Account());

            //Main CLASS CHỦ ĐỘNG TẠO OBJECT CLASS B, DEPENDENCY, ĐƯA VÀO CLASS CHÍNH
            //THẰNG CHỨA, TẠO CÁC DEPENDENCY ĐC GỌI LÀ CONTAINER
            
            //CONTAINER LÀ NƠI CHỨA CÁC DEPENDENCY 
            //CHỦ ĐỘNG TẠO DEPENDENCY, ĐƯA VÀO, TRONG SERVICE CHÍNH A
            //CLASS CHÍNH THÌ KĨ THUẬT NÀY CODE Ở TRÊN GỌI LÀ IoC, ĐẢO NGƯỢC VIỆC TẠO OBJECT 
            //SERVICE MẤT BỚT QUYỀN, TRAO BỚT QUYỀN, ĐẢO QUYỀN KIỂM SOÁT DEPENDENCY
            //Inversion of Control

            //Springboot thay main kiểm soát, tiêm chính dependency cho class và 2 thằng này đc gọi là IoC container
            //2 thằng Spring, spring boot gọi là IoC Container 
        }
    }
}
