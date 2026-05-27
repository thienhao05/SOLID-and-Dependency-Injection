Dependency Injection (DI) — Notes based on SRP examples

Mục tiêu
- Giải thích các khái niệm DI, IoC, tight/loose coupling và các cách tiêm dependency, dựa trên mã nguồn trong thư mục SRP.

1. Tổng quan
- IoC (Inversion of Control): đảo ngược quyền tạo/kiểm soát dependency — class chính không trực tiếp new dependency.
- DI (Dependency Injection): cách hiện thực IoC bằng việc "tiêm" các dependency từ bên ngoài vào class chính.

2. Tight coupling (Ví dụ: DI.TightCoupling)
- Mô tả: class UserService tự new các dependency (UserRepo, EmailSender).
- File tham khảo: DI.TightCoupling\UserService.cs, DI.TightCouplingController\Program.cs
- Hệ quả: khó thay thế, khó test, phải sửa Service khi đổi cách gửi (Email -> SMS).

3. Loose coupling (Ví dụ: DI.LooseCoupling)
- Mô tả: dependency được tạo bên ngoài rồi truyền vào Service (constructor injection trong ví dụ).
- File tham khảo: DI.LooseCoupling\UserService.cs, DI.LooseCouplingController\Program.cs
- Lợi ích: dễ mock/test, dễ thay thế implementation, tôn trọng SRP (mỗi class 1 nhiệm vụ).

4. Các cách tiêm dependency
- Constructor injection (đã minh họa trong DI.LooseCoupling\UserService.cs): an toàn, rõ ràng, khuyến nghị.
- Setter injection: truyền qua phương thức set; tùy chọn khi dependency có thể optional.
- Field injection: gán trực tiếp vào field (thường dùng framework, ít khuyến nghị vì vi phạm encapsulation).

5. IoC Container
- Ý tưởng: nơi chịu trách nhiệm tạo và quản lý dependency, rồi tiêm vào các class (ví dụ: Spring IoC container).
- Trong mã demo: Program.Main đóng vai controller/container đơn giản, tạo dependency và truyền vào Service.

6. Mối liên hệ với SRP (Single Responsibility Principle)
- SRP khuyến khích tách trách nhiệm: UserRepo lo CRUD, EmailSender lo gửi mail, UserService xử lý business.
- Việc tách rõ giúp DI trở nên hiệu quả: mỗi dependency là một service chuyên trách dễ bị thay thế hoặc mock.

7. Ghi chú thực hành
- Khi muốn đổi implementation (Email -> SMS), chỉ cần tạo class mới (SmsSender implements same API) rồi tiêm vào Service, không sửa Service nếu dùng DI đúng.
- Ưu tiên constructor injection cho dependencies bắt buộc.

Tệp code tham chiếu
- DI.TightCoupling\UserService.cs — tight coupling bằng new bên trong.
- DI.TightCoupling\EmailSender.cs — implementation gửi mail (dependency cứng).
- DI.TightCouplingController\Program.cs — demo tạo service có sẵn dependency bên trong.
- DI.LooseCoupling\UserService.cs — constructor injection demo.
- DI.LooseCoupling\EmailSender.cs — implementation gửi mail (dùng khi tiêm vào Service).
- DI.LooseCouplingController\Program.cs — demo tạo dependency ngoài và truyền vào (như IoC container đơn giản).

Kết luận
- Mã trong SRP minh họa rõ ràng sự khác biệt giữa tight và loose coupling, các cách tiêm dependency, và lợi ích của DI/IoC khi kết hợp với SRP để tạo code dễ bảo trì và test.
